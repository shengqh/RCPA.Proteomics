namespace RCPA.Proteomics.Quantification.Labelfree
{
  partial class ProteinChromatographProcessorUI
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
      this.splitContainer4 = new System.Windows.Forms.SplitContainer();
      this.txtWindow = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtPPMTolerance = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtRawDirectory = new System.Windows.Forms.TextBox();
      this.btnRawDirectory = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.lvPeptides = new System.Windows.Forms.ListView();
      this.zgcScans = new ZedGraph.ZedGraphControl();
      this.cbShowInRawFileOnly = new RCPA.Gui.RcpaCheckField();
      this.pnlFile.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
      this.splitContainer4.Panel1.SuspendLayout();
      this.splitContainer4.Panel2.SuspendLayout();
      this.splitContainer4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Location = new System.Drawing.Point(7, 12);
      this.pnlFile.Size = new System.Drawing.Size(1129, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(883, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 603);
      this.lblProgress.Size = new System.Drawing.Size(1148, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 624);
      this.progressBar.Size = new System.Drawing.Size(1148, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(622, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(537, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(452, 7);
      // 
      // splitContainer4
      // 
      this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer4.Location = new System.Drawing.Point(0, -1);
      this.splitContainer4.Name = "splitContainer4";
      this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer4.Panel1
      // 
      this.splitContainer4.Panel1.Controls.Add(this.cbShowInRawFileOnly);
      this.splitContainer4.Panel1.Controls.Add(this.txtWindow);
      this.splitContainer4.Panel1.Controls.Add(this.label2);
      this.splitContainer4.Panel1.Controls.Add(this.txtPPMTolerance);
      this.splitContainer4.Panel1.Controls.Add(this.label1);
      this.splitContainer4.Panel1.Controls.Add(this.txtRawDirectory);
      this.splitContainer4.Panel1.Controls.Add(this.btnRawDirectory);
      // 
      // splitContainer4.Panel2
      // 
      this.splitContainer4.Panel2.Controls.Add(this.splitContainer1);
      this.splitContainer4.Size = new System.Drawing.Size(1148, 577);
      this.splitContainer4.SplitterDistance = 90;
      this.splitContainer4.TabIndex = 2;
      // 
      // txtWindow
      // 
      this.txtWindow.Location = new System.Drawing.Point(597, 66);
      this.txtWindow.Name = "txtWindow";
      this.txtWindow.Size = new System.Drawing.Size(100, 21);
      this.txtWindow.TabIndex = 15;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(398, 70);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(197, 12);
      this.label2.TabIndex = 14;
      this.label2.Text = "Minimum elution window (minutes)";
      // 
      // txtPPMTolerance
      // 
      this.txtPPMTolerance.Location = new System.Drawing.Point(253, 67);
      this.txtPPMTolerance.Name = "txtPPMTolerance";
      this.txtPPMTolerance.Size = new System.Drawing.Size(100, 21);
      this.txtPPMTolerance.TabIndex = 13;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(104, 71);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(143, 12);
      this.label1.TabIndex = 12;
      this.label1.Text = "Precursor ppm Tolerance";
      // 
      // txtRawDirectory
      // 
      this.txtRawDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRawDirectory.Location = new System.Drawing.Point(253, 40);
      this.txtRawDirectory.Name = "txtRawDirectory";
      this.txtRawDirectory.Size = new System.Drawing.Size(883, 21);
      this.txtRawDirectory.TabIndex = 7;
      // 
      // btnRawDirectory
      // 
      this.btnRawDirectory.Location = new System.Drawing.Point(7, 38);
      this.btnRawDirectory.Name = "btnRawDirectory";
      this.btnRawDirectory.Size = new System.Drawing.Size(246, 23);
      this.btnRawDirectory.TabIndex = 6;
      this.btnRawDirectory.Text = "button1";
      this.btnRawDirectory.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.lvPeptides);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.zgcScans);
      this.splitContainer1.Size = new System.Drawing.Size(1148, 483);
      this.splitContainer1.SplitterDistance = 541;
      this.splitContainer1.TabIndex = 3;
      // 
      // lvPeptides
      // 
      this.lvPeptides.CheckBoxes = true;
      this.lvPeptides.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvPeptides.FullRowSelect = true;
      this.lvPeptides.GridLines = true;
      this.lvPeptides.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvPeptides.HideSelection = false;
      this.lvPeptides.Location = new System.Drawing.Point(0, 0);
      this.lvPeptides.MultiSelect = false;
      this.lvPeptides.Name = "lvPeptides";
      this.lvPeptides.ShowItemToolTips = true;
      this.lvPeptides.Size = new System.Drawing.Size(541, 483);
      this.lvPeptides.TabIndex = 24;
      this.lvPeptides.UseCompatibleStateImageBehavior = false;
      this.lvPeptides.View = System.Windows.Forms.View.Details;
      this.lvPeptides.SelectedIndexChanged += new System.EventHandler(this.lvPeptides_SelectedIndexChanged);
      // 
      // zgcScans
      // 
      this.zgcScans.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcScans.Location = new System.Drawing.Point(0, 0);
      this.zgcScans.Name = "zgcScans";
      this.zgcScans.ScrollGrace = 0D;
      this.zgcScans.ScrollMaxX = 0D;
      this.zgcScans.ScrollMaxY = 0D;
      this.zgcScans.ScrollMaxY2 = 0D;
      this.zgcScans.ScrollMinX = 0D;
      this.zgcScans.ScrollMinY = 0D;
      this.zgcScans.ScrollMinY2 = 0D;
      this.zgcScans.Size = new System.Drawing.Size(603, 483);
      this.zgcScans.TabIndex = 16;
      // 
      // cbShowInRawFileOnly
      // 
      this.cbShowInRawFileOnly.AutoSize = true;
      this.cbShowInRawFileOnly.Key = "cbShowInRawFileOnly";
      this.cbShowInRawFileOnly.Location = new System.Drawing.Point(732, 68);
      this.cbShowInRawFileOnly.Name = "cbShowInRawFileOnly";
      this.cbShowInRawFileOnly.PreCondition = null;
      this.cbShowInRawFileOnly.Size = new System.Drawing.Size(264, 16);
      this.cbShowInRawFileOnly.TabIndex = 16;
      this.cbShowInRawFileOnly.Text = "Show peptides from current raw file only";
      this.cbShowInRawFileOnly.UseVisualStyleBackColor = true;
      this.cbShowInRawFileOnly.CheckedChanged += new System.EventHandler(this.cbShowInRawFileOnly_CheckedChanged);
      // 
      // ProteinChromotographProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1148, 681);
      this.Controls.Add(this.splitContainer4);
      this.Name = "ProteinChromotographProcessorUI";
      this.TabText = "ProteinChromotographViewer";
      this.Text = "ProteinChromotographViewer";
      this.Shown += new System.EventHandler(this.ProteinChromotographViewer_Shown);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.splitContainer4, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.splitContainer4.Panel1.ResumeLayout(false);
      this.splitContainer4.Panel1.PerformLayout();
      this.splitContainer4.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
      this.splitContainer4.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer4;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox txtRawDirectory;
    private System.Windows.Forms.Button btnRawDirectory;
    private System.Windows.Forms.TextBox txtPPMTolerance;
    private System.Windows.Forms.Label label1;
    private ZedGraph.ZedGraphControl zgcScans;
    private System.Windows.Forms.ListView lvPeptides;
    private System.Windows.Forms.TextBox txtWindow;
    private System.Windows.Forms.Label label2;
    private Gui.RcpaCheckField cbShowInRawFileOnly;
  }
}