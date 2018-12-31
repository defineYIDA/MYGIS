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
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.esriSystem;

namespace Arcgis.View
{
    //ArcGIS桌面中才能使用图层属性对话框及相关的属性页
    public partial class SymbolizationByLayerPropPage : Form
    {
        IHookHelper m_hookHelper = null;
        IActiveView m_activeView = null;
        IMap m_map = null;
        IMapControl3 m_mapControl = null;

        IFeatureLayer layer2Symbolize = null;
        string strSymbolizeMethod = string.Empty;

        public SymbolizationByLayerPropPage(IHookHelper hookHelper)
        {
            InitializeComponent();

            m_hookHelper = hookHelper;
            m_activeView = m_hookHelper.ActiveView;
            m_map = m_hookHelper.FocusMap;

            if (m_hookHelper.Hook is IToolbarControl)
            {
                IToolbarControl toolbarControl = m_hookHelper.Hook as IToolbarControl;
                m_mapControl = (IMapControl3)toolbarControl.Buddy;
            }
            if (m_hookHelper.Hook is IMapControl3)
            {
                m_mapControl = m_hookHelper.Hook as IMapControl3;
            }
        }

        private void SymbolizationByLayerPropPage_Load(object sender, EventArgs e)
        {
            CbxLayersAddItems();
        }

        private void btnSymbolize_Click(object sender, EventArgs e)
        {
            SetupFeaturePropertySheet(layer2Symbolize);
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

        private void cbxLayers2Symbolize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLayers2Symbolize.SelectedItem != null)
            {
                string strLayer2Symbolize = cbxLayers2Symbolize.SelectedItem.ToString();
                layer2Symbolize = GetFeatureLayer(strLayer2Symbolize);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool SetupFeaturePropertySheet(ILayer layer)
        {
            if (layer == null) return false;
            try
            {
                ESRI.ArcGIS.Framework.IComPropertySheet pComPropSheet;
                pComPropSheet = new ESRI.ArcGIS.Framework.ComPropertySheetClass();
                pComPropSheet.Title = layer.Name + "- Properties";

                ESRI.ArcGIS.esriSystem.UID pPPUID = new ESRI.ArcGIS.esriSystem.UIDClass();
                pComPropSheet.AddCategoryID(pPPUID);

                // General....
                ESRI.ArcGIS.Framework.IPropertyPage pGenPage = new ESRI.ArcGIS.CartoUI.GeneralLayerPropPageClass();
                pComPropSheet.AddPage(pGenPage);

                // Source
                ESRI.ArcGIS.Framework.IPropertyPage pSrcPage = new ESRI.ArcGIS.CartoUI.FeatureLayerSourcePropertyPageClass();
                pComPropSheet.AddPage(pSrcPage);

                // Selection...
                ESRI.ArcGIS.Framework.IPropertyPage pSelectPage = new ESRI.ArcGIS.CartoUI.FeatureLayerSelectionPropertyPageClass();
                pComPropSheet.AddPage(pSelectPage);

                // Display....
                ESRI.ArcGIS.Framework.IPropertyPage pDispPage = new ESRI.ArcGIS.CartoUI.FeatureLayerDisplayPropertyPageClass();
                pComPropSheet.AddPage(pDispPage);

                // Symbology....
                ESRI.ArcGIS.Framework.IComPropertyPage pDrawPage = new ESRI.ArcGIS.CartoUI.LayerDrawingPropertyPageClass();
                pComPropSheet.AddPage(pDrawPage);

                // Fields... 
                ESRI.ArcGIS.Framework.IPropertyPage pFieldsPage = new ESRI.ArcGIS.CartoUI.LayerFieldsPropertyPageClass();
                pComPropSheet.AddPage(pFieldsPage);

                // Definition Query... 
                ESRI.ArcGIS.Framework.IPropertyPage pQueryPage = new ESRI.ArcGIS.CartoUI.LayerDefinitionQueryPropertyPageClass();
                pComPropSheet.AddPage(pQueryPage);

                // Labels....
                ESRI.ArcGIS.Framework.IPropertyPage pSelPage = new ESRI.ArcGIS.CartoUI.LayerLabelsPropertyPageClass();
                pComPropSheet.AddPage(pSelPage);

                // Joins & Relates....
                ESRI.ArcGIS.Framework.IPropertyPage pJoinPage = new ESRI.ArcGIS.ArcMapUI.JoinRelatePageClass();
                pComPropSheet.AddPage(pJoinPage);

                // Setup layer link
                ESRI.ArcGIS.esriSystem.ISet pMySet = new ESRI.ArcGIS.esriSystem.SetClass();
                pMySet.Add(layer);
                pMySet.Reset();

                // make the symbology tab active
                pComPropSheet.ActivePage = 4;

                // show the property sheet
                bool bOK = pComPropSheet.EditProperties(pMySet, this.Handle.ToInt32());

                m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_activeView.Extent);

                return (bOK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
