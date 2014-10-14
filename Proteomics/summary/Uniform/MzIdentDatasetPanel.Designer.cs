namespace RCPA.Proteomics.Summary.Uniform
{
  partial class MzIdentDatasetPanel
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
      this.lvDatFiles = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnFindDatabase = new System.Windows.Forms.Button();
      this.btnAutoRename = new System.Windows.Forms.Button();
      this.btnMgfFiles = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.cbEngines = new System.Windows.Forms.ComboBox();
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.cbFilterByScore = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer3
      // 
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.lvDatFiles);
      this.splitContainer3.Panel1.Controls.Add(this.panel1);
      this.splitContainer3.Size = new System.Drawing.Size(1016, 310);
      this.splitContainer3.SplitterDistance = 280;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Size = new System.Drawing.Size(1016, 394);
      this.splitContainer2.SplitterDistance = 80;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtMinScore);
      this.groupBox1.Controls.Add(this.cbFilterByScore);
      this.groupBox1.Controls.Add(this.cbEngines);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Size = new System.Drawing.Size(1016, 80);
      this.groupBox1.Controls.SetChildIndex(this.label1, 0);
      this.groupBox1.Controls.SetChildIndex(this.cbEngines, 0);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByScore, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtMinScore, 0);
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
      this.lvDatFiles.Location = new System.Drawing.Point(0, 0);
      this.lvDatFiles.Name = "lvDatFiles";
      this.lvDatFiles.Size = new System.Drawing.Size(891, 280);
      this.lvDatFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvDatFiles.TabIndex = 20;
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
      // panel1
      // 
      this.panel1.Controls.Add(this.btnFindDatabase);
      this.panel1.Controls.Add(this.btnAutoRename);
      this.panel1.Controls.Add(this.btnMgfFiles);
      this.panel1.Controls.Add(this.btnSave);
      this.panel1.Controls.Add(this.btnLoad);
      this.panel1.Controls.Add(this.btnRemoveFiles);
      this.panel1.Controls.Add(this.btnAddFiles);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel1.Location = new System.Drawing.Point(891, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(125, 280);
      this.panel1.TabIndex = 21;
      // 
      // btnFindDatabase
      // 
      this.btnFindDatabase.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnFindDatabase.Location = new System.Drawing.Point(0, 138);
      this.btnFindDatabase.Name = "btnFindDatabase";
      this.btnFindDatabase.Size = new System.Drawing.Size(125, 23);
      this.btnFindDatabase.TabIndex = 25;
      this.btnFindDatabase.Text = "Find Database";
      this.btnFindDatabase.UseVisualStyleBackColor = true;
      // 
      // btnAutoRename
      // 
      this.btnAutoRename.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAutoRename.Location = new System.Drawing.Point(0, 115);
      this.btnAutoRename.Name = "btnAutoRename";
      this.btnAutoRename.Size = new System.Drawing.Size(125, 23);
      this.btnAutoRename.TabIndex = 24;
      this.btnAutoRename.Text = "Auto Rename";
      this.btnAutoRename.UseVisualStyleBackColor = true;
      // 
      // btnMgfFiles
      // 
      this.btnMgfFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnMgfFiles.Location = new System.Drawing.Point(0, 92);
      this.btnMgfFiles.Name = "btnMgfFiles";
      this.btnMgfFiles.Size = new System.Drawing.Size(125, 23);
      this.btnMgfFiles.TabIndex = 23;
      this.btnMgfFiles.Text = "Find Source";
      this.btnMgfFiles.UseVisualStyleBackColor = true;
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
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(375, 51);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(110, 13);
      this.label1.TabIndex = 66;
      this.label1.Text = "Select search engine:";
      // 
      // cbEngines
      // 
      this.cbEngines.FormattingEnabled = true;
      this.cbEngines.Location = new System.Drawing.Point(497, 47);
      this.cbEngines.Name = "cbEngines";
      this.cbEngines.Size = new System.Drawing.Size(171, 21);
      this.cbEngines.TabIndex = 67;
      // 
      // txtMinScore
      // 
      this.txtMinScore.Location = new System.Drawing.Point(123, 47);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(94, 20);
      this.txtMinScore.TabIndex = 69;
      // 
      // cbFilterByScore
      // 
      this.cbFilterByScore.AutoSize = true;
      this.cbFilterByScore.Location = new System.Drawing.Point(9, 49);
      this.cbFilterByScore.Name = "cbFilterByScore";
      this.cbFilterByScore.Size = new System.Drawing.Size(109, 17);
      this.cbFilterByScore.TabIndex = 68;
      this.cbFilterByScore.Text = "Filter by score >= ";
      this.cbFilterByScore.UseVisualStyleBackColor = true;
      // 
      // MzIdentDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "MzIdentDatasetPanel";
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    protected System.Windows.Forms.ListView lvDatFiles;
    protected System.Windows.Forms.ColumnHeader columnHeader1;
    protected System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.Panel panel1;
    protected System.Windows.Forms.Button btnFindDatabase;
    protected System.Windows.Forms.Button btnAutoRename;
    protected System.Windows.Forms.Button btnMgfFiles;
    protected System.Windows.Forms.Button btnSave;
    protected System.Windows.Forms.Button btnLoad;
    protected System.Windows.Forms.Button btnRemoveFiles;
    protected System.Windows.Forms.Button btnAddFiles;
    private System.Windows.Forms.ComboBox cbEngines;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.CheckBox cbFilterByScore;
  }
}
