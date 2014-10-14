namespace RCPA.Proteomics.Summary.Uniform
{
  partial class FilePanel
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
      this.chFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chDatabase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.pnlButton = new System.Windows.Forms.Panel();
      this.btnCategory = new System.Windows.Forms.Button();
      this.btnFindDatabase = new System.Windows.Forms.Button();
      this.btnAutoRename = new System.Windows.Forms.Button();
      this.btnSouceFiles = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.pnlTitle = new System.Windows.Forms.Panel();
      this.btnFind = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.pnlButton.SuspendLayout();
      this.pnlTitle.SuspendLayout();
      this.SuspendLayout();
      // 
      // lvDatFiles
      // 
      this.lvDatFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFile,
            this.chSource,
            this.chDatabase,
            this.chCategory});
      this.lvDatFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvDatFiles.FullRowSelect = true;
      this.lvDatFiles.HideSelection = false;
      this.lvDatFiles.Location = new System.Drawing.Point(0, 0);
      this.lvDatFiles.Name = "lvDatFiles";
      this.lvDatFiles.Size = new System.Drawing.Size(1014, 365);
      this.lvDatFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvDatFiles.TabIndex = 20;
      this.lvDatFiles.UseCompatibleStateImageBehavior = false;
      this.lvDatFiles.View = System.Windows.Forms.View.Details;
      // 
      // chFile
      // 
      this.chFile.Text = "Data file";
      this.chFile.Width = 403;
      // 
      // chSource
      // 
      this.chSource.Text = "Source file";
      this.chSource.Width = 299;
      // 
      // chCategory
      // 
      this.chCategory.Text = "Category";
      this.chCategory.Width = 100;
      // 
      // chDatabase
      // 
      this.chDatabase.Text = "Database";
      this.chDatabase.Width = 187;
      // 
      // pnlButton
      // 
      this.pnlButton.Controls.Add(this.btnCategory);
      this.pnlButton.Controls.Add(this.btnFindDatabase);
      this.pnlButton.Controls.Add(this.btnAutoRename);
      this.pnlButton.Controls.Add(this.btnSouceFiles);
      this.pnlButton.Controls.Add(this.btnSave);
      this.pnlButton.Controls.Add(this.btnLoad);
      this.pnlButton.Controls.Add(this.btnRemoveFiles);
      this.pnlButton.Controls.Add(this.btnAddFiles);
      this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
      this.pnlButton.Location = new System.Drawing.Point(1014, 0);
      this.pnlButton.Name = "pnlButton";
      this.pnlButton.Size = new System.Drawing.Size(125, 365);
      this.pnlButton.TabIndex = 21;
      // 
      // btnCategory
      // 
      this.btnCategory.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnCategory.Location = new System.Drawing.Point(0, 161);
      this.btnCategory.Name = "btnCategory";
      this.btnCategory.Size = new System.Drawing.Size(125, 23);
      this.btnCategory.TabIndex = 26;
      this.btnCategory.Text = "Categorize";
      this.btnCategory.UseVisualStyleBackColor = true;
      this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
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
      // btnSouceFiles
      // 
      this.btnSouceFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnSouceFiles.Location = new System.Drawing.Point(0, 92);
      this.btnSouceFiles.Name = "btnSouceFiles";
      this.btnSouceFiles.Size = new System.Drawing.Size(125, 23);
      this.btnSouceFiles.TabIndex = 23;
      this.btnSouceFiles.Text = "Find Source";
      this.btnSouceFiles.UseVisualStyleBackColor = true;
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
      // pnlTitle
      // 
      this.pnlTitle.Controls.Add(this.btnFind);
      this.pnlTitle.Controls.Add(this.label7);
      this.pnlTitle.Controls.Add(this.cbTitleFormat);
      this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlTitle.Location = new System.Drawing.Point(0, 365);
      this.pnlTitle.Name = "pnlTitle";
      this.pnlTitle.Size = new System.Drawing.Size(1139, 36);
      this.pnlTitle.TabIndex = 22;
      // 
      // btnFind
      // 
      this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFind.Location = new System.Drawing.Point(1011, 6);
      this.btnFind.Name = "btnFind";
      this.btnFind.Size = new System.Drawing.Size(125, 25);
      this.btnFind.TabIndex = 35;
      this.btnFind.Text = "Find title format";
      this.btnFind.UseVisualStyleBackColor = true;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(11, 12);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(65, 13);
      this.label7.TabIndex = 34;
      this.label7.Text = "Title format :";
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(82, 9);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(923, 21);
      this.cbTitleFormat.TabIndex = 33;
      // 
      // FilePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.lvDatFiles);
      this.Controls.Add(this.pnlButton);
      this.Controls.Add(this.pnlTitle);
      this.Name = "FilePanel";
      this.Size = new System.Drawing.Size(1139, 401);
      this.pnlButton.ResumeLayout(false);
      this.pnlTitle.ResumeLayout(false);
      this.pnlTitle.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    protected System.Windows.Forms.ListView lvDatFiles;
    protected System.Windows.Forms.ColumnHeader chFile;
    protected System.Windows.Forms.ColumnHeader chSource;
    private System.Windows.Forms.ColumnHeader chDatabase;
    private System.Windows.Forms.Panel pnlButton;
    protected System.Windows.Forms.Button btnFindDatabase;
    protected System.Windows.Forms.Button btnAutoRename;
    protected System.Windows.Forms.Button btnSouceFiles;
    protected System.Windows.Forms.Button btnSave;
    protected System.Windows.Forms.Button btnLoad;
    protected System.Windows.Forms.Button btnRemoveFiles;
    protected System.Windows.Forms.Button btnAddFiles;
    private System.Windows.Forms.ColumnHeader chCategory;
    protected System.Windows.Forms.Button btnCategory;
    private System.Windows.Forms.Panel pnlTitle;
    private System.Windows.Forms.Button btnFind;
    protected System.Windows.Forms.Label label7;
    protected System.Windows.Forms.ComboBox cbTitleFormat;
  }
}
