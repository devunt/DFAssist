using System.Windows.Forms;

namespace App
{
    public partial class UpdaterForm : Form
    {
        /*
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        */

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        internal UpdaterForm()
        {
            InitializeComponent();
        }

        private void label_Updating_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //ReleaseCapture();
                //SendMessage(Handle, WM_NCLBUTTONDOWN, new IntPtr(HT_CAPTION), IntPtr.Zero);
            }
        }

        internal void SetVersion(string version)
        {
            label_Updating.SetLocalizedText("ui-updating", version);
        }
    }
}
