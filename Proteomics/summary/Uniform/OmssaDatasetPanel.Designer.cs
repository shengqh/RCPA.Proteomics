namespace RCPA.Proteomics.Summary.Uniform
{
  partial class OmssaDatasetPanel
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
      this.xmlFiles = new RCPA.Gui.MultipleFileField();
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
      // groupBox2
      // 
      this.groupBox2.Size = new System.Drawing.Size(1016, 194);
      // 
      // lvDatFiles
      // 
      this.lvDatFiles.Size = new System.Drawing.Size(885, 175);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Width = 582;
      // 
      // splitContainer3
      // 
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.xmlFiles);
      this.splitContainer3.Size = new System.Drawing.Size(1016, 262);
      this.splitContainer3.SplitterDistance = 194;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.Location = new System.Drawing.Point(8, 4);
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.Location = new System.Drawing.Point(106, 1);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Size = new System.Drawing.Size(1016, 361);
      // 
      // xmlFiles
      // 
      this.xmlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.xmlFiles.FileArgument = null;
      this.xmlFiles.FileDescription = "PeptidePhophet Xml Files";
      this.xmlFiles.FileNames = new string[0];
      this.xmlFiles.Key = "File";
      this.xmlFiles.Location = new System.Drawing.Point(0, 0);
      this.xmlFiles.Name = "xmlFiles";
      this.xmlFiles.SelectedIndex = -1;
      this.xmlFiles.SelectedItem = null;
      this.xmlFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.xmlFiles.Size = new System.Drawing.Size(1016, 194);
      this.xmlFiles.TabIndex = 0;
      // 
      // OmssaDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Name = "OmssaDatasetPanel";
      this.Size = new System.Drawing.Size(1016, 399);
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

    private RCPA.Gui.MultipleFileField xmlFiles;

  }
}
