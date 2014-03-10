namespace RCPA.Proteomics.Distiller
{
  partial class TargetPeptideDistillerUI
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
      this.txtDecoyPattern = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(43, 12);
      this.pnlFile.Size = new System.Drawing.Size(853, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(219, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(634, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(219, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 122);
      this.lblProgress.Size = new System.Drawing.Size(931, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 143);
      this.progressBar.Size = new System.Drawing.Size(931, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(513, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(428, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(343, 7);
      // 
      // txtDecoyPattern
      // 
      this.txtDecoyPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDecoyPattern.Location = new System.Drawing.Point(260, 58);
      this.txtDecoyPattern.Name = "txtDecoyPattern";
      this.txtDecoyPattern.Size = new System.Drawing.Size(636, 21);
      this.txtDecoyPattern.TabIndex = 10;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(135, 61);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(119, 12);
      this.label1.TabIndex = 11;
      this.label1.Text = "Input decoy pattern";
      // 
      // TargetPeptideDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(931, 200);
      this.Controls.Add(this.txtDecoyPattern);
      this.Controls.Add(this.label1);
      this.Name = "TargetPeptideDistillerUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtDecoyPattern, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtDecoyPattern;
    private System.Windows.Forms.Label label1;

  }
}
