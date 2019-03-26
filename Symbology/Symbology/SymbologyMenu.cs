using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.SystemUI;

namespace Symbology
{
    /// <summary>
    /// Summary description for SymbologyMenu.
    /// </summary>
    [Guid("aed07d14-afbb-4c30-ac92-52ecbb214d4e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Symbology.SymbologyMenu")]
    public sealed class SymbologyMenu : BaseMenu
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
            GxCommandBars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion

        public SymbologyMenu()
        {
            //
            // TODO: Define your menu here by adding items
            //
            //AddItem("esriArcMapUI.ZoomInFixedCommand");
            //BeginGroup(); //Separator
            //AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1); //undo command
            //AddItem(new Guid("FBF8C3FB-0480-11D2-8D21-080009EE4E51"), 2); //redo command
            AddItem("Symbology.SimpleRenderCommand");
            AddItem("Symbology.UniqueValueRender");
            AddItem("Symbology.DotDensityRender");
        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "My C# Menu";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "SymbologyMenu";
            }
        }
        public int  Itemcount { get {return 2;}}

        public void GetItemInfo(int pos, IItemDef itemDef)
        {
            switch (pos)
            {
                case 0: itemDef.ID = "SimpleRenderCommand"; break;//简单着色
                case 1: itemDef.ID = "Symbology.UniqueValueRender"; break;//唯一值着色
                case 2: itemDef.ID = "Symbology.DotDensityRender"; break;//点密度图
            }
        }
    }
}