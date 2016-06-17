using System.Net;
using System;
using System.IO;
using System.Net.Sockets;

namespace App
{
    partial class Network
    {
        class IPPacket
        {
            public ProtocolFamily Version { get; }
            public byte HeaderLength { get; }
            public byte DifferentiatedServices { get; }
            public byte Congestion { get; }
            public ushort TotalLength { get; }
            public ushort Identification { get; }
            public byte Flags { get; }
            public ushort FragmentOffset { get; }
            private byte TTL { get; }
            public ProtocolType Protocol { get; }
            public short Checksum { get; }

            public IPAddress SourceIPAddress;
            public IPAddress DestinationIPAddress;

            public byte[] Data { get; }

            public bool IsValid { get; }

            public IPPacket(byte[] buffer, int length)
            {

                try
                {
                    MemoryStream memoryStream = new MemoryStream(buffer, 0, length);
                    BinaryReader binaryReader = new BinaryReader(memoryStream);

                    byte versionAndHeaderLength = binaryReader.ReadByte();
                    Version = (versionAndHeaderLength >> 4) == 4 ? ProtocolFamily.InterNetwork : ProtocolFamily.InterNetworkV6;
                    HeaderLength = (byte)((versionAndHeaderLength & 15) * 4); // 0b1111 = 15

                    byte dscpAndEcn = binaryReader.ReadByte();
                    DifferentiatedServices = (byte)(dscpAndEcn >> 2);
                    Congestion = (byte)(dscpAndEcn & 3); // 0b11 = 3

                    TotalLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                    Identification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    ushort flagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                    Flags = (byte)(flagsAndOffset >> 13);
                    FragmentOffset = (ushort)(flagsAndOffset & 8191); // 0b1111111111111 = 8191

                    TTL = binaryReader.ReadByte();
                    Protocol = (ProtocolType)binaryReader.ReadByte();
                    Checksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    SourceIPAddress = new IPAddress(binaryReader.ReadUInt32());
                    DestinationIPAddress = new IPAddress(binaryReader.ReadUInt32());

                    Data = new byte[TotalLength - HeaderLength];
                    Array.Copy(buffer, HeaderLength, Data, 0, TotalLength - HeaderLength);

                    IsValid = true;
                }
                catch (Exception ex)
                {
                    IsValid = false;
                    Log.Ex(ex, "IP 패킷 파싱 에러: {0}");
                }
            }
        }

        class TCPPacket
        {
            public ushort SourcePort { get; }
            public ushort DestinationPort { get; }
            public uint SequenceNumber { get; }
            public uint AcknowledgementNumber { get; }
            public byte DataOffset { get; }
            public ushort Flags { get; }
            public ushort Window { get; }
            public short Checksum { get; }
            public ushort UrgentPointer { get; }

            public byte[] Payload;

            public bool IsValid { get; }

            public TCPPacket(byte[] buffer, int length)
            {
                try
                {
                    MemoryStream memoryStream = new MemoryStream(buffer, 0, length);
                    BinaryReader binaryReader = new BinaryReader(memoryStream);

                    SourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                    DestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                    SequenceNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
                    AcknowledgementNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());

                    ushort offsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                    DataOffset = (byte)((offsetAndFlags >> 12) * 4);
                    Flags = (ushort)(offsetAndFlags & 511); // 0b111111111 = 511

                    Window = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                    Checksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                    UrgentPointer = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    Payload = new byte[length - DataOffset];
                    Array.Copy(buffer, DataOffset, Payload, 0, length - DataOffset);

                    IsValid = true;
                }
                catch (Exception ex)
                {
                    IsValid = false;
                    Log.Ex(ex, "TCP 패킷 파싱 에러: {0}");
                }
            }
        }

        public enum TCPFlags
        {
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
