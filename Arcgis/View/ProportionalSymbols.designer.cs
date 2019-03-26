namespace Arcgis.View
{
    partial class ProportionalSymbols
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProportionalSymbols));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelectSymbol = new System.Windows.Forms.Button();
            this.nudLegendCount = new System.Windows.Forms.NumericUpDown();
            this.btnSelectBackColor = new System.Windows.Forms.Button();
            this.nudMinsize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxNormalization = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFields = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSymbolize = new System.Windows.Forms.Button();
            this.cbxLayers2Symbolize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLegendCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinsize)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelectSymbol);
            this.groupBox2.Controls.Add(this.nudLegendCount);
            this.groupBox2.Controls.Add(this.btnSelectBackColor);
            this.groupBox2.Controls.Add(this.nudMinsize);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(14, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 91);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "符号设置";
            // 
            // btnSelectSymbol
            // 
            this.btnSelectSymbol.BackColor = System.Drawing.Color.Bisque;
            this.btnSelectSymbol.Location = new System.Drawing.Point(8, 20);
            this.btnSelectSymbol.Name = "btnSelectSymbol";
            this.btnSelectSymbol.Size = new System.Drawing.Size(91, 23);
            this.btnSelectSymbol.TabIndex = 14;
            this.btnSelectSymbol.Text = "选择点符号...";
            this.btnSelectSymbol.UseVisualStyleBackColor = false;
            this.btnSelectSymbol.Click += new System.EventHandler(this.btnSelectSymbol_Click);
            // 
            // nudLegendCount
            // 
            this.nudLegendCount.Location = new System.Drawing.Point(230, 56);
            this.nudLegendCount.Name = "nudLegendCount";
            this.nudLegendCount.Size = new System.Drawing.Size(41, 21);
            this.nudLegendCount.TabIndex = 20;
            this.nudLegendCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudLegendCount.ValueChanged += new System.EventHandler(this.nudLegendCount_ValueChanged);
            // 
            // btnSelectBackColor
            // 
            this.btnSelectBackColor.BackColor = System.Drawing.Color.Bisque;
            this.btnSelectBackColor.Location = new System.Drawing.Point(177, 21);
            this.btnSelectBackColor.Name = "btnSelectBackColor";
            this.btnSelectBackColor.Size = new System.Drawing.Size(94, 23);
            this.btnSelectBackColor.TabIndex = 14;
            this.btnSelectBackColor.Text = "选择背景...";
            this.btnSelectBackColor.UseVisualStyleBackColor = false;
            this.btnSelectBackColor.Click += new System.EventHandler(this.btnSelectBackColor_Click);
            // 
            // nudMinsize
            // 
            this.nudMinsize.DecimalPlaces = 2;
            this.nudMinsize.Location = new System.Drawing.Point(96, 56);
            this.nudMinsize.Name = "nudMinsize";
            this.nudMinsize.Size = new System.Drawing.Size(54, 21);
            this.nudMinsize.TabIndex = 20;
            this.nudMinsize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudMinsize.ValueChanged += new System.EventHandler(this.nudMinsize_ValueChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(161, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "图例个数：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "最小符号大小：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbxNormalization);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxFields);
            this.groupBox1.Location = new System.Drawing.Point(14, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 84);
            this.groupBox1.TabIndex = 27;
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
            this.cbxNormalization.Location = new System.Drawing.Point(105, 52);
            this.cbxNormalization.Name = "cbxNormalization";
            this.cbxNormalization.Size = new System.Drawing.Size(122, 20);
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
            this.cbxFields.Location = new System.Drawing.Point(105, 20);
            this.cbxFields.Name = "cbxFields";
            this.cbxFields.Size = new System.Drawing.Size(122, 20);
            this.cbxFields.TabIndex = 10;
            this.cbxFields.SelectedIndexChanged += new System.EventHandler(this.cbxFields_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(265, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(171, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(341, 186);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "关 闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSymbolize
            // 
            this.btnSymbolize.Location = new System.Drawing.Point(341, 149);
            this.btnSymbolize.Name = "btnSymbolize";
            this.btnSymbolize.Size = new System.Drawing.Size(66, 23);
            this.btnSymbolize.TabIndex = 24;
            this.btnSymbolize.Text = "符号化";
            this.btnSymbolize.UseVisualStyleBackColor = true;
            this.btnSymbolize.Click += new System.EventHandler(this.btnSymbolize_Click);
            // 
            // cbxLayers2Symbolize
            // 
            this.cbxLayers2Symbolize.FormattingEnabled = true;
            this.cbxLayers2Symbolize.Location = new System.Drawing.Point(119, 15);
            this.cbxLayers2Symbolize.Name = "cbxLayers2Symbolize";
            this.cbxLayers2Symbolize.Size = new System.Drawing.Size(136, 20);
            this.cbxLayers2Symbolize.TabIndex = 23;
            this.cbxLayers2Symbolize.SelectedIndexChanged += new System.EventHandler(this.cbxLayers2Symbolize_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "选择符号化图层：";
            // 
            // ProportionalSymbols
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(473, 247);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSymbolize);
            this.Controls.Add(this.cbxLayers2Symbolize);
            this.Controls.Add(this.label2);
            this.Name = "ProportionalSymbols";
            this.Text = "依比例符号化";
            this.Load += new System.EventHandler(this.ProportionalSymbols_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudLegendCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinsize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelectSymbol;
        private System.Windows.Forms.NumericUpDown nudLegendCount;
        private System.Windows.Forms.Button btnSelectBackColor;
        private System.Windows.Forms.NumericUpDown nudMinsize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxNormalization;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxFields;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSymbolize;
        private System.Windows.Forms.ComboBox cbxLayers2Symbolize;
        private System.Windows.Forms.Label label2;
    }
}