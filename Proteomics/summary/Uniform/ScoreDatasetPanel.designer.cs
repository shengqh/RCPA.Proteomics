namespace RCPA.Proteomics.Summary.Uniform
{
  partial class ScoreDatasetPanel
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
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.cbFilterByScore = new System.Windows.Forms.CheckBox();
      this.lvDatFiles = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.pnlDataButton = new System.Windows.Forms.Panel();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.pnlDataButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer3
      // 
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(8, 6);
      // 
      // splitContainer2
      // 
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtMinScore);
      this.groupBox1.Controls.Add(this.cbFilterByScore);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByScore, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtMinScore, 0);
      // 
      // txtMinScore
      // 
      this.txtMinScore.Location = new System.Drawing.Point(137, 48);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(94, 20);
      this.txtMinScore.TabIndex = 49;
      // 
      // cbFilterByScore
      // 
      this.cbFilterByScore.AutoSize = true;
      this.cbFilterByScore.Location = new System.Drawing.Point(9, 50);
      this.cbFilterByScore.Name = "cbFilterByScore";
      this.cbFilterByScore.Size = new System.Drawing.Size(105, 17);
      this.cbFilterByScore.TabIndex = 48;
      this.cbFilterByScore.Text = "Filter by Score = ";
      this.cbFilterByScore.UseVisualStyleBackColor = true;
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
      this.lvDatFiles.Size = new System.Drawing.Size(885, 241);
      this.lvDatFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvDatFiles.TabIndex = 15;
      this.lvDatFiles.UseCompatibleStateImageBehavior = false;
      this.lvDatFiles.View = System.Windows.Forms.View.Details;
      this.lvDatFiles.SizeChanged += new System.EventHandler(this.lvDatFiles_SizeChanged);
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
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.lvDatFiles);
      this.groupBox2.Controls.Add(this.pnlDataButton);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(1016, 260);
      this.groupBox2.TabIndex = 56;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Select data files you want to extract peptides";
      // 
      // panel1
      // 
      this.pnlDataButton.Controls.Add(this.btnSave);
      this.pnlDataButton.Controls.Add(this.btnLoad);
      this.pnlDataButton.Controls.Add(this.btnRemoveFiles);
      this.pnlDataButton.Controls.Add(this.btnAddFiles);
      this.pnlDataButton.Dock = System.Windows.Forms.DockStyle.Right;
      this.pnlDataButton.Location = new System.Drawing.Point(888, 16);
      this.pnlDataButton.Name = "panel1";
      this.pnlDataButton.Size = new System.Drawing.Size(125, 241);
      this.pnlDataButton.TabIndex = 19;
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
      // ScoreDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "ScoreDatasetPanel";
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
      this.groupBox2.ResumeLayout(false);
      this.pnlDataButton.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.CheckBox cbFilterByScore;
    protected System.Windows.Forms.Panel pnlDataButton;
    protected System.Windows.Forms.GroupBox groupBox2;
    protected System.Windows.Forms.ListView lvDatFiles;
    protected System.Windows.Forms.ColumnHeader columnHeader1;
    protected System.Windows.Forms.ColumnHeader columnHeader2;
    protected System.Windows.Forms.ColumnHeader columnHeader3;
    protected System.Windows.Forms.Button btnSave;
    protected System.Windows.Forms.Button btnLoad;
    protected System.Windows.Forms.Button btnRemoveFiles;
    protected System.Windows.Forms.Button btnAddFiles;
  }
}
