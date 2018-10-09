using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;

namespace Arcgis
{
    public class File
    {
        IMapDocument mapDocument = new MapDocumentClass();
        //打开地图文档
        public void newMapDoc(AxMapControl axMapControl)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "新建地图文档";
            saveFileDialog1.Filter = "地图文档(*.mxd)|*.mxd";//设置过滤属性
            saveFileDialog1.ShowDialog();
            //if (saveFileDialog1.ShowDialog() != DialogResult.OK) return null;//未选择文件return
            string filePath = saveFileDialog1.FileName;//获取到文件路径
            if(mapDocument.get_IsMapDocument(filePath))
            {
                mapDocument.New(filePath);//新建
                mapDocument.Open(filePath,"");//打开地图
                axMapControl.Map=mapDocument.get_Map(0);
                axMapControl.Refresh();
            }
            //return null;
        }
        /// <summary>
        /// 打开地图文档
        /// </summary>
        /// <param name="axMapControl"></param>
        public void loadMapDoc(AxMapControl axMapControl)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开地图文档";
            openFileDialog1.Filter = "地图文档(*.mxd)|*.mxd";//设置过滤属性
            openFileDialog1.ShowDialog();
            string filePath = openFileDialog1.FileName;//获取到文件路径
            if (axMapControl.CheckMxFile(filePath))
            {
                axMapControl.LoadMxFile(filePath, 0,Type.Missing);
            }
            else
            {
                MessageBox.Show(filePath+"不是有效的地图文档路经");
                return;
            }
            axMapControl.Refresh();
        }

    }
}
