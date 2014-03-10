namespace RCPA.Proteomics.Isotopic
{
  partial class GenerateIsotopicFileUI
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
      this.txtUrl = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 119);
      this.lblProgress.Size = new System.Drawing.Size(864, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 140);
      this.progressBar.Size = new System.Drawing.Size(864, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(480, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(395, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(310, 7);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(33, 34);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(101, 12);
      this.label1.TabIndex = 9;
      this.label1.Text = "Input NIST Link:";
      // 
      // txtUrl
      // 
      this.txtUrl.Location = new System.Drawing.Point(148, 31);
      this.txtUrl.Name = "txtUrl";
      this.txtUrl.Size = new System.Drawing.Size(681, 21);
      this.txtUrl.TabIndex = 10;
      // 
      // GenerateIsotopicFileUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(864, 197);
      this.Controls.Add(this.txtUrl);
      this.Controls.Add(this.label1);
      this.Name = "GenerateIsotopicFileUI";
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtUrl, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtUrl;

  }
}
