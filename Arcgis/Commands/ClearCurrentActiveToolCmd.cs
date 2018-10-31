using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;

namespace Arcgis.Commands
{
    /// <summary>
    /// Command that works in ArcGlobe or GlobeControl
    /// </summary>
    [Guid("8346555a-6356-4b90-b43a-b2153cde79ba")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Arcgis.Commands.ClearCurrentActiveToolCmd")]
    public sealed class ClearCurrentActiveToolCmd : BaseCommand
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

        private IGlobeHookHelper m_globeHookHelper = null;
        private IToolbarControl pToolbarControl;

        public ClearCurrentActiveToolCmd()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Custom Command"; //localizable text  将命令进行归类
            base.m_caption = "清除当前活动工具";  //localizable text 
            base.m_message = "清除当前活动工具";  //localizable text 信息提示
            base.m_toolTip = "清除当前活动工具";  //localizable text 鼠标提示信息
            base.m_name = "ClearCurrentActiveToolCmd";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
                System.Diagnostics.Trace.WriteLine(ex.Message, "无效图标");
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
               
                m_globeHookHelper = new GlobeHookHelperClass();
                m_globeHookHelper.Hook = hook;

                pToolbarControl = hook as IToolbarControl;

                //if (m_globeHookHelper.ActiveViewer == null)
                //{
                //    m_globeHookHelper = null;
                //}
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
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ClearCurrentActiveToolCmd.OnClick implementation
            pToolbarControl.CurrentTool = null;
        }

        #endregion
    }
}
