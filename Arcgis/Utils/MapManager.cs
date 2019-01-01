using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;

namespace Arcgis.IDName
{
    public class MapManager
    {
        public MapManager() 
        {
        }
        private static IEngineEditor engineEditor;
        public static IEngineEditor EngineEditor
        {
            get { return MapManager.engineEditor; }
            set { MapManager.engineEditor= value; }
        }
    }
}
