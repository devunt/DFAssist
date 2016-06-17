using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using NetFwTypeLib;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace App
{
    partial class Network
    {
        [DllImport("Iphlpapi.dll", SetLastError = true)]
        public static extern uint GetExtendedTcpTable(IntPtr tcpTable, ref int tcpTableLength, bool sort, AddressFamily ipVersion, int tcpTableType, int reserved);

        public const int TCP_TABLE_OWNER_PID_CONNECTIONS = 4;
        public readonly byte[] RCVALL_IPLEVEL = new byte[4] { 3, 0, 0, 0 };

        [StructLayout(LayoutKind.Sequential)]
        public struct TcpTable
        {
            public uint length;
            public TcpRow row;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TcpRow
        {
            public TcpState state;
            public uint localAddr;
            public uint localPort;
            public uint remoteAddr;
            public uint remotePort;
            public uint owningPid;
        }

        private List<Connection> connections;
        private string exePath;
        private MainForm mainForm;
        private Socket socket;
        private byte[] recvBuffer = new byte[0x8000];
        private bool isRunning = false;

        public Network(MainForm mainForm)
        {
            exePath = Process.GetCurrentProcess().MainModule.FileName;
            this.mainForm = mainForm;
        }

        public void StartCapture()
        {
            try
            {
                Log.I("N: 시작중...");

                if (isRunning)
                {
                    Log.E("N: 이미 시작되어 있음");
                    return;
                }

                connections = GetConnections();

                if (connections.Count == 0)
                {
                    Log.E("N: 파이널판타지14 연결을 찾을 수 없음");
                    return;
                }

                Connection connection = connections[0]; // TODO: FF14 게임 서버가 맞는지 체크하기

                Log.S("N: 파이널판타지14 연결을 찾음: {0}", connection.localEndPoint.Address.ToString());

                RegisterToFirewall();

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                socket.Bind(new IPEndPoint(connection.localEndPoint.Address, 0));
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, true);
                socket.IOControl(IOControlCode.ReceiveAll, RCVALL_IPLEVEL, null);

                socket.BeginReceive(recvBuffer, 0, recvBuffer.Length, 0, new AsyncCallback(OnReceive), null);
                isRunning = true;

                mainForm.overlayForm.SetStatus(true);
                Log.S("N: 시작됨");
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "N: 시작하지 못함");
            }
        }

        public void StopCapture()
        {
            socket.Close();
            isRunning = false;
            mainForm.overlayForm.Invoke((MethodInvoker)delegate
            {
                mainForm.overlayForm.SetStatus(false);
            });
            Log.S("N: 중지됨");
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                int length = socket.EndReceive(ar);
                FilterAndProcessPacket(recvBuffer, length);
            }
            catch (ObjectDisposedException) { return; }
            catch (Exception ex)
            {
                Log.Ex(ex, "N: 패킷을 받는 중 에러 발생");
            }
            finally
            {
                try
                {
                    socket.BeginReceive(recvBuffer, 0, recvBuffer.Length, 0, new AsyncCallback(OnReceive), null);
                }
                catch (ObjectDisposedException) { }
                catch (Exception ex)
                {
                    Log.Ex(ex, "N: 시작하지 못함");
                }
            }
        }

        private void FilterAndProcessPacket(byte[] buffer, int length)
        {
            IPPacket ipPacket = new IPPacket(buffer, length);

            if (ipPacket.IsValid && (ipPacket.Protocol == ProtocolType.Tcp))
            {
                TCPPacket tcpPacket = new TCPPacket(ipPacket.Data, ipPacket.Data.Length);

                if (!tcpPacket.IsValid)
                {
                    return;
                }

                IPEndPoint sourceEndPoint = new IPEndPoint(ipPacket.SourceIPAddress, tcpPacket.SourcePort);
                IPEndPoint destinationEndPoint = new IPEndPoint(ipPacket.DestinationIPAddress, tcpPacket.DestinationPort);
                Connection connection = new Connection() { remoteEndPoint = sourceEndPoint, localEndPoint = destinationEndPoint };

                if (connections.Contains(connection))
                {
                    AnalyseFFIXPacket(tcpPacket.Payload);
                }
            }
        }

        private T GetInstance<T>(string typeName)
        {
            return (T)Activator.CreateInstance(Type.GetTypeFromProgID(typeName));
        }

        private void RegisterToFirewall()
        {
            try
            {
                var netFwMgr = GetInstance<INetFwMgr>("HNetCfg.FwMgr");
                var netAuthApps = netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications;

                bool isExists = false;
                foreach (var netAuthAppObject in netAuthApps)
                {
                    var netAuthApp = netAuthAppObject as INetFwAuthorizedApplication;
                    if ((netAuthApp != null) && (netAuthApp.ProcessImageFileName == exePath) && (netAuthApp.Enabled))
                    {
                        isExists = true;
                    }
                }

                if (!isExists)
                {
                    var netAuthApp = GetInstance<INetFwAuthorizedApplication>("HNetCfg.FwAuthorizedApplication");

                    netAuthApp.Enabled = true;
                    netAuthApp.Name = Global.FW_APPNAME;
                    netAuthApp.ProcessImageFileName = exePath;
                    netAuthApp.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;

                    netAuthApps.Add(netAuthApp);

                    Log.S("FW: 추가됨");
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "FW: 추가중 오류 발생함");
            }
        }

        private List<Connection> GetConnections()
        {
            var connections = new List<Connection>();

            if (mainForm.FFXIVProcess == null)
            {
                Log.E("N: 파이널판타지14 프로세스가 설정되지 않음");
                return connections;
            }

            IntPtr tcpTable = IntPtr.Zero;
            int tcpTableLength = 0;

            if (GetExtendedTcpTable(tcpTable, ref tcpTableLength, false, AddressFamily.InterNetwork, TCP_TABLE_OWNER_PID_CONNECTIONS, 0) != 0)
            {
                try
                {
                    tcpTable = Marshal.AllocHGlobal(tcpTableLength);
                    if (GetExtendedTcpTable(tcpTable, ref tcpTableLength, false, AddressFamily.InterNetwork, TCP_TABLE_OWNER_PID_CONNECTIONS, 0) == 0)
                    {
                        TcpTable table = (TcpTable)Marshal.PtrToStructure(tcpTable, typeof(TcpTable));

                        IntPtr rowPtr = new IntPtr(tcpTable.ToInt64() + Marshal.SizeOf(typeof(uint)));
                        for (int i = 0; i < table.length; i++)
                        {
                            TcpRow row = (TcpRow)Marshal.PtrToStructure(rowPtr, typeof(TcpRow));

                            if (row.owningPid == mainForm.FFXIVProcess.Id)
                            {
                                IPEndPoint local = new IPEndPoint(row.localAddr, (ushort)IPAddress.NetworkToHostOrder((short)row.localPort));
                                IPEndPoint remote = new IPEndPoint(row.remoteAddr, (ushort)IPAddress.NetworkToHostOrder((short)row.remotePort));

                                connections.Add(new Connection() { localEndPoint = local, remoteEndPoint = remote });
                            }

                            rowPtr = new IntPtr(rowPtr.ToInt64() + Marshal.SizeOf(typeof(TcpRow)));
                        }
                    }
                }
                finally
                {
                    if (tcpTable != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(tcpTable);
                    }
                }
            }

            return connections;
        }

        private class Connection
        {
            public IPEndPoint localEndPoint { get; set; }
            public IPEndPoint remoteEndPoint { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                Connection connection = obj as Connection;

                return (localEndPoint.Equals(connection.localEndPoint) && remoteEndPoint.Equals(connection.remoteEndPoint));
            }

            public override int GetHashCode()
            {
                return localEndPoint.GetHashCode() ^ remoteEndPoint.GetHashCode();
            }
        }

    }
}
