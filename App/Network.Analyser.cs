using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    partial class Network
    {
        enum DFState
        {
            IDLE,
            RUNNING,
            MATCHED,
        }

        DFState state = DFState.IDLE;


        private void AnalyseFFIXPacket(byte[] payload)
        {
            if (payload.Length <= 40)
            {
                return;
            }

            ushort type = BitConverter.ToUInt16(payload, 0);
            if (type != 0x5252)
            {
                return;
            }

            MemoryStream messages = new MemoryStream();
            using (MemoryStream stream = new MemoryStream(payload))
            {
                stream.Seek(40, SeekOrigin.Begin);

                if (payload[33] == 0x00)
                {
                    StreamCopy(stream, messages);
                }
                else {
                    stream.Seek(2, SeekOrigin.Current); // .Net DeflateStream 버그 (앞 2바이트 강제 무시)

                    using (DeflateStream z = new DeflateStream(stream, CompressionMode.Decompress))
                    {
                        StreamCopy(z, messages);
                    }
                }
            }
            messages.Seek(0, SeekOrigin.Begin);

            ushort messageCount = BitConverter.ToUInt16(payload, 30);
            for (int i = 0; i < messageCount; i++)
            {
                try
                {
                    var buffer = new byte[4];
                    var read = messages.Read(buffer, 0, 4);
                    if (read < 4)
                    {
                        if (read != 0)
                        {
                            Log.E("메시지 처리 요청중 길이 에러 발생함: {0}, {1}/{2}", read, i, messageCount);
                        }
                        break;
                    }
                    var length = BitConverter.ToInt32(buffer, 0);

                    var message = new byte[length];
                    messages.Seek(-4, SeekOrigin.Current);
                    messages.Read(message, 0, length);

                    HandleMessage(message);
                }
                catch (Exception ex)
                {
                    Log.Ex(ex, "메시지 처리 요청중 에러 발생함");
                }
            }
        }

        private void HandleMessage(byte[] message)
        {
            try
            {
                if (message.Length < 32)
                {
                    return;
                }

                var opcode = BitConverter.ToInt16(message, 18);
                var data = new byte[message.Length - 32];
                Array.Copy(message, 32, data, 0, message.Length - 32);

                if (opcode == 0x006C)
                {
                    var code = BitConverter.ToUInt16(data, 12);
                }
                else if (opcode == 0x0074)
                {
                    var instances = new List<Instance>();

                    for (int i = 0; i < 5; i++)
                    {
                        var code = BitConverter.ToUInt16(data, 48 + (i * 2));
                        if (code == 0)
                        {
                            break;
                        }
                        instances.Add(InstanceList.GetInstance(code));
                    }

                    mainForm.overlayForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.overlayForm.SetDutyCount(instances.Count);
                    });

                    state = DFState.RUNNING;
                    Log.I("DFAN: 매칭 시작됨 [{0}]", string.Join(", ", instances.Select(x => x.Name).ToArray()));
                }
                else if (opcode == 0x02DE)
                {
                    var code = BitConverter.ToUInt16(data, 0);
                    var status = data[4];
                    var tank = data[5];
                    var dps = data[6];
                    var healer = data[7];

                    var instance = InstanceList.GetInstance(code);

                    if (status == 4)
                    {
                        // 매칭 뒤 참가자 확인용 패킷
                        // 현재로서는 처리 계획 없음
                    }
                    else
                    {
                        mainForm.overlayForm.Invoke((MethodInvoker)delegate
                        {
                            mainForm.overlayForm.SetDutyStatus(instance, tank, dps, healer);
                        });

                        state = DFState.RUNNING;
                    }

                    Log.I("DFAN: 매칭 상태 업데이트됨 [{0}, {1}/{2}, {3}/{4}, {5}/{6}]",
                        instance.Name, tank, instance.Tank, healer, instance.Healer, dps, instance.DPS);
                }
                else if (opcode == 0x0338)
                {
                    var code = BitConverter.ToUInt16(data, 4);

                    var instance = InstanceList.GetInstance(code);

                    mainForm.overlayForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.overlayForm.SetDutyAsMatched(instance);
                    });

                    state = DFState.MATCHED;
                    Log.S("DFAN: 매칭됨 [{0}]", instance.Name);
                }
                else if (opcode == 0x006F || opcode == 0x0070)
                {
                    mainForm.overlayForm.Invoke((MethodInvoker)delegate
                    {
                        mainForm.overlayForm.CancelDutyFinder();
                    });

                    state = DFState.IDLE;
                    Log.E("DFAP: 매칭 중지됨");
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "메시지 처리중 에러 발생함");
            }
        }

        private void StreamCopy(Stream source, Stream destination)
        {
            byte[] buffer = new byte[16 * 1024];
            int bytesRead;

            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                destination.Write(buffer, 0, bytesRead);
            }
        }
    }
}
