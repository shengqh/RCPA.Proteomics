namespace RCPA.Proteomics.Quantification.Srm
{
  partial class SrmValidatorUI
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dlgExport = new System.Windows.Forms.SaveFileDialog();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.mainMenu = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOpenMrms = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSaveChanges = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExportResult = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExportCompound = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExportTransition = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExportHtml = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExportStatistic = new System.Windows.Forms.ToolStripMenuItem();
      this.tsm3 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuDecoy = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuValidOnly = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuGreenLine = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHighlightCurrent = new System.Windows.Forms.ToolStripMenuItem();
      this.tsm1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuFullSize = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFullHeight = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPerfectSize = new System.Windows.Forms.ToolStripMenuItem();
      this.tsm2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuCompoundMode = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileMode = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOption = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuRatioByArea = new System.Windows.Forms.ToolStripMenuItem();
      this.dockPanel1 = new DigitalRune.Windows.Docking.DockPanel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.gvCompounds = new System.Windows.Forms.DataGridView();
      this.CompoundEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.objectNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.precursurFormulaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lightMzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.heavyMzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.compoundItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.tcMode = new System.Windows.Forms.TabControl();
      this.tabFileMode = new System.Windows.Forms.TabPage();
      this.splitContainer4 = new System.Windows.Forms.SplitContainer();
      this.gvProductPair = new System.Windows.Forms.DataGridView();
      this.ProductIonEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Light = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Heavy = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ratioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.headerCorrel = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.EnabledScanCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.distanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.headerLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.headerHSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lightAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.heavyAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.noiseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dsProductIon = new System.Windows.Forms.BindingSource(this.components);
      this.tabControl2 = new System.Windows.Forms.TabControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.zgcFileModePeptide = new ZedGraph.ZedGraphControl();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.zgcFileModeTransaction = new ZedGraph.ZedGraphControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.zgcAllInOne = new ZedGraph.ZedGraphControl();
      this.tcFiles = new System.Windows.Forms.TabControl();
      this.tabCompoundMode = new System.Windows.Forms.TabPage();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.gvFiles = new System.Windows.Forms.DataGridView();
      this.tcCompoundGraph = new System.Windows.Forms.TabControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.zgcRetentionTime = new ZedGraph.ZedGraphControl();
      this.zgcCompoundModePeptide = new ZedGraph.ZedGraphControl();
      this.panel1 = new System.Windows.Forms.Panel();
      this.barProgress = new System.Windows.Forms.ProgressBar();
      this.lblProgress = new System.Windows.Forms.Label();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.tsView = new System.Windows.Forms.ToolStrip();
      this.btnFullSize = new System.Windows.Forms.ToolStripButton();
      this.btnFullHeight = new System.Windows.Forms.ToolStripButton();
      this.btnPerfectSize = new System.Windows.Forms.ToolStripButton();
      this.tssView = new System.Windows.Forms.ToolStripSeparator();
      this.btnCompoundMode = new System.Windows.Forms.ToolStripButton();
      this.btnFileMode = new System.Windows.Forms.ToolStripButton();
      this.tsFile = new System.Windows.Forms.ToolStrip();
      this.btnOpenMrms = new System.Windows.Forms.ToolStripButton();
      this.btnSaveChanges = new System.Windows.Forms.ToolStripButton();
      this.btnClose = new System.Windows.Forms.ToolStripButton();
      this.mnuZgc = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSetScanEnabledOnly = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
      this.mainMenu.SuspendLayout();
      this.dockPanel1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvCompounds)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.compoundItemBindingSource)).BeginInit();
      this.tcMode.SuspendLayout();
      this.tabFileMode.SuspendLayout();
      this.splitContainer4.Panel1.SuspendLayout();
      this.splitContainer4.Panel2.SuspendLayout();
      this.splitContainer4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvProductPair)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsProductIon)).BeginInit();
      this.tabControl2.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabCompoundMode.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).BeginInit();
      this.tcCompoundGraph.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.panel1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.tsView.SuspendLayout();
      this.tsFile.SuspendLayout();
      this.mnuZgc.SuspendLayout();
      this.SuspendLayout();
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
      // mainMenu
      // 
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuOption});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(1123, 25);
      this.mainMenu.TabIndex = 23;
      this.mainMenu.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenMrms,
            this.mnuSaveChanges,
            this.mnuExportResult,
            this.tsm3,
            this.mnuClose});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(39, 21);
      this.mnuFile.Text = "File";
      this.mnuFile.DropDownOpening += new System.EventHandler(this.mnuFile_DropDownOpening);
      // 
      // mnuOpenMrms
      // 
      this.mnuOpenMrms.Image = global::RCPA.Properties.Resources.fileopen;
      this.mnuOpenMrms.Name = "mnuOpenMrms";
      this.mnuOpenMrms.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.mnuOpenMrms.Size = new System.Drawing.Size(201, 22);
      this.mnuOpenMrms.Text = "Open SRM...";
      this.mnuOpenMrms.Click += new System.EventHandler(this.mnuOpenMrms_Click);
      // 
      // mnuSaveChanges
      // 
      this.mnuSaveChanges.Image = global::RCPA.Properties.Resources.save;
      this.mnuSaveChanges.Name = "mnuSaveChanges";
      this.mnuSaveChanges.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.mnuSaveChanges.Size = new System.Drawing.Size(201, 22);
      this.mnuSaveChanges.Text = "Save Changes";
      this.mnuSaveChanges.Click += new System.EventHandler(this.mnuSaveChanges_Click);
      // 
      // mnuExportResult
      // 
      this.mnuExportResult.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportCompound,
            this.mnuExportTransition,
            this.mnuExportHtml,
            this.mnuExportStatistic});
      this.mnuExportResult.Name = "mnuExportResult";
      this.mnuExportResult.Size = new System.Drawing.Size(201, 22);
      this.mnuExportResult.Text = "Export";
      // 
      // mnuExportCompound
      // 
      this.mnuExportCompound.Name = "mnuExportCompound";
      this.mnuExportCompound.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
      this.mnuExportCompound.Size = new System.Drawing.Size(194, 22);
      this.mnuExportCompound.Text = "Compound...";
      this.mnuExportCompound.Click += new System.EventHandler(this.mnuExportCompound_Click);
      // 
      // mnuExportTransition
      // 
      this.mnuExportTransition.Name = "mnuExportTransition";
      this.mnuExportTransition.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
      this.mnuExportTransition.Size = new System.Drawing.Size(194, 22);
      this.mnuExportTransition.Text = "Transition...";
      this.mnuExportTransition.Click += new System.EventHandler(this.mnuExportTransition_Click);
      // 
      // mnuExportHtml
      // 
      this.mnuExportHtml.Name = "mnuExportHtml";
      this.mnuExportHtml.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
      this.mnuExportHtml.Size = new System.Drawing.Size(194, 22);
      this.mnuExportHtml.Text = "Html...";
      this.mnuExportHtml.Click += new System.EventHandler(this.mnuExportHtml_Click);
      // 
      // mnuExportStatistic
      // 
      this.mnuExportStatistic.Name = "mnuExportStatistic";
      this.mnuExportStatistic.Size = new System.Drawing.Size(194, 22);
      this.mnuExportStatistic.Text = "Sample Statistic...";
      this.mnuExportStatistic.Click += new System.EventHandler(this.mnuExportStatistic_Click);
      // 
      // tsm3
      // 
      this.tsm3.Name = "tsm3";
      this.tsm3.Size = new System.Drawing.Size(198, 6);
      // 
      // mnuClose
      // 
      this.mnuClose.Image = global::RCPA.Properties.Resources.close;
      this.mnuClose.Name = "mnuClose";
      this.mnuClose.Size = new System.Drawing.Size(201, 22);
      this.mnuClose.Text = "Close";
      this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
      // 
      // mnuView
      // 
      this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDecoy,
            this.mnuValidOnly,
            this.mnuGreenLine,
            this.mnuHighlightCurrent,
            this.tsm1,
            this.mnuFullSize,
            this.mnuFullHeight,
            this.mnuPerfectSize,
            this.tsm2,
            this.mnuCompoundMode,
            this.mnuFileMode});
      this.mnuView.Name = "mnuView";
      this.mnuView.Size = new System.Drawing.Size(47, 21);
      this.mnuView.Text = "View";
      this.mnuView.DropDownOpening += new System.EventHandler(this.mnuView_DropDownOpening);
      // 
      // mnuDecoy
      // 
      this.mnuDecoy.Name = "mnuDecoy";
      this.mnuDecoy.Size = new System.Drawing.Size(236, 22);
      this.mnuDecoy.Text = "Decoy";
      this.mnuDecoy.Click += new System.EventHandler(this.mnuDecoy_Click);
      // 
      // mnuValidOnly
      // 
      this.mnuValidOnly.Name = "mnuValidOnly";
      this.mnuValidOnly.Size = new System.Drawing.Size(236, 22);
      this.mnuValidOnly.Text = "Valid Only";
      this.mnuValidOnly.Click += new System.EventHandler(this.mnuValidOnly_Click);
      // 
      // mnuGreenLine
      // 
      this.mnuGreenLine.Name = "mnuGreenLine";
      this.mnuGreenLine.Size = new System.Drawing.Size(236, 22);
      this.mnuGreenLine.Text = "90% Green Line";
      this.mnuGreenLine.Click += new System.EventHandler(this.mnuGreenLine_Click);
      // 
      // mnuHighlightCurrent
      // 
      this.mnuHighlightCurrent.Name = "mnuHighlightCurrent";
      this.mnuHighlightCurrent.Size = new System.Drawing.Size(236, 22);
      this.mnuHighlightCurrent.Text = "Highlight Current Transition";
      this.mnuHighlightCurrent.Click += new System.EventHandler(this.mnuHighlightCurrent_Click);
      // 
      // tsm1
      // 
      this.tsm1.Name = "tsm1";
      this.tsm1.Size = new System.Drawing.Size(233, 6);
      // 
      // mnuFullSize
      // 
      this.mnuFullSize.Image = global::RCPA.Properties.Resources.fullSize;
      this.mnuFullSize.Name = "mnuFullSize";
      this.mnuFullSize.Size = new System.Drawing.Size(236, 22);
      this.mnuFullSize.Text = "Full Size";
      this.mnuFullSize.Click += new System.EventHandler(this.rbFullSize_Click);
      // 
      // mnuFullHeight
      // 
      this.mnuFullHeight.Image = global::RCPA.Properties.Resources.fullHeight;
      this.mnuFullHeight.Name = "mnuFullHeight";
      this.mnuFullHeight.Size = new System.Drawing.Size(236, 22);
      this.mnuFullHeight.Text = "Full Height";
      this.mnuFullHeight.Click += new System.EventHandler(this.rbFullHeight_Click);
      // 
      // mnuPerfectSize
      // 
      this.mnuPerfectSize.Image = global::RCPA.Properties.Resources.perfectSize;
      this.mnuPerfectSize.Name = "mnuPerfectSize";
      this.mnuPerfectSize.Size = new System.Drawing.Size(236, 22);
      this.mnuPerfectSize.Text = "Perfect Size";
      this.mnuPerfectSize.Click += new System.EventHandler(this.rbPercentage_Click);
      // 
      // tsm2
      // 
      this.tsm2.Name = "tsm2";
      this.tsm2.Size = new System.Drawing.Size(233, 6);
      // 
      // mnuCompoundMode
      // 
      this.mnuCompoundMode.Image = global::RCPA.Properties.Resources.compoundMode;
      this.mnuCompoundMode.Name = "mnuCompoundMode";
      this.mnuCompoundMode.Size = new System.Drawing.Size(236, 22);
      this.mnuCompoundMode.Tag = "0";
      this.mnuCompoundMode.Text = "Compound Mode";
      this.mnuCompoundMode.Click += new System.EventHandler(this.mnuCompoundMode_Click);
      // 
      // mnuFileMode
      // 
      this.mnuFileMode.Image = global::RCPA.Properties.Resources.fileMode;
      this.mnuFileMode.Name = "mnuFileMode";
      this.mnuFileMode.Size = new System.Drawing.Size(236, 22);
      this.mnuFileMode.Tag = "1";
      this.mnuFileMode.Text = "File Mode";
      this.mnuFileMode.Click += new System.EventHandler(this.mnuFileMode_Click);
      // 
      // mnuOption
      // 
      this.mnuOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRatioByArea});
      this.mnuOption.Name = "mnuOption";
      this.mnuOption.Size = new System.Drawing.Size(60, 21);
      this.mnuOption.Text = "Option";
      this.mnuOption.DropDownOpening += new System.EventHandler(this.mnuOption_DropDownOpening);
      // 
      // mnuRatioByArea
      // 
      this.mnuRatioByArea.Name = "mnuRatioByArea";
      this.mnuRatioByArea.Size = new System.Drawing.Size(154, 22);
      this.mnuRatioByArea.Text = "Ratio by area";
      this.mnuRatioByArea.Click += new System.EventHandler(this.mnuRatioByArea_Click);
      // 
      // dockPanel1
      // 
      this.dockPanel1.ActiveAutoHideContent = null;
      this.dockPanel1.Controls.Add(this.splitContainer1);
      this.dockPanel1.Controls.Add(this.panel1);
      this.dockPanel1.Controls.Add(this.toolStripContainer1);
      this.dockPanel1.DefaultFloatingWindowSize = new System.Drawing.Size(300, 300);
      this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dockPanel1.Location = new System.Drawing.Point(0, 25);
      this.dockPanel1.Name = "dockPanel1";
      this.dockPanel1.Size = new System.Drawing.Size(1123, 493);
      this.dockPanel1.TabIndex = 24;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 38);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.gvCompounds);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tcMode);
      this.splitContainer1.Size = new System.Drawing.Size(1123, 433);
      this.splitContainer1.SplitterDistance = 358;
      this.splitContainer1.TabIndex = 10;
      // 
      // gvCompounds
      // 
      this.gvCompounds.AutoGenerateColumns = false;
      this.gvCompounds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvCompounds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompoundEnabled,
            this.objectNameDataGridViewTextBoxColumn,
            this.precursurFormulaDataGridViewTextBoxColumn,
            this.lightMzDataGridViewTextBoxColumn,
            this.heavyMzDataGridViewTextBoxColumn});
      this.gvCompounds.DataSource = this.compoundItemBindingSource;
      this.gvCompounds.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvCompounds.Location = new System.Drawing.Point(0, 0);
      this.gvCompounds.MultiSelect = false;
      this.gvCompounds.Name = "gvCompounds";
      this.gvCompounds.ReadOnly = true;
      this.gvCompounds.RowTemplate.Height = 23;
      this.gvCompounds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvCompounds.Size = new System.Drawing.Size(358, 433);
      this.gvCompounds.TabIndex = 0;
      this.gvCompounds.SelectionChanged += new System.EventHandler(this.gvCompounds_SelectionChanged);
      // 
      // CompoundEnabled
      // 
      this.CompoundEnabled.DataPropertyName = "Enabled";
      this.CompoundEnabled.HeaderText = "";
      this.CompoundEnabled.Name = "CompoundEnabled";
      this.CompoundEnabled.ReadOnly = true;
      this.CompoundEnabled.Width = 20;
      // 
      // objectNameDataGridViewTextBoxColumn
      // 
      this.objectNameDataGridViewTextBoxColumn.DataPropertyName = "ObjectName";
      this.objectNameDataGridViewTextBoxColumn.HeaderText = "Name";
      this.objectNameDataGridViewTextBoxColumn.Name = "objectNameDataGridViewTextBoxColumn";
      this.objectNameDataGridViewTextBoxColumn.ReadOnly = true;
      this.objectNameDataGridViewTextBoxColumn.Width = 80;
      // 
      // precursurFormulaDataGridViewTextBoxColumn
      // 
      this.precursurFormulaDataGridViewTextBoxColumn.DataPropertyName = "PrecursurFormula";
      this.precursurFormulaDataGridViewTextBoxColumn.HeaderText = "Formula";
      this.precursurFormulaDataGridViewTextBoxColumn.Name = "precursurFormulaDataGridViewTextBoxColumn";
      this.precursurFormulaDataGridViewTextBoxColumn.ReadOnly = true;
      this.precursurFormulaDataGridViewTextBoxColumn.Width = 80;
      // 
      // lightMzDataGridViewTextBoxColumn
      // 
      this.lightMzDataGridViewTextBoxColumn.DataPropertyName = "LightMz";
      dataGridViewCellStyle1.NullValue = null;
      this.lightMzDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
      this.lightMzDataGridViewTextBoxColumn.HeaderText = "Light";
      this.lightMzDataGridViewTextBoxColumn.Name = "lightMzDataGridViewTextBoxColumn";
      this.lightMzDataGridViewTextBoxColumn.ReadOnly = true;
      this.lightMzDataGridViewTextBoxColumn.Width = 60;
      // 
      // heavyMzDataGridViewTextBoxColumn
      // 
      this.heavyMzDataGridViewTextBoxColumn.DataPropertyName = "HeavyMz";
      this.heavyMzDataGridViewTextBoxColumn.HeaderText = "Heavy";
      this.heavyMzDataGridViewTextBoxColumn.Name = "heavyMzDataGridViewTextBoxColumn";
      this.heavyMzDataGridViewTextBoxColumn.ReadOnly = true;
      this.heavyMzDataGridViewTextBoxColumn.Width = 60;
      // 
      // compoundItemBindingSource
      // 
      this.compoundItemBindingSource.DataSource = typeof(RCPA.Proteomics.Quantification.Srm.CompoundItem);
      // 
      // tcMode
      // 
      this.tcMode.Controls.Add(this.tabFileMode);
      this.tcMode.Controls.Add(this.tabCompoundMode);
      this.tcMode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tcMode.Location = new System.Drawing.Point(0, 0);
      this.tcMode.Name = "tcMode";
      this.tcMode.SelectedIndex = 0;
      this.tcMode.Size = new System.Drawing.Size(761, 433);
      this.tcMode.TabIndex = 0;
      // 
      // tabFileMode
      // 
      this.tabFileMode.Controls.Add(this.splitContainer4);
      this.tabFileMode.Controls.Add(this.tcFiles);
      this.tabFileMode.Location = new System.Drawing.Point(4, 22);
      this.tabFileMode.Name = "tabFileMode";
      this.tabFileMode.Padding = new System.Windows.Forms.Padding(3);
      this.tabFileMode.Size = new System.Drawing.Size(753, 407);
      this.tabFileMode.TabIndex = 0;
      this.tabFileMode.Text = "FileMode";
      this.tabFileMode.UseVisualStyleBackColor = true;
      // 
      // splitContainer4
      // 
      this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer4.Location = new System.Drawing.Point(3, 31);
      this.splitContainer4.Name = "splitContainer4";
      this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer4.Panel1
      // 
      this.splitContainer4.Panel1.Controls.Add(this.gvProductPair);
      // 
      // splitContainer4.Panel2
      // 
      this.splitContainer4.Panel2.Controls.Add(this.tabControl2);
      this.splitContainer4.Size = new System.Drawing.Size(747, 373);
      this.splitContainer4.SplitterDistance = 216;
      this.splitContainer4.TabIndex = 1;
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
      this.gvProductPair.Size = new System.Drawing.Size(747, 216);
      this.gvProductPair.TabIndex = 23;
      this.gvProductPair.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gvProductPair_CellBeginEdit);
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
      dataGridViewCellStyle2.Format = "0.0000";
      this.ratioDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
      this.ratioDataGridViewTextBoxColumn.HeaderText = "Ratio";
      this.ratioDataGridViewTextBoxColumn.Name = "ratioDataGridViewTextBoxColumn";
      this.ratioDataGridViewTextBoxColumn.Width = 80;
      // 
      // headerCorrel
      // 
      this.headerCorrel.DataPropertyName = "RegressionCorrelation";
      dataGridViewCellStyle3.Format = "0.0000";
      this.headerCorrel.DefaultCellStyle = dataGridViewCellStyle3;
      this.headerCorrel.HeaderText = "Correl";
      this.headerCorrel.Name = "headerCorrel";
      this.headerCorrel.Width = 80;
      // 
      // EnabledScanCount
      // 
      this.EnabledScanCount.DataPropertyName = "EnabledScanCount";
      this.EnabledScanCount.HeaderText = "ValidScan";
      this.EnabledScanCount.Name = "EnabledScanCount";
      this.EnabledScanCount.ReadOnly = true;
      // 
      // distanceDataGridViewTextBoxColumn
      // 
      this.distanceDataGridViewTextBoxColumn.DataPropertyName = "Distance";
      dataGridViewCellStyle4.Format = "0.0000";
      this.distanceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
      this.distanceDataGridViewTextBoxColumn.HeaderText = "Distance";
      this.distanceDataGridViewTextBoxColumn.Name = "distanceDataGridViewTextBoxColumn";
      this.distanceDataGridViewTextBoxColumn.Width = 80;
      // 
      // headerLSN
      // 
      this.headerLSN.DataPropertyName = "LightSignalToNoise";
      dataGridViewCellStyle5.Format = "0.0000";
      this.headerLSN.DefaultCellStyle = dataGridViewCellStyle5;
      this.headerLSN.HeaderText = "Light(S/N)";
      this.headerLSN.Name = "headerLSN";
      this.headerLSN.ReadOnly = true;
      this.headerLSN.Width = 80;
      // 
      // headerHSN
      // 
      this.headerHSN.DataPropertyName = "HeavySignalToNoise";
      dataGridViewCellStyle6.Format = "0.0000";
      this.headerHSN.DefaultCellStyle = dataGridViewCellStyle6;
      this.headerHSN.HeaderText = "Heavy(S/N)";
      this.headerHSN.Name = "headerHSN";
      this.headerHSN.ReadOnly = true;
      this.headerHSN.Width = 80;
      // 
      // lightAreaDataGridViewTextBoxColumn
      // 
      this.lightAreaDataGridViewTextBoxColumn.DataPropertyName = "LightArea";
      dataGridViewCellStyle7.Format = "0.0";
      this.lightAreaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
      this.lightAreaDataGridViewTextBoxColumn.HeaderText = "Light(Area)";
      this.lightAreaDataGridViewTextBoxColumn.Name = "lightAreaDataGridViewTextBoxColumn";
      this.lightAreaDataGridViewTextBoxColumn.Width = 80;
      // 
      // heavyAreaDataGridViewTextBoxColumn
      // 
      this.heavyAreaDataGridViewTextBoxColumn.DataPropertyName = "HeavyArea";
      dataGridViewCellStyle8.Format = "0.0";
      this.heavyAreaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
      this.heavyAreaDataGridViewTextBoxColumn.HeaderText = "Heavy(Area)";
      this.heavyAreaDataGridViewTextBoxColumn.Name = "heavyAreaDataGridViewTextBoxColumn";
      this.heavyAreaDataGridViewTextBoxColumn.Width = 80;
      // 
      // noiseDataGridViewTextBoxColumn
      // 
      this.noiseDataGridViewTextBoxColumn.DataPropertyName = "Noise";
      dataGridViewCellStyle9.Format = "0.0";
      this.noiseDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
      this.noiseDataGridViewTextBoxColumn.HeaderText = "Noise";
      this.noiseDataGridViewTextBoxColumn.Name = "noiseDataGridViewTextBoxColumn";
      this.noiseDataGridViewTextBoxColumn.ReadOnly = true;
      this.noiseDataGridViewTextBoxColumn.Width = 80;
      // 
      // dsProductIon
      // 
      this.dsProductIon.DataSource = typeof(RCPA.Proteomics.Quantification.Srm.SrmPairedProductIon);
      // 
      // tabControl2
      // 
      this.tabControl2.Controls.Add(this.tabPage2);
      this.tabControl2.Controls.Add(this.tabPage4);
      this.tabControl2.Controls.Add(this.tabPage1);
      this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl2.Location = new System.Drawing.Point(0, 0);
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new System.Drawing.Size(747, 153);
      this.tabControl2.TabIndex = 24;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.zgcFileModePeptide);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(739, 127);
      this.tabPage2.TabIndex = 0;
      this.tabPage2.Text = "Compound";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // zgcFileModePeptide
      // 
      this.zgcFileModePeptide.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcFileModePeptide.Location = new System.Drawing.Point(3, 3);
      this.zgcFileModePeptide.Name = "zgcFileModePeptide";
      this.zgcFileModePeptide.ScrollGrace = 0D;
      this.zgcFileModePeptide.ScrollMaxX = 0D;
      this.zgcFileModePeptide.ScrollMaxY = 0D;
      this.zgcFileModePeptide.ScrollMaxY2 = 0D;
      this.zgcFileModePeptide.ScrollMinX = 0D;
      this.zgcFileModePeptide.ScrollMinY = 0D;
      this.zgcFileModePeptide.ScrollMinY2 = 0D;
      this.zgcFileModePeptide.Size = new System.Drawing.Size(733, 121);
      this.zgcFileModePeptide.TabIndex = 25;
      this.zgcFileModePeptide.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcPeptide_ContextMenuBuilder);
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.zgcFileModeTransaction);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(739, 127);
      this.tabPage4.TabIndex = 2;
      this.tabPage4.Text = "Current Transition";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // zgcFileModeTransaction
      // 
      this.zgcFileModeTransaction.BackColor = System.Drawing.Color.Transparent;
      this.zgcFileModeTransaction.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcFileModeTransaction.Location = new System.Drawing.Point(3, 3);
      this.zgcFileModeTransaction.Name = "zgcFileModeTransaction";
      this.zgcFileModeTransaction.ScrollGrace = 0D;
      this.zgcFileModeTransaction.ScrollMaxX = 0D;
      this.zgcFileModeTransaction.ScrollMaxY = 0D;
      this.zgcFileModeTransaction.ScrollMaxY2 = 0D;
      this.zgcFileModeTransaction.ScrollMinX = 0D;
      this.zgcFileModeTransaction.ScrollMinY = 0D;
      this.zgcFileModeTransaction.ScrollMinY2 = 0D;
      this.zgcFileModeTransaction.Size = new System.Drawing.Size(733, 121);
      this.zgcFileModeTransaction.TabIndex = 22;
      this.zgcFileModeTransaction.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcTransaction_ContextMenuBuilder);
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.zgcAllInOne);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(739, 127);
      this.tabPage1.TabIndex = 3;
      this.tabPage1.Text = "All in one";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // zgcAllInOne
      // 
      this.zgcAllInOne.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcAllInOne.Location = new System.Drawing.Point(3, 3);
      this.zgcAllInOne.Name = "zgcAllInOne";
      this.zgcAllInOne.ScrollGrace = 0D;
      this.zgcAllInOne.ScrollMaxX = 0D;
      this.zgcAllInOne.ScrollMaxY = 0D;
      this.zgcAllInOne.ScrollMaxY2 = 0D;
      this.zgcAllInOne.ScrollMinX = 0D;
      this.zgcAllInOne.ScrollMinY = 0D;
      this.zgcAllInOne.ScrollMinY2 = 0D;
      this.zgcAllInOne.Size = new System.Drawing.Size(733, 121);
      this.zgcAllInOne.TabIndex = 26;
      // 
      // tcFiles
      // 
      this.tcFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.tcFiles.Location = new System.Drawing.Point(3, 3);
      this.tcFiles.Name = "tcFiles";
      this.tcFiles.SelectedIndex = 0;
      this.tcFiles.Size = new System.Drawing.Size(747, 28);
      this.tcFiles.TabIndex = 0;
      this.tcFiles.SelectedIndexChanged += new System.EventHandler(this.tcFiles_SelectedIndexChanged);
      // 
      // tabCompoundMode
      // 
      this.tabCompoundMode.Controls.Add(this.splitContainer2);
      this.tabCompoundMode.Location = new System.Drawing.Point(4, 22);
      this.tabCompoundMode.Name = "tabCompoundMode";
      this.tabCompoundMode.Padding = new System.Windows.Forms.Padding(3);
      this.tabCompoundMode.Size = new System.Drawing.Size(753, 407);
      this.tabCompoundMode.TabIndex = 1;
      this.tabCompoundMode.Text = "CompoundMode";
      this.tabCompoundMode.UseVisualStyleBackColor = true;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.Location = new System.Drawing.Point(3, 3);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.gvFiles);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.tcCompoundGraph);
      this.splitContainer2.Size = new System.Drawing.Size(747, 401);
      this.splitContainer2.SplitterDistance = 188;
      this.splitContainer2.TabIndex = 1;
      // 
      // gvFiles
      // 
      this.gvFiles.AllowUserToAddRows = false;
      this.gvFiles.AllowUserToDeleteRows = false;
      this.gvFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.gvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvFiles.Location = new System.Drawing.Point(0, 0);
      this.gvFiles.MultiSelect = false;
      this.gvFiles.Name = "gvFiles";
      this.gvFiles.RowTemplate.Height = 23;
      this.gvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvFiles.Size = new System.Drawing.Size(747, 188);
      this.gvFiles.TabIndex = 4;
      this.gvFiles.VirtualMode = true;
      this.gvFiles.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gvFiles_CellBeginEdit);
      this.gvFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvFiles_CellDoubleClick);
      this.gvFiles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvFiles_CellFormatting);
      this.gvFiles.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gvFiles_CellValueNeeded);
      this.gvFiles.SelectionChanged += new System.EventHandler(this.gvFiles_SelectionChanged);
      // 
      // tcCompoundGraph
      // 
      this.tcCompoundGraph.Controls.Add(this.tabPage3);
      this.tcCompoundGraph.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tcCompoundGraph.Location = new System.Drawing.Point(0, 0);
      this.tcCompoundGraph.Name = "tcCompoundGraph";
      this.tcCompoundGraph.SelectedIndex = 0;
      this.tcCompoundGraph.Size = new System.Drawing.Size(747, 209);
      this.tcCompoundGraph.TabIndex = 31;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.splitContainer3);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(739, 183);
      this.tabPage3.TabIndex = 0;
      this.tabPage3.Text = "Peptide";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.Location = new System.Drawing.Point(3, 3);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.zgcRetentionTime);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.zgcCompoundModePeptide);
      this.splitContainer3.Size = new System.Drawing.Size(733, 177);
      this.splitContainer3.SplitterDistance = 88;
      this.splitContainer3.TabIndex = 24;
      // 
      // zgcRetentionTime
      // 
      this.zgcRetentionTime.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcRetentionTime.Location = new System.Drawing.Point(0, 0);
      this.zgcRetentionTime.Name = "zgcRetentionTime";
      this.zgcRetentionTime.ScrollGrace = 0D;
      this.zgcRetentionTime.ScrollMaxX = 0D;
      this.zgcRetentionTime.ScrollMaxY = 0D;
      this.zgcRetentionTime.ScrollMaxY2 = 0D;
      this.zgcRetentionTime.ScrollMinX = 0D;
      this.zgcRetentionTime.ScrollMinY = 0D;
      this.zgcRetentionTime.ScrollMinY2 = 0D;
      this.zgcRetentionTime.Size = new System.Drawing.Size(733, 88);
      this.zgcRetentionTime.TabIndex = 25;
      // 
      // zgcCompoundModePeptide
      // 
      this.zgcCompoundModePeptide.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcCompoundModePeptide.Location = new System.Drawing.Point(0, 0);
      this.zgcCompoundModePeptide.Name = "zgcCompoundModePeptide";
      this.zgcCompoundModePeptide.ScrollGrace = 0D;
      this.zgcCompoundModePeptide.ScrollMaxX = 0D;
      this.zgcCompoundModePeptide.ScrollMaxY = 0D;
      this.zgcCompoundModePeptide.ScrollMaxY2 = 0D;
      this.zgcCompoundModePeptide.ScrollMinX = 0D;
      this.zgcCompoundModePeptide.ScrollMinY = 0D;
      this.zgcCompoundModePeptide.ScrollMinY2 = 0D;
      this.zgcCompoundModePeptide.Size = new System.Drawing.Size(733, 85);
      this.zgcCompoundModePeptide.TabIndex = 24;
      this.zgcCompoundModePeptide.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcPeptide_ContextMenuBuilder);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.barProgress);
      this.panel1.Controls.Add(this.lblProgress);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 471);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1123, 22);
      this.panel1.TabIndex = 8;
      // 
      // barProgress
      // 
      this.barProgress.Dock = System.Windows.Forms.DockStyle.Right;
      this.barProgress.Location = new System.Drawing.Point(766, 0);
      this.barProgress.Name = "barProgress";
      this.barProgress.Size = new System.Drawing.Size(357, 22);
      this.barProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.barProgress.TabIndex = 1;
      // 
      // lblProgress
      // 
      this.lblProgress.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblProgress.Location = new System.Drawing.Point(0, 0);
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(1123, 22);
      this.lblProgress.TabIndex = 0;
      // 
      // toolStripContainer1
      // 
      this.toolStripContainer1.BottomToolStripPanelVisible = false;
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.toolStripContainer1.ContentPanel.Controls.Add(this.tsView);
      this.toolStripContainer1.ContentPanel.Controls.Add(this.tsFile);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1123, 38);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Top;
      this.toolStripContainer1.LeftToolStripPanelVisible = false;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(1123, 38);
      this.toolStripContainer1.TabIndex = 9;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.toolStripContainer1.TopToolStripPanelVisible = false;
      // 
      // tsView
      // 
      this.tsView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tsView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullSize,
            this.btnFullHeight,
            this.btnPerfectSize,
            this.tssView,
            this.btnCompoundMode,
            this.btnFileMode});
      this.tsView.Location = new System.Drawing.Point(313, 0);
      this.tsView.Name = "tsView";
      this.tsView.Size = new System.Drawing.Size(808, 36);
      this.tsView.TabIndex = 7;
      this.tsView.Text = "toolStrip1";
      // 
      // btnFullSize
      // 
      this.btnFullSize.Image = global::RCPA.Properties.Resources.fullSize;
      this.btnFullSize.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFullSize.Name = "btnFullSize";
      this.btnFullSize.Size = new System.Drawing.Size(74, 33);
      this.btnFullSize.Text = "Full Size";
      this.btnFullSize.Click += new System.EventHandler(this.rbFullSize_Click);
      // 
      // btnFullHeight
      // 
      this.btnFullHeight.Image = global::RCPA.Properties.Resources.fullHeight;
      this.btnFullHeight.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFullHeight.Name = "btnFullHeight";
      this.btnFullHeight.Size = new System.Drawing.Size(89, 33);
      this.btnFullHeight.Text = "Full Height";
      this.btnFullHeight.Click += new System.EventHandler(this.rbFullHeight_Click);
      // 
      // btnPerfectSize
      // 
      this.btnPerfectSize.Checked = true;
      this.btnPerfectSize.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnPerfectSize.Image = global::RCPA.Properties.Resources.perfectSize;
      this.btnPerfectSize.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnPerfectSize.Name = "btnPerfectSize";
      this.btnPerfectSize.Size = new System.Drawing.Size(95, 33);
      this.btnPerfectSize.Text = "Perfect Size";
      this.btnPerfectSize.Click += new System.EventHandler(this.rbPercentage_Click);
      // 
      // tssView
      // 
      this.tssView.Name = "tssView";
      this.tssView.Size = new System.Drawing.Size(6, 36);
      // 
      // btnCompoundMode
      // 
      this.btnCompoundMode.CheckOnClick = true;
      this.btnCompoundMode.Image = global::RCPA.Properties.Resources.compoundMode;
      this.btnCompoundMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCompoundMode.Name = "btnCompoundMode";
      this.btnCompoundMode.Size = new System.Drawing.Size(132, 33);
      this.btnCompoundMode.Text = "Compound Mode";
      this.btnCompoundMode.Click += new System.EventHandler(this.btnFileMode_Click);
      // 
      // btnFileMode
      // 
      this.btnFileMode.Checked = true;
      this.btnFileMode.CheckOnClick = true;
      this.btnFileMode.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnFileMode.Image = global::RCPA.Properties.Resources.fileMode;
      this.btnFileMode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFileMode.Name = "btnFileMode";
      this.btnFileMode.Size = new System.Drawing.Size(86, 33);
      this.btnFileMode.Text = "File Mode";
      this.btnFileMode.Click += new System.EventHandler(this.btnFileMode_Click);
      // 
      // tsFile
      // 
      this.tsFile.Dock = System.Windows.Forms.DockStyle.Left;
      this.tsFile.ImageScalingSize = new System.Drawing.Size(26, 26);
      this.tsFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenMrms,
            this.btnSaveChanges,
            this.btnClose});
      this.tsFile.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.tsFile.Location = new System.Drawing.Point(0, 0);
      this.tsFile.Name = "tsFile";
      this.tsFile.Size = new System.Drawing.Size(313, 36);
      this.tsFile.TabIndex = 6;
      this.tsFile.Text = "toolStrip1";
      // 
      // btnOpenMrms
      // 
      this.btnOpenMrms.Image = global::RCPA.Properties.Resources.fileopen;
      this.btnOpenMrms.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOpenMrms.Name = "btnOpenMrms";
      this.btnOpenMrms.Size = new System.Drawing.Size(112, 33);
      this.btnOpenMrms.Text = "Open Files ...";
      this.btnOpenMrms.ToolTipText = "Open Mrm Results...";
      this.btnOpenMrms.Click += new System.EventHandler(this.mnuOpenMrms_Click);
      // 
      // btnSaveChanges
      // 
      this.btnSaveChanges.Enabled = false;
      this.btnSaveChanges.Image = global::RCPA.Properties.Resources.save;
      this.btnSaveChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSaveChanges.Name = "btnSaveChanges";
      this.btnSaveChanges.Size = new System.Drawing.Size(119, 33);
      this.btnSaveChanges.Text = "Save Changes";
      this.btnSaveChanges.Click += new System.EventHandler(this.mnuSaveChanges_Click);
      // 
      // btnClose
      // 
      this.btnClose.Image = global::RCPA.Properties.Resources.close;
      this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(70, 33);
      this.btnClose.Text = "Close";
      this.btnClose.Click += new System.EventHandler(this.mnuClose_Click);
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
      // mnuExport
      // 
      this.mnuExport.Name = "mnuExport";
      this.mnuExport.Size = new System.Drawing.Size(201, 22);
      this.mnuExport.Text = "Export";
      // 
      // SrmValidator2UI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1123, 518);
      this.Controls.Add(this.dockPanel1);
      this.Controls.Add(this.mainMenu);
      this.IsMdiContainer = true;
      this.KeyPreview = true;
      this.MainMenuStrip = this.mainMenu;
      this.Name = "SrmValidator2UI";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SrmValidator2UI_FormClosing);
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.dockPanel1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvCompounds)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.compoundItemBindingSource)).EndInit();
      this.tcMode.ResumeLayout(false);
      this.tabFileMode.ResumeLayout(false);
      this.splitContainer4.Panel1.ResumeLayout(false);
      this.splitContainer4.Panel2.ResumeLayout(false);
      this.splitContainer4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvProductPair)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsProductIon)).EndInit();
      this.tabControl2.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabCompoundMode.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).EndInit();
      this.tcCompoundGraph.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.tsView.ResumeLayout(false);
      this.tsView.PerformLayout();
      this.tsFile.ResumeLayout(false);
      this.tsFile.PerformLayout();
      this.mnuZgc.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.SaveFileDialog dlgExport;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private System.Windows.Forms.MenuStrip mainMenu;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuOpenMrms;
    private DigitalRune.Windows.Docking.DockPanel dockPanel1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ProgressBar barProgress;
    private System.Windows.Forms.Label lblProgress;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip tsFile;
    private System.Windows.Forms.ToolStripButton btnFullSize;
    private System.Windows.Forms.ToolStripButton btnFullHeight;
    private System.Windows.Forms.ToolStripButton btnPerfectSize;
    private System.Windows.Forms.ToolStripButton btnOpenMrms;
    private System.Windows.Forms.ToolStripMenuItem mnuExportResult;
    private System.Windows.Forms.ToolStripMenuItem mnuSaveChanges;
    private System.Windows.Forms.ToolStripMenuItem mnuClose;
    private System.Windows.Forms.ToolStripMenuItem mnuExportCompound;
    private System.Windows.Forms.ToolStripMenuItem mnuExportTransition;
    private System.Windows.Forms.ToolStripMenuItem mnuExportHtml;
    private System.Windows.Forms.ToolStripMenuItem mnuExportStatistic;
    private System.Windows.Forms.ToolStripButton btnCompoundMode;
    private System.Windows.Forms.ToolStripButton btnFileMode;
    private System.Windows.Forms.ToolStripButton btnSaveChanges;
    private System.Windows.Forms.ToolStripMenuItem mnuView;
    private System.Windows.Forms.ToolStripMenuItem mnuDecoy;
    private System.Windows.Forms.ToolStripMenuItem mnuCompoundMode;
    private System.Windows.Forms.ToolStripMenuItem mnuFileMode;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.DataGridView gvCompounds;
    private System.Windows.Forms.BindingSource compoundItemBindingSource;
    private System.Windows.Forms.TabControl tcMode;
    private System.Windows.Forms.TabPage tabFileMode;
    private System.Windows.Forms.TabControl tcFiles;
    private System.Windows.Forms.TabPage tabCompoundMode;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.DataGridView gvFiles;
    private System.Windows.Forms.TabControl tcCompoundGraph;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private ZedGraph.ZedGraphControl zgcRetentionTime;
    private ZedGraph.ZedGraphControl zgcCompoundModePeptide;
    private System.Windows.Forms.SplitContainer splitContainer4;
    private System.Windows.Forms.DataGridView gvProductPair;
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
    private System.Windows.Forms.BindingSource dsProductIon;
    private System.Windows.Forms.TabControl tabControl2;
    private System.Windows.Forms.TabPage tabPage2;
    private ZedGraph.ZedGraphControl zgcFileModePeptide;
    private System.Windows.Forms.TabPage tabPage4;
    private ZedGraph.ZedGraphControl zgcFileModeTransaction;
    private System.Windows.Forms.ContextMenuStrip mnuZgc;
    private System.Windows.Forms.ToolStripMenuItem mnuSetScanEnabledOnly;
    private System.Windows.Forms.DataGridViewCheckBoxColumn CompoundEnabled;
    private System.Windows.Forms.DataGridViewTextBoxColumn objectNameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn precursurFormulaDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn lightMzDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn heavyMzDataGridViewTextBoxColumn;
    private System.Windows.Forms.ToolStripMenuItem mnuExport;
    private System.Windows.Forms.ToolStripSeparator tsm2;
    private System.Windows.Forms.ToolStripSeparator tsm1;
    private System.Windows.Forms.ToolStripMenuItem mnuFullSize;
    private System.Windows.Forms.ToolStripMenuItem mnuFullHeight;
    private System.Windows.Forms.ToolStripMenuItem mnuPerfectSize;
    private System.Windows.Forms.ToolStripButton btnClose;
    private System.Windows.Forms.ToolStripSeparator tssView;
    private System.Windows.Forms.ToolStrip tsView;
    private System.Windows.Forms.ToolStripSeparator tsm3;
    private System.Windows.Forms.ToolStripMenuItem mnuGreenLine;
    private System.Windows.Forms.ToolStripMenuItem mnuOption;
    private System.Windows.Forms.ToolStripMenuItem mnuRatioByArea;
    private System.Windows.Forms.ToolStripMenuItem mnuValidOnly;
    private System.Windows.Forms.TabPage tabPage1;
    private ZedGraph.ZedGraphControl zgcAllInOne;
    private System.Windows.Forms.ToolStripMenuItem mnuHighlightCurrent;
  }
}
