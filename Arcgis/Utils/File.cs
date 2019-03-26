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
        //设置文档对象的成员变量
        IMapDocument mapDocument = new MapDocumentClass();

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="axMapControl"></param>
        public void saveAsDocument(AxMapControl axMapControl)
        {
            if (axMapControl.DocumentFilename == null)//空地图为null
            {
                MessageBox.Show("没有地图文档！");
                return;
            }
            mapDocument.Open(axMapControl.DocumentFilename, "");//必须的一步，用于将AxMapControl的实例的DocumentFileName传递给pMapDoc的
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "保存地图文档";
            saveFileDialog1.Filter = "地图文档(*.mxd)|*.mxd";//设置过滤属性
            saveFileDialog1.FileName = axMapControl.DocumentFilename;//给定一个初始保存路为原路径
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)return;//未选择文件return
            string filePath = saveFileDialog1.FileName;//获取到文件路径
            if(filePath=="")return;
            if (filePath == mapDocument.DocumentFilename)//判断路径是否改变，如果没有改变保存当前修改，改变则另存为
            {
                saveDocument();
            }
            else
            {
                mapDocument.SaveAs(filePath,true,true);
            }

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
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;//未选择文件return
            string filePath = openFileDialog1.FileName;//获取到文件路径
            if (axMapControl.CheckMxFile(filePath))//检查路径是否合法
            {
                try
                {
                    axMapControl.LoadMxFile(filePath, 0, Type.Missing);
                }
                catch (Exception e) {
                    MessageBox.Show("该地图已损坏或者受保护不能被打开");
                }
            }
            else
            {
                MessageBox.Show(filePath+"不是有效的地图文档路径");
                return;
            }
            axMapControl.Refresh();
        }
        /// <summary>
        /// 保存当前修改
        /// </summary>
        public void saveDocument()
        {
            if (mapDocument.get_IsReadOnly(mapDocument.DocumentFilename) == true)//是否可写
            {
                MessageBox.Show("This map document is read only !");
                return;
            }
            mapDocument.Save(mapDocument.UsesRelativePaths,true);//以相对路径保存,
            MessageBox.Show("Changes saved successfully !");
        }

    }
}
