using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System.Drawing.Drawing2D;
using ESRI.ArcGIS.Geodatabase;
using stdole;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.DisplayUI;

namespace Arcgis.View
{
    public partial class StatisticsSymbols : Form
    {
        IHookHelper m_hookHelper = null;
        IActiveView m_activeView = null;
        IMap m_map = null;

        IFeatureLayer layer2Symbolize = null;
        IColorRamp colorRamp = null;
        IEnumColors gEnumColors = null;
        ISymbol gBaseSymbol = null;

        IChartSymbol chartSymbol = null;

        double symbolSize = 32;
        string strSymbolizeMethod = "饼状图";

        System.Collections.Hashtable fieldSymbolHashTable = new System.Collections.Hashtable();

        public StatisticsSymbols(IHookHelper hookHelper)
        {
            InitializeComponent();
            m_hookHelper = hookHelper;
            m_activeView = m_hookHelper.ActiveView;
            m_map = m_hookHelper.FocusMap;
        }

        private void StatisticsSymbols_Load(object sender, EventArgs e)
        {
            CbxLayersAddItems();
            colorRamp = GetColorRamp(10);
            gEnumColors = colorRamp.Colors;
            gEnumColors.Reset();
            lvRendererFields.View = System.Windows.Forms.View.List;
        }

        private IColorRamp GetColorRamp(int colorSize)
        {
            IRandomColorRamp pColorRamp = new RandomColorRampClass();
            pColorRamp.StartHue = 0;  //0
            pColorRamp.EndHue = 360;     //360       
            pColorRamp.MinSaturation = 15;  //15
            pColorRamp.MaxSaturation = 30;   //30
            pColorRamp.MinValue = 99;    //99
            pColorRamp.MaxValue = 100;     //100          
            pColorRamp.Size = colorSize;
            bool ok = true;
            pColorRamp.CreateRamp(out ok);
            return pColorRamp as IColorRamp;
        }

        private void cbxLayers2Symbolize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLayers2Symbolize.SelectedItem != null)
            {
                string strLayer2Symbolize = cbxLayers2Symbolize.SelectedItem.ToString();
                layer2Symbolize = GetFeatureLayer(strLayer2Symbolize);
                lstSourceFieldsAdditems(layer2Symbolize);
                lvRendererFields.Items.Clear();
                fieldSymbolHashTable.Clear();
                if (layer2Symbolize.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    gBaseSymbol = new SimpleLineSymbolClass();
                    (gBaseSymbol as ISimpleLineSymbol).Color = getRGB(128, 128, 255);
                }
                if (layer2Symbolize.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    gBaseSymbol = new SimpleFillSymbolClass();
                    (gBaseSymbol as ISimpleFillSymbol).Color = getRGB(128, 128, 255);
                }
                if (layer2Symbolize.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint || layer2Symbolize.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                {
                    gBaseSymbol = new SimpleMarkerSymbolClass();
                  (gBaseSymbol as ISimpleMarkerSymbol ).Color = getRGB(128, 128, 255);
                    //btnSelectBackGroud.Enabled = false;
                }
                else
                    btnSelectBackGroud.Enabled = true;
            }
        }

        private void cbxSymbolizeMethds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSymbolizeMethds.SelectedItem != null)
            {
                strSymbolizeMethod = cbxSymbolizeMethds.SelectedItem.ToString();
                switch (strSymbolizeMethod)
                {
                    case "饼状图":
                        chartSymbol = new PieChartSymbolClass();
                        pictureBox1.BackgroundImage = backImageList.Images[0];
                        break;
                    case "柱状图":
                        chartSymbol = new BarChartSymbolClass();
                        pictureBox1.BackgroundImage = backImageList.Images[1];
                        break;
                    case "堆积柱状图":
                        chartSymbol = new StackedChartSymbolClass();
                        pictureBox1.BackgroundImage = backImageList.Images[2];
                        break;
                    default:
                        break;
                }
            }
        }

        private void lstSourceFieldsAdditems(IFeatureLayer featureLayer)
        {
            IFields fields = featureLayer.FeatureClass.Fields;
            lstSourceFields.Items.Clear();

            for (int i = 0; i < fields.FieldCount; i++)
            {
                if ((fields.get_Field(i).Type == esriFieldType.esriFieldTypeDouble) ||
                    (fields.get_Field(i).Type == esriFieldType.esriFieldTypeInteger) ||
                    (fields.get_Field(i).Type == esriFieldType.esriFieldTypeSingle) ||
                    (fields.get_Field(i).Type == esriFieldType.esriFieldTypeSmallInteger))
                {
                    lstSourceFields.Items.Add(fields.get_Field(i).Name);
                }
            }
        }

        private void CbxLayersAddItems()
        {
            if (GetLayers() == null) return;
            IEnumLayer layers = GetLayers();
            layers.Reset();
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer is IFeatureLayer)
                {
                    cbxLayers2Symbolize.Items.Add(layer.Name);
                }
                layer = layers.Next();
            }
        }

        private void btnSymbolize_Click(object sender, EventArgs e)
        {
            if (layer2Symbolize == null) return;
            if (fieldSymbolHashTable.Count < 2)
            {
                MessageBox.Show("统计符号化字段数应大于等于2，请增加用于符号化的字段！！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Renderer();
        }

        private void Renderer()
        {
            IGeoFeatureLayer pGeoFeatureL = (IGeoFeatureLayer)layer2Symbolize;
            IFeatureClass featureClass = pGeoFeatureL.FeatureClass;

            IChartRenderer renderer = CreateRenderer(featureClass);
            if (renderer == null) return;
            pGeoFeatureL.Renderer = (IFeatureRenderer)renderer;
            m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_activeView.Extent);
        }

        private double FindMaxValueInRendererFields(IFeatureClass featureClass)
        {
            ITable pTable = (ITable)featureClass;
            ICursor pCursor = pTable.Search(null, false);
            //Use the statistics objects to calculate the max value and the min value
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Cursor = pCursor;
            double gMaxValue = 0.0;
            foreach (System.Collections.DictionaryEntry de in fieldSymbolHashTable)
            {
                string strField = de.Key.ToString();
                //Set statistical field
                pDataStatistics.Field = strField;
                //Get the result of statistics
                IStatisticsResults pStatisticsResult = pDataStatistics.Statistics;
                double dmaxValue = pStatisticsResult.Maximum;
                if (dmaxValue > gMaxValue) gMaxValue = dmaxValue;
            }
            return gMaxValue;
        }

        private IChartRenderer CreateRenderer(IFeatureClass featureClass)
        {
            if (chartSymbol == null) return null;

            double dmaxValue = 0.0;
            dmaxValue = FindMaxValueInRendererFields(featureClass);
            chartSymbol.MaxValue = dmaxValue;

            IMarkerSymbol pMarkerSymbol = (IMarkerSymbol)chartSymbol;
            pMarkerSymbol.Size = symbolSize;
            //Now set up symbols for each bar
            ISymbolArray pSymbolArray = (ISymbolArray)chartSymbol;
            pSymbolArray.ClearSymbols();
            IChartRenderer pChartRenderer = new ChartRendererClass();
            IRendererFields pRendererFields = (IRendererFields)pChartRenderer;
            foreach (System.Collections.DictionaryEntry de in fieldSymbolHashTable)
            {
                string strField = de.Key.ToString(); ;
                ISymbol symbol = de.Value as ISymbol;
                pRendererFields.AddField(strField, strField);
                pSymbolArray.AddSymbol(symbol);
            }

            pChartRenderer.ChartSymbol = chartSymbol;
            pChartRenderer.Label = "统计符号化";
            pChartRenderer.UseOverposter = false;
          
            if (gBaseSymbol != nudDotSize)
                pChartRenderer.BaseSymbol = gBaseSymbol;
            pChartRenderer.CreateLegend();

            return pChartRenderer;
        }

        private void btnSelectColorRamp_Click(object sender, EventArgs e)
        {
            ColorRampForm colorRampForm = new ColorRampForm();
            colorRampForm.ShowDialog();
            colorRamp = colorRampForm.m_styleGalleryItem.Item as IColorRamp;
            colorRampForm.Dispose();
            if (colorRamp == null) return;
            gEnumColors = colorRamp.Colors;
            gEnumColors.Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IChartSymbolEditor chartSymbolEditor = new ChartSymbolEditorClass();
                chartSymbolEditor.EditChartSymbol(ref chartSymbol, 0);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region "GetLayers"
        private IEnumLayer GetLayers()
        {
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";// IFeatureLayer
            //uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}";  // IGeoFeatureLayer
            //uid.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}";  // IDataLayer
            if (m_map.LayerCount != 0)
            {
                IEnumLayer layers = m_map.get_Layers(uid, true);
                return layers;
            }
            return null;
        }
        #endregion

        #region "GetFeatureLayer"
        private IFeatureLayer GetFeatureLayer(string layerName)
        {
            //get the layers from the maps
            if (GetLayers() == null) return null;
            IEnumLayer layers = GetLayers();
            layers.Reset();

            ILayer layer = null;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == layerName)
                    return layer as IFeatureLayer;
            }
            return null;
        }

        #endregion

        private void btnSelectBackGroud_Click(object sender, EventArgs e)
        {
            if (layer2Symbolize == null) return;
            gBaseSymbol = GetSymbolBySymbolSelector(layer2Symbolize.FeatureClass.ShapeType);
        }

        public IRgbColor getRGB(int yourRed, int yourGreen, int yourBlue)
        {
            IRgbColor pRGB;
            pRGB = new RgbColorClass();
            pRGB.Red = yourRed;
            pRGB.Green = yourGreen;
            pRGB.Blue = yourBlue;
            pRGB.UseWindowsDithering = true;
            return pRGB;

        }

        private IColor GetColorByColorBrowser()
        {
            IColor pNewColor;

            IColor pInitColor = new RgbColorClass();
            pInitColor.RGB = 255;
            IColorBrowser pColorBrowser = new ColorBrowserClass();
            pColorBrowser.Color = pInitColor;
            bool bColorSet = pColorBrowser.DoModal(0);
            if (bColorSet)
            {
                pNewColor = pColorBrowser.Color;
                return pNewColor;
            }
            else return pInitColor;
        }

        private IColor GetColorByColorSelector()
        {
            //Set the initial color to be diaplyed when the dialog opens
            IColor pColor;
            pColor = new RgbColorClass();
            pColor.RGB = 255;

            IColorSelector pSelector;
            pSelector = new ColorSelectorClass();
            pSelector.Color = pColor;

            // Display the dialog
            if (pSelector.DoModal(0))
            {
                IColor pOutColor;
                pOutColor = pSelector.Color;
                return pOutColor;
            }
            else return pColor;
        }

        private IColor GetColorByColorPalette(int left, int top)
        {
            IColor pColor;

            pColor = new RgbColorClass();
            pColor.RGB = 255;

            IColorPalette pPalette;
            pPalette = new ColorPaletteClass();

            tagRECT pRect = new tagRECT();
            pRect.left = left;
            pRect.top = top;

            pPalette.TrackPopupMenu(ref pRect, pColor, false, 0);
            pColor = pPalette.Color;
            return pColor;
        }

        private void nudDotSize_ValueChanged(object sender, EventArgs e)
        {
            symbolSize = Convert.ToDouble(nudDotSize.Value);
        }

        //private ISymbol GetSimpleFillSymbol()
        //{
        //    ISymbol symbol = null;
        //    IColor pColor = gEnumColors.Next();
        //    ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
        //    simpleFillSymbol.Color = pColor;
        //    symbol = simpleFillSymbol as ISymbol;
        //    return symbol;
        //}

        private void lstSourceFields_DoubleClick(object sender, EventArgs e)
        {
            System.Object selectedItem = lstSourceFields.SelectedItem;
            if (selectedItem != null)
            {
                lstSourceFields.Items.Remove(selectedItem);
                lvRendererFieldsAddItemWithSymbol(selectedItem);
            }
        }

        private void lvRendererFieldsAddItemWithSymbol(System.Object selectedItem)
        {
            IStyleGalleryItem styleItem = GetSymbolBySymbologyControl("Fill Symbols");
            if (styleItem == null) return;

            ISymbol symbol = styleItem.Item as ISymbol;
            if (symbol == null) return;

            //symbolPBox.Visible = true;
            IStyleGalleryClass mStyleClass = new FillSymbolStyleGalleryClassClass();
            Bitmap image = StyleGalleryItemToBmp(24, 24, mStyleClass, styleItem);
            //Bitmap image = DrawToPictureBox(symbol, symbolPBox);
            int currentIdx = Largeimage.Images.Count;
            currentIdx = Smallimage.Images.Count;
            Largeimage.Images.Add(image);
            Smallimage.Images.Add(image);
            ListViewItem newItem = new ListViewItem();
            newItem.ImageIndex = currentIdx;
            newItem.Text = selectedItem.ToString();
            lvRendererFields.Items.Add(newItem);
            lvRendererFields.Refresh();
            if (fieldSymbolHashTable.ContainsKey(selectedItem.ToString()))
            {
                fieldSymbolHashTable.Remove(selectedItem.ToString());
            }
            fieldSymbolHashTable.Add(selectedItem.ToString(), symbol);
            //symbolPBox.Visible = false;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            System.Object selectedItem = lstSourceFields.SelectedItem;
            if (selectedItem != null)
            {
                lstSourceFields.Items.Remove(selectedItem);
                lvRendererFieldsAddItemWithSymbol(selectedItem);
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = lvRendererFields.FocusedItem;
            if (selectedItem != null)
            {
                lvRendererFields.Items.Remove(selectedItem);
                lstSourceFields.Items.Add(selectedItem.Text);
                fieldSymbolHashTable.Remove(selectedItem.Text);
            }
        }

        private void btnAllOut_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvRendererFields.Items)
            {
                lstSourceFields.Items.Add(item.Text);
            }
            lvRendererFields.Items.Clear();
            fieldSymbolHashTable.Clear();
        }

        private ISymbol GetSymbolBySymbolSelector(esriGeometryType geometryType)
        {
            try
            {
                ISymbolSelector pSymbolSelector = new SymbolSelectorClass();
                ISymbol symbol = null;
                switch (geometryType)
                {
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                        symbol = new SimpleMarkerSymbolClass();
                        break;
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                        symbol = new SimpleLineSymbolClass();
                        break;
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                        symbol = new SimpleFillSymbolClass();
                        break;
                    default:
                        break;
                }
                pSymbolSelector.AddSymbol(symbol);
                bool response = pSymbolSelector.SelectSymbol(0);
                if (response)
                {
                    symbol = pSymbolSelector.GetSymbolAt(0);
                    return symbol;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //自定义
                ISymbol symbol = null;
                switch (geometryType)
                {               
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                        symbol = new SimpleMarkerSymbolClass();
                        break;
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                        symbol = new SimpleLineSymbolClass();
                        break;
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                        symbol = new SimpleFillSymbolClass();
                        break;
                    default:
                        break;
                }
                return symbol;
            }
        }

            private IStyleGalleryItem GetSymbolBySymbologyControl(string styleClass)
            {
                SelectSymbolByControl symbolForm = new SelectSymbolByControl(styleClass);
                symbolForm.ShowDialog();
                IStyleGalleryItem styleItem = symbolForm.m_styleGalleryItem;
                symbolForm.Dispose();

                return styleItem;
            }

            private void lvRendererFields_DoubleClick(object sender, EventArgs e)
            {
                ListViewItem selectedItem = lvRendererFields.FocusedItem;
                if (selectedItem == null) return;
                int imgIdx = selectedItem.ImageIndex;

                //ISymbol symbol = GetSymbolBySymbolSelector(esriGeometryType.esriGeometryPolygon);
                IStyleGalleryItem styleItem = GetSymbolBySymbologyControl("Fill Symbols");
                if (styleItem == null) return;

                ISymbol symbol = styleItem.Item as ISymbol;
                if (symbol == null) return;

                //symbolPBox.Visible = true;
                IStyleGalleryClass mStyleClass = new FillSymbolStyleGalleryClassClass();
                Bitmap image = StyleGalleryItemToBmp(24, 24, mStyleClass, styleItem);
                //Bitmap image = DrawToPictureBox(symbol, symbolPBox);
                Largeimage.Images[imgIdx] = image;
                Smallimage.Images[imgIdx] = image;
                lvRendererFields.Refresh();
                if (fieldSymbolHashTable.ContainsKey(selectedItem.Text))
                {
                    fieldSymbolHashTable.Remove(selectedItem.Text);
                }
                fieldSymbolHashTable.Add(selectedItem.Text, symbol);
            }

            public Bitmap StyleGalleryItemToBmp(int iWidth, int iHeight, IStyleGalleryClass mStyleGlyCs, IStyleGalleryItem mStyleGlyItem)
            {
                //建立符合规格的内存图片
                Bitmap bmp = new Bitmap(iWidth, iHeight);
                Graphics gImage = Graphics.FromImage(bmp);
                //建立对应的符号显示范围
                tagRECT rect = new tagRECT();
                rect.right = bmp.Width;
                rect.bottom = bmp.Height;
                //生成预览
                System.IntPtr hdc = new IntPtr();
                hdc = gImage.GetHdc();
                //在图片上绘制符号
                mStyleGlyCs.Preview(mStyleGlyItem.Item, hdc.ToInt32(), ref rect);
                gImage.ReleaseHdc(hdc);
                gImage.Dispose();
                return bmp;
            }

            private Bitmap DrawToPictureBox(ISymbol pSym, PictureBox pBox)
            {
                IGeometry pGeometry = null;
                System.Drawing.Graphics pGraphics = null;
                pGraphics = System.Drawing.Graphics.FromHwnd(pBox.Handle);
                pGraphics.FillRectangle(System.Drawing.Brushes.White, pBox.ClientRectangle);
                Bitmap image = new Bitmap(pBox.Width, pBox.Height, pGraphics);
                IEnvelope pEnvelope = new EnvelopeClass();

                if (pSym is IMarkerSymbol)
                {
                    IPoint pPoint = new PointClass();      //the geometry of a MarkerSymbol
                    pPoint.PutCoords(pBox.Width / 2, pBox.Height / 2);       //center in middle of pBox
                    pGeometry = pPoint;
                }
                if (pSym is ILineSymbol)
                {
                    pEnvelope.PutCoords(0, pBox.Height / 2, pBox.Width, pBox.Height / 2);
                    IPolyline pPolyline = new PolylineClass();
                    pPolyline.FromPoint = pEnvelope.LowerLeft;
                    pPolyline.ToPoint = pEnvelope.UpperRight;
                    pGeometry = pPolyline;
                }
                if (pSym is IFillSymbol)
                {
                    pEnvelope.PutCoords(1, 1, pBox.Width - 1, pBox.Height - 1);
                    pGeometry = pEnvelope;

                    if (pSym is IMultiLayerFillSymbol)
                    {
                        IMultiLayerFillSymbol pMultiLayerFillSymbol = pSym as IMultiLayerFillSymbol;
                        //For mLayers As Integer = 0 To pMultiLayerFillSymbol.LayerCount - 1
                        for (int i = 0; i < pMultiLayerFillSymbol.LayerCount; i++)
                        {
                            if (pMultiLayerFillSymbol.get_Layer(i) is IPictureFillSymbol)
                            {
                                IPictureFillSymbol pPictureFillSymbol = pMultiLayerFillSymbol.get_Layer(0) as IPictureFillSymbol;
                                image = Bitmap.FromHbitmap(new IntPtr(pPictureFillSymbol.Picture.Handle));
                                //m.MakeTransparent(System.Drawing.ColorTranslator.FromOle(pPictureFillSymbol.Color.RGB))
                                return image;
                            }
                            else if (pMultiLayerFillSymbol.get_Layer(i) is ISimpleFillSymbol)
                            {
                                pEnvelope.PutCoords(1, 1, pBox.Width - 1, pBox.Height - 1);
                                pGeometry = pEnvelope;
                            }
                            else if (pMultiLayerFillSymbol.get_Layer(i) is IGradientFillSymbol)
                            {
                                IGradientFillSymbol pGradientFillSymbol = pMultiLayerFillSymbol.get_Layer(0) as IGradientFillSymbol;
                                IAlgorithmicColorRamp pRamp = pGradientFillSymbol.ColorRamp as IAlgorithmicColorRamp;
                                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), new System.Drawing.Size(pBox.Width, pBox.Height));
                                LinearGradientBrush lgBrush = new LinearGradientBrush(rect,
                                    System.Drawing.ColorTranslator.FromOle(pRamp.FromColor.RGB),
                                    System.Drawing.ColorTranslator.FromOle(pRamp.ToColor.RGB),
                                    45);
                                pGraphics.FillRectangle(lgBrush, rect);
                                rect.Width = rect.Width - 1;
                                rect.Height = rect.Height - 1;
                                pGraphics.DrawRectangle(new Pen(ColorTranslator.FromOle(pGradientFillSymbol.Outline.Color.RGB),
                                    (float)pGradientFillSymbol.Outline.Width), rect);
                                image = new Bitmap(pBox.Width, pBox.Height, pGraphics);
                            }
                        }
                    }
                    else if (pSym is IPictureFillSymbol)
                    {
                        IPictureFillSymbol pPictureFillSymbol = pSym as IPictureFillSymbol;
                        image = Bitmap.FromHbitmap(new IntPtr(pPictureFillSymbol.Picture.Handle));
                        //m.MakeTransparent(System.Drawing.ColorTranslator.FromOle(pPictureFillSymbol.Color.RGB))
                        return image;
                    }
                }

                IntPtr hDC = GetDC(pBox.Handle);
                pSym.SetupDC(hDC.ToInt32(), null);
                pSym.ROP2 = esriRasterOpCode.esriROPCopyPen;
                if (pGeometry != null) pSym.Draw(pGeometry);
                pSym.ResetDC();

                Graphics g2 = Graphics.FromImage(image);
                //获得屏幕的句柄 
                IntPtr dc3 = pGraphics.GetHdc();
                //获得位图的句柄 
                IntPtr dc2 = g2.GetHdc();
                BitBlt(dc2, 0, 0, pBox.Width, pBox.Height, dc3, 0, 0, SRCCOPY);
                pGraphics.ReleaseHdc(dc3);//释放屏幕句柄             
                g2.ReleaseHdc(dc2);//释放位图句柄             
                //image.Save("c:\\MyJpeg.Icon", ImageFormat.Bmp);
                return image;
            }

            private void demo2(string CurrentStyleFile, string CurrentStyleGalleryClass)
            {
                IStyleGallery pStyleGallery = new ServerStyleGalleryClass();
                IStyleGalleryStorage pStyleGalleryStorage;
                pStyleGalleryStorage = pStyleGallery as IStyleGalleryStorage;
                //增加符号文件
                pStyleGalleryStorage.AddFile(CurrentStyleFile);
                //根据当前符号的类别和文件得到符号的枚举循环
                //符号类别包括Fill Symbol,Line Symbol等
                IEnumStyleGalleryItem mEnumStyleItem;
                mEnumStyleItem = pStyleGallery.get_Items(CurrentStyleGalleryClass, CurrentStyleFile, "");
                //得到符号文件类别的各个条目,增加到一个Combox中
                IEnumBSTR pEnumBSTR = pStyleGallery.get_Categories(CurrentStyleGalleryClass);
                pEnumBSTR.Reset();
                string Category = "";
                Category = pEnumBSTR.Next();
                while (Category != null)
                {
                    //cbxCategory.Items.Add(Category);
                    Category = pEnumBSTR.Next();
                }
                //得到各个符号并转化为图片
                mEnumStyleItem.Reset();
                IStyleGalleryItem mStyleItem = mEnumStyleItem.Next();
                int ImageIndex = 0;
                IStyleGalleryClass mStyleClass = new FillSymbolStyleGalleryClassClass();
                while (mStyleItem != null)
                {
                    //调用另一个类的方法将符号转化为图片
                    Bitmap bmpB = StyleGalleryItemToBmp(32, 32, mStyleClass, mStyleItem);
                    Bitmap bmpS = StyleGalleryItemToBmp(16, 16, mStyleClass, mStyleItem);
                    Largeimage.Images.Add(bmpB);
                    Smallimage.Images.Add(bmpS);
                    ListViewItem lvItem = new ListViewItem(new string[] { mStyleItem.Name, mStyleItem.ID.ToString(), mStyleItem.Category }, ImageIndex);
                    this.lvRendererFields.Items.Add(lvItem);
                    mStyleItem = mEnumStyleItem.Next();
                    ImageIndex++;
                }
                //必须采用此方式进行释放，第二次才能正常读取
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mEnumStyleItem);
            }


            public const int HORZSIZE = 4;
            public const int VERTSIZE = 6;
            public const int HORZRES = 8;
            public const int VERTRES = 10;
            public const int ASPECTX = 40;
            public const int ASPECTY = 42;
            public const int LOGPIXELSX = 88;
            public const int LOGPIXELSY = 90;
            public const int SRCCOPY = 13369376;
            //public const long SRCCOPY = 0xCC0020;

            public struct GUID
            {
                public int Data1;
                public int Data2;
                public int Data3;
                public byte[] Data4;
            }

            public struct PicDesc
            {
                public int SIZE;
                public int Type;
                public int hBmp;
                public int hPal;
                public int intReserved;
            }

            public struct RECT
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }

            [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
            public static extern IntPtr DeleteDC(IntPtr hDc);

            [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
            public static extern IntPtr DeleteObject(IntPtr hDc);

            [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
            public static extern bool BitBlt(IntPtr hdcDest, int xDest,
                       int yDest, int wDest, int hDest, IntPtr hdcSource,
                       int xSrc, int ySrc, int RasterOp);

            [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc,
                       int nWidth, int nHeight);

            [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

            [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
            public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);


            [DllImport("gdi32")]
            public static extern int CreateCompatibleDC(int hDC);
            [DllImport("gdi32")]
            public static extern int CreateCompatibleBitmap(int hDC, int hObject);
            [DllImport("gdi32")]
            public static extern int CreateCompatibleBitmap(int hDC, int width, int hight);
            [DllImport("gdi32")]
            public static extern int DeleteDC(int hDC);
            [DllImport("gdi32")]
            public static extern int SelectObject(int hDC, int hObject);
            [DllImport("gdi32")]
            public static extern int CreatePen(int nPenStyle, int nWidth, int crColor);
            [DllImport("gdi32")]
            public static extern int CreateSolidBrush(long crColor);
            [DllImport("gdi32")]
            public static extern int DeleteObject(int hObject);

            [DllImport("GDI32.dll")]
            public static extern int GetDeviceCaps(int hdc, int nIndex);

            [DllImport("gdi32.dll")]
            private static extern int CreateDC(
            string lpszDriver, // 驱动名称 
            string lpszDevice, // 设备名称 
            string lpszOutput, // 无用，可以设定位"NULL" 
            int lpInitData // 任意的打印机数据 
            );


            [DllImportAttribute("olepro32.dll")]
            public static extern int OleCreatePictureIndirect(PicDesc pDesc, GUID RefIID, int fPictureOwnsHandle, IPicture pPic);

            [DllImport("User32.dll")]
            public static extern IntPtr GetDC(IntPtr hWnd);
            [DllImport("User32.dll")]
            public static extern int ReleaseDC(int hWnd, int hDC);
            [DllImport("User32.dll")]
            public static extern int FillRect(int hdc, RECT lpRect, int hBrush);
            [DllImport("User32.dll")]
            public static extern int IsRectEmpty(RECT lpRect);
            [DllImport("user32.dll", SetLastError = true)]
            static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref int pvParam, uint fWinIni);
            [DllImport("user32.dll")]
            public static extern int GetClientRect(int hWnd, out RECT lpRect);
            [DllImport("user32.dll")]
            public static extern int GetWindowRect(int hwnd, out RECT lpRect);

       
    }

}
