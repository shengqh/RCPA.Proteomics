namespace RCPA.Proteomics.Quantification
{
  partial class TurboCensusMs1BuilderUI
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
      this.tcBatchMode.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // tcBatchMode
      // 
      this.tcBatchMode.Size = new System.Drawing.Size(839, 92);
      // 
      // tabPage1
      // 
      this.tabPage1.Size = new System.Drawing.Size(831, 66);
      // 
      // tabPage2
      // 
      this.tabPage2.Size = new System.Drawing.Size(831, 66);
      // 
      // txtSingle
      // 
      this.txtSingle.Size = new System.Drawing.Size(612, 21);
      // 
      // lblSecondProgress
      // 
      this.lblSecondProgress.Location = new System.Drawing.Point(0, 194);
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(31, 152);
      this.pnlFile.Visible = false;
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 215);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 236);
      // 
      // btnTargetDir
      // 
      this.btnTargetDir.Location = new System.Drawing.Point(52, 123);
      this.btnTargetDir.Name = "btnTargetDir";
      this.btnTargetDir.Size = new System.Drawing.Size(172, 23);
      this.btnTargetDir.TabIndex = 26;
      this.btnTargetDir.Text = "button1";
      this.btnTargetDir.UseVisualStyleBackColor = true;
      // 
      // txtTargetDir
      // 
      this.txtTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTargetDir.Location = new System.Drawing.Point(243, 125);
      this.txtTargetDir.Name = "txtTargetDir";
      this.txtTargetDir.Size = new System.Drawing.Size(612, 21);
      this.txtTargetDir.TabIndex = 27;
      // 
      // TurboCensusMs1BuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(897, 293);
      this.Controls.Add(this.btnTargetDir);
      this.Controls.Add(this.txtTargetDir);
      this.Name = "TurboCensusMs1BuilderUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.txtTargetDir, 0);
      this.Controls.SetChildIndex(this.btnTargetDir, 0);
      this.Controls.SetChildIndex(this.tcBatchMode, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.lblSecondProgress, 0);
      this.tcBatchMode.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnTargetDir;
    private System.Windows.Forms.TextBox txtTargetDir;
  }
}
