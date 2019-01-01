using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Arcgis;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
//using System.Runtime.InteroperationServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using Arcgis.Presenters;
using Arcgis.Commands;
using Arcgis.Tools;
using Symbology;
using ESRI.ArcGIS.DataSourcesFile;//ShapefileWorkspaceFactory CoClass的程序集
using ESRI.ArcGIS.DataSourcesRaster;//RasterWorkspaceFactoryClass
using ESRI.ArcGIS.DataSourcesGDB;//RasterWorkspaceFactoryClass
using ESRI.ArcGIS.Geodatabase;
using Arcgis.IDName;
using ESRI.ArcGIS.Output;
using YCMap.Utils;
namespace Arcgis.View
{
    public partial class MainPage : Form
    {
        /// <summary>
        /// 图层对象
        /// </summary>
        private ILayer layer;
        /// <summary>
        /// 选中的图层
        /// </summary>
        public IFeatureLayer pCurrentLyr = null;
        /// <summary>
        /// 该view对应的controller
        /// </summary>
        private MainPagePresenters presenter;
        //---
        public IEngineEditor pEngineEditor = null;
        public IEngineEditTask pEngineEditTask = null;
        public IEngineEditLayers pEngineEditLayers = null;
        public IMap pMap = null;
        //mvp设计模式，减少main.cs的代码
        public MainPagePresenters Presenter
        {
            get { return presenter; }
            set { presenter = value; }
        }

        IBasicMap bMap = null;//当前地图
        object other = new object();//UNK图例组
        object index = new object();//图例的索引号

        public IMapControlDefault m_mapControl = null;
        public IPageLayoutControlDefault m_pageLayoutControl = null;
        public ControlsSynchronizer m_controlsSynchronizer = null;
        public ITOCControlDefault m_tocControl = null;
        //总览视图
        private FormOverview m_FormOverview = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainPage()
        {
            this.Presenter= new MainPagePresenters(this);
            InitializeComponent();
            try
            {
                //初始化IMapControlDefault与IPageLayoutControlDefault接口变量
                m_mapControl = axMapControl1.Object as IMapControlDefault;
                m_pageLayoutControl = axPageLayoutControl1.Object as IPageLayoutControlDefault;
                m_tocControl = axTOCControl1.Object as ITOCControlDefault;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            m_controlsSynchronizer = new ControlsSynchronizer(m_mapControl, m_pageLayoutControl);
            //把 MapControl 和 PageLayoutControl 绑定起来(两个都指向同一个 Map),然后设置 MapControl 为活动的 Control
            m_controlsSynchronizer.BindControls(true);
            //为了在切换 MapControl 和 PageLayoutControl 视图同步，要添加 Framework Control
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControl1.Object);
            m_controlsSynchronizer.AddFrameworkControl(axTOCControl1.Object);

            

            IMenuDef menu = new Symbology.SymbologyMenu();
            axToolbarControl1.AddItem(menu,-1,-1,false,-1,esriCommandStyles.esriCommandStyleIconAndText);
            axTOCControl1.SetBuddyControl(axMapControl1);
            
            axToolbarControl1.SetBuddyControl(axMapControl1);
            //定制菜单
            chkCustomize.Checked = false;
            chkCustomize.CheckOnClick = true;
            //调用非模态定制对话框
            this.presenter.CreateCustomizeDialog();
            //初始化自定义命令
            axToolbarControl1.AddItem(new ClearCurrentActiveToolCmd(),-1,-1,false,0,esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new AddDateTool(axToolbarControl1, axPageLayoutControl1), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
            //设置编辑菜单的状态
            saveEdit.Enabled = false;
            endEdit.Enabled = false;
            selectLayer.Enabled = false;
            addLayer.Enabled = false;
            //编辑
            pEngineEditor = new EngineEditorClass(); 
            MapManager.EngineEditor = pEngineEditor;
            pEngineEditTask = pEngineEditor as IEngineEditTask;
            pEngineEditLayers=pEngineEditor as IEngineEditLayers;
           
        }
        #region MainPage的事件

        #region 打开和保存地图文档
        /// <summary>
        /// 保存和另存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            File file = new File();
            file.saveAsDocument(axMapControl1);
        }
        /// <summary>
        /// 打开地图文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void openMapDoc_Click(object sender, EventArgs e)
        {
            File file = new File();
            OpenNewMapDocument openDoc=new OpenNewMapDocument(m_controlsSynchronizer);
            openDoc.OnClick();
            //file.loadMapDoc(axMapControl1);
        }
        #endregion

        /// <summary>
        /// 地图发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            //this.Presenter.copyToPageLayout();//地图控件和布局控件数据共享
            //this.Presenter.fillEagleEye();//填充鹰眼视图
        }
        /// <summary>
        /// map绘制完成后发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void axMapControl1_OnAfterScreenDraw(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            //if (axMapControl1.LayerCount != 0) //mapcontrol不为空，和pagelayout共享数据和视图范围
            //{
            //    IActiveView activeView = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;//获得pagelayout的当前视图
            //    IDisplayTransformation displayTransformation = activeView.ScreenDisplay.DisplayTransformation;//获得显示转换对象
            //    //根据MapControl的视图范围,确定PageLayoutControl的视图范围
            //    displayTransformation.VisibleBounds = axMapControl1.Extent;
            //    axPageLayoutControl1.ActiveView.Refresh();
            //    //this.Presenter.copyToPageLayout();
            //}         
            
        }
        /// <summary>
        /// pageLayout改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axPageLayoutControl1_OnPageLayoutReplaced(object sender, IPageLayoutControlEvents_OnPageLayoutReplacedEvent e)
        {            
            if (axMapControl1.LayerCount == 0)
            {
                //this.presenter.copyToMapControl();
            }
        }
        /// <summary>
        /// 主窗体视图范围变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void axMapControl1_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            ////获得当前地图视图的外包矩形
            //IEnvelope pEnvelope = (IEnvelope)e.newEnvelope;

            ////获得GraphicsContainer对象用来管理元素对象
            //IGraphicsContainer pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;

            ////清除对象中的所有图形元素
            //pGraphicsContainer.DeleteAllElements();

            ////获得矩形图形元素
            //IRectangleElement pRectangleEle = new RectangleElementClass();
            //IElement pElement = pRectangleEle as IElement;
            //pElement.Geometry = pEnvelope;


            ////设置FillShapeElement对象的symbol
            //IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
            //pFillShapeEle.Symbol = this.Presenter.getFillSymbol();

            ////进行填充
            //pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);

            ////刷新视图
            //IActiveView pActiveView = pGraphicsContainer as IActiveView;
            //pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

            //创建矩形元素
            IFillShapeElement fillShapeElement = ElementsHelper.GetRectangleElement(e.newEnvelope as IGeometry);
            //刷新总览窗体的mapcontrol
            if (m_FormOverview != null && !m_FormOverview.IsDisposed)
            {
                m_FormOverview.UpdateMapControlGraphics(fillShapeElement as IElement);
            }
        }

        #region 鹰眼视图上鼠标的事件
        /// <summary>
        /// 鹰眼视图上鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void axMapControl2_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        //{
        //    if (axMapControl2.Map.LayerCount > 0)
        //    {
        //        if (e.button == 1)//左键将所点击的位置，设置为主视图的中心
        //        {
        //            IPoint pPoint = new PointClass();
        //            pPoint.PutCoords(e.mapX, e.mapY);//设置point对象的坐标
        //            axMapControl1.CenterAt(pPoint);
        //            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        //        }
        //        else if (e.button == 2)//右键拉框范围设置为主视图显示范围
        //        {
        //            IEnvelope pEnv = axMapControl2.TrackRectangle();//获得拉框的范围
        //            axMapControl1.Extent = pEnv;
        //            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        //        }
        //    }

        //}
        /// <summary>
        /// 鹰眼视图上鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void axMapControl2_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button == 1)//左键点击移动，俩个视图联动
            {
                IPoint pPoint = new PointClass();
                pPoint.PutCoords(e.mapX, e.mapY);
                axMapControl1.CenterAt(pPoint);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }

        }
        #endregion

        /// <summary>
        /// mapcontrol刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void axMapControl1_OnViewRefreshed(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnViewRefreshedEvent e)
        {
            this.Presenter.copyToPageLayout();//刷新布局视图
        }
        /// <summary>
        /// TOCC的鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            //获得hittest，需要的参数
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;//TOCC中项的类型

            axTOCControl1.HitTest(e.x, e.y, ref item, ref bMap, ref layer, ref other, ref index); //实现赋值,ref的参数必须初始化
            if (e.button == 1)
            {
                //修改图例功能待添加
            }
            else if (e.button == 2)//右键
            {
                if (item == esriTOCControlItem.esriTOCControlItemLayer) ////点击的是图层的话，就显示右键菜单
                {
                    contextMenuStrip1.Show(axTOCControl1, new System.Drawing.Point(e.x, e.y));
                }
            }
        }

        /// <summary>
        /// 打开属性表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void attributeTable_Click(object sender, EventArgs e)
        {
            AttributeTable attributeTable = new AttributeTable(layer);
            attributeTable.Text = "属性表：" + layer.Name;
            attributeTable.ShowDialog();
        }

        public void axTOCControl1_OnBeginLabelEdit(object sender, ITOCControlEvents_OnBeginLabelEditEvent e)
        {
            ITOCControl m_TOCControl;
            IBasicMap map = null;
            object other = null;
            object index = null;
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            m_TOCControl = axTOCControl1.Object as ITOCControl;
            m_TOCControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
            if (item != esriTOCControlItem.esriTOCControlItemLayer)
            {
                e.canEdit = false;
            }
        }
        //当图层名设置为空时编辑失败
        public void axTOCControl1_OnEndLabelEdit(object sender, ITOCControlEvents_OnEndLabelEditEvent e)
        {
            if (e.newLabel.Trim() == "")
            {
                e.canEdit = false;
            }
        }

        public void chkCustomize_CheckStateChanged(object sender, System.EventArgs e)
        {
            //显示或者隐藏定制对话框
            if (chkCustomize.Checked == false)
            {
                this.Presenter.m_CustomizeDialog.CloseDialog();
            }
            else
            {
                this.Presenter.m_CustomizeDialog.StartDialog(axToolbarControl1.hWnd);
            }
        }
        #endregion
        /// <summary>
        /// 打开矢量文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openSHPData_Click(object sender, EventArgs e)
        {
            this.presenter.addShpData();
        }
        /// <summary>
        /// 打开栅格文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openRasterFile_Click(object sender, EventArgs e)
        {
            this.presenter.addRasterData();
        }
        /// <summary>
        /// 打开个人地理数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openPersonalDataSet_Click(object sender, EventArgs e)
        {
            this.presenter.addDataSet();
        }
        /// <summary>
        ///创建要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createFeatureClass_Click(object sender, EventArgs e)
        {
            this.presenter.createFeatureClass();
        }
        /// <summary>
        /// 开始编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void starEdit_Click(object sender, EventArgs e)
        {
            this.presenter.startEdit();
        }
        /// <summary>
        /// 选中图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectLayer.SelectedItem == null) return;
            string lyrName = selectLayer.SelectedItem.ToString();
            ILayer layer = this.presenter.SelectedIndexChanged(lyrName);
            if(layer==null)return;
            pCurrentLyr = layer as IFeatureLayer;
            IDataset pDataset = pCurrentLyr.FeatureClass as IDataset;
            IWorkspace pws = pDataset.Workspace;
            pEngineEditLayers.SetTargetLayer(pCurrentLyr, 0);
            
        }
        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveEdit_Click(object sender, EventArgs e)
        {
            ICommand m_SavaEditCmd = new SaveEditCmd();
            m_SavaEditCmd.OnCreate(axMapControl1.Object);
            m_SavaEditCmd.OnClick();
        }
        /// <summary>
        /// 结束编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endEdit_Click(object sender, EventArgs e)
        {
            ICommand m_StopEditCmd = new StopEditCmd();
            m_StopEditCmd.OnCreate(axMapControl1.Object);
            m_StopEditCmd.OnClick();
            axMapControl1.CurrentTool = null;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            //按钮处于不可用状态 清空图层选择
            selectLayer.Items.Clear();
            selectLayer.Text = "";
            saveEdit.Enabled = false;
            endEdit.Enabled = false;
            selectLayer.Enabled = false;
            addLayer.Enabled = false;

        }
        /// <summary>
        /// 添加要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addLayer_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand createFeatureTool = new CreatFeatureToolClass();
                createFeatureTool.OnCreate(this.axMapControl1.Object);
                createFeatureTool.OnClick();
                this.axMapControl1.CurrentTool = createFeatureTool as ITool;
                this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            }
            catch (Exception) { throw; }

        }

        #region 符号系统
        /// <summary>
        /// 唯一值符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UniqueValueRenderer_Click(object sender, EventArgs e)
        {
            ICommand command = new UniqueValuesSymbolCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 分类符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClassBreaksRendererSymbol_Click(object sender, EventArgs e)
        {
            ICommand command = new ClassBreaksRendererSymbolCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 分类符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClassBreaksRendererColor_Click(object sender, EventArgs e)
        {
            ICommand command = new GraduatedColorSymbolsCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 单一符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleSymbol_Click(object sender, EventArgs e)
        {
            ICommand command = new SingleSymbolCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 通用符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolizationByLayerPropPage_Click(object sender, EventArgs e)
        {
            ICommand command = new SymbolizationByLayerPropPageCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 分级符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraduatedSymbols_Click(object sender, EventArgs e)
        {
            ICommand command = new GraduatedSymbolsCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 分级色彩符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraduatedColorSymbols_Click(object sender, EventArgs e)
        {
            ICommand command = new GraduatedColorSymbolsCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 依比例符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProportionalSymbols_Click(object sender, EventArgs e)
        {
            ICommand command = new ProportionalSymbolsCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 点值符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DotDensitySymbols_Click(object sender, EventArgs e)
        {
            ICommand command = new DotDensitySymbolsCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 统计符号化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatisticsSymbols_Click(object sender, EventArgs e)
        {
            ICommand command = new StatisticsSymbolsCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        #endregion
        /// <summary>
        /// 属性查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttributeQuery_Click(object sender, EventArgs e)
        {
            ICommand command = new AttributeQueryCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
        /// <summary>
        /// 导出地图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportMap_Click(object sender, EventArgs e)
        {
            IActiveView activeView = axPageLayoutControl1.ActiveView;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "导出地图";
            saveFileDialog1.Filter = "(*.jpg)|*.jpg|(*.tiff)|*.tiff|(*.bmp)|*.bmp|(*.emf)|*.emf|(*.png)|*.png|(*.gif)|*.gif";//设置过滤属性
            saveFileDialog1.FileName = axMapControl1.DocumentFilename;//给定一个初始保存路为原路径
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;//未选择文件return
            string filePath = saveFileDialog1.FileName;//获取到文件路径
            if (filePath == "") return;
            this.presenter.ExportMapExtent(activeView,filePath);//导出地图
        }
        #region 绘图
        private void CreatePoint_Click(object sender, EventArgs e)
        {
            ICommand cmd = new CreatePointTool();
            cmd.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = cmd as ITool;
        }

        private void CreatePolyline_Click(object sender, EventArgs e)
        {
            ICommand cmd = new CreatePolylineTool();
            cmd.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = cmd as ITool;
        }

        private void CreatePolygon_Click(object sender, EventArgs e)
        {
            ICommand cmd = new CreatePolygonTool();
            cmd.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = cmd as ITool;
        }

        private void CreateCircle_Click(object sender, EventArgs e)
        {
            ICommand cmd = new CreateCircleTool();
            cmd.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = cmd as ITool;
        }

        private void CreateRectangle_Click(object sender, EventArgs e)
        {
            ICommand cmd = new CreateRectangleTool();
            cmd.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = cmd as ITool;
        }
        #endregion
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewMapDocument_Click(object sender, EventArgs e)
        {
            //非空白文档询问是否保存地图
            if (!String.IsNullOrEmpty(m_mapControl.DocumentFilename))
            {
                //询问是否保存当前地图
                DialogResult result = MessageBox.Show("是否保存当前地图?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //保存地图
                    this.presenter.menuItemSave_Click(null, null);
                }
            }
            //创建新的地图实例
            IMap map = new MapClass();
            map.Name = "Map";
            m_controlsSynchronizer.MapControl.DocumentFilename = string.Empty;
            //更新新建地图实例的共享地图文档
            m_controlsSynchronizer.ReplaceMap(map);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                //空白文档不保存
                if (String.IsNullOrEmpty(m_mapControl.DocumentFilename)) return;

                //创建地图文档，调用open方法，调用ReplaceContents方法
                IMapDocument mapDocument = new MapDocumentClass();
                mapDocument.Open(m_mapControl.DocumentFilename);
                mapDocument.ReplaceContents(m_mapControl as IMxdContents);

                IObjectCopy objCopy = new ObjectCopyClass(); //使用Copy，避免共享引用  
                m_mapControl.Map = (IMap)objCopy.Copy(mapDocument.get_Map(0));
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
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, EventArgs e)
        {
            //空白文档不保存
            if (String.IsNullOrEmpty(m_mapControl.DocumentFilename)) return;

            //询问是否保存当前地图
            DialogResult result = MessageBox.Show("是否保存当前地图?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //保存地图
                this.presenter.menuItemSave_Click(null, null);
            }
            Application.Exit();
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            //用户关闭窗体，触发退出菜单click事件
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //空白文档不保存
                if (String.IsNullOrEmpty(m_mapControl.DocumentFilename)) return;
                Exit_Click(null, null);
            }
        }

        private void EagleEye_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_FormOverview == null || m_FormOverview.IsDisposed)
                {
                    m_FormOverview = new FormOverview(axMapControl1);
                }
                m_FormOverview.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainPage_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //隐藏总览窗体
                if (m_FormOverview != null && !m_FormOverview.IsDisposed)
                {
                    m_FormOverview.Hide();
                }
            }
            else
            {
                if (m_FormOverview != null) {
                    m_FormOverview.Show();
                }
                
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) //map view
            {
                //activate the MapControl and deactivate the PageLayoutControl
                m_controlsSynchronizer.ActivateMap();
            }
            else //layout view
            {
                //activate the PageLayoutControl and deactivate the MapControl
                m_controlsSynchronizer.ActivatePageLayout();
            }
        }
    }
}
