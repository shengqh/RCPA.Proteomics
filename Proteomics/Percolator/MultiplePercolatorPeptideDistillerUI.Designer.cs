namespace RCPA.Proteomics.Percolator
{
  partial class MultiplePercolatorPeptideDistillerUI
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
      this.xmlFiles = new RCPA.Gui.MultipleFileField();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 529);
      this.lblProgress.Size = new System.Drawing.Size(1083, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 550);
      this.progressBar.Size = new System.Drawing.Size(1083, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(589, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(504, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(419, 7);
      // 
      // mgfFiles
      // 
      this.xmlFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.xmlFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.xmlFiles.FileArgument = null;
      this.xmlFiles.FileDescription = "Percolator Output Xml File";
      this.xmlFiles.FileNames = new string[0];
      this.xmlFiles.Key = "XmlFiles";
      this.xmlFiles.Location = new System.Drawing.Point(2, 3);
      this.xmlFiles.Name = "xmlFiles";
      this.xmlFiles.SelectedIndex = -1;
      this.xmlFiles.SelectedItem = null;
      this.xmlFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.xmlFiles.Size = new System.Drawing.Size(1081, 469);
      this.xmlFiles.TabIndex = 49;
      // 
      // MultiplePercolatorPeptideDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1083, 607);
      this.Controls.Add(this.xmlFiles);
      this.Name = "MultiplePercolatorPeptideDistillerUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.xmlFiles, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion

    private RCPA.Gui.MultipleFileField xmlFiles;
  }
}