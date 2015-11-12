namespace RCPA.Proteomics.Sequest
{
  partial class SequestDatasetPanel
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
      this.btnAddZips = new System.Windows.Forms.Button();
      this.btnAddSubDirectories = new System.Windows.Forms.Button();
      this.btnMsfFiles = new System.Windows.Forms.Button();
      this.pnlButton.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlButton
      // 
      this.pnlButton.Controls.Add(this.btnMsfFiles);
      this.pnlButton.Controls.Add(this.btnAddZips);
      this.pnlButton.Controls.Add(this.btnAddSubDirectories);
      this.pnlButton.Controls.SetChildIndex(this.btnAddFiles, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnRemoveFiles, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnLoad, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnSave, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnAddSubDirectories, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnAddZips, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnMsfFiles, 0);
      // 
      // colData
      // 
      this.colData.Text = "Data directories or files";
      // 
      // splitContainer2
      // 
      // 
      // btnAddZips
      // 
      this.btnAddZips.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddZips.Location = new System.Drawing.Point(0, 125);
      this.btnAddZips.Name = "btnAddZips";
      this.btnAddZips.Size = new System.Drawing.Size(125, 25);
      this.btnAddZips.TabIndex = 26;
      this.btnAddZips.Text = "Add Zip Files";
      this.btnAddZips.UseVisualStyleBackColor = true;
      this.btnAddZips.Click += new System.EventHandler(this.btnAddZips_Click);
      // 
      // btnAddSubDirectories
      // 
      this.btnAddSubDirectories.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddSubDirectories.Location = new System.Drawing.Point(0, 100);
      this.btnAddSubDirectories.Name = "btnAddSubDirectories";
      this.btnAddSubDirectories.Size = new System.Drawing.Size(125, 25);
      this.btnAddSubDirectories.TabIndex = 22;
      this.btnAddSubDirectories.Text = "Add Subdirs";
      this.btnAddSubDirectories.UseVisualStyleBackColor = true;
      this.btnAddSubDirectories.Click += new System.EventHandler(this.btnAddSubDirectories_Click);
      // 
      // btnMsfFiles
      // 
      this.btnMsfFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnMsfFiles.Location = new System.Drawing.Point(0, 150);
      this.btnMsfFiles.Name = "btnMsfFiles";
      this.btnMsfFiles.Size = new System.Drawing.Size(125, 25);
      this.btnMsfFiles.TabIndex = 27;
      this.btnMsfFiles.Text = "Add Msf Files";
      this.btnMsfFiles.UseVisualStyleBackColor = true;
      this.btnMsfFiles.Click += new System.EventHandler(this.btnMsfFiles_Click);
      // 
      // SequestDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "SequestDatasetPanel";
      this.pnlButton.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnAddZips;
    private System.Windows.Forms.Button btnAddSubDirectories;
    private System.Windows.Forms.Button btnMsfFiles;
  }
}
