using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;
using ESRI.ArcGIS.GeoDatabaseUI;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace convertingData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        string sdePath = @"C:\Users\Xinying\AppData\Roaming\ESRI\Desktop10.4\ArcCatalog\Connection to 192.168.220.132.sde";
        string fileGDBPath = Application.StartupPath + "\\test.gdb";
        string sourceFCName = "testPoint_20w";
        string targetFCName = "testPoint_20w_sde";
        string outputXmlFile = Application.StartupPath + "\\workspaceGDB.XML";
        
        private void iFeatureDataConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch myWatch = Stopwatch.StartNew();

            ConvertFileGDBToSDE(fileGDBPath, sourceFCName, targetFCName);

            myWatch.Stop();
            string time = myWatch.Elapsed.TotalMinutes.ToString();
            MessageBox.Show(time + " Minutes");         

           
        }
       
        public void ConvertFileGDBToSDE(string fileGDBPath, string sourceFCName, string targetFCName)
        {            
            // Create a name object for the source (fileGDB) workspace and open it.
            IWorkspaceName sourceWorkspaceName = getWorkspaceName("esriDataSourcesGDB.FileGDBWorkspaceFactory", fileGDBPath);

            IName sourceWorkspaceIName = (IName)sourceWorkspaceName;
            IWorkspace sourceWorkspace = (IWorkspace)sourceWorkspaceIName.Open();

            // Create a name object for the target (SDE) workspace and open it.
            IWorkspaceName targetWorkspaceName = getWorkspaceName("esriDataSourcesGDB.SdeWorkspaceFactory", sdePath);
            IName targetWorkspaceIName = (IName)targetWorkspaceName;
            IWorkspace targetWorkspace = (IWorkspace)targetWorkspaceIName.Open();

            // Create a name object for the source dataset.
            IFeatureClassName sourceFeatureClassName = getFeatureClassName(sourceWorkspaceName, sourceFCName);

            // Create a name object for the target dataset.
            IFeatureClassName targetFeatureClassName = getFeatureClassName(targetWorkspaceName, targetFCName);

            // Open source feature class to get field definitions.
            IName sourceName = (IName)sourceFeatureClassName;
            IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();

            // Create the objects and references necessary for field validation.
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IFields sourceFields = sourceFeatureClass.Fields;
            IFields targetFields = null;
            IEnumFieldError enumFieldError = null;

            // Set the required properties for the IFieldChecker interface.
            fieldChecker.InputWorkspace = sourceWorkspace;
            fieldChecker.ValidateWorkspace = targetWorkspace;

            // Validate the fields and check for errors.
            fieldChecker.Validate(sourceFields, out enumFieldError, out targetFields);
            if (enumFieldError != null)
            {
                // Handle the errors in a way appropriate to your application.
                Console.WriteLine("Errors were encountered during field validation.");
            }

            // Find the shape field.
            String shapeFieldName = sourceFeatureClass.ShapeFieldName;
            int shapeFieldIndex = sourceFeatureClass.FindField(shapeFieldName);
            IField shapeField = sourceFields.get_Field(shapeFieldIndex);

            // Get the geometry definition from the shape field and clone it.
            IGeometryDef geometryDef = shapeField.GeometryDef;
            IClone geometryDefClone = (IClone)geometryDef;
            IClone targetGeometryDefClone = geometryDefClone.Clone();
            IGeometryDef targetGeometryDef = (IGeometryDef)targetGeometryDefClone;

            // Cast the IGeometryDef to the IGeometryDefEdit interface.
            IGeometryDefEdit targetGeometryDefEdit = (IGeometryDefEdit)targetGeometryDef;

            // Set the IGeometryDefEdit properties.
            //targetGeometryDefEdit.GridCount_2 = 1;
            //targetGeometryDefEdit.set_GridSize(0, 0.75);

            // Create a query filter to only select features you want to convert
            IQueryFilter queryFilter = new QueryFilterClass();

            // Create the converter and run the conversion.
            IFeatureDataConverter featureDataConverter = new FeatureDataConverterClass();
            IEnumInvalidObject enumInvalidObject = featureDataConverter.ConvertFeatureClass
                (sourceFeatureClassName, queryFilter, null, targetFeatureClassName,
                targetGeometryDef, targetFields, "", 1000, 0);

            // Check for errors.
            IInvalidObjectInfo invalidObjectInfo = null;
            enumInvalidObject.Reset();
            while ((invalidObjectInfo = enumInvalidObject.Next()) != null)
            {
                // Handle the errors in a way appropriate to the application.
                Console.WriteLine("Errors occurred for the following feature: {0}",
                    invalidObjectInfo.InvalidObjectID);
            }
           
        }

        //"esriDataSourcesGDB.FileGDBWorkspaceFactory"
        //"esriDataSourcesFile.ShapefileWorkspaceFactory"
        //"esriDataSourcesGDB.SdeWorkspaceFactory"
        private IWorkspaceName getWorkspaceName(string WorkspaceFactoryProgID, string PathName)
        {
            IWorkspaceName workspaceName = new WorkspaceNameClass
            {
                WorkspaceFactoryProgID = WorkspaceFactoryProgID,
                PathName = PathName
            };
            return workspaceName;
        }
       
        private IFeatureClassName getFeatureClassName(IWorkspaceName WorkspaceName, string Name)
        {
            // Create a name object for the dataset.
            IFeatureClassName featureClassName = new FeatureClassNameClass();
            IDatasetName datasetName = (IDatasetName)featureClassName;
            datasetName.Name = Name;
            datasetName.WorkspaceName = WorkspaceName;
            return featureClassName;
        }

        private void iGeoDBTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch myWatch = Stopwatch.StartNew();
        
            TansferFileGDBToSDE(fileGDBPath, sourceFCName);
            
            myWatch.Stop();
            string time = myWatch.Elapsed.TotalMinutes.ToString();
            MessageBox.Show(time + " Minutes");
        }
        public void TansferFileGDBToSDE(string fileGDBPath, string sourceFCName)
        {
            // Create workspace name objects.
            IWorkspaceName sourceWorkspaceName = getWorkspaceName("esriDataSourcesGDB.FileGDBWorkspaceFactory", fileGDBPath);

            // Create a name object for the target (SDE) workspace and open it.
            IWorkspaceName targetWorkspaceName = getWorkspaceName("esriDataSourcesGDB.SdeWorkspaceFactory", sdePath);

            IName targetName = (IName)targetWorkspaceName;

            // Create a name object for the source dataset.
            IFeatureClassName sourceFeatureClassName = getFeatureClassName(sourceWorkspaceName, sourceFCName);
            IName sourceName = (IName)sourceFeatureClassName;

            // Create an enumerator for source datasets.
            IEnumName sourceEnumName = new NamesEnumeratorClass();
            IEnumNameEdit sourceEnumNameEdit = (IEnumNameEdit)sourceEnumName;

            // Add the name object for the source class to the enumerator.
            sourceEnumNameEdit.Add(sourceName);

            // Create a GeoDBDataTransfer object and a null name mapping enumerator.
            IGeoDBDataTransfer geoDBDataTransfer = new GeoDBDataTransferClass();
            IEnumNameMapping enumNameMapping = null;

            // Use the data transfer object to create a name mapping enumerator.
            Boolean conflictsFound = geoDBDataTransfer.GenerateNameMapping(sourceEnumName,
                targetName, out enumNameMapping);
            enumNameMapping.Reset();

            // Check for conflicts.
            if (conflictsFound)
            {
                // Iterate through each name mapping.
                INameMapping nameMapping = null;
                while ((nameMapping = enumNameMapping.Next()) != null)
                {
                    // Resolve the mapping's conflict (if there is one).
                    if (nameMapping.NameConflicts)
                    {
                        nameMapping.TargetName = nameMapping.GetSuggestedName(targetName);
                    }

                    // See if the mapping's children have conflicts.
                    IEnumNameMapping childEnumNameMapping = nameMapping.Children;
                    if (childEnumNameMapping != null)
                    {
                        childEnumNameMapping.Reset();

                        // Iterate through each child mapping.
                        INameMapping childNameMapping = null;
                        while ((childNameMapping = childEnumNameMapping.Next()) != null)
                        {
                            if (childNameMapping.NameConflicts)
                            {
                                childNameMapping.TargetName = childNameMapping.GetSuggestedName(targetName);
                            }
                        }
                    }
                }
            }

            // Start the transfer.
            geoDBDataTransfer.Transfer(enumNameMapping, targetName);
        }
        private void ExportDatasetToXML(string fileGdbPath, string sourceFCName, string outputXmlFile)
        {
            // Open the source geodatabase and create a name object for it.
            IWorkspaceName sourceWorkspaceName = getWorkspaceName("esriDataSourcesGDB.FileGDBWorkspaceFactory", fileGdbPath);

            IFeatureClassName sourceFeatureClassName = getFeatureClassName(sourceWorkspaceName, sourceFCName);
            IName sourceName = (IName)sourceFeatureClassName;

            // Create a new names enumerator and add the feature dataset name.
            IEnumNameEdit enumNameEdit = new NamesEnumeratorClass();
            enumNameEdit.Add(sourceName);
            IEnumName enumName = (IEnumName)enumNameEdit;          

            // Create a GeoDBDataTransfer object and create a name mapping.
            IGeoDBDataTransfer geoDBDataTransfer = new GeoDBDataTransferClass();
            IEnumNameMapping enumNameMapping = null;
            geoDBDataTransfer.GenerateNameMapping(enumName, sourceWorkspaceName as IName, out enumNameMapping);

            // Create an exporter and export the dataset with binary geometry, not compressed,
            // and including metadata.
            IGdbXmlExport gdbXmlExport = new GdbExporterClass();
            gdbXmlExport.ExportDatasets(enumNameMapping, outputXmlFile, true, false, true);
          
        }
        private void ImportXmlWorkspaceDocument(string inputXmlFile)
        {
            IWorkspaceName targetWorkspaceName = getWorkspaceName("esriDataSourcesGDB.SdeWorkspaceFactory", sdePath);
            IName targetName = (IName)targetWorkspaceName;
            IWorkspace targetWorkspace = (IWorkspace)targetName.Open();           

            IGdbXmlImport gdbXmlImport = new GdbImporterClass();
            IEnumNameMapping enumNameMapping = null;
            
            Boolean conflictsFound = gdbXmlImport.GenerateNameMapping(inputXmlFile, targetWorkspace, out enumNameMapping);

            // Check for conflicts.
            if (conflictsFound)
            {
                // Iterate through each name mapping.
                INameMapping nameMapping = null;
                enumNameMapping.Reset();
                while ((nameMapping = enumNameMapping.Next()) != null)
                {
                    // Resolve the mapping's conflict (if there is one).
                    if (nameMapping.NameConflicts)
                    {
                        nameMapping.TargetName = nameMapping.GetSuggestedName(targetName);
                    }

                    // See if the mapping's children have conflicts.
                    IEnumNameMapping childEnumNameMapping = nameMapping.Children;
                    if (childEnumNameMapping != null)
                    {
                        childEnumNameMapping.Reset();

                        // Iterate through each child mapping.
                        INameMapping childNameMapping = null;
                        while ((childNameMapping = childEnumNameMapping.Next()) != null)
                        {
                            if (childNameMapping.NameConflicts)
                            {
                                childNameMapping.TargetName =
                                    childNameMapping.GetSuggestedName(targetName);
                            }
                        }
                    }
                }
            }

            // Import the workspace document, including both schema and data.
            gdbXmlImport.ImportWorkspace(inputXmlFile, enumNameMapping, targetWorkspace, false);
        }
     
        private void exportDatasetToXMLImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch myWatch = Stopwatch.StartNew();            
           
            ExportDatasetToXML(fileGDBPath, sourceFCName, outputXmlFile);
            myWatch.Stop();
            string time = myWatch.Elapsed.TotalMinutes.ToString();
            MessageBox.Show(time + " Minutes");

            Stopwatch myWatch1 = Stopwatch.StartNew();

            ImportXmlWorkspaceDocument(outputXmlFile);
            myWatch1.Stop();
            string time1 = myWatch1.Elapsed.TotalMinutes.ToString();
            MessageBox.Show(time1 + " Minutes"); 
    
        }

        private void iExportOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch myWatch = Stopwatch.StartNew();          
          
            ExportOperationFileGDBToSDE(fileGDBPath, sourceFCName, targetFCName);
            
            myWatch.Stop();
            string time = myWatch.Elapsed.TotalMinutes.ToString();
            MessageBox.Show(time + " Minutes");
        }

        public void ExportOperationFileGDBToSDE(string fileGDBPath, string sourceFCName, string targetFCName)
        {

            // Create a name object for the source (file GDB) workspace and open it.
            IWorkspaceName sourceWorkspaceName = getWorkspaceName("esriDataSourcesGDB.FileGDBWorkspaceFactory", fileGDBPath);

            IName sourceWorkspaceIName = (IName)sourceWorkspaceName;
            IWorkspace sourceWorkspace = (IWorkspace)sourceWorkspaceIName.Open();

            // Create a name object for the target (sde) workspace and open it.

            IWorkspaceName targetWorkspaceName = getWorkspaceName("esriDataSourcesGDB.SdeWorkspaceFactory", sdePath);
          
            // Create a name object for the source dataset.
            IFeatureClassName sourceFeatureClassName = getFeatureClassName(sourceWorkspaceName, sourceFCName);

            // Create a name object for the target dataset.
            IFeatureClassName targetFeatureClassName = getFeatureClassName(targetWorkspaceName, targetFCName);

            // Open source feature class to get field definitions.
            IName sourceName = (IName)sourceFeatureClassName;
            IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();

            // Find the shape field.
            String shapeFieldName = sourceFeatureClass.ShapeFieldName;
            int shapeFieldIndex = sourceFeatureClass.FindField(shapeFieldName);
            IField shapeField = sourceFeatureClass.Fields.get_Field(shapeFieldIndex);

            // Get the geometry definition from the shape field and clone it.
            IGeometryDef geometryDef = shapeField.GeometryDef;
            IClone geometryDefClone = (IClone)geometryDef;
            IClone targetGeometryDefClone = geometryDefClone.Clone();
            IGeometryDef targetGeometryDef = (IGeometryDef)targetGeometryDefClone;

            // Cast the IGeometryDef to the IGeometryDefEdit interface.
            IGeometryDefEdit targetGeometryDefEdit = (IGeometryDefEdit)targetGeometryDef;

            // Set the IGeometryDefEdit properties.
            //targetGeometryDefEdit.GridCount_2 = 1;
            //targetGeometryDefEdit.set_GridSize(0, 0.75);

            // Create a query filter to only select features you want to convert
            IQueryFilter queryFilter = new QueryFilterClass();

            // Create the converter and run the conversion.
            IExportOperation exportOperation = new ExportOperationClass();
            exportOperation.ExportFeatureClass(sourceFeatureClassName as IDatasetName, queryFilter, null, targetGeometryDef, targetFeatureClassName, 0);

        }
        private void ExecuteGP(IGPProcess GPProcess)
        {
           
            Geoprocessor gp = new Geoprocessor { OverwriteOutput = true };           
            
            try
            {
                IGeoProcessorResult2 result = gp.Execute(GPProcess, null) as IGeoProcessorResult2;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GP Error");
            }
            finally
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < gp.MessageCount; i++)
                    sb.AppendLine(gp.GetMessage(i));
                if (sb.Capacity > 0) MessageBox.Show(sb.ToString(), "GP Messages");
            }

        }
        private void featureClassToFeatureClassToolStripMenuItem_Click(object sender, EventArgs e)
        {        
          
            ESRI.ArcGIS.ConversionTools.FeatureClassToFeatureClass featureClassToFeatureClass = new ESRI.ArcGIS.ConversionTools.FeatureClassToFeatureClass();
            featureClassToFeatureClass.in_features = fileGDBPath + "\\" + sourceFCName;
            featureClassToFeatureClass.out_path = sdePath;
            featureClassToFeatureClass.out_name = targetFCName;

            ExecuteGP(featureClassToFeatureClass as IGPProcess);          
           
            
        }

        private void featureClassToGeodatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {                       
            ESRI.ArcGIS.ConversionTools.FeatureClassToGeodatabase featureClassToGeodatabase = new ESRI.ArcGIS.ConversionTools.FeatureClassToGeodatabase();
            featureClassToGeodatabase.Input_Features = fileGDBPath + "\\" + sourceFCName;
            featureClassToGeodatabase.Output_Geodatabase = sdePath;           

            ExecuteGP(featureClassToGeodatabase as IGPProcess);    
        }

        private void copyFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {                  
            ESRI.ArcGIS.DataManagementTools.CopyFeatures copyFeatures = new ESRI.ArcGIS.DataManagementTools.CopyFeatures();
            copyFeatures.in_features = fileGDBPath + "\\" + sourceFCName;
            copyFeatures.out_feature_class = sdePath + "\\" + targetFCName;

            ExecuteGP(copyFeatures as IGPProcess);   
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {                    
            ESRI.ArcGIS.DataManagementTools.Copy copy = new ESRI.ArcGIS.DataManagementTools.Copy();
            copy.in_data = fileGDBPath + "\\" + sourceFCName;
            copy.out_data = sdePath + "\\" + targetFCName;

            ExecuteGP(copy as IGPProcess);   
        }
    }
}
