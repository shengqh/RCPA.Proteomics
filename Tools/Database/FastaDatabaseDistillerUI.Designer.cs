namespace RCPA.Tools.Database
{
  partial class FastaDatabaseDistillerUI
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
      this.txtName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtPattern = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(53, 12);
      this.pnlFile.Size = new System.Drawing.Size(784, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(235, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(549, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(235, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 143);
      this.lblProgress.Size = new System.Drawing.Size(872, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 164);
      this.progressBar.Size = new System.Drawing.Size(872, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(484, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(399, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(314, 7);
      // 
      // txtName
      // 
      this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtName.Location = new System.Drawing.Point(287, 50);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(550, 21);
      this.txtName.TabIndex = 9;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(237, 53);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(29, 12);
      this.label1.TabIndex = 10;
      this.label1.Text = "Name";
      // 
      // txtPattern
      // 
      this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPattern.Location = new System.Drawing.Point(287, 77);
      this.txtPattern.Name = "txtPattern";
      this.txtPattern.Size = new System.Drawing.Size(550, 21);
      this.txtPattern.TabIndex = 11;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(219, 80);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(47, 12);
      this.label2.TabIndex = 12;
      this.label2.Text = "Pattern";
      // 
      // FastaDatabaseDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(872, 221);
      this.Controls.Add(this.txtPattern);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.label1);
      this.Name = "FastaDatabaseDistillerUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtName, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtPattern, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPattern;
    private System.Windows.Forms.Label label2;
  }
}
