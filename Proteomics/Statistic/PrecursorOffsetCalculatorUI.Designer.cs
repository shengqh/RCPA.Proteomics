namespace RCPA.Proteomics.Statistic
{
  partial class PrecursorOffsetCalculatorUI
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
      this.toolTip1 = new System.Windows.Forms.ToolTip();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlFile.Location = new System.Drawing.Point(0, 0);
      this.pnlFile.Size = new System.Drawing.Size(1215, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(239, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(976, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(239, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 36);
      this.lblProgress.Size = new System.Drawing.Size(1215, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 57);
      this.progressBar.Size = new System.Drawing.Size(1215, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(655, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(570, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(485, 7);
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // PrecursorOffsetCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1215, 114);
      this.Name = "PrecursorOffsetCalculatorUI";
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolTip toolTip1;
  }
}
