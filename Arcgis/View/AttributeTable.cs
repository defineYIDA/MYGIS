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
using ESRI.ArcGIS.Geodatabase;
using Arcgis.Presenters;

namespace Arcgis.View
{
    public partial class AttributeTable : Form,Arcgis.Presenters.IAttributeTable
    {

        /// <summary>
        /// 该view对应的presenter
        /// </summary>
        private AttributeTablePresenter presenter;
        public AttributeTablePresenter Presenter
        {
            get { return presenter; }
            set { presenter = value; }
        }
        
        /// <summary>
        /// 图层对象数据源
        /// </summary>
        public ILayer mLayer { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="layer"></param>
        public AttributeTable(ILayer layer)
        {
            this.presenter = new AttributeTablePresenter(this);
            InitializeComponent();
            mLayer = layer;
        }
        /// <summary>
        /// Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttributeTable_Load(object sender, EventArgs e)
        {
            AttributedataGridView.DataSource = this.presenter.fillAttributeTable();
        }
    }
}
