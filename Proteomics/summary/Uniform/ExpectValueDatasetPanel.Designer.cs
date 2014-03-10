namespace RCPA.Proteomics.Summary.Uniform
{
  partial class ExpectValueDatasetPanel
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
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.cbFilterByScore = new System.Windows.Forms.CheckBox();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.lvDatFiles = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnMgfFiles = new System.Windows.Forms.Button();
      this.btnRenameDat = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnFindDB = new System.Windows.Forms.Button();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer3
      // 
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
      // 
      // splitContainer1
      // 
      // 
      // splitContainer2
      // 
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtMaxEvalue);
      this.groupBox1.Controls.Add(this.txtMinScore);
      this.groupBox1.Controls.Add(this.cbFilterByEvalue);
      this.groupBox1.Controls.Add(this.cbFilterByScore);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByScore, 0);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByEvalue, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtMinScore, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtMaxEvalue, 0);
      // 
      // txtMaxEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(179, 68);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 21);
      this.txtMaxEvalue.TabIndex = 51;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(8, 70);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(174, 16);
      this.cbFilterByEvalue.TabIndex = 50;
      this.cbFilterByEvalue.Text = "Filter by Expect value = ";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // txtMinScore
      // 
      this.txtMinScore.Location = new System.Drawing.Point(137, 44);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(94, 21);
      this.txtMinScore.TabIndex = 49;
      // 
      // cbFilterByScore
      // 
      this.cbFilterByScore.AutoSize = true;
      this.cbFilterByScore.Location = new System.Drawing.Point(9, 46);
      this.cbFilterByScore.Name = "cbFilterByScore";
      this.cbFilterByScore.Size = new System.Drawing.Size(132, 16);
      this.cbFilterByScore.TabIndex = 48;
      this.cbFilterByScore.Text = "Filter by Score = ";
      this.cbFilterByScore.UseVisualStyleBackColor = true;
      // 
      // btnRemoveFiles
      // 
      this.btnRemoveFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRemoveFiles.Location = new System.Drawing.Point(914, 46);
      this.btnRemoveFiles.Name = "btnRemoveFiles";
      this.btnRemoveFiles.Size = new System.Drawing.Size(96, 21);
      this.btnRemoveFiles.TabIndex = 12;
      this.btnRemoveFiles.Text = "button2";
      this.btnRemoveFiles.UseVisualStyleBackColor = true;
      // 
      // btnAddFiles
      // 
      this.btnAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddFiles.Location = new System.Drawing.Point(914, 20);
      this.btnAddFiles.Name = "btnAddFiles";
      this.btnAddFiles.Size = new System.Drawing.Size(96, 21);
      this.btnAddFiles.TabIndex = 11;
      this.btnAddFiles.Text = "button1";
      this.btnAddFiles.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(914, 73);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(96, 21);
      this.btnLoad.TabIndex = 13;
      this.btnLoad.Text = "button1";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(914, 100);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(96, 21);
      this.btnSave.TabIndex = 14;
      this.btnSave.Text = "button2";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // lvDatFiles
      // 
      this.lvDatFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lvDatFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.lvDatFiles.FullRowSelect = true;
      this.lvDatFiles.HideSelection = false;
      this.lvDatFiles.Location = new System.Drawing.Point(6, 20);
      this.lvDatFiles.Name = "lvDatFiles";
      this.lvDatFiles.Size = new System.Drawing.Size(902, 208);
      this.lvDatFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvDatFiles.TabIndex = 15;
      this.lvDatFiles.UseCompatibleStateImageBehavior = false;
      this.lvDatFiles.View = System.Windows.Forms.View.Details;
      this.lvDatFiles.SizeChanged += new System.EventHandler(this.lvDatFiles_SizeChanged);
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
      // btnMgfFiles
      // 
      this.btnMgfFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnMgfFiles.Location = new System.Drawing.Point(914, 127);
      this.btnMgfFiles.Name = "btnMgfFiles";
      this.btnMgfFiles.Size = new System.Drawing.Size(96, 21);
      this.btnMgfFiles.TabIndex = 16;
      this.btnMgfFiles.Text = "Find MGF";
      this.btnMgfFiles.UseVisualStyleBackColor = true;
      // 
      // btnRenameDat
      // 
      this.btnRenameDat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRenameDat.Location = new System.Drawing.Point(914, 154);
      this.btnRenameDat.Name = "btnRenameDat";
      this.btnRenameDat.Size = new System.Drawing.Size(96, 21);
      this.btnRenameDat.TabIndex = 17;
      this.btnRenameDat.Text = "Rename Dat";
      this.btnRenameDat.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.btnFindDB);
      this.groupBox2.Controls.Add(this.btnRenameDat);
      this.groupBox2.Controls.Add(this.btnMgfFiles);
      this.groupBox2.Controls.Add(this.lvDatFiles);
      this.groupBox2.Controls.Add(this.btnSave);
      this.groupBox2.Controls.Add(this.btnLoad);
      this.groupBox2.Controls.Add(this.btnAddFiles);
      this.groupBox2.Controls.Add(this.btnRemoveFiles);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(1016, 230);
      this.groupBox2.TabIndex = 56;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Select dat files you want to extract peptides (all files will be used)";
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Database";
      this.columnHeader3.Width = 187;
      // 
      // btnFindDB
      // 
      this.btnFindDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFindDB.Location = new System.Drawing.Point(912, 181);
      this.btnFindDB.Name = "btnFindDB";
      this.btnFindDB.Size = new System.Drawing.Size(96, 21);
      this.btnFindDB.TabIndex = 18;
      this.btnFindDB.Text = "Find DB";
      this.btnFindDB.UseVisualStyleBackColor = true;
      // 
      // ExpectValueDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "ExpectValueDatasetPanel";
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      this.splitContainer3.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.CheckBox cbFilterByScore;
    protected System.Windows.Forms.GroupBox groupBox2;
    protected System.Windows.Forms.Button btnRenameDat;
    protected System.Windows.Forms.Button btnMgfFiles;
    protected System.Windows.Forms.ListView lvDatFiles;
    protected System.Windows.Forms.ColumnHeader columnHeader1;
    protected System.Windows.Forms.ColumnHeader columnHeader2;
    protected System.Windows.Forms.Button btnSave;
    protected System.Windows.Forms.Button btnLoad;
    protected System.Windows.Forms.Button btnAddFiles;
    protected System.Windows.Forms.Button btnRemoveFiles;
    protected System.Windows.Forms.Button btnFindDB;
    private System.Windows.Forms.ColumnHeader columnHeader3;
  }
}
