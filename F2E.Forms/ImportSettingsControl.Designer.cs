namespace F2E.Forms
{
    partial class ImportSettingsControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.BrightnessUpDown = new System.Windows.Forms.NumericUpDown();
            this.WidthUpDown = new System.Windows.Forms.NumericUpDown();
            this.HeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.InterpoationComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BrightnessTargetUpDown = new System.Windows.Forms.NumericUpDown();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightUpDown)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessTargetUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Brightness";
            // 
            // BrightnessUpDown
            // 
            this.BrightnessUpDown.DecimalPlaces = 2;
            this.BrightnessUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.BrightnessUpDown.Location = new System.Drawing.Point(123, 3);
            this.BrightnessUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.BrightnessUpDown.Name = "BrightnessUpDown";
            this.BrightnessUpDown.Size = new System.Drawing.Size(114, 20);
            this.BrightnessUpDown.TabIndex = 1;
            this.BrightnessUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // WidthUpDown
            // 
            this.WidthUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.WidthUpDown.Location = new System.Drawing.Point(123, 90);
            this.WidthUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.WidthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WidthUpDown.Name = "WidthUpDown";
            this.WidthUpDown.Size = new System.Drawing.Size(114, 20);
            this.WidthUpDown.TabIndex = 2;
            this.WidthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // HeightUpDown
            // 
            this.HeightUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.HeightUpDown.Location = new System.Drawing.Point(123, 119);
            this.HeightUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.HeightUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HeightUpDown.Name = "HeightUpDown";
            this.HeightUpDown.Size = new System.Drawing.Size(114, 20);
            this.HeightUpDown.TabIndex = 3;
            this.HeightUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Height";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.BrightnessUpDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.HeightUpDown, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.WidthUpDown, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.InterpoationComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.BrightnessTargetUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ApplyButton, 2, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(331, 148);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // InterpoationComboBox
            // 
            this.InterpoationComboBox.FormattingEnabled = true;
            this.InterpoationComboBox.Location = new System.Drawing.Point(123, 61);
            this.InterpoationComboBox.Name = "InterpoationComboBox";
            this.InterpoationComboBox.Size = new System.Drawing.Size(114, 21);
            this.InterpoationComboBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Resize interpolation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Brightness in target";
            // 
            // BrightnessTargetUpDown
            // 
            this.BrightnessTargetUpDown.DecimalPlaces = 2;
            this.BrightnessTargetUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.BrightnessTargetUpDown.Location = new System.Drawing.Point(123, 32);
            this.BrightnessTargetUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.BrightnessTargetUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BrightnessTargetUpDown.Name = "BrightnessTargetUpDown";
            this.BrightnessTargetUpDown.Size = new System.Drawing.Size(114, 20);
            this.BrightnessTargetUpDown.TabIndex = 9;
            this.BrightnessTargetUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Location = new System.Drawing.Point(243, 119);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(85, 26);
            this.ApplyButton.TabIndex = 10;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 174);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import settings";
            // 
            // ImportSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(350, 180);
            this.MinimumSize = new System.Drawing.Size(350, 180);
            this.Name = "ImportSettingsControl";
            this.Size = new System.Drawing.Size(350, 180);
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightUpDown)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessTargetUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown BrightnessUpDown;
        private System.Windows.Forms.NumericUpDown WidthUpDown;
        private System.Windows.Forms.NumericUpDown HeightUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox InterpoationComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown BrightnessTargetUpDown;
        private System.Windows.Forms.Button ApplyButton;
    }
}
