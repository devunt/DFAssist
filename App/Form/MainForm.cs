using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

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
            Data.Initialize(Settings.Language, this);

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
            networkWorker.notificationPlayer = new System.Media.SoundPlayer();

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
                new Language { Name = "Deutsch", Code = "de-de" },
                new Language { Name = "日本語", Code = "ja-jp" },
            };

            comboBox_Language.DisplayMember = "Name";
            comboBox_Language.ValueMember = "Code";

            comboBox_Language.SelectedValue = Settings.Language;

            comboBox_Language.SelectedValueChanged += comboBox_Language_SelectedValueChanged;

            checkBox_StartupShow.Checked = Settings.StartupShowMainForm;
            checkBox_autoHideOverlay.Checked = Settings.autoHideOverlay;
            checkBox_FlashWindow.Checked = Settings.FlashWindow;
            checkBox_FateSound.Checked = Settings.FateSound;
            checkBox_useVPN.Checked = Settings.useVPN;
            // Check if Custom Sound File Exists
            if (File.Exists(Settings.CustomSoundPath))
            {
                label_CustomSoundFileName.Text = Path.GetFileName(Settings.CustomSoundPath);
                checkBox_CustomSound.Checked = Settings.CustomSound;
                networkWorker.notificationPlayer.SoundLocation = Settings.CustomSoundPath;
            }
            else
            {
                label_CustomSoundFileName.Text = "";
                checkBox_CustomSound.Checked = false;
                Settings.CustomSound = false;
                Settings.Save();
            }
            SetCheatRoulleteCheckBox(Settings.CheatRoulette);
            // checkBox_Twitter.Checked = Settings.TwitterEnabled;
            // textBox_Telegram.Enabled = Settings.TwitterEnabled;
            // textBox_Telegram.Text = Settings.TwitterAccount;
            checkBox_Telegram.Checked = Settings.TelegramEnabled;
            textBox_Telegram.Enabled = Settings.TelegramEnabled;
            textBox_Telegram.Text = Settings.TelegramChatId;
            checkBox_Discord.Checked = Settings.DiscordEnabled;
            textBox_Discord.Enabled = Settings.DiscordEnabled;
            textBox_Discord.Text = Settings.DiscordAccount;

            checkBox_EnableHttpRequest.Checked = Settings.customHttpRequest;
            textBox_CustomHttpUrl.Text = Settings.customHttpUrl;
            checkBox_RequestOnDutyMatched.Checked = Settings.requestOnDutyMatched;
            checkBox_RequestOnFateOccur.Checked = Settings.requestOnFateOccured;

            refresh_Fates();

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
        }

        internal void refresh_Fates()
        {
            triStateTreeView_FATEs.Nodes.Clear();
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

        private void linkLabel_Telegram_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Global.TELEGRAM_BOT);
        }

        private void linkLabel_DiscordServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Global.DISCORD_INVITE);
        }

        private void linkLabel_GitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start($"https://github.com/{Global.GITHUB_REPO}");
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

            // Disable autohide overlay function when ShowOverlay is disabled
            if (checkBox_Overlay.Checked)
            {
                checkBox_autoHideOverlay.Enabled = true;
            }
            else
            {
                checkBox_autoHideOverlay.Enabled = false;
                checkBox_autoHideOverlay.Checked = false;
                Settings.autoHideOverlay = false;
            }
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

        private void checkBox_autoHideOverlay_CheckedChanged(object sender, EventArgs e)
        {
            Settings.autoHideOverlay = checkBox_autoHideOverlay.Checked;
            Settings.Save();
        }

        private void checkBox_autoHideOverlay_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkBox_autoHideOverlay.Checked)
            {
                LMessageBox.I("ui-notification-overlay-autohide");
            }
        }

        private void checkBox_StartupShow_CheckedChanged(object sender, EventArgs e)
        {
            Settings.StartupShowMainForm = checkBox_StartupShow.Checked;
            Settings.Save();
        }

        private void checkBox_FlashWindow_CheckedChanged(object sender, EventArgs e)
        {
            Settings.FlashWindow = checkBox_FlashWindow.Checked;
            Settings.Save();
        }

        private void checkBox_FateSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.FateSound = checkBox_FateSound.Checked;
            Settings.Save();
        }

        private void checkBox_CustomSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.CustomSound = checkBox_CustomSound.Checked;
            Settings.Save();
        }

        private void checkBox_CustomSound_MouseDown(object sender, MouseEventArgs e)
        {
            if (label_CustomSoundFileName.Text == "")
            {
                LMessageBox.I("ui-notification-customsound-selectfile");
                checkBox_CustomSound.Checked = false;
            }
        }

        private void button_getSoundFile_Click(object sender, EventArgs e)
        {
            // Create a new OpenFileDialog.
            OpenFileDialog dlg = new OpenFileDialog();

            // Make sure the dialog checks for existence of the 
            // selected file.
            dlg.CheckFileExists = true;
            
            dlg.Filter = Localization.GetText("ui-settings-getfilewav");
            dlg.DefaultExt = ".wav";

            // Activate the file selection dialog.
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file's path from the dialog.
                this.label_CustomSoundFileName.Text = Path.GetFileName(dlg.FileName);

                Settings.CustomSoundPath = dlg.FileName;
                Settings.Save();
                
                networkWorker.notificationPlayer.SoundLocation = dlg.FileName;
            }
        }

        private void checkBox_CheatRoullete_CheckedChanged(object sender, EventArgs e)
        {
            var @checked = checkBox_CheatRoullete.Checked;
            SetCheatRoulleteCheckBox(false);
            if (@checked)
            {
                var respond = LMessageBox.W("ui-cheat-roulette-confirm", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                if (respond == DialogResult.Yes)
                {
                    LMessageBox.I("ui-cheat-roulette-enabled");
                    SetCheatRoulleteCheckBox(true);
                }
            }

            Settings.CheatRoulette = checkBox_CheatRoullete.Checked;
            Settings.Save();
        }

        private void checkBox_useVPN_CheckedChanged(object sender, EventArgs e)
        {
            Settings.useVPN = checkBox_useVPN.Checked;
            Settings.Save();
            networkWorker.StopCapture();
            FFXIVProcess = null;
            FindFFXIVProcess();
        }

        private void checkBox_useVPN_MouseUp(object sender, MouseEventArgs e)
        {
            if (checkBox_useVPN.Checked)
            {
                LMessageBox.I("ui-settings-usevpn-alert");
            }
        }

        private void textBox_Telegram_TextChanged(object sender, EventArgs e)
        {
            Settings.TelegramChatId = textBox_Telegram.Text;
            Settings.Save();
        }

        private void checkBox_Telegram_CheckedChanged(object sender, EventArgs e)
        {
            textBox_Telegram.Enabled = checkBox_Telegram.Checked;
            Settings.TelegramEnabled = checkBox_Telegram.Checked;
            Settings.Save();
        }

        /*private void textBox_Twitter_TextChanged(object sender, EventArgs e)
        {
            Settings.TwitterAccount = textBox_Twitter.Text;
            Settings.Save();
        }

        private void checkBox_Twitter_CheckedChanged(object sender, EventArgs e)
        {
            textBox_Twitter.Enabled = checkBox_Twitter.Checked;
            Settings.TwitterEnabled = checkBox_Twitter.Checked;
            Settings.Save();
        }
        */
        private void textBox_Discord_TextChanged(object sender, EventArgs e)
        {
            string strRegex = "^[0-9]{15}";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(textBox_Discord.Text))
            {
                textBox_Discord.ForeColor = Color.Blue;
            }
            else
            {
                textBox_Discord.ForeColor = Color.Red;
            }
            Settings.DiscordAccount = textBox_Discord.Text;
            Settings.Save();
        }

        private void textBox_Discord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                LMessageBox.I("ui-3rdparty-discord-numberonly-alert");
                e.Handled = true;
            }
        }

        private void checkBox_Discord_CheckedChanged(object sender, EventArgs e)
        {
            textBox_Discord.Enabled = checkBox_Discord.Checked;
            Settings.DiscordEnabled = checkBox_Discord.Checked;
            Settings.Save();
        }

        private void checkBox_EnableHttpRequest_CheckedChanged(object sender, EventArgs e)
        {
            var @checked = checkBox_EnableHttpRequest.Checked;
            Settings.customHttpRequest = @checked;
            textBox_CustomHttpUrl.Enabled = @checked;
            checkBox_RequestOnDutyMatched.Enabled = @checked;
            checkBox_RequestOnFateOccur.Enabled = @checked;
            Settings.Save();
        }

        private void textBox_CustomHttpUrl_TextChanged(object sender, EventArgs e)
        {
            Settings.customHttpUrl = textBox_CustomHttpUrl.Text;
            Settings.Save();
        }

        private void checkBox_RequestOnDutyMatched_CheckedChanged(object sender, EventArgs e)
        {
            Settings.requestOnDutyMatched = checkBox_RequestOnDutyMatched.Checked;
            Settings.Save();
        }

        private void checkBox_RequestOnFateOccur_CheckedChanged(object sender, EventArgs e)
        {
            Settings.requestOnFateOccured = checkBox_RequestOnFateOccur.Checked;
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

        private void IxionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 1103, 1104, 1105 };
            PresetAccept(arr);
        }

        private void TamamoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 1106, 1107, 1108, 1109, 1110, 1111 };
            PresetAccept(arr);
        }

        private void frogSuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 1426, 1427, 1428, 1429, 1430, 1431, 1432 };
            PresetAccept(arr);
        }

        private void frogMountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arr = { 1462, 1463, 1464, 1477, 1478, 1479 };
            PresetAccept(arr);
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
            Data.Initialize(Settings.Language, this);

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
            tabControl.TabPages[1].Text = Localization.GetText("ui-tabcontrol-3rdparty");
            tabControl.TabPages[2].Text = Localization.GetText("ui-tabcontrol-advanced");
            tabControl.TabPages[3].Text = Localization.GetText("ui-tabcontrol-fate");
            tabControl.TabPages[4].Text = Localization.GetText("ui-tabcontrol-logs");
            tabControl.TabPages[5].Text = Localization.GetText("ui-tabcontrol-info");
            groupBox_DefaultSet.Text = Localization.GetText("ui-settings-title");
            checkBox_Overlay.Text = Localization.GetText("ui-settings-overlay-use");
            toolTip.SetToolTip(checkBox_Overlay, Localization.GetText("ui-settings-overlay-tooltip"));
            button_ResetOverlayPosition.Text = Localization.GetText("ui-settings-overlay-reset");
            checkBox_autoHideOverlay.Text = Localization.GetText("ui-settings-overlay-autohide");
            checkBox_StartupShow.Text = Localization.GetText("ui-settings-startupshow");
            checkBox_FlashWindow.Text = Localization.GetText("ui-settings-iconflash");
            checkBox_FateSound.Text = Localization.GetText("ui-settings-fatesound");
            checkBox_CustomSound.Text = Localization.GetText("ui-settings-customsound");
            button_getSoundFile.Text = Localization.GetText("ui-settings-getsoundfile");
            checkBox_CheatRoullete.Text = Localization.GetText("ui-settings-cheatroulette");
            checkBox_useVPN.Text = Localization.GetText("ui-settings-usevpn");
            // groupBox_TwitterSet.Text = Localization.GetText("ui-3rdparty-twitter-title");
            // checkBox_Twitter.Text = Localization.GetText("ui-3rdparty-twitter-activate");
            // label_TwitterAbout.Text = Localization.GetText("ui-3rdparty-twitter-about");
             groupBox_TelegramSet.Text = Localization.GetText("ui-3rdparty-telegram-title");
             checkBox_Telegram.Text = Localization.GetText("ui-3rdparty-telegram-activate");
             label_TelegramAbout.Text = Localization.GetText("ui-3rdparty-telegram-about");
            groupBox_DiscordSet.Text = Localization.GetText("ui-3rdparty-discord-title");
            checkBox_Discord.Text = Localization.GetText("ui-3rdparty-discord-activate");
            label_DiscordAbout.Text = Localization.GetText("ui-3rdparty-discord-about");
            groupBox_CustomHttpRequest.Text = Localization.GetText("ui-advanced-http");
            checkBox_EnableHttpRequest.Text = Localization.GetText("ui-advanced-http-enable");
            checkBox_RequestOnDutyMatched.Text = Localization.GetText("ui-advanced-http-duty-matched");
            checkBox_RequestOnFateOccur.Text = Localization.GetText("ui-advanced-http-fate-occured");
            label_HttpRequestReadme.Text = Localization.GetText("ui-advanced-http-readme");
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
            IxionToolStripMenuItem.Text = Localization.GetText("fate-preset-Ixion");
            TamamoToolStripMenuItem.Text = Localization.GetText("fate-preset-Tamamo");
            frogMountToolStripMenuItem.Text = Localization.GetText("fate-preset-frogMount");
            frogSuitToolStripMenuItem.Text = Localization.GetText("fate-preset-frogSuit");
            toolStripMenuItem_SelectApply.Text = Localization.GetText("ui-fate-apply");
            label_FATEAbout.Text = Localization.GetText("ui-fate-about");
            toolStripMenuItem_LogCopy.Text = Localization.GetText("ui-logs-copy");
            toolStripMenuItem_LogClear.Text = Localization.GetText("ui-logs-clear");
            label_About.Text = Localization.GetText("ui-info-about");

            // Move items as String length change with languages.
            button_ResetOverlayPosition.Left = checkBox_Overlay.Location.X + checkBox_Overlay.Size.Width;
            button_getSoundFile.Left = checkBox_CustomSound.Location.X + checkBox_CustomSound.Size.Width;
            label_CustomSoundFileName.Left = button_getSoundFile.Location.X + button_getSoundFile.Size.Width;
            comboBox_Process.Left = label_Process.Location.X + label_Process.Size.Width;
            button_SelectProcess.Left = comboBox_Process.Location.X + comboBox_Process.Size.Width;
            button_ResetProcess.Left = button_SelectProcess.Location.X + button_SelectProcess.Size.Width;
        }
    }
}
