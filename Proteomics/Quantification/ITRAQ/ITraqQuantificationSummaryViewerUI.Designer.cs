namespace RCPA.Proteomics.Quantification.ITraq
{
  partial class ITraqQuantificationSummaryViewerUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ITraqQuantificationSummaryViewerUI));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tvResult = new TreeViewMS.TreeViewMS();
      this.tcInformations = new System.Windows.Forms.TabControl();
      this.tpProtein = new System.Windows.Forms.TabPage();
      this.pnlProtein = new System.Windows.Forms.Panel();
      this.zgcProtein = new ZedGraph.ZedGraphControl();
      this.tpUniquePeptide = new System.Windows.Forms.TabPage();
      this.zgcUniquePeptide = new ZedGraph.ZedGraphControl();
      this.tpSpectrum = new System.Windows.Forms.TabPage();
      this.zgcSpectrum = new ZedGraph.ZedGraphControl();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnOpen = new System.Windows.Forms.ToolStripButton();
      this.btnCancel = new System.Windows.Forms.ToolStripButton();
      this.btnExit = new System.Windows.Forms.ToolStripButton();
      this.Progress = new RCPA.Gui.ProgressField();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tcInformations.SuspendLayout();
      this.tpProtein.SuspendLayout();
      this.pnlProtein.SuspendLayout();
      this.tpUniquePeptide.SuspendLayout();
      this.tpSpectrum.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 54);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tvResult);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tcInformations);
      this.splitContainer1.Size = new System.Drawing.Size(1063, 489);
      this.splitContainer1.SplitterDistance = 354;
      this.splitContainer1.TabIndex = 0;
      // 
      // tvResult
      // 
      this.tvResult.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvResult.FullRowSelect = true;
      this.tvResult.HideSelection = false;
      this.tvResult.Location = new System.Drawing.Point(0, 0);
      this.tvResult.Name = "tvResult";
      this.tvResult.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("tvResult.SelectedNodes")));
      this.tvResult.Size = new System.Drawing.Size(354, 489);
      this.tvResult.TabIndex = 1;
      this.tvResult.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvResult_AfterSelect);
      // 
      // tcInformations
      // 
      this.tcInformations.Controls.Add(this.tpProtein);
      this.tcInformations.Controls.Add(this.tpUniquePeptide);
      this.tcInformations.Controls.Add(this.tpSpectrum);
      this.tcInformations.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tcInformations.Location = new System.Drawing.Point(0, 0);
      this.tcInformations.Name = "tcInformations";
      this.tcInformations.SelectedIndex = 0;
      this.tcInformations.Size = new System.Drawing.Size(705, 489);
      this.tcInformations.TabIndex = 0;
      // 
      // tpProtein
      // 
      this.tpProtein.AutoScroll = true;
      this.tpProtein.Controls.Add(this.pnlProtein);
      this.tpProtein.Location = new System.Drawing.Point(4, 22);
      this.tpProtein.Name = "tpProtein";
      this.tpProtein.Padding = new System.Windows.Forms.Padding(3);
      this.tpProtein.Size = new System.Drawing.Size(697, 463);
      this.tpProtein.TabIndex = 1;
      this.tpProtein.Text = "Protein";
      this.tpProtein.UseVisualStyleBackColor = true;
      // 
      // pnlProtein
      // 
      this.pnlProtein.Controls.Add(this.zgcProtein);
      this.pnlProtein.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlProtein.Location = new System.Drawing.Point(3, 3);
      this.pnlProtein.Name = "pnlProtein";
      this.pnlProtein.Size = new System.Drawing.Size(691, 457);
      this.pnlProtein.TabIndex = 0;
      // 
      // zgcProtein
      // 
      this.zgcProtein.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcProtein.Location = new System.Drawing.Point(0, 0);
      this.zgcProtein.Name = "zgcProtein";
      this.zgcProtein.ScrollGrace = 0D;
      this.zgcProtein.ScrollMaxX = 0D;
      this.zgcProtein.ScrollMaxY = 0D;
      this.zgcProtein.ScrollMaxY2 = 0D;
      this.zgcProtein.ScrollMinX = 0D;
      this.zgcProtein.ScrollMinY = 0D;
      this.zgcProtein.ScrollMinY2 = 0D;
      this.zgcProtein.Size = new System.Drawing.Size(691, 457);
      this.zgcProtein.TabIndex = 10;
      // 
      // tpUniquePeptide
      // 
      this.tpUniquePeptide.Controls.Add(this.zgcUniquePeptide);
      this.tpUniquePeptide.Location = new System.Drawing.Point(4, 22);
      this.tpUniquePeptide.Name = "tpUniquePeptide";
      this.tpUniquePeptide.Padding = new System.Windows.Forms.Padding(3);
      this.tpUniquePeptide.Size = new System.Drawing.Size(697, 463);
      this.tpUniquePeptide.TabIndex = 4;
      this.tpUniquePeptide.Text = "Unique peptide";
      this.tpUniquePeptide.UseVisualStyleBackColor = true;
      // 
      // zgcUniquePeptide
      // 
      this.zgcUniquePeptide.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcUniquePeptide.Location = new System.Drawing.Point(3, 3);
      this.zgcUniquePeptide.Name = "zgcUniquePeptide";
      this.zgcUniquePeptide.ScrollGrace = 0D;
      this.zgcUniquePeptide.ScrollMaxX = 0D;
      this.zgcUniquePeptide.ScrollMaxY = 0D;
      this.zgcUniquePeptide.ScrollMaxY2 = 0D;
      this.zgcUniquePeptide.ScrollMinX = 0D;
      this.zgcUniquePeptide.ScrollMinY = 0D;
      this.zgcUniquePeptide.ScrollMinY2 = 0D;
      this.zgcUniquePeptide.Size = new System.Drawing.Size(691, 450);
      this.zgcUniquePeptide.TabIndex = 10;
      // 
      // tpSpectrum
      // 
      this.tpSpectrum.Controls.Add(this.zgcSpectrum);
      this.tpSpectrum.Location = new System.Drawing.Point(4, 22);
      this.tpSpectrum.Name = "tpSpectrum";
      this.tpSpectrum.Padding = new System.Windows.Forms.Padding(3);
      this.tpSpectrum.Size = new System.Drawing.Size(697, 463);
      this.tpSpectrum.TabIndex = 3;
      this.tpSpectrum.Text = "Peptide";
      this.tpSpectrum.UseVisualStyleBackColor = true;
      // 
      // zgcSpectrum
      // 
      this.zgcSpectrum.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcSpectrum.Location = new System.Drawing.Point(3, 3);
      this.zgcSpectrum.Name = "zgcSpectrum";
      this.zgcSpectrum.ScrollGrace = 0D;
      this.zgcSpectrum.ScrollMaxX = 0D;
      this.zgcSpectrum.ScrollMaxY = 0D;
      this.zgcSpectrum.ScrollMaxY2 = 0D;
      this.zgcSpectrum.ScrollMinX = 0D;
      this.zgcSpectrum.ScrollMinY = 0D;
      this.zgcSpectrum.ScrollMinY2 = 0D;
      this.zgcSpectrum.Size = new System.Drawing.Size(691, 457);
      this.zgcSpectrum.TabIndex = 11;
      // 
      // toolStrip1
      // 
      this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnCancel,
            this.btnExit});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(1063, 54);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnOpen
      // 
      this.btnOpen.Image = global::RCPA.Properties.Resources.fileopen;
      this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(40, 51);
      this.btnOpen.Text = "Open";
      this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.btnOpen.Click += new System.EventHandler(this.mnuOpen_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Image = global::RCPA.Properties.Resources.error;
      this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(47, 51);
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnExit
      // 
      this.btnExit.Image = global::RCPA.Properties.Resources.close;
      this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new System.Drawing.Size(36, 51);
      this.btnExit.Text = "Exit";
      this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.btnExit.Click += new System.EventHandler(this.mnuExit_Click);
      // 
      // Progress
      // 
      this.Progress.AutoSize = true;
      this.Progress.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.Progress.Location = new System.Drawing.Point(0, 543);
      this.Progress.MaximumSize = new System.Drawing.Size(0, 53);
      this.Progress.MinimumSize = new System.Drawing.Size(0, 53);
      this.Progress.Name = "Progress";
      this.Progress.Size = new System.Drawing.Size(1063, 53);
      this.Progress.TabIndex = 2;
      // 
      // ITraqQuantificationSummaryViewerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1063, 596);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.Progress);
      this.Name = "ITraqQuantificationSummaryViewerUI";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.TabText = "ITraqQuantificationSummaryViewerUI";
      this.Text = "ITraqQuantificationSummaryViewerUI";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ITraqQuantificationSummaryViewerUI_FormClosed);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tcInformations.ResumeLayout(false);
      this.tpProtein.ResumeLayout(false);
      this.pnlProtein.ResumeLayout(false);
      this.tpUniquePeptide.ResumeLayout(false);
      this.tpSpectrum.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private TreeViewMS.TreeViewMS tvResult;
    private System.Windows.Forms.TabControl tcInformations;
    private System.Windows.Forms.TabPage tpProtein;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnOpen;
    private System.Windows.Forms.ToolStripButton btnExit;
    private System.Windows.Forms.TabPage tpSpectrum;
    private ZedGraph.ZedGraphControl zgcSpectrum;
    private System.Windows.Forms.TabPage tpUniquePeptide;
    private ZedGraph.ZedGraphControl zgcUniquePeptide;
    private System.Windows.Forms.Panel pnlProtein;
    private ZedGraph.ZedGraphControl zgcProtein;
    private Gui.ProgressField Progress;
    private System.Windows.Forms.ToolStripButton btnCancel;
  }
}