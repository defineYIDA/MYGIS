using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Drawing.Printing;
using Microsoft.VisualBasic;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS;
using ESRI.ArcGIS.OutputExtensions;

namespace Arcgis.View
{
    public class PrintAndExport : System.Windows.Forms.Form
    {
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnPrint;

        internal PrintPreviewDialog printPreviewDialog1;
        internal PrintDialog printDialog1;
        internal PageSetupDialog pageSetupDialog1;

        private System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();
        private ITrackCancel m_TrackCancel = new CancelTrackerClass();
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private short m_CurrentPrintPage;
          
        IPrintAndExport printAndExport = new PrintAndExportClass();
        IExport export=new ExportBMPClass();
        IOutputRasterSettings rasterSettings;
        string strOutputType = "ExportBMP";
        private GroupBox groupBox1;
        private Button btnPrintPreview;
        private Button btnPageSetup;
        private Label label1;
        private ComboBox cbxMappingType;
        private Label lblMapping;
        private RadioButton rdoOutput;
        private RadioButton rdoPrint;
        private Button btnOutputFile;
        long iOutputResolution = 300;
        private SaveFileDialog saveFileDialog1=new SaveFileDialog();
        string exportFileName = "C:\\Temp\\test.bmp";
        string fileExtension = "BMP";
        private Label outputPath;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;

        public PrintAndExport(string path)
        {
            InitializeComponent();
            ////IObjectCopy接口提供Copy方法用于地图的复制
            //IObjectCopy objectCopy = new ObjectCopyClass();
            //object copyFromMap = axMapControl1.Map;//要copy的map
            //object copyMap = objectCopy.Copy(copyFromMap);
            //object copyToMap = axPageLayoutControl1.ActiveView.FocusMap;
            ////Overwrite方法用于地图写入PageLayoutControl控件的视图中
            //objectCopy.Overwrite(copyMap, ref copyToMap);//引用传递焦点视图
            axPageLayoutControl1.LoadMxFile(path);
        }

        protected override void Dispose(bool disposing)
        {
            //Release COM objects
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintAndExport));
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.outputPath = new System.Windows.Forms.Label();
            this.btnOutputFile = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPageSetup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxMappingType = new System.Windows.Forms.ComboBox();
            this.lblMapping = new System.Windows.Forms.Label();
            this.rdoOutput = new System.Windows.Forms.RadioButton();
            this.rdoPrint = new System.Windows.Forms.RadioButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(563, 85);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(88, 26);
            this.btnOpenFile.TabIndex = 1;
            this.btnOpenFile.Text = "其他地图文档";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(563, 25);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 25);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "打印/输出";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Location = new System.Drawing.Point(10, 115);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(679, 476);
            this.axPageLayoutControl1.TabIndex = 9;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(10, 10);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(321, 28);
            this.axToolbarControl1.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOutputFile);
            this.groupBox1.Controls.Add(this.outputPath);
            this.groupBox1.Controls.Add(this.btnPageSetup);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxMappingType);
            this.groupBox1.Controls.Add(this.lblMapping);
            this.groupBox1.Controls.Add(this.rdoOutput);
            this.groupBox1.Controls.Add(this.rdoPrint);
            this.groupBox1.Location = new System.Drawing.Point(10, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 73);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印/输出";
            // 
            // outputPath
            // 
            this.outputPath.AutoSize = true;
            this.outputPath.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outputPath.Location = new System.Drawing.Point(159, 51);
            this.outputPath.Name = "outputPath";
            this.outputPath.Size = new System.Drawing.Size(21, 14);
            this.outputPath.TabIndex = 23;
            this.outputPath.Text = "无";
            // 
            // btnOutputFile
            // 
            this.btnOutputFile.BackColor = System.Drawing.Color.White;
            this.btnOutputFile.Location = new System.Drawing.Point(429, 46);
            this.btnOutputFile.Name = "btnOutputFile";
            this.btnOutputFile.Size = new System.Drawing.Size(87, 22);
            this.btnOutputFile.TabIndex = 22;
            this.btnOutputFile.Text = "选择输出路径";
            this.btnOutputFile.UseVisualStyleBackColor = false;
            this.btnOutputFile.Click += new System.EventHandler(this.btnOutputFile_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Location = new System.Drawing.Point(563, 56);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(88, 26);
            this.btnPrintPreview.TabIndex = 21;
            this.btnPrintPreview.Text = "打印预览";
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnPageSetup
            // 
            this.btnPageSetup.Location = new System.Drawing.Point(429, 13);
            this.btnPageSetup.Name = "btnPageSetup";
            this.btnPageSetup.Size = new System.Drawing.Size(87, 26);
            this.btnPageSetup.TabIndex = 20;
            this.btnPageSetup.Text = "页面设置";
            this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "输出路径：";
            // 
            // cbxMappingType
            // 
            this.cbxMappingType.Items.AddRange(new object[] {
            "esriPageMappingScale",
            "esriPageMappingTile",
            "esriPageMappingCrop"});
            this.cbxMappingType.Location = new System.Drawing.Point(161, 14);
            this.cbxMappingType.Name = "cbxMappingType";
            this.cbxMappingType.Size = new System.Drawing.Size(171, 20);
            this.cbxMappingType.TabIndex = 18;
            // 
            // lblMapping
            // 
            this.lblMapping.AutoSize = true;
            this.lblMapping.Location = new System.Drawing.Point(73, 20);
            this.lblMapping.Name = "lblMapping";
            this.lblMapping.Size = new System.Drawing.Size(89, 12);
            this.lblMapping.TabIndex = 16;
            this.lblMapping.Text = "选择映射关系：";
            // 
            // rdoOutput
            // 
            this.rdoOutput.AutoSize = true;
            this.rdoOutput.Location = new System.Drawing.Point(25, 47);
            this.rdoOutput.Name = "rdoOutput";
            this.rdoOutput.Size = new System.Drawing.Size(47, 16);
            this.rdoOutput.TabIndex = 1;
            this.rdoOutput.Text = "输出";
            this.rdoOutput.UseVisualStyleBackColor = true;
            // 
            // rdoPrint
            // 
            this.rdoPrint.AutoSize = true;
            this.rdoPrint.Checked = true;
            this.rdoPrint.Location = new System.Drawing.Point(25, 19);
            this.rdoPrint.Name = "rdoPrint";
            this.rdoPrint.Size = new System.Drawing.Size(47, 16);
            this.rdoPrint.TabIndex = 0;
            this.rdoPrint.TabStop = true;
            this.rdoPrint.Text = "打印";
            this.rdoPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rdoPrint.UseVisualStyleBackColor = true;
            // 
            // PrintAndExport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(709, 599);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.btnPrintPreview);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "PrintAndExport";
            this.Text = "地图的打印与转换输出";
            this.Load += new System.EventHandler(this.PrintActiveView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void PrintActiveView_Load(object sender, EventArgs e)
        {
            InitializePrintPreviewDialog(); //initialize the print preview dialog
            printDialog1 = new PrintDialog(); //create a print dialog object
            InitializePageSetupDialog(); //initialize the page setup dialog         
            axPageLayoutControl1.AutoMouseWheel = true;
            axPageLayoutControl1.Refresh();
        }

        private void InitializePrintPreviewDialog()
        {
            printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Name = "打印预览";
            //set UseAntiAlias to true to allow the operating system to smooth fonts
            printPreviewDialog1.UseAntiAlias = true;

            //printPreviewDialog1.ClientSize = new System.Drawing.Size(800, 600);
            //printPreviewDialog1.Location = new System.Drawing.Point(29, 29);
            //printPreviewDialog1.MinimumSize = new System.Drawing.Size(375, 250);

            //associate the event-handling method with the document's PrintPage event
            this.document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(document_PrintPage);
        }

        private void InitializePageSetupDialog()
        {
            pageSetupDialog1 = new PageSetupDialog();  
            pageSetupDialog1.PageSettings = new System.Drawing.Printing.PageSettings();  
            pageSetupDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();    
            pageSetupDialog1.ShowNetwork = false;
            
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "template files (*.mxt)|*.mxt|mxd files (*.mxd)|*.mxd";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //检测是否选择了一个文件
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    string fileName = openFileDialog1.FileName;
                    //检测选择的文件是否为一mxd 文件
                    if (axPageLayoutControl1.CheckMxFile(fileName))
                    {                        
                        axPageLayoutControl1.LoadMxFile(fileName, "");
                    }
                    myStream.Close();
                }
            }
        }

        private void btnPageSetup_Click(object sender, EventArgs e)
        {
            DialogResult result = pageSetupDialog1.ShowDialog();
            document.PrinterSettings = pageSetupDialog1.PrinterSettings;
            document.DefaultPageSettings = pageSetupDialog1.PageSettings;

            IEnumerator paperSizes = pageSetupDialog1.PrinterSettings.PaperSizes.GetEnumerator();
            paperSizes.Reset();

            for (int i = 0; i < pageSetupDialog1.PrinterSettings.PaperSizes.Count; ++i)
            {
                paperSizes.MoveNext();
                if (((PaperSize)paperSizes.Current).Kind == document.DefaultPageSettings.PaperSize.Kind)
                {
                    document.DefaultPageSettings.PaperSize = ((PaperSize)paperSizes.Current);
                }
            }
            IPaper paper = new PaperClass();            
            paper.Attach(pageSetupDialog1.PrinterSettings.GetHdevmode(pageSetupDialog1.PageSettings).ToInt32(), pageSetupDialog1.PrinterSettings.GetHdevnames().ToInt32());
            IPrinter printer = new EmfPrinterClass();
            printer.Paper = paper;
            axPageLayoutControl1.Printer = printer;
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            m_CurrentPrintPage = 0;
            if (axPageLayoutControl1.DocumentFilename == null) return;   
            document.DocumentName = axPageLayoutControl1.DocumentFilename;
            printPreviewDialog1.Document = document;
            printPreviewDialog1.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (axPageLayoutControl1.ActiveView.FocusMap.LayerCount == 0) {
                MessageBox.Show("No Map !");
                return;
            }
            if (rdoPrint.Checked)
            {
                printDialog1.AllowSomePages = true; //允许用户选择打印哪些页面
                printDialog1.ShowHelp = true;
                printDialog1.Document = document;
                DialogResult result = printDialog1.ShowDialog();
                if (result == DialogResult.OK) document.Print();
            }
            if (rdoOutput.Checked)
            {
                IExport export = null;
                string path = outputPath.Text;
                if (path == "无") {
                    MessageBox.Show("请先选择导出路径");
                    return;
                }
                export = getExportClass(path);
                if (export == null) {
                    MessageBox.Show("导出路径错误");
                    return;
                }
                export.ExportFileName = path;
                IEnvelope pEnvelope = axPageLayoutControl1.Extent;

                //导出参数           
                export.Resolution = 300;
                tagRECT exportRect = new tagRECT();
                exportRect.left = 0;
                exportRect.top = 0;
                //这里暂时固定大小
                exportRect.right = 1000;
                exportRect.bottom = 1300;
                IEnvelope envelope = new EnvelopeClass();
                //输出范围
                envelope.PutCoords(exportRect.left, exportRect.bottom, exportRect.right, exportRect.top);
                export.PixelBounds = envelope;
                //可用于取消操作
                ITrackCancel pCancel = new CancelTrackerClass();
                export.TrackCancel = pCancel;
                pCancel.Reset();
                //点击ESC键时，中止转出
                pCancel.CancelOnKeyPress = true;
                pCancel.CancelOnClick = false;
                pCancel.ProcessMessages = true;
                //获取handle
                int hDC = export.StartExporting();
                //开始转出
                axPageLayoutControl1.ActiveView.Output(hDC, (int)export.Resolution, ref exportRect, pEnvelope, pCancel);
                bool bContinue = pCancel.Continue();
                //捕获是否继续
                if (bContinue)
                {
                    export.FinishExporting();
                    export.Cleanup();
                    MessageBox.Show("导出至" + path);
                }
                else
                {
                    export.Cleanup();
                }
                bContinue = pCancel.Continue();

            }
        }
        /// <summary>
        /// 根据导出文件的路径export实例对象
        /// </summary>
        /// <returns></returns>
        private IExport getExportClass(string path)
        {            
            if (path == "无")
            {
                MessageBox.Show("请选择输出路径！");
                return null;
            }
            //根据给定的文件扩展名，来决定生成不同类型的对象
            IExport export = null;
            if (path.EndsWith(".jpg"))
            {
                export = new ExportJPEGClass();
            }
            else if (path.EndsWith(".tiff"))
            {
                export = new ExportTIFFClass();
            }
            else if (path.EndsWith(".bmp"))
            {
                export = new ExportBMPClass();
            }
            else if (path.EndsWith(".emf"))
            {
                export = new ExportEMFClass();
            }
            else if (path.EndsWith(".png"))
            {
                export = new ExportPNGClass();
            }
            else if (path.EndsWith(".gif"))
            {
                export = new ExportGIFClass();
            }
            return export;
        }
        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string sPageToPrinterMapping = (string)this.cbxMappingType.SelectedItem;
            if (sPageToPrinterMapping == null)
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
            else if (sPageToPrinterMapping.Equals("esriPageMappingTile"))
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
            else if (sPageToPrinterMapping.Equals("esriPageMappingCrop"))
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingCrop;
            else if (sPageToPrinterMapping.Equals("esriPageMappingScale"))
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale;
            else
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;

            short dpi = (short)e.Graphics.DpiX;
            IEnvelope devBounds = new EnvelopeClass();
            IPage page = axPageLayoutControl1.Page;
            short printPageCount = axPageLayoutControl1.get_PrinterPageCount(0);
            m_CurrentPrintPage++;
            IPrinter printer = axPageLayoutControl1.Printer;
            page.GetDeviceBounds(printer, m_CurrentPrintPage, 0, dpi, devBounds);
            tagRECT deviceRect;
            double xmin, ymin, xmax, ymax;
            devBounds.QueryCoords(out xmin, out ymin, out xmax, out ymax);
            deviceRect.bottom = (int)ymax;
            deviceRect.left = (int)xmin;
            deviceRect.top = (int)ymin;
            deviceRect.right = (int)xmax;
            IEnvelope visBounds = new EnvelopeClass();
            page.GetPageBounds(printer, m_CurrentPrintPage, 0, visBounds);
            IntPtr hdc = e.Graphics.GetHdc();
            axPageLayoutControl1.ActiveView.Output(hdc.ToInt32(), dpi, ref deviceRect, visBounds, m_TrackCancel);

            e.Graphics.ReleaseHdc(hdc);
            if (m_CurrentPrintPage < printPageCount)
                e.HasMorePages = true; //document_PrintPage event will be called again
            else
                e.HasMorePages = false;
        }        

        //private void cbxOutputType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbxOutputType.SelectedItem == null) return;
        //    strOutputType = cbxOutputType.SelectedItem.ToString();
        //    try
        //    {
        //        switch (strOutputType)
        //        {
        //            case "ExportBMP":
        //                export = new ExportBMPClass();
        //                fileExtension = "BMP";
        //                break;
        //            case "ExportGIF":  //ArcPressPrinter需要ArcGIS桌面许可
        //                export = new ExportGIFClass();
        //                fileExtension = "GIF";
        //                break;
        //            case "ExportJPEG":
        //                export = new ExportJPEGClass();
        //                fileExtension = "JPG";
        //                break;
        //            case "ExportPNG":
        //                export = new ExportPNGClass();
        //                fileExtension = "PNG";
        //                break;
        //            case "ExportTIFF":
        //                export = new ExportTIFFClass();
        //                fileExtension = "TIFF";
        //                break;
        //            case "ExportAI":
        //                export = new ExportAIClass();
        //                fileExtension = "AI";
        //                break;
        //            case "ExportEMF":
        //                export = new ExportEMFClass();
        //                fileExtension = "EMF";
        //                break;
        //            case "ExportPDF":
        //                export = new ExportPDFClass();
        //                fileExtension = "PDF";
        //                break;
        //            case "ExportPS":
        //                export = new ExportPSClass();
        //                fileExtension = "PS";
        //                break;
        //            case "ExportSVG":
        //                export = new ExportSVGClass();
        //                fileExtension = "SVG";
        //                break;
        //            default:
        //                break;
        //        }                
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btnOutputFile_Click(object sender, EventArgs e)
        {
            if (!rdoOutput.Checked) return;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "导出地图";
            saveFileDialog1.Filter = "(*.jpg)|*.jpg|(*.tiff)|*.tiff|(*.bmp)|*.bmp|(*.emf)|*.emf|(*.png)|*.png|(*.gif)|*.gif";//设置过滤属性
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;//未选择文件return
            string filePath = saveFileDialog1.FileName;//获取到文件路径
            if (filePath == "") return;
            this.outputPath.Text = filePath;
        }



    }
}
