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

    }
}
