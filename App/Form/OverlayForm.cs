using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace App
{
    public partial class OverlayForm : Form
    {
        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        private static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

            [DllImport("user32.dll")]
            public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

            [DllImport("user32.dll")]
            public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        }

        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int WS_EX_TRANSPARENT = 0x20;

        private const int HWND_TOPMOST = -1;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;

        private const int EVENT_SYSTEM_FOREGROUND = 0x3;

        private const int WINEVENT_OUTOFCONTEXT = 0;
        private const int WINEVENT_SKIPOWNPROCESS = 2;

        private readonly WinEventDelegate m_hookProc;
        private readonly OverlayFormMove m_overlay;
        private Color accentColor;
        private Timer timer = null;
        private int blinkCount;
        private bool isOkay = false;
        private bool isRoulette = false;
        private bool isMatched = false;
        private byte[] memberCount = null;
        internal int currentZone = 0;
        private IntPtr m_eventHook;

        public int currentArea
        {
            get
            {
                return currentZone;
            }
            set
            {
                if (Settings.ShowOverlay)
                {
                    this.Invoke(() =>
                    {
                        if (Data.Instances.ContainsKey(value) && Settings.AutoOverlayHide)
                            Hide();
                        else
                            Show();
                    });
                }
            }
        }

        internal OverlayForm()
        {
            InitializeComponent();

            this.m_hookProc = new WinEventDelegate(this.WinEventProc);
            this.m_overlay = new OverlayFormMove(this);

            timer = new Timer();
            timer.Interval = Global.BLINK_INTERVAL;
            timer.Tick += Timer_Tick;
       }

        private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (idObject != 0 || idChild != 0)
                return;

            NativeMethods.SetWindowPos(this.Handle, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            NativeMethods.SetWindowPos(this.m_overlay.Handle, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED;
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                cp.ExStyle |= WS_EX_TRANSPARENT;
                return cp;
            }
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            SetStatus(false);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (++blinkCount > Global.BLINK_COUNT)
            {
                StopBlink();
            }
            else {
                if (BackColor == Color.Black)
                {
                    BackColor = accentColor;
                }
                else
                {
                    BackColor = Color.Black;
                }
            }
        }

        internal void SetStatus(bool isOkay)
        {
            this.Invoke(() =>
            {
                if (isOkay && !this.isOkay)
                {
                    m_overlay.BackColor = Color.FromArgb(0, 64, 0);

                    CancelDutyFinder();
                }
                else if (!isOkay)
                {
                    m_overlay.BackColor = Color.FromArgb(64, 0, 0);

                    CancelDutyFinderSync();
                    label_DutyName.Text = "클라이언트 통신 대기 중";
                }
                this.isOkay = isOkay;
            });
        }

        internal void SetDutyCount(int dutyCount)
        {
            isRoulette = false;
            this.Invoke(() =>
            {
                if (dutyCount < 0)
                {
                    label_DutyCount.Text = "임무 매칭 중";
                }
                else
                {
                    label_DutyCount.Text = string.Format("총 {0}개 임무 매칭 중", dutyCount);
                }
            });
        }

        internal void SetDutyStatus(Instance instance, byte tank, byte dps, byte healer)
        {
            isMatched = false;
            memberCount = null;
            this.Invoke(() =>
            {
                if (instance.PvP)
                {
                    label_DutyName.Text = instance.Name;
                    if (tank != 0 || healer != 0 || dps != 0)
                    {
                        label_DutyStatus.Text = string.Format("파티 매칭 중: {0} / {1} / {2}", tank, healer, dps);
                    }
                    else
                    {
                        label_DutyStatus.Text = "매칭 대기 중";
                    }
                }
                else if (isRoulette)
                {
                    if (tank == 255) // 순번 대기
                    {
                        label_DutyStatus.Text = "매칭 대기 중";
                    }
                    else // TODO: 순번이 1번일 때?
                    {
                        label_DutyStatus.Text = string.Format("대기 순번: {0}", tank + 1);
                    }
                }
                else
                {
                    label_DutyName.Text = instance.Name;
                    label_DutyStatus.Text = string.Format("{0}/{3}    {1}/{4}    {2}/{5}", tank, healer, dps, instance.Tank, instance.Healer, instance.DPS);
                }
            });
        }

        internal void SetRoulleteDuty(Roulette roulette)
        {
            isMatched = false;
            isRoulette = true;
            memberCount = null;
            this.Invoke(() =>
            {
                label_DutyCount.Text = "무작위 임무";
                label_DutyName.Text = roulette.Name;
                label_DutyStatus.Text = "매칭 대기 중";
            });
        }

        internal void SetDutyAsMatched(Instance instance)
        {
            this.Invoke(() =>
            {
                label_DutyCount.Text = "입장 확인 대기 중";
                label_DutyName.Text = instance.Name;
                label_DutyStatus.Text = "매칭!";

                accentColor = Color.Red;
                StartBlink();
            });
        }

        internal void SetMemberCount(byte tank, byte dps, byte healer)
        {
            memberCount = new byte[] { tank, dps, healer };
        }

        internal void SetConfirmStatus(Instance instance, byte tank, byte dps, byte healer)
        {
            if (isMatched) return;
            if (memberCount == null) // fallback
            {
                memberCount = new byte[] { instance.Tank, instance.DPS, instance.Healer };
            }

            this.Invoke(() =>
            {
                label_DutyCount.Text = "입장 확인 중";
                label_DutyStatus.Text = string.Format("{0}/{3}    {1}/{4}    {2}/{5}", tank, healer, dps, memberCount[0], memberCount[2], memberCount[1]);
            });
        }

        internal void SetFATEAsAppeared(FATE fate)
        {
            this.Invoke(() =>
            {
                // label_DutyCount.Text = Data.GetArea(fate.Zone).Name;
                label_DutyCount.Text = "";
                label_DutyName.Text = fate.Name;
                label_DutyStatus.Text = "돌발 임무 발생!";

                accentColor = Color.DarkOrange;
                StartBlink();
            });
        }

        internal void CancelDutyFinder()
        {
            this.Invoke(CancelDutyFinderSync);
        }

        internal void CancelDutyFinderSync()
        {
            isMatched = true;
            StopBlink();

            label_DutyCount.Text = "";
            label_DutyName.Text = "매칭 중인 임무 없음";
            label_DutyStatus.Text = "";
        }

        internal void ResetFormLocation()
        {
            this.m_overlay.CenterToScreen();
        }

        internal void StartBlink()
        {
            blinkCount = 0;
            timer.Start();
        }

        internal void StopBlink()
        {
            timer.Stop();
            BackColor = Color.Black;

            if (accentColor == Color.DarkOrange) // 현재 타이머가 돌발이면
            {
                accentColor = Color.Black;

                // 내용을 비움
                CancelDutyFinder();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            NativeMethods.UnhookWinEvent(this.m_eventHook);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
            {
                this.m_overlay.Show();
                this.m_overlay.Width  = this.Width - 10;
                this.m_overlay.Height = this.Height;
                m_eventHook = NativeMethods.SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, this.m_hookProc, 0, 0, WINEVENT_OUTOFCONTEXT | WINEVENT_SKIPOWNPROCESS);
            }
            else
            {
                this.m_overlay.Hide();
                NativeMethods.UnhookWinEvent(m_eventHook);
            }
        }
    }
}
