namespace RCPA.Proteomics.MaxQuant
{
  partial class MaxQuantSiteToPeptideProcessorUI
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
      this.label2 = new System.Windows.Forms.Label();
      this.txtMinProbability = new System.Windows.Forms.TextBox();
      this.txtMinScoreDiff = new System.Windows.Forms.TextBox();
      this.siteFile = new RCPA.Gui.FileField();
      this.msmsFile = new RCPA.Gui.FileField();
      this.cbSILAC = new RCPA.Gui.RcpaCheckField();
      this.txtSILACAminoacids = new System.Windows.Forms.TextBox();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 193);
      this.lblProgress.Size = new System.Drawing.Size(954, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 216);
      this.progressBar.Size = new System.Drawing.Size(954, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 239);
      this.pnlButton.Size = new System.Drawing.Size(954, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(525, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(440, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(355, 8);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(87, 54);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(156, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Minimum localization probability:";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(137, 83);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(106, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Minimum delta score:";
      // 
      // txtMinProbability
      // 
      this.txtMinProbability.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinProbability.Location = new System.Drawing.Point(247, 51);
      this.txtMinProbability.Name = "txtMinProbability";
      this.txtMinProbability.Size = new System.Drawing.Size(671, 20);
      this.txtMinProbability.TabIndex = 8;
      // 
      // txtMinScoreDiff
      // 
      this.txtMinScoreDiff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinScoreDiff.Location = new System.Drawing.Point(248, 80);
      this.txtMinScoreDiff.Name = "txtMinScoreDiff";
      this.txtMinScoreDiff.Size = new System.Drawing.Size(670, 20);
      this.txtMinScoreDiff.TabIndex = 8;
      // 
      // siteFile
      // 
      this.siteFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.siteFile.FullName = "";
      this.siteFile.Key = "SiteFile";
      this.siteFile.Location = new System.Drawing.Point(21, 21);
      this.siteFile.Name = "siteFile";
      this.siteFile.OpenButtonText = "Browse All File ...";
      this.siteFile.PreCondition = null;
      this.siteFile.Size = new System.Drawing.Size(898, 23);
      this.siteFile.TabIndex = 9;
      this.siteFile.WidthOpenButton = 226;
      // 
      // msmsFile
      // 
      this.msmsFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.msmsFile.FullName = "";
      this.msmsFile.Key = "MSMSFile";
      this.msmsFile.Location = new System.Drawing.Point(21, 142);
      this.msmsFile.Name = "msmsFile";
      this.msmsFile.OpenButtonText = "Browse All File ...";
      this.msmsFile.PreCondition = null;
      this.msmsFile.Size = new System.Drawing.Size(898, 23);
      this.msmsFile.TabIndex = 10;
      this.msmsFile.WidthOpenButton = 226;
      // 
      // cbSILAC
      // 
      this.cbSILAC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbSILAC.AutoSize = true;
      this.cbSILAC.Key = "cbSILAC";
      this.cbSILAC.Location = new System.Drawing.Point(27, 112);
      this.cbSILAC.Name = "cbSILAC";
      this.cbSILAC.PreCondition = null;
      this.cbSILAC.Size = new System.Drawing.Size(219, 17);
      this.cbSILAC.TabIndex = 11;
      this.cbSILAC.Text = "Is SILAC data? Input SILAC amino acids:";
      this.cbSILAC.UseVisualStyleBackColor = true;
      // 
      // txtSILACAminoacids
      // 
      this.txtSILACAminoacids.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSILACAminoacids.Location = new System.Drawing.Point(248, 110);
      this.txtSILACAminoacids.Name = "txtSILACAminoacids";
      this.txtSILACAminoacids.Size = new System.Drawing.Size(671, 20);
      this.txtSILACAminoacids.TabIndex = 12;
      // 
      // MaxQuantSiteToPeptideProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(954, 278);
      this.Controls.Add(this.txtSILACAminoacids);
      this.Controls.Add(this.cbSILAC);
      this.Controls.Add(this.msmsFile);
      this.Controls.Add(this.siteFile);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtMinProbability);
      this.Controls.Add(this.txtMinScoreDiff);
      this.Controls.Add(this.label2);
      this.Name = "MaxQuantSiteToPeptideProcessorUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtMinScoreDiff, 0);
      this.Controls.SetChildIndex(this.txtMinProbability, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.siteFile, 0);
      this.Controls.SetChildIndex(this.msmsFile, 0);
      this.Controls.SetChildIndex(this.cbSILAC, 0);
      this.Controls.SetChildIndex(this.txtSILACAminoacids, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMinProbability;
    private System.Windows.Forms.TextBox txtMinScoreDiff;
    private Gui.FileField siteFile;
    private Gui.FileField msmsFile;
    private Gui.RcpaCheckField cbSILAC;
    private System.Windows.Forms.TextBox txtSILACAminoacids;
  }
}
