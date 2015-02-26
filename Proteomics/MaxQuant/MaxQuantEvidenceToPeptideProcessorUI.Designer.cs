namespace RCPA.Proteomics.MaxQuant
{
  partial class MaxQuantEvidenceToPeptideProcessorUI
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
      this.evidenceFile = new RCPA.Gui.FileField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 69);
      this.lblProgress.Size = new System.Drawing.Size(1000, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 92);
      this.progressBar.Size = new System.Drawing.Size(1000, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 115);
      this.pnlButton.Size = new System.Drawing.Size(1000, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(548, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(463, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(378, 8);
      // 
      // evidenceFile
      // 
      this.evidenceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.evidenceFile.FullName = "";
      this.evidenceFile.Key = "EvidenceFile";
      this.evidenceFile.Location = new System.Drawing.Point(23, 28);
      this.evidenceFile.Name = "evidenceFile";
      this.evidenceFile.OpenButtonText = "Browse All File ...";
      this.evidenceFile.PreCondition = null;
      this.evidenceFile.Size = new System.Drawing.Size(947, 23);
      this.evidenceFile.TabIndex = 9;
      this.evidenceFile.WidthOpenButton = 226;
      // 
      // MaxQuantEvidenceToPeptideProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1000, 154);
      this.Controls.Add(this.evidenceFile);
      this.Name = "MaxQuantEvidenceToPeptideProcessorUI";
      this.TabText = "MaxQuantEvidenceToPeptideConverterUI";
      this.Text = "MaxQuantEvidenceToPeptideConverterUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.evidenceFile, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.FileField evidenceFile;
  }
}