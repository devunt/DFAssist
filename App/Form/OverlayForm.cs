using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading.Tasks;

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
                    label_DutyName.SetLocalizedText("overlay-waiting-connection");
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
                    label_DutyCount.SetLocalizedText("overlay-duty-count-unknown");
                }
                else
                {
                    label_DutyCount.SetLocalizedText("overlay-duty-count", dutyCount);
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
                        label_DutyStatus.SetLocalizedText("overlay-queue-waiting-party", tank, healer, dps);
                    }
                    else
                    {
                        label_DutyStatus.SetLocalizedText("overlay-queue-waiting");
                    }
                }
                else if (isRoulette)
                {
                    if (tank == 255) // 순번 대기
                    {
                        label_DutyStatus.SetLocalizedText("overlay-queue-waiting");
                    }
                    else // TODO: 순번이 1번일 때?
                    {
                        label_DutyStatus.SetLocalizedText("overlay-queue-order", tank + 1);
                    }
                }
                else
                {
                    label_DutyName.Text = instance.Name;
                    label_DutyStatus.Text = $@"{tank}/{instance.Tank}    {healer}/{instance.Healer}    {dps}/{instance.DPS}";
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
                label_DutyCount.SetLocalizedText("overlay-roulette");
                label_DutyName.Text = roulette.Name;
                label_DutyStatus.SetLocalizedText("overlay-queue-waiting");
            });
        }

        internal void SetDutyAsMatched(Instance instance)
        {
            this.Invoke(() =>
            {
                label_DutyCount.SetLocalizedText("overlay-queue-waiting-confirm");
                label_DutyName.Text = instance.Name;
                label_DutyStatus.SetLocalizedText("overlay-queue-matched");

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
                label_DutyCount.SetLocalizedText("overlay-queue-confirming");
                label_DutyStatus.Text = $@"{tank}/{memberCount[0]}    {healer}/{memberCount[2]}    {dps}/{memberCount[1]}";
            });
        }

        internal void SetFATEAsOccured(FATE fate)
        {
            this.Invoke(() =>
            {
                label_DutyCount.Text = fate.Area.Name;
                label_DutyName.Text = fate.Name;
                label_DutyStatus.SetLocalizedText("overlay-fate-occured");

                accentColor = Color.DarkOrange;
                StartBlink();
            });
        }

        internal void CancelDutyFinder()
        {
            this.Invoke(CancelDutyFinderSync);

            if (Settings.autoHideOverlay)
                this.Hide();
        }

        internal void CancelDutyFinderSync()
        {
            isMatched = true;
            StopBlink();

            label_DutyCount.Text = "";
            label_DutyName.SetLocalizedText("overlay-not-queuing");
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

                // 오버레이 자동 숨기기 옵션에 숨겨주고
                if(Settings.autoHideOverlay)
                    this.Hide();

                // 내용을 비움
                CancelDutyFinder();
            }
        }

        internal void instances_callback(int code)
        {
            if (Settings.copyMacro)
            {
                Task.Factory.StartNew(() =>
                {
                    var instance = Data.GetInstance(code);
                    if (instance.Macro != null)
                    {
                        var respond = LMessageBox.Dialog(Localization.GetText("ui-settings-copymacro-dialog-text", instance.Name), Localization.GetText("ui-settings-copymacro-dialog-title"), MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                        if (respond == DialogResult.Yes)
                        {
                            this.Invoke(() => { Clipboard.SetDataObject(instance.Macro, true); });
                        }
                    }
                });
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
