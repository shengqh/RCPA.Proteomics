namespace RCPA.Proteomics.Summary.Uniform
{
  partial class AbstractDatasetPanel
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
      this.pnlName = new System.Windows.Forms.Panel();
      this.cbEnabled = new System.Windows.Forms.CheckBox();
      this.txtDatasetName = new System.Windows.Forms.TextBox();
      this.lblName = new System.Windows.Forms.Label();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      this.cbFilterByScore = new System.Windows.Forms.CheckBox();
      this.cbFilterByDeltaScore = new System.Windows.Forms.CheckBox();
      this.txtMinDeltaScore = new System.Windows.Forms.TextBox();
      this.txtScore3 = new System.Windows.Forms.TextBox();
      this.txtScore2 = new System.Windows.Forms.TextBox();
      this.txtScore1 = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cbFilterByDynamicPrecursorTolerance = new System.Windows.Forms.CheckBox();
      this.cbFilterByPrecursorIsotopic = new System.Windows.Forms.CheckBox();
      this.txtPrecursorPPMTolerance = new System.Windows.Forms.TextBox();
      this.cbFilterByPrecursor = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.lvDatFiles = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.panel2 = new System.Windows.Forms.Panel();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.btnFind = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.pnlName.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlName
      // 
      this.pnlName.Controls.Add(this.cbEnabled);
      this.pnlName.Controls.Add(this.txtDatasetName);
      this.pnlName.Controls.Add(this.lblName);
      this.pnlName.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlName.Location = new System.Drawing.Point(0, 0);
      this.pnlName.Name = "pnlName";
      this.pnlName.Size = new System.Drawing.Size(1016, 38);
      this.pnlName.TabIndex = 55;
      // 
      // cbEnabled
      // 
      this.cbEnabled.AutoSize = true;
      this.cbEnabled.Checked = true;
      this.cbEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbEnabled.Location = new System.Drawing.Point(6, 11);
      this.cbEnabled.Name = "cbEnabled";
      this.cbEnabled.Size = new System.Drawing.Size(65, 17);
      this.cbEnabled.TabIndex = 5;
      this.cbEnabled.Text = "Enabled";
      this.cbEnabled.UseVisualStyleBackColor = true;
      // 
      // txtDatasetName
      // 
      this.txtDatasetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDatasetName.Location = new System.Drawing.Point(130, 9);
      this.txtDatasetName.Name = "txtDatasetName";
      this.txtDatasetName.Size = new System.Drawing.Size(880, 20);
      this.txtDatasetName.TabIndex = 4;
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(92, 12);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(35, 13);
      this.lblName.TabIndex = 3;
      this.lblName.Text = "Name";
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 38);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
      this.splitContainer2.Size = new System.Drawing.Size(1016, 372);
      this.splitContainer2.SplitterDistance = 95;
      this.splitContainer2.TabIndex = 56;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtMaxEvalue);
      this.groupBox1.Controls.Add(this.cbFilterByEvalue);
      this.groupBox1.Controls.Add(this.cbFilterByScore);
      this.groupBox1.Controls.Add(this.cbFilterByDeltaScore);
      this.groupBox1.Controls.Add(this.txtMinDeltaScore);
      this.groupBox1.Controls.Add(this.txtScore3);
      this.groupBox1.Controls.Add(this.txtScore2);
      this.groupBox1.Controls.Add(this.txtScore1);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.cbFilterByDynamicPrecursorTolerance);
      this.groupBox1.Controls.Add(this.cbFilterByPrecursorIsotopic);
      this.groupBox1.Controls.Add(this.txtPrecursorPPMTolerance);
      this.groupBox1.Controls.Add(this.cbFilterByPrecursor);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(1016, 95);
      this.groupBox1.TabIndex = 55;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Peptide criteria";
      // 
      // txtMaxEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(756, 43);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 20);
      this.txtMaxEvalue.TabIndex = 77;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(627, 45);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(113, 17);
      this.cbFilterByEvalue.TabIndex = 76;
      this.cbFilterByEvalue.Text = "Filter by Evalue <=";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // cbFilterByScore
      // 
      this.cbFilterByScore.AutoSize = true;
      this.cbFilterByScore.Location = new System.Drawing.Point(323, 17);
      this.cbFilterByScore.Name = "cbFilterByScore";
      this.cbFilterByScore.Size = new System.Drawing.Size(186, 17);
      this.cbFilterByScore.TabIndex = 68;
      this.cbFilterByScore.Text = "Filter by score: charge 1, score >=";
      this.cbFilterByScore.UseVisualStyleBackColor = true;
      // 
      // cbFilterByDeltaScore
      // 
      this.cbFilterByDeltaScore.AutoSize = true;
      this.cbFilterByDeltaScore.Location = new System.Drawing.Point(627, 18);
      this.cbFilterByDeltaScore.Name = "cbFilterByDeltaScore";
      this.cbFilterByDeltaScore.Size = new System.Drawing.Size(132, 17);
      this.cbFilterByDeltaScore.TabIndex = 72;
      this.cbFilterByDeltaScore.Text = "Filter by delta score >=";
      this.cbFilterByDeltaScore.UseVisualStyleBackColor = true;
      // 
      // txtMinDeltaScore
      // 
      this.txtMinDeltaScore.Location = new System.Drawing.Point(756, 15);
      this.txtMinDeltaScore.Name = "txtMinDeltaScore";
      this.txtMinDeltaScore.Size = new System.Drawing.Size(94, 20);
      this.txtMinDeltaScore.TabIndex = 73;
      // 
      // txtScore3
      // 
      this.txtScore3.Location = new System.Drawing.Point(510, 66);
      this.txtScore3.Name = "txtScore3";
      this.txtScore3.Size = new System.Drawing.Size(94, 20);
      this.txtScore3.TabIndex = 69;
      // 
      // txtScore2
      // 
      this.txtScore2.Location = new System.Drawing.Point(510, 40);
      this.txtScore2.Name = "txtScore2";
      this.txtScore2.Size = new System.Drawing.Size(94, 20);
      this.txtScore2.TabIndex = 70;
      // 
      // txtScore1
      // 
      this.txtScore1.Location = new System.Drawing.Point(510, 14);
      this.txtScore1.Name = "txtScore1";
      this.txtScore1.Size = new System.Drawing.Size(94, 20);
      this.txtScore1.TabIndex = 71;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(409, 69);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(99, 13);
      this.label6.TabIndex = 75;
      this.label6.Text = "charge 3+, score>=";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(409, 43);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(96, 13);
      this.label1.TabIndex = 74;
      this.label1.Text = "charge 2, score >=";
      // 
      // cbFilterByDynamicPrecursorTolerance
      // 
      this.cbFilterByDynamicPrecursorTolerance.AutoSize = true;
      this.cbFilterByDynamicPrecursorTolerance.Location = new System.Drawing.Point(31, 68);
      this.cbFilterByDynamicPrecursorTolerance.Name = "cbFilterByDynamicPrecursorTolerance";
      this.cbFilterByDynamicPrecursorTolerance.Size = new System.Drawing.Size(187, 17);
      this.cbFilterByDynamicPrecursorTolerance.TabIndex = 65;
      this.cbFilterByDynamicPrecursorTolerance.Text = "using dynamic precursor tolerance";
      this.cbFilterByDynamicPrecursorTolerance.UseVisualStyleBackColor = true;
      // 
      // cbFilterByPrecursorIsotopic
      // 
      this.cbFilterByPrecursorIsotopic.AutoSize = true;
      this.cbFilterByPrecursorIsotopic.Location = new System.Drawing.Point(31, 45);
      this.cbFilterByPrecursorIsotopic.Name = "cbFilterByPrecursorIsotopic";
      this.cbFilterByPrecursorIsotopic.Size = new System.Drawing.Size(137, 17);
      this.cbFilterByPrecursorIsotopic.TabIndex = 65;
      this.cbFilterByPrecursorIsotopic.Text = "consider isotopic peaks";
      this.cbFilterByPrecursorIsotopic.UseVisualStyleBackColor = true;
      // 
      // txtPrecursorPPMTolerance
      // 
      this.txtPrecursorPPMTolerance.Location = new System.Drawing.Point(207, 16);
      this.txtPrecursorPPMTolerance.Name = "txtPrecursorPPMTolerance";
      this.txtPrecursorPPMTolerance.Size = new System.Drawing.Size(94, 20);
      this.txtPrecursorPPMTolerance.TabIndex = 64;
      // 
      // cbFilterByPrecursor
      // 
      this.cbFilterByPrecursor.AutoSize = true;
      this.cbFilterByPrecursor.Location = new System.Drawing.Point(9, 18);
      this.cbFilterByPrecursor.Name = "cbFilterByPrecursor";
      this.cbFilterByPrecursor.Size = new System.Drawing.Size(194, 17);
      this.cbFilterByPrecursor.TabIndex = 63;
      this.cbFilterByPrecursor.Text = "Filter by precursor tolerance (ppm) =";
      this.cbFilterByPrecursor.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.lvDatFiles);
      this.groupBox2.Controls.Add(this.panel2);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(1016, 273);
      this.groupBox2.TabIndex = 57;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Select data files";
      // 
      // lvDatFiles
      // 
      this.lvDatFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.lvDatFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvDatFiles.FullRowSelect = true;
      this.lvDatFiles.HideSelection = false;
      this.lvDatFiles.Location = new System.Drawing.Point(3, 16);
      this.lvDatFiles.Name = "lvDatFiles";
      this.lvDatFiles.Size = new System.Drawing.Size(885, 254);
      this.lvDatFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvDatFiles.TabIndex = 15;
      this.lvDatFiles.UseCompatibleStateImageBehavior = false;
      this.lvDatFiles.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Data file";
      this.columnHeader1.Width = 403;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Source file";
      this.columnHeader2.Width = 299;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Database";
      this.columnHeader3.Width = 187;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.btnSave);
      this.panel2.Controls.Add(this.btnLoad);
      this.panel2.Controls.Add(this.btnRemoveFiles);
      this.panel2.Controls.Add(this.btnAddFiles);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel2.Location = new System.Drawing.Point(888, 16);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(125, 254);
      this.panel2.TabIndex = 19;
      // 
      // btnSave
      // 
      this.btnSave.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnSave.Location = new System.Drawing.Point(0, 69);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(125, 23);
      this.btnSave.TabIndex = 22;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnLoad.Location = new System.Drawing.Point(0, 46);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(125, 23);
      this.btnLoad.TabIndex = 21;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnRemoveFiles
      // 
      this.btnRemoveFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnRemoveFiles.Location = new System.Drawing.Point(0, 23);
      this.btnRemoveFiles.Name = "btnRemoveFiles";
      this.btnRemoveFiles.Size = new System.Drawing.Size(125, 23);
      this.btnRemoveFiles.TabIndex = 20;
      this.btnRemoveFiles.Text = "Remove";
      this.btnRemoveFiles.UseVisualStyleBackColor = true;
      // 
      // btnAddFiles
      // 
      this.btnAddFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddFiles.Location = new System.Drawing.Point(0, 0);
      this.btnAddFiles.Name = "btnAddFiles";
      this.btnAddFiles.Size = new System.Drawing.Size(125, 23);
      this.btnAddFiles.TabIndex = 19;
      this.btnAddFiles.Text = "Add";
      this.btnAddFiles.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cbTitleFormat);
      this.panel1.Controls.Add(this.btnFind);
      this.panel1.Controls.Add(this.label7);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 410);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1016, 22);
      this.panel1.TabIndex = 57;
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(65, 0);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(823, 21);
      this.cbTitleFormat.TabIndex = 30;
      // 
      // btnFind
      // 
      this.btnFind.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnFind.Location = new System.Drawing.Point(888, 0);
      this.btnFind.Name = "btnFind";
      this.btnFind.Size = new System.Drawing.Size(128, 22);
      this.btnFind.TabIndex = 32;
      this.btnFind.Text = "Find title format";
      this.btnFind.UseVisualStyleBackColor = true;
      // 
      // label7
      // 
      this.label7.Dock = System.Windows.Forms.DockStyle.Left;
      this.label7.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.label7.Location = new System.Drawing.Point(0, 0);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(65, 22);
      this.label7.TabIndex = 31;
      this.label7.Text = "Title format :";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // AbstractDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer2);
      this.Controls.Add(this.pnlName);
      this.Controls.Add(this.panel1);
      this.Name = "AbstractDatasetPanel";
      this.Size = new System.Drawing.Size(1016, 432);
      this.pnlName.ResumeLayout(false);
      this.pnlName.PerformLayout();
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlName;
    private System.Windows.Forms.CheckBox cbEnabled;
    private System.Windows.Forms.TextBox txtDatasetName;
    private System.Windows.Forms.Label lblName;
    protected System.Windows.Forms.SplitContainer splitContainer2;
    protected System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox cbFilterByDynamicPrecursorTolerance;
    private System.Windows.Forms.CheckBox cbFilterByPrecursorIsotopic;
    private System.Windows.Forms.TextBox txtPrecursorPPMTolerance;
    private System.Windows.Forms.CheckBox cbFilterByPrecursor;
    private System.Windows.Forms.Panel panel1;
    protected System.Windows.Forms.ComboBox cbTitleFormat;
    private System.Windows.Forms.Button btnFind;
    protected System.Windows.Forms.Label label7;
    protected System.Windows.Forms.GroupBox groupBox2;
    protected System.Windows.Forms.ListView lvDatFiles;
    protected System.Windows.Forms.ColumnHeader columnHeader1;
    protected System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.Panel panel2;
    protected System.Windows.Forms.Button btnSave;
    protected System.Windows.Forms.Button btnLoad;
    protected System.Windows.Forms.Button btnRemoveFiles;
    protected System.Windows.Forms.Button btnAddFiles;
    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
    private System.Windows.Forms.CheckBox cbFilterByScore;
    private System.Windows.Forms.CheckBox cbFilterByDeltaScore;
    private System.Windows.Forms.TextBox txtMinDeltaScore;
    private System.Windows.Forms.TextBox txtScore3;
    private System.Windows.Forms.TextBox txtScore2;
    private System.Windows.Forms.TextBox txtScore1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label1;

  }
}
