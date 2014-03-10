namespace RCPA.Tools.Quantification.Lipid
{
  partial class LipidQuantificationProcessorUI
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
      this.txtRawFile = new System.Windows.Forms.TextBox();
      this.btnRawFile = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.lvQuery = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.lvPrecursor = new System.Windows.Forms.ListView();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.lvScans = new System.Windows.Forms.ListView();
      this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
      this.zgcCurve = new ZedGraph.ZedGraphControl();
      this.btnSmooth = new System.Windows.Forms.Button();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuSetEnabled = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSetEnabledOnly = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSmoothing = new System.Windows.Forms.ToolStripMenuItem();
      this.btnExport = new System.Windows.Forms.Button();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.btnSave = new System.Windows.Forms.Button();
      this.splitContainer4 = new System.Windows.Forms.SplitContainer();
      this.txtPrecursorPPM = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtProductPPM = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.splitContainer4.Panel1.SuspendLayout();
      this.splitContainer4.Panel2.SuspendLayout();
      this.splitContainer4.SuspendLayout();
      this.SuspendLayout();
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(28, 556);
      this.progressBar.Size = new System.Drawing.Size(1051, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(29, 532);
      this.lblProgress.Size = new System.Drawing.Size(1050, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(432, 583);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(602, 583);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(517, 583);
      // 
      // txtRawFile
      // 
      this.txtRawFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRawFile.Location = new System.Drawing.Point(203, 58);
      this.txtRawFile.Name = "txtRawFile";
      this.txtRawFile.Size = new System.Drawing.Size(876, 21);
      this.txtRawFile.TabIndex = 3;
      // 
      // btnRawFile
      // 
      this.btnRawFile.Location = new System.Drawing.Point(28, 56);
      this.btnRawFile.Name = "btnRawFile";
      this.btnRawFile.Size = new System.Drawing.Size(169, 23);
      this.btnRawFile.TabIndex = 2;
      this.btnRawFile.Text = "button2";
      this.btnRawFile.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(28, 119);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
      this.splitContainer1.Size = new System.Drawing.Size(1051, 392);
      this.splitContainer1.SplitterDistance = 432;
      this.splitContainer1.TabIndex = 10;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.lvQuery);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.lvPrecursor);
      this.splitContainer2.Size = new System.Drawing.Size(432, 392);
      this.splitContainer2.SplitterDistance = 194;
      this.splitContainer2.TabIndex = 0;
      // 
      // lvQuery
      // 
      this.lvQuery.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.lvQuery.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvQuery.FullRowSelect = true;
      this.lvQuery.HideSelection = false;
      this.lvQuery.Location = new System.Drawing.Point(0, 0);
      this.lvQuery.Name = "lvQuery";
      this.lvQuery.Size = new System.Drawing.Size(194, 392);
      this.lvQuery.TabIndex = 10;
      this.lvQuery.UseCompatibleStateImageBehavior = false;
      this.lvQuery.View = System.Windows.Forms.View.Details;
      this.lvQuery.SelectedIndexChanged += new System.EventHandler(this.lvQuery_SelectedIndexChanged);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "m/z";
      this.columnHeader1.Width = 65;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "minIntensity";
      this.columnHeader2.Width = 100;
      // 
      // lvPrecursor
      // 
      this.lvPrecursor.CheckBoxes = true;
      this.lvPrecursor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader6,
            this.columnHeader7});
      this.lvPrecursor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvPrecursor.FullRowSelect = true;
      this.lvPrecursor.HideSelection = false;
      this.lvPrecursor.Location = new System.Drawing.Point(0, 0);
      this.lvPrecursor.Name = "lvPrecursor";
      this.lvPrecursor.Size = new System.Drawing.Size(234, 392);
      this.lvPrecursor.TabIndex = 10;
      this.lvPrecursor.UseCompatibleStateImageBehavior = false;
      this.lvPrecursor.View = System.Windows.Forms.View.Details;
      this.lvPrecursor.SelectedIndexChanged += new System.EventHandler(this.lvPrecursor_SelectedIndexChanged);
      this.lvPrecursor.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPrecursor_ColumnClick);
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "m/z";
      this.columnHeader3.Width = 96;
      // 
      // columnHeader6
      // 
      this.columnHeader6.Text = "MS2";
      this.columnHeader6.Width = 44;
      // 
      // columnHeader7
      // 
      this.columnHeader7.Text = "Area";
      this.columnHeader7.Width = 87;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.lvScans);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.zgcCurve);
      this.splitContainer3.Size = new System.Drawing.Size(615, 392);
      this.splitContainer3.SplitterDistance = 183;
      this.splitContainer3.TabIndex = 0;
      // 
      // lvScans
      // 
      this.lvScans.CheckBoxes = true;
      this.lvScans.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
      this.lvScans.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvScans.FullRowSelect = true;
      this.lvScans.HideSelection = false;
      this.lvScans.Location = new System.Drawing.Point(0, 0);
      this.lvScans.Name = "lvScans";
      this.lvScans.Size = new System.Drawing.Size(183, 392);
      this.lvScans.TabIndex = 10;
      this.lvScans.UseCompatibleStateImageBehavior = false;
      this.lvScans.View = System.Windows.Forms.View.Details;
      this.lvScans.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvScans_ItemChecked);
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Scan";
      this.columnHeader4.Width = 67;
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Intensity";
      this.columnHeader5.Width = 80;
      // 
      // zgcCurve
      // 
      this.zgcCurve.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcCurve.IsSynchronizeXAxes = true;
      this.zgcCurve.Location = new System.Drawing.Point(0, 0);
      this.zgcCurve.Name = "zgcCurve";
      this.zgcCurve.ScrollGrace = 0;
      this.zgcCurve.ScrollMaxX = 0;
      this.zgcCurve.ScrollMaxY = 0;
      this.zgcCurve.ScrollMaxY2 = 0;
      this.zgcCurve.ScrollMinX = 0;
      this.zgcCurve.ScrollMinY = 0;
      this.zgcCurve.ScrollMinY2 = 0;
      this.zgcCurve.Size = new System.Drawing.Size(428, 392);
      this.zgcCurve.TabIndex = 9;
      this.zgcCurve.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgcCurve_ContextMenuBuilder);
      // 
      // btnSmooth
      // 
      this.btnSmooth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSmooth.Location = new System.Drawing.Point(683, 583);
      this.btnSmooth.Name = "btnSmooth";
      this.btnSmooth.Size = new System.Drawing.Size(94, 21);
      this.btnSmooth.TabIndex = 11;
      this.btnSmooth.Text = "Smooth";
      this.btnSmooth.UseVisualStyleBackColor = true;
      this.btnSmooth.Click += new System.EventHandler(this.btnSmooth_Click);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.mnuSetEnabled,
            this.mnuSetEnabledOnly,
            this.mnuSmoothing});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(239, 76);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(235, 6);
      // 
      // mnuSetEnabled
      // 
      this.mnuSetEnabled.Name = "mnuSetEnabled";
      this.mnuSetEnabled.Size = new System.Drawing.Size(238, 22);
      this.mnuSetEnabled.Text = "Set Those Scans Checked";
      this.mnuSetEnabled.Click += new System.EventHandler(this.setEnabledToolStripMenuItem_Click);
      // 
      // mnuSetEnabledOnly
      // 
      this.mnuSetEnabledOnly.Name = "mnuSetEnabledOnly";
      this.mnuSetEnabledOnly.Size = new System.Drawing.Size(238, 22);
      this.mnuSetEnabledOnly.Text = "Set Those Scans Checked Only";
      this.mnuSetEnabledOnly.Click += new System.EventHandler(this.setEnabledOnlyToolStripMenuItem_Click);
      // 
      // mnuSmoothing
      // 
      this.mnuSmoothing.Name = "mnuSmoothing";
      this.mnuSmoothing.Size = new System.Drawing.Size(238, 22);
      this.mnuSmoothing.Text = "Savitzky-Golay Smoothing";
      this.mnuSmoothing.Click += new System.EventHandler(this.mnuSavitzkyGolaySmoothing_Click);
      // 
      // btnExport
      // 
      this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnExport.Location = new System.Drawing.Point(883, 583);
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(94, 21);
      this.btnExport.TabIndex = 12;
      this.btnExport.Text = "Export";
      this.btnExport.UseVisualStyleBackColor = true;
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "area";
      this.saveFileDialog1.Filter = "Quantitation File|*.area|All Files|*.*";
      this.saveFileDialog1.Title = "Export select precursor/area to file ...";
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSave.Location = new System.Drawing.Point(783, 583);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(94, 21);
      this.btnSave.TabIndex = 13;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // splitContainer4
      // 
      this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer4.Location = new System.Drawing.Point(27, 86);
      this.splitContainer4.Name = "splitContainer4";
      // 
      // splitContainer4.Panel1
      // 
      this.splitContainer4.Panel1.Controls.Add(this.txtProductPPM);
      this.splitContainer4.Panel1.Controls.Add(this.label1);
      // 
      // splitContainer4.Panel2
      // 
      this.splitContainer4.Panel2.Controls.Add(this.txtPrecursorPPM);
      this.splitContainer4.Panel2.Controls.Add(this.label2);
      this.splitContainer4.Size = new System.Drawing.Size(1052, 27);
      this.splitContainer4.SplitterDistance = 525;
      this.splitContainer4.TabIndex = 14;
      // 
      // txtPrecursorPPM
      // 
      this.txtPrecursorPPM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPrecursorPPM.Location = new System.Drawing.Point(192, 4);
      this.txtPrecursorPPM.Name = "txtPrecursorPPM";
      this.txtPrecursorPPM.Size = new System.Drawing.Size(331, 21);
      this.txtPrecursorPPM.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 7);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(179, 12);
      this.label2.TabIndex = 2;
      this.label2.Text = "Precursor Ion Tolerance (ppm)";
      // 
      // txtProductPPM
      // 
      this.txtProductPPM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtProductPPM.Location = new System.Drawing.Point(176, 4);
      this.txtProductPPM.Name = "txtProductPPM";
      this.txtProductPPM.Size = new System.Drawing.Size(346, 21);
      this.txtProductPPM.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 7);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(167, 12);
      this.label1.TabIndex = 2;
      this.label1.Text = "Product Ion Tolerance (ppm)";
      // 
      // LipidQuantificationProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1108, 624);
      this.Controls.Add(this.splitContainer4);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.btnExport);
      this.Controls.Add(this.txtRawFile);
      this.Controls.Add(this.btnRawFile);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.btnSmooth);
      this.Name = "LipidQuantificationProcessorUI";
      this.Text = "LiqidQuantificationForm";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.LipidQuantificationProcessorUI_Load);
      this.Controls.SetChildIndex(this.btnSmooth, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      
      this.Controls.SetChildIndex(this.splitContainer1, 0);
      
      this.Controls.SetChildIndex(this.btnRawFile, 0);
      this.Controls.SetChildIndex(this.txtRawFile, 0);
      
      
      
      this.Controls.SetChildIndex(this.btnExport, 0);
      this.Controls.SetChildIndex(this.btnSave, 0);
      this.Controls.SetChildIndex(this.splitContainer4, 0);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.splitContainer4.Panel1.ResumeLayout(false);
      this.splitContainer4.Panel1.PerformLayout();
      this.splitContainer4.Panel2.ResumeLayout(false);
      this.splitContainer4.Panel2.PerformLayout();
      this.splitContainer4.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtRawFile;
    private System.Windows.Forms.Button btnRawFile;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.ListView lvQuery;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ListView lvPrecursor;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.ColumnHeader columnHeader7;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.ListView lvScans;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private ZedGraph.ZedGraphControl zgcCurve;
    private System.Windows.Forms.Button btnSmooth;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnuSetEnabled;
    private System.Windows.Forms.ToolStripMenuItem mnuSetEnabledOnly;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem mnuSmoothing;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.SplitContainer splitContainer4;
    private System.Windows.Forms.TextBox txtProductPPM;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPrecursorPPM;
    private System.Windows.Forms.Label label2;
  }
}