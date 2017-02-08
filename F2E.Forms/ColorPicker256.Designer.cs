namespace F2E.Forms
{
    partial class ColorPicker256
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PickerPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PickerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PickerPictureBox
            // 
            this.PickerPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PickerPictureBox.Location = new System.Drawing.Point(0, 0);
            this.PickerPictureBox.Name = "PickerPictureBox";
            this.PickerPictureBox.Size = new System.Drawing.Size(314, 359);
            this.PickerPictureBox.TabIndex = 0;
            this.PickerPictureBox.TabStop = false;
            this.PickerPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PickerPictureBox_MouseClick);
            // 
            // ColorPicker256
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PickerPictureBox);
            this.Name = "ColorPicker256";
            this.Size = new System.Drawing.Size(314, 359);
            ((System.ComponentModel.ISupportInitialize)(this.PickerPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PickerPictureBox;
    }
}
