using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;

namespace Arcgis.View
{
    public partial class SelectByAttribute : Form
    {
        IHookHelper m_hookhelper;
        IActiveView m_activeview;
        IMap m_map;

        private esriSelectionResultEnum selectmethod = esriSelectionResultEnum.esriSelectionResultNew;/*用来记录处理结果的方法*/
        private IFeatureSelection pFeatureSelection = null;

        public SelectByAttribute(IHookHelper hookhelper)
        {
            InitializeComponent();
            m_hookhelper = hookhelper;
            m_map = m_hookhelper.FocusMap;
            m_activeview = m_hookhelper.ActiveView;
        }

        private void SelectByAttribute_Load(object sender, EventArgs e)
        {
            IEnumLayer Layers = GetLayers();
            Layers.Reset();
            ILayer Layer = Layers.Next();
            while (Layer != null)
            {
                comboBoxLayers.Items.Add(Layer.Name.ToString());
                Layer = Layers.Next();
            }
            SetupEvents();
        }

        private void comboBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxFields.Items.Clear();
            listBoxValues.Items.Clear();
            string strSelectedLayerName = comboBoxLayers.Text;
            IFeatureLayer pFeatureLayer;

            try
            {
                for (int i = 0; i <= m_map.LayerCount - 1; i++)
                {
                    if (m_map.get_Layer(i).Name == strSelectedLayerName)
                    {
                        if (m_map.get_Layer(i) is IFeatureLayer)
                        {
                            pFeatureLayer = m_map.get_Layer(i) as IFeatureLayer;

                            for (int j = 0; j <= pFeatureLayer.FeatureClass.Fields.FieldCount - 1; j++)
                            {
                                listBoxFields.Items.Add(pFeatureLayer.FeatureClass.Fields.get_Field(j).Name);
                            }

                            labelDescription2.Text = strSelectedLayerName;
                        }
                        else
                        {
                            MessageBox.Show("该图层不能够进行属性查询!请重新选择");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 确定选择方法
        private void comboBoxMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxMethod.SelectedIndex)
            {
                case 0: selectmethod = esriSelectionResultEnum.esriSelectionResultNew; break;
                case 1: selectmethod = esriSelectionResultEnum.esriSelectionResultAdd; break;
                case 2: selectmethod = esriSelectionResultEnum.esriSelectionResultSubtract; break;
                case 3: selectmethod = esriSelectionResultEnum.esriSelectionResultAnd; break;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxWhereClause.Text == string.Empty)
            {
                MessageBox.Show("请构造SQL查询语句！");
                return;
            }
            int result = ExceuteAttributeSelect();
            if (result == -1)
            {
                labelResult.Text = "查询出现错误!";
                return;
            }
            labelResult.Text = string.Format("查到{0}个对象", result);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.Clear();
        }

        private void SelectByAttribute_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pFeatureSelection != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureSelection);
            } 
        }

        #region 方法
        /// <summary>
        /// 根据图层名获取其引用
        /// </summary>
        /// <param name="strLayerName"></param>
        /// <returns></returns>
        private ILayer GetLayerByName(string strLayerName)
        {
            ILayer pLayer = null;
            for (int i = 0; i < m_map.LayerCount; i++)
            {
                pLayer = m_map.get_Layer(i);
                if (strLayerName == pLayer.Name)
                {
                    break;
                }
            }
            return pLayer;
        }

        /// <summary>
        /// 执行按属性选择
        /// </summary>
        private int ExceuteAttributeSelect()
        {
            try
            {
                /*构造查询对象 搜索被查询图层 执行查询*/
                IQueryFilter pQueryFilter = new QueryFilterClass();
                IFeatureLayer pFeatureLayer = null;
                pQueryFilter.WhereClause = textBoxWhereClause.Text;
                ILayer targetLayer = GetLayerByName(comboBoxLayers.Text);
                pFeatureLayer = targetLayer as IFeatureLayer;
                pFeatureSelection = pFeatureLayer as IFeatureSelection;
                pFeatureSelection.SelectFeatures(pQueryFilter, selectmethod, false);//选择满足条件的要素

                if (pFeatureSelection.SelectionSet.Count == 0)
                {
                    MessageBox.Show("没有查询到相关的记录！");
                    return 0;
                }

                m_activeview.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                return pFeatureSelection.SelectionSet.Count;
            }
            catch
            {
                MessageBox.Show("查询语句可能有误,请检查重试");
                return -1;
            }
        }
        #endregion

        #region 构造查询语句
        private void listBoxFields_DoubleClick(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = listBoxFields.SelectedItem.ToString() + " ";
        }

        private void buttonGetValue_Click(object sender, EventArgs e)
        {
            if (listBoxFields.Text == "")
            {
                MessageBox.Show("请选择一个属性字段！");
                return;
            }
            string strSelectedFieldName = listBoxFields.Text;//这个名字是选中的属性字段的名称
            listBoxValues.Items.Clear();
            valueCounts.Text = "";
            if (strSelectedFieldName == null) return;
            IFeatureClass pFeatureClass = (GetLayerByName(comboBoxLayers.Text) as IFeatureLayer).FeatureClass;
            if (pFeatureClass == null) return;
            int fieldIdx = pFeatureClass.Fields.FindField(strSelectedFieldName);
            IField field = pFeatureClass.Fields.get_Field(fieldIdx);
            try
            {
                System.Collections.IEnumerator uniqueValues = GetUniqueValues(pFeatureClass, strSelectedFieldName);
                if (uniqueValues == null) return;
                if ((field.Type == esriFieldType.esriFieldTypeDouble) ||
                   (field.Type == esriFieldType.esriFieldTypeInteger) ||
                   (field.Type == esriFieldType.esriFieldTypeSingle) ||
                   (field.Type == esriFieldType.esriFieldTypeSmallInteger))
                {
                    System.Collections.Generic.List<double> valuesList = new System.Collections.Generic.List<double>();
                    while (uniqueValues.MoveNext())
                    {
                        valuesList.Add(double.Parse(uniqueValues.Current.ToString()));
                    }
                    valuesList.Sort();
                    foreach (object uniqueValue in valuesList)
                    {
                        listBoxValues.Items.Add(uniqueValue.ToString());
                    }
                }
                else
                {
                    System.Collections.Generic.List<object> valuesList = new System.Collections.Generic.List<object>();
                    while (uniqueValues.MoveNext())
                    {
                        valuesList.Add(uniqueValues.Current);
                    }
                    valuesList.Sort();
                    foreach (object uniqueValue in valuesList)
                    {
                        listBoxValues.Items.Add(uniqueValue.ToString());
                    }
                }
                valueCounts.Text = GetUniqueValuesCount(pFeatureClass, strSelectedFieldName).ToString() + "个值";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetUniqueValuesCount(IFeatureClass featureClass, string strField)
        {
            ICursor cursor = (ICursor)featureClass.Search(null, false);
            IDataStatistics dataStatistics = new DataStatisticsClass();
            dataStatistics.Field = strField;
            dataStatistics.Cursor = cursor;
            System.Collections.IEnumerator enumerator = dataStatistics.UniqueValues;
            return dataStatistics.UniqueValueCount;
        }

        private System.Collections.IEnumerator GetUniqueValues(IFeatureClass featureClass, string strField)
        {
            ICursor cursor = (ICursor)featureClass.Search(null, false);
            IDataStatistics dataStatistics = new DataStatisticsClass();
            dataStatistics.Field = strField;
            dataStatistics.Cursor = cursor;


            System.Collections.IEnumerator enumerator = dataStatistics.UniqueValues;
            return enumerator;
        }

        private void listBoxValues_DoubleClick(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " " + listBoxValues.SelectedItem.ToString();
        }

        /// <summary>
        /// 绑定所有生成语句的一般按钮到同一事件处理，将其添加到语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clauseElementClicked(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = ((Button)sender).Text;
        }

        /// <summary>
        /// 括号添加到语句需要特殊处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrace_Click(object sender, EventArgs e)
        {
             textBoxWhereClause.SelectedText = "(  )";
            /*让输入的位置恰好处在（）里面，就同arcmap的效果一样*/
            textBoxWhereClause.SelectionStart = textBoxWhereClause.Text.Length - 2;
        }
          #endregion

        #region "GetLayers"
        private IEnumLayer GetLayers()
        {
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";// IFeatureLayer
            IEnumLayer Layers = m_hookhelper.FocusMap.get_Layers(uid, true);
            return Layers;
        }
        #endregion

        #region "GetFeatureLayer"
        private IFeatureLayer GetFeatureLayer(string strLayer)
        {
            IEnumLayer Layers = GetLayers();
            Layers.Reset();
            ILayer Layer = Layers.Next();
            while (Layer != null)
            {
                if (Layer.Name == strLayer)
                    return Layer as IFeatureLayer;
                Layer = Layers.Next();
            }
            return null;
        }
        #endregion

        IActiveViewEvents_Event activeViewEvent = null;
        IActiveViewEvents_SelectionChangedEventHandler mapSelectionChanged;
        private void SetupEvents()
        {
            activeViewEvent = m_activeview as IActiveViewEvents_Event;
            mapSelectionChanged = new IActiveViewEvents_SelectionChangedEventHandler(OnMapSelectionChanged);
            activeViewEvent.SelectionChanged += mapSelectionChanged;
        }

        private void OnMapSelectionChanged()
        {
            m_activeview.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, m_activeview.Extent);
        }

    }
}
