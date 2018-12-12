namespace Arcgis.View
{
    partial class MainPage
    {
        
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl2 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.doc = new System.Windows.Forms.ToolStripMenuItem();
            this.openMapDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.data = new System.Windows.Forms.ToolStripMenuItem();
            this.openSHPData = new System.Windows.Forms.ToolStripMenuItem();
            this.openRasterFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openPersonalDataSet = new System.Windows.Forms.ToolStripMenuItem();
            this.createFeatureClass = new System.Windows.Forms.ToolStripMenuItem();
            this.chkCustomize = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.starEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.endEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.selectLayer = new System.Windows.Forms.ToolStripComboBox();
            this.addLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.attributeTable = new System.Windows.Forms.ToolStripMenuItem();
            this.delLayer = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 25);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(823, 28);
            this.axToolbarControl1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 53);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(823, 587);
            this.splitContainer1.SplitterDistance = 273;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.axTOCControl1);
            this.splitContainer2.Panel1.Controls.Add(this.axLicenseControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.axMapControl2);
            this.splitContainer2.Size = new System.Drawing.Size(273, 587);
            this.splitContainer2.SplitterDistance = 333;
            this.splitContainer2.TabIndex = 0;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(273, 333);
            this.axTOCControl1.TabIndex = 1;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            this.axTOCControl1.OnBeginLabelEdit += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnBeginLabelEditEventHandler(this.axTOCControl1_OnBeginLabelEdit);
            this.axTOCControl1.OnEndLabelEdit += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnEndLabelEditEventHandler(this.axTOCControl1_OnEndLabelEdit);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(79, 106);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // axMapControl2
            // 
            this.axMapControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl2.Location = new System.Drawing.Point(0, 0);
            this.axMapControl2.Name = "axMapControl2";
            this.axMapControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl2.OcxState")));
            this.axMapControl2.Size = new System.Drawing.Size(273, 250);
            this.axMapControl2.TabIndex = 0;
            this.axMapControl2.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl2_OnMouseDown);
            this.axMapControl2.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl2_OnMouseMove);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(546, 587);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axMapControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(538, 561);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据视图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(3, 3);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(532, 555);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
            this.axMapControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.axMapControl1_OnExtentUpdated);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.axPageLayoutControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(740, 669);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "布局视图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(3, 3);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(734, 663);
            this.axPageLayoutControl1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 640);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(823, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(77, 17);
            this.toolStripStatusLabel1.Text = "define_YIDA";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doc,
            this.data,
            this.chkCustomize,
            this.编辑ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // doc
            // 
            this.doc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMapDoc,
            this.toolStripMenuItem1});
            this.doc.Name = "doc";
            this.doc.Size = new System.Drawing.Size(44, 21);
            this.doc.Text = "文件";
            // 
            // openMapDoc
            // 
            this.openMapDoc.Name = "openMapDoc";
            this.openMapDoc.Size = new System.Drawing.Size(112, 22);
            this.openMapDoc.Text = "打开";
            this.openMapDoc.Click += new System.EventHandler(this.openMapDoc_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem1.Text = "另存为";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // data
            // 
            this.data.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSHPData,
            this.openRasterFile,
            this.openPersonalDataSet,
            this.createFeatureClass});
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(44, 21);
            this.data.Text = "数据";
            // 
            // openSHPData
            // 
            this.openSHPData.Name = "openSHPData";
            this.openSHPData.Size = new System.Drawing.Size(160, 22);
            this.openSHPData.Text = "添加矢量数据";
            this.openSHPData.Click += new System.EventHandler(this.openSHPData_Click);
            // 
            // openRasterFile
            // 
            this.openRasterFile.Name = "openRasterFile";
            this.openRasterFile.Size = new System.Drawing.Size(160, 22);
            this.openRasterFile.Text = "添加栅格数据";
            this.openRasterFile.Click += new System.EventHandler(this.openRasterFile_Click);
            // 
            // openPersonalDataSet
            // 
            this.openPersonalDataSet.Name = "openPersonalDataSet";
            this.openPersonalDataSet.Size = new System.Drawing.Size(160, 22);
            this.openPersonalDataSet.Text = "打开个人数据库";
            this.openPersonalDataSet.Click += new System.EventHandler(this.openPersonalDataSet_Click);
            // 
            // createFeatureClass
            // 
            this.createFeatureClass.Name = "createFeatureClass";
            this.createFeatureClass.Size = new System.Drawing.Size(160, 22);
            this.createFeatureClass.Text = "创建要素类";
            this.createFeatureClass.Click += new System.EventHandler(this.createFeatureClass_Click);
            // 
            // chkCustomize
            // 
            this.chkCustomize.Name = "chkCustomize";
            this.chkCustomize.Size = new System.Drawing.Size(80, 21);
            this.chkCustomize.Text = "定制对话框";
            this.chkCustomize.CheckStateChanged += new System.EventHandler(this.chkCustomize_CheckStateChanged);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.starEdit,
            this.saveEdit,
            this.endEdit,
            this.selectLayer,
            this.addLayer});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // starEdit
            // 
            this.starEdit.Name = "starEdit";
            this.starEdit.Size = new System.Drawing.Size(181, 22);
            this.starEdit.Text = "开始编辑";
            this.starEdit.Click += new System.EventHandler(this.starEdit_Click);
            // 
            // saveEdit
            // 
            this.saveEdit.Name = "saveEdit";
            this.saveEdit.Size = new System.Drawing.Size(181, 22);
            this.saveEdit.Text = "保存编辑";
            this.saveEdit.Click += new System.EventHandler(this.saveEdit_Click);
            // 
            // endEdit
            // 
            this.endEdit.Name = "endEdit";
            this.endEdit.Size = new System.Drawing.Size(181, 22);
            this.endEdit.Text = "结束编辑";
            this.endEdit.Click += new System.EventHandler(this.endEdit_Click);
            // 
            // selectLayer
            // 
            this.selectLayer.Name = "selectLayer";
            this.selectLayer.Size = new System.Drawing.Size(121, 25);
            this.selectLayer.SelectedIndexChanged += new System.EventHandler(this.selectLayer_SelectedIndexChanged);
            // 
            // addLayer
            // 
            this.addLayer.Name = "addLayer";
            this.addLayer.Size = new System.Drawing.Size(181, 22);
            this.addLayer.Text = "添加要素";
            this.addLayer.Click += new System.EventHandler(this.addLayer_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attributeTable,
            this.delLayer});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // attributeTable
            // 
            this.attributeTable.Name = "attributeTable";
            this.attributeTable.Size = new System.Drawing.Size(124, 22);
            this.attributeTable.Text = "属性表";
            this.attributeTable.Click += new System.EventHandler(this.attributeTable_Click);
            // 
            // delLayer
            // 
            this.delLayer.Name = "delLayer";
            this.delLayer.Size = new System.Drawing.Size(124, 22);
            this.delLayer.Text = "删除图层";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 662);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "APP";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        public System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.SplitContainer splitContainer2;
        public ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        public ESRI.ArcGIS.Controls.AxMapControl axMapControl2;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem doc;
        public System.Windows.Forms.ToolStripMenuItem map;
        public ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        public ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        public System.Windows.Forms.TabPage tabPage2;
        public ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem openMapDoc;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem attributeTable;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripMenuItem delLayer;
        public System.Windows.Forms.ToolStripMenuItem chkCustomize;
        private System.Windows.Forms.ToolStripMenuItem data;
        private System.Windows.Forms.ToolStripMenuItem openSHPData;
        private System.Windows.Forms.ToolStripMenuItem openRasterFile;
        private System.Windows.Forms.ToolStripMenuItem openPersonalDataSet;
        private System.Windows.Forms.ToolStripMenuItem createFeatureClass;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem starEdit;
        public System.Windows.Forms.ToolStripMenuItem saveEdit;
        public System.Windows.Forms.ToolStripMenuItem endEdit;
        public System.Windows.Forms.ToolStripComboBox selectLayer;
        public System.Windows.Forms.ToolStripMenuItem addLayer;
    }
}

