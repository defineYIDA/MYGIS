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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Arcgis.View
{
    public partial class ClassBreaksRendererSymbol : Form
    {
        IHookHelper m_hookHelper = null;
        IActiveView m_activeView = null;
        IMap m_map = null;

        IFeatureLayer layer2Symbolize = null;
        string strRendererField = string.Empty;
        string strNormalizeField = string.Empty;
        string strClassifyMethod = string.Empty;
        double dblStdDevInterval = 0.0;
        double dblDefinedInterval = 0.0;
        int classCount = 5;
        private IClassBreaksRenderer m_classBreaksRenderer;

        IStyleGallery pStyleGlry = new ServerStyleGalleryClass();
        IColorRamp colorRamp = null;
        ISymbol gloabalSymbol = null;


        public ClassBreaksRendererSymbol(IHookHelper hookHelper)
        {
            InitializeComponent();
            m_hookHelper = hookHelper;
            m_activeView = m_hookHelper.ActiveView;
            m_map = m_hookHelper.FocusMap;     
        }

        private void ClassBreaksRendererSymbol_Load(object sender, EventArgs e)
        {
            CbxLayersAddItems();
            lblInterval.Visible = false;
            txtInterval.Visible = false;
            cbxInterval.Visible = false;
        }

        private void cbxLayers2Symbolize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLayers2Symbolize.SelectedItem != null)
            {
                string strLayer2Symbolize = cbxLayers2Symbolize.SelectedItem.ToString();
                layer2Symbolize = GetFeatureLayer(strLayer2Symbolize);
                CbxFieldAdditems(layer2Symbolize);
                strRendererField = cbxFields.Items[0].ToString();
            }
        }

        private void CbxFieldAdditems(IFeatureLayer featureLayer)
        {
            if (featureLayer == null) return;
            IFields fields = featureLayer.FeatureClass.Fields;
            cbxFields.Items.Clear();
            cbxNormalization.Items.Clear();
            cbxNormalization.Items.Add("None");

            for (int i = 0; i < fields.FieldCount; i++)
            {
                if ((fields.get_Field(i).Type == esriFieldType.esriFieldTypeDouble) ||
                    (fields.get_Field(i).Type == esriFieldType.esriFieldTypeInteger) ||
                    (fields.get_Field(i).Type == esriFieldType.esriFieldTypeSingle) ||
                    (fields.get_Field(i).Type == esriFieldType.esriFieldTypeSmallInteger))
                {
                    cbxFields.Items.Add(fields.get_Field(i).Name);
                    cbxNormalization.Items.Add(fields.get_Field(i).Name);
                }

            }
            cbxFields.SelectedIndex = 0;
            cbxNormalization.SelectedIndex = 0;
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

            Renderer();
        }

        private void Renderer()
        {
            IGeoFeatureLayer pGeoFeatureL = (IGeoFeatureLayer)layer2Symbolize;
            IFeatureClass featureClass = pGeoFeatureL.FeatureClass;

            //找出rendererField在字段中的编号
            int lfieldNumber = featureClass.FindField(strRendererField);
            if (lfieldNumber == -1)
            {
                MessageBox.Show("Can't find field called " + strRendererField);
                return;
            }
            m_classBreaksRenderer = CreateClassBreaksRenderer(featureClass, strClassifyMethod);
            if (m_classBreaksRenderer == null) return;
            pGeoFeatureL.Renderer = (IFeatureRenderer)m_classBreaksRenderer;
            m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_activeView.Extent);
        }

        private IClassBreaksRenderer CreateClassBreaksRenderer(IFeatureClass featureClass, string breakMethod)
        {
            ITable pTable = (ITable)featureClass;

            //从pTable的strRendererField字段中得到信息给datavalues和datafrequency两个数组
            ITableHistogram pTableHistogram = new BasicTableHistogramClass();
            IBasicHistogram pHistogram = (IBasicHistogram)pTableHistogram;
            pTableHistogram.Field = strRendererField;
            if (strNormalizeField.ToLower() != "none")
                pTableHistogram.NormField = strNormalizeField;
            pTableHistogram.Table = pTable;
            object dataFrequency;
            object dataValues;
            pHistogram.GetHistogram(out dataValues, out dataFrequency);

            //下面是分级方法，用于根据获得的值计算得出符合要求的数据
            //根据条件计算出Classes和ClassesCount，numDesiredClasses为预定的分级数目
            IClassifyGEN pClassify = null;
            int numDesiredClasses = classCount;
            switch (breakMethod)
            {
                case "等间隔":
                    pClassify = new EqualIntervalClass();
                    break;
                case "已定义的间隔":
                    pClassify = new DefinedIntervalClass();
                    IIntervalRange2 intervalRange = pClassify as IIntervalRange2;
                    intervalRange.IntervalRange = dblDefinedInterval;
                    break;
                case "分位数":
                    pClassify = new QuantileClass();
                    break;
                case "自然裂点":
                    pClassify = new NaturalBreaksClass();
                    break;
                case "标准差":
                    pClassify = new StandardDeviationClass();
                    IStatisticsResults pStatRes = pHistogram as IStatisticsResults;
                    IDeviationInterval pStdDev = pClassify as IDeviationInterval;
                    pStdDev.Mean = pStatRes.Mean;
                    pStdDev.StandardDev = pStatRes.StandardDeviation;
                    pStdDev.DeviationInterval = dblStdDevInterval;
                    break;
                default:
                    break;
            }

            if (pClassify == null) return null;
            pClassify.Classify(dataValues, dataFrequency, ref numDesiredClasses);
            //返回一个数组
            double[] classBreaks = (double[])pClassify.ClassBreaks;
            int ClassesCount = classBreaks.GetUpperBound(0);
            nudClassCount.Value = ClassesCount;
            IClassBreaksRenderer pClassBreaksRenderer = new ClassBreaksRendererClass();
            pClassBreaksRenderer.Field = strRendererField;
            if (strNormalizeField.ToLower() != "none")
                pClassBreaksRenderer.NormField = strNormalizeField;
            //设置着色对象的分级数目
            pClassBreaksRenderer.BreakCount = ClassesCount;
            pClassBreaksRenderer.SortClassesAscending = true;
            if (colorRamp == null)
            {
                MessageBox.Show("请先选择色带！！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            //通过色带设置各级分类符号的颜色
            colorRamp.Size = ClassesCount;
            bool createRamp;
            colorRamp.CreateRamp(out createRamp);
            IEnumColors enumColors = colorRamp.Colors;
            enumColors.Reset();
            IColor pColor = null;
            ISymbol symbol = null;
            if (gloabalSymbol == null)
            {
                MessageBox.Show("请选择符号...", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            //需要注意的是分级着色对象中的symbol和break的下标都是从0开始
            for (int i = 0; i < ClassesCount; i++)
            {
                pColor = enumColors.Next();
                switch (featureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        IMarkerSymbol markerSymbol = gloabalSymbol as IMarkerSymbol;
                        markerSymbol.Color = pColor;
                        symbol = markerSymbol as ISymbol;
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        ILineSymbol lineSymbol = gloabalSymbol as ILineSymbol;
                        lineSymbol.Color = pColor;
                        symbol = lineSymbol as ISymbol;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        IFillSymbol fillSymbol = gloabalSymbol as IFillSymbol;
                        fillSymbol.Color = pColor;
                        symbol = fillSymbol as ISymbol;
                        break;
                    default:
                        break;
                }
                pClassBreaksRenderer.set_Symbol(i, symbol);
                pClassBreaksRenderer.set_Break(i, classBreaks[i + 1]);
            }
            return pClassBreaksRenderer;
        }

        private void btnSelectColorRamp_Click(object sender, EventArgs e)
        {
            ColorRampForm colorRampForm = new ColorRampForm();
            colorRampForm.ShowDialog();
            colorRamp = colorRampForm.m_styleGalleryItem.Item as IColorRamp;
            colorRampForm.Dispose();
        }

        private void btnSelectSymbol_Click(object sender, EventArgs e)
        {
            gloabalSymbol = GetSymbolByControl(layer2Symbolize.FeatureClass.ShapeType);
        }

        private ISymbol GetSymbolByControl(esriGeometryType geometryType)
        {
            GetSymbolByControl symbolForm = null;
            ISymbol symbol = null;
            switch (geometryType)
            {
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    symbolForm = new GetSymbolByControl(esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    symbolForm = new GetSymbolByControl(esriSymbologyStyleClass.esriStyleClassLineSymbols);
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    symbolForm = new GetSymbolByControl(esriSymbologyStyleClass.esriStyleClassFillSymbols);
                    break;
                default:
                    break;
            }
            symbolForm.ShowDialog();
            if (symbolForm.m_styleGalleryItem != null)
                symbol = symbolForm.m_styleGalleryItem.Item as ISymbol;
            symbolForm.Dispose();
            return symbol;
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

        private void cbxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFields.SelectedItem != null)
            {
                strRendererField = cbxFields.SelectedItem.ToString();
                //Find the selected field in the feature layer
                IFeatureClass featureClass = layer2Symbolize.FeatureClass;
                IField field = featureClass.Fields.get_Field(featureClass.FindField(strRendererField));

                //Get a feature cursor
                ICursor cursor = (ICursor)layer2Symbolize.Search(null, false);

                //Create a DataStatistics object and initialize properties
                IDataStatistics dataStatistics = new DataStatisticsClass();
                dataStatistics.Field = field.Name;
                dataStatistics.Cursor = cursor;
                //Get the result statistics
                IStatisticsResults statisticsResults = dataStatistics.Statistics;

                //Set the values min min and max values
                txtMinValue.Text = statisticsResults.Minimum.ToString();
                txtMaxValue.Text = statisticsResults.Maximum.ToString();
            }
        }

        private void cbxNormalization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxNormalization.SelectedItem != null)
            {
                strNormalizeField = cbxNormalization.SelectedItem.ToString();
            }
        }

        private void initialClassify()
        {
            if (layer2Symbolize == null) return;
            IFeatureClass featureClass = layer2Symbolize.FeatureClass;
            ITable pTable = (ITable)featureClass;
            ITableHistogram pTableHistogram = new BasicTableHistogramClass();
            IBasicHistogram pHistogram = (IBasicHistogram)pTableHistogram;
            pTableHistogram.Field = strRendererField;
            if (strNormalizeField.ToLower() != "none")
                pTableHistogram.NormField = strNormalizeField;
            pTableHistogram.Table = pTable;
            object dataFrequency;
            object dataValues;
            pHistogram.GetHistogram(out dataValues, out dataFrequency);

            IClassifyGEN pClassify = null;
            int numDesiredClasses = classCount;
            pClassify = new EqualIntervalClass();
            pClassify.Classify(dataValues, dataFrequency, ref numDesiredClasses);
            double[] clsbreaks = (double[])pClassify.ClassBreaks;
            double dblInterval = clsbreaks[1] - clsbreaks[0];
            txtInterval.Text = dblInterval.ToString();

        }

        private void cbxClassifyMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxClassifyMethods.SelectedItem != null)
            {
                strClassifyMethod = cbxClassifyMethods.SelectedItem.ToString();
                if (strClassifyMethod == "已定义的间隔")
                {
                    lblInterval.Visible = true;
                    lblInterval.Text = "间隔大小：";
                    txtInterval.Visible = true;
                    txtInterval.Enabled = true;
                    initialClassify();
                }
                else if (strClassifyMethod == "标准差")
                {
                    lblInterval.Visible = true;
                    lblInterval.Text = "间隔大小：";
                    cbxInterval.Visible = true;
                    cbxInterval.Enabled = true;
                }
                else
                {
                    lblInterval.Visible = false;
                    lblInterval.Text = "";
                    txtInterval.Visible = false;
                    txtInterval.Enabled = false;
                    cbxInterval.Visible = false;
                    cbxInterval.Enabled = false;
                }
            }
        }

        private void cbxInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxInterval.SelectedItem != null)
            {
                string strStdDevInterval = cbxInterval.SelectedItem.ToString();
                switch (strStdDevInterval)
                {
                    case "1个标准差":
                        dblStdDevInterval = 1.0;
                        break;
                    case "1/2个标准差":
                        dblStdDevInterval = 0.5;
                        break;
                    case "1/3个标准差":
                        dblStdDevInterval = 1.0 / 3.0;
                        break;
                    case "1/4个标准差":
                        dblStdDevInterval = 0.25;
                        break;
                    default:
                        break;
                }
            }
        }

        public bool IsInteger(string s)
        {
            try
            {
                Int32.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }




        private void txtInterval_TextChanged(object sender, EventArgs e)
        {
            string strDefinedInterval = txtInterval.Text;
            if (IsDouble(strDefinedInterval))
                dblDefinedInterval = Double.Parse(strDefinedInterval);
            else
            {
                System.Windows.Forms.MessageBox.Show("Interval size must be a numeric!");
                txtInterval.Focus();
            }
        }

        public bool IsDouble(string s)
        {
            try
            {
                Double.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void nudClassCount_ValueChanged(object sender, EventArgs e)
        {
            classCount = Convert.ToInt32(nudClassCount.Value);
        }        

    }
}
