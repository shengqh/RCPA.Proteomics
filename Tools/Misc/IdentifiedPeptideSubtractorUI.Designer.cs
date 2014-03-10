namespace RCPA.Tools.Misc
{
  partial class IdentifiedPeptideSubtractorUI
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
      this.btnSource = new System.Windows.Forms.Button();
      this.txtSource = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Location = new System.Drawing.Point(35, 58);
      this.btnOriginalFile.Size = new System.Drawing.Size(232, 21);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(273, 58);
      this.txtOriginalFile.Size = new System.Drawing.Size(556, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 121);
      this.progressBar.Size = new System.Drawing.Size(794, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(33, 97);
      this.lblProgress.Size = new System.Drawing.Size(796, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(310, 172);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(480, 172);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(395, 172);
      // 
      // btnSource
      // 
      this.btnSource.Location = new System.Drawing.Point(35, 29);
      this.btnSource.Name = "btnSource";
      this.btnSource.Size = new System.Drawing.Size(232, 23);
      this.btnSource.TabIndex = 7;
      this.btnSource.Text = "button1";
      this.btnSource.UseVisualStyleBackColor = true;
      // 
      // txtSource
      // 
      this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSource.Location = new System.Drawing.Point(273, 31);
      this.txtSource.Name = "txtSource";
      this.txtSource.Size = new System.Drawing.Size(556, 21);
      this.txtSource.TabIndex = 8;
      // 
      // IdentifiedPeptideSubtractorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(864, 213);
      this.Controls.Add(this.txtSource);
      this.Controls.Add(this.btnSource);
      this.Name = "IdentifiedPeptideSubtractorUI";
      
      
      this.Controls.SetChildIndex(this.btnSource, 0);
      this.Controls.SetChildIndex(this.txtSource, 0);
      
      
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnSource;
    private System.Windows.Forms.TextBox txtSource;

  }
}
