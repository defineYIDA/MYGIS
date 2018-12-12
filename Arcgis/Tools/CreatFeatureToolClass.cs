using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
//using System.Runtime.InteroperationServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System.Text;


namespace Arcgis.Tools
{
    /// <summary>
    /// Summary description for CreatFeatureToolClass.
    /// </summary>
    [Guid("15621c29-9359-49d0-a3a9-79272f5c67a8")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Arcgis.Tools.CreatFeatureToolClass")]
    public sealed class CreatFeatureToolClass : BaseTool
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
        private bool bEnable;
        private IMap m_Map = null;
        private IActiveView m_activeView = null;
        private IEngineEditor m_EngineEditor = null;
        private IEngineEditLayers m_EngineEditLayers = null;
        private IPointCollection m_pointCollection;
        private INewLineFeedback m_newLineFeedback;
        private INewPolygonFeedback m_newPolygonFeedback;
        private INewMultiPointFeedback m_newMultiPointFeedback;

        public CreatFeatureToolClass()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "编辑工具"; //localizable text 
            base.m_caption = "";  //localizable text 
            base.m_checked = false;
            base.m_enabled = bEnable;
            base.m_helpID = -1;
            base.m_helpFile = "";
            base.m_message = "添加要素";  //localizable text 
            base.m_toolTip = "添加要素";  //localizable text
            base.m_name = "CreatFeatureTool";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
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
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            m_Map = m_HookHelper.FocusMap;
            m_activeView = m_Map as IActiveView;
            m_EngineEditor = MapManager.EngineEditor;
            m_EngineEditLayers = MapManager.EngineEditor as IEngineEditLayers;

        }
        public int Cursor { get { return -1; } } public bool Deactivate() { return true; }
        public bool OnContextMenu(int x, int y) { return false; }

        public override void OnDblClick()
        {
            IGeometry pResultGeometry =null; 
            if (m_EngineEditLayers==null) return;
            //获取编辑目标图层
            IFeatureLayer pFeatLyr =m_EngineEditLayers.TargetLayer;
            if (pFeatLyr == null) return;
            IFeatureClass pFeatCls= pFeatLyr.FeatureClass;
            if(pFeatCls == null) return;
            switch (pFeatCls.ShapeType)
            {
                case esriGeometryType.esriGeometryMultipoint:
                    m_newMultiPointFeedback.Stop(); 
                    pResultGeometry = m_pointCollection as IGeometry;
                    m_newMultiPointFeedback = null; 
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    IPolyline pPolyline = null; pPolyline = m_newLineFeedback.Stop();
                    pResultGeometry = pPolyline as IGeometry; 
                    m_newLineFeedback = null;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    IPolygon pPolygon = null; 
                    pPolygon = m_newPolygonFeedback.Stop();
                    pResultGeometry = pPolygon as IGeometry; 
                    m_newPolygonFeedback = null;
                    break;
            }
            IZAware pZAware = pResultGeometry as IZAware; pZAware.ZAware = true;
            CreateFeature(pResultGeometry); //创建新要素
            
        }
        public void OnKeyDown(int keyCode, int shift) { }
        public void OnKeyUp(int keyCode, int shift) { }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            try
            {
                IPoint pPt = m_activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
                if (m_EngineEditor == null) return;
                if (m_EngineEditor.EditState != esriEngineEditState.esriEngineStateEditing) return;
                if (m_EngineEditLayers == null) return;
                IFeatureLayer pFeatLyr = m_EngineEditLayers.TargetLayer;
                if (pFeatLyr == null) return;
                IFeatureClass pFeatCls = pFeatLyr.FeatureClass;
                if (pFeatCls == null) return;
                //解决编辑要素的z值问题
                IZAware pZAware = pPt as IZAware;
                pZAware.ZAware = true; pPt.Z = 0;
                object missing = Type.Missing;
                m_Map.ClearSelection();
                switch (pFeatCls.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        //当为点层时, 直接创建要素
                        CreateFeature(pPt as IGeometry); break;
                    case esriGeometryType.esriGeometryMultipoint: //点集的处理方式
                        if (m_pointCollection == null) m_pointCollection = new MultipointClass();
                        else m_pointCollection.AddPoint(pPt, ref missing, ref missing);
                        if (m_newMultiPointFeedback == null)
                        {
                            m_newMultiPointFeedback = new NewMultiPointFeedbackClass();
                            m_newMultiPointFeedback.Display = m_activeView.ScreenDisplay;
                            m_newMultiPointFeedback.Start(m_pointCollection, pPt);
                        }
                        break;
                    case esriGeometryType.esriGeometryPolyline : //多义线处理方式
                        if (m_newLineFeedback == null)
                        {
                            m_newLineFeedback=new NewLineFeedbackClass();
                            m_newLineFeedback.Display = m_activeView.ScreenDisplay;
                            m_newLineFeedback.Start(pPt);
                        }
                        else
                        {
                            m_newLineFeedback.AddPoint(pPt);
                        }
                        break;
                     case esriGeometryType .esriGeometryPolygon: //多边形处理方式
                         if(m_newPolygonFeedback == null)
                         {
                             m_newPolygonFeedback=new NewPolygonFeedbackClass();
                             m_newPolygonFeedback.Display =m_activeView.ScreenDisplay;
                             m_newPolygonFeedback.Start(pPt);
                         }
                        else
                         {
                             m_newPolygonFeedback.AddPoint(pPt);
                         }
                         break;

                }


            }
            catch (Exception e) 
            { 
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pPt = m_activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            if (m_EngineEditLayers == null) return;
            //获取编辑目标图层
            IFeatureLayer pFeatLyr = m_EngineEditLayers.TargetLayer;
            if (pFeatLyr == null) return;
            IFeatureClass pFeatCls = pFeatLyr.FeatureClass;
            if (pFeatCls == null) return;
            switch (pFeatCls.ShapeType)
            {
                case esriGeometryType.esriGeometryPolyline:
                    if (m_newLineFeedback != null)
                        m_newLineFeedback.MoveTo(pPt);
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    if (m_newPolygonFeedback != null)
                        m_newPolygonFeedback.MoveTo(pPt);
                    break;

            }


        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add CreatFeatureToolClass.OnMouseUp implementation
        }
        public void OnMouseUp(int button, int shift, int x, int y){ }
        public void Refresh(int hdc){ }
        #endregion
        #region 操作函数
        private void CreateFeature(IGeometry pGeometry)
        {
            try
            {
                if(m_EngineEditLayers == null) return;
                IFeatureLayer pFeatLyr = m_EngineEditLayers.TargetLayer;
                if (pFeatLyr == null) return;
                IFeatureClass pFeatCls = pFeatLyr.FeatureClass;
                if (pFeatCls == null) return;
                if (m_EngineEditor ==null)return;
                if (pGeometry == null) return;
                ITopologicalOperator pTop = pGeometry as ITopologicalOperator;
                pTop.Simplify();
                IGeoDataset pGeoDataset = pFeatCls as IGeoDataset;
                if (pGeoDataset.SpatialReference!= null)
                {
                    pGeometry.Project(pGeoDataset.SpatialReference);
                }
                m_EngineEditor.StartOperation();
                IFeature pFeature = null;
                pFeature = pFeatCls.CreateFeature();
                pFeature.Shape = SupportZMFeatureClass.ModifyGeomtryZMValue(pFeatCls, pGeometry);
                pFeature.Store();
                m_EngineEditor.StopOperation("添加要素");
                m_Map.SelectFeature(pFeatLyr, pFeature);
                m_activeView.Refresh();
            }
            catch (Exception e)
            {
            }
        }
        #endregion
    }
}
