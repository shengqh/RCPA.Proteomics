namespace RCPA.Proteomics.Quantification.Labelfree
{
  partial class ProteinChromotographViewer
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
      this.txtNoredundant = new System.Windows.Forms.TextBox();
      this.btnNoredundant = new System.Windows.Forms.Button();
      this.cbRebuildAll = new System.Windows.Forms.CheckBox();
      this.txtPPMTolerance = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtRawDirectory = new System.Windows.Forms.TextBox();
      this.btnRawDirectory = new System.Windows.Forms.Button();
      this.lblProtease = new System.Windows.Forms.Label();
      this.cbProtease = new System.Windows.Forms.ComboBox();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.lbProteins = new System.Windows.Forms.ListBox();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.lbPeptides = new System.Windows.Forms.ListBox();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.lbIdentified = new System.Windows.Forms.ListBox();
      this.lbPrecursor = new System.Windows.Forms.ListBox();
      this.zgcScans = new ZedGraph.ZedGraphControl();
      this.pnlFile.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
      this.splitContainer4.Panel1.SuspendLayout();
      this.splitContainer4.Panel2.SuspendLayout();
      this.splitContainer4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlFile.Location = new System.Drawing.Point(0, 0);
      this.pnlFile.Size = new System.Drawing.Size(1148, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(902, 21);
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
      this.splitContainer4.Location = new System.Drawing.Point(0, 0);
      this.splitContainer4.Name = "splitContainer4";
      this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer4.Panel1
      // 
      this.splitContainer4.Panel1.Controls.Add(this.txtNoredundant);
      this.splitContainer4.Panel1.Controls.Add(this.btnNoredundant);
      this.splitContainer4.Panel1.Controls.Add(this.cbRebuildAll);
      this.splitContainer4.Panel1.Controls.Add(this.txtPPMTolerance);
      this.splitContainer4.Panel1.Controls.Add(this.label1);
      this.splitContainer4.Panel1.Controls.Add(this.txtRawDirectory);
      this.splitContainer4.Panel1.Controls.Add(this.btnRawDirectory);
      this.splitContainer4.Panel1.Controls.Add(this.lblProtease);
      this.splitContainer4.Panel1.Controls.Add(this.cbProtease);
      // 
      // splitContainer4.Panel2
      // 
      this.splitContainer4.Panel2.Controls.Add(this.splitContainer1);
      this.splitContainer4.Size = new System.Drawing.Size(1148, 576);
      this.splitContainer4.SplitterDistance = 122;
      this.splitContainer4.TabIndex = 2;
      // 
      // txtNoredundant
      // 
      this.txtNoredundant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtNoredundant.Location = new System.Drawing.Point(265, 67);
      this.txtNoredundant.Name = "txtNoredundant";
      this.txtNoredundant.Size = new System.Drawing.Size(871, 21);
      this.txtNoredundant.TabIndex = 16;
      // 
      // btnNoredundant
      // 
      this.btnNoredundant.Location = new System.Drawing.Point(7, 65);
      this.btnNoredundant.Name = "btnNoredundant";
      this.btnNoredundant.Size = new System.Drawing.Size(251, 23);
      this.btnNoredundant.TabIndex = 15;
      this.btnNoredundant.Text = "button1";
      this.btnNoredundant.UseVisualStyleBackColor = true;
      // 
      // cbRebuildAll
      // 
      this.cbRebuildAll.AutoSize = true;
      this.cbRebuildAll.Location = new System.Drawing.Point(808, 97);
      this.cbRebuildAll.Name = "cbRebuildAll";
      this.cbRebuildAll.Size = new System.Drawing.Size(90, 16);
      this.cbRebuildAll.TabIndex = 14;
      this.cbRebuildAll.Text = "Rebuild all";
      this.cbRebuildAll.UseVisualStyleBackColor = true;
      // 
      // txtPPMTolerance
      // 
      this.txtPPMTolerance.Location = new System.Drawing.Point(674, 94);
      this.txtPPMTolerance.Name = "txtPPMTolerance";
      this.txtPPMTolerance.Size = new System.Drawing.Size(100, 21);
      this.txtPPMTolerance.TabIndex = 13;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(525, 98);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(143, 12);
      this.label1.TabIndex = 12;
      this.label1.Text = "Precursor ppm Tolerance";
      // 
      // txtRawDirectory
      // 
      this.txtRawDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRawDirectory.Location = new System.Drawing.Point(265, 40);
      this.txtRawDirectory.Name = "txtRawDirectory";
      this.txtRawDirectory.Size = new System.Drawing.Size(871, 21);
      this.txtRawDirectory.TabIndex = 7;
      // 
      // btnRawDirectory
      // 
      this.btnRawDirectory.Location = new System.Drawing.Point(7, 38);
      this.btnRawDirectory.Name = "btnRawDirectory";
      this.btnRawDirectory.Size = new System.Drawing.Size(251, 23);
      this.btnRawDirectory.TabIndex = 6;
      this.btnRawDirectory.Text = "button1";
      this.btnRawDirectory.UseVisualStyleBackColor = true;
      // 
      // lblProtease
      // 
      this.lblProtease.AutoSize = true;
      this.lblProtease.Location = new System.Drawing.Point(151, 97);
      this.lblProtease.Name = "lblProtease";
      this.lblProtease.Size = new System.Drawing.Size(107, 12);
      this.lblProtease.TabIndex = 5;
      this.lblProtease.Text = "Select protease :";
      // 
      // cbProtease
      // 
      this.cbProtease.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbProtease.FormattingEnabled = true;
      this.cbProtease.Location = new System.Drawing.Point(264, 94);
      this.cbProtease.Name = "cbProtease";
      this.cbProtease.Size = new System.Drawing.Size(232, 20);
      this.cbProtease.TabIndex = 4;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.lbProteins);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1148, 450);
      this.splitContainer1.SplitterDistance = 119;
      this.splitContainer1.TabIndex = 3;
      // 
      // lbProteins
      // 
      this.lbProteins.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbProteins.FormattingEnabled = true;
      this.lbProteins.ItemHeight = 12;
      this.lbProteins.Location = new System.Drawing.Point(0, 0);
      this.lbProteins.Name = "lbProteins";
      this.lbProteins.Size = new System.Drawing.Size(119, 450);
      this.lbProteins.TabIndex = 0;
      this.lbProteins.SelectedIndexChanged += new System.EventHandler(this.lbProteins_SelectedIndexChanged);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.lbPeptides);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
      this.splitContainer2.Size = new System.Drawing.Size(1025, 450);
      this.splitContainer2.SplitterDistance = 135;
      this.splitContainer2.TabIndex = 0;
      // 
      // lbPeptides
      // 
      this.lbPeptides.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbPeptides.FormattingEnabled = true;
      this.lbPeptides.ItemHeight = 12;
      this.lbPeptides.Location = new System.Drawing.Point(0, 0);
      this.lbPeptides.Name = "lbPeptides";
      this.lbPeptides.Size = new System.Drawing.Size(135, 450);
      this.lbPeptides.TabIndex = 0;
      this.lbPeptides.SelectedIndexChanged += new System.EventHandler(this.lbPeptides_SelectedIndexChanged);
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.lbIdentified);
      this.splitContainer3.Panel1.Controls.Add(this.lbPrecursor);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.zgcScans);
      this.splitContainer3.Size = new System.Drawing.Size(886, 450);
      this.splitContainer3.SplitterDistance = 143;
      this.splitContainer3.TabIndex = 0;
      // 
      // lbIdentified
      // 
      this.lbIdentified.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbIdentified.FormattingEnabled = true;
      this.lbIdentified.ItemHeight = 12;
      this.lbIdentified.Location = new System.Drawing.Point(0, 100);
      this.lbIdentified.Name = "lbIdentified";
      this.lbIdentified.Size = new System.Drawing.Size(143, 350);
      this.lbIdentified.TabIndex = 3;
      // 
      // lbPrecursor
      // 
      this.lbPrecursor.Dock = System.Windows.Forms.DockStyle.Top;
      this.lbPrecursor.FormattingEnabled = true;
      this.lbPrecursor.ItemHeight = 12;
      this.lbPrecursor.Location = new System.Drawing.Point(0, 0);
      this.lbPrecursor.Name = "lbPrecursor";
      this.lbPrecursor.Size = new System.Drawing.Size(143, 100);
      this.lbPrecursor.TabIndex = 0;
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
      this.zgcScans.Size = new System.Drawing.Size(739, 450);
      this.zgcScans.TabIndex = 15;
      // 
      // ProteinChromotographViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1148, 681);
      this.Controls.Add(this.splitContainer4);
      this.Name = "ProteinChromotographViewer";
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
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer4;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ListBox lbProteins;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.ListBox lbPeptides;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.ListBox lbPrecursor;
    private ZedGraph.ZedGraphControl zgcScans;
    private System.Windows.Forms.TextBox txtRawDirectory;
    private System.Windows.Forms.Button btnRawDirectory;
    private System.Windows.Forms.Label lblProtease;
    private System.Windows.Forms.ComboBox cbProtease;
    private System.Windows.Forms.TextBox txtPPMTolerance;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox cbRebuildAll;
    private System.Windows.Forms.TextBox txtNoredundant;
    private System.Windows.Forms.Button btnNoredundant;
    private System.Windows.Forms.ListBox lbIdentified;
  }
}