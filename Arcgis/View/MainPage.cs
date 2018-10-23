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
//using System.Runtime.InteroperationServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using Arcgis.Controller;



namespace Arcgis.View
{
    public partial class MainPage : Form
    {
        private ILayer layer;

        public MainPage()
        {
            MainPageController mainPageController = new MainPageController(this);
            InitializeComponent();
        }

        /// <summary>
        /// 该view对应的controller
        /// </summary>
        private MainPageController _Controller;
        
        public MainPageController Controller
        {
            get { return _Controller; }
            set { _Controller = value; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axTOCControl1.SetBuddyControl(axMapControl1);
            //定制菜单
            chkCustomize.Checked = false;
            chkCustomize.CheckOnClick = true;
            //调用非模态定制对话框
            this._Controller.CreateCustomizeDialog();
        }
    }
}
