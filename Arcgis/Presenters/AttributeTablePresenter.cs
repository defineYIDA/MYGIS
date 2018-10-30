using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Arcgis;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Arcgis.Presenters
{
    public class AttributeTablePresenter
    {
        /// <summary>
        /// 该控制器对应的view
        /// </summary>
        public IAttributeTable view;

        public AttributeTablePresenter(IAttributeTable view)
        {
            this.view = view;//通过构造函数给view赋值           
        }
        /// <summary>
        /// 处理图层获取属性表
        /// </summary>
        /// <param name="mLayer"></param>
        /// <returns></returns>
        public DataTable fillAttributeTable()
        {
            ILayer mLayer = this.view.mLayer;
            IFeatureLayer pFeatureLayer = mLayer as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            DataTable dt = new DataTable();//生成存放数据的表
            if (pFeatureClass != null)
            {
                DataColumn dc;
                for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
                {
                    dc = new DataColumn(pFeatureClass.Fields.get_Field(i).Name);
                    dt.Columns.Add(dc);//获取所有列的属性值
                }
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                DataRow dr;
                while (pFeature != null)
                {
                    dr = dt.NewRow();
                    for (int j = 0; j < pFeatureClass.Fields.FieldCount; j++)
                    {
                        //判断feature的形状
                        if (pFeature.Fields.get_Field(j).Name == "Shape")
                        {
                            if (pFeature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                            {
                                dr[j] = "点";
                            }
                            if (pFeature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
                            {
                                dr[j] = "线";
                            }
                            if (pFeature.Shape.GeometryType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
                            {
                                dr[j] = "面";
                            }
                        }
                        else
                        {
                            dr[j] = pFeature.get_Value(j).ToString();//增加行
                        }
                    }
                    dt.Rows.Add(dr);
                    pFeature = pFeatureCursor.NextFeature();
                }
                return dt;
            }
            return null;
        }

    }
}
