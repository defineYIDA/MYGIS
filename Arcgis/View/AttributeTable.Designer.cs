namespace Arcgis.View
{
    partial class AttributeTable
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
            this.AttributedataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.AttributedataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AttributedataGridView
            // 
            this.AttributedataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AttributedataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AttributedataGridView.Location = new System.Drawing.Point(0, 0);
            this.AttributedataGridView.Name = "AttributedataGridView";
            this.AttributedataGridView.RowTemplate.Height = 23;
            this.AttributedataGridView.Size = new System.Drawing.Size(426, 366);
            this.AttributedataGridView.TabIndex = 0;
            // 
            // AttributeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 366);
            this.Controls.Add(this.AttributedataGridView);
            this.Name = "AttributeTable";
            this.Text = "AttributeTable";
            this.Load += new System.EventHandler(this.AttributeTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AttributedataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AttributedataGridView;
    }
}