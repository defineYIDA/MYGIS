using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
//using System.Runtime.InteroperationServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;

namespace Arcgis.Tools
{
    /// <summary>
    /// Summary description for AddDataTool.
    /// </summary>
    [Guid("7ffd67ca-1d2a-40ca-acb6-fab13ee34625")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Arcgis.Tools.AddDataTool")]
    public sealed class AddDateTool : BaseTool
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

        private IHookHelper m_globeHookHelper = null;
        AxToolbarControl toolBar;
        AxPageLayoutControl pageLayout;

        public AddDateTool(AxToolbarControl toolBar,AxPageLayoutControl pageLayout)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Custom Command"; //localizable text 
            base.m_caption = "添加日期元素";  //localizable text 
            base.m_message = "添加日期元素";  //localizable text 
            base.m_toolTip = "添加日期元素";  //localizable text
            base.m_name = "AddDateTool";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
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
            this.toolBar = toolBar;
            this.pageLayout = pageLayout;
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
                m_globeHookHelper = new HookHelperClass();
                m_globeHookHelper.Hook = hook;
                if (m_globeHookHelper.ActiveView == null)
                {
                    m_globeHookHelper = null;
                }
                toolBar.SetBuddyControl(pageLayout);
            }
            catch
            {
                m_globeHookHelper = null;
            }

            if (m_globeHookHelper == null)
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
            // TODO: Add AddDataTool.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDataTool.OnMouseDown implementation
            base.OnMouseDown(Button, Shift, X, Y);
            //获得当前活动视图
            IActiveView activeView = m_globeHookHelper.ActiveView;
            //创建新的文本元素
            ITextElement textElement = new TextElementClass();
            //创建文本符号
            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.Size = 25;
            //设置文本元素属性
            textElement.Symbol = textSymbol;
            textElement.Text = DateTime.Now.ToShortDateString();
            IElement element = textElement as IElement;
            //创建点
            IPoint point = new PointClass();
            point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X,Y);
            //设置元素属性
            element.Geometry = point;
            //增加元素到图形的绘制容器
            activeView.GraphicsContainer.AddElement(element,0);
            //refresh
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics,null,null);

        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDataTool.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDataTool.OnMouseUp implementation
        }
        #endregion
    }
}
