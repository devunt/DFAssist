using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace App
{
    partial class Network
    {
        struct IPPacket
        {
            public ProtocolFamily Version;
            public byte HeaderLength;
            public byte DifferentiatedServices;
            public byte Congestion;
            public ushort TotalLength;
            public ushort Identification;
            public byte Flags;
            public ushort FragmentOffset;
            private byte TTL;
            public ProtocolType Protocol;
            public short Checksum;

            public IPAddress SourceIPAddress;
            public IPAddress DestinationIPAddress;

            public byte[] Data;

            public bool IsValid;

            public IPPacket(byte[] buffer)
            {
                try
                {
                    byte versionAndHeaderLength = buffer[0];
                    Version = (versionAndHeaderLength >> 4) == 4 ? ProtocolFamily.InterNetwork : ProtocolFamily.InterNetworkV6;
                    HeaderLength = (byte)((versionAndHeaderLength & 15) * 4); // 0b1111 = 15

                    byte dscpAndEcn = buffer[1];
                    DifferentiatedServices = (byte)(dscpAndEcn >> 2);
                    Congestion = (byte)(dscpAndEcn & 3); // 0b11 = 3

                    TotalLength = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 2));
                    Identification = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 4));

                    ushort flagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 6));
                    Flags = (byte)(flagsAndOffset >> 13);
                    FragmentOffset = (ushort)(flagsAndOffset & 8191); // 0b1111111111111 = 8191

                    TTL = buffer[8];
                    Protocol = (ProtocolType)buffer[9];
                    Checksum = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 10));

                    SourceIPAddress = new IPAddress(BitConverter.ToUInt32(buffer, 12));
                    DestinationIPAddress = new IPAddress(BitConverter.ToUInt32(buffer, 16));
                    
                    Data = buffer.Skip(HeaderLength).ToArray();

                    IsValid = true;
                }
                catch (Exception ex)
                {
                    Version = ProtocolFamily.Unknown;
                    HeaderLength = 0;
                    DifferentiatedServices = 0;
                    Congestion = 0;
                    TotalLength = 0;
                    Identification = 0;
                    Flags = 0;
                    FragmentOffset = 0;
                    TTL = 0;
                    Protocol = ProtocolType.Unknown;
                    Checksum = 0;

                    SourceIPAddress = null;
                    DestinationIPAddress = null;

                    Data = null;

                    IsValid = false;
                    Log.Ex(ex, "IP 패킷 파싱 에러");
                }
            }
        }

        struct TCPPacket
        {
            public ushort SourcePort;
            public ushort DestinationPort;
            public uint SequenceNumber;
            public uint AcknowledgementNumber;
            public byte DataOffset;
            public TCPFlags Flags;
            public ushort Window;
            public short Checksum;
            public ushort UrgentPointer;

            public byte[] Payload;

            public bool IsValid;

            public TCPPacket(byte[] buffer)
            {

                try
                {
                    SourcePort = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                    DestinationPort = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 2));
                    SequenceNumber = (uint)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
                    AcknowledgementNumber = (uint)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));

                    ushort offsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 12));
                    DataOffset = (byte)((offsetAndFlags >> 12) * 4);
                    Flags = (TCPFlags)(offsetAndFlags & 511); // 0b111111111 = 511

                    Window = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 14));
                    Checksum = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 16));
                    UrgentPointer = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 18));

                    Payload = buffer.Skip(DataOffset).ToArray();

                    IsValid = true;
                }
                catch (Exception ex)
                {
                    SourcePort = 0;
                    DestinationPort = 0;
                    SequenceNumber = 0;
                    AcknowledgementNumber = 0;
                    DataOffset = 0;
                    Flags = TCPFlags.NONE;
                    Window = 0;
                    Checksum = 0;
                    UrgentPointer = 0;

                    Payload = null;

                    IsValid = false;

                    Log.Ex(ex, "TCP 패킷 파싱 에러");
                }
            }
        }

        [Flags]
        public enum TCPFlags
        {
            NONE = 0,
            FIN = 1,
            SYN = 2,
            RST = 4,
            PSH = 8,
            ACK = 16,
            URG = 32,
            ECE = 64,
            CWR = 128,
            NS = 256,
        }
    }
}
