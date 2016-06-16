namespace App
{
    partial class OverlayForm
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
            this.label_DutyCount = new System.Windows.Forms.Label();
            this.label_DutyName = new System.Windows.Forms.Label();
            this.label_DutyStatus = new System.Windows.Forms.Label();
            this.panel_Move = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label_DutyCount
            // 
            this.label_DutyCount.Font = new System.Drawing.Font("Dotum", 8.25F);
            this.label_DutyCount.Location = new System.Drawing.Point(11, 3);
            this.label_DutyCount.Name = "label_DutyCount";
            this.label_DutyCount.Size = new System.Drawing.Size(245, 14);
            this.label_DutyCount.TabIndex = 1;
            this.label_DutyCount.Text = "총 1개 임무 매칭중";
            this.label_DutyCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_DutyName
            // 
            this.label_DutyName.AutoEllipsis = true;
            this.label_DutyName.Font = new System.Drawing.Font("Dotum", 10.25F);
            this.label_DutyName.Location = new System.Drawing.Point(11, 22);
            this.label_DutyName.Name = "label_DutyName";
            this.label_DutyName.Size = new System.Drawing.Size(245, 14);
            this.label_DutyName.TabIndex = 2;
            this.label_DutyName.Text = "<대미궁 바하무트: 진성편 4>";
            this.label_DutyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_DutyStatus
            // 
            this.label_DutyStatus.AutoEllipsis = true;
            this.label_DutyStatus.Font = new System.Drawing.Font("Dotum", 10.25F);
            this.label_DutyStatus.Location = new System.Drawing.Point(11, 40);
            this.label_DutyStatus.Name = "label_DutyStatus";
            this.label_DutyStatus.Size = new System.Drawing.Size(245, 14);
            this.label_DutyStatus.TabIndex = 3;
            this.label_DutyStatus.Text = "1/2    1/2     4/4";
            this.label_DutyStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Move
            // 
            this.panel_Move.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel_Move.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panel_Move.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Move.Location = new System.Drawing.Point(0, 0);
            this.panel_Move.Name = "panel_Move";
            this.panel_Move.Size = new System.Drawing.Size(10, 60);
            this.panel_Move.TabIndex = 4;
            this.panel_Move.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Move_MouseDown);
            // 
            // OverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(270, 60);
            this.ControlBox = false;
            this.Controls.Add(this.panel_Move);
            this.Controls.Add(this.label_DutyStatus);
            this.Controls.Add(this.label_DutyName);
            this.Controls.Add(this.label_DutyCount);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Dotum", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OverlayForm";
            this.Opacity = 0.6D;
            this.ShowInTaskbar = false;
            this.Text = "DFA OverlayForm";
            this.TopMost = true;
            this.LocationChanged += new System.EventHandler(this.OverlayForm_LocationChanged);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_DutyCount;
        private System.Windows.Forms.Label label_DutyName;
        private System.Windows.Forms.Label label_DutyStatus;
        private System.Windows.Forms.Panel panel_Move;
    }
}