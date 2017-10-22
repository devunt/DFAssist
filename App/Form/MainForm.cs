using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            Localization.Initialize(Settings.Language);
            Data.Initialize(Settings.Language);

            ApplyLanguage();

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

            comboBox_Language.DataSource = new[]
            {
                new Language { Name = "한국어", Code = "ko-kr" },
                new Language { Name = "English", Code = "en-us" },
                new Language { Name = "Français", Code = "fr-fr" },
                new Language { Name = "日本語", Code = "ja-jp" },
            };

            comboBox_Language.DisplayMember = "Name";
            comboBox_Language.ValueMember = "Code";

            comboBox_Language.SelectedValue = Settings.Language;

            comboBox_Language.SelectedValueChanged += comboBox_Language_SelectedValueChanged;

            checkBox_StartupShow.Checked = Settings.StartupShowMainForm;
            checkBox_AutoOverlayHide.Checked = Settings.AutoOverlayHide;
            checkBox_FlashWindow.Checked = Settings.FlashWindow;
            checkBox_PlaySound.Checked = Settings.PlaySound;
            if (System.IO.File.Exists(Settings.SoundLocation) == false)
            {
                checkBox_PlaySound.Checked = false;
                label_SoundLocation.Text = "";
            }
            else
            {
                label_SoundLocation.Text = System.IO.Path.GetFileName(Settings.SoundLocation);
            }
            if (checkBox_PlaySound.Checked == false) { button_SoundLocation.Enabled = false; }
            checkBox_ShowAnnouncement.Checked = Settings.ShowAnnouncement;

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
                ShowNotification("notification-app-updated", Global.VERSION);
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

        private void linkLabel_GitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start($"https://github.com/{Global.GITHUB_REPO}");
        }

        private void linkLabel_NewUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start($"https://github.com/{Global.GITHUB_REPO}/releases/latest");
        }

        private void button_SelectProcess_Click(object sender, EventArgs e)
        {
            try
            {
                SetFFXIVProcess(Process.GetProcessById(int.Parse(((string)comboBox_Process.SelectedItem).Split(':')[1])));
            }
            catch
            {
                Log.E("l-process-set-failed");
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

        private void checkBox_ShowAnnouncement_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowAnnouncement = checkBox_ShowAnnouncement.Checked;
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
            LMessageBox.I("ui-clipboard-copied");
        }

        private void toolStripMenuItem_LogClear_Click(object sender, EventArgs e)
        {
            if (LMessageBox.I("ui-clear-log-confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
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
            FateAllUnset(true);
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
            LMessageBox.I("ui-fate-selection-saved");
        }

        private void FateAllUnset(bool save = false)
        {
            foreach (var node in nodes)
            {
                node.Checked = false;
            }

            Settings.FATEs.Clear();

            if (save)
                Settings.Save();
        }

        private void PresetAccept(int[] arr)
        {
            FateAllUnset();

            foreach (var node in nodes)
            {
                var c = ushort.Parse(node.Name);
                if (arr.Any(code => code == c))
                {
                    node.Checked = true;
                    Settings.FATEs.Add(c);
                }
            }

            Settings.Save();
            LMessageBox.I("ui-fate-preset-applied");
        }

        private void bookOfSkyfireIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 611, 480, 589 };
            PresetAccept(arr);
        }

        private void bookOfSkyfireIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 424, 633, 571 };
            PresetAccept(arr);
        }

        private void bookOfNetherfireIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 521, 620, 430 };
            PresetAccept(arr);
        }

        private void bookOfSkyfallIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 540, 577, 475 };
            PresetAccept(arr);
        }

        private void bookOfSkyfallIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 569, 616, 516 };
            PresetAccept(arr);
        }
        
        private void bookOfNetherfireIToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int[] arr = { 632, 642, 499 };
            PresetAccept(arr);
        }

        private void bookOfSkywindIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 604, 317, 517 };
            PresetAccept(arr);
        }

        private void bookOfSkywindIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 552, 628, 486 };
            PresetAccept(arr);
        }

        private void bookOfSkyearthIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 543, 493, 587 };
            PresetAccept(arr);
        }

        private void FindFFXIVProcess()
        {
            comboBox_Process.Items.Clear();
            Log.I("l-process-finding");

            var processes = new List<Process>();
            processes.AddRange(Process.GetProcessesByName("ffxiv"));
            processes.AddRange(Process.GetProcessesByName("ffxiv_dx11"));

            if (processes.Count == 0)
            {
                Log.E("l-process-found-nothing");
                button_SelectProcess.Enabled = false;
                comboBox_Process.Enabled = false;
            }
            else if (processes.Count >= 2)
            {
                Log.E("l-process-found-multiple");
                button_SelectProcess.Enabled = true;
                comboBox_Process.Enabled = true;

                foreach (var process in processes)
                {
                    comboBox_Process.Items.Add($"{process.ProcessName}:{process.Id}");
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

            var name = $"{FFXIVProcess.ProcessName}:{FFXIVProcess.Id}";
            Log.S("l-process-set-success", name);

            comboBox_Process.Enabled = false;
            button_SelectProcess.Enabled = false;

            comboBox_Process.Items.Clear();
            comboBox_Process.Items.Add(name);
            comboBox_Process.SelectedIndex = 0;

            networkWorker.StartCapture(FFXIVProcess);
        }

        internal void ShowNotification(string key, params object[] args)
        {
            this.Invoke(() =>
            {
                notifyIcon.ShowBalloonTip(10 * 1000, Localization.GetText("app-name"), Localization.GetText(key, args), ToolTipIcon.Info);
            });
        }

        private void comboBox_Language_SelectedValueChanged(object sender, EventArgs e)
        {
            var language = comboBox_Language.SelectedValue.ToString();
            if (Settings.Language == language)
            {
                return;
            }

            Settings.Language = language;
            Settings.Save();

            Localization.Initialize(Settings.Language);
            Data.Initialize(Settings.Language);

            ApplyLanguage();

            LMessageBox.I("ui-language-changed");
        }

        private void ApplyLanguage()
        {
            this.Text = Localization.GetText("app-name");
            notifyIcon.Text = Localization.GetText("app-name");
            toolStripMenuItem_Open.Text = Localization.GetText("ui-notifymenustrip-open");
            toolStripMenuItem_Close.Text = Localization.GetText("ui-notifymenustrip-close");
            label_Process.Text = Localization.GetText("ui-topsetting-process");
            button_SelectProcess.Text = Localization.GetText("ui-topsetting-select");
            button_ResetProcess.Text = Localization.GetText("ui-topsetting-reset");
            tabControl.TabPages[0].Text = Localization.GetText("ui-tabcontrol-settings");
            tabControl.TabPages[1].Text = Localization.GetText("ui-tabcontrol-fate");
            tabControl.TabPages[2].Text = Localization.GetText("ui-tabcontrol-logs");
            tabControl.TabPages[3].Text = Localization.GetText("ui-tabcontrol-info");
            groupBox_DefaultSet.Text = Localization.GetText("ui-settings-title");
            checkBox_Overlay.Text = Localization.GetText("ui-settings-overlay-use");
            toolTip.SetToolTip(checkBox_Overlay, Localization.GetText("ui-settings-overlay-tooltip"));
            button_ResetOverlayPosition.Text = Localization.GetText("ui-settings-overlay-reset");
            checkBox_StartupShow.Text = Localization.GetText("ui-settings-startupshow");
            checkBox_AutoOverlayHide.Text = Localization.GetText("ui-settings-autohide");
            checkBox_FlashWindow.Text = Localization.GetText("ui-settings-iconflash");
            checkBox_PlaySound.Text = Localization.GetText("ui-settings-playsound");
            button_SoundLocation.Text = Localization.GetText("ui-settings-soundlocation");
            checkBox_ShowAnnouncement.Text = Localization.GetText("ui-settings-overlay-announcement");
            groupBox_TwitterSet.Text = Localization.GetText("ui-settings-tweet-title");
            checkBox_Twitter.Text = Localization.GetText("ui-settings-tweet-activate");
            label_TwitterAbout.Text = Localization.GetText("ui-settings-tweet-about");
            toolStripMenuItem_SelectAll.Text = Localization.GetText("ui-fate-selectall");
            toolStripMenuItem_UnSelectAll.Text = Localization.GetText("ui-fate-unselectall");
            presetToolStripMenuItem.Text = Localization.GetText("ui-fate-preset");
            bookOfSkyfireIToolStripMenuItem.Text = Localization.GetText("fate-preset-animus-SkyfireI");
            bookOfSkyfireIIToolStripMenuItem.Text = Localization.GetText("fate-preset-animus-SkyfireII");
            bookOfNetherfireIToolStripMenuItem.Text = Localization.GetText("fate-preset-animus-NetherfireI");
            bookOfSkyfallIToolStripMenuItem.Text = Localization.GetText("fate-preset-animus-SkyfallI");
            bookOfSkyfallIIToolStripMenuItem.Text = Localization.GetText("fate-preset-animus-SkyfallII");
            bookOfNetherfireIToolStripMenuItem1.Text = Localization.GetText("fate-preset-animus-NetherfallI");
            bookOfSkywindIToolStripMenuItem.Text = Localization.GetText("fate-preset-animus-SkywindI");
            bookOfSkywindIIToolStripMenuItem.Text = Localization.GetText("fate-preset-animus-SkywindII");
            bookOfSkyearthIToolStripMenuItem.Text = Localization.GetText("fate-preset-animus-SkyearthI");
            toolStripMenuItem_SelectApply.Text = Localization.GetText("ui-fate-apply");
            label_FATEAbout.Text = Localization.GetText("ui-fate-about");
            toolStripMenuItem_LogCopy.Text = Localization.GetText("ui-logs-copy");
            toolStripMenuItem_LogClear.Text = Localization.GetText("ui-logs-clear");
            label_About.Text = Localization.GetText("ui-info-about");

        }

        private void checkBox_PlaySound_CheckedChanged(object sender, EventArgs e)
        {
            button_SoundLocation.Enabled = checkBox_PlaySound.Checked;
            if (button_SoundLocation.Enabled == false)
            {
                label_SoundLocation.Text = "";
                Settings.SoundLocation = "";
            }
            Settings.PlaySound = checkBox_PlaySound.Checked;
            Settings.Save();
        }

        private void button_SoundLocation_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "WAVE Files|*.wav";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                label_SoundLocation.Text = System.IO.Path.GetFileName(openFileDialog1.FileName);
                Settings.SoundLocation = openFileDialog1.FileName;
                Settings.Save();
            }
        }

        private void groupBox_DefaultSet_Enter(object sender, EventArgs e)
        {

        }
    }
}
