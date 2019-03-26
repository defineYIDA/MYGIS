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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.Geodatabase;

namespace Arcgis.View
{
    public partial class GraduatedSymbols : Form
    {
        IHookHelper m_hookHelper = null;
        IActiveView m_activeView = null;
        IMap m_map = null;

        IFeatureLayer layer2Symbolize = null;
        string strRendererField = string.Empty;
        string strNormalizeField = string.Empty;

        int gClassCount = 5;
        double[] gClassbreaks;
        string strClassifyMethod = "自然裂点分类";
        private IClassBreaksRenderer m_classBreaksRenderer;

        IStyleGallery pStyleGlry = new ServerStyleGalleryClass();
        IMarkerSymbol markerSymbol = null;
        IFillSymbol fillSymbol = null;
        double minSize = 4;
        double maxSize = 18;

        public GraduatedSymbols(IHookHelper hookHelper)
        {
            InitializeComponent();
            m_hookHelper = hookHelper;
            m_activeView = m_hookHelper.ActiveView;
            m_map = m_hookHelper.FocusMap;
        }

        private void GraduatedSymbols_Load(object sender, EventArgs e)
        {
            CbxLayersAddItems();
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

            m_classBreaksRenderer = CreateClassBreaksRenderer(featureClass);
            if (m_classBreaksRenderer == null) return;
            pGeoFeatureL.Renderer = (IFeatureRenderer)m_classBreaksRenderer;
            m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_activeView.Extent);
        }

        private IClassBreaksRenderer CreateClassBreaksRenderer(IFeatureClass featureClass)
        {
            classify();
            if (gClassbreaks == null)
            {
                MessageBox.Show("请先分类...", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            int ClassesCount = gClassbreaks.GetUpperBound(0);
            if (ClassesCount == 0) return null;
            nudClassCount.Value = ClassesCount;
            IClassBreaksRenderer pClassBreaksRenderer = new ClassBreaksRendererClass();
            pClassBreaksRenderer.Field = strRendererField;
            if (strNormalizeField.ToLower() != "none")
                pClassBreaksRenderer.NormField = strNormalizeField;
            //设置着色对象的分级数目
            pClassBreaksRenderer.BreakCount = ClassesCount;
            pClassBreaksRenderer.SortClassesAscending = true;
            pClassBreaksRenderer.BackgroundSymbol = fillSymbol;

            IMarkerSymbol symbol = null;
            if (markerSymbol == null)
            {
                MessageBox.Show("请先选择点符号", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            //需要注意的是分级着色对象中的symbol和break的下标都是从0开始
            double sizeInterval = (maxSize - minSize) / ClassesCount;
            for (int i = 0; i < ClassesCount; i++)
            {
                symbol = markerSymbol;
                symbol.Size = minSize + i * sizeInterval;
                pClassBreaksRenderer.set_Symbol(i, symbol as ISymbol);
                pClassBreaksRenderer.set_Break(i, gClassbreaks[i + 1]);
            }
            return pClassBreaksRenderer;
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
            }
        }

        private void cbxNormalization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxNormalization.SelectedItem != null)
            {
                strNormalizeField = cbxNormalization.SelectedItem.ToString();
            }
        }

        private void btnSelectSymbol_Click(object sender, EventArgs e)
        {
            ISymbol symbol = GetSymbolByControl(esriGeometryType.esriGeometryPoint);
            if (symbol != null)
                markerSymbol = symbol as IMarkerSymbol;
        }

        private void lblClassificationMethod_Click(object sender, EventArgs e)
        {
            if (layer2Symbolize == null) return;
            IFeatureClass featureClass = layer2Symbolize.FeatureClass;
            ITable pTable = (ITable)featureClass;

            ITableHistogram pTableHistogram2 = new BasicTableHistogramClass();
            IBasicHistogram pHistogram2 = (IBasicHistogram)pTableHistogram2;
            pTableHistogram2.Field = strRendererField;
            if (strNormalizeField.ToLower() != "none")
                pTableHistogram2.NormField = strNormalizeField;
            pTableHistogram2.Table = pTable;
            object dataFrequency;
            object dataValues;
            pHistogram2.GetHistogram(out dataValues, out dataFrequency);

            //下面是分级方法，用于根据获得的值计算得出符合要求的数据
            //根据条件计算出Classes和ClassesCount，numDesiredClasses为预定的分级数目
            IClassifyGEN pClassify = null;
            pClassify = new NaturalBreaksClass();
            int numDesiredClasses = gClassCount;
            pClassify.Classify(dataValues, dataFrequency, ref numDesiredClasses);

            UID pUid;
            pUid = new UIDClass();
            pUid = pClassify.ClassID;
            IClassificationDialog pClassDialog;
            pClassDialog = new ClassificationDialogClass();
            pClassDialog.Classification = pUid;
            pClassDialog.SetHistogramData(pHistogram2 as IHistogram);
            pClassDialog.ClassBreaks = pClassify.ClassBreaks;
            bool ok = pClassDialog.DoModal(0);
            if (ok == false) return;

            pUid = pClassDialog.Classification;
            gClassbreaks = (double[])pClassDialog.ClassBreaks;
            switch (pUid.Value.ToString())
            {
                case "{62144BE1-E05E-11D1-AAAE-00C04FA334B3}":
                    lblClassificationMethod.Text = "等间隔";
                    break;
                case "{62144BE8-E05E-11D1-AAAE-00C04FA334B3}":
                    lblClassificationMethod.Text = "已定义的间隔";
                    break;
                case "{62144BE9-E05E-11D1-AAAE-00C04FA334B3}":
                    lblClassificationMethod.Text = "分位数";
                    break;
                case "{62144BEA-E05E-11D1-AAAE-00C04FA334B3}":
                    lblClassificationMethod.Text = "自然裂点";
                    break;
                case "{DC6D8015-49C2-11D2-AAFF-00C04FA334B3}":
                    lblClassificationMethod.Text = "标准差";
                    break;
                default:
                    break;
            }
        }

        private void nudClassCount_ValueChanged(object sender, EventArgs e)
        {
            gClassCount = Convert.ToInt32(nudClassCount.Value);
        }

        private void btnSelectBackColor_Click(object sender, EventArgs e)
        {
            ISymbol symbol = GetSymbolByControl(esriGeometryType.esriGeometryPolygon);
            if (symbol != null)
                fillSymbol = symbol as IFillSymbol;
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

        private void txtMinSize_Leave(object sender, EventArgs e)
        {
            if (IsDouble(txtMinSize.Text))
                minSize = Convert.ToDouble(txtMinSize.Text);
        }

        private void txtMaxSize_Leave(object sender, EventArgs e)
        {
            if (IsDouble(txtMaxSize.Text))
                maxSize = Convert.ToDouble(txtMaxSize.Text);
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

        private void classifyCBX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (classifyCBX.SelectedItem != null)
            {
                strClassifyMethod = classifyCBX.SelectedItem.ToString();
            }  
        }

        private void classify()
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
            //下面是分级方法，用于根据获得的值计算得出符合要求的数据
            //根据条件计算出Classes和ClassesCount，numDesiredClasses为预定的分级数目
            IClassifyGEN pClassify = new NaturalBreaksClass();
            switch (strClassifyMethod)
            {
                case "等间隔分类":
                    pClassify = new EqualIntervalClass();
                    break;
                //case "预定义间隔分类":
                //    pClassify = new DefinedIntervalClass();
                //    break;
                case "分位数分类":
                    pClassify = new QuantileClass();
                    break;
                case "自然裂点分类":
                    pClassify = new NaturalBreaksClass();
                    break;
                case "标准差分类":
                    pClassify = new StandardDeviationClass();
                    break;
                case "几何间隔分类":
                    pClassify = new GeometricalIntervalClass();
                    break;
                default:
                    break;
            }
            int numDesiredClasses = gClassCount;
            pClassify.Classify(dataValues, dataFrequency, ref numDesiredClasses);
            gClassbreaks = (double[])pClassify.ClassBreaks;

        }
       
    }
}
