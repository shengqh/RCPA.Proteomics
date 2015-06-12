namespace RCPA.Tools.Summary
{
  partial class SummaryBuilderFromPeptidesUI
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
      this.peptideFile = new RCPA.Gui.FileField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 71);
      this.lblProgress.Size = new System.Drawing.Size(931, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 94);
      this.progressBar.Size = new System.Drawing.Size(931, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 117);
      this.pnlButton.Size = new System.Drawing.Size(931, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(513, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(428, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(343, 8);
      // 
      // peptideFile
      // 
      this.peptideFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.peptideFile.FullName = "";
      this.peptideFile.Key = "PeptideFile";
      this.peptideFile.Location = new System.Drawing.Point(12, 25);
      this.peptideFile.Name = "peptideFile";
      this.peptideFile.OpenButtonText = "Browse All File ...";
      this.peptideFile.PreCondition = null;
      this.peptideFile.Size = new System.Drawing.Size(907, 23);
      this.peptideFile.TabIndex = 9;
      this.peptideFile.WidthOpenButton = 226;
      // 
      // SummaryBuilderFromPeptidesUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(931, 156);
      this.Controls.Add(this.peptideFile);
      this.Name = "SummaryBuilderFromPeptidesUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.peptideFile, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.FileField peptideFile;

  }
}
