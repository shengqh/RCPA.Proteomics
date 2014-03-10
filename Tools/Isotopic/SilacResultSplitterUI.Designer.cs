namespace RCPA.Tools.Isotopic
{
  partial class SilacResultSplitterUI
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
      this.btnLight = new System.Windows.Forms.Button();
      this.txtLightFile = new System.Windows.Forms.TextBox();
      this.btnHeavy = new System.Windows.Forms.Button();
      this.txtHeavyFile = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(262, 23);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(308, 28);
      this.txtOriginalFile.Size = new System.Drawing.Size(384, 20);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 151);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 126);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(241, 207);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(410, 207);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(326, 207);
      // 
      // btnLight
      // 
      this.btnLight.Location = new System.Drawing.Point(31, 55);
      this.btnLight.Name = "btnLight";
      this.btnLight.Size = new System.Drawing.Size(262, 23);
      this.btnLight.TabIndex = 7;
      this.btnLight.Text = "button1";
      this.btnLight.UseVisualStyleBackColor = true;
      // 
      // txtLightFile
      // 
      this.txtLightFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLightFile.Location = new System.Drawing.Point(308, 57);
      this.txtLightFile.Name = "txtLightFile";
      this.txtLightFile.Size = new System.Drawing.Size(384, 20);
      this.txtLightFile.TabIndex = 8;
      // 
      // btnHeavy
      // 
      this.btnHeavy.Location = new System.Drawing.Point(31, 87);
      this.btnHeavy.Name = "btnHeavy";
      this.btnHeavy.Size = new System.Drawing.Size(262, 23);
      this.btnHeavy.TabIndex = 9;
      this.btnHeavy.Text = "button2";
      this.btnHeavy.UseVisualStyleBackColor = true;
      // 
      // txtHeavyFile
      // 
      this.txtHeavyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHeavyFile.Location = new System.Drawing.Point(308, 89);
      this.txtHeavyFile.Name = "txtHeavyFile";
      this.txtHeavyFile.Size = new System.Drawing.Size(384, 20);
      this.txtHeavyFile.TabIndex = 10;
      // 
      // SilacResultSplitterUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(727, 250);
      this.Controls.Add(this.btnHeavy);
      this.Controls.Add(this.txtHeavyFile);
      this.Controls.Add(this.btnLight);
      this.Controls.Add(this.txtLightFile);
      this.Name = "SilacResultSplitterUI";
      this.Controls.SetChildIndex(this.txtLightFile, 0);
      this.Controls.SetChildIndex(this.btnLight, 0);
      
      
      
      
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtHeavyFile, 0);
      this.Controls.SetChildIndex(this.btnHeavy, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnLight;
    private System.Windows.Forms.TextBox txtLightFile;
    private System.Windows.Forms.Button btnHeavy;
    private System.Windows.Forms.TextBox txtHeavyFile;
  }
}
