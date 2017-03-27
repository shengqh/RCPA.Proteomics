namespace RCPA.Proteomics.MSFlagger
{
  partial class MSFlaggerDatasetPanel
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
      this.pnlDataButton.SuspendLayout();
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
      // btnAutoRename
      // 
      this.btnAutoRename.Text = "Rename Xml";
      // 
      // pnlDataButton
      // 
      this.pnlDataButton.Size = new System.Drawing.Size(125, 244);
      // 
      // groupBox2
      // 
      this.groupBox2.Size = new System.Drawing.Size(1016, 263);
      this.groupBox2.Text = "Select xml files you want to extract peptides (all files will be used)";
      // 
      // lvDatFiles
      // 
      this.lvDatFiles.Size = new System.Drawing.Size(885, 244);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Xml file";
      // 
      // splitContainer3
      // 
      this.splitContainer3.SplitterDistance = 263;
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Location = new System.Drawing.Point(106, 2);
      // 
      // splitContainer2
      // 
      // 
      // MSFlaggerDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Name = "MSFlaggerDatasetPanel";
      this.pnlDataButton.ResumeLayout(false);
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
