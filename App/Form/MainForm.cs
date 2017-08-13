using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

            radioButton_Langko.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButton_Langen.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
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

            if (Settings.lang == 0)
                radioButton_Langko.Checked = true;
            else if (Settings.lang == 1)
                radioButton_Langen.Checked = true;
            checkBox_StartupShow.Checked = Settings.StartupShowMainForm;
            checkBox_AutoOverlayHide.Checked = Settings.AutoOverlayHide;
            checkBox_FlashWindow.Checked = Settings.FlashWindow;
            SetCheatRoulleteCheckBox(Settings.CheatRoulette);

            checkBox_Twitter.Checked = Settings.TwitterEnabled;
            textBox_Twitter.Enabled = Settings.TwitterEnabled;
            textBox_Twitter.Text = Settings.TwitterAccount;

            foreach (var area in Data.Areas)
            {
                triStateTreeView_FATEs.Nodes.Add(area.Key.ToString(), area.Value.Name);

                foreach (var fate in area.Value.FATEs)
                {
                    var node = triStateTreeView_FATEs.Nodes[area.Key.ToString()].Nodes.Add(fate.Key.ToString(), fate.Value.Name);
                    node.Checked = Settings.FATEs.Contains(fate.Key);
                    nodes.Add(node);
                }
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(30 * 1000);

                    if (FFXIVProcess == null || FFXIVProcess.HasExited)
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
            if (@checked && Settings.lang == 0)
            {
                var respond = MessageBox.Show("악용 방지를 위해 기본적으로 비활성화 되어있는 기능입니다.\n특정 비인기 임무를 고의적으로 입장 거부하는 행위 등은 자제해주세요.\n\n그래도 활성화 하시겠습니까?", "DFA 경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (respond == DialogResult.Yes)
                {
                    MessageBox.Show("활성화되었습니다.\n특정 비인기 임무를 고의적으로 입장 거부하는 행위 등은 자제해주세요.\n\n본 기능은 클라이언트가 재시작 될 때 자동으로 비활성화됩니다.", "DFA 알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetCheatRoulleteCheckBox(true);
                }
            }
            else if (@checked && Settings.lang == 1)
            {
                var respond = MessageBox.Show("This function is disabled by default to prevent abuse.\nPlease refrain from deliberately rejecting a specific dislike duty.\n\nDo you still want to enable it?", "DFA Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (respond == DialogResult.Yes)
                {
                    MessageBox.Show("Enabled.\nPlease DON'T deliberately reject a specific dislike duty\n\nThis feature is automatically disabled when the program restarts.", "DFA Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (Settings.lang == 0)
                MessageBox.Show("로그가 클립보드에 복사되었습니다.", "DFA 알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (Settings.lang == 1)
                MessageBox.Show("Logs have copied to Clipboard.", "DFA Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem_LogClear_Click(object sender, EventArgs e)
        {
            if (Settings.lang == 0 && MessageBox.Show("로그를 비우시겠습니까?", "DFA 알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
             {
                richTextBox_Log.Text = " ";
             }
            else if (Settings.lang == 1 && MessageBox.Show("Clear Logs?", "DFA Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
             {
                richTextBox_Log.Text = " ";
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
            if (Settings.lang == 0)
                MessageBox.Show("돌발 알림 설정이 적용되었습니다.", "DFA 알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (Settings.lang == 1)
                MessageBox.Show("FATE Notification Set.", "DFA Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void radioButtons_CheckedChanged (object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton_Langko.Checked) // 한국어
            {
                Settings.lang = 0;
                Settings.Save();
                this.Text = "임무/돌발 찾기 도우미";
                notifyIcon.Text = "임무/돌발 찾기 도우미";
                toolStripMenuItem_Open.Text = "열기";
                toolStripMenuItem_Close.Text = "종료";
                label_Process.Text = "FFXIV 프로세스";
                button_SelectProcess.Text = "수동설정";
                button_ResetProcess.Text = "재설정";
                checkBox_Overlay.Text = "오버레이 사용";
                button_ResetOverlayPosition.Text = "위치 초기화";
                button_ResetOverlayPosition.Font = new Font("맑은 고딕", 8);
                groupBox_DefaultSet.Text = "기본 설정";
                checkBox_StartupShow.Text = "프로그램 시작시 이 창 보이기";
                checkBox_AutoOverlayHide.Text = "임무 입장시 자동으로 오버레이 숨김";
                checkBox_FlashWindow.Text = "매칭/돌발 발생시 파이널판타지14 작업 표시줄 아이콘 깜빡이기";
                checkBox_CheatRoullete.Text = "무작위 임무일 경우에도 실제 매칭된 임무 보여주기";
                groupBox_TwitterSet.Text = "트위터 알림";
                checkBox_Twitter.Text = "활성화";
                label_TwitterAbout.Text = "매칭이 됐을 시 입력된 트위터 계정으로 멘션을 보내 해당 사실을 알립니다.\n원하는 돌발이 발생했을 시에도 멘션을 보내 해당 사실을 알립니다.\n계정명 입력시 앞의 @ 표시는 제외하고 순수 계정명만 입력해주세요.";
                tabControl.TabPages[0].Text = "설정";
                tabControl.TabPages[1].Text = "돌발";
                tabControl.TabPages[2].Text = "로그";
                tabControl.TabPages[3].Text = "정보";
                toolStripMenuItem_SelectAll.Text = "모두 선택";
                toolStripMenuItem_UnSelectAll.Text = "모두 해제";
                toolStripMenuItem_SelectApply.Text = "적용하기";
                toolStripMenuItem_LogCopy.Text = "로그 복사";
                toolStripMenuItem_LogClear.Text = "로그 삭제";
                label_About.Text = "[제작 및 문의]\n유채색\n라그린네\n히비야\n윈도ce [인벤]\n\n[저작권]\n기재되어있는 회사명 · 제품명 · 시스템 이름은\n해당 소유자의 상표 또는 등록 상표입니다.\n(C)2010 - 2017 SQUARE ENIX CO., LTD All Rights Reserved.\nKorea Published by EYEDENTITY MOBILE.";

            }
            else if (radioButton_Langen.Checked) // English
            {
                Settings.lang = 1;
                Settings.Save();
                this.Text = "Duty/FATE Notificator";
                notifyIcon.Text = "DFAssist";
                toolStripMenuItem_Open.Text = "Open";
                toolStripMenuItem_Close.Text = "Exit";
                label_Process.Text = "FFXIV Process";
                button_SelectProcess.Text = "Manual";
                button_ResetProcess.Text = "Reset";
                checkBox_Overlay.Text = "Use Overlay";
                button_ResetOverlayPosition.Font = new Font("맑은 고딕", 7);
                button_ResetOverlayPosition.Text = "Overlay Reset";
                groupBox_DefaultSet.Text = "Basic Setting";
                checkBox_StartupShow.Text = "Show MainForm when Program Starts";
                checkBox_AutoOverlayHide.Text = "Auto Hide Overlay while in Duty";
                checkBox_FlashWindow.Text = "FFXIV Icon Blinks when Duty Matched/FATE occur";
                checkBox_CheatRoullete.Text = "Show Actual Matched Duty when using Duty Roulette";
                groupBox_TwitterSet.Text = "Twitter Alarm";
                checkBox_Twitter.Text = "Activate";
                label_TwitterAbout.Text = "When matched or FATE occurs, \nsend a Tweet with mention to the entered Twitter account.\nEnter the Twitter Account except the preceding symbol @.";
                tabControl.TabPages[0].Text = "Setting";
                tabControl.TabPages[1].Text = "FATE";
                tabControl.TabPages[2].Text = "Logs";
                tabControl.TabPages[3].Text = "Info";
                toolStripMenuItem_SelectAll.Text = "Select All";
                toolStripMenuItem_UnSelectAll.Text = "Unselect All";
                toolStripMenuItem_SelectApply.Text = "Apply";
                toolStripMenuItem_LogCopy.Text = "Copy Logs";
                toolStripMenuItem_LogClear.Text = "Clear";
                label_About.Text = "[Contributor]\n유채색\n라그린네\n히비야\nAlex00728 [Reddit]\n\n[Copyright]\nAll company, product, system names are\n registered or unregistered trademarks of their respective owners.\n(C)2010 - 2017 SQUARE ENIX CO., LTD All Rights Reserved.\nKorea Published by EYEDENTITY MOBILE.";
            }
            
        }

        private void radioButton_Langen_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You should Restart Program \nTo apply language setting perfectly.", "DFA Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void radioButton_Langko_Click(object sender, EventArgs e)
        {
            MessageBox.Show("변경된 언어를 완벽하게 적용하기 위해서는 \n프로그램을 재시작해야 합니다.", "DFA 알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
