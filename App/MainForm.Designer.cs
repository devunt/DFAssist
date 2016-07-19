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
            this.label_AboutTitle = new System.Windows.Forms.Label();
            this.button_ResetProcess = new System.Windows.Forms.Button();
            this.comboBox_Process = new System.Windows.Forms.ComboBox();
            this.button_SelectProcess = new System.Windows.Forms.Button();
            this.button_ResetOverlayPosition = new System.Windows.Forms.Button();
            this.checkBox_Overlay = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlBlack1 = new App.TabControlBlack();
            this.DefaultTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_Twitter = new System.Windows.Forms.TextBox();
            this.label_TwitterAt = new System.Windows.Forms.Label();
            this.label_TwitterAbout = new System.Windows.Forms.Label();
            this.checkBox_Twitter = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_StartupShow = new System.Windows.Forms.CheckBox();
            this.checkBox_CheckUpdate = new System.Windows.Forms.CheckBox();
            this.checkBox_AutoUpdate = new System.Windows.Forms.CheckBox();
            this.linkLabel_NewUpdate = new System.Windows.Forms.LinkLabel();
            this.FATETab = new System.Windows.Forms.TabPage();
            this.triStateTreeView_FATEs = new RikTheVeggie.TriStateTreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.allSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allDeselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectApplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogTab = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.logCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InformationTab = new System.Windows.Forms.TabPage();
            this.label_About = new System.Windows.Forms.Label();
            this.linkLabel_GitHub = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControlBlack1.SuspendLayout();
            this.DefaultTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FATETab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.LogTab.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.InformationTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_AboutTitle
            // 
            this.label_AboutTitle.BackColor = System.Drawing.Color.Silver;
            this.label_AboutTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_AboutTitle.Font = new System.Drawing.Font("맑은 고딕", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label_AboutTitle.ForeColor = System.Drawing.Color.Gray;
            this.label_AboutTitle.Location = new System.Drawing.Point(0, 297);
            this.label_AboutTitle.Name = "label_AboutTitle";
            this.label_AboutTitle.Size = new System.Drawing.Size(544, 24);
            this.label_AboutTitle.TabIndex = 0;
            this.label_AboutTitle.Text = "VERSION STRING";
            this.label_AboutTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_ResetProcess
            // 
            this.button_ResetProcess.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.button_ResetProcess.Location = new System.Drawing.Point(295, 0);
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
            this.comboBox_Process.FormattingEnabled = true;
            this.comboBox_Process.Location = new System.Drawing.Point(114, 1);
            this.comboBox_Process.Name = "comboBox_Process";
            this.comboBox_Process.Size = new System.Drawing.Size(120, 25);
            this.comboBox_Process.Sorted = true;
            this.comboBox_Process.TabIndex = 0;
            // 
            // button_SelectProcess
            // 
            this.button_SelectProcess.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.button_SelectProcess.Location = new System.Drawing.Point(235, 0);
            this.button_SelectProcess.Name = "button_SelectProcess";
            this.button_SelectProcess.Size = new System.Drawing.Size(60, 27);
            this.button_SelectProcess.TabIndex = 0;
            this.button_SelectProcess.Text = "수동설정";
            this.button_SelectProcess.UseVisualStyleBackColor = true;
            this.button_SelectProcess.Click += new System.EventHandler(this.button_SelectProcess_Click);
            // 
            // button_ResetOverlayPosition
            // 
            this.button_ResetOverlayPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ResetOverlayPosition.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.button_ResetOverlayPosition.Location = new System.Drawing.Point(470, 0);
            this.button_ResetOverlayPosition.Name = "button_ResetOverlayPosition";
            this.button_ResetOverlayPosition.Size = new System.Drawing.Size(74, 27);
            this.button_ResetOverlayPosition.TabIndex = 0;
            this.button_ResetOverlayPosition.Text = "위치 초기화";
            this.button_ResetOverlayPosition.UseVisualStyleBackColor = true;
            this.button_ResetOverlayPosition.Click += new System.EventHandler(this.button_ResetOverlayPosition_Click);
            // 
            // checkBox_Overlay
            // 
            this.checkBox_Overlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Overlay.AutoSize = true;
            this.checkBox_Overlay.Checked = true;
            this.checkBox_Overlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Overlay.Font = new System.Drawing.Font("맑은 고딕", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.checkBox_Overlay.ForeColor = System.Drawing.Color.Gray;
            this.checkBox_Overlay.Location = new System.Drawing.Point(356, 4);
            this.checkBox_Overlay.Name = "checkBox_Overlay";
            this.checkBox_Overlay.Size = new System.Drawing.Size(119, 19);
            this.checkBox_Overlay.TabIndex = 0;
            this.checkBox_Overlay.Text = "오버레이 UI 사용";
            this.toolTip1.SetToolTip(this.checkBox_Overlay, "오버레이 UI의 좌측 막대를 이용해 드래그 할 수 있습니다.");
            this.checkBox_Overlay.UseVisualStyleBackColor = true;
            this.checkBox_Overlay.CheckedChanged += new System.EventHandler(this.checkBox_Overlay_CheckedChanged);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.button_ResetOverlayPosition);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox_Process);
            this.panel1.Controls.Add(this.button_SelectProcess);
            this.panel1.Controls.Add(this.button_ResetProcess);
            this.panel1.Controls.Add(this.checkBox_Overlay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(544, 27);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "FFXIV 프로세스";
            // 
            // tabControlBlack1
            // 
            this.tabControlBlack1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlBlack1.Controls.Add(this.DefaultTab);
            this.tabControlBlack1.Controls.Add(this.FATETab);
            this.tabControlBlack1.Controls.Add(this.LogTab);
            this.tabControlBlack1.Controls.Add(this.InformationTab);
            this.tabControlBlack1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlBlack1.ItemSize = new System.Drawing.Size(30, 110);
            this.tabControlBlack1.Location = new System.Drawing.Point(0, 27);
            this.tabControlBlack1.Multiline = true;
            this.tabControlBlack1.Name = "tabControlBlack1";
            this.tabControlBlack1.SelectedIndex = 0;
            this.tabControlBlack1.Size = new System.Drawing.Size(544, 270);
            this.tabControlBlack1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlBlack1.TabIndex = 2;
            // 
            // DefaultTab
            // 
            this.DefaultTab.BackColor = System.Drawing.SystemColors.Control;
            this.DefaultTab.Controls.Add(this.groupBox2);
            this.DefaultTab.Controls.Add(this.groupBox1);
            this.DefaultTab.Controls.Add(this.linkLabel_NewUpdate);
            this.DefaultTab.Location = new System.Drawing.Point(114, 4);
            this.DefaultTab.Name = "DefaultTab";
            this.DefaultTab.Size = new System.Drawing.Size(426, 262);
            this.DefaultTab.TabIndex = 1;
            this.DefaultTab.Text = "일반";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_Twitter);
            this.groupBox2.Controls.Add(this.label_TwitterAt);
            this.groupBox2.Controls.Add(this.label_TwitterAbout);
            this.groupBox2.Controls.Add(this.checkBox_Twitter);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 117);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "트위터 알림";
            // 
            // textBox_Twitter
            // 
            this.textBox_Twitter.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Twitter.Location = new System.Drawing.Point(37, 25);
            this.textBox_Twitter.MaxLength = 16;
            this.textBox_Twitter.Name = "textBox_Twitter";
            this.textBox_Twitter.Size = new System.Drawing.Size(156, 25);
            this.textBox_Twitter.TabIndex = 0;
            this.textBox_Twitter.TextChanged += new System.EventHandler(this.textBox_Twitter_TextChanged);
            // 
            // label_TwitterAt
            // 
            this.label_TwitterAt.AutoSize = true;
            this.label_TwitterAt.Location = new System.Drawing.Point(12, 28);
            this.label_TwitterAt.Name = "label_TwitterAt";
            this.label_TwitterAt.Size = new System.Drawing.Size(21, 17);
            this.label_TwitterAt.TabIndex = 0;
            this.label_TwitterAt.Text = "@";
            // 
            // label_TwitterAbout
            // 
            this.label_TwitterAbout.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.label_TwitterAbout.Location = new System.Drawing.Point(13, 61);
            this.label_TwitterAbout.Name = "label_TwitterAbout";
            this.label_TwitterAbout.Size = new System.Drawing.Size(379, 41);
            this.label_TwitterAbout.TabIndex = 0;
            this.label_TwitterAbout.Text = "매칭이 됐을 시 입력된 트위터 계정으로 멘션을 보내 해당 사실을 알립니다.\r\n원하는 돌발이 발생했을 시에도 멘션을 보내 해당 사실을 알립니다.\r\n" +
    "계정명 입력시 앞의 @ 표시는 제외하고 순수 계정명만 입력해주세요.";
            // 
            // checkBox_Twitter
            // 
            this.checkBox_Twitter.AutoSize = true;
            this.checkBox_Twitter.Location = new System.Drawing.Point(199, 28);
            this.checkBox_Twitter.Name = "checkBox_Twitter";
            this.checkBox_Twitter.Size = new System.Drawing.Size(66, 21);
            this.checkBox_Twitter.TabIndex = 0;
            this.checkBox_Twitter.Text = "활성화";
            this.checkBox_Twitter.UseVisualStyleBackColor = true;
            this.checkBox_Twitter.CheckedChanged += new System.EventHandler(this.checkBox_Twitter_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_StartupShow);
            this.groupBox1.Controls.Add(this.checkBox_CheckUpdate);
            this.groupBox1.Controls.Add(this.checkBox_AutoUpdate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 110);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "기본설정";
            // 
            // checkBox_StartupShow
            // 
            this.checkBox_StartupShow.AutoSize = true;
            this.checkBox_StartupShow.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_StartupShow.Location = new System.Drawing.Point(14, 29);
            this.checkBox_StartupShow.Name = "checkBox_StartupShow";
            this.checkBox_StartupShow.Size = new System.Drawing.Size(186, 19);
            this.checkBox_StartupShow.TabIndex = 0;
            this.checkBox_StartupShow.Text = "프로그램 시작시 이 창 보이기";
            this.checkBox_StartupShow.UseVisualStyleBackColor = true;
            this.checkBox_StartupShow.CheckedChanged += new System.EventHandler(this.checkBox_StartupShow_CheckedChanged);
            // 
            // checkBox_CheckUpdate
            // 
            this.checkBox_CheckUpdate.AutoSize = true;
            this.checkBox_CheckUpdate.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_CheckUpdate.Location = new System.Drawing.Point(14, 52);
            this.checkBox_CheckUpdate.Name = "checkBox_CheckUpdate";
            this.checkBox_CheckUpdate.Size = new System.Drawing.Size(278, 19);
            this.checkBox_CheckUpdate.TabIndex = 0;
            this.checkBox_CheckUpdate.Text = "주기적으로 업데이트 확인하기 (재시작시 반영)";
            this.checkBox_CheckUpdate.UseVisualStyleBackColor = true;
            this.checkBox_CheckUpdate.CheckedChanged += new System.EventHandler(this.checkBox_StartupUpdate_CheckedChanged);
            // 
            // checkBox_AutoUpdate
            // 
            this.checkBox_AutoUpdate.AutoSize = true;
            this.checkBox_AutoUpdate.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.checkBox_AutoUpdate.Location = new System.Drawing.Point(14, 75);
            this.checkBox_AutoUpdate.Name = "checkBox_AutoUpdate";
            this.checkBox_AutoUpdate.Size = new System.Drawing.Size(266, 19);
            this.checkBox_AutoUpdate.TabIndex = 0;
            this.checkBox_AutoUpdate.Text = "업데이트가 존재하면 자동으로 업데이트하기";
            this.checkBox_AutoUpdate.UseVisualStyleBackColor = true;
            this.checkBox_AutoUpdate.CheckedChanged += new System.EventHandler(this.checkBox_StartupAutoUpdate_CheckedChanged);
            // 
            // linkLabel_NewUpdate
            // 
            this.linkLabel_NewUpdate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkLabel_NewUpdate.Location = new System.Drawing.Point(0, 245);
            this.linkLabel_NewUpdate.Name = "linkLabel_NewUpdate";
            this.linkLabel_NewUpdate.Size = new System.Drawing.Size(426, 17);
            this.linkLabel_NewUpdate.TabIndex = 0;
            this.linkLabel_NewUpdate.TabStop = true;
            this.linkLabel_NewUpdate.Text = "새로운 업데이트가 있습니다!";
            this.linkLabel_NewUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel_NewUpdate.Visible = false;
            this.linkLabel_NewUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_NewUpdate_LinkClicked);
            // 
            // FATETab
            // 
            this.FATETab.BackColor = System.Drawing.SystemColors.Control;
            this.FATETab.Controls.Add(this.triStateTreeView_FATEs);
            this.FATETab.Controls.Add(this.menuStrip1);
            this.FATETab.Location = new System.Drawing.Point(114, 4);
            this.FATETab.Name = "FATETab";
            this.FATETab.Size = new System.Drawing.Size(446, 262);
            this.FATETab.TabIndex = 0;
            this.FATETab.Text = "돌발";
            // 
            // triStateTreeView_FATEs
            // 
            this.triStateTreeView_FATEs.BackColor = System.Drawing.SystemColors.Control;
            this.triStateTreeView_FATEs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triStateTreeView_FATEs.FullRowSelect = true;
            this.triStateTreeView_FATEs.HotTracking = true;
            this.triStateTreeView_FATEs.Location = new System.Drawing.Point(0, 24);
            this.triStateTreeView_FATEs.Name = "triStateTreeView_FATEs";
            this.triStateTreeView_FATEs.Size = new System.Drawing.Size(446, 238);
            this.triStateTreeView_FATEs.TabIndex = 0;
            this.triStateTreeView_FATEs.TriStateStyleProperty = RikTheVeggie.TriStateTreeView.TriStateStyles.Installer;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allSelectToolStripMenuItem,
            this.allDeselectToolStripMenuItem,
            this.selectApplyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(446, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // allSelectToolStripMenuItem
            // 
            this.allSelectToolStripMenuItem.Name = "allSelectToolStripMenuItem";
            this.allSelectToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.allSelectToolStripMenuItem.Text = "모두 선택";
            this.allSelectToolStripMenuItem.Click += new System.EventHandler(this.allSelectToolStripMenuItem_Click);
            // 
            // allDeselectToolStripMenuItem
            // 
            this.allDeselectToolStripMenuItem.Name = "allDeselectToolStripMenuItem";
            this.allDeselectToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.allDeselectToolStripMenuItem.Text = "모두 해제";
            this.allDeselectToolStripMenuItem.Click += new System.EventHandler(this.allDeselectToolStripMenuItem_Click);
            // 
            // selectApplyToolStripMenuItem
            // 
            this.selectApplyToolStripMenuItem.Name = "selectApplyToolStripMenuItem";
            this.selectApplyToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.selectApplyToolStripMenuItem.Text = "선택 적용";
            // 
            // LogTab
            // 
            this.LogTab.BackColor = System.Drawing.SystemColors.Control;
            this.LogTab.Controls.Add(this.panel2);
            this.LogTab.Controls.Add(this.menuStrip2);
            this.LogTab.Location = new System.Drawing.Point(114, 4);
            this.LogTab.Name = "LogTab";
            this.LogTab.Size = new System.Drawing.Size(446, 262);
            this.LogTab.TabIndex = 2;
            this.LogTab.Text = "로그";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.richTextBox_Log);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(446, 238);
            this.panel2.TabIndex = 1;
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
            this.richTextBox_Log.Size = new System.Drawing.Size(444, 236);
            this.richTextBox_Log.TabIndex = 0;
            this.richTextBox_Log.Text = "";
            this.richTextBox_Log.TextChanged += new System.EventHandler(this.richTextBox_Log_TextChanged);
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logCopyToolStripMenuItem,
            this.logClearToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(446, 24);
            this.menuStrip2.TabIndex = 0;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // logCopyToolStripMenuItem
            // 
            this.logCopyToolStripMenuItem.Name = "logCopyToolStripMenuItem";
            this.logCopyToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.logCopyToolStripMenuItem.Text = "로그 복사";
            this.logCopyToolStripMenuItem.Click += new System.EventHandler(this.logCopyToolStripMenuItem_Click);
            // 
            // logClearToolStripMenuItem
            // 
            this.logClearToolStripMenuItem.Name = "logClearToolStripMenuItem";
            this.logClearToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.logClearToolStripMenuItem.Text = "로그 삭제";
            this.logClearToolStripMenuItem.Click += new System.EventHandler(this.logClearToolStripMenuItem_Click);
            // 
            // InformationTab
            // 
            this.InformationTab.BackColor = System.Drawing.SystemColors.Control;
            this.InformationTab.Controls.Add(this.label_About);
            this.InformationTab.Controls.Add(this.linkLabel_GitHub);
            this.InformationTab.Location = new System.Drawing.Point(114, 4);
            this.InformationTab.Name = "InformationTab";
            this.InformationTab.Size = new System.Drawing.Size(446, 262);
            this.InformationTab.TabIndex = 3;
            this.InformationTab.Text = "정보";
            // 
            // label_About
            // 
            this.label_About.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_About.Font = new System.Drawing.Font("맑은 고딕", 8F);
            this.label_About.Location = new System.Drawing.Point(0, 0);
            this.label_About.Name = "label_About";
            this.label_About.Size = new System.Drawing.Size(446, 210);
            this.label_About.TabIndex = 1;
            this.label_About.Text = "유채색 @ 오딘 <<리녹>>\r\ndevunt@gmail.com\r\n\r\n\r\n기재되어있는 회사명 · 제품명 · 시스템 이름은\r\n해당 소유자의 상표 또는 " +
    "등록 상표입니다.\r\n(C) 2010 - 2016 SQUARE ENIX CO., LTD All Rights Reserved.\r\nKorea Publ" +
    "ished by EYEDENTITY MOBILE.";
            this.label_About.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel_GitHub
            // 
            this.linkLabel_GitHub.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkLabel_GitHub.Location = new System.Drawing.Point(0, 210);
            this.linkLabel_GitHub.Name = "linkLabel_GitHub";
            this.linkLabel_GitHub.Size = new System.Drawing.Size(446, 52);
            this.linkLabel_GitHub.TabIndex = 0;
            this.linkLabel_GitHub.TabStop = true;
            this.linkLabel_GitHub.Text = "GitHub";
            this.linkLabel_GitHub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_GitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GitHub_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(544, 321);
            this.Controls.Add(this.tabControlBlack1);
            this.Controls.Add(this.label_AboutTitle);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(560, 360);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "임무 찾기 도우미";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlBlack1.ResumeLayout(false);
            this.DefaultTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FATETab.ResumeLayout(false);
            this.FATETab.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.LogTab.ResumeLayout(false);
            this.LogTab.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.InformationTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.LinkLabel linkLabel_GitHub;
        private System.Windows.Forms.CheckBox checkBox_Overlay;
        private System.Windows.Forms.Label label_AboutTitle;
        private System.Windows.Forms.Button button_ResetOverlayPosition;
        private System.Windows.Forms.Button button_SelectProcess;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Open;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Close;
        private System.Windows.Forms.CheckBox checkBox_CheckUpdate;
        private System.Windows.Forms.CheckBox checkBox_StartupShow;
        private System.Windows.Forms.ComboBox comboBox_Process;
        private System.Windows.Forms.Button button_ResetProcess;
        internal System.Windows.Forms.LinkLabel linkLabel_NewUpdate;
        private System.Windows.Forms.CheckBox checkBox_AutoUpdate;
        private System.Windows.Forms.TextBox textBox_Twitter;
        private System.Windows.Forms.Label label_TwitterAt;
        private System.Windows.Forms.Label label_TwitterAbout;
        private System.Windows.Forms.CheckBox checkBox_Twitter;
        internal RikTheVeggie.TriStateTreeView triStateTreeView_FATEs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private TabControlBlack tabControlBlack1;
        private System.Windows.Forms.TabPage FATETab;
        private System.Windows.Forms.TabPage DefaultTab;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem allSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allDeselectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectApplyToolStripMenuItem;
        private System.Windows.Forms.TabPage LogTab;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem logCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logClearToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage InformationTab;
        private System.Windows.Forms.Label label_About;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

