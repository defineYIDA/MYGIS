using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;


namespace Arcgis.Commands
{
    /// <summary>
    /// Command that works in ArcGlobe or GlobeControl
    /// </summary>
    [Guid("0d2c0430-9b99-46fd-8317-cd61cc4c4153")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Arcgis.Commands.SaveEditCmd")]
    public sealed class SaveEditCmd : BaseCommand
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
        private IMap m_Map = null;
        private bool bEnable = true;
        private IActiveView m_activeView = null;
        private IEngineEditor m_EngineEditor = null;
        public SaveEditCmd()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "编辑按钮"; //localizable text
            base.m_caption = "保存编辑";  //localizable text
            base.m_message = "保存编辑过程所做的操作";  //localizable text 
            base.m_toolTip = "";  //localizable text
            base.m_name = "SaveEditCmd";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            m_Map = m_HookHelper.FocusMap;
            m_activeView = m_Map as IActiveView;
            m_EngineEditor = MapManager.EngineEditor;
             if (m_EngineEditor == null) return; //为空则返回 
             if(m_EngineEditor.EditState!= esriEngineEditState.esriEngineStateEditing)  return; 
             IWorkspace pWs = m_EngineEditor.EditWorkspace; 
             Boolean bHasEdit = m_EngineEditor.HasEdits();//是否编辑 
             if (bHasEdit)  {  
               if ( MessageBox.Show("是否保存所做的编辑？", "提示", MessageBoxButtons.YesNo, 
 MessageBoxIcon.Information) == DialogResult.Yes ) 
                 {   m_EngineEditor.StopEditing(true); //停止编辑并将改动保存
                     m_EngineEditor.StartEditing(pWs, m_Map); 
                     m_activeView.Refresh();
                 }
             }
        }

        #endregion
    }
}
