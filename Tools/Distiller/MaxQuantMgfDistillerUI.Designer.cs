namespace RCPA.Tools.Distiller
{
  partial class MaxQuantMgfDistillerUI
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.btnTitle = new System.Windows.Forms.Button();
      this.lvMgfFiles = new System.Windows.Forms.ListView();
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.btnRemoveFiles = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.cbFilterByScore = new System.Windows.Forms.CheckBox();
      this.cbSingleFile = new System.Windows.Forms.CheckBox();
      this.btnMgfFile = new System.Windows.Forms.Button();
      this.txtSingleFile = new System.Windows.Forms.TextBox();
      this.pnlFile.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(39, 12);
      this.pnlFile.Size = new System.Drawing.Size(1111, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(267, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(844, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(267, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 542);
      this.lblProgress.Size = new System.Drawing.Size(1185, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 563);
      this.progressBar.Size = new System.Drawing.Size(1185, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(640, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(555, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(470, 7);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.cbTitleFormat);
      this.groupBox1.Controls.Add(this.btnTitle);
      this.groupBox1.Controls.Add(this.lvMgfFiles);
      this.groupBox1.Controls.Add(this.btnSave);
      this.groupBox1.Controls.Add(this.btnLoad);
      this.groupBox1.Controls.Add(this.btnAddFiles);
      this.groupBox1.Controls.Add(this.btnRemoveFiles);
      this.groupBox1.Location = new System.Drawing.Point(35, 94);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(1115, 408);
      this.groupBox1.TabIndex = 23;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select msm/MGF files you want to extract peptides (only selected file will be use" +
    "d)";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(18, 438);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(77, 12);
      this.label1.TabIndex = 20;
      this.label1.Text = "Title format";
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(101, 382);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(889, 20);
      this.cbTitleFormat.TabIndex = 19;
      // 
      // btnTitle
      // 
      this.btnTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnTitle.Location = new System.Drawing.Point(997, 135);
      this.btnTitle.Name = "btnTitle";
      this.btnTitle.Size = new System.Drawing.Size(96, 21);
      this.btnTitle.TabIndex = 16;
      this.btnTitle.Text = "Get title";
      this.btnTitle.UseVisualStyleBackColor = true;
      this.btnTitle.Click += new System.EventHandler(this.btnTitle_Click);
      // 
      // lvMgfFiles
      // 
      this.lvMgfFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lvMgfFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
      this.lvMgfFiles.FullRowSelect = true;
      this.lvMgfFiles.HideSelection = false;
      this.lvMgfFiles.Location = new System.Drawing.Point(20, 26);
      this.lvMgfFiles.Name = "lvMgfFiles";
      this.lvMgfFiles.Size = new System.Drawing.Size(970, 350);
      this.lvMgfFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvMgfFiles.TabIndex = 15;
      this.lvMgfFiles.UseCompatibleStateImageBehavior = false;
      this.lvMgfFiles.View = System.Windows.Forms.View.Details;
      this.lvMgfFiles.SizeChanged += new System.EventHandler(this.lvDatFiles_SizeChanged);
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "MGF file";
      this.columnHeader2.Width = 566;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Title";
      this.columnHeader1.Width = 359;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(997, 108);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(96, 21);
      this.btnSave.TabIndex = 14;
      this.btnSave.Text = "button2";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(997, 81);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(96, 21);
      this.btnLoad.TabIndex = 13;
      this.btnLoad.Text = "button1";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnAddFiles
      // 
      this.btnAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddFiles.Location = new System.Drawing.Point(997, 28);
      this.btnAddFiles.Name = "btnAddFiles";
      this.btnAddFiles.Size = new System.Drawing.Size(96, 21);
      this.btnAddFiles.TabIndex = 11;
      this.btnAddFiles.Text = "button1";
      this.btnAddFiles.UseVisualStyleBackColor = true;
      // 
      // btnRemoveFiles
      // 
      this.btnRemoveFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRemoveFiles.Location = new System.Drawing.Point(997, 54);
      this.btnRemoveFiles.Name = "btnRemoveFiles";
      this.btnRemoveFiles.Size = new System.Drawing.Size(96, 21);
      this.btnRemoveFiles.TabIndex = 12;
      this.btnRemoveFiles.Text = "button2";
      this.btnRemoveFiles.UseVisualStyleBackColor = true;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(21, 226);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(89, 12);
      this.label6.TabIndex = 18;
      this.label6.Text = "Title format :";
      // 
      // txtMaxEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(185, 165);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 21);
      this.txtMaxEvalue.TabIndex = 47;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(14, 167);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(174, 16);
      this.cbFilterByEvalue.TabIndex = 46;
      this.cbFilterByEvalue.Text = "Filter by Expect value = ";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // txtMinScore
      // 
      this.txtMinScore.Location = new System.Drawing.Point(142, 141);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(94, 21);
      this.txtMinScore.TabIndex = 45;
      // 
      // cbFilterByScore
      // 
      this.cbFilterByScore.AutoSize = true;
      this.cbFilterByScore.Location = new System.Drawing.Point(14, 143);
      this.cbFilterByScore.Name = "cbFilterByScore";
      this.cbFilterByScore.Size = new System.Drawing.Size(132, 16);
      this.cbFilterByScore.TabIndex = 44;
      this.cbFilterByScore.Text = "Filter by Score = ";
      this.cbFilterByScore.UseVisualStyleBackColor = true;
      // 
      // cbSingleFile
      // 
      this.cbSingleFile.AutoSize = true;
      this.cbSingleFile.Location = new System.Drawing.Point(39, 48);
      this.cbSingleFile.Name = "cbSingleFile";
      this.cbSingleFile.Size = new System.Drawing.Size(156, 16);
      this.cbSingleFile.TabIndex = 24;
      this.cbSingleFile.Text = "Extract to single file";
      this.cbSingleFile.UseVisualStyleBackColor = true;
      // 
      // btnMgfFile
      // 
      this.btnMgfFile.Location = new System.Drawing.Point(201, 44);
      this.btnMgfFile.Name = "btnMgfFile";
      this.btnMgfFile.Size = new System.Drawing.Size(102, 23);
      this.btnMgfFile.TabIndex = 25;
      this.btnMgfFile.Text = "Browse";
      this.btnMgfFile.UseVisualStyleBackColor = true;
      // 
      // txtSingleFile
      // 
      this.txtSingleFile.Location = new System.Drawing.Point(309, 46);
      this.txtSingleFile.Name = "txtSingleFile";
      this.txtSingleFile.Size = new System.Drawing.Size(841, 21);
      this.txtSingleFile.TabIndex = 26;
      // 
      // MaxQuantMgfDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1185, 620);
      this.Controls.Add(this.txtSingleFile);
      this.Controls.Add(this.btnMgfFile);
      this.Controls.Add(this.cbSingleFile);
      this.Controls.Add(this.groupBox1);
      this.Name = "MaxQuantMgfDistillerUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.cbSingleFile, 0);
      this.Controls.SetChildIndex(this.btnMgfFile, 0);
      this.Controls.SetChildIndex(this.txtSingleFile, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnAddFiles;
    private System.Windows.Forms.Button btnRemoveFiles;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.ListView lvMgfFiles;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.Button btnTitle;
    private System.Windows.Forms.ComboBox cbTitleFormat;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.CheckBox cbFilterByScore;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox cbSingleFile;
    private System.Windows.Forms.Button btnMgfFile;
    private System.Windows.Forms.TextBox txtSingleFile;
  }
}
