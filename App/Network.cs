using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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

        private List<Connection> connections = new List<Connection>();
        private string exePath;
        private MainForm mainForm;
        private Socket socket;
        private byte[] recvBuffer = new byte[0x20000];
        internal bool IsRunning { get; private set; } = false;
        private object lockAnalyse = new object();

        internal Network(MainForm mainForm)
        {
            exePath = Process.GetCurrentProcess().MainModule.FileName;
            this.mainForm = mainForm;
        }

        internal void StartCapture(Process process)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Log.I("N: 시작중...");

                    if (IsRunning)
                    {
                        Log.E("N: 이미 시작되어 있음");
                        return;
                    }

                    UpdateGameConnections(process);

                    if (connections.Count < 2)
                    {
                        Log.E("N: 게임 서버 연결을 찾지 못했습니다");
                        return;
                    }

                    IPAddress localAddress = connections[0].localEndPoint.Address;

                    RegisterToFirewall();

                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                    socket.Bind(new IPEndPoint(localAddress, 0));
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, true);
                    socket.IOControl(IOControlCode.ReceiveAll, RCVALL_IPLEVEL, null);
                    socket.ReceiveBufferSize = recvBuffer.Length * 4;

                    socket.BeginReceive(recvBuffer, 0, recvBuffer.Length, 0, new AsyncCallback(OnReceive), null);
                    IsRunning = true;

                    mainForm.overlayForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.overlayForm.SetStatus(true);
                    });
                    Log.S("N: 시작됨");
                }
                catch (Exception ex)
                {
                    Log.Ex(ex, "N: 시작하지 못함");
                }
            });
        }

        internal void StopCapture()
        {
            try {
                if (!IsRunning)
                {
                    Log.E("N: 이미 중지되어 있음");
                    return;
                }

                socket.Close();
                connections.Clear();

                mainForm.overlayForm.Invoke((MethodInvoker)delegate
                {
                    mainForm.overlayForm.SetStatus(false);
                });
                Log.I("N: 중지 요청중...");
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "N: 중지하지 못함");
            }
        }

        internal void UpdateGameConnections(Process process)
        {
            if (connections.Count < 2)
            {
                var allConnections = GetConnections(process);
                var lobbyEndPoint = GetLobbyEndPoint(process);

                connections = allConnections.Where(x => !x.remoteEndPoint.Equals(lobbyEndPoint)).ToList();

                foreach (var connection in connections)
                {
                    Log.I("N: 게임서버 연결 감지: {0}", connection.ToString());
                }
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                var length = socket.EndReceive(ar);
                var buffer = recvBuffer.Take(length).ToArray();
                socket.BeginReceive(recvBuffer, 0, recvBuffer.Length, 0, new AsyncCallback(OnReceive), null);

                FilterAndProcessPacket(buffer);
            }
            catch (Exception ex) when (ex is ObjectDisposedException || ex is NullReferenceException)
            {
                IsRunning = false;
                socket = null;
                Log.S("N: 중지됨");
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "N: 패킷을 받는 중 에러 발생");
            }
        }

        private void FilterAndProcessPacket(byte[] buffer)
        {
            try {
                IPPacket ipPacket = new IPPacket(buffer);

                if (ipPacket.IsValid && (ipPacket.Protocol == ProtocolType.Tcp))
                {
                    TCPPacket tcpPacket = new TCPPacket(ipPacket.Data);

                    if (!tcpPacket.IsValid)
                    {
                        // 올바르지 못한 TCP 패킷
                        return;
                    }

                    if (!tcpPacket.Flags.HasFlag(TCPFlags.ACK | TCPFlags.PSH))
                    {
                        // 파판 서버에서 클라이언트로 보내주는 모든 TCP 패킷에는
                        // ACK와 PSH 플래그가 설정되어 있음을 이용해 필터링 부하를 낮춤
                        return;
                    }

                    IPEndPoint sourceEndPoint = new IPEndPoint(ipPacket.SourceIPAddress, tcpPacket.SourcePort);
                    IPEndPoint destinationEndPoint = new IPEndPoint(ipPacket.DestinationIPAddress, tcpPacket.DestinationPort);
                    Connection outgoingConnection = new Connection() { remoteEndPoint = sourceEndPoint, localEndPoint = destinationEndPoint };
                    Connection incomingConnection = new Connection() { localEndPoint = sourceEndPoint, remoteEndPoint = destinationEndPoint };

                    if (!connections.Contains(outgoingConnection) && !connections.Contains(incomingConnection))
                    {
                        // 파판 서버와 주고받는 패킷이 아님
                        return;
                    }

                    if (tcpPacket.Flags.HasFlag(TCPFlags.RST) || tcpPacket.Flags.HasFlag(TCPFlags.FIN))
                    {
                        // 연결 종료 발생. 네트워크 캡춰를 중지함
                        // connections.Remove(connection);
                        Log.E("게임서버와의 연결 종료됨");
                        StopCapture();
                    }

                    if (!connections.Contains(outgoingConnection))
                    {
                        // 받는 패킷이 아님
                        return;
                    }

                    // 파판 서버에서 오는 패킷이니 분석함
                    lock (lockAnalyse)
                    {
                        AnalyseFFXIVPacket(tcpPacket.Payload);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "패킷 필터링 중 에러 발생함");
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
                    netAuthApp.Name = Global.APPNAME;
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

        private IPEndPoint GetLobbyEndPoint(Process process)
        {
            IPEndPoint ipep = null;
            string lobbyHost = null;
            int lobbyPort = 0;

            try {
                using (var searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
                {
                    foreach (var @object in searcher.Get())
                    {
                        var commandline = @object["CommandLine"].ToString();
                        var argv = commandline.Split(' ');

                        foreach (var arg in argv)
                        {
                            var splitted = arg.Split('=');
                            if (splitted.Length == 2)
                            {
                                if (splitted[0] == "DEV.LobbyHost01")
                                {
                                    lobbyHost = splitted[1];
                                }
                                else if (splitted[0] == "DEV.LobbyPort01")
                                {
                                    lobbyPort = int.Parse(splitted[1]);
                                }
                            }
                        }
                    }
                }

                if ((lobbyHost != null) && (lobbyPort > 0))
                {
                    IPAddress address = Dns.GetHostAddresses(lobbyHost)[0];
                    ipep = new IPEndPoint(address, lobbyPort);
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "N: 로비 서버 정보를 받아오는 중 에러 발생함");
            }

            return ipep;
        }

        private List<Connection> GetConnections(Process process)
        {
            var connections = new List<Connection>();

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

                            if (row.owningPid == process.Id)
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

            public override string ToString()
            {
                return string.Format("{0} -> {1}", localEndPoint.ToString(), remoteEndPoint.ToString());
            }
        }

    }
}
