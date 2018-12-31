namespace Arcgis.View
{
    partial class ClassBreaksRendererSymbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassBreaksRendererSymbol));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxNormalization = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFields = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudClassCount = new System.Windows.Forms.NumericUpDown();
            this.cbxInterval = new System.Windows.Forms.ComboBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.cbxClassifyMethods = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInterval = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaxValue = new System.Windows.Forms.TextBox();
            this.txtMinValue = new System.Windows.Forms.TextBox();
            this.btnSelectSymbol = new System.Windows.Forms.Button();
            this.btnSelectColorRamp = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSymbolize = new System.Windows.Forms.Button();
            this.cbxLayers2Symbolize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClassCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbxNormalization);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxFields);
            this.groupBox1.Location = new System.Drawing.Point(14, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 84);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "标准化字段：";
            // 
            // cbxNormalization
            // 
            this.cbxNormalization.FormattingEnabled = true;
            this.cbxNormalization.Location = new System.Drawing.Point(94, 51);
            this.cbxNormalization.Name = "cbxNormalization";
            this.cbxNormalization.Size = new System.Drawing.Size(106, 20);
            this.cbxNormalization.TabIndex = 10;
            this.cbxNormalization.SelectedIndexChanged += new System.EventHandler(this.cbxNormalization_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "值字段：";
            // 
            // cbxFields
            // 
            this.cbxFields.FormattingEnabled = true;
            this.cbxFields.Location = new System.Drawing.Point(94, 19);
            this.cbxFields.Name = "cbxFields";
            this.cbxFields.Size = new System.Drawing.Size(106, 20);
            this.cbxFields.TabIndex = 10;
            this.cbxFields.SelectedIndexChanged += new System.EventHandler(this.cbxFields_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudClassCount);
            this.groupBox2.Controls.Add(this.cbxInterval);
            this.groupBox2.Controls.Add(this.txtInterval);
            this.groupBox2.Controls.Add(this.cbxClassifyMethods);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblInterval);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtMaxValue);
            this.groupBox2.Controls.Add(this.txtMinValue);
            this.groupBox2.Location = new System.Drawing.Point(14, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 93);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "分类";
            // 
            // nudClassCount
            // 
            this.nudClassCount.Location = new System.Drawing.Point(67, 58);
            this.nudClassCount.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudClassCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudClassCount.Name = "nudClassCount";
            this.nudClassCount.Size = new System.Drawing.Size(40, 21);
            this.nudClassCount.TabIndex = 19;
            this.nudClassCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudClassCount.ValueChanged += new System.EventHandler(this.nudClassCount_ValueChanged);
            // 
            // cbxInterval
            // 
            this.cbxInterval.Enabled = false;
            this.cbxInterval.FormattingEnabled = true;
            this.cbxInterval.Items.AddRange(new object[] {
            "1个标准差",
            "1/2个标准差",
            "1/3个标准差",
            "1/4个标准差"});
            this.cbxInterval.Location = new System.Drawing.Point(280, 15);
            this.cbxInterval.Name = "cbxInterval";
            this.cbxInterval.Size = new System.Drawing.Size(93, 20);
            this.cbxInterval.TabIndex = 10;
            this.cbxInterval.Visible = false;
            this.cbxInterval.SelectedIndexChanged += new System.EventHandler(this.cbxInterval_SelectedIndexChanged);
            // 
            // txtInterval
            // 
            this.txtInterval.Enabled = false;
            this.txtInterval.Location = new System.Drawing.Point(280, 39);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(93, 21);
            this.txtInterval.TabIndex = 9;
            this.txtInterval.Visible = false;
            this.txtInterval.TextChanged += new System.EventHandler(this.txtInterval_TextChanged);
            // 
            // cbxClassifyMethods
            // 
            this.cbxClassifyMethods.FormattingEnabled = true;
            this.cbxClassifyMethods.Items.AddRange(new object[] {
            "等间隔",
            "已定义的间隔",
            "分位数",
            "自然裂点",
            "标准差"});
            this.cbxClassifyMethods.Location = new System.Drawing.Point(79, 21);
            this.cbxClassifyMethods.Name = "cbxClassifyMethods";
            this.cbxClassifyMethods.Size = new System.Drawing.Size(123, 20);
            this.cbxClassifyMethods.TabIndex = 8;
            this.cbxClassifyMethods.SelectedIndexChanged += new System.EventHandler(this.cbxClassifyMethods_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(244, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "最大值：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(113, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "最小值：";
            // 
            // lblInterval
            // 
            this.lblInterval.Location = new System.Drawing.Point(208, 21);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(66, 14);
            this.lblInterval.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "分类方法：";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "分类数：";
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.Enabled = false;
            this.txtMaxValue.Location = new System.Drawing.Point(304, 66);
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.Size = new System.Drawing.Size(69, 21);
            this.txtMaxValue.TabIndex = 4;
            // 
            // txtMinValue
            // 
            this.txtMinValue.Enabled = false;
            this.txtMinValue.Location = new System.Drawing.Point(168, 66);
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.Size = new System.Drawing.Size(70, 21);
            this.txtMinValue.TabIndex = 3;
            // 
            // btnSelectSymbol
            // 
            this.btnSelectSymbol.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSelectSymbol.Location = new System.Drawing.Point(221, 131);
            this.btnSelectSymbol.Name = "btnSelectSymbol";
            this.btnSelectSymbol.Size = new System.Drawing.Size(178, 23);
            this.btnSelectSymbol.TabIndex = 22;
            this.btnSelectSymbol.Text = "选择符号...";
            this.btnSelectSymbol.UseVisualStyleBackColor = false;
            this.btnSelectSymbol.Click += new System.EventHandler(this.btnSelectSymbol_Click);
            // 
            // btnSelectColorRamp
            // 
            this.btnSelectColorRamp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSelectColorRamp.Location = new System.Drawing.Point(14, 131);
            this.btnSelectColorRamp.Name = "btnSelectColorRamp";
            this.btnSelectColorRamp.Size = new System.Drawing.Size(178, 23);
            this.btnSelectColorRamp.TabIndex = 23;
            this.btnSelectColorRamp.Text = "选择用于符号化的色带...";
            this.btnSelectColorRamp.UseVisualStyleBackColor = false;
            this.btnSelectColorRamp.Click += new System.EventHandler(this.btnSelectColorRamp_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(247, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(224, 259);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 23);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "关 闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSymbolize
            // 
            this.btnSymbolize.Location = new System.Drawing.Point(115, 259);
            this.btnSymbolize.Name = "btnSymbolize";
            this.btnSymbolize.Size = new System.Drawing.Size(66, 23);
            this.btnSymbolize.TabIndex = 19;
            this.btnSymbolize.Text = "符号化";
            this.btnSymbolize.UseVisualStyleBackColor = true;
            this.btnSymbolize.Click += new System.EventHandler(this.btnSymbolize_Click);
            // 
            // cbxLayers2Symbolize
            // 
            this.cbxLayers2Symbolize.FormattingEnabled = true;
            this.cbxLayers2Symbolize.Location = new System.Drawing.Point(119, 14);
            this.cbxLayers2Symbolize.Name = "cbxLayers2Symbolize";
            this.cbxLayers2Symbolize.Size = new System.Drawing.Size(111, 20);
            this.cbxLayers2Symbolize.TabIndex = 18;
            this.cbxLayers2Symbolize.SelectedIndexChanged += new System.EventHandler(this.cbxLayers2Symbolize_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "选择符号化图层：";
            // 
            // ClassBreaksRendererSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(443, 294);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSelectSymbol);
            this.Controls.Add(this.btnSelectColorRamp);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSymbolize);
            this.Controls.Add(this.cbxLayers2Symbolize);
            this.Controls.Add(this.label2);
            this.Name = "ClassBreaksRendererSymbol";
            this.Text = "分类符号化";
            this.Load += new System.EventHandler(this.ClassBreaksRendererSymbol_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClassCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxNormalization;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxFields;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudClassCount;
        private System.Windows.Forms.ComboBox cbxInterval;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.ComboBox cbxClassifyMethods;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMaxValue;
        private System.Windows.Forms.TextBox txtMinValue;
        private System.Windows.Forms.Button btnSelectSymbol;
        private System.Windows.Forms.Button btnSelectColorRamp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSymbolize;
        private System.Windows.Forms.ComboBox cbxLayers2Symbolize;
        private System.Windows.Forms.Label label2;
    }
}