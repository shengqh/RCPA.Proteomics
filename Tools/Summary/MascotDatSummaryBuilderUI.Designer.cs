namespace RCPA.Tools.Summary
{
  partial class MascotDatSummaryBuilderUI
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
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.cbFilterByScore = new System.Windows.Forms.CheckBox();
      this.btnFindDB = new System.Windows.Forms.Button();
      this.btnRenameDat = new System.Windows.Forms.Button();
      this.btnClassification = new System.Windows.Forms.Button();
      this.btnMgfFiles = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.label7 = new System.Windows.Forms.Label();
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
      this.tabControl1.Size = new System.Drawing.Size(1225, 646);
      // 
      // filterGroup
      // 
      this.filterGroup.Controls.Add(this.txtMaxEvalue);
      this.filterGroup.Controls.Add(this.cbFilterByScore);
      this.filterGroup.Controls.Add(this.cbFilterByEvalue);
      this.filterGroup.Controls.Add(this.txtMinScore);
      this.filterGroup.Size = new System.Drawing.Size(1217, 620);
      this.filterGroup.Controls.SetChildIndex(this.txtMinScore, 0);
      this.filterGroup.Controls.SetChildIndex(this.cbFilterByEvalue, 0);
      this.filterGroup.Controls.SetChildIndex(this.cbFilterByScore, 0);
      this.filterGroup.Controls.SetChildIndex(this.txtMaxEvalue, 0);
      // 
      // datafileGroup
      // 
      this.datafileGroup.Size = new System.Drawing.Size(1152, 569);
      // 
      // databaseGroup
      // 
      this.databaseGroup.Size = new System.Drawing.Size(1152, 569);
      // 
      // pnlDataFiles
      // 
      // 
      // pnlDataFiles.Panel2
      // 
      this.pnlDataFiles.Panel2.Controls.Add(this.btnFindDB);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnClassification);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnRenameDat);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnMgfFiles);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnSave);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnLoad);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnRemoveFiles);
      this.pnlDataFiles.Panel2.Controls.Add(this.btnAddFiles);
      this.pnlDataFiles.Size = new System.Drawing.Size(1146, 563);
      // 
      // lvDatFiles
      // 
      this.lvDatFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this.lvDatFiles.Size = new System.Drawing.Size(1070, 567);
      this.lvDatFiles.SizeChanged += new System.EventHandler(this.lvDatFiles_SizeChanged);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 646);
      this.lblProgress.Size = new System.Drawing.Size(1225, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 667);
      this.progressBar.Size = new System.Drawing.Size(1225, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(778, 6);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(457, 6);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(350, 6);
      // 
      // txtMaxEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(199, 320);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 21);
      this.txtMaxEvalue.TabIndex = 47;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(27, 322);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(174, 16);
      this.cbFilterByEvalue.TabIndex = 46;
      this.cbFilterByEvalue.Text = "Filter by Expect value = ";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // txtMinScore
      // 
      this.txtMinScore.Location = new System.Drawing.Point(199, 296);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(94, 21);
      this.txtMinScore.TabIndex = 45;
      // 
      // cbFilterByScore
      // 
      this.cbFilterByScore.AutoSize = true;
      this.cbFilterByScore.Location = new System.Drawing.Point(27, 298);
      this.cbFilterByScore.Name = "cbFilterByScore";
      this.cbFilterByScore.Size = new System.Drawing.Size(132, 16);
      this.cbFilterByScore.TabIndex = 44;
      this.cbFilterByScore.Text = "Filter by Score = ";
      this.cbFilterByScore.UseVisualStyleBackColor = true;
      // 
      // btnFindDB
      // 
      this.btnFindDB.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnFindDB.Location = new System.Drawing.Point(0, 147);
      this.btnFindDB.Name = "btnFindDB";
      this.btnFindDB.Size = new System.Drawing.Size(142, 21);
      this.btnFindDB.TabIndex = 30;
      this.btnFindDB.Text = "Find DB";
      this.btnFindDB.UseVisualStyleBackColor = true;
      this.btnFindDB.Click += new System.EventHandler(this.btnFindDB_Click);
      // 
      // btnRenameDat
      // 
      this.btnRenameDat.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnRenameDat.Location = new System.Drawing.Point(0, 105);
      this.btnRenameDat.Name = "btnRenameDat";
      this.btnRenameDat.Size = new System.Drawing.Size(142, 21);
      this.btnRenameDat.TabIndex = 29;
      this.btnRenameDat.Text = "Rename DAT";
      this.btnRenameDat.UseVisualStyleBackColor = true;
      this.btnRenameDat.Click += new System.EventHandler(this.btnRenameDat_Click);
      // 
      // btnClassification
      // 
      this.btnClassification.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnClassification.Location = new System.Drawing.Point(0, 126);
      this.btnClassification.Name = "btnClassification";
      this.btnClassification.Size = new System.Drawing.Size(142, 21);
      this.btnClassification.TabIndex = 28;
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
      this.btnMgfFiles.TabIndex = 27;
      this.btnMgfFiles.Text = "Find MGF";
      this.btnMgfFiles.UseVisualStyleBackColor = true;
      this.btnMgfFiles.Click += new System.EventHandler(this.btnMgfFiles_Click);
      // 
      // btnSave
      // 
      this.btnSave.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnSave.Location = new System.Drawing.Point(0, 63);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(142, 21);
      this.btnSave.TabIndex = 26;
      this.btnSave.Text = "save";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnLoad.Location = new System.Drawing.Point(0, 42);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(142, 21);
      this.btnLoad.TabIndex = 25;
      this.btnLoad.Text = "load";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnAddFiles
      // 
      this.btnAddFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddFiles.Location = new System.Drawing.Point(0, 0);
      this.btnAddFiles.Name = "btnAddFiles";
      this.btnAddFiles.Size = new System.Drawing.Size(142, 21);
      this.btnAddFiles.TabIndex = 23;
      this.btnAddFiles.Text = "add files";
      this.btnAddFiles.UseVisualStyleBackColor = true;
      // 
      // btnRemoveFiles
      // 
      this.btnRemoveFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnRemoveFiles.Location = new System.Drawing.Point(0, 21);
      this.btnRemoveFiles.Name = "btnRemoveFiles";
      this.btnRemoveFiles.Size = new System.Drawing.Size(142, 21);
      this.btnRemoveFiles.TabIndex = 24;
      this.btnRemoveFiles.Text = "remove files";
      this.btnRemoveFiles.UseVisualStyleBackColor = true;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Dat file";
      this.columnHeader1.Width = 403;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "MGF file";
      this.columnHeader2.Width = 299;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Classification";
      this.columnHeader3.Width = 113;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Database";
      this.columnHeader4.Width = 100;
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(0, 0);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(100, 23);
      this.label7.TabIndex = 0;
      // 
      // MascotDatSummaryBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1225, 724);
      this.Name = "MascotDatSummaryBuilderUI";
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

    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.CheckBox cbFilterByScore;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btnFindDB;
    private System.Windows.Forms.Button btnRenameDat;
    private System.Windows.Forms.Button btnClassification;
    private System.Windows.Forms.Button btnMgfFiles;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnAddFiles;
    private System.Windows.Forms.Button btnRemoveFiles;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
  }
}
