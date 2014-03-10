namespace RCPA.Proteomics.Quantification.Srm
{
  partial class SrmFileModeForm
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SrmFileModeForm));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      this.mnuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuCrosslink = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSplit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuZgc = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSetScanEnabledOnly = new System.Windows.Forms.ToolStripMenuItem();
      this.dlgExport = new System.Windows.Forms.SaveFileDialog();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.tvMRM = new TreeViewMS.TreeViewMS();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.gvProductPair = new System.Windows.Forms.DataGridView();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.zgcPeptide = new ZedGraph.ZedGraphControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.zgcTransaction = new ZedGraph.ZedGraphControl();
      this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ProductIonEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.EnabledScanCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Light = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Heavy = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ratioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.headerCorrel = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.distanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.headerLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.headerHSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lightAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.heavyAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.noiseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dsProductIon = new System.Windows.Forms.BindingSource(this.components);
      this.dsScan = new System.Windows.Forms.BindingSource(this.components);
      this.viewAtCompoundModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuTree.SuspendLayout();
      this.mnuZgc.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvProductPair)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dsProductIon)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsScan)).BeginInit();
      this.SuspendLayout();
      // 
      // mnuTree
      // 
      this.mnuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCrosslink,
            this.mnuSplit,
            this.toolStripMenuItem1,
            this.mnuSave,
            this.viewAtCompoundModeToolStripMenuItem});
      this.mnuTree.Name = "contextMenuStrip1";
      this.mnuTree.Size = new System.Drawing.Size(224, 120);
      this.mnuTree.Opening += new System.ComponentModel.CancelEventHandler(this.mnuTree_Opening);
      // 
      // mnuCrosslink
      // 
      this.mnuCrosslink.Name = "mnuCrosslink";
      this.mnuCrosslink.Size = new System.Drawing.Size(223, 22);
      this.mnuCrosslink.Text = "Crosslink";
      this.mnuCrosslink.Click += new System.EventHandler(this.mnuCrosslink_Click);
      // 
      // mnuSplit
      // 
      this.mnuSplit.Name = "mnuSplit";
      this.mnuSplit.Size = new System.Drawing.Size(223, 22);
      this.mnuSplit.Text = "Split";
      this.mnuSplit.Click += new System.EventHandler(this.pmiSplit_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(220, 6);
      // 
      // mnuSave
      // 
      this.mnuSave.Name = "mnuSave";
      this.mnuSave.Size = new System.Drawing.Size(223, 22);
      this.mnuSave.Text = "Save";
      this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
      // 
      // mnuZgc
      // 
      this.mnuZgc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetScanEnabledOnly});
      this.mnuZgc.Name = "contextMenuStrip2";
      this.mnuZgc.Size = new System.Drawing.Size(218, 26);
      // 
      // mnuSetScanEnabledOnly
      // 
      this.mnuSetScanEnabledOnly.Name = "mnuSetScanEnabledOnly";
      this.mnuSetScanEnabledOnly.Size = new System.Drawing.Size(217, 22);
      this.mnuSetScanEnabledOnly.Text = "Set Selected Scans Valid";
      this.mnuSetScanEnabledOnly.Click += new System.EventHandler(this.mnuSetScanEnabledOnly_Click);
      // 
      // dlgExport
      // 
      this.dlgExport.DefaultExt = "ratio";
      this.dlgExport.Filter = "Peptide Ratio File(*.pepRatio)|*.pepRatio|All file(*.*)|*.*";
      this.dlgExport.Title = "Export MRM result to text format file ...";
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.DataPropertyName = "Light";
      this.dataGridViewTextBoxColumn1.HeaderText = "Light";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.DataPropertyName = "Heavy";
      this.dataGridViewTextBoxColumn2.HeaderText = "Heavy";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      // 
      // dataGridViewTextBoxColumn3
      // 
      this.dataGridViewTextBoxColumn3.DataPropertyName = "Light";
      this.dataGridViewTextBoxColumn3.HeaderText = "Light";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.Width = 150;
      // 
      // dataGridViewTextBoxColumn4
      // 
      this.dataGridViewTextBoxColumn4.DataPropertyName = "Heavy";
      this.dataGridViewTextBoxColumn4.HeaderText = "Heavy";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.Width = 150;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "100percentagesize.png");
      this.imageList1.Images.SetKeyName(1, "fullsize.png");
      this.imageList1.Images.SetKeyName(2, "heightsize.png");
      // 
      // dataGridViewTextBoxColumn5
      // 
      this.dataGridViewTextBoxColumn5.DataPropertyName = "Light";
      this.dataGridViewTextBoxColumn5.HeaderText = "Light";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      this.dataGridViewTextBoxColumn5.Width = 150;
      // 
      // dataGridViewTextBoxColumn6
      // 
      this.dataGridViewTextBoxColumn6.DataPropertyName = "Heavy";
      this.dataGridViewTextBoxColumn6.HeaderText = "Heavy";
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.dataGridViewTextBoxColumn6.ReadOnly = true;
      this.dataGridViewTextBoxColumn6.Width = 150;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.tvMRM);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
      this.splitContainer2.Size = new System.Drawing.Size(1123, 676);
      this.splitContainer2.SplitterDistance = 253;
      this.splitContainer2.TabIndex = 24;
      // 
      // tvMRM
      // 
      this.tvMRM.CheckBoxes = true;
      this.tvMRM.ContextMenuStrip = this.mnuTree;
      this.tvMRM.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMRM.FullRowSelect = true;
      this.tvMRM.HideSelection = false;
      this.tvMRM.Location = new System.Drawing.Point(0, 0);
      this.tvMRM.Name = "tvMRM";
      this.tvMRM.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("tvMRM.SelectedNodes")));
      this.tvMRM.Size = new System.Drawing.Size(253, 676);
      this.tvMRM.TabIndex = 19;
      this.tvMRM.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvMRM_AfterCheck);
      this.tvMRM.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMRM_AfterSelect);
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.gvProductPair);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.tabControl1);
      this.splitContainer3.Size = new System.Drawing.Size(866, 676);
      this.splitContainer3.SplitterDistance = 216;
      this.splitContainer3.TabIndex = 0;
      // 
      // gvProductPair
      // 
      this.gvProductPair.AllowUserToAddRows = false;
      this.gvProductPair.AllowUserToDeleteRows = false;
      this.gvProductPair.AutoGenerateColumns = false;
      this.gvProductPair.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvProductPair.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductIonEnabled,
            this.Light,
            this.Heavy,
            this.ratioDataGridViewTextBoxColumn,
            this.headerCorrel,
            this.EnabledScanCount,
            this.distanceDataGridViewTextBoxColumn,
            this.headerLSN,
            this.headerHSN,
            this.lightAreaDataGridViewTextBoxColumn,
            this.heavyAreaDataGridViewTextBoxColumn,
            this.noiseDataGridViewTextBoxColumn});
      this.gvProductPair.DataSource = this.dsProductIon;
      this.gvProductPair.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvProductPair.Location = new System.Drawing.Point(0, 0);
      this.gvProductPair.MultiSelect = false;
      this.gvProductPair.Name = "gvProductPair";
      this.gvProductPair.RowTemplate.Height = 23;
      this.gvProductPair.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvProductPair.ShowEditingIcon = false;
      this.gvProductPair.Size = new System.Drawing.Size(866, 216);
      this.gvProductPair.TabIndex = 23;
      this.gvProductPair.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gvProductPair_CellBeginEdit);
      this.gvProductPair.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvProductPair_CellFormatting);
      this.gvProductPair.SelectionChanged += new System.EventHandler(this.gvProductPair_SelectionChanged);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(866, 456);
      this.tabControl1.TabIndex = 24;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.zgcPeptide);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(858, 430);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Compound";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // zgcPeptide
      // 
      this.zgcPeptide.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcPeptide.Location = new System.Drawing.Point(3, 3);
      this.zgcPeptide.Name = "zgcPeptide";
      this.zgcPeptide.ScrollGrace = 0D;
      this.zgcPeptide.ScrollMaxX = 0D;
      this.zgcPeptide.ScrollMaxY = 0D;
      this.zgcPeptide.ScrollMaxY2 = 0D;
      this.zgcPeptide.ScrollMinX = 0D;
      this.zgcPeptide.ScrollMinY = 0D;
      this.zgcPeptide.ScrollMinY2 = 0D;
      this.zgcPeptide.Size = new System.Drawing.Size(852, 424);
      this.zgcPeptide.TabIndex = 25;
      this.zgcPeptide.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcPeptide_ContextMenuBuilder);
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.zgcTransaction);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(858, 430);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Transition";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // zgcTransaction
      // 
      this.zgcTransaction.BackColor = System.Drawing.Color.Transparent;
      this.zgcTransaction.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcTransaction.Location = new System.Drawing.Point(3, 3);
      this.zgcTransaction.Name = "zgcTransaction";
      this.zgcTransaction.ScrollGrace = 0D;
      this.zgcTransaction.ScrollMaxX = 0D;
      this.zgcTransaction.ScrollMaxY = 0D;
      this.zgcTransaction.ScrollMaxY2 = 0D;
      this.zgcTransaction.ScrollMinX = 0D;
      this.zgcTransaction.ScrollMinY = 0D;
      this.zgcTransaction.ScrollMinY2 = 0D;
      this.zgcTransaction.Size = new System.Drawing.Size(852, 424);
      this.zgcTransaction.TabIndex = 22;
      this.zgcTransaction.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcTransaction_ContextMenuBuilder);
      // 
      // dataGridViewTextBoxColumn7
      // 
      this.dataGridViewTextBoxColumn7.DataPropertyName = "Light";
      this.dataGridViewTextBoxColumn7.HeaderText = "Light";
      this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      this.dataGridViewTextBoxColumn7.Width = 150;
      // 
      // dataGridViewTextBoxColumn8
      // 
      this.dataGridViewTextBoxColumn8.DataPropertyName = "Heavy";
      this.dataGridViewTextBoxColumn8.HeaderText = "Heavy";
      this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
      this.dataGridViewTextBoxColumn8.Width = 150;
      // 
      // ProductIonEnabled
      // 
      this.ProductIonEnabled.DataPropertyName = "Enabled";
      this.ProductIonEnabled.HeaderText = "Enabled";
      this.ProductIonEnabled.Name = "ProductIonEnabled";
      this.ProductIonEnabled.Width = 50;
      // 
      // EnabledScanCount
      // 
      this.EnabledScanCount.DataPropertyName = "EnabledScanCount";
      this.EnabledScanCount.HeaderText = "ValidScan";
      this.EnabledScanCount.Name = "EnabledScanCount";
      this.EnabledScanCount.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn9
      // 
      this.dataGridViewTextBoxColumn9.DataPropertyName = "Light";
      this.dataGridViewTextBoxColumn9.HeaderText = "Light";
      this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
      this.dataGridViewTextBoxColumn9.Width = 150;
      // 
      // dataGridViewTextBoxColumn10
      // 
      this.dataGridViewTextBoxColumn10.DataPropertyName = "Heavy";
      this.dataGridViewTextBoxColumn10.HeaderText = "Heavy";
      this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
      this.dataGridViewTextBoxColumn10.Width = 150;
      // 
      // Light
      // 
      this.Light.DataPropertyName = "Light";
      this.Light.HeaderText = "Light";
      this.Light.Name = "Light";
      this.Light.Width = 150;
      // 
      // Heavy
      // 
      this.Heavy.DataPropertyName = "Heavy";
      this.Heavy.HeaderText = "Heavy";
      this.Heavy.Name = "Heavy";
      this.Heavy.Width = 150;
      // 
      // ratioDataGridViewTextBoxColumn
      // 
      this.ratioDataGridViewTextBoxColumn.DataPropertyName = "Ratio";
      dataGridViewCellStyle1.Format = "0.0000";
      this.ratioDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
      this.ratioDataGridViewTextBoxColumn.HeaderText = "Ratio";
      this.ratioDataGridViewTextBoxColumn.Name = "ratioDataGridViewTextBoxColumn";
      this.ratioDataGridViewTextBoxColumn.Width = 80;
      // 
      // headerCorrel
      // 
      this.headerCorrel.DataPropertyName = "RegressionCorrelation";
      dataGridViewCellStyle2.Format = "0.0000";
      this.headerCorrel.DefaultCellStyle = dataGridViewCellStyle2;
      this.headerCorrel.HeaderText = "Correl";
      this.headerCorrel.Name = "headerCorrel";
      this.headerCorrel.Width = 80;
      // 
      // distanceDataGridViewTextBoxColumn
      // 
      this.distanceDataGridViewTextBoxColumn.DataPropertyName = "Distance";
      dataGridViewCellStyle3.Format = "0.0000";
      this.distanceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
      this.distanceDataGridViewTextBoxColumn.HeaderText = "Distance";
      this.distanceDataGridViewTextBoxColumn.Name = "distanceDataGridViewTextBoxColumn";
      this.distanceDataGridViewTextBoxColumn.Width = 80;
      // 
      // headerLSN
      // 
      this.headerLSN.DataPropertyName = "LightSignalToNoise";
      dataGridViewCellStyle4.Format = "0.0000";
      this.headerLSN.DefaultCellStyle = dataGridViewCellStyle4;
      this.headerLSN.HeaderText = "Light(S/N)";
      this.headerLSN.Name = "headerLSN";
      this.headerLSN.ReadOnly = true;
      this.headerLSN.Width = 80;
      // 
      // headerHSN
      // 
      this.headerHSN.DataPropertyName = "HeavySignalToNoise";
      dataGridViewCellStyle5.Format = "0.0000";
      this.headerHSN.DefaultCellStyle = dataGridViewCellStyle5;
      this.headerHSN.HeaderText = "Heavy(S/N)";
      this.headerHSN.Name = "headerHSN";
      this.headerHSN.ReadOnly = true;
      this.headerHSN.Width = 80;
      // 
      // lightAreaDataGridViewTextBoxColumn
      // 
      this.lightAreaDataGridViewTextBoxColumn.DataPropertyName = "LightArea";
      dataGridViewCellStyle6.Format = "0.0";
      this.lightAreaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
      this.lightAreaDataGridViewTextBoxColumn.HeaderText = "Light(Area)";
      this.lightAreaDataGridViewTextBoxColumn.Name = "lightAreaDataGridViewTextBoxColumn";
      this.lightAreaDataGridViewTextBoxColumn.Width = 80;
      // 
      // heavyAreaDataGridViewTextBoxColumn
      // 
      this.heavyAreaDataGridViewTextBoxColumn.DataPropertyName = "HeavyArea";
      dataGridViewCellStyle7.Format = "0.0";
      this.heavyAreaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
      this.heavyAreaDataGridViewTextBoxColumn.HeaderText = "Heavy(Area)";
      this.heavyAreaDataGridViewTextBoxColumn.Name = "heavyAreaDataGridViewTextBoxColumn";
      this.heavyAreaDataGridViewTextBoxColumn.Width = 80;
      // 
      // noiseDataGridViewTextBoxColumn
      // 
      this.noiseDataGridViewTextBoxColumn.DataPropertyName = "Noise";
      dataGridViewCellStyle8.Format = "0.0";
      this.noiseDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
      this.noiseDataGridViewTextBoxColumn.HeaderText = "Noise";
      this.noiseDataGridViewTextBoxColumn.Name = "noiseDataGridViewTextBoxColumn";
      this.noiseDataGridViewTextBoxColumn.ReadOnly = true;
      this.noiseDataGridViewTextBoxColumn.Width = 80;
      // 
      // dsProductIon
      // 
      this.dsProductIon.DataSource = typeof(RCPA.Proteomics.Quantification.Srm.SrmPairedProductIon);
      // 
      // dsScan
      // 
      this.dsScan.DataSource = typeof(RCPA.Proteomics.Quantification.Srm.SrmScan);
      // 
      // viewAtCompoundModeToolStripMenuItem
      // 
      this.viewAtCompoundModeToolStripMenuItem.Name = "viewAtCompoundModeToolStripMenuItem";
      this.viewAtCompoundModeToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
      this.viewAtCompoundModeToolStripMenuItem.Text = "View at compound mode";
      this.viewAtCompoundModeToolStripMenuItem.Click += new System.EventHandler(this.viewAtCompoundModeToolStripMenuItem_Click);
      // 
      // SrmFileModeForm
      // 
      this.ClientSize = new System.Drawing.Size(1123, 676);
      this.Controls.Add(this.splitContainer2);
      this.KeyPreview = true;
      this.Name = "SrmFileModeForm";
      this.TabText = "File Mode";
      this.Text = "File Mode";
      this.mnuTree.ResumeLayout(false);
      this.mnuZgc.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvProductPair)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dsProductIon)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsScan)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip mnuTree;
    private System.Windows.Forms.ToolStripMenuItem mnuCrosslink;
    private System.Windows.Forms.ContextMenuStrip mnuZgc;
    private System.Windows.Forms.ToolStripMenuItem mnuSetScanEnabledOnly;
    private System.Windows.Forms.SaveFileDialog dlgExport;
    private System.Windows.Forms.BindingSource dsProductIon;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.BindingSource dsScan;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ToolStripMenuItem mnuSplit;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private TreeViewMS.TreeViewMS tvMRM;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.DataGridView gvProductPair;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage3;
    private ZedGraph.ZedGraphControl zgcTransaction;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem mnuSave;
    private ZedGraph.ZedGraphControl zgcPeptide;
    private System.Windows.Forms.DataGridViewCheckBoxColumn ProductIonEnabled;
    private System.Windows.Forms.DataGridViewTextBoxColumn Light;
    private System.Windows.Forms.DataGridViewTextBoxColumn Heavy;
    private System.Windows.Forms.DataGridViewTextBoxColumn ratioDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn headerCorrel;
    private System.Windows.Forms.DataGridViewTextBoxColumn EnabledScanCount;
    private System.Windows.Forms.DataGridViewTextBoxColumn distanceDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn headerLSN;
    private System.Windows.Forms.DataGridViewTextBoxColumn headerHSN;
    private System.Windows.Forms.DataGridViewTextBoxColumn lightAreaDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn heavyAreaDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn noiseDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
    private System.Windows.Forms.ToolStripMenuItem viewAtCompoundModeToolStripMenuItem;
  }
}
