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
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using Arcgis.View;

namespace Arcgis
{
    public partial class Form1 : Form
    {
        private ILayer layer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axTOCControl1.SetBuddyControl(axMapControl1);
        }

        /// <summary>
        /// 地图控件和布局控件数据共享
        /// </summary>
        private void copyToPageLayout()
        {
            //IObjectCopy接口提供Copy方法用于地图的复制
            IObjectCopy objectCopy = new ObjectCopyClass();
            object copyFromMap = axMapControl1.Map;//要copy的map
            object copyMap = objectCopy.Copy(copyFromMap);
            object copyToMap = axPageLayoutControl1.ActiveView.FocusMap;
            //Overwrite方法用于地图写入PageLayoutControl控件的视图中
            objectCopy.Overwrite(copyMap, ref copyToMap);//引用传递焦点视图
        }

        /// <summary>
        /// 地图发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
   
            copyToPageLayout();//地图控件和布局控件数据共享

            if (axMapControl1.LayerCount > 0)
            {
                for (int i = 0; i <= axMapControl1.Map.LayerCount - 1; ++i)
                {
                    axMapControl2.Map.AddLayer(axMapControl1.Map.get_Layer(i));
                }
                axMapControl2.Extent = axMapControl1.FullExtent;//设置鹰眼视图为全局视图
                axMapControl2.Refresh();
            }

        }

        private void axMapControl1_OnAfterScreenDraw(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            IActiveView activeView = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;//获得pagelayout的当前视图
            IDisplayTransformation displayTransformation = activeView.ScreenDisplay.DisplayTransformation;//获得显示转换对象
            //根据MapControl的视图范围,确定PageLayoutControl的视图范围
            displayTransformation.VisibleBounds = axMapControl1.Extent;
            axPageLayoutControl1.ActiveView.Refresh();
            copyToPageLayout();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            File file = new File();
            file.saveAsDocument(axMapControl1);
        }

        private void 打开地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File file = new File();
            file.loadMapDoc(axMapControl1);
        }

        private void axMapControl2_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            if (axMapControl2.Map.LayerCount > 0)
            {
                if (e.button == 1)
                {
                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(e.mapX, e.mapY);
                    axMapControl1.CenterAt(pPoint);
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
                else if (e.button == 2)
                {
                    IEnvelope pEnv = axMapControl2.TrackRectangle();
                    axMapControl1.Extent = pEnv;
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }

        }

        private void axMapControl2_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button == 1)
            {
                IPoint pPoint = new PointClass();
                pPoint.PutCoords(e.mapX, e.mapY);
                axMapControl1.CenterAt(pPoint);
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }

        }

        private void axMapControl1_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            IEnvelope pEnvelope = (IEnvelope)e.newEnvelope;
            IGraphicsContainer pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            pGraphicsContainer.DeleteAllElements();
            IRectangleElement pRectangleEle = new RectangleElementClass();
            IElement pElement = pRectangleEle as IElement;
            pElement.Geometry = pEnvelope;

            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;

            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 3;
            pOutline.Color = pColor;
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 0;

            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        /// <summary>
        /// mapcontrol刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnViewRefreshed(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnViewRefreshedEvent e)
        {
            copyToPageLayout();//刷新布局视图
        }
        /// <summary>
        /// TOCC的鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            //获得hittest，需要的参数
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;//TOCC中项的类型
            IBasicMap map = null;//当前地图
            object other = new object();//UNK图例组
            object index = new object();//图例的索引号

            axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index); //实现赋值,ref的参数必须初始化
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

        private void 属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttributeTable attributeTable = new AttributeTable(layer);
            attributeTable.Text = "属性表：" + layer.Name;
            attributeTable.ShowDialog();
        }

    }
}
