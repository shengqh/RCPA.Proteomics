namespace RCPA.Proteomics.Quantification.Srm
{
  partial class SrmBrowserUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SrmBrowserUI));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuCrosslink = new System.Windows.Forms.ToolStripMenuItem();
      this.pmiSplit = new System.Windows.Forms.ToolStripMenuItem();
      this.tvMRM = new TreeViewMS.TreeViewMS();
      this.btnSaveCross = new System.Windows.Forms.Button();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSetScanEnabledOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.btnExport = new System.Windows.Forms.Button();
      this.dlgExport = new System.Windows.Forms.SaveFileDialog();
      this.gvProductPair = new System.Windows.Forms.DataGridView();
      this.ProductIonEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Light = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Heavy = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ratioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.distanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.headerCorrel = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.headerLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.headerHSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lightAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.heavyAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.noiseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dsProductIon = new System.Windows.Forms.BindingSource(this.components);
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.zgcPeptide = new ZedGraph.ZedGraphControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.zgcTransaction = new ZedGraph.ZedGraphControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.panel3 = new System.Windows.Forms.Panel();
      this.lightView = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.dsScan = new System.Windows.Forms.BindingSource(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.panel4 = new System.Windows.Forms.Panel();
      this.heavyView = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.panel2 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbFullHeight = new System.Windows.Forms.RadioButton();
      this.rbPercentage = new System.Windows.Forms.RadioButton();
      this.rbFullSize = new System.Windows.Forms.RadioButton();
      this.pnlFile.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.contextMenuStrip2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvProductPair)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsProductIon)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.lightView)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsScan)).BeginInit();
      this.panel1.SuspendLayout();
      this.panel4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.heavyView)).BeginInit();
      this.panel2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(31, 12);
      this.pnlFile.Size = new System.Drawing.Size(1057, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(259, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(798, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(259, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 598);
      this.lblProgress.Size = new System.Drawing.Size(1123, 21);
      this.lblProgress.Visible = false;
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 619);
      this.progressBar.Size = new System.Drawing.Size(1123, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(609, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(524, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(439, 7);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCrosslink,
            this.pmiSplit});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(130, 48);
      this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
      // 
      // mnuCrosslink
      // 
      this.mnuCrosslink.Name = "mnuCrosslink";
      this.mnuCrosslink.Size = new System.Drawing.Size(129, 22);
      this.mnuCrosslink.Text = "Crosslink";
      this.mnuCrosslink.Click += new System.EventHandler(this.mnuCrosslink_Click);
      // 
      // pmiSplit
      // 
      this.pmiSplit.Name = "pmiSplit";
      this.pmiSplit.Size = new System.Drawing.Size(129, 22);
      this.pmiSplit.Text = "Split";
      this.pmiSplit.Click += new System.EventHandler(this.pmiSplit_Click);
      // 
      // tvMRM
      // 
      this.tvMRM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.tvMRM.CheckBoxes = true;
      this.tvMRM.ContextMenuStrip = this.contextMenuStrip1;
      this.tvMRM.FullRowSelect = true;
      this.tvMRM.HideSelection = false;
      this.tvMRM.Location = new System.Drawing.Point(31, 63);
      this.tvMRM.Name = "tvMRM";
      this.tvMRM.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("tvMRM.SelectedNodes")));
      this.tvMRM.Size = new System.Drawing.Size(259, 518);
      this.tvMRM.TabIndex = 18;
      this.tvMRM.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvMRM_AfterCheck);
      this.tvMRM.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMRM_AfterSelect);
      // 
      // btnSaveCross
      // 
      this.btnSaveCross.Location = new System.Drawing.Point(696, 633);
      this.btnSaveCross.Name = "btnSaveCross";
      this.btnSaveCross.Size = new System.Drawing.Size(75, 23);
      this.btnSaveCross.TabIndex = 19;
      this.btnSaveCross.Text = "&Save";
      this.btnSaveCross.UseVisualStyleBackColor = true;
      this.btnSaveCross.Click += new System.EventHandler(this.btnSaveCross_Click);
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetScanEnabledOnlyToolStripMenuItem});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.contextMenuStrip2.Size = new System.Drawing.Size(234, 26);
      // 
      // mnuSetScanEnabledOnlyToolStripMenuItem
      // 
      this.mnuSetScanEnabledOnlyToolStripMenuItem.Name = "mnuSetScanEnabledOnlyToolStripMenuItem";
      this.mnuSetScanEnabledOnlyToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.mnuSetScanEnabledOnlyToolStripMenuItem.Text = "Set selected scans enabled";
      this.mnuSetScanEnabledOnlyToolStripMenuItem.Click += new System.EventHandler(this.mnuSetScanEnabledOnlyToolStripMenuItem_Click);
      // 
      // btnExport
      // 
      this.btnExport.Location = new System.Drawing.Point(777, 633);
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(75, 23);
      this.btnExport.TabIndex = 21;
      this.btnExport.Text = "&Export";
      this.btnExport.UseVisualStyleBackColor = true;
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // dlgExport
      // 
      this.dlgExport.DefaultExt = "ratio";
      this.dlgExport.Filter = "Peptide Ratio File(*.pepRatio)|*.pepRatio|All file(*.*)|*.*";
      this.dlgExport.Title = "Export MRM result to text format file ...";
      // 
      // gvProductPair
      // 
      this.gvProductPair.AllowUserToAddRows = false;
      this.gvProductPair.AllowUserToDeleteRows = false;
      this.gvProductPair.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gvProductPair.AutoGenerateColumns = false;
      this.gvProductPair.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvProductPair.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductIonEnabled,
            this.Light,
            this.Heavy,
            this.ratioDataGridViewTextBoxColumn,
            this.distanceDataGridViewTextBoxColumn,
            this.headerCorrel,
            this.headerLSN,
            this.headerHSN,
            this.lightAreaDataGridViewTextBoxColumn,
            this.heavyAreaDataGridViewTextBoxColumn,
            this.noiseDataGridViewTextBoxColumn});
      this.gvProductPair.DataSource = this.dsProductIon;
      this.gvProductPair.Location = new System.Drawing.Point(296, 63);
      this.gvProductPair.MultiSelect = false;
      this.gvProductPair.Name = "gvProductPair";
      this.gvProductPair.RowTemplate.Height = 23;
      this.gvProductPair.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvProductPair.ShowEditingIcon = false;
      this.gvProductPair.Size = new System.Drawing.Size(792, 164);
      this.gvProductPair.TabIndex = 22;
      this.gvProductPair.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvProductPair_CellFormatting);
      this.gvProductPair.SelectionChanged += new System.EventHandler(this.gvProductPair_SelectionChanged);
      // 
      // ProductIonEnabled
      // 
      this.ProductIonEnabled.DataPropertyName = "Enabled";
      this.ProductIonEnabled.HeaderText = "Enabled";
      this.ProductIonEnabled.Name = "ProductIonEnabled";
      this.ProductIonEnabled.Width = 50;
      // 
      // Light
      // 
      this.Light.DataPropertyName = "Light";
      this.Light.HeaderText = "Light";
      this.Light.Name = "Light";
      this.Light.ReadOnly = true;
      this.Light.Width = 150;
      // 
      // Heavy
      // 
      this.Heavy.DataPropertyName = "Heavy";
      this.Heavy.HeaderText = "Heavy";
      this.Heavy.Name = "Heavy";
      this.Heavy.ReadOnly = true;
      this.Heavy.Width = 150;
      // 
      // ratioDataGridViewTextBoxColumn
      // 
      this.ratioDataGridViewTextBoxColumn.DataPropertyName = "Ratio";
      dataGridViewCellStyle1.Format = "0.0000";
      this.ratioDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
      this.ratioDataGridViewTextBoxColumn.HeaderText = "Ratio";
      this.ratioDataGridViewTextBoxColumn.Name = "ratioDataGridViewTextBoxColumn";
      this.ratioDataGridViewTextBoxColumn.ReadOnly = true;
      this.ratioDataGridViewTextBoxColumn.Width = 80;
      // 
      // distanceDataGridViewTextBoxColumn
      // 
      this.distanceDataGridViewTextBoxColumn.DataPropertyName = "Distance";
      dataGridViewCellStyle2.Format = "0.0000";
      this.distanceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
      this.distanceDataGridViewTextBoxColumn.HeaderText = "Distance";
      this.distanceDataGridViewTextBoxColumn.Name = "distanceDataGridViewTextBoxColumn";
      this.distanceDataGridViewTextBoxColumn.ReadOnly = true;
      this.distanceDataGridViewTextBoxColumn.Width = 80;
      // 
      // headerCorrel
      // 
      this.headerCorrel.DataPropertyName = "RegressionCorrelation";
      dataGridViewCellStyle3.Format = "0.0000";
      this.headerCorrel.DefaultCellStyle = dataGridViewCellStyle3;
      this.headerCorrel.HeaderText = "Correl";
      this.headerCorrel.Name = "headerCorrel";
      this.headerCorrel.ReadOnly = true;
      this.headerCorrel.Width = 80;
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
      this.lightAreaDataGridViewTextBoxColumn.ReadOnly = true;
      this.lightAreaDataGridViewTextBoxColumn.Width = 80;
      // 
      // heavyAreaDataGridViewTextBoxColumn
      // 
      this.heavyAreaDataGridViewTextBoxColumn.DataPropertyName = "HeavyArea";
      dataGridViewCellStyle7.Format = "0.0";
      this.heavyAreaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
      this.heavyAreaDataGridViewTextBoxColumn.HeaderText = "Heavy(Area)";
      this.heavyAreaDataGridViewTextBoxColumn.Name = "heavyAreaDataGridViewTextBoxColumn";
      this.heavyAreaDataGridViewTextBoxColumn.ReadOnly = true;
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
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Location = new System.Drawing.Point(296, 281);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(792, 300);
      this.tabControl1.TabIndex = 23;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.zgcPeptide);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(784, 274);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Peptide";
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
      this.zgcPeptide.Size = new System.Drawing.Size(778, 268);
      this.zgcPeptide.TabIndex = 23;
      this.zgcPeptide.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcScan_ContextMenuBuilder);
      this.zgcPeptide.ZoomEvent += new ZedGraph.ZedGraphControl.ZoomEventHandler(this.zgcScan_ZoomEvent);
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.zgcTransaction);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(784, 274);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Transaction";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // zgcTransaction
      // 
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
      this.zgcTransaction.Size = new System.Drawing.Size(778, 268);
      this.zgcTransaction.TabIndex = 22;
      this.zgcTransaction.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcScan_ContextMenuBuilder);
      this.zgcTransaction.ZoomEvent += new ZedGraph.ZedGraphControl.ZoomEventHandler(this.zgcScan_ZoomEvent);
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.splitContainer1);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(784, 274);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Transaction data";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(3, 3);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.AutoScroll = true;
      this.splitContainer1.Panel1.Controls.Add(this.panel3);
      this.splitContainer1.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.AutoScroll = true;
      this.splitContainer1.Panel2.Controls.Add(this.panel4);
      this.splitContainer1.Panel2.Controls.Add(this.panel2);
      this.splitContainer1.Size = new System.Drawing.Size(778, 268);
      this.splitContainer1.SplitterDistance = 388;
      this.splitContainer1.TabIndex = 0;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.lightView);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 25);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(388, 243);
      this.panel3.TabIndex = 2;
      // 
      // lightView
      // 
      this.lightView.AutoGenerateColumns = false;
      this.lightView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.lightView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewCheckBoxColumn1});
      this.lightView.DataSource = this.dsScan;
      this.lightView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lightView.Location = new System.Drawing.Point(0, 0);
      this.lightView.MultiSelect = false;
      this.lightView.Name = "lightView";
      this.lightView.ReadOnly = true;
      this.lightView.RowTemplate.Height = 23;
      this.lightView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.lightView.Size = new System.Drawing.Size(388, 243);
      this.lightView.TabIndex = 1;
      // 
      // dataGridViewTextBoxColumn7
      // 
      this.dataGridViewTextBoxColumn7.DataPropertyName = "RetentionTime";
      this.dataGridViewTextBoxColumn7.HeaderText = "RetentionTime";
      this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      this.dataGridViewTextBoxColumn7.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn8
      // 
      this.dataGridViewTextBoxColumn8.DataPropertyName = "Intensity";
      this.dataGridViewTextBoxColumn8.HeaderText = "Intensity";
      this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
      this.dataGridViewTextBoxColumn8.ReadOnly = true;
      // 
      // dataGridViewCheckBoxColumn1
      // 
      this.dataGridViewCheckBoxColumn1.DataPropertyName = "Enabled";
      this.dataGridViewCheckBoxColumn1.HeaderText = "Enabled";
      this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
      this.dataGridViewCheckBoxColumn1.ReadOnly = true;
      // 
      // dsScan
      // 
      this.dsScan.DataSource = typeof(RCPA.Proteomics.Quantification.Srm.SrmScan);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(388, 25);
      this.panel1.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(107, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "Light transaction";
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.heavyView);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel4.Location = new System.Drawing.Point(0, 25);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(386, 243);
      this.panel4.TabIndex = 3;
      // 
      // heavyView
      // 
      this.heavyView.AutoGenerateColumns = false;
      this.heavyView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.heavyView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewCheckBoxColumn2});
      this.heavyView.DataSource = this.dsScan;
      this.heavyView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.heavyView.Location = new System.Drawing.Point(0, 0);
      this.heavyView.MultiSelect = false;
      this.heavyView.Name = "heavyView";
      this.heavyView.ReadOnly = true;
      this.heavyView.RowTemplate.Height = 23;
      this.heavyView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.heavyView.Size = new System.Drawing.Size(386, 243);
      this.heavyView.TabIndex = 1;
      // 
      // dataGridViewTextBoxColumn11
      // 
      this.dataGridViewTextBoxColumn11.DataPropertyName = "RetentionTime";
      this.dataGridViewTextBoxColumn11.HeaderText = "RetentionTime";
      this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
      this.dataGridViewTextBoxColumn11.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn12
      // 
      this.dataGridViewTextBoxColumn12.DataPropertyName = "Intensity";
      this.dataGridViewTextBoxColumn12.HeaderText = "Intensity";
      this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
      this.dataGridViewTextBoxColumn12.ReadOnly = true;
      // 
      // dataGridViewCheckBoxColumn2
      // 
      this.dataGridViewCheckBoxColumn2.DataPropertyName = "Enabled";
      this.dataGridViewCheckBoxColumn2.HeaderText = "Enabled";
      this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
      this.dataGridViewCheckBoxColumn2.ReadOnly = true;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.label2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(386, 25);
      this.panel2.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 6);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 12);
      this.label2.TabIndex = 0;
      this.label2.Text = "Heavy transaction";
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
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.rbFullHeight);
      this.groupBox1.Controls.Add(this.rbPercentage);
      this.groupBox1.Controls.Add(this.rbFullSize);
      this.groupBox1.Location = new System.Drawing.Point(296, 231);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(792, 45);
      this.groupBox1.TabIndex = 24;
      this.groupBox1.TabStop = false;
      // 
      // rbFullHeight
      // 
      this.rbFullHeight.Appearance = System.Windows.Forms.Appearance.Button;
      this.rbFullHeight.AutoSize = true;
      this.rbFullHeight.ImageIndex = 2;
      this.rbFullHeight.ImageList = this.imageList1;
      this.rbFullHeight.Location = new System.Drawing.Point(37, 6);
      this.rbFullHeight.Name = "rbFullHeight";
      this.rbFullHeight.Size = new System.Drawing.Size(38, 38);
      this.rbFullHeight.TabIndex = 2;
      this.rbFullHeight.UseVisualStyleBackColor = true;
      this.rbFullHeight.Click += new System.EventHandler(this.rbFullHeight_Click);
      // 
      // rbPercentage
      // 
      this.rbPercentage.Appearance = System.Windows.Forms.Appearance.Button;
      this.rbPercentage.AutoSize = true;
      this.rbPercentage.Checked = true;
      this.rbPercentage.ImageIndex = 0;
      this.rbPercentage.ImageList = this.imageList1;
      this.rbPercentage.Location = new System.Drawing.Point(74, 6);
      this.rbPercentage.Name = "rbPercentage";
      this.rbPercentage.Size = new System.Drawing.Size(38, 38);
      this.rbPercentage.TabIndex = 1;
      this.rbPercentage.TabStop = true;
      this.rbPercentage.UseVisualStyleBackColor = true;
      this.rbPercentage.Click += new System.EventHandler(this.rbPercentage_Click);
      // 
      // rbFullSize
      // 
      this.rbFullSize.Appearance = System.Windows.Forms.Appearance.Button;
      this.rbFullSize.AutoSize = true;
      this.rbFullSize.ImageIndex = 1;
      this.rbFullSize.ImageList = this.imageList1;
      this.rbFullSize.Location = new System.Drawing.Point(0, 6);
      this.rbFullSize.Name = "rbFullSize";
      this.rbFullSize.Size = new System.Drawing.Size(38, 38);
      this.rbFullSize.TabIndex = 0;
      this.rbFullSize.UseVisualStyleBackColor = true;
      this.rbFullSize.Click += new System.EventHandler(this.rbFullSize_Click);
      // 
      // SrmBrowserUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1123, 676);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.gvProductPair);
      this.Controls.Add(this.btnExport);
      this.Controls.Add(this.btnSaveCross);
      this.Controls.Add(this.tvMRM);
      this.KeyPreview = true;
      this.Name = "SrmBrowserUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.tvMRM, 0);
      this.Controls.SetChildIndex(this.btnSaveCross, 0);
      this.Controls.SetChildIndex(this.btnExport, 0);
      this.Controls.SetChildIndex(this.gvProductPair, 0);
      this.Controls.SetChildIndex(this.tabControl1, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.contextMenuStrip2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvProductPair)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsProductIon)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.lightView)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsScan)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.heavyView)).EndInit();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnuCrosslink;
    private TreeViewMS.TreeViewMS tvMRM;
    private System.Windows.Forms.Button btnSaveCross;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem mnuSetScanEnabledOnlyToolStripMenuItem;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.SaveFileDialog dlgExport;
    private System.Windows.Forms.DataGridView gvProductPair;
    private System.Windows.Forms.BindingSource dsProductIon;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.BindingSource dsScan;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TabPage tabPage3;
    private ZedGraph.ZedGraphControl zgcTransaction;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.DataGridView lightView;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.DataGridView heavyView;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
    private System.Windows.Forms.DataGridViewCheckBoxColumn ProductIonEnabled;
    private System.Windows.Forms.DataGridViewTextBoxColumn Light;
    private System.Windows.Forms.DataGridViewTextBoxColumn Heavy;
    private System.Windows.Forms.DataGridViewTextBoxColumn ratioDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn distanceDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn headerCorrel;
    private System.Windows.Forms.DataGridViewTextBoxColumn headerLSN;
    private System.Windows.Forms.DataGridViewTextBoxColumn headerHSN;
    private System.Windows.Forms.DataGridViewTextBoxColumn lightAreaDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn heavyAreaDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn noiseDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ToolStripMenuItem pmiSplit;
    private ZedGraph.ZedGraphControl zgcPeptide;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rbFullHeight;
    private System.Windows.Forms.RadioButton rbPercentage;
    private System.Windows.Forms.RadioButton rbFullSize;
  }
}
