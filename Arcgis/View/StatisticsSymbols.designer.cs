namespace Arcgis.View
{
    partial class StatisticsSymbols
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsSymbols));
            this.btnSelectBackGroud = new System.Windows.Forms.Button();
            this.btnSelectColorRamp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.nudDotSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvRendererFields = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAllOut = new System.Windows.Forms.Button();
            this.btnOut = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.lstSourceFields = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSymbolize = new System.Windows.Forms.Button();
            this.cbxSymbolizeMethds = new System.Windows.Forms.ComboBox();
            this.cbxLayers2Symbolize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.symbolImageList = new System.Windows.Forms.ImageList(this.components);
            this.backImageList = new System.Windows.Forms.ImageList(this.components);
            this.Largeimage = new System.Windows.Forms.ImageList(this.components);
            this.Smallimage = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDotSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectBackGroud
            // 
            this.btnSelectBackGroud.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSelectBackGroud.Location = new System.Drawing.Point(197, 35);
            this.btnSelectBackGroud.Name = "btnSelectBackGroud";
            this.btnSelectBackGroud.Size = new System.Drawing.Size(128, 23);
            this.btnSelectBackGroud.TabIndex = 30;
            this.btnSelectBackGroud.Text = "选择背景...";
            this.btnSelectBackGroud.UseVisualStyleBackColor = false;
            this.btnSelectBackGroud.Click += new System.EventHandler(this.btnSelectBackGroud_Click);
            // 
            // btnSelectColorRamp
            // 
            this.btnSelectColorRamp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSelectColorRamp.Location = new System.Drawing.Point(40, 35);
            this.btnSelectColorRamp.Name = "btnSelectColorRamp";
            this.btnSelectColorRamp.Size = new System.Drawing.Size(128, 23);
            this.btnSelectColorRamp.TabIndex = 33;
            this.btnSelectColorRamp.Text = "选择色带...";
            this.btnSelectColorRamp.UseVisualStyleBackColor = false;
            this.btnSelectColorRamp.Click += new System.EventHandler(this.btnSelectColorRamp_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.nudDotSize);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(14, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 56);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "符号设置";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(183, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "属  性...";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nudDotSize
            // 
            this.nudDotSize.DecimalPlaces = 2;
            this.nudDotSize.Location = new System.Drawing.Point(100, 20);
            this.nudDotSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudDotSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDotSize.Name = "nudDotSize";
            this.nudDotSize.Size = new System.Drawing.Size(54, 21);
            this.nudDotSize.TabIndex = 20;
            this.nudDotSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudDotSize.ValueChanged += new System.EventHandler(this.nudDotSize_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "符号大小：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvRendererFields);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnAllOut);
            this.groupBox1.Controls.Add(this.btnOut);
            this.groupBox1.Controls.Add(this.btnIn);
            this.groupBox1.Controls.Add(this.lstSourceFields);
            this.groupBox1.Location = new System.Drawing.Point(14, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 146);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段";
            // 
            // lvRendererFields
            // 
            this.lvRendererFields.Location = new System.Drawing.Point(197, 36);
            this.lvRendererFields.Name = "lvRendererFields";
            this.lvRendererFields.Size = new System.Drawing.Size(128, 100);
            this.lvRendererFields.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvRendererFields.TabIndex = 25;
            this.lvRendererFields.UseCompatibleStateImageBehavior = false;
            this.lvRendererFields.DoubleClick += new System.EventHandler(this.lvRendererFields_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "用于符号化的字段：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "所有字段：";
            // 
            // btnAllOut
            // 
            this.btnAllOut.Image = ((System.Drawing.Image)(resources.GetObject("btnAllOut.Image")));
            this.btnAllOut.Location = new System.Drawing.Point(149, 103);
            this.btnAllOut.Name = "btnAllOut";
            this.btnAllOut.Size = new System.Drawing.Size(35, 23);
            this.btnAllOut.TabIndex = 3;
            this.btnAllOut.UseVisualStyleBackColor = true;
            this.btnAllOut.Click += new System.EventHandler(this.btnAllOut_Click);
            // 
            // btnOut
            // 
            this.btnOut.Image = ((System.Drawing.Image)(resources.GetObject("btnOut.Image")));
            this.btnOut.Location = new System.Drawing.Point(149, 74);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(35, 23);
            this.btnOut.TabIndex = 2;
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // btnIn
            // 
            this.btnIn.Image = ((System.Drawing.Image)(resources.GetObject("btnIn.Image")));
            this.btnIn.Location = new System.Drawing.Point(149, 45);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(35, 23);
            this.btnIn.TabIndex = 1;
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // lstSourceFields
            // 
            this.lstSourceFields.FormattingEnabled = true;
            this.lstSourceFields.ItemHeight = 12;
            this.lstSourceFields.Location = new System.Drawing.Point(10, 36);
            this.lstSourceFields.Name = "lstSourceFields";
            this.lstSourceFields.Size = new System.Drawing.Size(128, 100);
            this.lstSourceFields.Sorted = true;
            this.lstSourceFields.TabIndex = 0;
            this.lstSourceFields.DoubleClick += new System.EventHandler(this.lstSourceFields_DoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(238, 363);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 23);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "关 闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSymbolize
            // 
            this.btnSymbolize.Location = new System.Drawing.Point(238, 320);
            this.btnSymbolize.Name = "btnSymbolize";
            this.btnSymbolize.Size = new System.Drawing.Size(66, 23);
            this.btnSymbolize.TabIndex = 27;
            this.btnSymbolize.Text = "符号化";
            this.btnSymbolize.UseVisualStyleBackColor = true;
            this.btnSymbolize.Click += new System.EventHandler(this.btnSymbolize_Click);
            // 
            // cbxSymbolizeMethds
            // 
            this.cbxSymbolizeMethds.FormattingEnabled = true;
            this.cbxSymbolizeMethds.Items.AddRange(new object[] {
            "饼状图",
            "柱状图",
            "堆积柱状图"});
            this.cbxSymbolizeMethds.Location = new System.Drawing.Point(271, 6);
            this.cbxSymbolizeMethds.Name = "cbxSymbolizeMethds";
            this.cbxSymbolizeMethds.Size = new System.Drawing.Size(79, 20);
            this.cbxSymbolizeMethds.TabIndex = 26;
            this.cbxSymbolizeMethds.SelectedIndexChanged += new System.EventHandler(this.cbxSymbolizeMethds_SelectedIndexChanged);
            // 
            // cbxLayers2Symbolize
            // 
            this.cbxLayers2Symbolize.FormattingEnabled = true;
            this.cbxLayers2Symbolize.Location = new System.Drawing.Point(59, 6);
            this.cbxLayers2Symbolize.Name = "cbxLayers2Symbolize";
            this.cbxLayers2Symbolize.Size = new System.Drawing.Size(131, 20);
            this.cbxLayers2Symbolize.TabIndex = 25;
            this.cbxLayers2Symbolize.SelectedIndexChanged += new System.EventHandler(this.cbxLayers2Symbolize_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "符号化方法：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "图层：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(14, 288);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // symbolImageList
            // 
            this.symbolImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.symbolImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.symbolImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // backImageList
            // 
            this.backImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("backImageList.ImageStream")));
            this.backImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.backImageList.Images.SetKeyName(0, "Pie.bmp");
            this.backImageList.Images.SetKeyName(1, "BarColumn.bmp");
            this.backImageList.Images.SetKeyName(2, "Stacked.bmp");
            // 
            // Largeimage
            // 
            this.Largeimage.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.Largeimage.ImageSize = new System.Drawing.Size(16, 16);
            this.Largeimage.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Smallimage
            // 
            this.Smallimage.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.Smallimage.ImageSize = new System.Drawing.Size(16, 16);
            this.Smallimage.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // StatisticsSymbols
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(390, 446);
            this.Controls.Add(this.btnSelectBackGroud);
            this.Controls.Add(this.btnSelectColorRamp);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSymbolize);
            this.Controls.Add(this.cbxSymbolizeMethds);
            this.Controls.Add(this.cbxLayers2Symbolize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "StatisticsSymbols";
            this.Text = "统计符号化";
            this.Load += new System.EventHandler(this.StatisticsSymbols_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDotSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectBackGroud;
        private System.Windows.Forms.Button btnSelectColorRamp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nudDotSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvRendererFields;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAllOut;
        private System.Windows.Forms.Button btnOut;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.ListBox lstSourceFields;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSymbolize;
        private System.Windows.Forms.ComboBox cbxSymbolizeMethds;
        private System.Windows.Forms.ComboBox cbxLayers2Symbolize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ImageList symbolImageList;
        private System.Windows.Forms.ImageList backImageList;
        private System.Windows.Forms.ImageList Largeimage;
        private System.Windows.Forms.ImageList Smallimage;
    }
}