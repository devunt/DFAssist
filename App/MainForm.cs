using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace App
{
    public partial class MainForm : Form
    {
        Communicator communicator;
        internal OverlayForm overlayForm;

        public MainForm()
        {
            Settings.Load();

            InitializeComponent();

            Log.Form = this;
            overlayForm = new OverlayForm();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!Settings.StartupShowMainForm)
            {
                Hide();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label_AboutTitle.Text = string.Format("DFH {0}", Global.VERSION);
            communicator = new Communicator(this);

            Log.I("파이널판타지14 프로세스를 찾는 중...");
            Process[] processes = Process.GetProcessesByName("ffxiv_dx11");
            if (processes.Length == 0)
            {
                Log.E("파이널판타지14 프로세스를 찾을 수 없습니다");
                Log.E("실행중인 파이널판타지14가 DX11 버전이 맞는지 확인해주세요");
                button_SelectProcess.Enabled = false;
                comboBox_Process.Enabled = false;
            }
            else if (processes.Length >= 2)
            {
                Log.E("파이널판타지14가 2개 이상 실행중입니다");
                button_SelectProcess.Enabled = true;
                comboBox_Process.Enabled = true;

                foreach (var process in processes)
                {
                    comboBox_Process.Items.Add(string.Format("{0}:{1}", process.ProcessName, process.Id));
                }
                comboBox_Process.SelectedIndex = 0;
            }
            else {
                Injector.TargetProcess = processes[0];
                Log.S("파이널판타지14 프로세스를 찾았습니다: {0}:{1}", Injector.TargetProcess.ProcessName, Injector.TargetProcess.Id);

                comboBox_Process.Enabled = false;
                button_SelectProcess.Enabled = false;

                comboBox_Process.Items.Add(string.Format("{0}:{1}", Injector.TargetProcess.ProcessName, Injector.TargetProcess.Id));
                comboBox_Process.SelectedIndex = 0;

                Injector.Inject();
            }

            if (Settings.ShowOverlay)
            {
                checkBox_Overlay.Checked = true;
                overlayForm.Show();
            }

            if (Settings.StartupCheckUpdate) {
                checkBox_StartupUpdate.Checked = true;
                if (Updater.CheckNewVersion())
                {
                    linkLabel_NewUpdate.Visible = true;
                    Show();
                }
            }

            checkBox_StartupShow.Checked = Settings.StartupShowMainForm;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void toolStripMenuItem_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void richTextBox_Log_TextChanged(object sender, EventArgs e)
        {
            richTextBox_Log.SelectionStart = richTextBox_Log.Text.Length;
            richTextBox_Log.SelectionLength = 0;
            richTextBox_Log.ScrollToCaret();
        }

        private void linkLabel_GitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(string.Format("https://github.com/{0}", Global.GITHUB_REPO));
        }

        private void linkLabel_NewUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(string.Format("https://github.com/{0}/releases", Global.GITHUB_REPO));
        }

        private void button_SelectProcess_Click(object sender, EventArgs e)
        {
            Injector.TargetProcess = Process.GetProcessById(int.Parse(((string)comboBox_Process.SelectedItem).Split(':')[1]));
            Log.S("파이널판타지14 프로세스가 선택되었습니다: {0}:{1}", Injector.TargetProcess.ProcessName, Injector.TargetProcess.Id);

            comboBox_Process.Enabled = false;
            button_SelectProcess.Enabled = false;

            comboBox_Process.Items.Clear();
            comboBox_Process.Items.Add(string.Format("{0}:{1}", Injector.TargetProcess.ProcessName, Injector.TargetProcess.Id));
            comboBox_Process.SelectedIndex = 0;

            Injector.Inject();
        }

        private void button_Eject_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("[디버깅용 기능]\n\n사용시 치명적인 오류가 발생할 수 있습니다.\n계속 진행하시겠습니까?", "경고",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Injector.Eject();
            }
        }

        private void checkBox_Overlay_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowOverlay = checkBox_Overlay.Checked;
            Settings.Save();

            if (Settings.ShowOverlay)
            {
                overlayForm.Show();
            }
            else
            {
                overlayForm.Hide();
            }
        }

        private void button_ResetOverlayPosition_Click(object sender, EventArgs e)
        {
            overlayForm.ResetFormLocation();
        }

        private void checkBox_StartupShow_CheckedChanged(object sender, EventArgs e)
        {
            Settings.StartupShowMainForm = checkBox_StartupShow.Checked;
            Settings.Save();
        }

        private void checkBox_StartupUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Settings.StartupCheckUpdate = checkBox_StartupUpdate.Checked;
            Settings.Save();
        }
    }
}
