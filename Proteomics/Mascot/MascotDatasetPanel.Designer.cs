﻿namespace RCPA.Proteomics.Mascot
{
  partial class MascotDatasetPanel
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
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // columnHeader1
      // 
      this.columnHeader1.Width = 582;
      // 
      // btnFindDatabase
      // 
      this.btnFindDatabase.Click += new System.EventHandler(this.btnFindDB_Click);
      // 
      // btnAutoRename
      // 
      this.btnAutoRename.Click += new System.EventHandler(this.btnRenameDat_Click);
      // 
      // btnMgfFiles
      // 
      this.btnMgfFiles.Click += new System.EventHandler(this.btnMgfFiles_Click);
      // 
      // splitContainer3
      // 
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(8, 3);
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Location = new System.Drawing.Point(79, 2);
      // 
      // splitContainer2
      // 
      // 
      // MascotDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Name = "MascotDatasetPanel";
      this.groupBox2.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

  }
}

