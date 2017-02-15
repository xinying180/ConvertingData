# ArcGIS Engine中导入数据的几种方式及其效率对比

在ArcGIS Engine开发过程中，比较常用并且重要的功能就是数据转换，对于数据转换方法您是否足够清楚？ArcGIS Engine中常用的数据转换方法有哪些？各种转换方法的优缺点是什么？采用哪种方法效率更高？如果您对这些问题感兴趣，那么一定要阅读下面文章，相信一定会让您有所收获。

## 一、ArcGIS Engine中导入数据的几种方式及其优缺点：

**IFeatureDataConverter：**细粒度，用于复制单个简单要素类或者要素数据集。***优点：***可使用QueryFilter（属性和空间均可）进行过滤，可以设置SubFields指定复制哪些字段，可以改变要素类的geometry definition以及设置configuration_keyword。除此之外，还可以在基于文件的数据源(如ShapeFile)与地理数据库之间进行复制。***缺点：***只能转换简单要素类和要素数据集，无法转换关系类，几何网络，拓扑等复杂对象。

**IGeoDBDataTransfer（等同于ArcCatalog中对地理数据库中数据集的复制和粘贴）：**适用于两个地理数据库之间复制一个或多个数据集。***优点：***可以一次导入多个数据集，并且能转换几乎所有类型的数据，包括关系类、拓扑、几何网络等复杂对象。***缺点：***不能进行条件过滤，也不能在基于文件的数据源与地理数据库之间进行复制，即不能实现shapefile与FileGDB、MDB或SDE间的导入导出。

**IGdbXmlExport and IGdbXmlImport：**使用XML作为中间文件，适用于两个地理数据库之间复制一个或多个数据集。***优点：***可以在离线下使用。比如SDE往其它地理数据库中进行数据导入，在连接上SDE时生成xml文件，后面即使断开该SDE连接，也可以成功将xml（可仅包括Schema，也可以包含数据）导入到目标库中。还有一个优点是便于数据共享与传输，如果给多个人共享数据，只需拷贝该xml文件即可。

**IExportOperation：**复制单个数据集到另一个Workspace，是经过包装的IFeatureDataConverter。***优点：***可以设置QueryFilter以及SelectionSet，可以跨数据源进行转换，比如从ShapeFile到GeoDatabase，并且可以显示进度条。***缺点：***只能在Desktop产品下使用。

**IDataset.Copy：**从基于文件的数据源（例如shapefile，dbf或coverage要素类）中复制Dataset到另一个基于文件的数据源。

**IWorkspaceFactory（copy或者move）：**复制或移动整个local geodatabase或SDE 连接文件。

**IObjectLoader：**往已有数据集中添加记录，仅能在Desktop产品下使用。由于本文仅讨论复制要素类的情况，该接口我们将在下一篇关于Engine中如何往已有要素类中插入数据中讨论。

当然，不要忘了还可以在程序中调用***GP工具***进行要素类的复制，比如**FeatureClassToFeatureClass工具**、**FeatureClassToGeoDatabase工具**、**CopyFeatures工具**以及**Copy工具**。

## 二、效率对比

前面我们介绍了Engine中常用的复制要素类的几种方法及每种方法的优缺点，接下来我们就进行测试，对比一下效率。本文以将FileGDB中含有20万条记录的点要素类导入SDE中为例进行测试。

代码可参考[ArcObjects帮助文档](http://resources.arcgis.com/en/help/arcobjects-net/conceptualhelp/index.html#/Converting_and_transferring_data/0001000003rp000000/)

*Tips：*如果程序中绑定Engine，则需要初始化EngineGeoDB许可（编辑SDE数据需要该许可，当然如果绑定Desktop，也可以使用Standard或者Advanced许可），直接使用Engine许可会报下面错误。

![报错信息](http://img.blog.csdn.net/20170215105901076?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQveGlueWluZzE4MA==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

下面我们就看测试结果吧。

1， 使用**IFeatureDataConverter.ConvertFeatureClass**将FileGDB中要素类导入SDE中所用时间为：**3分钟16秒**。为了便于对比，这里没有设置查询条件以及SubFields等。

2，使用**IGeoDBTransfer.Transfer**将同样数据导入SDE中所用时间为：**2分42秒**，比第一种方法快34秒。效率提高了17%，目前我导入的数据量仅为20万，相信如果数据量再大些，该方法的效率会更突出。

3，使用**IGdbXmlExport**将FileGDB中的要素类导出成xml所用时间为：**1分55秒**，接着使用**IGdbXmlImport**将生成的xml导入到SDE中所用时间为：**4分27秒**。两个时间加起来显然并未提高效率，但是生成一次xml，后续可以将其导入任意地理数据库中，便于传输与共享。

4，最后测试使用**IExportOperation.ExportFeatureClass** 方法，注意该接口只能在Desktop产品下使用，也就是程序中需要绑定Desktop并且初始化Standard或者Advanced许可。执行该方法时会自动出现进度条来显示进度。

![Export Progress](http://img.blog.csdn.net/20170215110630799?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQveGlueWluZzE4MA==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

所用时间为：**3分22秒**。可见该方法与IFeatureDataConverter所用时间大致相同，也就验证了前面说的“IExportOperation是包装过的IFeatureDataConverter”说法，如果您的程序中不需要使用MapControl、ToolBarControl等控件，并且需要显示进度条，那么使用这个方法最合适不过了。

其余的**IDataset.Copy**，**IWorkspaceFactory（copy或者move）**以及**IObjectLoader**方法无法实现将FileGDB中的要素类导出到SDE中，所以这里就不做讨论了。接下来我们在程序中调用**GP工具**测试一下时间。

1，调用**FeatureClassToFeatureClass工具**，所用时间为**3分32秒**。

2，调用**FeatureClassToGeoDatabase工具**，所用时间为**3分4秒**。

3，调用**CopyFeatures工具**，所用时间为**3分25秒**。

4，调用**Copy工具**，所用时间为**2分45秒**。

从执行时间上可以发现**Copy工具**和**FeatureClassToGeoDatabase工具**用时较少，为大家提供一个参考。

**小结：**执行相同操作**所花时间的长短**虽然是一个程序是否可优化的考量因素，但是**最重要的是实际需求**，比如IGeoDBDataTransfer虽然较IFeatureDataConverter用时短，但是如果想在复制过程中进行查询或者设置SubFields，再或者要将shapeFile导入SDE中，那么就只能使用IFeatureDataConverter了。

说了这么多，前面的东西是否又忘了呢，不要紧，文章最后将之前提到的使用AO接口导入数据的方法汇总如下：

![各种导入数据方法的表格](http://img.blog.csdn.net/20170215134612780?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQveGlueWluZzE4MA==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

## Demo 

使用ArcGIS Engine 10.4，Visual Stduio 2013编写，程序主要有两部分功能，一是通过ArcObjects接口分别实现文中提到的导入数据的几种方式；另一个是直接调用GP工具。主要是想测试一下不同方法实现相同功能的效率对比。

界面为：

![使用AO接口操作](http://img.blog.csdn.net/20170215105459074?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQveGlueWluZzE4MA==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)                ![直接调用GP工具](http://img.blog.csdn.net/20170215105542043?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQveGlueWluZzE4MA==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

### 工程下载地址：

[convertingData](https://github.com/xinying180/ConvertingData)

预告一下，接下来会给大家介绍Engine中往已有要素类中插入数据的方法，敬请期待…
