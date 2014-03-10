namespace RCPA.Proteomics.Quantification.SILAC
{
  partial class ExtendSilacQuantificationSummaryViewerUI
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
      this.SuspendLayout();
      // 
      // zgcExperimentalScans
      // 
      this.zgcExperimentalScans.Size = new System.Drawing.Size(259, 387);
      // 
      // zgcPeptides
      // 
      this.zgcPeptides.Size = new System.Drawing.Size(253, 0);
      // 
      // zgcProtein
      // 
      this.zgcProtein.Size = new System.Drawing.Size(247, 229);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(741, 6);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(656, 6);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(316, 6);
      // 
      // ExtendSilacQuantificationSummaryViewerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1100, 600);
      this.Name = "ExtendSilacQuantificationSummaryViewerUI";
      this.Load += new System.EventHandler(this.SilacQuantificationSummaryViewerUI_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
  }
}
