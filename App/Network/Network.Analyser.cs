﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace App
{
    internal partial class Network
    {
        private State state = State.IDLE;
        private int lastMember = 0;
        
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

                        if (length <= 0 || payload.Length < length)
                        {
                            break;
                        }

                        using (var messages = new MemoryStream(payload.Length))
                        {
                            using (var stream = new MemoryStream(payload, 0, length))
                            {
                                stream.Seek(40, SeekOrigin.Begin);

                                if (payload[33] == 0x00)
                                {
                                    stream.CopyTo(messages);
                                }
                                else {
                                    stream.Seek(2, SeekOrigin.Current); // .Net DeflateStream 버그 (앞 2바이트 강제 무시)

                                    using (var z = new DeflateStream(stream, CompressionMode.Decompress))
                                    {
                                        z.CopyTo(messages);
                                    }
                                }
                            }
                            messages.Seek(0, SeekOrigin.Begin);

                            var messageCount = BitConverter.ToUInt16(payload, 30);
                            for (var i = 0; i < messageCount; i++)
                            {
                                try
                                {
                                    var buffer = new byte[4];
                                    var read = messages.Read(buffer, 0, 4);
                                    if (read < 4)
                                    {
                                        Log.E("l-analyze-error-length", read, i, messageCount);
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
                                    Log.Ex(ex, "l-analyze-error-general");
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

                        for (var offset = 0; offset < payload.Length - 2; offset++)
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
                Log.Ex(ex, "l-analyze-error");
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

                mainForm.overlayForm.SetStatus(true);

                var opcode = BitConverter.ToUInt16(message, 18);

#if !DEBUG
                if (opcode != 0x0078 &&
                    opcode != 0x0079 &&
                    opcode != 0x0080 &&
                    opcode != 0x006C &&
                    opcode != 0x006F &&
                    opcode != 0x0121 &&
                    opcode != 0x0143 &&
                    opcode != 0x022F)
                    return;
#endif

                var data = message.Skip(32).ToArray();

                if (opcode == 0x022F)
                {
                    var code = BitConverter.ToInt16(data, 4);
                    var type = data[8];

                    Log.B(data);

                    if (type == 0x0B)
                    {
                        Log.I("l-field-instance-entered", Data.GetInstance(code).Name);
                    }
                    else if (type == 0x0C)
                    {
                        Log.I("l-field-instance-left");
                    }

                    if (Settings.ShowOverlay && Settings.AutoOverlayHide)
                    {
                        mainForm.overlayForm.Invoke(() =>
                        {
                            if (type == 0x0B)
                            {
                                mainForm.overlayForm.Hide();
                            }
                            else if (type == 0x0C)
                            {
                                mainForm.overlayForm.Show();
                            }
                        });
                    }
                }
                else if (opcode == 0x0143)
                {
                    var type = data[0];

                    if (type == 0x9B)
                    {
                        /*
                        var code = BitConverter.ToUInt16(data, 4);
                        var progress = data[8];

                        var fate = Data.GetFATE(code);

                        //Log.D("\"{0}\" 돌발 진행도 {1}%", fate.Name, progress);
                        */
                    }
                    else if (type == 0x79)
                    {
                        /*
                        // 돌발 임무 종료 (지역 이동시 발생할 수 있는 모든 임무에 대해 전부 옴)

                        var code = BitConverter.ToUInt16(data, 4);
                        var status = BitConverter.ToUInt16(data, 28);

                        var fate = Data.GetFATE(code);

                        //Log.D("\"{0}\" 돌발 종료!", fate.Name);
                        */
                    }
                    else if (type == 0x74)
                    {
                        // 돌발 임무 발생 (지역 이동시에도 기존 돌발 목록이 옴)

                        var code = BitConverter.ToUInt16(data, 4);

                        var fate = Data.GetFATE(code);

                        if (Settings.FATEs.Contains(code))
                        {
                            mainForm.overlayForm.SetFATEAsOccured(fate);
                            Log.I("l-fate-occured-info", fate.Name);

                            if (Settings.FlashWindow)
                            {
                                WinApi.FlashWindow(mainForm.FFXIVProcess);
                            }

                            if (Settings.TwitterEnabled)
                            {
                                WebApi.Tweet("tweet-fate-occured", fate.Name);
                            }
                        }
                    }
                }
                /*else if (opcode == 0x006C) // 3.5 cross-world 파티 참가하면 문제가 발생하는 부분.
                {
                    var code = BitConverter.ToUInt16(data, 192);

                    var instance = Data.GetInstance(code);

                    state = State.QUEUED;
                    mainForm.overlayForm.SetDutyCount(1);

                    Log.I("l-queue-started-general", instance.Name);
                }*/
                else if (opcode == 0x0078)
                {
                    var status = data[0];
                    var reason = data[4];

                    if (status == 0)
                    {
                        state = State.QUEUED;

                        var rouletteCode = data[20];

                        if (rouletteCode != 0 && (data[15] == 0 || data[15] == 64)) //무작위 임무 신청, 한국서버/글로벌 서버
                        {
                            var roulette = Data.GetRoulette(rouletteCode);
                            mainForm.overlayForm.SetRoulleteDuty(roulette);
                            Log.I("l-queue-started-roulette", roulette.Name);
                        }
                        else //특정 임무 신청
                        {
                            var instances = new List<Instance>();

                            for (int i = 0; i < 5; i++)
                            {
                                var code = BitConverter.ToUInt16(data, 22 + (i * 2));
                                if (code == 0)
                                {
                                    break;
                                }
                                instances.Add(Data.GetInstance(code));
                            }

                            if (!instances.Any())
                            {
                                return;
                            }

                            mainForm.overlayForm.SetDutyCount(instances.Count);

                            Log.I("l-queue-started-general",
                                string.Join(", ", instances.Select(x => x.Name).ToArray()));
                        }
                    }
                    else if (status == 3)
                    {
                        state = reason == 8 ? State.QUEUED : State.IDLE;
                        mainForm.overlayForm.CancelDutyFinder();

                        Log.E("l-queue-stopped");
                    }
                    else if (status == 6)
                    {
                        state = State.IDLE;
                        mainForm.overlayForm.CancelDutyFinder();

                        Log.I("l-queue-entered");
                    }
                    else if (status == 4) //글섭에서 매칭 잡혔을 때 출력
                    {
                        var roulette = data[20];
                        var code = BitConverter.ToUInt16(data, 22);

                        Instance instance;

                        if (!Settings.CheatRoulette && roulette != 0)
                        {
                            instance = new Instance { Name = Data.GetRoulette(roulette).Name };
                        }
                        else
                        {
                            instance = Data.GetInstance(code);
                        }

                        state = State.MATCHED;
                        mainForm.overlayForm.SetDutyAsMatched(instance);

                        if (Settings.FlashWindow)
                        {
                            WinApi.FlashWindow(mainForm.FFXIVProcess);
                        }
                        if (Settings.PlaySound && Settings.SoundLocation != "" && System.IO.File.Exists(Settings.SoundLocation))
                        {
                            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Settings.SoundLocation);
                            player.Play();
                        }
                        if (!Settings.ShowOverlay)
                        {
                            mainForm.ShowNotification("notification-queue-matched", instance.Name);
                        }

                        if (Settings.TwitterEnabled)
                        {
                            WebApi.Tweet("tweet-queue-matched", instance.Name);
                        }

                        Log.S("l-queue-matched", instance.Name);
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
                else if (opcode == 0x0121) //글로벌 서버
                {
                    var status = data[5];

                    if (status == 128)
                    {
                        // 매칭 참가 신청 확인 창에서 확인을 누름
                        mainForm.overlayForm.StopBlink();
                    }
                }
                else if (opcode == 0x0079)
                {
                    var code = BitConverter.ToUInt16(data, 0);
                    var status = data[4];
                    var tank = data[5];
                    var dps = data[6];
                    var healer = data[7];

                    var instance = Data.GetInstance(code);

                    if (status == 1)
                    {
                        // 인원 현황 패킷
                        var member = tank * 10000 + dps * 100 + healer;

                        if (state == State.MATCHED && lastMember != member)
                        {
                            // 매칭도중일 때 인원 현황 패킷이 오고 마지막 인원 정보와 다른 경우에 누군가에 의해 큐가 취소된 경우.
                            state = State.QUEUED;
                            mainForm.overlayForm.CancelDutyFinder();
                        }
                        else if (state == State.IDLE)
                        {
                            // 프로그램이 매칭 중간에 켜짐
                            state = State.QUEUED;
                            mainForm.overlayForm.SetDutyCount(-1); // 알 수 없음으로 설정함 (TODO: 알아낼 방법 있으면 정확히 나오게 수정하기)
                            mainForm.overlayForm.SetDutyStatus(instance, tank, dps, healer);
                        }
                        else if (state == State.QUEUED)
                        {
                            mainForm.overlayForm.SetDutyStatus(instance, tank, dps, healer);
                        }

                        lastMember = member;
                    }
                    else if (status == 2)
                    {
                        // 현재 매칭된 파티의 역할별 인원 수 정보
                        // 조율 해제 상태여도 역할별로 정확히 날아옴
                        mainForm.overlayForm.SetMemberCount(tank, dps, healer);
                        return;
                    }
                    else if (status == 4)
                    {
                        // 매칭 뒤 참가자 확인 현황 패킷
                        mainForm.overlayForm.SetConfirmStatus(instance, tank, dps, healer);
                    }
                    Log.I("l-queue-updated", instance.Name, status, tank, instance.Tank, healer, instance.Healer, dps,
                        instance.DPS);
                }
                else if (opcode == 0x0080)
                {
                    var roulette = data[2];
                    var code = BitConverter.ToUInt16(data, 4);

                    Instance instance;

                    if (!Settings.CheatRoulette && roulette != 0)
                    {
                        instance = new Instance { Name = Data.GetRoulette(roulette).Name };
                    }
                    else
                    {
                        instance = Data.GetInstance(code);
                    }

                    state = State.MATCHED;
                    mainForm.overlayForm.SetDutyAsMatched(instance);

                    if (Settings.FlashWindow)
                    {
                        WinApi.FlashWindow(mainForm.FFXIVProcess);
                    }

                    if (!Settings.ShowOverlay)
                    {
                        mainForm.ShowNotification("notification-queue-matched", instance.Name);
                    }

                    if (Settings.TwitterEnabled)
                    {
                        WebApi.Tweet("tweet-queue-matched", instance.Name);
                    }

                    Log.S("l-queue-matched", instance.Name);
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "l-analyze-error-general");
            }
        }

        private enum State
        {
            IDLE,
            QUEUED,
            MATCHED,
        }
    }
}
