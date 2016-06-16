using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace App
{
    class Communicator
    {
        enum DFState
        {
            IDLE,
            WAITING,
            MATCHED,
        }

        DFState state;
        MainForm mainForm;
        bool isConnectionAlive;

        public Communicator(MainForm mainForm)
        {
            state = DFState.IDLE;
            this.mainForm = mainForm;
            isConnectionAlive = false;

            Thread thread = new Thread(new ThreadStart(Task));
            thread.IsBackground = true;
            thread.Start();
        }

        private void Task()
        {
            Log.I("DFAP: 서버 시작중...");
            var server = new NamedPipeServerStream("FFXIV_DX11_DFA_PIPE", PipeDirection.In);
            Log.S("DFAP: 서버 시작됨");

            Log.I("클라이언트 연결 대기중...");
            server.WaitForConnection();
            Log.S("DFAP: 클라이언트 연결됨");

            while (true)
            {
                var lenBytes = new byte[4];
                server.Read(lenBytes, 0, 4);
                var length = BitConverter.ToInt32(lenBytes, 0);

                if (length == 0)
                {
                    break;
                }

                var data = new byte[length];
                server.Read(data, 0, length);

                int type = data[0];
                if (type == 1)
                {
                    if (!isConnectionAlive)
                    {
                        isConnectionAlive = true;
                        mainForm.overlayForm.Invoke((MethodInvoker)delegate
                        {
                            mainForm.overlayForm.SetStatus(true);
                        });
                    }

                    //Log.D("DFAP: PING");
                }
                else if (type == 2)
                {
                    var instances = new List<Instance>();
                    for (int i = 0; i < 5; i++)
                    {
                        short code = BitConverter.ToInt16(data, 1 + (i * 2));
                        if (code == 0)
                        {
                            break;
                        }
                        instances.Add(InstanceList.GetInstance(code));
                    }

                    mainForm.overlayForm.Invoke((MethodInvoker)delegate {
                        mainForm.overlayForm.SetDutyCount(instances.Count);
                    });

                    state = DFState.WAITING;
                    Log.I("DFAP: 매칭 시작됨 [{0}]", string.Join(", ", instances.Select(x => x.Name).ToArray()));
                }
                else if (type == 3)
                {
                    short code = BitConverter.ToInt16(data, 1);
                    byte tank = data[3];
                    byte dps = data[4];
                    byte healer = data[5];

                    var instance = InstanceList.GetInstance(code);

                    if (state == DFState.WAITING) // 패킷 순서 버그
                    {
                        mainForm.overlayForm.Invoke((MethodInvoker)delegate
                        {
                            mainForm.overlayForm.SetDutyStatus(instance, tank, dps, healer);
                        });
                    }

                    state = DFState.WAITING;

                    Log.I("DFAP: 매칭 상태 업데이트됨 [{0}, {1}/{2}, {3}/{4}, {5}/{6}]",
                        instance.Name, tank, instance.Tank, healer, instance.Healer, dps, instance.DPS);
                }
                else if (type == 4)
                {
                    short code = BitConverter.ToInt16(data, 1);

                    var instance = InstanceList.GetInstance(code);

                    mainForm.overlayForm.Invoke((MethodInvoker)delegate {
                        mainForm.overlayForm.SetDutyAsMatched(instance);
                    });

                    state = DFState.MATCHED;
                    Log.S("DFAP: 매칭됨 [{0}]", instance.Name);
                }
                else if (type == 5)
                {
                    mainForm.overlayForm.Invoke((MethodInvoker)delegate {
                        mainForm.overlayForm.CancelDutyFinder();
                    });

                    state = DFState.IDLE;
                    Log.E("DFAP: 매칭 중지됨");
                }
                else
                {   
                    Log.E("DFAP: 잘못된 데이터 받음");
                }
            }

            mainForm.overlayForm.Invoke((MethodInvoker)delegate {
                mainForm.overlayForm.SetStatus(false);
            });

            state = DFState.IDLE;
            Log.E("DFAP: 클라이언트 연결 종료됨");
        }
    }
}
