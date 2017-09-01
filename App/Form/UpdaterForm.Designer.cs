namespace App
{
    partial class UpdaterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdaterForm));
            this.label_Updating = new LocalizableLabel();
            this.SuspendLayout();
            // 
            // label_Updating
            // 
            this.label_Updating.BackColor = System.Drawing.Color.Transparent;
            this.label_Updating.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Updating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Updating.Font = new System.Drawing.Font("Dotum", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Updating.ForeColor = System.Drawing.Color.Black;
            this.label_Updating.Location = new System.Drawing.Point(0, 0);
            this.label_Updating.Name = "label_Updating";
            this.label_Updating.Size = new System.Drawing.Size(270, 60);
            this.label_Updating.TabIndex = 0;
            this.label_Updating.Text = "업데이트 진행중...";
            this.label_Updating.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Updating.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_Updating_MouseDown);
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(270, 60);
            this.ControlBox = false;
            this.Controls.Add(this.label_Updating);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Dotum", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdaterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DFA UpdaterForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private LocalizableLabel label_Updating;
    }
}