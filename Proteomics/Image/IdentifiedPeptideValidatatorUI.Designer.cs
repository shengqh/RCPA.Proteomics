namespace RCPA.Proteomics.Image
{
  partial class IdentifiedPeptideValidatatorUI
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
      this.iIdentifiedSpectrumBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnOpen = new System.Windows.Forms.ToolStripButton();
      this.gvPeptides = new System.Windows.Forms.DataGridView();
      this.colFileScan = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.TheoreticalMH = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.zgcPeaks = new ZedGraph.ZedGraphControl();
      ((System.ComponentModel.ISupportInitialize)(this.iIdentifiedSpectrumBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvPeptides)).BeginInit();
      this.SuspendLayout();
      // 
      // iIdentifiedSpectrumBindingSource
      // 
      this.iIdentifiedSpectrumBindingSource.DataSource = typeof(RCPA.Proteomics.Summary.IIdentifiedSpectrum);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.gvPeptides);
      this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.zgcPeaks);
      this.splitContainer1.Size = new System.Drawing.Size(995, 563);
      this.splitContainer1.SplitterDistance = 200;
      this.splitContainer1.TabIndex = 4;
      // 
      // toolStrip1
      // 
      this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(995, 39);
      this.toolStrip1.TabIndex = 4;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnOpen
      // 
      this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnOpen.Image = global::RCPA.Properties.Resources.fileopen;
      this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(36, 36);
      this.btnOpen.Text = "Open";
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // gvPeptides
      // 
      this.gvPeptides.AutoGenerateColumns = false;
      this.gvPeptides.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvPeptides.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileScan,
            this.colSequence,
            this.TheoreticalMH,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn30});
      this.gvPeptides.DataSource = this.iIdentifiedSpectrumBindingSource;
      this.gvPeptides.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvPeptides.Location = new System.Drawing.Point(0, 39);
      this.gvPeptides.MultiSelect = false;
      this.gvPeptides.Name = "gvPeptides";
      this.gvPeptides.ReadOnly = true;
      this.gvPeptides.RowTemplate.Height = 23;
      this.gvPeptides.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvPeptides.Size = new System.Drawing.Size(995, 161);
      this.gvPeptides.TabIndex = 3;
      this.gvPeptides.VirtualMode = true;
      this.gvPeptides.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.iIdentifiedSpectrumDataGridView_CellValueNeeded);
      this.gvPeptides.SelectionChanged += new System.EventHandler(this.iIdentifiedSpectrumDataGridView_SelectionChanged);
      // 
      // colFileScan
      // 
      this.colFileScan.HeaderText = "FileScan";
      this.colFileScan.Name = "colFileScan";
      this.colFileScan.ReadOnly = true;
      this.colFileScan.Width = 200;
      // 
      // colSequence
      // 
      this.colSequence.HeaderText = "Sequence";
      this.colSequence.Name = "colSequence";
      this.colSequence.ReadOnly = true;
      this.colSequence.Width = 200;
      // 
      // TheoreticalMH
      // 
      this.TheoreticalMH.DataPropertyName = "TheoreticalMH";
      this.TheoreticalMH.HeaderText = "TheoreticalMH";
      this.TheoreticalMH.Name = "TheoreticalMH";
      this.TheoreticalMH.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn14
      // 
      this.dataGridViewTextBoxColumn14.DataPropertyName = "DiffToExperimentalMass";
      this.dataGridViewTextBoxColumn14.HeaderText = "DiffToExperimentalMass";
      this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
      this.dataGridViewTextBoxColumn14.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn4
      // 
      this.dataGridViewTextBoxColumn4.DataPropertyName = "Charge";
      this.dataGridViewTextBoxColumn4.HeaderText = "Charge";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn18
      // 
      this.dataGridViewTextBoxColumn18.DataPropertyName = "Score";
      this.dataGridViewTextBoxColumn18.HeaderText = "Score";
      this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
      this.dataGridViewTextBoxColumn18.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn23
      // 
      this.dataGridViewTextBoxColumn23.DataPropertyName = "ExpectValue";
      this.dataGridViewTextBoxColumn23.HeaderText = "ExpectValue";
      this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
      this.dataGridViewTextBoxColumn23.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn30
      // 
      this.dataGridViewTextBoxColumn30.DataPropertyName = "Modifications";
      this.dataGridViewTextBoxColumn30.HeaderText = "Modifications";
      this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
      this.dataGridViewTextBoxColumn30.ReadOnly = true;
      // 
      // zgcPeaks
      // 
      this.zgcPeaks.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcPeaks.Location = new System.Drawing.Point(0, 0);
      this.zgcPeaks.Name = "zgcPeaks";
      this.zgcPeaks.ScrollGrace = 0D;
      this.zgcPeaks.ScrollMaxX = 0D;
      this.zgcPeaks.ScrollMaxY = 0D;
      this.zgcPeaks.ScrollMaxY2 = 0D;
      this.zgcPeaks.ScrollMinX = 0D;
      this.zgcPeaks.ScrollMinY = 0D;
      this.zgcPeaks.ScrollMinY2 = 0D;
      this.zgcPeaks.Size = new System.Drawing.Size(995, 359);
      this.zgcPeaks.TabIndex = 2;
      // 
      // IdentifiedPeptideValidatatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(995, 563);
      this.Controls.Add(this.splitContainer1);
      this.Name = "IdentifiedPeptideValidatatorUI";
      this.TabText = "IdentifiedPeptideValidatatorUI";
      this.Text = "IdentifiedPeptideValidatatorUI";
      ((System.ComponentModel.ISupportInitialize)(this.iIdentifiedSpectrumBindingSource)).EndInit();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvPeptides)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.BindingSource iIdentifiedSpectrumBindingSource;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.DataGridView gvPeptides;
    private ZedGraph.ZedGraphControl zgcPeaks;
    private System.Windows.Forms.DataGridViewTextBoxColumn colFileScan;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSequence;
    private System.Windows.Forms.DataGridViewTextBoxColumn TheoreticalMH;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnOpen;
  }
}