using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace App
{
    partial class Network
    {
        private State state = State.IDLE;

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
                                        break;
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
                if (message.Contains((byte)0x9D))
                {
                    //Log.B(message);
                }

                if (message.Length < 32)
                {
                    // type == 0x0000 이였던 메시지는 여기서 걸러짐
                    return;
                }

                var opcode = BitConverter.ToUInt16(message, 18);
                var data = message.Skip(32).ToArray();


                //Log.D("opcode = {0:X}", opcode);

                if (opcode == 0x0142)
                {
                    var type = data[0];

                    if (type == 0xCF)
                    {
                        int selfkey = BitConverter.ToInt32(message, 8);
                        int charkey = BitConverter.ToInt32(message, 40);

                        var code = BitConverter.ToUInt16(data, 16);
                        var zone = Data.GetZone(code);


                        byte teleMeasure = message[36];
                        // 01 : NPC를 통한 이동
                        // 02 : ?
                        // 03 : 일반이동
                        // 04 : 텔레포
                        // 05 : ?
                        // 06 : ?
                        // 07 : 데존
                        // 0C : 퇴장
                        // 0E : 도시 내 이동
                        // 13 : 거주구 이동
                        Log.D("{0:X2}:{1}", teleMeasure, DataStorage.getZoneString(code));
                        Log.B(message);
                        if (selfkey == charkey) // isSelf
                        {
                            Log.D("자신 지역이동 {0} : {1}", code, DataStorage.getZoneString(code));
                        }
                        else
                        {
                            Log.D("타인 지역이동 {0} : {1}", code, DataStorage.getZoneString(code));
                        }
                        //Log.D("[{0} ({1})] 지역 진입", zone.Name, code);
                    }
                }
                else if (opcode == 0x0143)
                {
                    var type = data[0];

                    if (type == 0x18)
                    {
                        mainForm.overlayForm.SetStatus(true);
                    }
                    else if (type == 0x9B)
                    {
                        var code = BitConverter.ToUInt16(data, 4);
                        var progress = data[8];

                        var fate = Data.GetFATE(code);

                        //Log.D("\"{0}\" 돌발 진행도 {1}%", fate.Name, progress);
                    }
                    else if (type == 0x79)
                    {
                        // 돌발 임무 종료 (지역 이동시 발생할 수 있는 모든 임무에 대해 전부 옴)

                        var code = BitConverter.ToUInt16(data, 4);
                        var status = BitConverter.ToUInt16(data, 28);

                        var fate = Data.GetFATE(code);

                        //Log.D("\"{0}\" 돌발 종료!", fate.Name);
                    }
                    else if (type == 0x74)
                    {
                        // 돌발 임무 발생 (지역 이동시에도 기존 돌발 목록이 옴)

                        var code = BitConverter.ToUInt16(data, 4);

                        var fate = Data.GetFATE(code);

                        if (Settings.FATEs.Contains(code))
                        {
                            mainForm.overlayForm.SetFATEAsAppeared(fate);

                            if (Settings.TwitterEnabled)
                            {
                                Api.Tweet("< {0} > 돌발 발생!", fate.Name);
                            }
                        }

                        Log.D("\"{0}\" 돌발 발생!", fate.Name);
                    }
                }
                else if (opcode == 0x006C)
                {
                    var code = BitConverter.ToUInt16(data, 12);

                    var instance = Data.GetInstance(code);

                    state = State.QUEUED;
                    mainForm.overlayForm.SetDutyCount(1);

                    Log.I("DFAN: 매칭 시작됨 (6C) [{0}]", instance.Name);
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
                        instances.Add(Data.GetInstance(code));
                    }

                    state = State.QUEUED;
                    mainForm.overlayForm.SetDutyCount(instances.Count);

                    Log.I("DFAN: 매칭 시작됨 (74) [{0}]", string.Join(", ", instances.Select(x => x.Name).ToArray()));
                }
                else if (opcode == 0x02DB)
                {
                    var status = data[4];

                    if (status == 3)
                    {
                        state = State.IDLE;
                        mainForm.overlayForm.CancelDutyFinder();

                        Log.E("DFAN: 매칭 중지됨 (2DB)");
                    }
                    else if (status == 6)
                    {
                        var code = BitConverter.ToUInt16(data, 0);

                        var instance = Data.GetInstance(code);

                        state = State.IDLE;
                        mainForm.overlayForm.CancelDutyFinder();

                        Log.I("DFAN: 입장함 [{0}]", instance.Name);
                    }
                }
                else if (opcode == 0x006F)
                {
                    var status = data[0];

                    if (status == 0)
                    {
                        // 플레이어가 매칭 참가 확인 창에서 취소를 누르거나 참가 확인 제한 시간이 초과됨
                        // 매칭 중단을 알리기 위해 상단 2DB status 3 패킷이 연이어 옴
                    }
                    if (status == 1)
                    {
                        // 플레이어가 매칭 참가 확인 창에서 확인을 누름
                        // 다른 매칭 인원들도 전부 확인을 눌렀을 경우 입장을 위해 상단 2DB status 6 패킷이 옴
                        mainForm.overlayForm.StopBlink();
                    }
                }
                else if (opcode == 0x02DE)
                {
                    var code = BitConverter.ToUInt16(data, 0);
                    var status = data[4];
                    var tank = data[5];
                    var dps = data[6];
                    var healer = data[7];

                    var instance = Data.GetInstance(code);

                    if (status == 1)
                    {
                        // 매칭 전 인원 현황 패킷

                        if (state == State.IDLE)
                        {
                            // 프로그램이 매칭 중간에 켜짐
                            state = State.QUEUED;
                            mainForm.overlayForm.SetDutyCount(1); // 임의로 1개로 설정함 (TODO: 알아낼 방법 있으면 정학히 나오게 수정하기)
                            mainForm.overlayForm.SetDutyStatus(instance, tank, dps, healer);
                        }
                        else if (state == State.QUEUED)
                        {
                            mainForm.overlayForm.SetDutyStatus(instance, tank, dps, healer);
                        }
                    }
                    else if (status == 4)
                    {
                        // 매칭 뒤 참가자 확인 현황 패킷
                    }

                    Log.I("DFAN: 매칭 상태 업데이트됨 [{0}, {1}, {2}/{3}, {4}/{5}, {6}/{7}]",
                        instance.Name, status, tank, instance.Tank, healer, instance.Healer, dps, instance.DPS);
                }
                else if (opcode == 0x0338)
                {
                    var code = BitConverter.ToUInt16(data, 4);

                    var instance = Data.GetInstance(code);

                    state = State.MATCHED;
                    mainForm.overlayForm.SetDutyAsMatched(instance);

                    if (Settings.TwitterEnabled)
                    {
                        Api.Tweet("< {0} > 매칭!", instance.Name);
                    }

                    Log.S("DFAN: 매칭됨 [{0}]", instance.Name);
                }

                // TODO: 매칭이 된 뒤에 다른 누군가가 참가 확인을 거부하거나
                // 제한시간 초과로 참가 확인이 취소됐을 경우 어떤 패킷이 오는지 알아내기
                // 매칭에서 누군가가 참가 확인을 안 누르는 상황을 재현하기 힘들어 찾아내지 못함...
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "메시지 처리중 에러 발생함");
            }
        }

        enum State
        {
            IDLE,
            QUEUED,
            MATCHED,
        }
    }
}
