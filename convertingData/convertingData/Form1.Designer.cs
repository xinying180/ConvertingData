namespace convertingData
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.数据转换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iFeatureDataConverterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iGeoDBTransferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDatasetToXMLImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iExportOperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调用GPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.featureClassToFeatureClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.featureClassToGeodatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据转换ToolStripMenuItem,
            this.调用GPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(336, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 数据转换ToolStripMenuItem
            // 
            this.数据转换ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iFeatureDataConverterToolStripMenuItem,
            this.iGeoDBTransferToolStripMenuItem,
            this.exportDatasetToXMLImportToolStripMenuItem,
            this.iExportOperationToolStripMenuItem});
            this.数据转换ToolStripMenuItem.Name = "数据转换ToolStripMenuItem";
            this.数据转换ToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.数据转换ToolStripMenuItem.Text = "数据转换";
            // 
            // iFeatureDataConverterToolStripMenuItem
            // 
            this.iFeatureDataConverterToolStripMenuItem.Name = "iFeatureDataConverterToolStripMenuItem";
            this.iFeatureDataConverterToolStripMenuItem.Size = new System.Drawing.Size(275, 26);
            this.iFeatureDataConverterToolStripMenuItem.Text = "IFeatureDataConverter";
            this.iFeatureDataConverterToolStripMenuItem.Click += new System.EventHandler(this.iFeatureDataConverterToolStripMenuItem_Click);
            // 
            // iGeoDBTransferToolStripMenuItem
            // 
            this.iGeoDBTransferToolStripMenuItem.Name = "iGeoDBTransferToolStripMenuItem";
            this.iGeoDBTransferToolStripMenuItem.Size = new System.Drawing.Size(275, 26);
            this.iGeoDBTransferToolStripMenuItem.Text = "IGeoDBTransfer";
            this.iGeoDBTransferToolStripMenuItem.Click += new System.EventHandler(this.iGeoDBTransferToolStripMenuItem_Click);
            // 
            // exportDatasetToXMLImportToolStripMenuItem
            // 
            this.exportDatasetToXMLImportToolStripMenuItem.Name = "exportDatasetToXMLImportToolStripMenuItem";
            this.exportDatasetToXMLImportToolStripMenuItem.Size = new System.Drawing.Size(275, 26);
            this.exportDatasetToXMLImportToolStripMenuItem.Text = "ExportDatasetToXML_Import";
            this.exportDatasetToXMLImportToolStripMenuItem.Click += new System.EventHandler(this.exportDatasetToXMLImportToolStripMenuItem_Click);
            // 
            // iExportOperationToolStripMenuItem
            // 
            this.iExportOperationToolStripMenuItem.Name = "iExportOperationToolStripMenuItem";
            this.iExportOperationToolStripMenuItem.Size = new System.Drawing.Size(275, 26);
            this.iExportOperationToolStripMenuItem.Text = "IExportOperation";
            this.iExportOperationToolStripMenuItem.Click += new System.EventHandler(this.iExportOperationToolStripMenuItem_Click);
            // 
            // 调用GPToolStripMenuItem
            // 
            this.调用GPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.featureClassToFeatureClassToolStripMenuItem,
            this.featureClassToGeodatabaseToolStripMenuItem,
            this.copyFeaturesToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.调用GPToolStripMenuItem.Name = "调用GPToolStripMenuItem";
            this.调用GPToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.调用GPToolStripMenuItem.Text = "调用GP";
            // 
            // featureClassToFeatureClassToolStripMenuItem
            // 
            this.featureClassToFeatureClassToolStripMenuItem.Name = "featureClassToFeatureClassToolStripMenuItem";
            this.featureClassToFeatureClassToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.featureClassToFeatureClassToolStripMenuItem.Text = "FeatureClassToFeatureClass";
            this.featureClassToFeatureClassToolStripMenuItem.Click += new System.EventHandler(this.featureClassToFeatureClassToolStripMenuItem_Click);
            // 
            // featureClassToGeodatabaseToolStripMenuItem
            // 
            this.featureClassToGeodatabaseToolStripMenuItem.Name = "featureClassToGeodatabaseToolStripMenuItem";
            this.featureClassToGeodatabaseToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.featureClassToGeodatabaseToolStripMenuItem.Text = "FeatureClassToGeodatabase";
            this.featureClassToGeodatabaseToolStripMenuItem.Click += new System.EventHandler(this.featureClassToGeodatabaseToolStripMenuItem_Click);
            // 
            // copyFeaturesToolStripMenuItem
            // 
            this.copyFeaturesToolStripMenuItem.Name = "copyFeaturesToolStripMenuItem";
            this.copyFeaturesToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.copyFeaturesToolStripMenuItem.Text = "Copy Features";
            this.copyFeaturesToolStripMenuItem.Click += new System.EventHandler(this.copyFeaturesToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 329);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 数据转换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iFeatureDataConverterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iGeoDBTransferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDatasetToXMLImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iExportOperationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 调用GPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem featureClassToFeatureClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem featureClassToGeodatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFeaturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}

