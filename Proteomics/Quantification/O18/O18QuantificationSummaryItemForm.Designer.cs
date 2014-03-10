namespace RCPA.Proteomics.Quantification.O18
{
  partial class O18QuantificationSummaryItemForm
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.txtPostDigestionLabeling = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.txtLabellingEfficiency = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.txtRegressionCorrelation = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.btnClose = new System.Windows.Forms.Button();
      this.txtRatio = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtPurityOfO18Water = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtCompositionFormula = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtPeptideSequence = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.lvExperimentalScans = new System.Windows.Forms.ListView();
      this.chScan = new System.Windows.Forms.ColumnHeader();
      this.chScanO161 = new System.Windows.Forms.ColumnHeader();
      this.chScanO162 = new System.Windows.Forms.ColumnHeader();
      this.chScanO163 = new System.Windows.Forms.ColumnHeader();
      this.chScanO164 = new System.Windows.Forms.ColumnHeader();
      this.chScanO165 = new System.Windows.Forms.ColumnHeader();
      this.chScanO166 = new System.Windows.Forms.ColumnHeader();
      this.zgcExperimentalIndividualScan = new ZedGraph.ZedGraphControl();
      this.zgcExperimentalScans = new ZedGraph.ZedGraphControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.splitContainer8 = new System.Windows.Forms.SplitContainer();
      this.zgcRatio = new ZedGraph.ZedGraphControl();
      this.zgcTheoreticalIsotopic = new ZedGraph.ZedGraphControl();
      this.splitContainer4 = new System.Windows.Forms.SplitContainer();
      this.lvRegression = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.zgcRegression = new ZedGraph.ZedGraphControl();
      this.panel3 = new System.Windows.Forms.Panel();
      this.label6 = new System.Windows.Forms.Label();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSetScanEnabled = new System.Windows.Forms.ToolStripMenuItem();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.splitContainer8.Panel1.SuspendLayout();
      this.splitContainer8.Panel2.SuspendLayout();
      this.splitContainer8.SuspendLayout();
      this.splitContainer4.Panel1.SuspendLayout();
      this.splitContainer4.Panel2.SuspendLayout();
      this.splitContainer4.SuspendLayout();
      this.panel3.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.txtPostDigestionLabeling);
      this.panel1.Controls.Add(this.label8);
      this.panel1.Controls.Add(this.btnSave);
      this.panel1.Controls.Add(this.txtLabellingEfficiency);
      this.panel1.Controls.Add(this.label7);
      this.panel1.Controls.Add(this.txtRegressionCorrelation);
      this.panel1.Controls.Add(this.label5);
      this.panel1.Controls.Add(this.btnClose);
      this.panel1.Controls.Add(this.txtRatio);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.txtPurityOfO18Water);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.txtCompositionFormula);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.txtPeptideSequence);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(777, 183);
      this.panel1.TabIndex = 1;
      // 
      // txtPostDigestionLabeling
      // 
      this.txtPostDigestionLabeling.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPostDigestionLabeling.Location = new System.Drawing.Point(170, 81);
      this.txtPostDigestionLabeling.Name = "txtPostDigestionLabeling";
      this.txtPostDigestionLabeling.ReadOnly = true;
      this.txtPostDigestionLabeling.Size = new System.Drawing.Size(514, 21);
      this.txtPostDigestionLabeling.TabIndex = 15;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(12, 84);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(143, 12);
      this.label8.TabIndex = 14;
      this.label8.Text = "Post Digestion Labeling";
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(704, 9);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(66, 69);
      this.btnSave.TabIndex = 13;
      this.btnSave.Text = "&Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // txtLabellingEfficiency
      // 
      this.txtLabellingEfficiency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLabellingEfficiency.Location = new System.Drawing.Point(170, 106);
      this.txtLabellingEfficiency.Name = "txtLabellingEfficiency";
      this.txtLabellingEfficiency.ReadOnly = true;
      this.txtLabellingEfficiency.Size = new System.Drawing.Size(514, 21);
      this.txtLabellingEfficiency.TabIndex = 12;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(30, 109);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(125, 12);
      this.label7.TabIndex = 11;
      this.label7.Text = "Labelling Efficiency";
      // 
      // txtRegressionCorrelation
      // 
      this.txtRegressionCorrelation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRegressionCorrelation.Location = new System.Drawing.Point(170, 131);
      this.txtRegressionCorrelation.Name = "txtRegressionCorrelation";
      this.txtRegressionCorrelation.ReadOnly = true;
      this.txtRegressionCorrelation.Size = new System.Drawing.Size(514, 21);
      this.txtRegressionCorrelation.TabIndex = 10;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(18, 134);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(137, 12);
      this.label5.TabIndex = 9;
      this.label5.Text = "Regression Correlation";
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(704, 108);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(66, 69);
      this.btnClose.TabIndex = 8;
      this.btnClose.Text = "&Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // txtRatio
      // 
      this.txtRatio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRatio.Location = new System.Drawing.Point(170, 156);
      this.txtRatio.Name = "txtRatio";
      this.txtRatio.ReadOnly = true;
      this.txtRatio.Size = new System.Drawing.Size(514, 21);
      this.txtRatio.TabIndex = 7;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(96, 159);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(59, 12);
      this.label4.TabIndex = 6;
      this.label4.Text = "O18 : O16";
      // 
      // txtPurityOfO18Water
      // 
      this.txtPurityOfO18Water.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPurityOfO18Water.Location = new System.Drawing.Point(170, 57);
      this.txtPurityOfO18Water.Name = "txtPurityOfO18Water";
      this.txtPurityOfO18Water.ReadOnly = true;
      this.txtPurityOfO18Water.Size = new System.Drawing.Size(514, 21);
      this.txtPurityOfO18Water.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(54, 60);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(101, 12);
      this.label3.TabIndex = 4;
      this.label3.Text = "O18 Water Purity";
      // 
      // txtCompositionFormula
      // 
      this.txtCompositionFormula.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtCompositionFormula.Location = new System.Drawing.Point(170, 33);
      this.txtCompositionFormula.Name = "txtCompositionFormula";
      this.txtCompositionFormula.ReadOnly = true;
      this.txtCompositionFormula.Size = new System.Drawing.Size(514, 21);
      this.txtCompositionFormula.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(36, 36);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(119, 12);
      this.label2.TabIndex = 2;
      this.label2.Text = "Composition Formula";
      // 
      // txtPeptideSequence
      // 
      this.txtPeptideSequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPeptideSequence.Location = new System.Drawing.Point(170, 9);
      this.txtPeptideSequence.Name = "txtPeptideSequence";
      this.txtPeptideSequence.ReadOnly = true;
      this.txtPeptideSequence.Size = new System.Drawing.Size(514, 21);
      this.txtPeptideSequence.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(54, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(101, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "Peptide Sequence";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.tabControl1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 183);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(777, 333);
      this.panel2.TabIndex = 2;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(777, 333);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.splitContainer1);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(769, 307);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Experimental Scans";
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
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.zgcExperimentalScans);
      this.splitContainer1.Size = new System.Drawing.Size(763, 301);
      this.splitContainer1.SplitterDistance = 396;
      this.splitContainer1.TabIndex = 0;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.lvExperimentalScans);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.zgcExperimentalIndividualScan);
      this.splitContainer2.Size = new System.Drawing.Size(396, 301);
      this.splitContainer2.SplitterDistance = 153;
      this.splitContainer2.TabIndex = 0;
      // 
      // lvExperimentalScans
      // 
      this.lvExperimentalScans.CheckBoxes = true;
      this.lvExperimentalScans.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chScan,
            this.chScanO161,
            this.chScanO162,
            this.chScanO163,
            this.chScanO164,
            this.chScanO165,
            this.chScanO166});
      this.lvExperimentalScans.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvExperimentalScans.FullRowSelect = true;
      this.lvExperimentalScans.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvExperimentalScans.HideSelection = false;
      this.lvExperimentalScans.Location = new System.Drawing.Point(0, 0);
      this.lvExperimentalScans.MultiSelect = false;
      this.lvExperimentalScans.Name = "lvExperimentalScans";
      this.lvExperimentalScans.Size = new System.Drawing.Size(396, 153);
      this.lvExperimentalScans.TabIndex = 0;
      this.lvExperimentalScans.UseCompatibleStateImageBehavior = false;
      this.lvExperimentalScans.View = System.Windows.Forms.View.Details;
      this.lvExperimentalScans.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvExperimentalScans_ItemChecked);
      this.lvExperimentalScans.SelectedIndexChanged += new System.EventHandler(this.lvExperimentalScans_SelectedIndexChanged);
      this.lvExperimentalScans.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvExperimentalScans_ItemCheck);
      // 
      // chScan
      // 
      this.chScan.Text = "Scan";
      // 
      // chScanO161
      // 
      this.chScanO161.Width = 61;
      // 
      // zgcExperimentalIndividualScan
      // 
      this.zgcExperimentalIndividualScan.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcExperimentalIndividualScan.Location = new System.Drawing.Point(0, 0);
      this.zgcExperimentalIndividualScan.Name = "zgcExperimentalIndividualScan";
      this.zgcExperimentalIndividualScan.ScrollGrace = 0;
      this.zgcExperimentalIndividualScan.ScrollMaxX = 0;
      this.zgcExperimentalIndividualScan.ScrollMaxY = 0;
      this.zgcExperimentalIndividualScan.ScrollMaxY2 = 0;
      this.zgcExperimentalIndividualScan.ScrollMinX = 0;
      this.zgcExperimentalIndividualScan.ScrollMinY = 0;
      this.zgcExperimentalIndividualScan.ScrollMinY2 = 0;
      this.zgcExperimentalIndividualScan.Size = new System.Drawing.Size(396, 144);
      this.zgcExperimentalIndividualScan.TabIndex = 6;
      // 
      // zgcExperimentalScans
      // 
      this.zgcExperimentalScans.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcExperimentalScans.Location = new System.Drawing.Point(0, 0);
      this.zgcExperimentalScans.Name = "zgcExperimentalScans";
      this.zgcExperimentalScans.ScrollGrace = 0;
      this.zgcExperimentalScans.ScrollMaxX = 0;
      this.zgcExperimentalScans.ScrollMaxY = 0;
      this.zgcExperimentalScans.ScrollMaxY2 = 0;
      this.zgcExperimentalScans.ScrollMinX = 0;
      this.zgcExperimentalScans.ScrollMinY = 0;
      this.zgcExperimentalScans.ScrollMinY2 = 0;
      this.zgcExperimentalScans.Size = new System.Drawing.Size(363, 301);
      this.zgcExperimentalScans.TabIndex = 14;
      this.zgcExperimentalScans.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcExperimentalScans_ContextMenuBuilder);
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.splitContainer3);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(769, 307);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Others";
      this.tabPage1.UseVisualStyleBackColor = true;
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
      this.splitContainer3.Panel1.Controls.Add(this.splitContainer8);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
      this.splitContainer3.Panel2.Controls.Add(this.panel3);
      this.splitContainer3.Size = new System.Drawing.Size(763, 301);
      this.splitContainer3.SplitterDistance = 149;
      this.splitContainer3.TabIndex = 0;
      // 
      // splitContainer8
      // 
      this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer8.Location = new System.Drawing.Point(0, 0);
      this.splitContainer8.Name = "splitContainer8";
      // 
      // splitContainer8.Panel1
      // 
      this.splitContainer8.Panel1.Controls.Add(this.zgcRatio);
      // 
      // splitContainer8.Panel2
      // 
      this.splitContainer8.Panel2.Controls.Add(this.zgcTheoreticalIsotopic);
      this.splitContainer8.Size = new System.Drawing.Size(763, 149);
      this.splitContainer8.SplitterDistance = 380;
      this.splitContainer8.TabIndex = 0;
      // 
      // zgcRatio
      // 
      this.zgcRatio.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcRatio.Location = new System.Drawing.Point(0, 0);
      this.zgcRatio.Name = "zgcRatio";
      this.zgcRatio.ScrollGrace = 0;
      this.zgcRatio.ScrollMaxX = 0;
      this.zgcRatio.ScrollMaxY = 0;
      this.zgcRatio.ScrollMaxY2 = 0;
      this.zgcRatio.ScrollMinX = 0;
      this.zgcRatio.ScrollMinY = 0;
      this.zgcRatio.ScrollMinY2 = 0;
      this.zgcRatio.Size = new System.Drawing.Size(380, 149);
      this.zgcRatio.TabIndex = 3;
      // 
      // zgcTheoreticalIsotopic
      // 
      this.zgcTheoreticalIsotopic.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcTheoreticalIsotopic.Location = new System.Drawing.Point(0, 0);
      this.zgcTheoreticalIsotopic.Name = "zgcTheoreticalIsotopic";
      this.zgcTheoreticalIsotopic.ScrollGrace = 0;
      this.zgcTheoreticalIsotopic.ScrollMaxX = 0;
      this.zgcTheoreticalIsotopic.ScrollMaxY = 0;
      this.zgcTheoreticalIsotopic.ScrollMaxY2 = 0;
      this.zgcTheoreticalIsotopic.ScrollMinX = 0;
      this.zgcTheoreticalIsotopic.ScrollMinY = 0;
      this.zgcTheoreticalIsotopic.ScrollMinY2 = 0;
      this.zgcTheoreticalIsotopic.Size = new System.Drawing.Size(379, 149);
      this.zgcTheoreticalIsotopic.TabIndex = 5;
      // 
      // splitContainer4
      // 
      this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer4.Location = new System.Drawing.Point(0, 17);
      this.splitContainer4.Name = "splitContainer4";
      // 
      // splitContainer4.Panel1
      // 
      this.splitContainer4.Panel1.Controls.Add(this.lvRegression);
      // 
      // splitContainer4.Panel2
      // 
      this.splitContainer4.Panel2.Controls.Add(this.zgcRegression);
      this.splitContainer4.Size = new System.Drawing.Size(763, 131);
      this.splitContainer4.SplitterDistance = 380;
      this.splitContainer4.TabIndex = 5;
      // 
      // lvRegression
      // 
      this.lvRegression.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.lvRegression.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvRegression.Location = new System.Drawing.Point(0, 0);
      this.lvRegression.Name = "lvRegression";
      this.lvRegression.Size = new System.Drawing.Size(380, 131);
      this.lvRegression.TabIndex = 0;
      this.lvRegression.UseCompatibleStateImageBehavior = false;
      this.lvRegression.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "m/z";
      this.columnHeader1.Width = 73;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Observed Intensity";
      this.columnHeader2.Width = 103;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Regression Intensity";
      this.columnHeader3.Width = 111;
      // 
      // zgcRegression
      // 
      this.zgcRegression.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcRegression.Location = new System.Drawing.Point(0, 0);
      this.zgcRegression.Name = "zgcRegression";
      this.zgcRegression.ScrollGrace = 0;
      this.zgcRegression.ScrollMaxX = 0;
      this.zgcRegression.ScrollMaxY = 0;
      this.zgcRegression.ScrollMaxY2 = 0;
      this.zgcRegression.ScrollMinX = 0;
      this.zgcRegression.ScrollMinY = 0;
      this.zgcRegression.ScrollMinY2 = 0;
      this.zgcRegression.Size = new System.Drawing.Size(379, 131);
      this.zgcRegression.TabIndex = 0;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.label6);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(763, 17);
      this.panel3.TabIndex = 4;
      // 
      // label6
      // 
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Location = new System.Drawing.Point(0, 0);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(763, 17);
      this.label6.TabIndex = 5;
      this.label6.Text = "Experimentation vs Regression";
      this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetScanEnabled});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(176, 26);
      // 
      // mnuSetScanEnabled
      // 
      this.mnuSetScanEnabled.Name = "mnuSetScanEnabled";
      this.mnuSetScanEnabled.Size = new System.Drawing.Size(175, 22);
      this.mnuSetScanEnabled.Text = "Set Enabled Only";
      this.mnuSetScanEnabled.Click += new System.EventHandler(this.mnuSetScanEnabled_Click);
      // 
      // O18QuantificationSummaryItemForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(777, 516);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Name = "O18QuantificationSummaryItemForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "O18QuantificationResultViewerUI";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.O18PurityQuantificationResultForm_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.O18QuantificationSummaryItemForm_FormClosing);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      this.splitContainer8.Panel1.ResumeLayout(false);
      this.splitContainer8.Panel2.ResumeLayout(false);
      this.splitContainer8.ResumeLayout(false);
      this.splitContainer4.Panel1.ResumeLayout(false);
      this.splitContainer4.Panel2.ResumeLayout(false);
      this.splitContainer4.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TextBox txtPurityOfO18Water;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtCompositionFormula;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtPeptideSequence;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.ListView lvExperimentalScans;
    private System.Windows.Forms.ColumnHeader chScan;
    private System.Windows.Forms.ColumnHeader chScanO161;
    private System.Windows.Forms.ColumnHeader chScanO162;
    private System.Windows.Forms.ColumnHeader chScanO163;
    private System.Windows.Forms.ColumnHeader chScanO164;
    private System.Windows.Forms.ColumnHeader chScanO165;
    private System.Windows.Forms.ColumnHeader chScanO166;
    private System.Windows.Forms.TextBox txtRatio;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.TextBox txtRegressionCorrelation;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.SplitContainer splitContainer8;
    private ZedGraph.ZedGraphControl zgcRatio;
    private ZedGraph.ZedGraphControl zgcTheoreticalIsotopic;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.SplitContainer splitContainer4;
    private System.Windows.Forms.ListView lvRegression;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private ZedGraph.ZedGraphControl zgcRegression;
    private System.Windows.Forms.TextBox txtLabellingEfficiency;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.TextBox txtPostDigestionLabeling;
    private System.Windows.Forms.Label label8;
    private ZedGraph.ZedGraphControl zgcExperimentalIndividualScan;
    private ZedGraph.ZedGraphControl zgcExperimentalScans;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnuSetScanEnabled;

  }
}