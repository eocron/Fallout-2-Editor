namespace F2E.App
{
    partial class Main
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
            this.FalloutFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.FalloutFolderPathTb = new System.Windows.Forms.TextBox();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.FrmEditorBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FalloutFolderPathTb
            // 
            this.FalloutFolderPathTb.Location = new System.Drawing.Point(12, 29);
            this.FalloutFolderPathTb.Name = "FalloutFolderPathTb";
            this.FalloutFolderPathTb.Size = new System.Drawing.Size(398, 20);
            this.FalloutFolderPathTb.TabIndex = 1;
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Location = new System.Drawing.Point(417, 25);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.BrowseBtn.TabIndex = 2;
            this.BrowseBtn.Text = "Browse";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // FrmEditorBtn
            // 
            this.FrmEditorBtn.Location = new System.Drawing.Point(12, 67);
            this.FrmEditorBtn.Name = "FrmEditorBtn";
            this.FrmEditorBtn.Size = new System.Drawing.Size(75, 23);
            this.FrmEditorBtn.TabIndex = 3;
            this.FrmEditorBtn.Text = "FRM Editor";
            this.FrmEditorBtn.UseVisualStyleBackColor = true;
            this.FrmEditorBtn.Click += new System.EventHandler(this.FrmEditorBtn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 464);
            this.Controls.Add(this.FrmEditorBtn);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.FalloutFolderPathTb);
            this.Name = "Main";
            this.Text = "Fallout 2 Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog FalloutFolderBrowserDialog;
        private System.Windows.Forms.TextBox FalloutFolderPathTb;
        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.Button FrmEditorBtn;
    }
}

