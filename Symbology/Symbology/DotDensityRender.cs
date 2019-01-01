using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
namespace Symbology
{
    /// <summary>
    /// Command that works in ArcGlobe or GlobeControl
    /// </summary>
    [Guid("755584e6-2273-481f-830d-76c15bbd44e7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Symbology.DotDensityRender")]
    public sealed class DotDensityRender : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GMxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GMxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_HookHelper = null;

        public DotDensityRender()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "点密度图"; //localizable text
            base.m_caption = "点密度图";  //localizable text
            base.m_message = "点密度图";  //localizable text 
            base.m_toolTip = "点密度图";  //localizable text
            base.m_name = "DotDensityRender";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_HookHelper = new HookHelperClass();
                m_HookHelper.Hook = hook;
                if (m_HookHelper.ActiveView == null)
                {
                    m_HookHelper = null;
                }
            }
            catch
            {
                m_HookHelper = null;
            }

            if (m_HookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add DotDensityRender.OnClick implementation
            string strPopField = "value";
            IActiveView pActiveView = m_HookHelper.ActiveView;
            IMap pMap = m_HookHelper.FocusMap;
            IGeoFeatureLayer pGeoFeatureLayer = pMap.get_Layer(0)as IGeoFeatureLayer;
            IDotDensityRenderer pDotDensityRenderer = new DotDensityRendererClass();
            IRendererFields pRendererFields = (IRendererFields)pDotDensityRenderer;
            pRendererFields.AddField(strPopField,strPopField);
            IDotDensityFillSymbol pDotDensityFillSymbol =new DotDensityFillSymbolClass();
            pDotDensityFillSymbol.DotSize=5;
            pDotDensityFillSymbol.Color=GetRGB(0,0,0);
            pDotDensityFillSymbol.BackgroundColor = GetRGB(239,228,190);
            ISymbolArray pSymbolArray = (ISymbolArray)pDotDensityFillSymbol;
            ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
            pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            pSimpleMarkerSymbol.Size = 5;
            pSimpleMarkerSymbol.Color = GetRGB(128,128,255);
            pSymbolArray.AddSymbol((ISymbol)pSimpleMarkerSymbol);
            pDotDensityRenderer.DotDensitySymbol = pDotDensityFillSymbol;
            pDotDensityRenderer.DotValue = 0.5;
            pDotDensityRenderer.CreateLegend();
            pGeoFeatureLayer.Renderer = (IFeatureRenderer)pDotDensityRenderer;
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,null,null);
         }
        #endregion

        private IRgbColor GetRGB(int red, int green, int blue)
        {
            IRgbColor rgb = new RgbColorClass();
            rgb.Red = red;
            rgb.Green = green;
            rgb.Blue = blue;
            return rgb;
        }
    }




}
