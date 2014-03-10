namespace RCPA.Proteomics.Raw
{
  partial class RawScanParentChildDistillerUI
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
      this.rawFile = new RCPA.Gui.FileField();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 46);
      this.lblProgress.Size = new System.Drawing.Size(1037, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 69);
      this.progressBar.Size = new System.Drawing.Size(1037, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(566, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(481, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(396, 8);
      // 
      // rawFile
      // 
      this.rawFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.rawFile.FullName = "";
      this.rawFile.Key = "RawFile";
      this.rawFile.Location = new System.Drawing.Point(3, 12);
      this.rawFile.Name = "rawFile";
      this.rawFile.OpenButtonText = "Browse All File ...";
      this.rawFile.WidthOpenButton = 226;
      this.rawFile.PreCondition = null;
      this.rawFile.Size = new System.Drawing.Size(1022, 23);
      this.rawFile.TabIndex = 9;
      // 
      // RawScanParentChildDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1037, 131);
      this.Controls.Add(this.rawFile);
      this.Name = "RawScanParentChildDistillerUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.rawFile, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.FileField rawFile;

  }
}
