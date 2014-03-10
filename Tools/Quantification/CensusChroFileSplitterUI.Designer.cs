namespace RCPA.Tools.Quantification
{
  partial class CensusChroFileSplitterUI
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
      this.txtResultCount = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Location = new System.Drawing.Point(35, 24);
      this.btnOriginalFile.Size = new System.Drawing.Size(177, 21);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(633, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 123);
      this.progressBar.Size = new System.Drawing.Size(833, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 100);
      this.lblProgress.Size = new System.Drawing.Size(836, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(329, 173);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(499, 173);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(414, 173);
      // 
      // txtResultCount
      // 
      this.txtResultCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtResultCount.Location = new System.Drawing.Point(235, 59);
      this.txtResultCount.Name = "txtResultCount";
      this.txtResultCount.Size = new System.Drawing.Size(633, 21);
      this.txtResultCount.TabIndex = 12;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(105, 62);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 12);
      this.label2.TabIndex = 11;
      this.label2.Text = "Result File Count";
      // 
      // CensusChroFileSplitterUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(903, 214);
      this.Controls.Add(this.txtResultCount);
      this.Controls.Add(this.label2);
      this.Name = "CensusChroFileSplitterUI";
      
      
      
      
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtResultCount, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtResultCount;
    private System.Windows.Forms.Label label2;
  }
}
