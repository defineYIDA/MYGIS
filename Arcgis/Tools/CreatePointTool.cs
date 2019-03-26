using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace Arcgis.Tools
{
    /// <summary>
    /// Summary description for CreatePointTool.
    /// </summary>
    [Guid("c4ca405e-c5bb-437d-965c-5f7081ee9e11")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GeometryAndSR.CreatePointTool")]
    public sealed class CreatePointTool : BaseTool
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
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        IMap m_Map;
        IActiveView m_ActiveView;
    
        public CreatePointTool( )
        {
            base.m_category = "WindowsFormsApplication1"; 
            base.m_caption = "创建点元素";
            base.m_message = "创建点元素";
            base.m_toolTip = "创建点元素";
            base.m_name = "CreatePointTool";
            try
            {
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }   

        #region Overriden Class Methods       
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;            
        }            

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            m_ActiveView = m_hookHelper.ActiveView;
            m_Map = m_hookHelper.FocusMap;
            IPoint pPt = m_ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            IMarkerElement pMarkerElement = new MarkerElementClass();
            ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbolClass();
            pMarkerSymbol.Color = getRGB(255, 0, 0);
            pMarkerSymbol.Size = 4;
            pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCross;
            IElement pElement = pMarkerElement as IElement;
            pElement.Geometry = pPt;
            pMarkerElement.Symbol = pMarkerSymbol;
            IGraphicsContainer pGraphicsContainer = m_Map as IGraphicsContainer;
            pGraphicsContainer.AddElement(pMarkerElement as IElement, 0);
            m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }       
        #endregion       

        public IColor getRGB(int yourRed, int yourGreen, int yourBlue)
        {
            IRgbColor pRGB = new RgbColorClass();
            pRGB.Red = yourRed;
            pRGB.Green = yourGreen;
            pRGB.Blue = yourBlue;
            pRGB.UseWindowsDithering = true;
            return pRGB;
        }

    }
}
