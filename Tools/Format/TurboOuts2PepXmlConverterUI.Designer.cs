namespace RCPA.Tools.Format
{
  partial class TurboOuts2PepXmlConverterUI
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
      this.btnOutsFile = new System.Windows.Forms.Button();
      this.txtOutsFile = new System.Windows.Forms.TextBox();
      this.tcBatchMode.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabPage1
      // 
      this.tabPage1.Location = new System.Drawing.Point(4, 21);
      this.tabPage1.Size = new System.Drawing.Size(823, 67);
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 21);
      this.tabPage2.Size = new System.Drawing.Size(823, 67);
      // 
      // lblSecondProgress
      // 
      this.lblSecondProgress.Location = new System.Drawing.Point(32, 473);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Location = new System.Drawing.Point(31, 254);
      this.btnOriginalFile.Size = new System.Drawing.Size(211, 21);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(248, 254);
      this.txtOriginalFile.Size = new System.Drawing.Size(614, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 193);
      this.progressBar.Size = new System.Drawing.Size(827, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 170);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(326, 234);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(496, 234);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(411, 234);
      // 
      // btnOutsFile
      // 
      this.btnOutsFile.Location = new System.Drawing.Point(52, 137);
      this.btnOutsFile.Name = "btnOutsFile";
      this.btnOutsFile.Size = new System.Drawing.Size(172, 21);
      this.btnOutsFile.TabIndex = 2;
      this.btnOutsFile.Text = "button1";
      this.btnOutsFile.UseVisualStyleBackColor = true;
      // 
      // txtOutsFile
      // 
      this.txtOutsFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtOutsFile.Location = new System.Drawing.Point(243, 137);
      this.txtOutsFile.Name = "txtOutsFile";
      this.txtOutsFile.Size = new System.Drawing.Size(619, 21);
      this.txtOutsFile.TabIndex = 3;
      // 
      // Outs2PepXmlProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(897, 275);
      this.Controls.Add(this.txtOutsFile);
      this.Controls.Add(this.btnOutsFile);
      this.Name = "Outs2PepXmlProcessorUI";
      this.Text = "Convert Sequest Outs File To PepXml";
      this.Controls.SetChildIndex(this.tcBatchMode, 0);
      this.Controls.SetChildIndex(this.lblSecondProgress, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      
      
      
      
      
      this.Controls.SetChildIndex(this.btnOutsFile, 0);
      this.Controls.SetChildIndex(this.txtOutsFile, 0);
      this.tcBatchMode.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOutsFile;
    private System.Windows.Forms.TextBox txtOutsFile;
  }
}