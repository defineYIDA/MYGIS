namespace Arcgis.View
{
    partial class UniqueValuesSymbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UniqueValuesSymbol));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSymbolize = new System.Windows.Forms.Button();
            this.cbxFields = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxLayers2Symbolize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(14, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(161, 109);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(193, 133);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 23);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "关 闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSymbolize
            // 
            this.btnSymbolize.Location = new System.Drawing.Point(193, 89);
            this.btnSymbolize.Name = "btnSymbolize";
            this.btnSymbolize.Size = new System.Drawing.Size(66, 23);
            this.btnSymbolize.TabIndex = 18;
            this.btnSymbolize.Text = "符号化";
            this.btnSymbolize.UseVisualStyleBackColor = true;
            this.btnSymbolize.Click += new System.EventHandler(this.btnSymbolize_Click);
            // 
            // cbxFields
            // 
            this.cbxFields.FormattingEnabled = true;
            this.cbxFields.Location = new System.Drawing.Point(119, 36);
            this.cbxFields.Name = "cbxFields";
            this.cbxFields.Size = new System.Drawing.Size(155, 20);
            this.cbxFields.TabIndex = 17;
            this.cbxFields.SelectedIndexChanged += new System.EventHandler(this.cbxFields_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "选择符号化字段：";
            // 
            // cbxLayers2Symbolize
            // 
            this.cbxLayers2Symbolize.FormattingEnabled = true;
            this.cbxLayers2Symbolize.Location = new System.Drawing.Point(119, 6);
            this.cbxLayers2Symbolize.Name = "cbxLayers2Symbolize";
            this.cbxLayers2Symbolize.Size = new System.Drawing.Size(155, 20);
            this.cbxLayers2Symbolize.TabIndex = 16;
            this.cbxLayers2Symbolize.SelectedIndexChanged += new System.EventHandler(this.cbxLayers2Symbolize_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "选择符号化图层：";
            // 
            // UniqueValuesSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(302, 195);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSymbolize);
            this.Controls.Add(this.cbxFields);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxLayers2Symbolize);
            this.Controls.Add(this.label2);
            this.Name = "UniqueValuesSymbol";
            this.Text = "唯一值符号化";
            this.Load += new System.EventHandler(this.UniqueValuesSymbol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSymbolize;
        private System.Windows.Forms.ComboBox cbxFields;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxLayers2Symbolize;
        private System.Windows.Forms.Label label2;
    }
}