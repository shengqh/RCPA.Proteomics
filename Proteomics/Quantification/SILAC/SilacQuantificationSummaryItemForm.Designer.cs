namespace RCPA.Proteomics.Quantification.SILAC
{
  partial class SilacQuantificationSummaryItemForm
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
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.zgcTheoreticalIsotopic = new ZedGraph.ZedGraphControl();
      this.btnSave = new System.Windows.Forms.Button();
      this.txtRegressionCorrelation = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.btnClose = new System.Windows.Forms.Button();
      this.txtRatio = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtReferenceComposition = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtSampleComposition = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtPeptideSequence = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.splitContainer4 = new System.Windows.Forms.SplitContainer();
      this.lvExperimentalScans = new System.Windows.Forms.ListView();
      this.chScan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chScanO161 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chScanO162 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chScanO163 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chScanO164 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chScanO165 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chScanO166 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.mnuScans = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSelectedChecked = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSelectedUnchecked = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer6 = new System.Windows.Forms.SplitContainer();
      this.zgcExperimentalScans = new ZedGraph.ZedGraphControl();
      this.splitContainer7 = new System.Windows.Forms.SplitContainer();
      this.zgcSilacRegression = new ZedGraph.ZedGraphControl();
      this.zgcExperimentalIndividualScan = new ZedGraph.ZedGraphControl();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSetScanEnabled = new System.Windows.Forms.ToolStripMenuItem();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.splitContainer4.Panel1.SuspendLayout();
      this.splitContainer4.Panel2.SuspendLayout();
      this.splitContainer4.SuspendLayout();
      this.mnuScans.SuspendLayout();
      this.splitContainer6.Panel1.SuspendLayout();
      this.splitContainer6.Panel2.SuspendLayout();
      this.splitContainer6.SuspendLayout();
      this.splitContainer7.Panel1.SuspendLayout();
      this.splitContainer7.Panel2.SuspendLayout();
      this.splitContainer7.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.textBox1);
      this.panel1.Controls.Add(this.label6);
      this.panel1.Controls.Add(this.zgcTheoreticalIsotopic);
      this.panel1.Controls.Add(this.btnSave);
      this.panel1.Controls.Add(this.txtRegressionCorrelation);
      this.panel1.Controls.Add(this.label5);
      this.panel1.Controls.Add(this.btnClose);
      this.panel1.Controls.Add(this.txtRatio);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.txtReferenceComposition);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.txtSampleComposition);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.txtPeptideSequence);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(940, 180);
      this.panel1.TabIndex = 1;
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(170, 36);
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(426, 21);
      this.textBox1.TabIndex = 16;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(114, 39);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(41, 12);
      this.label6.TabIndex = 15;
      this.label6.Text = "Charge";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // zgcTheoreticalIsotopic
      // 
      this.zgcTheoreticalIsotopic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.zgcTheoreticalIsotopic.Location = new System.Drawing.Point(602, 9);
      this.zgcTheoreticalIsotopic.Name = "zgcTheoreticalIsotopic";
      this.zgcTheoreticalIsotopic.ScrollGrace = 0D;
      this.zgcTheoreticalIsotopic.ScrollMaxX = 0D;
      this.zgcTheoreticalIsotopic.ScrollMaxY = 0D;
      this.zgcTheoreticalIsotopic.ScrollMaxY2 = 0D;
      this.zgcTheoreticalIsotopic.ScrollMinX = 0D;
      this.zgcTheoreticalIsotopic.ScrollMinY = 0D;
      this.zgcTheoreticalIsotopic.ScrollMinY2 = 0D;
      this.zgcTheoreticalIsotopic.Size = new System.Drawing.Size(259, 156);
      this.zgcTheoreticalIsotopic.TabIndex = 14;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(867, 9);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(66, 45);
      this.btnSave.TabIndex = 13;
      this.btnSave.Text = "&Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // txtRegressionCorrelation
      // 
      this.txtRegressionCorrelation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRegressionCorrelation.Location = new System.Drawing.Point(170, 117);
      this.txtRegressionCorrelation.Name = "txtRegressionCorrelation";
      this.txtRegressionCorrelation.ReadOnly = true;
      this.txtRegressionCorrelation.Size = new System.Drawing.Size(426, 21);
      this.txtRegressionCorrelation.TabIndex = 10;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(18, 120);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(137, 12);
      this.label5.TabIndex = 9;
      this.label5.Text = "Regression Correlation";
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(867, 117);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(66, 48);
      this.btnClose.TabIndex = 8;
      this.btnClose.Text = "&Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // txtRatio
      // 
      this.txtRatio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRatio.Location = new System.Drawing.Point(170, 144);
      this.txtRatio.Name = "txtRatio";
      this.txtRatio.ReadOnly = true;
      this.txtRatio.Size = new System.Drawing.Size(426, 21);
      this.txtRatio.TabIndex = 7;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(42, 147);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(113, 12);
      this.label4.TabIndex = 6;
      this.label4.Text = "Sample : Reference";
      // 
      // txtReferenceComposition
      // 
      this.txtReferenceComposition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtReferenceComposition.Location = new System.Drawing.Point(170, 90);
      this.txtReferenceComposition.Name = "txtReferenceComposition";
      this.txtReferenceComposition.ReadOnly = true;
      this.txtReferenceComposition.Size = new System.Drawing.Size(426, 21);
      this.txtReferenceComposition.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(24, 93);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(131, 12);
      this.label3.TabIndex = 2;
      this.label3.Text = "Reference Composition";
      // 
      // txtSampleComposition
      // 
      this.txtSampleComposition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSampleComposition.Location = new System.Drawing.Point(170, 63);
      this.txtSampleComposition.Name = "txtSampleComposition";
      this.txtSampleComposition.ReadOnly = true;
      this.txtSampleComposition.Size = new System.Drawing.Size(426, 21);
      this.txtSampleComposition.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(42, 66);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(113, 12);
      this.label2.TabIndex = 2;
      this.label2.Text = "Sample Composition";
      // 
      // txtPeptideSequence
      // 
      this.txtPeptideSequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPeptideSequence.Location = new System.Drawing.Point(170, 9);
      this.txtPeptideSequence.Name = "txtPeptideSequence";
      this.txtPeptideSequence.ReadOnly = true;
      this.txtPeptideSequence.ShortcutsEnabled = false;
      this.txtPeptideSequence.Size = new System.Drawing.Size(426, 21);
      this.txtPeptideSequence.TabIndex = 1;
      this.txtPeptideSequence.WordWrap = false;
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
      this.panel2.Controls.Add(this.splitContainer4);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 180);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(940, 336);
      this.panel2.TabIndex = 2;
      // 
      // splitContainer4
      // 
      this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer4.Location = new System.Drawing.Point(0, 0);
      this.splitContainer4.Name = "splitContainer4";
      // 
      // splitContainer4.Panel1
      // 
      this.splitContainer4.Panel1.Controls.Add(this.lvExperimentalScans);
      // 
      // splitContainer4.Panel2
      // 
      this.splitContainer4.Panel2.Controls.Add(this.splitContainer6);
      this.splitContainer4.Size = new System.Drawing.Size(940, 336);
      this.splitContainer4.SplitterDistance = 313;
      this.splitContainer4.TabIndex = 1;
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
      this.lvExperimentalScans.ContextMenuStrip = this.mnuScans;
      this.lvExperimentalScans.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvExperimentalScans.FullRowSelect = true;
      this.lvExperimentalScans.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvExperimentalScans.HideSelection = false;
      this.lvExperimentalScans.Location = new System.Drawing.Point(0, 0);
      this.lvExperimentalScans.Name = "lvExperimentalScans";
      this.lvExperimentalScans.Size = new System.Drawing.Size(313, 336);
      this.lvExperimentalScans.TabIndex = 3;
      this.lvExperimentalScans.UseCompatibleStateImageBehavior = false;
      this.lvExperimentalScans.View = System.Windows.Forms.View.Details;
      this.lvExperimentalScans.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvExperimentalScans_ItemCheck);
      this.lvExperimentalScans.SelectedIndexChanged += new System.EventHandler(this.lvExperimentalScans_SelectedIndexChanged);
      this.lvExperimentalScans.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvExperimentalScans_MouseDown);
      // 
      // chScan
      // 
      this.chScan.Text = "Scan";
      // 
      // mnuScans
      // 
      this.mnuScans.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSelectedChecked,
            this.mnuSelectedUnchecked});
      this.mnuScans.Name = "mnuScans";
      this.mnuScans.Size = new System.Drawing.Size(203, 48);
      // 
      // mnuSelectedChecked
      // 
      this.mnuSelectedChecked.Name = "mnuSelectedChecked";
      this.mnuSelectedChecked.Size = new System.Drawing.Size(202, 22);
      this.mnuSelectedChecked.Text = "Set Selected Checked";
      this.mnuSelectedChecked.Click += new System.EventHandler(this.mnuSelectedChecked_Click);
      // 
      // mnuSelectedUnchecked
      // 
      this.mnuSelectedUnchecked.Name = "mnuSelectedUnchecked";
      this.mnuSelectedUnchecked.Size = new System.Drawing.Size(202, 22);
      this.mnuSelectedUnchecked.Text = "Set Selected Unchecked";
      this.mnuSelectedUnchecked.Click += new System.EventHandler(this.mnuSelectedUnchecked_Click);
      // 
      // splitContainer6
      // 
      this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer6.Location = new System.Drawing.Point(0, 0);
      this.splitContainer6.Name = "splitContainer6";
      // 
      // splitContainer6.Panel1
      // 
      this.splitContainer6.Panel1.Controls.Add(this.zgcExperimentalScans);
      // 
      // splitContainer6.Panel2
      // 
      this.splitContainer6.Panel2.Controls.Add(this.splitContainer7);
      this.splitContainer6.Size = new System.Drawing.Size(623, 336);
      this.splitContainer6.SplitterDistance = 363;
      this.splitContainer6.TabIndex = 0;
      // 
      // zgcExperimentalScans
      // 
      this.zgcExperimentalScans.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcExperimentalScans.Location = new System.Drawing.Point(0, 0);
      this.zgcExperimentalScans.Name = "zgcExperimentalScans";
      this.zgcExperimentalScans.ScrollGrace = 0D;
      this.zgcExperimentalScans.ScrollMaxX = 0D;
      this.zgcExperimentalScans.ScrollMaxY = 0D;
      this.zgcExperimentalScans.ScrollMaxY2 = 0D;
      this.zgcExperimentalScans.ScrollMinX = 0D;
      this.zgcExperimentalScans.ScrollMinY = 0D;
      this.zgcExperimentalScans.ScrollMinY2 = 0D;
      this.zgcExperimentalScans.Size = new System.Drawing.Size(363, 336);
      this.zgcExperimentalScans.TabIndex = 8;
      this.zgcExperimentalScans.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcExperimentalScans_ContextMenuBuilder);
      // 
      // splitContainer7
      // 
      this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer7.Location = new System.Drawing.Point(0, 0);
      this.splitContainer7.Name = "splitContainer7";
      this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer7.Panel1
      // 
      this.splitContainer7.Panel1.Controls.Add(this.zgcSilacRegression);
      // 
      // splitContainer7.Panel2
      // 
      this.splitContainer7.Panel2.Controls.Add(this.zgcExperimentalIndividualScan);
      this.splitContainer7.Size = new System.Drawing.Size(256, 336);
      this.splitContainer7.SplitterDistance = 171;
      this.splitContainer7.TabIndex = 0;
      // 
      // zgcSilacRegression
      // 
      this.zgcSilacRegression.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcSilacRegression.Location = new System.Drawing.Point(0, 0);
      this.zgcSilacRegression.Name = "zgcSilacRegression";
      this.zgcSilacRegression.ScrollGrace = 0D;
      this.zgcSilacRegression.ScrollMaxX = 0D;
      this.zgcSilacRegression.ScrollMaxY = 0D;
      this.zgcSilacRegression.ScrollMaxY2 = 0D;
      this.zgcSilacRegression.ScrollMinX = 0D;
      this.zgcSilacRegression.ScrollMinY = 0D;
      this.zgcSilacRegression.ScrollMinY2 = 0D;
      this.zgcSilacRegression.Size = new System.Drawing.Size(256, 171);
      this.zgcSilacRegression.TabIndex = 14;
      this.zgcSilacRegression.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zgcRegression_MouseClick);
      // 
      // zgcExperimentalIndividualScan
      // 
      this.zgcExperimentalIndividualScan.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcExperimentalIndividualScan.Location = new System.Drawing.Point(0, 0);
      this.zgcExperimentalIndividualScan.Name = "zgcExperimentalIndividualScan";
      this.zgcExperimentalIndividualScan.ScrollGrace = 0D;
      this.zgcExperimentalIndividualScan.ScrollMaxX = 0D;
      this.zgcExperimentalIndividualScan.ScrollMaxY = 0D;
      this.zgcExperimentalIndividualScan.ScrollMaxY2 = 0D;
      this.zgcExperimentalIndividualScan.ScrollMinX = 0D;
      this.zgcExperimentalIndividualScan.ScrollMinY = 0D;
      this.zgcExperimentalIndividualScan.ScrollMinY2 = 0D;
      this.zgcExperimentalIndividualScan.Size = new System.Drawing.Size(256, 161);
      this.zgcExperimentalIndividualScan.TabIndex = 7;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetScanEnabled});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(167, 26);
      // 
      // mnuSetScanEnabled
      // 
      this.mnuSetScanEnabled.Name = "mnuSetScanEnabled";
      this.mnuSetScanEnabled.Size = new System.Drawing.Size(166, 22);
      this.mnuSetScanEnabled.Text = "Set Enabled Only";
      this.mnuSetScanEnabled.Click += new System.EventHandler(this.setSelectedScanEnabledOnlyToolStripMenuItem_Click);
      // 
      // SilacQuantificationSummaryItemForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(940, 516);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.KeyPreview = true;
      this.Name = "SilacQuantificationSummaryItemForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "O18QuantificationResultViewerUI";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.SilacQuantificationResultForm_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.splitContainer4.Panel1.ResumeLayout(false);
      this.splitContainer4.Panel2.ResumeLayout(false);
      this.splitContainer4.ResumeLayout(false);
      this.mnuScans.ResumeLayout(false);
      this.splitContainer6.Panel1.ResumeLayout(false);
      this.splitContainer6.Panel2.ResumeLayout(false);
      this.splitContainer6.ResumeLayout(false);
      this.splitContainer7.Panel1.ResumeLayout(false);
      this.splitContainer7.Panel2.ResumeLayout(false);
      this.splitContainer7.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TextBox txtSampleComposition;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtPeptideSequence;
    private System.Windows.Forms.TextBox txtRatio;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.TextBox txtRegressionCorrelation;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.TextBox txtReferenceComposition;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ContextMenuStrip mnuScans;
    private System.Windows.Forms.ToolStripMenuItem mnuSelectedChecked;
    private System.Windows.Forms.ToolStripMenuItem mnuSelectedUnchecked;
    private ZedGraph.ZedGraphControl zgcTheoreticalIsotopic;
    private System.Windows.Forms.SplitContainer splitContainer4;
    private System.Windows.Forms.ListView lvExperimentalScans;
    private System.Windows.Forms.ColumnHeader chScan;
    private System.Windows.Forms.ColumnHeader chScanO161;
    private System.Windows.Forms.ColumnHeader chScanO162;
    private System.Windows.Forms.ColumnHeader chScanO163;
    private System.Windows.Forms.ColumnHeader chScanO164;
    private System.Windows.Forms.ColumnHeader chScanO165;
    private System.Windows.Forms.ColumnHeader chScanO166;
    private System.Windows.Forms.SplitContainer splitContainer6;
    private ZedGraph.ZedGraphControl zgcExperimentalScans;
    private System.Windows.Forms.SplitContainer splitContainer7;
    private ZedGraph.ZedGraphControl zgcSilacRegression;
    private ZedGraph.ZedGraphControl zgcExperimentalIndividualScan;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnuSetScanEnabled;

  }
}