using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class MainForm : Form
    {
        internal Network networkWorker;
        internal Process FFXIVProcess;
        internal OverlayForm overlayForm;
        internal List<TreeNode> nodes;

        public MainForm()
        {
            Settings.Load();

            InitializeComponent();

            Log.Form = this;
            overlayForm = new OverlayForm();
            nodes = new List<TreeNode>();
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
            Data.Initializer();

            overlayForm.Show();
            networkWorker = new Network(this);

            label_AboutTitle.Text = $@"DFA {Global.VERSION}";

            FindFFXIVProcess();

            if (!Settings.ShowOverlay)
            {
                overlayForm.Hide();
                checkBox_Overlay.Checked = false;
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Updater.CheckNewVersion(this);
                    Thread.Sleep(30 * 60 * 1000);
                }
            });

            checkBox_StartupShow.Checked = Settings.StartupShowMainForm;
            checkBox_AutoOverlayHide.Checked = Settings.AutoOverlayHide;
            checkBox_FlashWindow.Checked = Settings.FlashWindow;
            SetCheatRoulleteCheckBox(Settings.CheatRoulette);

            checkBox_Twitter.Checked = Settings.TwitterEnabled;
            textBox_Twitter.Enabled = Settings.TwitterEnabled;
            textBox_Twitter.Text = Settings.TwitterAccount;

            foreach (var zone in Data.Areas)
            {
                if (!zone.Value.isDuty && zone.Value.FATEList.Count > 0)
                    triStateTreeView_FATEs.Nodes.Add(zone.Key.ToString(), zone.Value.Name);
            }

            foreach (var fate in Data.GetFATEs())
            {
                var node = triStateTreeView_FATEs.Nodes[fate.Value.Zone.ToString()].Nodes.Add(fate.Key.ToString(), fate.Value.Name);
                node.Checked = Settings.FATEs.Contains(fate.Key);
                nodes.Add(node);
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(30 * 1000);

                    if ((FFXIVProcess == null) || FFXIVProcess.HasExited)
                    {
                        FFXIVProcess = null;

                        overlayForm.SetStatus(false);
                        this.Invoke(FindFFXIVProcess);
                    }
                    else {
                        // FFXIVProcess is alive

                        if (networkWorker.IsRunning)
                        {
                            networkWorker.UpdateGameConnections(FFXIVProcess);
                        }
                        else
                        {
                            networkWorker.StartCapture(FFXIVProcess);
                        }
                    }
                }
            });

            if (Settings.Updated)
            {
                Settings.Updated = false;
                Settings.Save();
                ShowNotification("버전 {0} 업데이트됨", Global.VERSION);
            }

            Sentry.ReportAsync("App started");
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

        private void button_CopyLog_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox_Log.Text);
            MessageBox.Show("로그가 클립보드에 복사되었습니다.", "DFA 알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel_GitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(string.Format("https://github.com/{0}", Global.GITHUB_REPO));
        }

        private void linkLabel_NewUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(string.Format("https://github.com/{0}/releases/latest", Global.GITHUB_REPO));
        }

        private void button_SelectProcess_Click(object sender, EventArgs e)
        {
            try
            {
                SetFFXIVProcess(Process.GetProcessById(int.Parse(((string)comboBox_Process.SelectedItem).Split(':')[1])));
            }
            catch
            {
                Log.E("파이널판타지14 프로세스 설정에 실패했습니다");
            }
        }

        private void button_ResetProcess_Click(object sender, EventArgs e)
        {
            networkWorker.StopCapture();
            FFXIVProcess = null;
            FindFFXIVProcess();
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

        private void checkBox_Twitter_CheckedChanged(object sender, EventArgs e)
        {
            textBox_Twitter.Enabled = checkBox_Twitter.Checked;
            Settings.TwitterEnabled = checkBox_Twitter.Checked;
            Settings.Save();
        }

        private void checkBox_AutoOverlayHide_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoOverlayHide = checkBox_AutoOverlayHide.Checked;
            Settings.Save();
        }

        private void checkBox_FlashWindow_CheckedChanged(object sender, EventArgs e)
        {
            Settings.FlashWindow = checkBox_FlashWindow.Checked;
            Settings.Save();
        }

        private void checkBox_CheatRoullete_CheckedChanged(object sender, EventArgs e)
        {
            var @checked = checkBox_CheatRoullete.Checked;
            SetCheatRoulleteCheckBox(false);
            if (@checked)
            {
                var respond = MessageBox.Show("악용 방지를 위해 기본적으로 비활성화 되어있는 기능입니다.\n특정 비인기 임무를 고의적으로 입장 거부하는 행위 등은 자제해주세요.\n\n그래도 활성화 하시겠습니까?", "DFA 경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (respond == DialogResult.Yes)
                {
                    MessageBox.Show("활성화되었습니다.\n특정 비인기 임무를 고의적으로 입장 거부하는 행위 등은 자제해주세요.\n\n본 기능은 클라이언트가 재시작 될 때 자동으로 비활성화됩니다.", "DFA 알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetCheatRoulleteCheckBox(true);
                }
            }

            Settings.CheatRoulette = checkBox_CheatRoullete.Checked;
            Settings.Save();
        }

        private void textBox_Twitter_TextChanged(object sender, EventArgs e)
        {
            Settings.TwitterAccount = textBox_Twitter.Text;
            Settings.Save();
        }

        private void toolStripMenuItem_LogCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox_Log.Text);
            MessageBox.Show("로그가 클립보드에 복사되었습니다.", "DFA 알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem_LogClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("로그를 비우시겠습니까?", "DFA 알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                richTextBox_Log.Text = "";
            }
        }

        private void toolStripMenuItem_SelectAll_Click(object sender, EventArgs e)
        {
            foreach (var node in nodes)
            {
                node.Checked = true;
                Settings.FATEs.Add(ushort.Parse(node.Name));
            }

            Settings.Save();
        }

        private void toolStripMenuItem_UnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var node in nodes)
            {
                node.Checked = false;
            }

            Settings.FATEs.Clear();
            Settings.Save();
        }

        private void toolStripMenuItem_SelectApply_Click(object sender, EventArgs e)
        {
            foreach (var node in nodes)
            {
                if (node.Checked)
                {
                    Settings.FATEs.Add(ushort.Parse(node.Name));
                }
                else
                {
                    Settings.FATEs.Remove(ushort.Parse(node.Name));
                }
            }

            Settings.Save();
            MessageBox.Show("돌발 알림 설정이 적용되었습니다.", "DFA 알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetCheatRoulleteCheckBox(bool @checked)
        {
            checkBox_CheatRoullete.CheckedChanged -= checkBox_CheatRoullete_CheckedChanged;
            checkBox_CheatRoullete.Checked = @checked;
            checkBox_CheatRoullete.CheckedChanged += checkBox_CheatRoullete_CheckedChanged;
        }

        private void FindFFXIVProcess()
        {
            comboBox_Process.Items.Clear();
            Log.I("파이널판타지14 프로세스를 찾는 중...");

            var processes = new List<Process>();
            processes.AddRange(Process.GetProcessesByName("ffxiv"));
            processes.AddRange(Process.GetProcessesByName("ffxiv_dx11"));

            if (processes.Count == 0)
            {
                Log.E("파이널판타지14 프로세스를 찾을 수 없습니다");
                button_SelectProcess.Enabled = false;
                comboBox_Process.Enabled = false;
            }
            else if (processes.Count >= 2)
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
                SetFFXIVProcess(processes[0]);
            }
        }

        private void SetFFXIVProcess(Process process)
        {
            FFXIVProcess = process;

            var name = string.Format("{0}:{1}", FFXIVProcess.ProcessName, FFXIVProcess.Id);
            Log.S("파이널판타지14 프로세스가 선택되었습니다: {0}", name);

            comboBox_Process.Enabled = false;
            button_SelectProcess.Enabled = false;

            comboBox_Process.Items.Clear();
            comboBox_Process.Items.Add(name);
            comboBox_Process.SelectedIndex = 0;

            networkWorker.StartCapture(FFXIVProcess);
        }

        internal void ShowNotification(string format, params object[] args)
        {
            this.Invoke(() =>
            {
                notifyIcon.ShowBalloonTip(10 * 1000, "임무/돌발 찾기 도우미", string.Format(format, args), ToolTipIcon.Info);
            });
        }
    }
}
