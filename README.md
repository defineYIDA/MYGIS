# MYGIS
**新增：**
* 添加shp,mxd文件；

* 符号系统；

* 导出地图；

* 总览视图；

* 属性查询(高亮显示带处理)；

**实现功能：**

**UniqueValueRenderer.cs(唯一值符号化)**

导入一个中国空气污染的矢量数据air_pollution：


![](https://github.com/defineYIDA/MYGIS/blob/master/img/1.png)

符号系统的的唯一值符号化，打开符号化窗口：

![](https://github.com/defineYIDA/MYGIS/blob/master/img/2.png)

选择符号化图层为air_pollution，字段为PM2.5；
![](https://github.com/defineYIDA/MYGIS/blob/master/img/3.png)

点击符号化；
![](https://github.com/defineYIDA/MYGIS/blob/master/img/4.png)

**ClassBreakRender.cs(分级色彩或分级符号)**
实现结果(分级色彩为例)：

还是打开打开大气污染的矢量图，点击符号系统的分级色彩；

 ![](https://github.com/defineYIDA/MYGIS/blob/master/img/5.png)
 
选择符号化图层，值字段为PM2.5标准化字段为None然后选择符号化的色带；

 ![](https://github.com/defineYIDA/MYGIS/blob/master/img/6.png)
 
选择分类数为默认5，分类方法为等间隔分类；

 ![](https://github.com/defineYIDA/MYGIS/blob/master/img/7.png)
 
点击符号化；

 ![](https://github.com/defineYIDA/MYGIS/blob/master/img/8.png)


****

版权所有Copyright © by define_YIDA

