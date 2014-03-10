namespace RCPA.Tools.Quantification.SmallMolecule
{
  partial class SmallMoleculeQuantificationViewerUI
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
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.crosslinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSetScanEnabledOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.lvPeaks = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
      this.splitContainer5 = new System.Windows.Forms.SplitContainer();
      this.splitContainer6 = new System.Windows.Forms.SplitContainer();
      this.sampleFiles = new RCPA.Gui.MultipleFileField();
      this.referenceFiles = new RCPA.Gui.MultipleFileField();
      this.splitContainer7 = new System.Windows.Forms.SplitContainer();
      this.zgcScanLeft = new ZedGraph.ZedGraphControl();
      this.zgcScanRight = new ZedGraph.ZedGraphControl();
      this.btnExport = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.contextMenuStrip1.SuspendLayout();
      this.contextMenuStrip2.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.splitContainer5.Panel1.SuspendLayout();
      this.splitContainer5.Panel2.SuspendLayout();
      this.splitContainer5.SuspendLayout();
      this.splitContainer6.Panel1.SuspendLayout();
      this.splitContainer6.Panel2.SuspendLayout();
      this.splitContainer6.SuspendLayout();
      this.splitContainer7.Panel1.SuspendLayout();
      this.splitContainer7.Panel2.SuspendLayout();
      this.splitContainer7.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(411, 597);
      this.btnGo.Size = new System.Drawing.Size(75, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(581, 597);
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(496, 597);
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.Visible = false;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crosslinkToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
      // 
      // crosslinkToolStripMenuItem
      // 
      this.crosslinkToolStripMenuItem.Name = "crosslinkToolStripMenuItem";
      this.crosslinkToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
      this.crosslinkToolStripMenuItem.Text = "Crosslink";
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetScanEnabledOnlyToolStripMenuItem});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.contextMenuStrip2.Size = new System.Drawing.Size(227, 26);
      // 
      // mnuSetScanEnabledOnlyToolStripMenuItem
      // 
      this.mnuSetScanEnabledOnlyToolStripMenuItem.Name = "mnuSetScanEnabledOnlyToolStripMenuItem";
      this.mnuSetScanEnabledOnlyToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
      this.mnuSetScanEnabledOnlyToolStripMenuItem.Text = "Set selected scans enabled";
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "ratio";
      this.saveFileDialog1.Filter = "Ratio File(*.ratio)|*.ratio|All file(*.*)|*.*";
      this.saveFileDialog1.Title = "Export result to text format file ...";
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.DefaultExt = "data";
      this.openFileDialog1.FileName = "openFileDialog1";
      this.openFileDialog1.Filter = "Data file|*.data|All Files|*.*";
      this.openFileDialog1.Multiselect = true;
      this.openFileDialog1.Title = "Select data files";
      // 
      // splitContainer2
      // 
      this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.Location = new System.Drawing.Point(27, 58);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.lvPeaks);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer5);
      this.splitContainer2.Size = new System.Drawing.Size(1015, 524);
      this.splitContainer2.SplitterDistance = 307;
      this.splitContainer2.TabIndex = 26;
      // 
      // lvPeaks
      // 
      this.lvPeaks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this.lvPeaks.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvPeaks.FullRowSelect = true;
      this.lvPeaks.HideSelection = false;
      this.lvPeaks.Location = new System.Drawing.Point(0, 0);
      this.lvPeaks.Name = "lvPeaks";
      this.lvPeaks.Size = new System.Drawing.Size(307, 524);
      this.lvPeaks.TabIndex = 26;
      this.lvPeaks.UseCompatibleStateImageBehavior = false;
      this.lvPeaks.View = System.Windows.Forms.View.Details;
      this.lvPeaks.SelectedIndexChanged += new System.EventHandler(this.lvPeaks_SelectedIndexChanged);
      this.lvPeaks.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPeaks_ColumnClick);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Peak";
      this.columnHeader1.Width = 54;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "twoTail";
      this.columnHeader2.Width = 71;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "leftTail";
      this.columnHeader3.Width = 75;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "rightTail";
      this.columnHeader4.Width = 74;
      // 
      // splitContainer5
      // 
      this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer5.Location = new System.Drawing.Point(0, 0);
      this.splitContainer5.Name = "splitContainer5";
      this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer5.Panel1
      // 
      this.splitContainer5.Panel1.Controls.Add(this.splitContainer6);
      // 
      // splitContainer5.Panel2
      // 
      this.splitContainer5.Panel2.Controls.Add(this.splitContainer7);
      this.splitContainer5.Size = new System.Drawing.Size(704, 524);
      this.splitContainer5.SplitterDistance = 168;
      this.splitContainer5.TabIndex = 0;
      // 
      // splitContainer6
      // 
      this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer6.Location = new System.Drawing.Point(0, 0);
      this.splitContainer6.Name = "splitContainer6";
      // 
      // splitContainer6.Panel1
      // 
      this.splitContainer6.Panel1.Controls.Add(this.sampleFiles);
      // 
      // splitContainer6.Panel2
      // 
      this.splitContainer6.Panel2.Controls.Add(this.referenceFiles);
      this.splitContainer6.Size = new System.Drawing.Size(704, 168);
      this.splitContainer6.SplitterDistance = 342;
      this.splitContainer6.TabIndex = 0;
      // 
      // sampleFiles
      // 
      this.sampleFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sampleFiles.FileArgument = null;
      this.sampleFiles.FileDescription = "Sample files";
      this.sampleFiles.FileNames = new string[0];
      this.sampleFiles.Location = new System.Drawing.Point(0, 0);
      this.sampleFiles.Name = "sampleFiles";
      this.sampleFiles.SelectedIndex = -1;
      this.sampleFiles.SelectedItem = null;
      this.sampleFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.sampleFiles.Size = new System.Drawing.Size(342, 168);
      this.sampleFiles.TabIndex = 2;
      // 
      // referenceFiles
      // 
      this.referenceFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.referenceFiles.FileArgument = null;
      this.referenceFiles.FileDescription = "Reference files";
      this.referenceFiles.FileNames = new string[0];
      this.referenceFiles.Location = new System.Drawing.Point(0, 0);
      this.referenceFiles.Name = "referenceFiles";
      this.referenceFiles.SelectedIndex = -1;
      this.referenceFiles.SelectedItem = null;
      this.referenceFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.referenceFiles.Size = new System.Drawing.Size(358, 168);
      this.referenceFiles.TabIndex = 2;
      // 
      // splitContainer7
      // 
      this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer7.Location = new System.Drawing.Point(0, 0);
      this.splitContainer7.Name = "splitContainer7";
      // 
      // splitContainer7.Panel1
      // 
      this.splitContainer7.Panel1.Controls.Add(this.zgcScanLeft);
      // 
      // splitContainer7.Panel2
      // 
      this.splitContainer7.Panel2.Controls.Add(this.zgcScanRight);
      this.splitContainer7.Size = new System.Drawing.Size(704, 352);
      this.splitContainer7.SplitterDistance = 347;
      this.splitContainer7.TabIndex = 0;
      // 
      // zgcScanLeft
      // 
      this.zgcScanLeft.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcScanLeft.Location = new System.Drawing.Point(0, 0);
      this.zgcScanLeft.Name = "zgcScanLeft";
      this.zgcScanLeft.ScrollGrace = 0;
      this.zgcScanLeft.ScrollMaxX = 0;
      this.zgcScanLeft.ScrollMaxY = 0;
      this.zgcScanLeft.ScrollMaxY2 = 0;
      this.zgcScanLeft.ScrollMinX = 0;
      this.zgcScanLeft.ScrollMinY = 0;
      this.zgcScanLeft.ScrollMinY2 = 0;
      this.zgcScanLeft.Size = new System.Drawing.Size(347, 352);
      this.zgcScanLeft.TabIndex = 24;
      // 
      // zgcScanRight
      // 
      this.zgcScanRight.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcScanRight.Location = new System.Drawing.Point(0, 0);
      this.zgcScanRight.Name = "zgcScanRight";
      this.zgcScanRight.ScrollGrace = 0;
      this.zgcScanRight.ScrollMaxX = 0;
      this.zgcScanRight.ScrollMaxY = 0;
      this.zgcScanRight.ScrollMaxY2 = 0;
      this.zgcScanRight.ScrollMinX = 0;
      this.zgcScanRight.ScrollMinY = 0;
      this.zgcScanRight.ScrollMinY2 = 0;
      this.zgcScanRight.Size = new System.Drawing.Size(353, 352);
      this.zgcScanRight.TabIndex = 24;
      // 
      // btnExport
      // 
      this.btnExport.Location = new System.Drawing.Point(662, 599);
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(75, 23);
      this.btnExport.TabIndex = 27;
      this.btnExport.Text = "&Export";
      this.btnExport.UseVisualStyleBackColor = true;
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(28, 15);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(207, 23);
      this.button1.TabIndex = 28;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(241, 17);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(801, 21);
      this.textBox1.TabIndex = 29;
      // 
      // SmallMoleculeQuantificationViewerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1067, 640);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.splitContainer2);
      this.Controls.Add(this.btnExport);
      this.Controls.Add(this.textBox1);
      this.KeyPreview = true;
      this.Name = "SmallMoleculeQuantificationViewerUI";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmallMoleculeQuantificationViewerUI_FormClosing);
      this.Controls.SetChildIndex(this.textBox1, 0);
      this.Controls.SetChildIndex(this.btnExport, 0);
      this.Controls.SetChildIndex(this.splitContainer2, 0);
      this.Controls.SetChildIndex(this.button1, 0);
      
      
      
      this.contextMenuStrip1.ResumeLayout(false);
      this.contextMenuStrip2.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer5.Panel1.ResumeLayout(false);
      this.splitContainer5.Panel2.ResumeLayout(false);
      this.splitContainer5.ResumeLayout(false);
      this.splitContainer6.Panel1.ResumeLayout(false);
      this.splitContainer6.Panel2.ResumeLayout(false);
      this.splitContainer6.ResumeLayout(false);
      this.splitContainer7.Panel1.ResumeLayout(false);
      this.splitContainer7.Panel2.ResumeLayout(false);
      this.splitContainer7.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem crosslinkToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem mnuSetScanEnabledOnlyToolStripMenuItem;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.ListView lvPeaks;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.SplitContainer splitContainer5;
    private System.Windows.Forms.SplitContainer splitContainer6;
    private RCPA.Gui.MultipleFileField sampleFiles;
    private RCPA.Gui.MultipleFileField referenceFiles;
    private System.Windows.Forms.SplitContainer splitContainer7;
    private ZedGraph.ZedGraphControl zgcScanLeft;
    private ZedGraph.ZedGraphControl zgcScanRight;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
  }
}
