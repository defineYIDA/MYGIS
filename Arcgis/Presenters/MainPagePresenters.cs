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
using ESRI.ArcGIS.DataSourcesGDB;
using System.Drawing;//RasterWorkspaceFactoryClass
using ESRI.ArcGIS.Output;

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
            ////IObjectCopy接口提供Copy方法用于地图的复制
            //IObjectCopy objectCopy = new ObjectCopyClass();
            //object copyFromMap = view.axMapControl1.Map;//要copy的map
            //object copyMap = objectCopy.Copy(copyFromMap);
            //object copyToMap = view.axPageLayoutControl1.ActiveView.FocusMap;
            ////Overwrite方法用于地图写入PageLayoutControl控件的视图中
            //objectCopy.Overwrite(copyMap, ref copyToMap);//引用传递焦点视图
        }
        public void copyToMapControl() 
        {
            //IObjectCopy objectCopy = new ObjectCopyClass();
            //object copyFromMap = view.axPageLayoutControl1.ActiveView.FocusMap;//要copy的map
            //object copyMap = objectCopy.Copy(copyFromMap);
            //object copyToMap = view.axMapControl1.ActiveView.FocusMap;
            ////Overwrite方法用于地图写入PageLayoutControl控件的视图中
            //objectCopy.Overwrite(copyMap, ref copyToMap);//引用传递焦点视图
            //this.fillEagleEye();
        }
        /// <summary>
        /// 根据主视图填充鹰眼视图
        /// </summary>
        //public void fillEagleEye()
        //{
        //    if (view.axMapControl1.LayerCount > 0)
        //    {
        //        for (int i = 0; i <= view.axMapControl1.Map.LayerCount - 1; ++i)
        //        {
        //            view.axMapControl2.Map.AddLayer(view.axMapControl1.Map.get_Layer(i));
        //        }
        //        //axMapControl2.Map = axMapControl1.Map;
        //        view.axMapControl2.Extent = view.axMapControl1.FullExtent;//设置鹰眼视图为全局视图
        //        view.axMapControl2.Refresh();
        //    }
        //}
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
                addDataSetMap(ws);
                MessageBox.Show("个人数据库"+pFileName+"打开成功！");                
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
                this.view.axMapControl1.AddLayer(pLayer);
                pDataset=pEnumDataset.Next();

            }
        }
        /// <summary>
        /// 保存地图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuItemSave_Click(object sender, EventArgs e)
        {
            try
            {
                //空白文档不保存
                if (String.IsNullOrEmpty(this.view.m_mapControl.DocumentFilename)) return;

                //创建地图文档，调用open方法，调用ReplaceContents方法
                IMapDocument mapDocument = new MapDocumentClass();
                mapDocument.Open(this.view.m_mapControl.DocumentFilename);
                mapDocument.ReplaceContents(this.view.m_mapControl as IMxdContents);

                IObjectCopy objCopy = new ObjectCopyClass(); //使用Copy，避免共享引用  
                this.view.m_mapControl.Map = (IMap)objCopy.Copy(mapDocument.get_Map(0));
                objCopy = null;

                mapDocument.Save(mapDocument.UsesRelativePaths, false);
                mapDocument.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("请联系管理员，错误原因是：" + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="featureClassName"></param>
        /// <param name="classExtensionUID"></param>
        /// <param name="featureWorkspace"></param>
        /// <returns></returns>
        public IFeatureClass CreateFeatureClassToAccessDB(string featureClassName,UID classExtensionUID,IFeatureWorkspace featureWorkspace)
        {
            //创建字段集合
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

            //定义单个的字段
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;

            pFieldEdit.Name_2 = "OID";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            pFieldsEdit.AddField(pField);
            //为要素类创建几何定义和空间参考
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = geometryDef as IGeometryDefEdit;
            pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;  //指定创建的要素类的要素类型
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference spatialReference = spatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954);
            ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
            spatialReferenceResolution.ConstructFromHorizon();
            ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
            spatialReferenceTolerance.SetDefaultXYTolerance();
          
            pGeoDefEdit.SpatialReference_2 = spatialReference;  //设置要素类的空间参考
            //将几何字段添加到字段集合
            IField geometryField = new FieldClass();
            IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
            geometryFieldEdit.Name_2 = "shape";
            geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            geometryFieldEdit.GeometryDef_2 = geometryDef;
            pFieldsEdit.AddField(geometryField);
            //创建字段name
            IField nameField = new FieldClass();
            IFieldEdit nameFieldEdit = (IFieldEdit)nameField;
            nameFieldEdit.Name_2 = "Name";
            nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            nameFieldEdit.Length_2 = 20;
            pFieldsEdit.AddField(nameField);
            IFeatureClass featureClass = featureWorkspace.CreateFeatureClass(featureClassName, pFields,null,classExtensionUID,esriFeatureType.esriFTSimple,"Shape","");
            return featureClass;
        }
        /// <summary>
        /// 创建要素类
        /// </summary>
        public void createFeatureClass()
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
            IWorkspace ws = wsf.OpenFromFile(fileName, 0);
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            IFeatureClass fc = CreateFeatureClassToAccessDB("point1",null,fws);
            if (fc != null) 
            {
                MessageBox.Show("创建成功!","提示",MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 开始编辑
        /// </summary>
        public void startEdit()
        {
            //设置编辑菜单的状态,激活
            this.view.saveEdit.Enabled = true;
            this.view.endEdit.Enabled = true;
            this.view.selectLayer.Enabled = true;
            this.view.addLayer.Enabled = true;
            this.view.pMap = this.view.axMapControl1.Map;
            if (this.view.axMapControl1.Map.LayerCount == 0)
            {

                MessageBox.Show("请加载编辑图层!", "提示", MessageBoxButtons.OK);
                return;
            }
            this.view.axMapControl1.Map.ClearSelection();
            this.view.axMapControl1.ActiveView.Refresh();
            //初始化图像选择
            this.view.selectLayer.Items.Clear();
            for(int i=0;i < this.view.axMapControl1.LayerCount;i++)
            {
                this.view.selectLayer.Items.Add(this.view.axMapControl1.Map.get_Layer(i).Name);
            }
            if (this.view.selectLayer.Items.Count != 0) this.view.selectLayer.SelectedIndex = 0; //默认选择顶层图层
            IDataset pDataSet = this.view.pCurrentLyr.FeatureClass as IDataset; //获取当前编辑图层工作空间
            IWorkspace pws = pDataSet.Workspace;
            //设置编辑模式,如果是ArcSDE采用版本模式
            if (pws.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                this.view.pEngineEditor.EditSessionMode = esriEngineEditSessionMode.esriEngineEditSessionModeVersioned;
            }else{
                this.view.pEngineEditor.EditSessionMode = esriEngineEditSessionMode.esriEngineEditSessionModeNonVersioned;
            }
            //设置编辑任务
            this.view.pEngineEditTask = this.view.pEngineEditor.GetTaskByUniqueName("ControlToolsEditing_CreateNewFeatureTask");
            this.view.pEngineEditor.CurrentTask = this.view.pEngineEditTask;
            this.view.pEngineEditor.EnableUndoRedo(true);
            this.view.pEngineEditor.StartEditing(pws, this.view.pMap);
            MessageBox.Show("已开始编辑！");

        }
        public ILayer SelectedIndexChanged(string lyrName)
        {
            for (int i = 0; i < this.view.axMapControl1.LayerCount; i++)
            {
                ILayer lyr = this.view.axMapControl1.Map.get_Layer(i);
                if (lyr.Name == lyrName) 
                {
                    return lyr;
                    break;
                }
            }
            return null;
        }
        /// <summary>
        /// 输出当前地图至指定的文件
        /// </summary>
        /// <param name="pView"></param>
        /// <param name="outPath"></param>
        public void ExportMapExtent(IActiveView pView, string outPath)
        {
            try
            {
                //参数检查
                if (pView == null)
                {
                    throw new Exception("输入参数错误,无法生成图片文件!");
                }
                //根据给定的文件扩展名，来决定生成不同类型的对象
                IExport export = null;
                if (outPath.EndsWith(".jpg"))
                {
                    export = new ExportJPEGClass();
                }
                else if (outPath.EndsWith(".tiff"))
                {
                    export = new ExportTIFFClass();
                }
                else if (outPath.EndsWith(".bmp"))
                {
                    export = new ExportBMPClass();
                }
                else if (outPath.EndsWith(".emf"))
                {
                    export = new ExportEMFClass();
                }
                else if (outPath.EndsWith(".png"))
                {
                    export = new ExportPNGClass();
                }
                else if (outPath.EndsWith(".gif"))
                {
                    export = new ExportGIFClass();
                }

                export.ExportFileName = outPath;
                IEnvelope pEnvelope = pView.Extent;

                //导出参数           
                export.Resolution = 300;
                tagRECT exportRect = new tagRECT();
                exportRect.left = 0;
                exportRect.top = 0;
                //这里暂时固定大小
                exportRect.right = 700;
                exportRect.bottom = 1000;
                IEnvelope envelope = new EnvelopeClass();
                //输出范围
                envelope.PutCoords(exportRect.left, exportRect.bottom, exportRect.right, exportRect.top);
                export.PixelBounds = envelope;
                //可用于取消操作
                ITrackCancel pCancel = new CancelTrackerClass();
                export.TrackCancel = pCancel;
                pCancel.Reset();
                //点击ESC键时，中止转出
                pCancel.CancelOnKeyPress = true;
                pCancel.CancelOnClick = false;
                pCancel.ProcessMessages = true;
                //获取handle
                int hDC = export.StartExporting();
                //开始转出
                pView.Output(hDC, (int)export.Resolution, ref exportRect, pEnvelope, pCancel);
                bool bContinue = pCancel.Continue();
                //捕获是否继续
                if (bContinue)
                {
                    export.FinishExporting();
                    export.Cleanup();
                    MessageBox.Show("导出至" + outPath);
                }
                else
                {
                    export.Cleanup();
                }
                bContinue = pCancel.Continue();
            }
            catch (Exception excep)
            {
                MessageBox.Show("导出失败" + excep.Message);//错误信息提示
            }
        }

    }
}
