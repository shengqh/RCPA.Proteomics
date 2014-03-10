namespace RCPA.Proteomics.Quantification.Srm
{
  partial class SrmCompoundModeForm
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.lvPeptides = new System.Windows.Forms.CheckedListBox();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.gvFiles = new System.Windows.Forms.DataGridView();
      this.tcGraph = new System.Windows.Forms.TabControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.zgcRetentionTime = new ZedGraph.ZedGraphControl();
      this.zgcPeptide = new ZedGraph.ZedGraphControl();
      this.mnuZgc = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSetScanEnabledOnly = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).BeginInit();
      this.tcGraph.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.mnuZgc.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.lvPeptides);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(894, 486);
      this.splitContainer1.SplitterDistance = 223;
      this.splitContainer1.TabIndex = 0;
      // 
      // lvPeptides
      // 
      this.lvPeptides.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvPeptides.FormattingEnabled = true;
      this.lvPeptides.Location = new System.Drawing.Point(0, 0);
      this.lvPeptides.Name = "lvPeptides";
      this.lvPeptides.Size = new System.Drawing.Size(223, 486);
      this.lvPeptides.TabIndex = 1;
      this.lvPeptides.SelectedIndexChanged += new System.EventHandler(this.lvPeptides_SelectedIndexChanged);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.gvFiles);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.tcGraph);
      this.splitContainer2.Size = new System.Drawing.Size(667, 486);
      this.splitContainer2.SplitterDistance = 188;
      this.splitContainer2.TabIndex = 0;
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
      this.gvFiles.Size = new System.Drawing.Size(667, 188);
      this.gvFiles.TabIndex = 4;
      this.gvFiles.VirtualMode = true;
      this.gvFiles.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gvFiles_CellBeginEdit);
      this.gvFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvFiles_CellDoubleClick);
      this.gvFiles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvFiles_CellFormatting);
      this.gvFiles.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gvFiles_CellValueNeeded);
      this.gvFiles.SelectionChanged += new System.EventHandler(this.gvFiles_SelectionChanged);
      // 
      // tcGraph
      // 
      this.tcGraph.Controls.Add(this.tabPage3);
      this.tcGraph.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tcGraph.Location = new System.Drawing.Point(0, 0);
      this.tcGraph.Name = "tcGraph";
      this.tcGraph.SelectedIndex = 0;
      this.tcGraph.Size = new System.Drawing.Size(667, 294);
      this.tcGraph.TabIndex = 31;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.splitContainer3);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(659, 268);
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
      this.splitContainer3.Panel2.Controls.Add(this.zgcPeptide);
      this.splitContainer3.Size = new System.Drawing.Size(653, 262);
      this.splitContainer3.SplitterDistance = 75;
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
      this.zgcRetentionTime.Size = new System.Drawing.Size(653, 75);
      this.zgcRetentionTime.TabIndex = 25;
      // 
      // zgcPeptide
      // 
      this.zgcPeptide.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcPeptide.Location = new System.Drawing.Point(0, 0);
      this.zgcPeptide.Name = "zgcPeptide";
      this.zgcPeptide.ScrollGrace = 0D;
      this.zgcPeptide.ScrollMaxX = 0D;
      this.zgcPeptide.ScrollMaxY = 0D;
      this.zgcPeptide.ScrollMaxY2 = 0D;
      this.zgcPeptide.ScrollMinX = 0D;
      this.zgcPeptide.ScrollMinY = 0D;
      this.zgcPeptide.ScrollMinY2 = 0D;
      this.zgcPeptide.Size = new System.Drawing.Size(653, 183);
      this.zgcPeptide.TabIndex = 24;
      this.zgcPeptide.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcPeptide_ContextMenuBuilder);
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
      // SrmCompoundModeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(894, 486);
      this.Controls.Add(this.splitContainer1);
      this.Name = "SrmCompoundModeForm";
      this.TabText = "Compound Mode";
      this.Text = "Compound Mode";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).EndInit();
      this.tcGraph.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      this.mnuZgc.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.CheckedListBox lvPeptides;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.TabControl tcGraph;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private ZedGraph.ZedGraphControl zgcRetentionTime;
    private ZedGraph.ZedGraphControl zgcPeptide;
    private System.Windows.Forms.DataGridView gvFiles;
    private System.Windows.Forms.ContextMenuStrip mnuZgc;
    private System.Windows.Forms.ToolStripMenuItem mnuSetScanEnabledOnly;

  }
}