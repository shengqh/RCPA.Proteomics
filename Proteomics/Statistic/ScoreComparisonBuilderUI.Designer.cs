namespace RCPA.Proteomics.Statistic
{
  partial class ScoreComparisonBuilderUI
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
      this.txtFastaFile = new System.Windows.Forms.TextBox();
      this.btnFastaFile = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(31, 12);
      this.pnlFile.Size = new System.Drawing.Size(934, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(688, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 83);
      this.lblProgress.Size = new System.Drawing.Size(1004, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 104);
      this.progressBar.Size = new System.Drawing.Size(1004, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(550, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(465, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(380, 7);
      // 
      // txtFastaFile
      // 
      this.txtFastaFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtFastaFile.Location = new System.Drawing.Point(266, 48);
      this.txtFastaFile.Name = "txtFastaFile";
      this.txtFastaFile.Size = new System.Drawing.Size(699, 21);
      this.txtFastaFile.TabIndex = 12;
      // 
      // btnFastaFile
      // 
      this.btnFastaFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnFastaFile.Location = new System.Drawing.Point(31, 48);
      this.btnFastaFile.Name = "btnFastaFile";
      this.btnFastaFile.Size = new System.Drawing.Size(229, 23);
      this.btnFastaFile.TabIndex = 32;
      this.btnFastaFile.Text = "button1";
      this.btnFastaFile.UseVisualStyleBackColor = true;
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // ScoreComparisonBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1004, 161);
      this.Controls.Add(this.btnFastaFile);
      this.Controls.Add(this.txtFastaFile);
      this.Name = "ScoreComparisonBuilderUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtFastaFile, 0);
      this.Controls.SetChildIndex(this.btnFastaFile, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtFastaFile;
    private System.Windows.Forms.Button btnFastaFile;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}
