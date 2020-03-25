﻿namespace App
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button_ResetProcess = new System.Windows.Forms.Button();
            this.comboBox_Process = new System.Windows.Forms.ComboBox();
            this.button_SelectProcess = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_TopSetting = new System.Windows.Forms.Panel();
            this.comboBox_Language = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_Process = new App.LocalizableLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBox_Overlay = new System.Windows.Forms.CheckBox();
            this.tabControl = new App.TabControlBlack();
            this.tabPage_Settings = new System.Windows.Forms.TabPage();
            this.linkLabel_Localization = new System.Windows.Forms.LinkLabel();
            this.groupBox_DefaultSet = new System.Windows.Forms.GroupBox();
            this.checkBox_CopyMacro = new System.Windows.Forms.CheckBox();
            this.checkBox_autoHideOverlay = new System.Windows.Forms.CheckBox();
            this.checkBox_useVPN = new System.Windows.Forms.CheckBox();
            this.label_CustomSoundFileName = new System.Windows.Forms.Label();
            this.button_getSoundFile = new System.Windows.Forms.Button();
            this.checkBox_CustomSound = new System.Windows.Forms.CheckBox();
            this.checkBox_FateSound = new System.Windows.Forms.CheckBox();
            this.button_ResetOverlayPosition = new System.Windows.Forms.Button();
            this.checkBox_CheatRoullete = new System.Windows.Forms.CheckBox();
            this.checkBox_FlashWindow = new System.Windows.Forms.CheckBox();
            this.checkBox_StartupShow = new System.Windows.Forms.CheckBox();
            this.tabPage_3rdParty = new System.Windows.Forms.TabPage();
            this.linkLabel_Telegram = new System.Windows.Forms.LinkLabel();
            this.linkLabel_DiscordServer = new System.Windows.Forms.LinkLabel();
            this.groupBox_DiscordSet = new System.Windows.Forms.GroupBox();
            this.textBox_Discord = new System.Windows.Forms.TextBox();
            this.label_DiscordAt = new App.LocalizableLabel();
            this.label_DiscordAbout = new App.LocalizableLabel();
            this.checkBox_Discord = new System.Windows.Forms.CheckBox();
            this.groupBox_TelegramSet = new System.Windows.Forms.GroupBox();
            this.checkBox_Telegram_Queue_Status = new System.Windows.Forms.CheckBox();
            this.textBox_Telegram = new System.Windows.Forms.TextBox();
            this.label_Telegram_ChatId = new App.LocalizableLabel();
            this.label_TelegramAbout = new App.LocalizableLabel();
            this.checkBox_Telegram = new System.Windows.Forms.CheckBox();
            this.tabPage_Advanced = new System.Windows.Forms.TabPage();
            this.groupBox_debug = new System.Windows.Forms.GroupBox();
            this.checkBox_DebugLog = new System.Windows.Forms.CheckBox();
            this.groupBox_CustomHttpRequest = new System.Windows.Forms.GroupBox();
            this.label_HttpRequestReadme = new System.Windows.Forms.Label();
            this.textBox_CustomHttpUrl = new System.Windows.Forms.TextBox();
            this.label_HttpRequestUrl = new System.Windows.Forms.Label();
            this.checkBox_RequestOnDutyMatched = new System.Windows.Forms.CheckBox();
            this.checkBox_RequestOnFateOccur = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableHttpRequest = new System.Windows.Forms.CheckBox();
            this.tabPage_FATE = new System.Windows.Forms.TabPage();
            this.label_FATEAbout = new App.LocalizableLabel();
            this.triStateTreeView_FATEs = new RikTheVeggie.TriStateTreeView();
            this.menuStrip_FATETab = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_UnSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.presetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookOfSkyfireIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookOfSkyfireIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookOfNetherfireIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.bookOfSkyfallIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookOfSkyfallIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookOfNetherfireIToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.bookOfSkywindIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookOfSkywindIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.bookOfSkyearthIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.IxionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TamamoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.frogMountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frogSuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SelectApply = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_Log = new System.Windows.Forms.TabPage();
            this.panel_LogCover = new System.Windows.Forms.Panel();
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.menuStrip_LogTab = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_LogCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_LogClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_Info = new System.Windows.Forms.TabPage();
            this.label_About = new App.LocalizableLabel();
            this.linkLabel_GitHub = new System.Windows.Forms.LinkLabel();
            this.label_AboutTitle = new App.LocalizableLabel();
            this.contextMenuStrip.SuspendLayout();
            this.panel_TopSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage_Settings.SuspendLayout();
            this.groupBox_DefaultSet.SuspendLayout();
            this.tabPage_3rdParty.SuspendLayout();
            this.groupBox_DiscordSet.SuspendLayout();
            this.groupBox_TelegramSet.SuspendLayout();
            this.tabPage_Advanced.SuspendLayout();
            this.groupBox_debug.SuspendLayout();
            this.groupBox_CustomHttpRequest.SuspendLayout();
            this.tabPage_FATE.SuspendLayout();
            this.menuStrip_FATETab.SuspendLayout();
            this.tabPage_Log.SuspendLayout();
            this.panel_LogCover.SuspendLayout();
            this.menuStrip_LogTab.SuspendLayout();
            this.tabPage_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_ResetProcess
            // 
            this.button_ResetProcess.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.button_ResetProcess.Location = new System.Drawing.Point(293, 0);
            this.button_ResetProcess.Name = "button_ResetProcess";
            this.button_ResetProcess.Size = new System.Drawing.Size(60, 27);
            this.button_ResetProcess.TabIndex = 0;
            this.button_ResetProcess.Text = "재설정";
            this.button_ResetProcess.UseVisualStyleBackColor = true;
            this.button_ResetProcess.Click += new System.EventHandler(this.button_ResetProcess_Click);
            // 
            // comboBox_Process
            // 
            this.comboBox_Process.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Process.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.comboBox_Process.FormattingEnabled = true;
            this.comboBox_Process.Location = new System.Drawing.Point(107, 2);
            this.comboBox_Process.Name = "comboBox_Process";
            this.comboBox_Process.Size = new System.Drawing.Size(120, 23);
            this.comboBox_Process.Sorted = true;
            this.comboBox_Process.TabIndex = 0;
            // 
            // button_SelectProcess
            // 
            this.button_SelectProcess.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.button_SelectProcess.Location = new System.Drawing.Point(228, 0);
            this.button_SelectProcess.Name = "button_SelectProcess";
            this.button_SelectProcess.Size = new System.Drawing.Size(65, 27);
            this.button_SelectProcess.TabIndex = 0;
            this.button_SelectProcess.Text = "수동설정";
            this.button_SelectProcess.UseVisualStyleBackColor = true;
            this.button_SelectProcess.Click += new System.EventHandler(this.button_SelectProcess_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "임무/돌발 찾기 도우미";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Open,
            this.toolStripMenuItem_Close});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(99, 48);
            // 
            // toolStripMenuItem_Open
            // 
            this.toolStripMenuItem_Open.Name = "toolStripMenuItem_Open";
            this.toolStripMenuItem_Open.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItem_Open.Text = "열기";
            this.toolStripMenuItem_Open.Click += new System.EventHandler(this.toolStripMenuItem_Open_Click);
            // 
            // toolStripMenuItem_Close
            // 
            this.toolStripMenuItem_Close.Name = "toolStripMenuItem_Close";
            this.toolStripMenuItem_Close.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItem_Close.Text = "종료";
            this.toolStripMenuItem_Close.Click += new System.EventHandler(this.toolStripMenuItem_Close_Click);
            // 
            // panel_TopSetting
            // 
            this.panel_TopSetting.BackColor = System.Drawing.Color.Silver;
            this.panel_TopSetting.Controls.Add(this.comboBox_Language);
            this.panel_TopSetting.Controls.Add(this.pictureBox1);
            this.panel_TopSetting.Controls.Add(this.label_Process);
            this.panel_TopSetting.Controls.Add(this.comboBox_Process);
            this.panel_TopSetting.Controls.Add(this.button_SelectProcess);
            this.panel_TopSetting.Controls.Add(this.button_ResetProcess);
            this.panel_TopSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_TopSetting.Location = new System.Drawing.Point(0, 0);
            this.panel_TopSetting.Name = "panel_TopSetting";
            this.panel_TopSetting.Padding = new System.Windows.Forms.Padding(5);
            this.panel_TopSetting.Size = new System.Drawing.Size(544, 27);
            this.panel_TopSetting.TabIndex = 1;
            // 
            // comboBox_Language
            // 
            this.comboBox_Language.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Language.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox_Language.DisplayMember = "Code";
            this.comboBox_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Language.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.comboBox_Language.FormattingEnabled = true;
            this.comboBox_Language.Location = new System.Drawing.Point(480, 2);
            this.comboBox_Language.Name = "comboBox_Language";
            this.comboBox_Language.Size = new System.Drawing.Size(62, 23);
            this.comboBox_Language.TabIndex = 0;
            this.comboBox_Language.ValueMember = "Code";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::App.Properties.Resources.language2;
            this.pictureBox1.Location = new System.Drawing.Point(464, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label_Process
            // 
            this.label_Process.AutoSize = true;
            this.label_Process.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_Process.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Process.ForeColor = System.Drawing.Color.Gray;
            this.label_Process.Location = new System.Drawing.Point(5, 5);
            this.label_Process.Name = "label_Process";
            this.label_Process.Size = new System.Drawing.Size(100, 17);
            this.label_Process.TabIndex = 0;
            this.label_Process.Text = "FFXIV 프로세스";
            // 
            // checkBox_Overlay
            // 
            this.checkBox_Overlay.AutoSize = true;
            this.checkBox_Overlay.Checked = true;
            this.checkBox_Overlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Overlay.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_Overlay.Location = new System.Drawing.Point(6, 23);
            this.checkBox_Overlay.Name = "checkBox_Overlay";
            this.checkBox_Overlay.Size = new System.Drawing.Size(102, 19);
            this.checkBox_Overlay.TabIndex = 1;
            this.checkBox_Overlay.Text = "오버레이 사용";
            this.toolTip.SetToolTip(this.checkBox_Overlay, "오버레이 UI의 좌측 막대를 이용해 드래그 할 수 있습니다.");
            this.checkBox_Overlay.UseVisualStyleBackColor = true;
            this.checkBox_Overlay.CheckedChanged += new System.EventHandler(this.checkBox_Overlay_CheckedChanged);
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl.Controls.Add(this.tabPage_Settings);
            this.tabControl.Controls.Add(this.tabPage_3rdParty);
            this.tabControl.Controls.Add(this.tabPage_Advanced);
            this.tabControl.Controls.Add(this.tabPage_FATE);
            this.tabControl.Controls.Add(this.tabPage_Log);
            this.tabControl.Controls.Add(this.tabPage_Info);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ItemSize = new System.Drawing.Size(30, 110);
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(544, 271);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // tabPage_Settings
            // 
            this.tabPage_Settings.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Settings.Controls.Add(this.linkLabel_Localization);
            this.tabPage_Settings.Controls.Add(this.groupBox_DefaultSet);
            this.tabPage_Settings.Location = new System.Drawing.Point(114, 4);
            this.tabPage_Settings.Name = "tabPage_Settings";
            this.tabPage_Settings.Size = new System.Drawing.Size(426, 263);
            this.tabPage_Settings.TabIndex = 1;
            this.tabPage_Settings.Text = "설정";
            // 
            // linkLabel_Localization
            // 
            this.linkLabel_Localization.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkLabel_Localization.Location = new System.Drawing.Point(0, 237);
            this.linkLabel_Localization.Name = "linkLabel_Localization";
            this.linkLabel_Localization.Size = new System.Drawing.Size(426, 26);
            this.linkLabel_Localization.TabIndex = 7;
            this.linkLabel_Localization.TabStop = true;
            this.linkLabel_Localization.Text = "DFAssist 번역 참여";
            this.linkLabel_Localization.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_Localization.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Localization_LinkClicked);
            // 
            // groupBox_DefaultSet
            // 
            this.groupBox_DefaultSet.Controls.Add(this.checkBox_CopyMacro);
            this.groupBox_DefaultSet.Controls.Add(this.checkBox_autoHideOverlay);
            this.groupBox_DefaultSet.Controls.Add(this.checkBox_useVPN);
            this.groupBox_DefaultSet.Controls.Add(this.label_CustomSoundFileName);
            this.groupBox_DefaultSet.Controls.Add(this.button_getSoundFile);
            this.groupBox_DefaultSet.Controls.Add(this.checkBox_CustomSound);
            this.groupBox_DefaultSet.Controls.Add(this.checkBox_FateSound);
            this.groupBox_DefaultSet.Controls.Add(this.button_ResetOverlayPosition);
            this.groupBox_DefaultSet.Controls.Add(this.checkBox_CheatRoullete);
            this.groupBox_DefaultSet.Controls.Add(this.checkBox_FlashWindow);
            this.groupBox_DefaultSet.Controls.Add(this.checkBox_StartupShow);
            this.groupBox_DefaultSet.Controls.Add(this.checkBox_Overlay);
            this.groupBox_DefaultSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_DefaultSet.Location = new System.Drawing.Point(0, 0);
            this.groupBox_DefaultSet.Name = "groupBox_DefaultSet";
            this.groupBox_DefaultSet.Size = new System.Drawing.Size(426, 234);
            this.groupBox_DefaultSet.TabIndex = 0;
            this.groupBox_DefaultSet.TabStop = false;
            this.groupBox_DefaultSet.Text = "기본설정";
            // 
            // checkBox_CopyMacro
            // 
            this.checkBox_CopyMacro.AutoSize = true;
            this.checkBox_CopyMacro.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_CopyMacro.Location = new System.Drawing.Point(6, 128);
            this.checkBox_CopyMacro.Name = "checkBox_CopyMacro";
            this.checkBox_CopyMacro.Size = new System.Drawing.Size(345, 19);
            this.checkBox_CopyMacro.TabIndex = 7;
            this.checkBox_CopyMacro.Text = "매크로 정보가 있는 경우, 클립보드에 자동으로 매크로 복사";
            this.checkBox_CopyMacro.UseVisualStyleBackColor = true;
            this.checkBox_CopyMacro.CheckedChanged += new System.EventHandler(this.checkBox_CopyMacro_CheckedChanged);
            // 
            // checkBox_autoHideOverlay
            // 
            this.checkBox_autoHideOverlay.AutoSize = true;
            this.checkBox_autoHideOverlay.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_autoHideOverlay.Location = new System.Drawing.Point(6, 44);
            this.checkBox_autoHideOverlay.Name = "checkBox_autoHideOverlay";
            this.checkBox_autoHideOverlay.Size = new System.Drawing.Size(245, 19);
            this.checkBox_autoHideOverlay.TabIndex = 3;
            this.checkBox_autoHideOverlay.Text = "임무 매칭 중이 아닐 때, 오버레이 숨기기";
            this.checkBox_autoHideOverlay.UseVisualStyleBackColor = true;
            this.checkBox_autoHideOverlay.CheckedChanged += new System.EventHandler(this.checkBox_autoHideOverlay_CheckedChanged);
            this.checkBox_autoHideOverlay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_autoHideOverlay_MouseClick);
            // 
            // checkBox_useVPN
            // 
            this.checkBox_useVPN.AutoSize = true;
            this.checkBox_useVPN.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_useVPN.Location = new System.Drawing.Point(6, 149);
            this.checkBox_useVPN.Name = "checkBox_useVPN";
            this.checkBox_useVPN.Size = new System.Drawing.Size(222, 19);
            this.checkBox_useVPN.TabIndex = 8;
            this.checkBox_useVPN.Text = "로컬 네트워크 필터링 해제 (VPN 등)";
            this.checkBox_useVPN.UseVisualStyleBackColor = true;
            this.checkBox_useVPN.CheckedChanged += new System.EventHandler(this.checkBox_useVPN_CheckedChanged);
            this.checkBox_useVPN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkBox_useVPN_MouseUp);
            // 
            // label_CustomSoundFileName
            // 
            this.label_CustomSoundFileName.AutoSize = true;
            this.label_CustomSoundFileName.Location = new System.Drawing.Point(213, 207);
            this.label_CustomSoundFileName.Name = "label_CustomSoundFileName";
            this.label_CustomSoundFileName.Size = new System.Drawing.Size(115, 17);
            this.label_CustomSoundFileName.TabIndex = 0;
            this.label_CustomSoundFileName.Text = "(기본값: 비워두기)";
            // 
            // button_getSoundFile
            // 
            this.button_getSoundFile.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.button_getSoundFile.Location = new System.Drawing.Point(142, 206);
            this.button_getSoundFile.Name = "button_getSoundFile";
            this.button_getSoundFile.Size = new System.Drawing.Size(70, 20);
            this.button_getSoundFile.TabIndex = 11;
            this.button_getSoundFile.Text = "파일 선택";
            this.button_getSoundFile.UseVisualStyleBackColor = true;
            this.button_getSoundFile.Click += new System.EventHandler(this.button_getSoundFile_Click);
            // 
            // checkBox_CustomSound
            // 
            this.checkBox_CustomSound.AutoSize = true;
            this.checkBox_CustomSound.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_CustomSound.Location = new System.Drawing.Point(6, 207);
            this.checkBox_CustomSound.Name = "checkBox_CustomSound";
            this.checkBox_CustomSound.Size = new System.Drawing.Size(130, 19);
            this.checkBox_CustomSound.TabIndex = 10;
            this.checkBox_CustomSound.Text = "알림음 사용자 설정";
            this.checkBox_CustomSound.UseVisualStyleBackColor = true;
            this.checkBox_CustomSound.CheckedChanged += new System.EventHandler(this.checkBox_CustomSound_CheckedChanged);
            this.checkBox_CustomSound.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBox_CustomSound_MouseDown);
            // 
            // checkBox_FateSound
            // 
            this.checkBox_FateSound.AutoSize = true;
            this.checkBox_FateSound.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_FateSound.Location = new System.Drawing.Point(6, 170);
            this.checkBox_FateSound.Name = "checkBox_FateSound";
            this.checkBox_FateSound.Size = new System.Drawing.Size(270, 34);
            this.checkBox_FateSound.TabIndex = 9;
            this.checkBox_FateSound.Text = "돌발임무 알림음 활성화\r\n(돌발임무 알림에서 사용자 알림음 사용 안함)";
            this.checkBox_FateSound.UseVisualStyleBackColor = true;
            this.checkBox_FateSound.CheckedChanged += new System.EventHandler(this.checkBox_FateSound_CheckedChanged);
            // 
            // button_ResetOverlayPosition
            // 
            this.button_ResetOverlayPosition.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.button_ResetOverlayPosition.Location = new System.Drawing.Point(110, 22);
            this.button_ResetOverlayPosition.Name = "button_ResetOverlayPosition";
            this.button_ResetOverlayPosition.Size = new System.Drawing.Size(74, 20);
            this.button_ResetOverlayPosition.TabIndex = 2;
            this.button_ResetOverlayPosition.Text = "위치 초기화";
            this.button_ResetOverlayPosition.UseVisualStyleBackColor = true;
            this.button_ResetOverlayPosition.Click += new System.EventHandler(this.button_ResetOverlayPosition_Click);
            // 
            // checkBox_CheatRoullete
            // 
            this.checkBox_CheatRoullete.AutoSize = true;
            this.checkBox_CheatRoullete.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_CheatRoullete.Location = new System.Drawing.Point(6, 107);
            this.checkBox_CheatRoullete.Name = "checkBox_CheatRoullete";
            this.checkBox_CheatRoullete.Size = new System.Drawing.Size(302, 19);
            this.checkBox_CheatRoullete.TabIndex = 6;
            this.checkBox_CheatRoullete.Text = "무작위 임무일 경우에도 실제 매칭된 임무 보여주기";
            this.checkBox_CheatRoullete.UseVisualStyleBackColor = true;
            this.checkBox_CheatRoullete.CheckedChanged += new System.EventHandler(this.checkBox_CheatRoullete_CheckedChanged);
            // 
            // checkBox_FlashWindow
            // 
            this.checkBox_FlashWindow.AutoSize = true;
            this.checkBox_FlashWindow.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_FlashWindow.Location = new System.Drawing.Point(6, 86);
            this.checkBox_FlashWindow.Name = "checkBox_FlashWindow";
            this.checkBox_FlashWindow.Size = new System.Drawing.Size(369, 19);
            this.checkBox_FlashWindow.TabIndex = 5;
            this.checkBox_FlashWindow.Text = "매칭/돌발 발생시 파이널판타지14 작업 표시줄 아이콘 깜빡이기";
            this.checkBox_FlashWindow.UseVisualStyleBackColor = true;
            this.checkBox_FlashWindow.CheckedChanged += new System.EventHandler(this.checkBox_FlashWindow_CheckedChanged);
            // 
            // checkBox_StartupShow
            // 
            this.checkBox_StartupShow.AutoSize = true;
            this.checkBox_StartupShow.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_StartupShow.Location = new System.Drawing.Point(6, 65);
            this.checkBox_StartupShow.Name = "checkBox_StartupShow";
            this.checkBox_StartupShow.Size = new System.Drawing.Size(186, 19);
            this.checkBox_StartupShow.TabIndex = 4;
            this.checkBox_StartupShow.Text = "프로그램 시작시 이 창 보이기";
            this.checkBox_StartupShow.UseVisualStyleBackColor = true;
            this.checkBox_StartupShow.CheckedChanged += new System.EventHandler(this.checkBox_StartupShow_CheckedChanged);
            // 
            // tabPage_3rdParty
            // 
            this.tabPage_3rdParty.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_3rdParty.Controls.Add(this.linkLabel_Telegram);
            this.tabPage_3rdParty.Controls.Add(this.linkLabel_DiscordServer);
            this.tabPage_3rdParty.Controls.Add(this.groupBox_DiscordSet);
            this.tabPage_3rdParty.Controls.Add(this.groupBox_TelegramSet);
            this.tabPage_3rdParty.Location = new System.Drawing.Point(114, 4);
            this.tabPage_3rdParty.Name = "tabPage_3rdParty";
            this.tabPage_3rdParty.Size = new System.Drawing.Size(426, 263);
            this.tabPage_3rdParty.TabIndex = 5;
            this.tabPage_3rdParty.Text = "외부 알림";
            // 
            // linkLabel_Telegram
            // 
            this.linkLabel_Telegram.Location = new System.Drawing.Point(-2, 243);
            this.linkLabel_Telegram.Name = "linkLabel_Telegram";
            this.linkLabel_Telegram.Size = new System.Drawing.Size(213, 20);
            this.linkLabel_Telegram.TabIndex = 6;
            this.linkLabel_Telegram.TabStop = true;
            this.linkLabel_Telegram.Text = "DFAssist Telegram Bot";
            this.linkLabel_Telegram.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_Telegram.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Telegram_LinkClicked);
            // 
            // linkLabel_DiscordServer
            // 
            this.linkLabel_DiscordServer.Location = new System.Drawing.Point(210, 243);
            this.linkLabel_DiscordServer.Name = "linkLabel_DiscordServer";
            this.linkLabel_DiscordServer.Size = new System.Drawing.Size(216, 20);
            this.linkLabel_DiscordServer.TabIndex = 4;
            this.linkLabel_DiscordServer.TabStop = true;
            this.linkLabel_DiscordServer.Text = "DFAssist Discord Server";
            this.linkLabel_DiscordServer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_DiscordServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_DiscordServer_LinkClicked);
            // 
            // groupBox_DiscordSet
            // 
            this.groupBox_DiscordSet.Controls.Add(this.textBox_Discord);
            this.groupBox_DiscordSet.Controls.Add(this.label_DiscordAt);
            this.groupBox_DiscordSet.Controls.Add(this.label_DiscordAbout);
            this.groupBox_DiscordSet.Controls.Add(this.checkBox_Discord);
            this.groupBox_DiscordSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_DiscordSet.Location = new System.Drawing.Point(0, 102);
            this.groupBox_DiscordSet.Name = "groupBox_DiscordSet";
            this.groupBox_DiscordSet.Size = new System.Drawing.Size(426, 140);
            this.groupBox_DiscordSet.TabIndex = 2;
            this.groupBox_DiscordSet.TabStop = false;
            this.groupBox_DiscordSet.Text = "디스코드 알림";
            // 
            // textBox_Discord
            // 
            this.textBox_Discord.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Discord.Enabled = false;
            this.textBox_Discord.ForeColor = System.Drawing.Color.Black;
            this.textBox_Discord.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Discord.Location = new System.Drawing.Point(30, 18);
            this.textBox_Discord.MaxLength = 18;
            this.textBox_Discord.Name = "textBox_Discord";
            this.textBox_Discord.Size = new System.Drawing.Size(137, 25);
            this.textBox_Discord.TabIndex = 11;
            this.textBox_Discord.TextChanged += new System.EventHandler(this.textBox_Discord_TextChanged);
            this.textBox_Discord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Discord_KeyPress);
            // 
            // label_DiscordAt
            // 
            this.label_DiscordAt.AutoSize = true;
            this.label_DiscordAt.Location = new System.Drawing.Point(4, 21);
            this.label_DiscordAt.Name = "label_DiscordAt";
            this.label_DiscordAt.Size = new System.Drawing.Size(24, 17);
            this.label_DiscordAt.TabIndex = 12;
            this.label_DiscordAt.Text = "ID:";
            // 
            // label_DiscordAbout
            // 
            this.label_DiscordAbout.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.label_DiscordAbout.Location = new System.Drawing.Point(4, 45);
            this.label_DiscordAbout.Name = "label_DiscordAbout";
            this.label_DiscordAbout.Size = new System.Drawing.Size(421, 92);
            this.label_DiscordAbout.TabIndex = 13;
            this.label_DiscordAbout.Text = "입력된 디스코드 계정으로도 멘션을 통해 알림을 보냅니다.\r\n디스코드 ID는 15자리 이상의 숫자입니다.\r\n\r\n디스코드 ID는 DFAssist 디스" +
    "코드 서버에서 #get-discord-id 채널에 !id 를 입력하면 확인할 수 있습니다.\r\n또한, DFAssist 디스코드 서버에 참여한 상태" +
    "여야만 알림을 보낼 수 있습니다.";
            // 
            // checkBox_Discord
            // 
            this.checkBox_Discord.AutoSize = true;
            this.checkBox_Discord.Location = new System.Drawing.Point(173, 20);
            this.checkBox_Discord.Name = "checkBox_Discord";
            this.checkBox_Discord.Size = new System.Drawing.Size(66, 21);
            this.checkBox_Discord.TabIndex = 14;
            this.checkBox_Discord.Text = "활성화";
            this.checkBox_Discord.UseVisualStyleBackColor = true;
            this.checkBox_Discord.CheckedChanged += new System.EventHandler(this.checkBox_Discord_CheckedChanged);
            // 
            // groupBox_TelegramSet
            // 
            this.groupBox_TelegramSet.Controls.Add(this.checkBox_Telegram_Queue_Status);
            this.groupBox_TelegramSet.Controls.Add(this.textBox_Telegram);
            this.groupBox_TelegramSet.Controls.Add(this.label_Telegram_ChatId);
            this.groupBox_TelegramSet.Controls.Add(this.label_TelegramAbout);
            this.groupBox_TelegramSet.Controls.Add(this.checkBox_Telegram);
            this.groupBox_TelegramSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_TelegramSet.Location = new System.Drawing.Point(0, 0);
            this.groupBox_TelegramSet.Name = "groupBox_TelegramSet";
            this.groupBox_TelegramSet.Size = new System.Drawing.Size(426, 102);
            this.groupBox_TelegramSet.TabIndex = 1;
            this.groupBox_TelegramSet.TabStop = false;
            this.groupBox_TelegramSet.Text = "텔레그램 알림";
            // 
            // checkBox_Telegram_Queue_Status
            // 
            this.checkBox_Telegram_Queue_Status.AutoSize = true;
            this.checkBox_Telegram_Queue_Status.Location = new System.Drawing.Point(237, 20);
            this.checkBox_Telegram_Queue_Status.Name = "checkBox_Telegram_Queue_Status";
            this.checkBox_Telegram_Queue_Status.Size = new System.Drawing.Size(115, 21);
            this.checkBox_Telegram_Queue_Status.TabIndex = 15;
            this.checkBox_Telegram_Queue_Status.Text = "매칭 현황 전송";
            this.checkBox_Telegram_Queue_Status.UseVisualStyleBackColor = true;
            this.checkBox_Telegram_Queue_Status.CheckedChanged += new System.EventHandler(this.checkBox_Telegram_Queue_Status_CheckedChanged);
            // 
            // textBox_Telegram
            // 
            this.textBox_Telegram.Enabled = false;
            this.textBox_Telegram.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Telegram.Location = new System.Drawing.Point(62, 18);
            this.textBox_Telegram.MaxLength = 12;
            this.textBox_Telegram.Name = "textBox_Telegram";
            this.textBox_Telegram.Size = new System.Drawing.Size(97, 25);
            this.textBox_Telegram.TabIndex = 11;
            this.textBox_Telegram.TextChanged += new System.EventHandler(this.textBox_Telegram_TextChanged);
            // 
            // label_Telegram_ChatId
            // 
            this.label_Telegram_ChatId.AutoSize = true;
            this.label_Telegram_ChatId.Location = new System.Drawing.Point(4, 21);
            this.label_Telegram_ChatId.Name = "label_Telegram_ChatId";
            this.label_Telegram_ChatId.Size = new System.Drawing.Size(56, 17);
            this.label_Telegram_ChatId.TabIndex = 12;
            this.label_Telegram_ChatId.Text = "Chat ID:";
            // 
            // label_TelegramAbout
            // 
            this.label_TelegramAbout.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.label_TelegramAbout.Location = new System.Drawing.Point(4, 45);
            this.label_TelegramAbout.Name = "label_TelegramAbout";
            this.label_TelegramAbout.Size = new System.Drawing.Size(421, 53);
            this.label_TelegramAbout.TabIndex = 13;
            this.label_TelegramAbout.Text = "매칭이 되면 입력한 텔레그램 채팅방으로 메시지를 보내 해당 사실을 알립니다.\r\n원하는 돌발이 발생했을 시에도 메시지를 보내 해당 사실을 알립니다." +
    "\r\n채팅방 ID는 DFAssist Notify Bot에게 아무 메시지나 보내면 확인할 수 있습니다.";
            // 
            // checkBox_Telegram
            // 
            this.checkBox_Telegram.AutoSize = true;
            this.checkBox_Telegram.Location = new System.Drawing.Point(165, 20);
            this.checkBox_Telegram.Name = "checkBox_Telegram";
            this.checkBox_Telegram.Size = new System.Drawing.Size(66, 21);
            this.checkBox_Telegram.TabIndex = 14;
            this.checkBox_Telegram.Text = "활성화";
            this.checkBox_Telegram.UseVisualStyleBackColor = true;
            this.checkBox_Telegram.CheckedChanged += new System.EventHandler(this.checkBox_Telegram_CheckedChanged);
            // 
            // tabPage_Advanced
            // 
            this.tabPage_Advanced.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Advanced.Controls.Add(this.groupBox_debug);
            this.tabPage_Advanced.Controls.Add(this.groupBox_CustomHttpRequest);
            this.tabPage_Advanced.Location = new System.Drawing.Point(114, 4);
            this.tabPage_Advanced.Name = "tabPage_Advanced";
            this.tabPage_Advanced.Size = new System.Drawing.Size(426, 263);
            this.tabPage_Advanced.TabIndex = 4;
            this.tabPage_Advanced.Text = "고급";
            // 
            // groupBox_debug
            // 
            this.groupBox_debug.Controls.Add(this.checkBox_DebugLog);
            this.groupBox_debug.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_debug.Location = new System.Drawing.Point(0, 189);
            this.groupBox_debug.Name = "groupBox_debug";
            this.groupBox_debug.Size = new System.Drawing.Size(426, 75);
            this.groupBox_debug.TabIndex = 6;
            this.groupBox_debug.TabStop = false;
            this.groupBox_debug.Text = "디버그";
            // 
            // checkBox_DebugLog
            // 
            this.checkBox_DebugLog.AutoSize = true;
            this.checkBox_DebugLog.Location = new System.Drawing.Point(7, 25);
            this.checkBox_DebugLog.Name = "checkBox_DebugLog";
            this.checkBox_DebugLog.Size = new System.Drawing.Size(128, 21);
            this.checkBox_DebugLog.TabIndex = 0;
            this.checkBox_DebugLog.Text = "디버그 로그 출력";
            this.checkBox_DebugLog.UseVisualStyleBackColor = true;
            this.checkBox_DebugLog.CheckedChanged += new System.EventHandler(this.checkBox_DebugLog_CheckedChanged);
            // 
            // groupBox_CustomHttpRequest
            // 
            this.groupBox_CustomHttpRequest.Controls.Add(this.label_HttpRequestReadme);
            this.groupBox_CustomHttpRequest.Controls.Add(this.textBox_CustomHttpUrl);
            this.groupBox_CustomHttpRequest.Controls.Add(this.label_HttpRequestUrl);
            this.groupBox_CustomHttpRequest.Controls.Add(this.checkBox_RequestOnDutyMatched);
            this.groupBox_CustomHttpRequest.Controls.Add(this.checkBox_RequestOnFateOccur);
            this.groupBox_CustomHttpRequest.Controls.Add(this.checkBox_EnableHttpRequest);
            this.groupBox_CustomHttpRequest.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_CustomHttpRequest.Location = new System.Drawing.Point(0, 0);
            this.groupBox_CustomHttpRequest.Name = "groupBox_CustomHttpRequest";
            this.groupBox_CustomHttpRequest.Size = new System.Drawing.Size(426, 189);
            this.groupBox_CustomHttpRequest.TabIndex = 5;
            this.groupBox_CustomHttpRequest.TabStop = false;
            this.groupBox_CustomHttpRequest.Text = "사용자 지정 HTTP 요청";
            // 
            // label_HttpRequestReadme
            // 
            this.label_HttpRequestReadme.AutoSize = true;
            this.label_HttpRequestReadme.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_HttpRequestReadme.Location = new System.Drawing.Point(1, 101);
            this.label_HttpRequestReadme.Name = "label_HttpRequestReadme";
            this.label_HttpRequestReadme.Size = new System.Drawing.Size(428, 78);
            this.label_HttpRequestReadme.TabIndex = 9;
            this.label_HttpRequestReadme.Text = resources.GetString("label_HttpRequestReadme.Text");
            // 
            // textBox_CustomHttpUrl
            // 
            this.textBox_CustomHttpUrl.Enabled = false;
            this.textBox_CustomHttpUrl.Location = new System.Drawing.Point(49, 20);
            this.textBox_CustomHttpUrl.Name = "textBox_CustomHttpUrl";
            this.textBox_CustomHttpUrl.Size = new System.Drawing.Size(300, 25);
            this.textBox_CustomHttpUrl.TabIndex = 7;
            this.textBox_CustomHttpUrl.TextChanged += new System.EventHandler(this.textBox_CustomHttpUrl_TextChanged);
            // 
            // label_HttpRequestUrl
            // 
            this.label_HttpRequestUrl.AutoSize = true;
            this.label_HttpRequestUrl.Location = new System.Drawing.Point(3, 23);
            this.label_HttpRequestUrl.Name = "label_HttpRequestUrl";
            this.label_HttpRequestUrl.Size = new System.Drawing.Size(45, 17);
            this.label_HttpRequestUrl.TabIndex = 8;
            this.label_HttpRequestUrl.Text = "http://";
            // 
            // checkBox_RequestOnDutyMatched
            // 
            this.checkBox_RequestOnDutyMatched.AutoSize = true;
            this.checkBox_RequestOnDutyMatched.Enabled = false;
            this.checkBox_RequestOnDutyMatched.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_RequestOnDutyMatched.Location = new System.Drawing.Point(6, 53);
            this.checkBox_RequestOnDutyMatched.Name = "checkBox_RequestOnDutyMatched";
            this.checkBox_RequestOnDutyMatched.Size = new System.Drawing.Size(158, 19);
            this.checkBox_RequestOnDutyMatched.TabIndex = 6;
            this.checkBox_RequestOnDutyMatched.Text = "던전 매칭 완료시 보내기";
            this.checkBox_RequestOnDutyMatched.UseVisualStyleBackColor = true;
            this.checkBox_RequestOnDutyMatched.CheckedChanged += new System.EventHandler(this.checkBox_RequestOnDutyMatched_CheckedChanged);
            // 
            // checkBox_RequestOnFateOccur
            // 
            this.checkBox_RequestOnFateOccur.AutoSize = true;
            this.checkBox_RequestOnFateOccur.Enabled = false;
            this.checkBox_RequestOnFateOccur.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_RequestOnFateOccur.Location = new System.Drawing.Point(6, 75);
            this.checkBox_RequestOnFateOccur.Name = "checkBox_RequestOnFateOccur";
            this.checkBox_RequestOnFateOccur.Size = new System.Drawing.Size(158, 19);
            this.checkBox_RequestOnFateOccur.TabIndex = 5;
            this.checkBox_RequestOnFateOccur.Text = "돌발 임무 발생시 보내기";
            this.checkBox_RequestOnFateOccur.UseVisualStyleBackColor = true;
            this.checkBox_RequestOnFateOccur.CheckedChanged += new System.EventHandler(this.checkBox_RequestOnFateOccur_CheckedChanged);
            // 
            // checkBox_EnableHttpRequest
            // 
            this.checkBox_EnableHttpRequest.AutoSize = true;
            this.checkBox_EnableHttpRequest.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_EnableHttpRequest.Location = new System.Drawing.Point(355, 23);
            this.checkBox_EnableHttpRequest.Name = "checkBox_EnableHttpRequest";
            this.checkBox_EnableHttpRequest.Size = new System.Drawing.Size(62, 19);
            this.checkBox_EnableHttpRequest.TabIndex = 4;
            this.checkBox_EnableHttpRequest.Text = "활성화";
            this.checkBox_EnableHttpRequest.UseVisualStyleBackColor = true;
            this.checkBox_EnableHttpRequest.CheckedChanged += new System.EventHandler(this.checkBox_EnableHttpRequest_CheckedChanged);
            // 
            // tabPage_FATE
            // 
            this.tabPage_FATE.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_FATE.Controls.Add(this.label_FATEAbout);
            this.tabPage_FATE.Controls.Add(this.triStateTreeView_FATEs);
            this.tabPage_FATE.Controls.Add(this.menuStrip_FATETab);
            this.tabPage_FATE.Location = new System.Drawing.Point(114, 4);
            this.tabPage_FATE.Name = "tabPage_FATE";
            this.tabPage_FATE.Size = new System.Drawing.Size(426, 263);
            this.tabPage_FATE.TabIndex = 0;
            this.tabPage_FATE.Text = "돌발";
            // 
            // label_FATEAbout
            // 
            this.label_FATEAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_FATEAbout.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.label_FATEAbout.Location = new System.Drawing.Point(0, 231);
            this.label_FATEAbout.Name = "label_FATEAbout";
            this.label_FATEAbout.Size = new System.Drawing.Size(426, 32);
            this.label_FATEAbout.TabIndex = 2;
            this.label_FATEAbout.Text = "알림을 받고자 하는 돌발을 선택하신 뒤 상단 적용하기 버튼을 눌러주세요.\r\n돌발 알림은 캐릭터가 해당 지역에 있을 때에만 동작합니다.";
            // 
            // triStateTreeView_FATEs
            // 
            this.triStateTreeView_FATEs.BackColor = System.Drawing.SystemColors.Control;
            this.triStateTreeView_FATEs.Dock = System.Windows.Forms.DockStyle.Top;
            this.triStateTreeView_FATEs.Font = new System.Drawing.Font("Meiryo", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.triStateTreeView_FATEs.FullRowSelect = true;
            this.triStateTreeView_FATEs.HotTracking = true;
            this.triStateTreeView_FATEs.Location = new System.Drawing.Point(0, 24);
            this.triStateTreeView_FATEs.Name = "triStateTreeView_FATEs";
            this.triStateTreeView_FATEs.Size = new System.Drawing.Size(426, 203);
            this.triStateTreeView_FATEs.TabIndex = 0;
            this.triStateTreeView_FATEs.TriStateStyleProperty = RikTheVeggie.TriStateTreeView.TriStateStyles.Installer;
            // 
            // menuStrip_FATETab
            // 
            this.menuStrip_FATETab.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip_FATETab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_SelectAll,
            this.toolStripMenuItem_UnSelectAll,
            this.presetToolStripMenuItem,
            this.toolStripMenuItem_SelectApply});
            this.menuStrip_FATETab.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_FATETab.Name = "menuStrip_FATETab";
            this.menuStrip_FATETab.Size = new System.Drawing.Size(426, 24);
            this.menuStrip_FATETab.TabIndex = 0;
            this.menuStrip_FATETab.Text = "menuStrip1";
            // 
            // toolStripMenuItem_SelectAll
            // 
            this.toolStripMenuItem_SelectAll.Name = "toolStripMenuItem_SelectAll";
            this.toolStripMenuItem_SelectAll.Size = new System.Drawing.Size(71, 20);
            this.toolStripMenuItem_SelectAll.Text = "모두 선택";
            this.toolStripMenuItem_SelectAll.Click += new System.EventHandler(this.toolStripMenuItem_SelectAll_Click);
            // 
            // toolStripMenuItem_UnSelectAll
            // 
            this.toolStripMenuItem_UnSelectAll.Name = "toolStripMenuItem_UnSelectAll";
            this.toolStripMenuItem_UnSelectAll.Size = new System.Drawing.Size(71, 20);
            this.toolStripMenuItem_UnSelectAll.Text = "모두 해제";
            this.toolStripMenuItem_UnSelectAll.Click += new System.EventHandler(this.toolStripMenuItem_UnSelectAll_Click);
            // 
            // presetToolStripMenuItem
            // 
            this.presetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bookOfSkyfireIToolStripMenuItem,
            this.bookOfSkyfireIIToolStripMenuItem,
            this.bookOfNetherfireIToolStripMenuItem,
            this.toolStripMenuItem1,
            this.bookOfSkyfallIToolStripMenuItem,
            this.bookOfSkyfallIIToolStripMenuItem,
            this.bookOfNetherfireIToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.bookOfSkywindIToolStripMenuItem,
            this.bookOfSkywindIIToolStripMenuItem,
            this.toolStripMenuItem3,
            this.bookOfSkyearthIToolStripMenuItem,
            this.toolStripSeparator1,
            this.IxionToolStripMenuItem,
            this.TamamoToolStripMenuItem,
            this.toolStripSeparator2,
            this.frogMountToolStripMenuItem,
            this.frogSuitToolStripMenuItem});
            this.presetToolStripMenuItem.Name = "presetToolStripMenuItem";
            this.presetToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.presetToolStripMenuItem.Text = "프리셋";
            // 
            // bookOfSkyfireIToolStripMenuItem
            // 
            this.bookOfSkyfireIToolStripMenuItem.Name = "bookOfSkyfireIToolStripMenuItem";
            this.bookOfSkyfireIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.bookOfSkyfireIToolStripMenuItem.Text = "불의 서 1권";
            this.bookOfSkyfireIToolStripMenuItem.Click += new System.EventHandler(this.bookOfSkyfireIToolStripMenuItem_Click);
            // 
            // bookOfSkyfireIIToolStripMenuItem
            // 
            this.bookOfSkyfireIIToolStripMenuItem.Name = "bookOfSkyfireIIToolStripMenuItem";
            this.bookOfSkyfireIIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.bookOfSkyfireIIToolStripMenuItem.Text = "불의 서 2권";
            this.bookOfSkyfireIIToolStripMenuItem.Click += new System.EventHandler(this.bookOfSkyfireIIToolStripMenuItem_Click);
            // 
            // bookOfNetherfireIToolStripMenuItem
            // 
            this.bookOfNetherfireIToolStripMenuItem.Name = "bookOfNetherfireIToolStripMenuItem";
            this.bookOfNetherfireIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.bookOfNetherfireIToolStripMenuItem.Text = "불의 서 3권";
            this.bookOfNetherfireIToolStripMenuItem.Click += new System.EventHandler(this.bookOfNetherfireIToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(199, 6);
            // 
            // bookOfSkyfallIToolStripMenuItem
            // 
            this.bookOfSkyfallIToolStripMenuItem.Name = "bookOfSkyfallIToolStripMenuItem";
            this.bookOfSkyfallIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.bookOfSkyfallIToolStripMenuItem.Text = "물의 서 1권";
            this.bookOfSkyfallIToolStripMenuItem.Click += new System.EventHandler(this.bookOfSkyfallIToolStripMenuItem_Click);
            // 
            // bookOfSkyfallIIToolStripMenuItem
            // 
            this.bookOfSkyfallIIToolStripMenuItem.Name = "bookOfSkyfallIIToolStripMenuItem";
            this.bookOfSkyfallIIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.bookOfSkyfallIIToolStripMenuItem.Text = "물의 서 2권";
            this.bookOfSkyfallIIToolStripMenuItem.Click += new System.EventHandler(this.bookOfSkyfallIIToolStripMenuItem_Click);
            // 
            // bookOfNetherfireIToolStripMenuItem1
            // 
            this.bookOfNetherfireIToolStripMenuItem1.Name = "bookOfNetherfireIToolStripMenuItem1";
            this.bookOfNetherfireIToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.bookOfNetherfireIToolStripMenuItem1.Text = "물의 서 3권";
            this.bookOfNetherfireIToolStripMenuItem1.Click += new System.EventHandler(this.bookOfNetherfireIToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(199, 6);
            // 
            // bookOfSkywindIToolStripMenuItem
            // 
            this.bookOfSkywindIToolStripMenuItem.Name = "bookOfSkywindIToolStripMenuItem";
            this.bookOfSkywindIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.bookOfSkywindIToolStripMenuItem.Text = "바람의 서 1권";
            this.bookOfSkywindIToolStripMenuItem.Click += new System.EventHandler(this.bookOfSkywindIToolStripMenuItem_Click);
            // 
            // bookOfSkywindIIToolStripMenuItem
            // 
            this.bookOfSkywindIIToolStripMenuItem.Name = "bookOfSkywindIIToolStripMenuItem";
            this.bookOfSkywindIIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.bookOfSkywindIIToolStripMenuItem.Text = "바람의 서 2권";
            this.bookOfSkywindIIToolStripMenuItem.Click += new System.EventHandler(this.bookOfSkywindIIToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(199, 6);
            // 
            // bookOfSkyearthIToolStripMenuItem
            // 
            this.bookOfSkyearthIToolStripMenuItem.Name = "bookOfSkyearthIToolStripMenuItem";
            this.bookOfSkyearthIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.bookOfSkyearthIToolStripMenuItem.Text = "땅의 서 1권";
            this.bookOfSkyearthIToolStripMenuItem.Click += new System.EventHandler(this.bookOfSkyearthIToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // IxionToolStripMenuItem
            // 
            this.IxionToolStripMenuItem.Name = "IxionToolStripMenuItem";
            this.IxionToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.IxionToolStripMenuItem.Text = "익시온 (호반지대)";
            this.IxionToolStripMenuItem.Click += new System.EventHandler(this.IxionToolStripMenuItem_Click);
            // 
            // TamamoToolStripMenuItem
            // 
            this.TamamoToolStripMenuItem.Name = "TamamoToolStripMenuItem";
            this.TamamoToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.TamamoToolStripMenuItem.Text = "타마모 (얀샤)";
            this.TamamoToolStripMenuItem.Click += new System.EventHandler(this.TamamoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // frogMountToolStripMenuItem
            // 
            this.frogMountToolStripMenuItem.Name = "frogMountToolStripMenuItem";
            this.frogMountToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.frogMountToolStripMenuItem.Text = "개구리 탈 것 (콜루시아)";
            this.frogMountToolStripMenuItem.Click += new System.EventHandler(this.frogMountToolStripMenuItem_Click);
            // 
            // frogSuitToolStripMenuItem
            // 
            this.frogSuitToolStripMenuItem.Name = "frogSuitToolStripMenuItem";
            this.frogSuitToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.frogSuitToolStripMenuItem.Text = "개구리 옷 (템페스트)";
            this.frogSuitToolStripMenuItem.Click += new System.EventHandler(this.frogSuitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_SelectApply
            // 
            this.toolStripMenuItem_SelectApply.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem_SelectApply.Name = "toolStripMenuItem_SelectApply";
            this.toolStripMenuItem_SelectApply.Size = new System.Drawing.Size(67, 20);
            this.toolStripMenuItem_SelectApply.Text = "적용하기";
            this.toolStripMenuItem_SelectApply.Click += new System.EventHandler(this.toolStripMenuItem_SelectApply_Click);
            // 
            // tabPage_Log
            // 
            this.tabPage_Log.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Log.Controls.Add(this.panel_LogCover);
            this.tabPage_Log.Controls.Add(this.menuStrip_LogTab);
            this.tabPage_Log.Location = new System.Drawing.Point(114, 4);
            this.tabPage_Log.Name = "tabPage_Log";
            this.tabPage_Log.Size = new System.Drawing.Size(426, 263);
            this.tabPage_Log.TabIndex = 2;
            this.tabPage_Log.Text = "로그";
            // 
            // panel_LogCover
            // 
            this.panel_LogCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_LogCover.Controls.Add(this.richTextBox_Log);
            this.panel_LogCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LogCover.Location = new System.Drawing.Point(0, 24);
            this.panel_LogCover.Name = "panel_LogCover";
            this.panel_LogCover.Size = new System.Drawing.Size(426, 239);
            this.panel_LogCover.TabIndex = 1;
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox_Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Log.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.richTextBox_Log.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.ReadOnly = true;
            this.richTextBox_Log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox_Log.Size = new System.Drawing.Size(424, 237);
            this.richTextBox_Log.TabIndex = 0;
            this.richTextBox_Log.Text = "";
            this.richTextBox_Log.TextChanged += new System.EventHandler(this.richTextBox_Log_TextChanged);
            // 
            // menuStrip_LogTab
            // 
            this.menuStrip_LogTab.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip_LogTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_LogCopy,
            this.toolStripMenuItem_LogClear});
            this.menuStrip_LogTab.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_LogTab.Name = "menuStrip_LogTab";
            this.menuStrip_LogTab.Size = new System.Drawing.Size(426, 24);
            this.menuStrip_LogTab.TabIndex = 0;
            this.menuStrip_LogTab.Text = "menuStrip2";
            // 
            // toolStripMenuItem_LogCopy
            // 
            this.toolStripMenuItem_LogCopy.Name = "toolStripMenuItem_LogCopy";
            this.toolStripMenuItem_LogCopy.Size = new System.Drawing.Size(71, 20);
            this.toolStripMenuItem_LogCopy.Text = "로그 복사";
            this.toolStripMenuItem_LogCopy.Click += new System.EventHandler(this.toolStripMenuItem_LogCopy_Click);
            // 
            // toolStripMenuItem_LogClear
            // 
            this.toolStripMenuItem_LogClear.Name = "toolStripMenuItem_LogClear";
            this.toolStripMenuItem_LogClear.Size = new System.Drawing.Size(71, 20);
            this.toolStripMenuItem_LogClear.Text = "로그 삭제";
            this.toolStripMenuItem_LogClear.Click += new System.EventHandler(this.toolStripMenuItem_LogClear_Click);
            // 
            // tabPage_Info
            // 
            this.tabPage_Info.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Info.Controls.Add(this.label_About);
            this.tabPage_Info.Controls.Add(this.linkLabel_GitHub);
            this.tabPage_Info.Location = new System.Drawing.Point(114, 4);
            this.tabPage_Info.Name = "tabPage_Info";
            this.tabPage_Info.Size = new System.Drawing.Size(426, 263);
            this.tabPage_Info.TabIndex = 3;
            this.tabPage_Info.Text = "정보";
            // 
            // label_About
            // 
            this.label_About.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_About.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.label_About.Location = new System.Drawing.Point(0, 0);
            this.label_About.Name = "label_About";
            this.label_About.Size = new System.Drawing.Size(426, 233);
            this.label_About.TabIndex = 0;
            this.label_About.Text = "[제작 및 문의]\r\n유채색\r\n라그린네\r\n히비야\r\n윈도ce\r\n[도움] 고먐미\r\n[저작권]\r\n기재되어있는 회사명 · 제품명 · 시스템 이름은\r\n해당 " +
    "소유자의 상표 또는 등록 상표입니다.\r\n(C) 2010 - 2019 SQUARE ENIX CO., LTD All Rights Reserved.\r" +
    "\nKorea Published by ACTOZ SOFT.";
            this.label_About.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel_GitHub
            // 
            this.linkLabel_GitHub.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkLabel_GitHub.Location = new System.Drawing.Point(0, 233);
            this.linkLabel_GitHub.Name = "linkLabel_GitHub";
            this.linkLabel_GitHub.Size = new System.Drawing.Size(426, 30);
            this.linkLabel_GitHub.TabIndex = 0;
            this.linkLabel_GitHub.TabStop = true;
            this.linkLabel_GitHub.Text = "GitHub";
            this.linkLabel_GitHub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_GitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GitHub_LinkClicked);
            // 
            // label_AboutTitle
            // 
            this.label_AboutTitle.BackColor = System.Drawing.Color.Silver;
            this.label_AboutTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_AboutTitle.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.label_AboutTitle.ForeColor = System.Drawing.Color.Gray;
            this.label_AboutTitle.Location = new System.Drawing.Point(0, 298);
            this.label_AboutTitle.Name = "label_AboutTitle";
            this.label_AboutTitle.Size = new System.Drawing.Size(544, 24);
            this.label_AboutTitle.TabIndex = 0;
            this.label_AboutTitle.Text = "VERSION STRING";
            this.label_AboutTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(544, 322);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.label_AboutTitle);
            this.Controls.Add(this.panel_TopSetting);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(560, 360);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "임무/돌발 찾기 도우미";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel_TopSetting.ResumeLayout(false);
            this.panel_TopSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage_Settings.ResumeLayout(false);
            this.groupBox_DefaultSet.ResumeLayout(false);
            this.groupBox_DefaultSet.PerformLayout();
            this.tabPage_3rdParty.ResumeLayout(false);
            this.groupBox_DiscordSet.ResumeLayout(false);
            this.groupBox_DiscordSet.PerformLayout();
            this.groupBox_TelegramSet.ResumeLayout(false);
            this.groupBox_TelegramSet.PerformLayout();
            this.tabPage_Advanced.ResumeLayout(false);
            this.groupBox_debug.ResumeLayout(false);
            this.groupBox_debug.PerformLayout();
            this.groupBox_CustomHttpRequest.ResumeLayout(false);
            this.groupBox_CustomHttpRequest.PerformLayout();
            this.tabPage_FATE.ResumeLayout(false);
            this.tabPage_FATE.PerformLayout();
            this.menuStrip_FATETab.ResumeLayout(false);
            this.menuStrip_FATETab.PerformLayout();
            this.tabPage_Log.ResumeLayout(false);
            this.tabPage_Log.PerformLayout();
            this.panel_LogCover.ResumeLayout(false);
            this.menuStrip_LogTab.ResumeLayout(false);
            this.menuStrip_LogTab.PerformLayout();
            this.tabPage_Info.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.LinkLabel linkLabel_GitHub;
        private System.Windows.Forms.CheckBox checkBox_Overlay;
        private LocalizableLabel label_AboutTitle;
        private System.Windows.Forms.Button button_ResetOverlayPosition;
        private System.Windows.Forms.Button button_SelectProcess;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Open;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Close;
        private System.Windows.Forms.CheckBox checkBox_StartupShow;
        private System.Windows.Forms.ComboBox comboBox_Process;
        private System.Windows.Forms.Button button_ResetProcess;
        internal RikTheVeggie.TriStateTreeView triStateTreeView_FATEs;
        private System.Windows.Forms.Panel panel_TopSetting;
        private LocalizableLabel label_Process;
        private TabControlBlack tabControl;
        private System.Windows.Forms.TabPage tabPage_FATE;
        private System.Windows.Forms.TabPage tabPage_Settings;
        private System.Windows.Forms.MenuStrip menuStrip_FATETab;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SelectAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_UnSelectAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SelectApply;
        private System.Windows.Forms.TabPage tabPage_Log;
        private System.Windows.Forms.Panel panel_LogCover;
        private System.Windows.Forms.MenuStrip menuStrip_LogTab;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_LogCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_LogClear;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabPage_Info;
        private LocalizableLabel label_About;
        private System.Windows.Forms.GroupBox groupBox_DefaultSet;
        internal System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox checkBox_FlashWindow;
        private System.Windows.Forms.CheckBox checkBox_CheatRoullete;
        private System.Windows.Forms.ToolStripMenuItem presetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookOfSkyfireIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookOfSkyfireIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookOfNetherfireIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bookOfSkyfallIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookOfSkyfallIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookOfNetherfireIToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem bookOfSkywindIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookOfSkywindIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem bookOfSkyearthIToolStripMenuItem;
        private LocalizableLabel label_FATEAbout;
        private System.Windows.Forms.ComboBox comboBox_Language;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem IxionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TamamoToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox_FateSound;
        private System.Windows.Forms.CheckBox checkBox_CustomSound;
        private System.Windows.Forms.Button button_getSoundFile;
        private System.Windows.Forms.Label label_CustomSoundFileName;
        private System.Windows.Forms.CheckBox checkBox_useVPN;
        private System.Windows.Forms.TabPage tabPage_Advanced;
        private System.Windows.Forms.GroupBox groupBox_CustomHttpRequest;
        private System.Windows.Forms.CheckBox checkBox_EnableHttpRequest;
        private System.Windows.Forms.CheckBox checkBox_RequestOnDutyMatched;
        private System.Windows.Forms.CheckBox checkBox_RequestOnFateOccur;
        private System.Windows.Forms.TextBox textBox_CustomHttpUrl;
        private System.Windows.Forms.Label label_HttpRequestUrl;
        private System.Windows.Forms.Label label_HttpRequestReadme;
        private System.Windows.Forms.TabPage tabPage_3rdParty;
        private System.Windows.Forms.GroupBox groupBox_DiscordSet;
        private System.Windows.Forms.TextBox textBox_Discord;
        private LocalizableLabel label_DiscordAt;
        private LocalizableLabel label_DiscordAbout;
        private System.Windows.Forms.CheckBox checkBox_Discord;
        private System.Windows.Forms.LinkLabel linkLabel_DiscordServer;
        private System.Windows.Forms.GroupBox groupBox_TelegramSet;
        private System.Windows.Forms.TextBox textBox_Telegram;
        private LocalizableLabel label_TelegramAbout;
        private System.Windows.Forms.CheckBox checkBox_Telegram;
        private LocalizableLabel label_Telegram_ChatId;
        private System.Windows.Forms.LinkLabel linkLabel_Telegram;
        private System.Windows.Forms.CheckBox checkBox_autoHideOverlay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem frogSuitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frogMountToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox_CopyMacro;
        private System.Windows.Forms.CheckBox checkBox_Telegram_Queue_Status;
        private System.Windows.Forms.LinkLabel linkLabel_Localization;
        private System.Windows.Forms.GroupBox groupBox_debug;
        private System.Windows.Forms.CheckBox checkBox_DebugLog;
    }
}

