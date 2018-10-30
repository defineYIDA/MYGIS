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
        //图层对象数据源
        public ILayer mLayer{get;set;}
        
        public AttributeTable(ILayer layer)
        {
            this.presenter = new AttributeTablePresenter(this);
            InitializeComponent();
            mLayer = layer;
        }
        /// <summary>
        /// 该view对应的controller
        /// </summary>
        private AttributeTablePresenter presenter;

        public AttributeTablePresenter Presenter
        {
            get { return presenter; }
            set { presenter = value; }
        }
        private void AttributeTable_Load(object sender, EventArgs e)
        {
            AttributedataGridView.DataSource = this.presenter.fillAttributeTable();
        }
    }
}
