using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace App
{
    partial class Network
    {
        private void AnalyseFFXIVPacket(byte[] payload)
        {
            try {
                while (true)
                {
                    if (payload.Length < 4)
                    {
                        break;
                    }

                    var type = BitConverter.ToUInt16(payload, 0);

                    if (type == 0x0000 || type == 0x5252)
                    {
                        if (payload.Length < 28)
                        {
                            break;
                        }

                        var length = BitConverter.ToInt32(payload, 24);

                        if ((length <= 0) || (payload.Length < length))
                        {
                            break;
                        }

                        using (MemoryStream messages = new MemoryStream())
                        {
                            using (MemoryStream stream = new MemoryStream(payload, 0, length))
                            {
                                stream.Seek(40, SeekOrigin.Begin);

                                if (payload[33] == 0x00)
                                {
                                    stream.CopyTo(messages);
                                }
                                else {
                                    stream.Seek(2, SeekOrigin.Current); // .Net DeflateStream 버그 (앞 2바이트 강제 무시)

                                    using (DeflateStream z = new DeflateStream(stream, CompressionMode.Decompress))
                                    {
                                        z.CopyTo(messages);
                                    }
                                }
                            }
                            messages.Seek(0, SeekOrigin.Begin);

                            var messageCount = BitConverter.ToUInt16(payload, 30);
                            for (int i = 0; i < messageCount; i++)
                            {
                                try
                                {
                                    var buffer = new byte[4];
                                    var read = messages.Read(buffer, 0, 4);
                                    if (read < 4)
                                    {
                                        Log.E("메시지 처리 요청중 길이 에러 발생함: {0}, {1}/{2}", read, i, messageCount);
                                    }
                                    var messageLength = BitConverter.ToInt32(buffer, 0);

                                    var message = new byte[messageLength];
                                    messages.Seek(-4, SeekOrigin.Current);
                                    messages.Read(message, 0, messageLength);

                                    HandleMessage(message);
                                }
                                catch (Exception ex)
                                {
                                    Log.Ex(ex, "메시지 처리 요청중 에러 발생함");
                                }
                            }
                        }

                        if (length < payload.Length)
                        {
                            // 더 처리해야 할 패킷이 남아 있음
                            payload = payload.Skip(length).ToArray();
                            continue;
                        }
                    }
                    else
                    {
                        // 앞쪽이 잘려서 오는 패킷 workaround
                        // 잘린 패킷 1개는 버리고 바로 다음 패킷부터 찾기...
                        // TODO: 버리는 패킷 없게 제대로 수정하기

                        for (var offset = 0; offset < (payload.Length - 2); offset++)
                        {
                            var possibleType = BitConverter.ToUInt16(payload, offset);
                            if (possibleType == 0x5252)
                            {
                                payload = payload.Skip(offset).ToArray();
                                AnalyseFFXIVPacket(payload);
                                break;
                            }
                        }
                    }

                    break;
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "패킷 처리중 에러 발생함");
            }
        }

        private void HandleMessage(byte[] message)
        {
            try
            {
                if (message.Length < 32)
                {
                    // type == 0x0000 이였던 메시지는 여기서 걸러짐
                    return;
                }

                var opcode = BitConverter.ToInt16(message, 18);
                var data = message.Skip(32).ToArray();

                //Log.D("opcode = {0:X}", opcode);
                if (opcode == 0x0143)
                {
                    mainForm.overlayForm.SetStatus(true);
                }
                else if (opcode == 0x006C)
                {
                    var code = BitConverter.ToUInt16(data, 12);

                    var instance = InstanceList.GetInstance(code);

                    mainForm.overlayForm.SetDutyCount(1);

                    Log.I("DFAN: 매칭 시작됨 [{0}]", instance.Name);
                }
                else if (opcode == 0x0074)
                {
                    var instances = new List<Instance>();

                    for (int i = 0; i < 5; i++)
                    {
                        var code = BitConverter.ToUInt16(data, 192 + (i * 2));
                        if (code == 0)
                        {
                            break;
                        }
                        instances.Add(InstanceList.GetInstance(code));
                    }

                    mainForm.overlayForm.SetDutyCount(instances.Count);

                    Log.I("DFAN: 매칭 시작됨 [{0}]", string.Join(", ", instances.Select(x => x.Name).ToArray()));
                }
                else if (opcode == 0x006F)
                {
                    mainForm.overlayForm.CancelDutyFinder();

                    Log.E("DFAN: 매칭 중지됨");
                }
                else if (opcode == 0x02DB)
                {
                    var status = data[4];

                    if (status == 3)
                    {
                        mainForm.overlayForm.CancelDutyFinder();

                        Log.E("DFAN: 매칭 중지됨");
                    }
                    else if (status == 6)
                    {
                        var code = BitConverter.ToUInt16(data, 0);

                        var instance = InstanceList.GetInstance(code);

                        // 인스턴스 입장함
                    }
                }
                else if (opcode == 0x02DE)
                {
                    var code = BitConverter.ToUInt16(data, 0);
                    var status = data[4];
                    var tank = data[5];
                    var dps = data[6];
                    var healer = data[7];

                    var instance = InstanceList.GetInstance(code);

                    if (status == 1)
                    {
                        mainForm.overlayForm.SetDutyStatus(instance, tank, dps, healer);
                    }
                    else if (status == 4)
                    {
                        // 매칭 뒤 참가자 확인 현황 패킷
                        // 현재로서는 처리 계획 없음
                    }

                    Log.I("DFAN: 매칭 상태 업데이트됨 [{0}, {1}/{2}, {3}/{4}, {5}/{6}]",
                        instance.Name, tank, instance.Tank, healer, instance.Healer, dps, instance.DPS);
                }
                else if (opcode == 0x0338)
                {
                    var code = BitConverter.ToUInt16(data, 4);

                    var instance = InstanceList.GetInstance(code);

                    mainForm.overlayForm.SetDutyAsMatched(instance);

                    if (Settings.TwitterEnabled)
                    {
                        Api.Tweet("< {0} > 매칭!", instance.Name);
                    }

                    Log.S("DFAN: 매칭됨 [{0}]", instance.Name);

                    // TODO: 랜덤 매칭에서 누군가가 매칭을 취소했을 경우
                    // 매칭이 재시작되는 패킷 찾아내기
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "메시지 처리중 에러 발생함");
            }
        }
    }
}
