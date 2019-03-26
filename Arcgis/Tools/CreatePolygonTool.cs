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
    /// Summary description for CreatePolygonTool.
    /// </summary>
    [Guid("4dcdef94-4686-4dda-b3dc-028a1b6a5a7f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GeometryAndSR.CreatePolygonTool")]
    public sealed class CreatePolygonTool : BaseTool
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
  
        public CreatePolygonTool( )
        {
            base.m_category = "WindowsFormsApplication1"; 
            base.m_caption = "创建多边形元素";
            base.m_message = "创建多边形元素";
            base.m_toolTip = "创建多边形元素";
            base.m_name = "CreatePolygonTool";   
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
            IScreenDisplay pScreenDisplay = m_ActiveView.ScreenDisplay;
            IRubberBand pRubberPolygon = new RubberPolygonClass();
            ISimpleFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = getRGB(255, 255, 0);
            IPolygon pPolygon = pRubberPolygon.TrackNew(pScreenDisplay, (ISymbol)pFillSymbol) as IPolygon;
            pFillSymbol.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            pFillSymbol.Color = getRGB(0, 0, 255);
            IFillShapeElement pPolygonEle = new PolygonElementClass();
            pPolygonEle.Symbol = pFillSymbol;
            IElement pEle = pPolygonEle as IElement;
            pEle.Geometry = pPolygon;
            IGraphicsContainer pGraphicsContainer = m_Map as IGraphicsContainer;
            pGraphicsContainer.AddElement(pEle, 0);
            m_ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public void DrawPolygon(ESRI.ArcGIS.Carto.IActiveView activeView)
        {

            if (activeView == null)
            {
                return;
            }

            ESRI.ArcGIS.Display.IScreenDisplay screenDisplay = activeView.ScreenDisplay;
            screenDisplay.StartDrawing(screenDisplay.hDC, (System.Int16)ESRI.ArcGIS.Display.esriScreenCache.esriNoScreenCache); // Explicit Cast
            ESRI.ArcGIS.Display.IRgbColor rgbColor = new ESRI.ArcGIS.Display.RgbColorClass();
            rgbColor.Red = 255;

            ESRI.ArcGIS.Display.IColor color = rgbColor; // Implicit Cast
            ESRI.ArcGIS.Display.ISimpleFillSymbol simpleFillSymbol = new ESRI.ArcGIS.Display.SimpleFillSymbolClass();
            simpleFillSymbol.Color = color;

            ESRI.ArcGIS.Display.ISymbol symbol = simpleFillSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
            ESRI.ArcGIS.Display.IRubberBand rubberBand = new ESRI.ArcGIS.Display.RubberPolygonClass();
            ESRI.ArcGIS.Geometry.IGeometry geometry = rubberBand.TrackNew(screenDisplay, symbol);
            screenDisplay.SetSymbol(symbol);
            screenDisplay.DrawPolygon(geometry);
            screenDisplay.FinishDrawing();
        }

        public IColor getRGB(int yourRed, int yourGreen, int yourBlue)
        {
            IRgbColor pRGB = new RgbColorClass();
            pRGB.Red = yourRed;
            pRGB.Green = yourGreen;
            pRGB.Blue = yourBlue;
            pRGB.UseWindowsDithering = true;
            return pRGB;
        }
        #endregion
    }
}
