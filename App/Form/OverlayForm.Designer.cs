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
            this.label_DutyCount = new App.LocalizableLabel();
            this.label_DutyName = new App.LocalizableLabel();
            this.label_DutyStatus = new App.LocalizableLabel();
            this.SuspendLayout();
            // 
            // label_DutyCount
            // 
            this.label_DutyCount.Font = new System.Drawing.Font("맑은 고딕", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_DutyCount.Location = new System.Drawing.Point(1, 3);
            this.label_DutyCount.Name = "label_DutyCount";
            this.label_DutyCount.Size = new System.Drawing.Size(258, 15);
            this.label_DutyCount.TabIndex = 1;
            this.label_DutyCount.Text = "총 1개 임무 매칭중";
            this.label_DutyCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_DutyName
            // 
            this.label_DutyName.AutoEllipsis = true;
            this.label_DutyName.Font = new System.Drawing.Font("맑은 고딕", 11.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_DutyName.Location = new System.Drawing.Point(1, 18);
            this.label_DutyName.Name = "label_DutyName";
            this.label_DutyName.Size = new System.Drawing.Size(258, 22);
            this.label_DutyName.TabIndex = 2;
            this.label_DutyName.Text = "대미궁 바하무트: 진성편 4";
            this.label_DutyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_DutyStatus
            // 
            this.label_DutyStatus.AutoEllipsis = true;
            this.label_DutyStatus.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_DutyStatus.Location = new System.Drawing.Point(1, 40);
            this.label_DutyStatus.Name = "label_DutyStatus";
            this.label_DutyStatus.Size = new System.Drawing.Size(258, 15);
            this.label_DutyStatus.TabIndex = 3;
            this.label_DutyStatus.Text = "1/2    1/2    4/4";
            this.label_DutyStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(260, 60);
            this.ControlBox = false;
            this.Controls.Add(this.label_DutyStatus);
            this.Controls.Add(this.label_DutyName);
            this.Controls.Add(this.label_DutyCount);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OverlayForm";
            this.Opacity = 0.6D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DFA OverlayForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OverlayForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private LocalizableLabel label_DutyCount;
        private LocalizableLabel label_DutyName;
        private LocalizableLabel label_DutyStatus;
    }
}