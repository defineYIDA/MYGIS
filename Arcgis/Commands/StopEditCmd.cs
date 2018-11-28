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
    [Guid("feeaecbd-4f44-4106-9f78-9b8c1c231ecc")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Arcgis.Commands.StopEditCmd")]
    public sealed class StopEditCmd : BaseCommand
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

        private IHookHelper m_hookHelper = null;
        private IMap m_Map = null;
        private bool bEnable = true;
        private IActiveView m_activeView = null;
        private IEngineEditor m_EngineEditor = null;

        public StopEditCmd()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "编辑按钮"; //localizable text
            base.m_caption = "停止编辑";  //localizable text
            base.m_message = "停止编辑";  //localizable text 
            base.m_toolTip = "";  //localizable text
            base.m_name = "StopEditCmd";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
           m_Map = m_hookHelper.FocusMap;
           m_activeView = m_Map as IActiveView;
           m_EngineEditor = MapManager.EngineEditor;
           Boolean bSave = true;
           if (m_EngineEditor == null) return;
           if (m_EngineEditor.EditState!= esriEngineEditState.esriEngineStateEditing) return;
           IWorkspaceEdit2 pWsEdit2 = m_EngineEditor.EditWorkspace as IWorkspaceEdit2;
           if (pWsEdit2.IsBeingEdited())
           {   
               Boolean bHasEdit = m_EngineEditor.HasEdits();
               if (bHasEdit)
               {   
                   if (MessageBox.Show("是否保存所做的编辑？", "提示", MessageBoxButtons.YesNo,
    MessageBoxIcon.Information) == DialogResult.Yes)
                   { 
                       bSave = true;
                   }
                   else
                   {
                       bSave = false;
                   }
               }
               m_EngineEditor.StopEditing(bSave);
           }
            m_Map.ClearSelection();   m_activeView.Refresh();
        }

        #endregion
    }
}
