namespace RCPA.Proteomics.Mascot
{
  partial class TurboMascotGenericFormatShift10ProcessorUI
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
      this.txtShift = new System.Windows.Forms.TextBox();
      this.tcBatchMode.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.pnlFile.SuspendLayout();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblSecondProgress
      // 
      this.lblSecondProgress.Location = new System.Drawing.Point(0, 214);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 237);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 260);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 283);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(496, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(411, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(326, 8);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(118, 170);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(159, 13);
      this.label1.TabIndex = 28;
      this.label1.Text = "Input append mass to precursor:";
      // 
      // txtShift
      // 
      this.txtShift.Location = new System.Drawing.Point(277, 167);
      this.txtShift.Name = "txtShift";
      this.txtShift.Size = new System.Drawing.Size(100, 20);
      this.txtShift.TabIndex = 29;
      // 
      // TurboMascotGenericFormatShift10ProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(897, 322);
      this.Controls.Add(this.txtShift);
      this.Controls.Add(this.label1);
      this.Name = "TurboMascotGenericFormatShift10ProcessorUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.tcBatchMode, 0);
      this.Controls.SetChildIndex(this.lblSecondProgress, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtShift, 0);
      this.tcBatchMode.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtShift;
  }
}
