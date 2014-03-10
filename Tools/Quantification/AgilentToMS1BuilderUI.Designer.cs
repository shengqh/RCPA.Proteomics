namespace RCPA.Tools.Quantification
{
  partial class AgilentToMS1BuilderUI
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
      this.btnTargetDir = new System.Windows.Forms.Button();
      this.txtTargetDir = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(250, 23);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(287, 25);
      this.txtOriginalFile.Size = new System.Drawing.Size(694, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 112);
      this.progressBar.Size = new System.Drawing.Size(946, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 89);
      this.lblProgress.Size = new System.Drawing.Size(949, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(386, 140);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(556, 140);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(471, 140);
      // 
      // btnTargetDir
      // 
      this.btnTargetDir.Location = new System.Drawing.Point(31, 51);
      this.btnTargetDir.Name = "btnTargetDir";
      this.btnTargetDir.Size = new System.Drawing.Size(250, 23);
      this.btnTargetDir.TabIndex = 7;
      this.btnTargetDir.Text = "button1";
      this.btnTargetDir.UseVisualStyleBackColor = true;
      // 
      // txtTargetDir
      // 
      this.txtTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTargetDir.Location = new System.Drawing.Point(287, 53);
      this.txtTargetDir.Name = "txtTargetDir";
      this.txtTargetDir.Size = new System.Drawing.Size(694, 21);
      this.txtTargetDir.TabIndex = 8;
      // 
      // AgilentToMS1BuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1016, 181);
      this.Controls.Add(this.btnTargetDir);
      this.Controls.Add(this.txtTargetDir);
      this.Name = "AgilentToMS1BuilderUI";
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.txtTargetDir, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.btnTargetDir, 0);
      
      
      
      
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnTargetDir;
    private System.Windows.Forms.TextBox txtTargetDir;
  }
}
