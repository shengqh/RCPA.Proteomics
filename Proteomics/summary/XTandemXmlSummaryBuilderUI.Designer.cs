namespace RCPA.Tools.Summary
{
  partial class XTandemXmlSummaryBuilderUI
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
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.label7 = new System.Windows.Forms.Label();
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.cbFilterByScore = new System.Windows.Forms.CheckBox();
      this.cbIgnoreUnanticipatedPeptide = new System.Windows.Forms.CheckBox();
      this.btnClassification = new System.Windows.Forms.Button();
      this.btnMgfFiles = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
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
      this.tabControl1.Size = new System.Drawing.Size(1273, 646);
      // 
      // filterGroup
      // 
      this.filterGroup.Controls.Add(this.txtMinScore);
      this.filterGroup.Controls.Add(this.cbFilterByScore);
      this.filterGroup.Controls.Add(this.txtMaxEvalue);
      this.filterGroup.Controls.Add(this.cbFilterByEvalue);
      this.filterGroup.Controls.Add(this.cbIgnoreUnanticipatedPeptide);
      this.filterGroup.Size = new System.Drawing.Size(1265, 620);
      this.filterGroup.Controls.SetChildIndex(this.cbIgnoreUnanticipatedPeptide, 0);
      this.filterGroup.Controls.SetChildIndex(this.cbFilterByEvalue, 0);
      this.filterGroup.Controls.SetChildIndex(this.txtMaxEvalue, 0);
      this.filterGroup.Controls.SetChildIndex(this.cbFilterByScore, 0);
      this.filterGroup.Controls.SetChildIndex(this.txtMinScore, 0);
      // 
      // datafileGroup
      // 
      this.datafileGroup.Size = new System.Drawing.Size(1195, 558);
      // 
      // databaseGroup
      // 
      this.databaseGroup.Size = new System.Drawing.Size(1195, 558);
      // 
      // pnlDataFiles
      // 
      // 
      // pnlDataFiles.Panel2
      // 
      this.pnlDataFiles.Panel2.Controls.Add(this.btnClassification);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnMgfFiles);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnSave);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnLoad);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnRemoveFiles);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnAddFiles);
      this.pnlDataFiles.Size = new System.Drawing.Size(1189, 552);
      this.pnlDataFiles.SplitterDistance = 1043;
      // 
      // lvDatFiles
      // 
      this.lvDatFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.lvDatFiles.Size = new System.Drawing.Size(1043, 518);
      this.lvDatFiles.TabIndex = 15;
      this.lvDatFiles.SizeChanged += new System.EventHandler(this.lvDatFiles_SizeChanged);
      // 
      // lblDataFile
      // 
      this.lblDataFile.Text = "Select xml files you want to extract peptides (only selected file will be used)";
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 646);
      this.lblProgress.Size = new System.Drawing.Size(1273, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 667);
      this.progressBar.Size = new System.Drawing.Size(1273, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(802, 6);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(481, 6);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(374, 6);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Xml file";
      this.columnHeader1.Width = 403;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Source file";
      this.columnHeader2.Width = 267;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Classification";
      this.columnHeader3.Width = 113;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(24, 214);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(89, 12);
      this.label7.TabIndex = 20;
      this.label7.Text = "Title format :";
      // 
      // txtMaxEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(198, 318);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 21);
      this.txtMaxEvalue.TabIndex = 48;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(27, 321);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(174, 16);
      this.cbFilterByEvalue.TabIndex = 47;
      this.cbFilterByEvalue.Text = "Filter by Expect value = ";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // txtMinScore
      // 
      this.txtMinScore.Location = new System.Drawing.Point(155, 295);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(94, 21);
      this.txtMinScore.TabIndex = 46;
      // 
      // cbFilterByScore
      // 
      this.cbFilterByScore.AutoSize = true;
      this.cbFilterByScore.Location = new System.Drawing.Point(27, 297);
      this.cbFilterByScore.Name = "cbFilterByScore";
      this.cbFilterByScore.Size = new System.Drawing.Size(132, 16);
      this.cbFilterByScore.TabIndex = 45;
      this.cbFilterByScore.Text = "Filter by Score = ";
      this.cbFilterByScore.UseVisualStyleBackColor = true;
      // 
      // cbIgnoreUnanticipatedPeptide
      // 
      this.cbIgnoreUnanticipatedPeptide.AutoSize = true;
      this.cbIgnoreUnanticipatedPeptide.Location = new System.Drawing.Point(301, 320);
      this.cbIgnoreUnanticipatedPeptide.Name = "cbIgnoreUnanticipatedPeptide";
      this.cbIgnoreUnanticipatedPeptide.Size = new System.Drawing.Size(306, 16);
      this.cbIgnoreUnanticipatedPeptide.TabIndex = 44;
      this.cbIgnoreUnanticipatedPeptide.Text = "Ignore peptide with unanticipated cleavage site";
      this.cbIgnoreUnanticipatedPeptide.UseVisualStyleBackColor = true;
      // 
      // btnClassification
      // 
      this.btnClassification.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnClassification.Location = new System.Drawing.Point(0, 105);
      this.btnClassification.Name = "btnClassification";
      this.btnClassification.Size = new System.Drawing.Size(142, 21);
      this.btnClassification.TabIndex = 37;
      this.btnClassification.Text = "Classify";
      this.btnClassification.UseVisualStyleBackColor = true;
      this.btnClassification.Click += new System.EventHandler(this.btnClassification_Click);
      // 
      // btnMgfFiles
      // 
      this.btnMgfFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnMgfFiles.Location = new System.Drawing.Point(0, 84);
      this.btnMgfFiles.Name = "btnMgfFiles";
      this.btnMgfFiles.Size = new System.Drawing.Size(142, 21);
      this.btnMgfFiles.TabIndex = 36;
      this.btnMgfFiles.Text = "Find Source";
      this.btnMgfFiles.UseVisualStyleBackColor = true;
      this.btnMgfFiles.Click += new System.EventHandler(this.btnMgfFiles_Click);
      // 
      // btnSave
      // 
      this.btnSave.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnSave.Location = new System.Drawing.Point(0, 63);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(142, 21);
      this.btnSave.TabIndex = 35;
      this.btnSave.Text = "button2";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnLoad.Location = new System.Drawing.Point(0, 42);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(142, 21);
      this.btnLoad.TabIndex = 34;
      this.btnLoad.Text = "button1";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnAddFiles
      // 
      this.btnAddFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddFiles.Location = new System.Drawing.Point(0, 0);
      this.btnAddFiles.Name = "btnAddFiles";
      this.btnAddFiles.Size = new System.Drawing.Size(142, 21);
      this.btnAddFiles.TabIndex = 32;
      this.btnAddFiles.Text = "button1";
      this.btnAddFiles.UseVisualStyleBackColor = true;
      // 
      // btnRemoveFiles
      // 
      this.btnRemoveFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnRemoveFiles.Location = new System.Drawing.Point(0, 21);
      this.btnRemoveFiles.Name = "btnRemoveFiles";
      this.btnRemoveFiles.Size = new System.Drawing.Size(142, 21);
      this.btnRemoveFiles.TabIndex = 33;
      this.btnRemoveFiles.Text = "button2";
      this.btnRemoveFiles.UseVisualStyleBackColor = true;
      // 
      // XTandemXmlSummaryBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1273, 724);
      this.Name = "XTandemXmlSummaryBuilderUI";
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

    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.CheckBox cbFilterByScore;
    private System.Windows.Forms.CheckBox cbIgnoreUnanticipatedPeptide;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btnClassification;
    private System.Windows.Forms.Button btnMgfFiles;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnAddFiles;
    private System.Windows.Forms.Button btnRemoveFiles;
  }
}
