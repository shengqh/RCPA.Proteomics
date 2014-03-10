namespace RCPA.Proteomics.Quantification.O18
{
  partial class O18QuantificationSummaryViewerUI
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
      this.zgcExperimentalScans.Size = new System.Drawing.Size(253, 485);
      // 
      // zgcPeptides
      // 
      this.zgcPeptides.Size = new System.Drawing.Size(253, 485);
      // 
      // zgcProteins
      // 
      this.zgcProteins.Size = new System.Drawing.Size(253, 485);
      // 
      // zgcProtein
      // 
      this.zgcProtein.Size = new System.Drawing.Size(253, 485);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(741, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(656, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(304, 8);
      // 
      // O18QuantificationSummaryViewerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1100, 650);
      this.Name = "O18QuantificationSummaryViewerUI";
      this.Load += new System.EventHandler(this.O18QuantificationSummaryViewerUI_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

  }
}
