namespace RCPA.Tools.Distiller
{
  partial class ValidatedPeptideDistillerUI
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
      this.btnImage = new System.Windows.Forms.Button();
      this.txtImageDirectory = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(245, 21);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(282, 26);
      this.txtOriginalFile.Size = new System.Drawing.Size(576, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 113);
      this.progressBar.Size = new System.Drawing.Size(823, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 90);
      this.lblProgress.Size = new System.Drawing.Size(826, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(324, 164);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(494, 164);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(409, 164);
      // 
      // btnImage
      // 
      this.btnImage.Location = new System.Drawing.Point(31, 51);
      this.btnImage.Name = "btnImage";
      this.btnImage.Size = new System.Drawing.Size(245, 23);
      this.btnImage.TabIndex = 7;
      this.btnImage.Text = "button1";
      this.btnImage.UseVisualStyleBackColor = true;
      // 
      // txtImageDirectory
      // 
      this.txtImageDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtImageDirectory.Location = new System.Drawing.Point(282, 53);
      this.txtImageDirectory.Name = "txtImageDirectory";
      this.txtImageDirectory.Size = new System.Drawing.Size(576, 21);
      this.txtImageDirectory.TabIndex = 8;
      // 
      // ValidatedPeptideDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(893, 205);
      this.Controls.Add(this.txtImageDirectory);
      this.Controls.Add(this.btnImage);
      this.Name = "ValidatedPeptideDistillerUI";
      this.Controls.SetChildIndex(this.btnImage, 0);
      this.Controls.SetChildIndex(this.txtImageDirectory, 0);
      
      
      
      
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnImage;
    private System.Windows.Forms.TextBox txtImageDirectory;
  }
}
