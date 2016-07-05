namespace App
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_Setting = new System.Windows.Forms.TabPage();
            this.checkBox_AutoUpdate = new System.Windows.Forms.CheckBox();
            this.label_OverlayDescription = new System.Windows.Forms.Label();
            this.label_Separator2 = new System.Windows.Forms.Label();
            this.button_ResetProcess = new System.Windows.Forms.Button();
            this.comboBox_Process = new System.Windows.Forms.ComboBox();
            this.checkBox_CheckUpdate = new System.Windows.Forms.CheckBox();
            this.checkBox_StartupShow = new System.Windows.Forms.CheckBox();
            this.linkLabel_NewUpdate = new System.Windows.Forms.LinkLabel();
            this.label_Separator = new System.Windows.Forms.Label();
            this.button_SelectProcess = new System.Windows.Forms.Button();
            this.label_Process = new System.Windows.Forms.Label();
            this.button_ResetOverlayPosition = new System.Windows.Forms.Button();
            this.checkBox_Overlay = new System.Windows.Forms.CheckBox();
            this.tabPage_FATE = new System.Windows.Forms.TabPage();
            this.button_Save = new System.Windows.Forms.Button();
            this.triStateTreeView_FATEs = new RikTheVeggie.TriStateTreeView();
            this.tabPage_Notification = new System.Windows.Forms.TabPage();
            this.checkBox_Twitter = new System.Windows.Forms.CheckBox();
            this.label_TwitterAbout = new System.Windows.Forms.Label();
            this.textBox_Twitter = new System.Windows.Forms.TextBox();
            this.label_TwitterAt = new System.Windows.Forms.Label();
            this.label_Twitter = new System.Windows.Forms.Label();
            this.tabPage_Log = new System.Windows.Forms.TabPage();
            this.button_CopyLog = new System.Windows.Forms.Button();
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.tabPage_About = new System.Windows.Forms.TabPage();
            this.label_AboutTitle = new System.Windows.Forms.Label();
            this.linkLabel_GitHub = new System.Windows.Forms.LinkLabel();
            this.label_About = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.button_UncheckAll = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage_Setting.SuspendLayout();
            this.tabPage_FATE.SuspendLayout();
            this.tabPage_Notification.SuspendLayout();
            this.tabPage_Log.SuspendLayout();
            this.tabPage_About.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_Setting);
            this.tabControl.Controls.Add(this.tabPage_FATE);
            this.tabControl.Controls.Add(this.tabPage_Notification);
            this.tabControl.Controls.Add(this.tabPage_Log);
            this.tabControl.Controls.Add(this.tabPage_About);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(484, 311);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // tabPage_Setting
            // 
            this.tabPage_Setting.Controls.Add(this.checkBox_AutoUpdate);
            this.tabPage_Setting.Controls.Add(this.label_OverlayDescription);
            this.tabPage_Setting.Controls.Add(this.label_Separator2);
            this.tabPage_Setting.Controls.Add(this.button_ResetProcess);
            this.tabPage_Setting.Controls.Add(this.comboBox_Process);
            this.tabPage_Setting.Controls.Add(this.checkBox_CheckUpdate);
            this.tabPage_Setting.Controls.Add(this.checkBox_StartupShow);
            this.tabPage_Setting.Controls.Add(this.linkLabel_NewUpdate);
            this.tabPage_Setting.Controls.Add(this.label_Separator);
            this.tabPage_Setting.Controls.Add(this.button_SelectProcess);
            this.tabPage_Setting.Controls.Add(this.label_Process);
            this.tabPage_Setting.Controls.Add(this.button_ResetOverlayPosition);
            this.tabPage_Setting.Controls.Add(this.checkBox_Overlay);
            this.tabPage_Setting.Location = new System.Drawing.Point(4, 23);
            this.tabPage_Setting.Name = "tabPage_Setting";
            this.tabPage_Setting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Setting.Size = new System.Drawing.Size(476, 284);
            this.tabPage_Setting.TabIndex = 0;
            this.tabPage_Setting.Text = "설정";
            this.tabPage_Setting.UseVisualStyleBackColor = true;
            // 
            // checkBox_AutoUpdate
            // 
            this.checkBox_AutoUpdate.AutoSize = true;
            this.checkBox_AutoUpdate.Location = new System.Drawing.Point(21, 214);
            this.checkBox_AutoUpdate.Name = "checkBox_AutoUpdate";
            this.checkBox_AutoUpdate.Size = new System.Drawing.Size(285, 17);
            this.checkBox_AutoUpdate.TabIndex = 0;
            this.checkBox_AutoUpdate.Text = "업데이트가 존재하면 자동으로 업데이트하기";
            this.checkBox_AutoUpdate.UseVisualStyleBackColor = true;
            this.checkBox_AutoUpdate.CheckedChanged += new System.EventHandler(this.checkBox_StartupAutoUpdate_CheckedChanged);
            // 
            // label_OverlayDescription
            // 
            this.label_OverlayDescription.AutoSize = true;
            this.label_OverlayDescription.Location = new System.Drawing.Point(18, 118);
            this.label_OverlayDescription.Name = "label_OverlayDescription";
            this.label_OverlayDescription.Size = new System.Drawing.Size(385, 13);
            this.label_OverlayDescription.TabIndex = 0;
            this.label_OverlayDescription.Text = "오버레이 창은 해당 창의 좌측 바를 드래그해 이동할 수 있습니다.";
            // 
            // label_Separator2
            // 
            this.label_Separator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Separator2.Location = new System.Drawing.Point(21, 153);
            this.label_Separator2.Name = "label_Separator2";
            this.label_Separator2.Size = new System.Drawing.Size(430, 2);
            this.label_Separator2.TabIndex = 0;
            // 
            // button_ResetProcess
            // 
            this.button_ResetProcess.Location = new System.Drawing.Point(225, 34);
            this.button_ResetProcess.Name = "button_ResetProcess";
            this.button_ResetProcess.Size = new System.Drawing.Size(75, 23);
            this.button_ResetProcess.TabIndex = 0;
            this.button_ResetProcess.Text = "재설정";
            this.button_ResetProcess.UseVisualStyleBackColor = true;
            this.button_ResetProcess.Click += new System.EventHandler(this.button_ResetProcess_Click);
            // 
            // comboBox_Process
            // 
            this.comboBox_Process.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Process.FormattingEnabled = true;
            this.comboBox_Process.Location = new System.Drawing.Point(21, 35);
            this.comboBox_Process.Name = "comboBox_Process";
            this.comboBox_Process.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Process.Sorted = true;
            this.comboBox_Process.TabIndex = 0;
            // 
            // checkBox_CheckUpdate
            // 
            this.checkBox_CheckUpdate.AutoSize = true;
            this.checkBox_CheckUpdate.Location = new System.Drawing.Point(21, 191);
            this.checkBox_CheckUpdate.Name = "checkBox_CheckUpdate";
            this.checkBox_CheckUpdate.Size = new System.Drawing.Size(299, 17);
            this.checkBox_CheckUpdate.TabIndex = 0;
            this.checkBox_CheckUpdate.Text = "주기적으로 업데이트 확인하기 (재시작시 반영)";
            this.checkBox_CheckUpdate.UseVisualStyleBackColor = true;
            this.checkBox_CheckUpdate.CheckedChanged += new System.EventHandler(this.checkBox_StartupUpdate_CheckedChanged);
            // 
            // checkBox_StartupShow
            // 
            this.checkBox_StartupShow.AutoSize = true;
            this.checkBox_StartupShow.Location = new System.Drawing.Point(21, 168);
            this.checkBox_StartupShow.Name = "checkBox_StartupShow";
            this.checkBox_StartupShow.Size = new System.Drawing.Size(198, 17);
            this.checkBox_StartupShow.TabIndex = 0;
            this.checkBox_StartupShow.Text = "프로그램 시작시 이 창 보이기";
            this.checkBox_StartupShow.UseVisualStyleBackColor = true;
            this.checkBox_StartupShow.CheckedChanged += new System.EventHandler(this.checkBox_StartupShow_CheckedChanged);
            // 
            // linkLabel_NewUpdate
            // 
            this.linkLabel_NewUpdate.Location = new System.Drawing.Point(8, 266);
            this.linkLabel_NewUpdate.Name = "linkLabel_NewUpdate";
            this.linkLabel_NewUpdate.Size = new System.Drawing.Size(460, 13);
            this.linkLabel_NewUpdate.TabIndex = 0;
            this.linkLabel_NewUpdate.TabStop = true;
            this.linkLabel_NewUpdate.Text = "새로운 업데이트가 있습니다!";
            this.linkLabel_NewUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel_NewUpdate.Visible = false;
            this.linkLabel_NewUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_NewUpdate_LinkClicked);
            // 
            // label_Separator
            // 
            this.label_Separator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Separator.Location = new System.Drawing.Point(21, 72);
            this.label_Separator.Name = "label_Separator";
            this.label_Separator.Size = new System.Drawing.Size(430, 2);
            this.label_Separator.TabIndex = 0;
            // 
            // button_SelectProcess
            // 
            this.button_SelectProcess.Location = new System.Drawing.Point(148, 34);
            this.button_SelectProcess.Name = "button_SelectProcess";
            this.button_SelectProcess.Size = new System.Drawing.Size(75, 23);
            this.button_SelectProcess.TabIndex = 0;
            this.button_SelectProcess.Text = "수동 설정";
            this.button_SelectProcess.UseVisualStyleBackColor = true;
            this.button_SelectProcess.Click += new System.EventHandler(this.button_SelectProcess_Click);
            // 
            // label_Process
            // 
            this.label_Process.AutoSize = true;
            this.label_Process.Location = new System.Drawing.Point(18, 17);
            this.label_Process.Name = "label_Process";
            this.label_Process.Size = new System.Drawing.Size(157, 13);
            this.label_Process.TabIndex = 0;
            this.label_Process.Text = "파이널판타지14 프로세스:";
            // 
            // button_ResetOverlayPosition
            // 
            this.button_ResetOverlayPosition.Location = new System.Drawing.Point(200, 89);
            this.button_ResetOverlayPosition.Name = "button_ResetOverlayPosition";
            this.button_ResetOverlayPosition.Size = new System.Drawing.Size(100, 23);
            this.button_ResetOverlayPosition.TabIndex = 0;
            this.button_ResetOverlayPosition.Text = "위치 초기화";
            this.button_ResetOverlayPosition.UseVisualStyleBackColor = true;
            this.button_ResetOverlayPosition.Click += new System.EventHandler(this.button_ResetOverlayPosition_Click);
            // 
            // checkBox_Overlay
            // 
            this.checkBox_Overlay.AutoSize = true;
            this.checkBox_Overlay.Checked = true;
            this.checkBox_Overlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Overlay.Location = new System.Drawing.Point(21, 94);
            this.checkBox_Overlay.Name = "checkBox_Overlay";
            this.checkBox_Overlay.Size = new System.Drawing.Size(179, 17);
            this.checkBox_Overlay.TabIndex = 0;
            this.checkBox_Overlay.Text = "반투명 오버레이 UI 띄우기";
            this.checkBox_Overlay.UseVisualStyleBackColor = true;
            this.checkBox_Overlay.CheckedChanged += new System.EventHandler(this.checkBox_Overlay_CheckedChanged);
            // 
            // tabPage_FATE
            // 
            this.tabPage_FATE.Controls.Add(this.button_UncheckAll);
            this.tabPage_FATE.Controls.Add(this.button_Save);
            this.tabPage_FATE.Controls.Add(this.triStateTreeView_FATEs);
            this.tabPage_FATE.Location = new System.Drawing.Point(4, 23);
            this.tabPage_FATE.Name = "tabPage_FATE";
            this.tabPage_FATE.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_FATE.Size = new System.Drawing.Size(476, 284);
            this.tabPage_FATE.TabIndex = 4;
            this.tabPage_FATE.Text = "돌발";
            this.tabPage_FATE.UseVisualStyleBackColor = true;
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(374, 253);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 2;
            this.button_Save.Text = "선택 적용";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // triStateTreeView_FATEs
            // 
            this.triStateTreeView_FATEs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triStateTreeView_FATEs.FullRowSelect = true;
            this.triStateTreeView_FATEs.HotTracking = true;
            this.triStateTreeView_FATEs.Location = new System.Drawing.Point(3, 3);
            this.triStateTreeView_FATEs.Name = "triStateTreeView_FATEs";
            this.triStateTreeView_FATEs.Size = new System.Drawing.Size(470, 278);
            this.triStateTreeView_FATEs.TabIndex = 0;
            this.triStateTreeView_FATEs.TriStateStyleProperty = RikTheVeggie.TriStateTreeView.TriStateStyles.Installer;
            // 
            // tabPage_Notification
            // 
            this.tabPage_Notification.Controls.Add(this.checkBox_Twitter);
            this.tabPage_Notification.Controls.Add(this.label_TwitterAbout);
            this.tabPage_Notification.Controls.Add(this.textBox_Twitter);
            this.tabPage_Notification.Controls.Add(this.label_TwitterAt);
            this.tabPage_Notification.Controls.Add(this.label_Twitter);
            this.tabPage_Notification.Location = new System.Drawing.Point(4, 23);
            this.tabPage_Notification.Name = "tabPage_Notification";
            this.tabPage_Notification.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Notification.Size = new System.Drawing.Size(476, 284);
            this.tabPage_Notification.TabIndex = 3;
            this.tabPage_Notification.Text = "알림";
            this.tabPage_Notification.UseVisualStyleBackColor = true;
            // 
            // checkBox_Twitter
            // 
            this.checkBox_Twitter.AutoSize = true;
            this.checkBox_Twitter.Location = new System.Drawing.Point(197, 37);
            this.checkBox_Twitter.Name = "checkBox_Twitter";
            this.checkBox_Twitter.Size = new System.Drawing.Size(65, 17);
            this.checkBox_Twitter.TabIndex = 0;
            this.checkBox_Twitter.Text = "활성화";
            this.checkBox_Twitter.UseVisualStyleBackColor = true;
            this.checkBox_Twitter.CheckedChanged += new System.EventHandler(this.checkBox_Twitter_CheckedChanged);
            // 
            // label_TwitterAbout
            // 
            this.label_TwitterAbout.AutoSize = true;
            this.label_TwitterAbout.Location = new System.Drawing.Point(18, 69);
            this.label_TwitterAbout.Name = "label_TwitterAbout";
            this.label_TwitterAbout.Size = new System.Drawing.Size(441, 26);
            this.label_TwitterAbout.TabIndex = 0;
            this.label_TwitterAbout.Text = "매칭이 됐을 시 입력된 트위터 계정으로 멘션을 보내 해당 사실을 알립니다.\r\n계정명 입력시 앞의 @ 표시는 제외하고 순수 계정명만 입력해주세요.";
            // 
            // textBox_Twitter
            // 
            this.textBox_Twitter.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Twitter.Location = new System.Drawing.Point(35, 33);
            this.textBox_Twitter.MaxLength = 16;
            this.textBox_Twitter.Name = "textBox_Twitter";
            this.textBox_Twitter.Size = new System.Drawing.Size(156, 22);
            this.textBox_Twitter.TabIndex = 0;
            this.textBox_Twitter.TextChanged += new System.EventHandler(this.textBox_Twitter_TextChanged);
            // 
            // label_TwitterAt
            // 
            this.label_TwitterAt.AutoSize = true;
            this.label_TwitterAt.Location = new System.Drawing.Point(18, 38);
            this.label_TwitterAt.Name = "label_TwitterAt";
            this.label_TwitterAt.Size = new System.Drawing.Size(17, 13);
            this.label_TwitterAt.TabIndex = 0;
            this.label_TwitterAt.Text = "@";
            // 
            // label_Twitter
            // 
            this.label_Twitter.AutoSize = true;
            this.label_Twitter.Location = new System.Drawing.Point(18, 17);
            this.label_Twitter.Name = "label_Twitter";
            this.label_Twitter.Size = new System.Drawing.Size(80, 13);
            this.label_Twitter.TabIndex = 0;
            this.label_Twitter.Text = "트위터 알림:";
            // 
            // tabPage_Log
            // 
            this.tabPage_Log.Controls.Add(this.button_CopyLog);
            this.tabPage_Log.Controls.Add(this.richTextBox_Log);
            this.tabPage_Log.Location = new System.Drawing.Point(4, 23);
            this.tabPage_Log.Name = "tabPage_Log";
            this.tabPage_Log.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Log.Size = new System.Drawing.Size(476, 284);
            this.tabPage_Log.TabIndex = 2;
            this.tabPage_Log.Text = "로그";
            this.tabPage_Log.UseVisualStyleBackColor = true;
            // 
            // button_CopyLog
            // 
            this.button_CopyLog.Location = new System.Drawing.Point(374, 253);
            this.button_CopyLog.Name = "button_CopyLog";
            this.button_CopyLog.Size = new System.Drawing.Size(75, 23);
            this.button_CopyLog.TabIndex = 0;
            this.button_CopyLog.Text = "로그 복사";
            this.button_CopyLog.UseVisualStyleBackColor = true;
            this.button_CopyLog.Click += new System.EventHandler(this.button_CopyLog_Click);
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox_Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Log.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.richTextBox_Log.Location = new System.Drawing.Point(3, 3);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.ReadOnly = true;
            this.richTextBox_Log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox_Log.Size = new System.Drawing.Size(470, 278);
            this.richTextBox_Log.TabIndex = 0;
            this.richTextBox_Log.Text = "";
            this.richTextBox_Log.TextChanged += new System.EventHandler(this.richTextBox_Log_TextChanged);
            // 
            // tabPage_About
            // 
            this.tabPage_About.Controls.Add(this.label_AboutTitle);
            this.tabPage_About.Controls.Add(this.linkLabel_GitHub);
            this.tabPage_About.Controls.Add(this.label_About);
            this.tabPage_About.Location = new System.Drawing.Point(4, 23);
            this.tabPage_About.Name = "tabPage_About";
            this.tabPage_About.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_About.Size = new System.Drawing.Size(476, 284);
            this.tabPage_About.TabIndex = 1;
            this.tabPage_About.Text = "정보";
            this.tabPage_About.UseVisualStyleBackColor = true;
            // 
            // label_AboutTitle
            // 
            this.label_AboutTitle.Font = new System.Drawing.Font("Dotum", 10.75F, System.Drawing.FontStyle.Bold);
            this.label_AboutTitle.Location = new System.Drawing.Point(6, 28);
            this.label_AboutTitle.Name = "label_AboutTitle";
            this.label_AboutTitle.Size = new System.Drawing.Size(467, 28);
            this.label_AboutTitle.TabIndex = 0;
            this.label_AboutTitle.Text = "VERSION STRING";
            this.label_AboutTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel_GitHub
            // 
            this.linkLabel_GitHub.Location = new System.Drawing.Point(6, 240);
            this.linkLabel_GitHub.Name = "linkLabel_GitHub";
            this.linkLabel_GitHub.Size = new System.Drawing.Size(467, 13);
            this.linkLabel_GitHub.TabIndex = 0;
            this.linkLabel_GitHub.TabStop = true;
            this.linkLabel_GitHub.Text = "GitHub";
            this.linkLabel_GitHub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_GitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GitHub_LinkClicked);
            // 
            // label_About
            // 
            this.label_About.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_About.Location = new System.Drawing.Point(3, 3);
            this.label_About.Name = "label_About";
            this.label_About.Size = new System.Drawing.Size(470, 278);
            this.label_About.TabIndex = 0;
            this.label_About.Text = "\r\n\r\n\r\n\r\n\r\n\r\n유채색 @ 오딘 <<리녹>>\r\ndevunt@gmail.com\r\n\r\n\r\n기재되어있는 회사명 · 제품명 · 시스템 이름은\r\n해당" +
    " 소유자의 상표 또는 등록 상표입니다.\r\n(C) 2010 - 2016 SQUARE ENIX CO., LTD All Rights Reserved." +
    "\r\nKorea Published by EYEDENTITY MOBILE.";
            this.label_About.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "임무 찾기 도우미";
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
            // button_UncheckAll
            // 
            this.button_UncheckAll.Location = new System.Drawing.Point(293, 253);
            this.button_UncheckAll.Name = "button_UncheckAll";
            this.button_UncheckAll.Size = new System.Drawing.Size(75, 23);
            this.button_UncheckAll.TabIndex = 3;
            this.button_UncheckAll.Text = "모두 해제";
            this.button_UncheckAll.UseVisualStyleBackColor = true;
            this.button_UncheckAll.Click += new System.EventHandler(this.button_UncheckAll_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Dotum", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "임무 찾기 도우미";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage_Setting.ResumeLayout(false);
            this.tabPage_Setting.PerformLayout();
            this.tabPage_FATE.ResumeLayout(false);
            this.tabPage_Notification.ResumeLayout(false);
            this.tabPage_Notification.PerformLayout();
            this.tabPage_Log.ResumeLayout(false);
            this.tabPage_About.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_Setting;
        private System.Windows.Forms.TabPage tabPage_About;
        private System.Windows.Forms.TabPage tabPage_Log;
        private System.Windows.Forms.Label label_About;
        internal System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.LinkLabel linkLabel_GitHub;
        private System.Windows.Forms.CheckBox checkBox_Overlay;
        private System.Windows.Forms.Label label_AboutTitle;
        private System.Windows.Forms.Button button_ResetOverlayPosition;
        private System.Windows.Forms.Label label_Process;
        private System.Windows.Forms.Button button_SelectProcess;
        private System.Windows.Forms.Label label_Separator;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Open;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Close;
        private System.Windows.Forms.CheckBox checkBox_CheckUpdate;
        private System.Windows.Forms.CheckBox checkBox_StartupShow;
        private System.Windows.Forms.ComboBox comboBox_Process;
        private System.Windows.Forms.Button button_CopyLog;
        private System.Windows.Forms.Button button_ResetProcess;
        internal System.Windows.Forms.LinkLabel linkLabel_NewUpdate;
        private System.Windows.Forms.Label label_OverlayDescription;
        private System.Windows.Forms.Label label_Separator2;
        private System.Windows.Forms.CheckBox checkBox_AutoUpdate;
        private System.Windows.Forms.TabPage tabPage_Notification;
        private System.Windows.Forms.TextBox textBox_Twitter;
        private System.Windows.Forms.Label label_TwitterAt;
        private System.Windows.Forms.Label label_Twitter;
        private System.Windows.Forms.Label label_TwitterAbout;
        private System.Windows.Forms.CheckBox checkBox_Twitter;
        private System.Windows.Forms.TabPage tabPage_FATE;
        internal RikTheVeggie.TriStateTreeView triStateTreeView_FATEs;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_UncheckAll;
    }
}

