namespace RCPA.Tools.Summary
{
  partial class SequestSummaryBuilderUI
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
      this.btnAddZips = new System.Windows.Forms.Button();
      this.btnClassification = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.btnAddSubDirectories = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.label7 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.cbFilterBySpRank = new System.Windows.Forms.CheckBox();
      this.txtSpRank = new System.Windows.Forms.TextBox();
      this.cbFilterByDeltaCn = new System.Windows.Forms.CheckBox();
      this.txtMinDeltaCn = new System.Windows.Forms.TextBox();
      this.cbFilterByXcorr = new System.Windows.Forms.CheckBox();
      this.txtXcorr1 = new System.Windows.Forms.TextBox();
      this.txtXcorr2 = new System.Windows.Forms.TextBox();
      this.txtXcorr3 = new System.Windows.Forms.TextBox();
      this.btnXml = new System.Windows.Forms.Button();
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      this.tabControl1.SuspendLayout();
      this.filterGroup.SuspendLayout();
      this.datafileGroup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pnlDataFiles)).BeginInit();
      this.pnlDataFiles.Panel1.SuspendLayout();
      this.pnlDataFiles.Panel2.SuspendLayout();
      this.pnlDataFiles.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Size = new System.Drawing.Size(1222, 685);
      // 
      // filterGroup
      // 
      this.filterGroup.Controls.Add(this.txtMaxEvalue);
      this.filterGroup.Controls.Add(this.cbFilterByEvalue);
      this.filterGroup.Controls.Add(this.txtXcorr3);
      this.filterGroup.Controls.Add(this.label7);
      this.filterGroup.Controls.Add(this.label6);
      this.filterGroup.Controls.Add(this.txtXcorr2);
      this.filterGroup.Controls.Add(this.txtXcorr1);
      this.filterGroup.Controls.Add(this.cbFilterByXcorr);
      this.filterGroup.Controls.Add(this.txtMinDeltaCn);
      this.filterGroup.Controls.Add(this.cbFilterByDeltaCn);
      this.filterGroup.Controls.Add(this.txtSpRank);
      this.filterGroup.Controls.Add(this.cbFilterBySpRank);
      this.filterGroup.Location = new System.Drawing.Point(4, 22);
      this.filterGroup.Size = new System.Drawing.Size(1214, 659);
      this.filterGroup.Controls.SetChildIndex(this.cbFilterBySpRank, 0);
      this.filterGroup.Controls.SetChildIndex(this.txtSpRank, 0);
      this.filterGroup.Controls.SetChildIndex(this.cbFilterByDeltaCn, 0);
      this.filterGroup.Controls.SetChildIndex(this.txtMinDeltaCn, 0);
      this.filterGroup.Controls.SetChildIndex(this.cbFilterByXcorr, 0);
      this.filterGroup.Controls.SetChildIndex(this.txtXcorr1, 0);
      this.filterGroup.Controls.SetChildIndex(this.txtXcorr2, 0);
      this.filterGroup.Controls.SetChildIndex(this.label6, 0);
      this.filterGroup.Controls.SetChildIndex(this.label7, 0);
      this.filterGroup.Controls.SetChildIndex(this.txtXcorr3, 0);
      this.filterGroup.Controls.SetChildIndex(this.cbFilterByEvalue, 0);
      this.filterGroup.Controls.SetChildIndex(this.txtMaxEvalue, 0);
      // 
      // datafileGroup
      // 
      this.datafileGroup.Location = new System.Drawing.Point(4, 22);
      this.datafileGroup.Size = new System.Drawing.Size(1214, 659);
      // 
      // databaseGroup
      // 
      this.databaseGroup.Location = new System.Drawing.Point(4, 22);
      this.databaseGroup.Size = new System.Drawing.Size(1222, 659);
      // 
      // pnlDataFiles
      // 
      // 
      // pnlDataFiles.Panel2
      // 
      this.pnlDataFiles.Panel2.Controls.Add(this.btnXml);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnAddZips);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnClassification);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnSave);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnLoad);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnAddSubDirectories);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnRemoveFiles);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnAddFiles);
      this.pnlDataFiles.Size = new System.Drawing.Size(1208, 653);
      this.pnlDataFiles.SplitterDistance = 1062;
      // 
      // lvDatFiles
      // 
      this.lvDatFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
      this.lvDatFiles.Size = new System.Drawing.Size(1062, 616);
      this.lvDatFiles.SizeChanged += new System.EventHandler(this.lvDatFiles_SizeChanged);
      // 
      // lblDataFile
      // 
      this.lblDataFile.Size = new System.Drawing.Size(404, 13);
      this.lblDataFile.Text = "Select directories you want to extract peptides (only selected directories will b" +
    "e used)";
      // 
      // lblProgress
      // 
      this.lblProgress.Size = new System.Drawing.Size(1222, 23);
      // 
      // progressBar
      // 
      this.progressBar.Size = new System.Drawing.Size(1222, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(777, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(456, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(349, 7);
      // 
      // btnAddZips
      // 
      this.btnAddZips.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddZips.Location = new System.Drawing.Point(0, 150);
      this.btnAddZips.Name = "btnAddZips";
      this.btnAddZips.Size = new System.Drawing.Size(142, 25);
      this.btnAddZips.TabIndex = 18;
      this.btnAddZips.Text = "Add Zip Files";
      this.btnAddZips.UseVisualStyleBackColor = true;
      this.btnAddZips.Click += new System.EventHandler(this.btnAddZips_Click);
      // 
      // btnClassification
      // 
      this.btnClassification.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnClassification.Location = new System.Drawing.Point(0, 125);
      this.btnClassification.Name = "btnClassification";
      this.btnClassification.Size = new System.Drawing.Size(142, 25);
      this.btnClassification.TabIndex = 17;
      this.btnClassification.Text = "Classify";
      this.btnClassification.UseVisualStyleBackColor = true;
      this.btnClassification.Click += new System.EventHandler(this.btnClassification_Click);
      // 
      // btnSave
      // 
      this.btnSave.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnSave.Location = new System.Drawing.Point(0, 100);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(142, 25);
      this.btnSave.TabIndex = 14;
      this.btnSave.Text = "button2";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnLoad.Location = new System.Drawing.Point(0, 75);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(142, 25);
      this.btnLoad.TabIndex = 13;
      this.btnLoad.Text = "button1";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnAddFiles
      // 
      this.btnAddFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddFiles.Location = new System.Drawing.Point(0, 0);
      this.btnAddFiles.Name = "btnAddFiles";
      this.btnAddFiles.Size = new System.Drawing.Size(142, 25);
      this.btnAddFiles.TabIndex = 11;
      this.btnAddFiles.Text = "button1";
      this.btnAddFiles.UseVisualStyleBackColor = true;
      // 
      // btnAddSubDirectories
      // 
      this.btnAddSubDirectories.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddSubDirectories.Location = new System.Drawing.Point(0, 50);
      this.btnAddSubDirectories.Name = "btnAddSubDirectories";
      this.btnAddSubDirectories.Size = new System.Drawing.Size(142, 25);
      this.btnAddSubDirectories.TabIndex = 12;
      this.btnAddSubDirectories.Text = "Add Subdirs";
      this.btnAddSubDirectories.UseVisualStyleBackColor = true;
      this.btnAddSubDirectories.Click += new System.EventHandler(this.btnAddSubDirectories_Click);
      // 
      // btnRemoveFiles
      // 
      this.btnRemoveFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnRemoveFiles.Location = new System.Drawing.Point(0, 25);
      this.btnRemoveFiles.Name = "btnRemoveFiles";
      this.btnRemoveFiles.Size = new System.Drawing.Size(142, 25);
      this.btnRemoveFiles.TabIndex = 12;
      this.btnRemoveFiles.Text = "button2";
      this.btnRemoveFiles.UseVisualStyleBackColor = true;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Directories";
      this.columnHeader1.Width = 723;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Classification";
      this.columnHeader3.Width = 113;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(329, 326);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(102, 13);
      this.label7.TabIndex = 40;
      this.label7.Text = "charge>=2, XCorr>=";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(519, 326);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(102, 13);
      this.label6.TabIndex = 40;
      this.label6.Text = "charge>=3, XCorr>=";
      // 
      // cbFilterBySpRank
      // 
      this.cbFilterBySpRank.AutoSize = true;
      this.cbFilterBySpRank.Location = new System.Drawing.Point(27, 373);
      this.cbFilterBySpRank.Name = "cbFilterBySpRank";
      this.cbFilterBySpRank.Size = new System.Drawing.Size(119, 17);
      this.cbFilterBySpRank.TabIndex = 37;
      this.cbFilterBySpRank.Text = "Filter by SpRank >=";
      this.cbFilterBySpRank.UseVisualStyleBackColor = true;
      // 
      // txtSpRank
      // 
      this.txtSpRank.Location = new System.Drawing.Point(173, 372);
      this.txtSpRank.Name = "txtSpRank";
      this.txtSpRank.Size = new System.Drawing.Size(94, 20);
      this.txtSpRank.TabIndex = 38;
      // 
      // cbFilterByDeltaCn
      // 
      this.cbFilterByDeltaCn.AutoSize = true;
      this.cbFilterByDeltaCn.Location = new System.Drawing.Point(27, 348);
      this.cbFilterByDeltaCn.Name = "cbFilterByDeltaCn";
      this.cbFilterByDeltaCn.Size = new System.Drawing.Size(120, 17);
      this.cbFilterByDeltaCn.TabIndex = 37;
      this.cbFilterByDeltaCn.Text = "Filter by DeltaCN >=";
      this.cbFilterByDeltaCn.UseVisualStyleBackColor = true;
      // 
      // txtMinDeltaCn
      // 
      this.txtMinDeltaCn.Location = new System.Drawing.Point(173, 346);
      this.txtMinDeltaCn.Name = "txtMinDeltaCn";
      this.txtMinDeltaCn.Size = new System.Drawing.Size(94, 20);
      this.txtMinDeltaCn.TabIndex = 38;
      // 
      // cbFilterByXcorr
      // 
      this.cbFilterByXcorr.AutoSize = true;
      this.cbFilterByXcorr.Location = new System.Drawing.Point(27, 322);
      this.cbFilterByXcorr.Name = "cbFilterByXcorr";
      this.cbFilterByXcorr.Size = new System.Drawing.Size(186, 17);
      this.cbFilterByXcorr.TabIndex = 35;
      this.cbFilterByXcorr.Text = "Filter by XCorr: charge=1, XCorr>=";
      this.cbFilterByXcorr.UseVisualStyleBackColor = true;
      // 
      // txtXcorr1
      // 
      this.txtXcorr1.Location = new System.Drawing.Point(255, 320);
      this.txtXcorr1.Name = "txtXcorr1";
      this.txtXcorr1.Size = new System.Drawing.Size(66, 20);
      this.txtXcorr1.TabIndex = 36;
      // 
      // txtXcorr2
      // 
      this.txtXcorr2.Location = new System.Drawing.Point(447, 320);
      this.txtXcorr2.Name = "txtXcorr2";
      this.txtXcorr2.Size = new System.Drawing.Size(66, 20);
      this.txtXcorr2.TabIndex = 36;
      // 
      // txtXcorr3
      // 
      this.txtXcorr3.Location = new System.Drawing.Point(638, 320);
      this.txtXcorr3.Name = "txtXcorr3";
      this.txtXcorr3.Size = new System.Drawing.Size(66, 20);
      this.txtXcorr3.TabIndex = 36;
      // 
      // btnXml
      // 
      this.btnXml.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnXml.Location = new System.Drawing.Point(0, 175);
      this.btnXml.Name = "btnXml";
      this.btnXml.Size = new System.Drawing.Size(142, 25);
      this.btnXml.TabIndex = 32;
      this.btnXml.Text = "Add Xml Files";
      this.btnXml.UseVisualStyleBackColor = true;
      this.btnXml.Click += new System.EventHandler(this.btnXml_Click);
      // 
      // txtMaxEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(173, 398);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 20);
      this.txtMaxEvalue.TabIndex = 149;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(27, 400);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(113, 17);
      this.cbFilterByEvalue.TabIndex = 148;
      this.cbFilterByEvalue.Text = "Filter by Evalue <=";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // SequestSummaryBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1222, 770);
      this.Name = "SequestSummaryBuilderUI";
      this.tabControl1.ResumeLayout(false);
      this.filterGroup.ResumeLayout(false);
      this.filterGroup.PerformLayout();
      this.datafileGroup.ResumeLayout(false);
      this.pnlDataFiles.Panel1.ResumeLayout(false);
      this.pnlDataFiles.Panel1.PerformLayout();
      this.pnlDataFiles.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pnlDataFiles)).EndInit();
      this.pnlDataFiles.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnAddFiles;
    private System.Windows.Forms.Button btnRemoveFiles;
    private System.Windows.Forms.TextBox txtXcorr1;
    private System.Windows.Forms.CheckBox cbFilterByXcorr;
    private System.Windows.Forms.TextBox txtMinDeltaCn;
    private System.Windows.Forms.CheckBox cbFilterByDeltaCn;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.Button btnClassification;
    private System.Windows.Forms.TextBox txtXcorr3;
    private System.Windows.Forms.TextBox txtXcorr2;
    private System.Windows.Forms.TextBox txtSpRank;
    private System.Windows.Forms.CheckBox cbFilterBySpRank;
    private System.Windows.Forms.Button btnAddSubDirectories;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button btnAddZips;
    private System.Windows.Forms.Button btnXml;
    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
  }
}
