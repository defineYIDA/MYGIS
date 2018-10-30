using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;

namespace Arcgis.Presenters
{
    public interface IAttributeTable
    {
        /// <summary>
        /// 属性表填充的数据源
        /// </summary>
        ILayer mLayer{get;set;}
    }
}
