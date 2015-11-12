namespace RCPA.Proteomics.Sequest
{
  partial class AbstractSequestDatasetPanel
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
      this.lvDirectories = new System.Windows.Forms.ListView();
      this.colData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.pnlButton = new System.Windows.Forms.Panel();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.outsGroup.SuspendLayout();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer2
      // 
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.outsGroup);
      this.splitContainer2.Size = new System.Drawing.Size(1016, 361);
      this.splitContainer2.SplitterDistance = 100;
      // 
      // groupBox1
      // 
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
      this.groupBox1.Size = new System.Drawing.Size(1016, 100);
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
      this.txtXcorr1.Location = new System.Drawing.Point(207, 47);
      this.txtXcorr1.Name = "txtXcorr1";
      this.txtXcorr1.Size = new System.Drawing.Size(50, 20);
      this.txtXcorr1.TabIndex = 54;
      // 
      // txtXcorr2
      // 
      this.txtXcorr2.Location = new System.Drawing.Point(366, 47);
      this.txtXcorr2.Name = "txtXcorr2";
      this.txtXcorr2.Size = new System.Drawing.Size(57, 20);
      this.txtXcorr2.TabIndex = 53;
      // 
      // txtXcorr3
      // 
      this.txtXcorr3.Location = new System.Drawing.Point(528, 47);
      this.txtXcorr3.Name = "txtXcorr3";
      this.txtXcorr3.Size = new System.Drawing.Size(55, 20);
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
      this.txtMinDeltaCn.Location = new System.Drawing.Point(135, 73);
      this.txtMinDeltaCn.Name = "txtMinDeltaCn";
      this.txtMinDeltaCn.Size = new System.Drawing.Size(60, 20);
      this.txtMinDeltaCn.TabIndex = 58;
      // 
      // cbFilterBySpRank
      // 
      this.cbFilterBySpRank.AutoSize = true;
      this.cbFilterBySpRank.Location = new System.Drawing.Point(201, 75);
      this.cbFilterBySpRank.Name = "cbFilterBySpRank";
      this.cbFilterBySpRank.Size = new System.Drawing.Size(119, 17);
      this.cbFilterBySpRank.TabIndex = 55;
      this.cbFilterBySpRank.Text = "Filter by SpRank >=";
      this.cbFilterBySpRank.UseVisualStyleBackColor = true;
      // 
      // txtSpRank
      // 
      this.txtSpRank.Location = new System.Drawing.Point(317, 73);
      this.txtSpRank.Name = "txtSpRank";
      this.txtSpRank.Size = new System.Drawing.Size(57, 20);
      this.txtSpRank.TabIndex = 57;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(429, 50);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(102, 13);
      this.label6.TabIndex = 60;
      this.label6.Text = "charge>=3, XCorr>=";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(264, 50);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(96, 13);
      this.label7.TabIndex = 59;
      this.label7.Text = "charge=2, XCorr>=";
      // 
      // outsGroup
      // 
      this.outsGroup.Controls.Add(this.lvDirectories);
      this.outsGroup.Controls.Add(this.pnlButton);
      this.outsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
      this.outsGroup.Location = new System.Drawing.Point(0, 0);
      this.outsGroup.Name = "outsGroup";
      this.outsGroup.Size = new System.Drawing.Size(1016, 257);
      this.outsGroup.TabIndex = 25;
      this.outsGroup.TabStop = false;
      this.outsGroup.Text = "Input search results";
      // 
      // lvDirectories
      // 
      this.lvDirectories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colData});
      this.lvDirectories.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvDirectories.FullRowSelect = true;
      this.lvDirectories.HideSelection = false;
      this.lvDirectories.Location = new System.Drawing.Point(3, 16);
      this.lvDirectories.Name = "lvDirectories";
      this.lvDirectories.Size = new System.Drawing.Size(885, 238);
      this.lvDirectories.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvDirectories.TabIndex = 15;
      this.lvDirectories.UseCompatibleStateImageBehavior = false;
      this.lvDirectories.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.colData.Text = "Search results";
      this.colData.Width = 723;
      // 
      // pnlButton
      // 
      this.pnlButton.Controls.Add(this.btnSave);
      this.pnlButton.Controls.Add(this.btnLoad);
      this.pnlButton.Controls.Add(this.btnRemoveFiles);
      this.pnlButton.Controls.Add(this.btnAddFiles);
      this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
      this.pnlButton.Location = new System.Drawing.Point(888, 16);
      this.pnlButton.Name = "pnlButton";
      this.pnlButton.Size = new System.Drawing.Size(125, 238);
      this.pnlButton.TabIndex = 21;
      // 
      // btnSave
      // 
      this.btnSave.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnSave.Location = new System.Drawing.Point(0, 75);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(125, 25);
      this.btnSave.TabIndex = 25;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnLoad.Location = new System.Drawing.Point(0, 50);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(125, 25);
      this.btnLoad.TabIndex = 24;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnRemoveFiles
      // 
      this.btnRemoveFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnRemoveFiles.Location = new System.Drawing.Point(0, 25);
      this.btnRemoveFiles.Name = "btnRemoveFiles";
      this.btnRemoveFiles.Size = new System.Drawing.Size(125, 25);
      this.btnRemoveFiles.TabIndex = 23;
      this.btnRemoveFiles.Text = "Remove";
      this.btnRemoveFiles.UseVisualStyleBackColor = true;
      // 
      // btnAddFiles
      // 
      this.btnAddFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddFiles.Location = new System.Drawing.Point(0, 0);
      this.btnAddFiles.Name = "btnAddFiles";
      this.btnAddFiles.Size = new System.Drawing.Size(125, 25);
      this.btnAddFiles.TabIndex = 21;
      this.btnAddFiles.Text = "Add";
      this.btnAddFiles.UseVisualStyleBackColor = true;
      // 
      // AbstractSequestDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "AbstractSequestDatasetPanel";
      this.Size = new System.Drawing.Size(1016, 399);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.outsGroup.ResumeLayout(false);
      this.pnlButton.ResumeLayout(false);
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
    protected System.Windows.Forms.Panel pnlButton;
    protected System.Windows.Forms.ListView lvDirectories;
    protected System.Windows.Forms.ColumnHeader colData;
    protected System.Windows.Forms.Button btnSave;
    protected System.Windows.Forms.Button btnLoad;
    protected System.Windows.Forms.Button btnRemoveFiles;
    protected System.Windows.Forms.Button btnAddFiles;

  }
}
