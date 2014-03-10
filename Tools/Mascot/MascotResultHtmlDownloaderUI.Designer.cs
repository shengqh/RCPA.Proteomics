namespace RCPA.Tools.Mascot
{
  partial class MascotResultHtmlDownloaderUI
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
      this.cbPeptideFormat = new System.Windows.Forms.CheckBox();
      this.txtThreshold = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(197, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 186);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 162);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(241, 208);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(411, 208);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(326, 208);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(115, 62);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(113, 12);
      this.label1.TabIndex = 7;
      this.label1.Text = "Protein Format URL";
      // 
      // txtUrl
      // 
      this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtUrl.Location = new System.Drawing.Point(235, 59);
      this.txtUrl.Name = "txtUrl";
      this.txtUrl.Size = new System.Drawing.Size(457, 21);
      this.txtUrl.TabIndex = 8;
      // 
      // cbPeptideFormat
      // 
      this.cbPeptideFormat.AutoSize = true;
      this.cbPeptideFormat.Location = new System.Drawing.Point(213, 91);
      this.cbPeptideFormat.Name = "cbPeptideFormat";
      this.cbPeptideFormat.Size = new System.Drawing.Size(15, 14);
      this.cbPeptideFormat.TabIndex = 9;
      this.cbPeptideFormat.UseVisualStyleBackColor = true;
      this.cbPeptideFormat.CheckedChanged += new System.EventHandler(this.cbPeptideFormat_CheckedChanged);
      // 
      // txtThreshold
      // 
      this.txtThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtThreshold.Location = new System.Drawing.Point(507, 90);
      this.txtThreshold.Name = "txtThreshold";
      this.txtThreshold.Size = new System.Drawing.Size(185, 21);
      this.txtThreshold.TabIndex = 10;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(232, 92);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(269, 12);
      this.label2.TabIndex = 11;
      this.label2.Text = "Download peptide format, threshold (0~0.1) =";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(430, 124);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(71, 12);
      this.label3.TabIndex = 12;
      this.label3.Text = "Min score =";
      // 
      // txtMinScore
      // 
      this.txtMinScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinScore.Location = new System.Drawing.Point(507, 124);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(185, 21);
      this.txtMinScore.TabIndex = 13;
      // 
      // MascotResultHtmlDownloaderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(727, 266);
      this.Controls.Add(this.txtMinScore);
      this.Controls.Add(this.txtUrl);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtThreshold);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cbPeptideFormat);
      this.Controls.Add(this.label3);
      this.Name = "MascotResultHtmlDownloaderUI";
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.cbPeptideFormat, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtThreshold, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtUrl, 0);
      
      
      
      
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtMinScore, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtUrl;
    private System.Windows.Forms.CheckBox cbPeptideFormat;
    private System.Windows.Forms.TextBox txtThreshold;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtMinScore;
  }
}
