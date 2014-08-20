namespace RCPA.Proteomics.Summary.Uniform
{
  partial class SequestDatasetPanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.cbFilterByXcorr = new System.Windows.Forms.CheckBox();
      this.txtXcorr1 = new System.Windows.Forms.TextBox();
      this.txtXcorr2 = new System.Windows.Forms.TextBox();
      this.txtXcorr3 = new System.Windows.Forms.TextBox();
      this.cbFilterByDeltaCn = new System.Windows.Forms.CheckBox();
      this.txtMinDeltaCn = new System.Windows.Forms.TextBox();
      this.cbFilterBySpRank = new System.Windows.Forms.CheckBox();
      this.txtSpRank = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.outsGroup = new System.Windows.Forms.GroupBox();
      this.btnXml = new System.Windows.Forms.Button();
      this.btnAddZips = new System.Windows.Forms.Button();
      this.lvDirectories = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.btnAddSubDirectories = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.outsGroup.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Size = new System.Drawing.Size(1016, 399);
      // 
      // splitContainer2
      // 
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.outsGroup);
      this.splitContainer2.Size = new System.Drawing.Size(1016, 359);
      this.splitContainer2.SplitterDistance = 130;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtMaxEvalue);
      this.groupBox1.Controls.Add(this.cbFilterByEvalue);
      this.groupBox1.Controls.Add(this.cbFilterByXcorr);
      this.groupBox1.Controls.Add(this.cbFilterByDeltaCn);
      this.groupBox1.Controls.Add(this.txtMinDeltaCn);
      this.groupBox1.Controls.Add(this.txtXcorr3);
      this.groupBox1.Controls.Add(this.txtSpRank);
      this.groupBox1.Controls.Add(this.txtXcorr2);
      this.groupBox1.Controls.Add(this.cbFilterBySpRank);
      this.groupBox1.Controls.Add(this.txtXcorr1);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Size = new System.Drawing.Size(1016, 130);
      this.groupBox1.Text = "Peptide Filter Criteria";
      this.groupBox1.Controls.SetChildIndex(this.label7, 0);
      this.groupBox1.Controls.SetChildIndex(this.label6, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtXcorr1, 0);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterBySpRank, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtXcorr2, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtSpRank, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtXcorr3, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtMinDeltaCn, 0);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByDeltaCn, 0);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByXcorr, 0);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByEvalue, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtMaxEvalue, 0);
      // 
      // cbFilterByXcorr
      // 
      this.cbFilterByXcorr.AutoSize = true;
      this.cbFilterByXcorr.Location = new System.Drawing.Point(9, 50);
      this.cbFilterByXcorr.Name = "cbFilterByXcorr";
      this.cbFilterByXcorr.Size = new System.Drawing.Size(186, 17);
      this.cbFilterByXcorr.TabIndex = 51;
      this.cbFilterByXcorr.Text = "Filter by XCorr: charge=1, XCorr>=";
      this.cbFilterByXcorr.UseVisualStyleBackColor = true;
      // 
      // txtXcorr1
      // 
      this.txtXcorr1.Location = new System.Drawing.Point(243, 47);
      this.txtXcorr1.Name = "txtXcorr1";
      this.txtXcorr1.Size = new System.Drawing.Size(66, 20);
      this.txtXcorr1.TabIndex = 54;
      // 
      // txtXcorr2
      // 
      this.txtXcorr2.Location = new System.Drawing.Point(445, 47);
      this.txtXcorr2.Name = "txtXcorr2";
      this.txtXcorr2.Size = new System.Drawing.Size(66, 20);
      this.txtXcorr2.TabIndex = 53;
      // 
      // txtXcorr3
      // 
      this.txtXcorr3.Location = new System.Drawing.Point(655, 47);
      this.txtXcorr3.Name = "txtXcorr3";
      this.txtXcorr3.Size = new System.Drawing.Size(66, 20);
      this.txtXcorr3.TabIndex = 52;
      // 
      // cbFilterByDeltaCn
      // 
      this.cbFilterByDeltaCn.AutoSize = true;
      this.cbFilterByDeltaCn.Location = new System.Drawing.Point(9, 75);
      this.cbFilterByDeltaCn.Name = "cbFilterByDeltaCn";
      this.cbFilterByDeltaCn.Size = new System.Drawing.Size(120, 17);
      this.cbFilterByDeltaCn.TabIndex = 56;
      this.cbFilterByDeltaCn.Text = "Filter by DeltaCN >=";
      this.cbFilterByDeltaCn.UseVisualStyleBackColor = true;
      // 
      // txtMinDeltaCn
      // 
      this.txtMinDeltaCn.Location = new System.Drawing.Point(161, 73);
      this.txtMinDeltaCn.Name = "txtMinDeltaCn";
      this.txtMinDeltaCn.Size = new System.Drawing.Size(94, 20);
      this.txtMinDeltaCn.TabIndex = 58;
      // 
      // cbFilterBySpRank
      // 
      this.cbFilterBySpRank.AutoSize = true;
      this.cbFilterBySpRank.Location = new System.Drawing.Point(9, 102);
      this.cbFilterBySpRank.Name = "cbFilterBySpRank";
      this.cbFilterBySpRank.Size = new System.Drawing.Size(119, 17);
      this.cbFilterBySpRank.TabIndex = 55;
      this.cbFilterBySpRank.Text = "Filter by SpRank >=";
      this.cbFilterBySpRank.UseVisualStyleBackColor = true;
      // 
      // txtSpRank
      // 
      this.txtSpRank.Location = new System.Drawing.Point(161, 100);
      this.txtSpRank.Name = "txtSpRank";
      this.txtSpRank.Size = new System.Drawing.Size(94, 20);
      this.txtSpRank.TabIndex = 57;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(536, 50);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(102, 13);
      this.label6.TabIndex = 60;
      this.label6.Text = "charge>=3, XCorr>=";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(327, 50);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(96, 13);
      this.label7.TabIndex = 59;
      this.label7.Text = "charge=2, XCorr>=";
      // 
      // outsGroup
      // 
      this.outsGroup.Controls.Add(this.btnXml);
      this.outsGroup.Controls.Add(this.btnAddZips);
      this.outsGroup.Controls.Add(this.lvDirectories);
      this.outsGroup.Controls.Add(this.btnSave);
      this.outsGroup.Controls.Add(this.btnLoad);
      this.outsGroup.Controls.Add(this.btnAddFiles);
      this.outsGroup.Controls.Add(this.btnAddSubDirectories);
      this.outsGroup.Controls.Add(this.btnRemoveFiles);
      this.outsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
      this.outsGroup.Location = new System.Drawing.Point(0, 0);
      this.outsGroup.Name = "outsGroup";
      this.outsGroup.Size = new System.Drawing.Size(1016, 225);
      this.outsGroup.TabIndex = 25;
      this.outsGroup.TabStop = false;
      this.outsGroup.Text = "Select directories/files you want to extract peptides (all directories will be us" +
    "ed)";
      // 
      // btnXml
      // 
      this.btnXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnXml.Location = new System.Drawing.Point(913, 178);
      this.btnXml.Name = "btnXml";
      this.btnXml.Size = new System.Drawing.Size(97, 25);
      this.btnXml.TabIndex = 20;
      this.btnXml.Text = "Add Xml Files";
      this.btnXml.UseVisualStyleBackColor = true;
      this.btnXml.Click += new System.EventHandler(this.btnXml_Click);
      // 
      // btnAddZips
      // 
      this.btnAddZips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddZips.Location = new System.Drawing.Point(913, 153);
      this.btnAddZips.Name = "btnAddZips";
      this.btnAddZips.Size = new System.Drawing.Size(97, 25);
      this.btnAddZips.TabIndex = 19;
      this.btnAddZips.Text = "Add Zip Files";
      this.btnAddZips.UseVisualStyleBackColor = true;
      this.btnAddZips.Click += new System.EventHandler(this.btnAddZips_Click);
      // 
      // lvDirectories
      // 
      this.lvDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lvDirectories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
      this.lvDirectories.FullRowSelect = true;
      this.lvDirectories.HideSelection = false;
      this.lvDirectories.Location = new System.Drawing.Point(6, 28);
      this.lvDirectories.Name = "lvDirectories";
      this.lvDirectories.Size = new System.Drawing.Size(901, 190);
      this.lvDirectories.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvDirectories.TabIndex = 15;
      this.lvDirectories.UseCompatibleStateImageBehavior = false;
      this.lvDirectories.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Directories";
      this.columnHeader1.Width = 723;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(913, 128);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(97, 25);
      this.btnSave.TabIndex = 14;
      this.btnSave.Text = "button2";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(913, 103);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(97, 25);
      this.btnLoad.TabIndex = 13;
      this.btnLoad.Text = "button1";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnAddFiles
      // 
      this.btnAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddFiles.Location = new System.Drawing.Point(913, 28);
      this.btnAddFiles.Name = "btnAddFiles";
      this.btnAddFiles.Size = new System.Drawing.Size(97, 25);
      this.btnAddFiles.TabIndex = 11;
      this.btnAddFiles.Text = "button1";
      this.btnAddFiles.UseVisualStyleBackColor = true;
      // 
      // btnAddSubDirectories
      // 
      this.btnAddSubDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddSubDirectories.Location = new System.Drawing.Point(913, 78);
      this.btnAddSubDirectories.Name = "btnAddSubDirectories";
      this.btnAddSubDirectories.Size = new System.Drawing.Size(97, 25);
      this.btnAddSubDirectories.TabIndex = 12;
      this.btnAddSubDirectories.Text = "Add Subdirs";
      this.btnAddSubDirectories.UseVisualStyleBackColor = true;
      this.btnAddSubDirectories.Click += new System.EventHandler(this.btnAddSubDirectories_Click);
      // 
      // btnRemoveFiles
      // 
      this.btnRemoveFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRemoveFiles.Location = new System.Drawing.Point(913, 53);
      this.btnRemoveFiles.Name = "btnRemoveFiles";
      this.btnRemoveFiles.Size = new System.Drawing.Size(97, 25);
      this.btnRemoveFiles.TabIndex = 12;
      this.btnRemoveFiles.Text = "button2";
      this.btnRemoveFiles.UseVisualStyleBackColor = true;
      // 
      // txtMaxCometEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(528, 100);
      this.txtMaxEvalue.Name = "txtMaxCometEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 20);
      this.txtMaxEvalue.TabIndex = 67;
      // 
      // cbFilterByCometEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(376, 102);
      this.cbFilterByEvalue.Name = "cbFilterByCometEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(113, 17);
      this.cbFilterByEvalue.TabIndex = 66;
      this.cbFilterByEvalue.Text = "Filter by Evalue <=";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // SequestDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "SequestDatasetPanel";
      this.Size = new System.Drawing.Size(1016, 399);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.outsGroup.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox cbFilterByXcorr;
    private System.Windows.Forms.TextBox txtXcorr1;
    private System.Windows.Forms.TextBox txtXcorr2;
    private System.Windows.Forms.TextBox txtXcorr3;
    private System.Windows.Forms.CheckBox cbFilterByDeltaCn;
    private System.Windows.Forms.TextBox txtMinDeltaCn;
    private System.Windows.Forms.CheckBox cbFilterBySpRank;
    private System.Windows.Forms.TextBox txtSpRank;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.GroupBox outsGroup;
    private System.Windows.Forms.ListView lvDirectories;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnAddFiles;
    private System.Windows.Forms.Button btnAddSubDirectories;
    private System.Windows.Forms.Button btnRemoveFiles;
    private System.Windows.Forms.Button btnAddZips;
    private System.Windows.Forms.Button btnXml;
    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;

  }
}
