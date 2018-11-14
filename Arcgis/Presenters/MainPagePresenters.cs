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
using Arcgis.View;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;//ShapefileWorkspaceFactory CoClass的程序集
using ESRI.ArcGIS.DataSourcesRaster;//RasterWorkspaceFactoryClass
using ESRI.ArcGIS.DataSourcesGDB;//RasterWorkspaceFactoryClass

namespace Arcgis.Presenters
{ 
    public class MainPagePresenters
    {
        /// <summary>
        /// 该控制器的view
        /// </summary>
        public MainPage view;
        /// <summary>
        /// 图层对象
        /// </summary>
        private ILayer layer;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="view"></param>
        public MainPagePresenters(MainPage view)
        {
            this.view = view;
        }


        /// <summary>
        /// 地图控件和布局控件数据共享
        /// </summary>
        public void copyToPageLayout()
        {
            //IObjectCopy接口提供Copy方法用于地图的复制
            IObjectCopy objectCopy = new ObjectCopyClass();
            object copyFromMap = view.axMapControl1.Map;//要copy的map
            object copyMap = objectCopy.Copy(copyFromMap);
            object copyToMap = view.axPageLayoutControl1.ActiveView.FocusMap;
            //Overwrite方法用于地图写入PageLayoutControl控件的视图中
            objectCopy.Overwrite(copyMap, ref copyToMap);//引用传递焦点视图
        }
        /// <summary>
        /// 根据主视图填充鹰眼视图
        /// </summary>
        public void fillEagleEye()
        {
            if (view.axMapControl1.LayerCount > 0)
            {
                for (int i = 0; i <= view.axMapControl1.Map.LayerCount - 1; ++i)
                {
                    view.axMapControl2.Map.AddLayer(view.axMapControl1.Map.get_Layer(i));
                }
                //axMapControl2.Map = axMapControl1.Map;
                view.axMapControl2.Extent = view.axMapControl1.FullExtent;//设置鹰眼视图为全局视图
                view.axMapControl2.Refresh();
            }
        }
        /// <summary>
        /// 获得鹰眼视图显示方框的symbol
        /// </summary>
        /// <returns></returns>
        public IFillSymbol getFillSymbol()
        {
            //矩形框的边界线颜色
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;
            //边界线
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 3;
            pOutline.Color = pColor;
            //symbol的背景色
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 0;
            //获得显示的图形元素
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            return pFillSymbol;
        }

        //声明非模态定制对话框
        public ICustomizeDialog m_CustomizeDialog = new CustomizeDialogClass();
        //声明事件委托（打开和关闭对话框）
        private ICustomizeDialogEvents_OnStartDialogEventHandler startDialogE;
        private ICustomizeDialogEvents_OnCloseDialogEventHandler closeDialogE;
        /// <summary>
        /// 生成非模态定制对话框
        /// </summary>
        public void CreateCustomizeDialog()
        {
            //定义事件接口变量
            ICustomizeDialogEvents_Event pCustomizeDialogEvent = m_CustomizeDialog as ICustomizeDialogEvents_Event;
            //为当前事件实例化(打开对话框事件)
            startDialogE = new ICustomizeDialogEvents_OnStartDialogEventHandler(OnStartDialogHandler);
            //利用该事件接口对象实现打开对话框事件
            pCustomizeDialogEvent.OnStartDialog+=startDialogE;
            pCustomizeDialogEvent.OnCloseDialog += OnCloseDialogHandler;
            m_CustomizeDialog.DialogTitle = "定制对话框";
            m_CustomizeDialog.SetDoubleClickDestination(this.view.axToolbarControl1);

        }
        /// <summary>
        /// 打开定制对话框触发的方法
        /// </summary>
        private void OnStartDialogHandler()
        {
            this.view.axToolbarControl1.Customize = true;
        }
        /// <summary>
        /// 关闭定制对话框触发的方法
        /// </summary>
        private void OnCloseDialogHandler()
        {
            this.view.axToolbarControl1.Customize = false;
        }
        /// <summary>
        /// 添加矢量数据
        /// </summary>
        public void addShpData()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "请选择地理数据文件";
            openFileDialog1.Filter = "矢量数据文件(*.shp)|*.shp";//设置过滤属性
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;//未选择文件return
            string[] fileNames = openFileDialog1.FileNames;//获取到文件路径
            for (int i = 0; i < fileNames.Length;i++ )
            {
                ILayer layer = addShpData(fileNames[i]);
                this.view.axMapControl1.Map.AddLayer(layer);
            }
        }
        /// <summary>
        /// 添加shp文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private ILayer addShpData(string fileName)
        {
            string workSpacePath = System.IO.Path.GetDirectoryName(fileName);
            string shapeFileName = System.IO.Path.GetFileName(fileName);
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace fws = (IFeatureWorkspace)wsf.OpenFromFile(workSpacePath, 0);//打开工作空间
            IFeatureClass pFeatureClass = fws.OpenFeatureClass(shapeFileName);
            IDataset pDataset = pFeatureClass as IDataset;
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = pFeatureClass;
            pFeatureLayer.Name = pDataset.Name;
            ILayer pLayer = pFeatureLayer as ILayer;
            return pLayer;
        }
        /// <summary>
        /// 添加栅格数据
        /// </summary>
        public void addRasterData()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "请选择地理数据文件";
            openFileDialog1.Filter = "栅格数据文件(*.jpg)|*.jpg";//设置过滤属性
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;//未选择文件return
            string fileName = openFileDialog1.FileName;//获取到文件路径
            ILayer layer = openRasterFile(fileName);
            this.view.axMapControl1.AddLayer(layer);
        }
        /// <summary>
        /// 打开栅格文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private ILayer openRasterFile(string fileName)
        {
            string workSpacePath = System.IO.Path.GetDirectoryName(fileName);
            string RasterFileName = System.IO.Path.GetFileName(fileName);
            IWorkspaceFactory wsf = new RasterWorkspaceFactoryClass();
            IRasterWorkspace rws = (IRasterWorkspace)wsf.OpenFromFile(workSpacePath, 0);//打开工作空间
            IRasterDataset rasterDataset = rws.OpenRasterDataset(RasterFileName);
            //影像金字塔判断与创建
            IRasterPyramid rasPyrmid = rasterDataset as IRasterPyramid;
            if (rasPyrmid != null) 
            {
                if (!(rasPyrmid.Present)) rasPyrmid.Create();
            }
            IRaster raster = rasterDataset.CreateDefaultRaster();
            IRasterLayer rLayer = new RasterLayerClass();
            ILayer layer = rLayer as ILayer;
            return layer;
        }
        /// <summary>
        /// 添加个人地理数据库
        /// </summary>
        public void addDataSet()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "请选择地理数据文件";
            openFileDialog1.Filter = "个人数据库(*.mdb)|*.mdb";//设置过滤属性
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;//未选择文件return
            string fileName = openFileDialog1.FileName;//获取到文件路径
            string pFolder = System.IO.Path.GetDirectoryName(fileName);
            string pFileName = System.IO.Path.GetFileName(fileName);
            IWorkspaceFactory wsf = new AccessWorkspaceFactoryClass();//打开数据库
            IWorkspace ws = wsf.OpenFromFile(fileName,0);
            if(ws!=null)
            {
                MessageBox.Show("个人数据库"+pFileName+"打开成功！");
                addDataSetMap(ws);
            }
            else{
                MessageBox.Show("个人数据库"+pFileName+"打开失败！");
            }           
        }
        /// <summary>
        /// 打开个人地理数据库
        /// </summary>
        /// <param name="ws"></param>
        private void addDataSetMap(IWorkspace ws)
        {
            IEnumDataset pEnumDataset;
            pEnumDataset=ws.get_Datasets(esriDatasetType.esriDTFeatureClass);
            IDataset pDataset;
            pEnumDataset.Reset();
            pDataset=pEnumDataset.Next();
            while(pDataset!=null)
            {
                IFeatureClass pFeatureClass = pDataset as IFeatureClass;
                IFeatureLayer pLayer=new FeatureLayerClass();
                pLayer.FeatureClass=pFeatureClass;
                pLayer.Name=pDataset.Name;
                MessageBox.Show("添加要素类"+pDataset.Name+"！");
                this.view.axMapControl1.AddLayer(pLayer);
                pDataset=pEnumDataset.Next();

            }
        }
    }
}
