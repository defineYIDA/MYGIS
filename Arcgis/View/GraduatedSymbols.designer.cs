namespace Arcgis.View
{
    partial class GraduatedSymbols
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraduatedSymbols));
            this.classifyCBX = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaxSize = new System.Windows.Forms.TextBox();
            this.txtMinSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxNormalization = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFields = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudClassCount = new System.Windows.Forms.NumericUpDown();
            this.lblClassificationMethod = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectBackColor = new System.Windows.Forms.Button();
            this.btnSelectSymbol = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSymbolize = new System.Windows.Forms.Button();
            this.cbxLayers2Symbolize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClassCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // classifyCBX
            // 
            this.classifyCBX.FormattingEnabled = true;
            this.classifyCBX.Items.AddRange(new object[] {
            "等间隔分类",
            "分位数分类",
            "自然裂点分类",
            "几何间隔分类"});
            this.classifyCBX.Location = new System.Drawing.Point(138, 214);
            this.classifyCBX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.classifyCBX.Name = "classifyCBX";
            this.classifyCBX.Size = new System.Drawing.Size(116, 20);
            this.classifyCBX.TabIndex = 31;
            this.classifyCBX.SelectedIndexChanged += new System.EventHandler(this.classifyCBX_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtMaxSize);
            this.groupBox3.Controls.Add(this.txtMinSize);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(247, 123);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(185, 54);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "符号大小";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "从：";
            // 
            // txtMaxSize
            // 
            this.txtMaxSize.Location = new System.Drawing.Point(133, 21);
            this.txtMaxSize.Name = "txtMaxSize";
            this.txtMaxSize.Size = new System.Drawing.Size(40, 21);
            this.txtMaxSize.TabIndex = 18;
            this.txtMaxSize.Text = "18";
            this.txtMaxSize.Leave += new System.EventHandler(this.txtMaxSize_Leave);
            // 
            // txtMinSize
            // 
            this.txtMinSize.Location = new System.Drawing.Point(51, 20);
            this.txtMinSize.Name = "txtMinSize";
            this.txtMinSize.Size = new System.Drawing.Size(40, 21);
            this.txtMinSize.TabIndex = 17;
            this.txtMinSize.Text = "4";
            this.txtMinSize.Leave += new System.EventHandler(this.txtMinSize_Leave);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(97, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "到：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbxNormalization);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxFields);
            this.groupBox1.Location = new System.Drawing.Point(14, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 84);
            this.groupBox1.TabIndex = 29;
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
            this.groupBox2.Controls.Add(this.lblClassificationMethod);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 78);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "分类";
            // 
            // nudClassCount
            // 
            this.nudClassCount.Location = new System.Drawing.Point(70, 41);
            this.nudClassCount.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudClassCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudClassCount.Name = "nudClassCount";
            this.nudClassCount.Size = new System.Drawing.Size(52, 21);
            this.nudClassCount.TabIndex = 18;
            this.nudClassCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudClassCount.ValueChanged += new System.EventHandler(this.nudClassCount_ValueChanged);
            // 
            // lblClassificationMethod
            // 
            this.lblClassificationMethod.Location = new System.Drawing.Point(68, 17);
            this.lblClassificationMethod.Name = "lblClassificationMethod";
            this.lblClassificationMethod.Size = new System.Drawing.Size(110, 14);
            this.lblClassificationMethod.TabIndex = 5;
            this.lblClassificationMethod.Text = "分类方法：";
            this.lblClassificationMethod.Click += new System.EventHandler(this.lblClassificationMethod_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "分类数：";
            // 
            // btnSelectBackColor
            // 
            this.btnSelectBackColor.BackColor = System.Drawing.Color.Bisque;
            this.btnSelectBackColor.Location = new System.Drawing.Point(129, 134);
            this.btnSelectBackColor.Name = "btnSelectBackColor";
            this.btnSelectBackColor.Size = new System.Drawing.Size(101, 23);
            this.btnSelectBackColor.TabIndex = 27;
            this.btnSelectBackColor.Text = "选择背景...";
            this.btnSelectBackColor.UseVisualStyleBackColor = false;
            this.btnSelectBackColor.Click += new System.EventHandler(this.btnSelectBackColor_Click);
            // 
            // btnSelectSymbol
            // 
            this.btnSelectSymbol.BackColor = System.Drawing.Color.Bisque;
            this.btnSelectSymbol.Location = new System.Drawing.Point(12, 134);
            this.btnSelectSymbol.Name = "btnSelectSymbol";
            this.btnSelectSymbol.Size = new System.Drawing.Size(101, 23);
            this.btnSelectSymbol.TabIndex = 26;
            this.btnSelectSymbol.Text = "选择点符号...";
            this.btnSelectSymbol.UseVisualStyleBackColor = false;
            this.btnSelectSymbol.Click += new System.EventHandler(this.btnSelectSymbol_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(247, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(185, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(358, 209);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 23);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "关 闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSymbolize
            // 
            this.btnSymbolize.Location = new System.Drawing.Point(272, 208);
            this.btnSymbolize.Name = "btnSymbolize";
            this.btnSymbolize.Size = new System.Drawing.Size(66, 23);
            this.btnSymbolize.TabIndex = 23;
            this.btnSymbolize.Text = "符号化";
            this.btnSymbolize.UseVisualStyleBackColor = true;
            this.btnSymbolize.Click += new System.EventHandler(this.btnSymbolize_Click);
            // 
            // cbxLayers2Symbolize
            // 
            this.cbxLayers2Symbolize.FormattingEnabled = true;
            this.cbxLayers2Symbolize.Location = new System.Drawing.Point(119, 5);
            this.cbxLayers2Symbolize.Name = "cbxLayers2Symbolize";
            this.cbxLayers2Symbolize.Size = new System.Drawing.Size(111, 20);
            this.cbxLayers2Symbolize.TabIndex = 22;
            this.cbxLayers2Symbolize.SelectedIndexChanged += new System.EventHandler(this.cbxLayers2Symbolize_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "选择符号化图层：";
            // 
            // GraduatedSymbols
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(457, 269);
            this.Controls.Add(this.classifyCBX);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSelectBackColor);
            this.Controls.Add(this.btnSelectSymbol);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSymbolize);
            this.Controls.Add(this.cbxLayers2Symbolize);
            this.Controls.Add(this.label2);
            this.Name = "GraduatedSymbols";
            this.Text = "分类符号化";
            this.Load += new System.EventHandler(this.GraduatedSymbols_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudClassCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox classifyCBX;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaxSize;
        private System.Windows.Forms.TextBox txtMinSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxNormalization;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxFields;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudClassCount;
        private System.Windows.Forms.Label lblClassificationMethod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSelectBackColor;
        private System.Windows.Forms.Button btnSelectSymbol;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSymbolize;
        private System.Windows.Forms.ComboBox cbxLayers2Symbolize;
        private System.Windows.Forms.Label label2;
    }
}