using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace App
{
    public partial class OverlayFormMove : Form
    {
        static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
        }

        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TOOLWINDOW = 0x80;

        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        
        private readonly Form m_parent;
        internal OverlayFormMove(Form parent)
        {
            InitializeComponent();
            
            this.Width = 10;

            this.m_parent = parent;
        }

        public new void CenterToScreen()
        {
            base.CenterToScreen();
        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED;
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, WM_NCLBUTTONDOWN, new IntPtr(HT_CAPTION), IntPtr.Zero);
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            this.m_parent.Left = this.Left + 10;
            this.m_parent.Top  = this.Top;

            Settings.OverlayX = Location.X;
            Settings.OverlayY = Location.Y;
            Settings.Save();
        }

        protected override void OnResize(EventArgs e)
        {
            this.Width = 10;
        }
    }
}
