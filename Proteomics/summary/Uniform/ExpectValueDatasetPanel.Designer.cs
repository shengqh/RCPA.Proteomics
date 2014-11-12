﻿namespace RCPA.Proteomics.Summary.Uniform
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
      this.lvDatFiles = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnFindDatabase = new System.Windows.Forms.Button();
      this.btnAutoRename = new System.Windows.Forms.Button();
      this.btnMgfFiles = new System.Windows.Forms.Button();
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
      this.panel1.SuspendLayout();
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
      this.txtMaxEvalue.Location = new System.Drawing.Point(180, 73);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 20);
      this.txtMaxEvalue.TabIndex = 51;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(9, 75);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(139, 17);
      this.cbFilterByEvalue.TabIndex = 50;
      this.cbFilterByEvalue.Text = "Filter by Expect value = ";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
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
      this.groupBox2.Controls.Add(this.panel1);
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
      this.panel1.Controls.Add(this.btnFindDatabase);
      this.panel1.Controls.Add(this.btnAutoRename);
      this.panel1.Controls.Add(this.btnMgfFiles);
      this.panel1.Controls.Add(this.btnSave);
      this.panel1.Controls.Add(this.btnLoad);
      this.panel1.Controls.Add(this.btnRemoveFiles);
      this.panel1.Controls.Add(this.btnAddFiles);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel1.Location = new System.Drawing.Point(888, 16);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(125, 241);
      this.panel1.TabIndex = 19;
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
      // ExpectValueDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "ExpectValueDatasetPanel";
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
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.CheckBox cbFilterByScore;
    protected System.Windows.Forms.GroupBox groupBox2;
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
  }
}
