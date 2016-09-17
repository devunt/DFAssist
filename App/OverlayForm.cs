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

        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TOOLWINDOW = 0x80;
        const int WS_EX_TRANSPARENT = 0x20;

        const int HWND_TOPMOST = -1;
        const int SWP_NOMOVE = 0x2;
        const int SWP_NOSIZE = 0x1;

        const int EVENT_SYSTEM_FOREGROUND = 0x3;

        const int WINEVENT_OUTOFCONTEXT = 0;
        const int WINEVENT_SKIPOWNPROCESS = 2;
        
        readonly WinEventDelegate m_hookProc;
        readonly OverlayFormMove m_overlay;
        Color accentColor;
        Timer timer = null;
        int blinkCount;
        bool isOkay = false;
        internal int currentZone = 0;
        IntPtr m_eventHook;

        public int currentArea
        {
            get
            {
                return currentZone;
            }
            set
            {
                this.Invoke(() =>
                {
                    if (Data.GetIsDuty(value) && Settings.AutoOverlayHide)
                        Hide();
                    else
                        Show();
                });
            }
        }

        internal OverlayForm()
        {
            InitializeComponent();

            this.m_overlay = new OverlayFormMove(this);
            this.m_hookProc = new WinEventDelegate(this.WinEventProc);

            timer = new Timer();
            timer.Interval = Global.BLINK_INTERVAL;
            timer.Tick += Timer_Tick;

            if (Settings.OverlayX == Global.OVERLAY_XY_UNSET || Settings.OverlayY == Global.OVERLAY_XY_UNSET)
            {
                StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                StartPosition = FormStartPosition.Manual;
                Location = new Point(Settings.OverlayX, Settings.OverlayY);
            }
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
                CreateParams cp = base.CreateParams;
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
                    label_DutyName.Text = "< 클라이언트 통신 대기중... >";
                }
                this.isOkay = isOkay;
            });
        }

        internal void SetDutyCount(int dutyCount)
        {
            this.Invoke(() =>
            {
                label_DutyCount.Text = string.Format("총 {0}개 임무 매칭중", dutyCount);
            });
        }

        internal void SetDutyStatus(Instance instance, byte tank, byte dps, byte healer)
        {
            if (tank == 0 && dps == 0 && healer == 0)
                this.Invoke(() =>
                {
                    label_DutyCount.Text = "무작위 임무";
                    label_DutyName.Text = "서버 예약";
                    label_DutyStatus.Text = "";
                });
            else
                this.Invoke(() =>
                {
                    label_DutyName.Text = string.Format("< {0} >", instance.Name);
                    label_DutyStatus.Text = string.Format("{0}/{3}    {1}/{4}    {2}/{5}", tank, healer, dps, instance.Tank, instance.Healer, instance.DPS);
                });
        }

        internal void SetDutyAsMatched(Instance instance)
        {
            this.Invoke(() =>
            {
                label_DutyCount.Text = "입장 확인 대기중";
                label_DutyName.Text = string.Format("< {0} >", instance.Name);
                label_DutyStatus.Text = "매칭!";

                accentColor = Color.Red;
                StartBlink();
            });
        }

        internal void SetFATEAsAppeared(FATE fate)
        {
            this.Invoke(() =>
            {
                label_DutyCount.Text = Data.GetArea(fate.Zone).Name;
                label_DutyName.Text = string.Format("< {0} >", fate.Name);
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
            StopBlink();

            label_DutyCount.Text = "";
            label_DutyName.Text = "< 매칭중인 임무 없음 >";
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
