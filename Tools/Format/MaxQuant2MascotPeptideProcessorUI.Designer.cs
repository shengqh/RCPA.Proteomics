namespace RCPA.Tools.Format
{
  partial class MaxQuantSiteToPeptideProcessorUI
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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtMinProbability = new System.Windows.Forms.TextBox();
      this.txtMinScoreDiff = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(294, 21);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(331, 26);
      this.txtOriginalFile.Size = new System.Drawing.Size(557, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 164);
      this.progressBar.Size = new System.Drawing.Size(853, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 141);
      this.lblProgress.Size = new System.Drawing.Size(856, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(339, 214);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(509, 214);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(424, 214);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(194, 58);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(131, 12);
      this.label1.TabIndex = 7;
      this.label1.Text = "Min Localization Prob";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(236, 85);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 12);
      this.label2.TabIndex = 7;
      this.label2.Text = "Min Score Diff";
      // 
      // txtMinProbability
      // 
      this.txtMinProbability.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinProbability.Location = new System.Drawing.Point(331, 54);
      this.txtMinProbability.Name = "txtMinProbability";
      this.txtMinProbability.Size = new System.Drawing.Size(557, 21);
      this.txtMinProbability.TabIndex = 8;
      // 
      // txtMinScoreDiff
      // 
      this.txtMinScoreDiff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinScoreDiff.Location = new System.Drawing.Point(331, 82);
      this.txtMinScoreDiff.Name = "txtMinScoreDiff";
      this.txtMinScoreDiff.Size = new System.Drawing.Size(557, 21);
      this.txtMinScoreDiff.TabIndex = 8;
      // 
      // MaxQuant2MascotPeptideProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(923, 255);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtMinProbability);
      this.Controls.Add(this.txtMinScoreDiff);
      this.Controls.Add(this.label2);
      this.Name = "MaxQuant2MascotPeptideProcessorUI";
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtMinScoreDiff, 0);
      this.Controls.SetChildIndex(this.txtMinProbability, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      
      
      
      
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMinProbability;
    private System.Windows.Forms.TextBox txtMinScoreDiff;
  }
}
