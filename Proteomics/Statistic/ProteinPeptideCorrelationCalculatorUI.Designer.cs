namespace RCPA.Proteomics.Statistic
{
  partial class ProteinPeptideCorrelationCalculatorUI
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
      this.btnLoad = new System.Windows.Forms.Button();
      this.pnlClassification = new RCPA.Proteomics.ClassificationPanel();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(38, 31);
      this.pnlFile.Size = new System.Drawing.Size(879, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(633, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 575);
      this.lblProgress.Size = new System.Drawing.Size(1040, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 596);
      this.progressBar.Size = new System.Drawing.Size(1040, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(568, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(483, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(398, 7);
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(924, 31);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 23);
      this.btnLoad.TabIndex = 9;
      this.btnLoad.Text = "&Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // pnlClassification
      // 
      this.pnlClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlClassification.Description = "Description";
      this.pnlClassification.GetName = null;
      this.pnlClassification.Location = new System.Drawing.Point(38, 60);
      this.pnlClassification.Name = "pnlClassification";
      this.pnlClassification.Pattern = "(.+)_(\\d){1,2}";
      this.pnlClassification.Size = new System.Drawing.Size(965, 475);
      this.pnlClassification.TabIndex = 21;
      // 
      // ProteinPeptideCorrelationCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1040, 653);
      this.Controls.Add(this.pnlClassification);
      this.Controls.Add(this.btnLoad);
      this.Name = "ProteinPeptideCorrelationCalculatorUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    protected System.Windows.Forms.Button btnLoad;
    protected RCPA.Proteomics.ClassificationPanel pnlClassification;
  }
}
