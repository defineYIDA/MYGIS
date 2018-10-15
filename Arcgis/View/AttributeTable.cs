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
using Arcgis.Controller;

namespace Arcgis.View
{
    public partial class AttributeTable : Form
    {
        ILayer mLayer;//图层对象

        public AttributeTable(ILayer layer)
        {
            AttributeTableController attributeTableController = new AttributeTableController(this);
            InitializeComponent();
            mLayer = layer;
        }
        /// <summary>
        /// 该view对应的controller
        /// </summary>
        private AttributeTableController _Controller;

        public AttributeTableController Controller
        {
            get { return _Controller; }
            set { _Controller = value; }
        }
        private void AttributeTable_Load(object sender, EventArgs e)
        {
            AttributedataGridView.DataSource = this._Controller.fillAttributeTable(mLayer);
        }
    }
}
