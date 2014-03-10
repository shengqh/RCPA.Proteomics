namespace RCPA.Proteomics.Quantification.ITraq
{
  partial class ITraqUniquePeptideStatisticBuilderUI
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
      this.cbSiteLevel = new RCPA.Gui.RcpaCheckField();
      this.fastaFile = new RCPA.Gui.FileField();
      this.cbAccessNumberParser = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlClassification
      // 
      this.pnlClassification.Size = new System.Drawing.Size(1033, 277);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 564);
      this.lblProgress.Size = new System.Drawing.Size(1104, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 585);
      this.progressBar.Size = new System.Drawing.Size(1104, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(600, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(515, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(430, 7);
      // 
      // cbSiteLevel
      // 
      this.cbSiteLevel.AutoSize = true;
      this.cbSiteLevel.Key = "SiteLevel";
      this.cbSiteLevel.Location = new System.Drawing.Point(167, 142);
      this.cbSiteLevel.Name = "cbSiteLevel";
      this.cbSiteLevel.PreCondition = null;
      this.cbSiteLevel.Size = new System.Drawing.Size(84, 16);
      this.cbSiteLevel.TabIndex = 19;
      this.cbSiteLevel.Text = "Site level";
      this.cbSiteLevel.UseVisualStyleBackColor = true;
      // 
      // fastaFile
      // 
      this.fastaFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.fastaFile.FullName = "";
      this.fastaFile.Key = "FastaFile";
      this.fastaFile.Location = new System.Drawing.Point(25, 496);
      this.fastaFile.Name = "fastaFile";
      this.fastaFile.OpenButtonText = "Browse All File ...";
      this.fastaFile.WidthOpenButton = 226;
      this.fastaFile.PreCondition = null;
      this.fastaFile.Size = new System.Drawing.Size(1031, 22);
      this.fastaFile.TabIndex = 20;
      // 
      // cbAccessNumberParser
      // 
      this.cbAccessNumberParser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbAccessNumberParser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbAccessNumberParser.FormattingEnabled = true;
      this.cbAccessNumberParser.Location = new System.Drawing.Point(251, 525);
      this.cbAccessNumberParser.Name = "cbAccessNumberParser";
      this.cbAccessNumberParser.Size = new System.Drawing.Size(805, 20);
      this.cbAccessNumberParser.TabIndex = 22;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(120, 528);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(125, 12);
      this.label2.TabIndex = 21;
      this.label2.Text = "Access number format";
      // 
      // ITraqUniquePeptideStatisticBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1104, 642);
      this.Controls.Add(this.cbAccessNumberParser);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cbSiteLevel);
      this.Controls.Add(this.fastaFile);
      this.Name = "ITraqUniquePeptideStatisticBuilderUI";
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.cbNormalize, 0);
      this.Controls.SetChildIndex(this.cbRatioCalculator, 0);
      this.Controls.SetChildIndex(this.itraqIons, 0);
      this.Controls.SetChildIndex(this.txtRLocation, 0);
      this.Controls.SetChildIndex(this.btnRLocation, 0);
      this.Controls.SetChildIndex(this.fastaFile, 0);
      this.Controls.SetChildIndex(this.txtValidProbability, 0);
      this.Controls.SetChildIndex(this.cbFilterPeptide, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.cbSiteLevel, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.cbAccessNumberParser, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Gui.RcpaCheckField cbSiteLevel;
    private Gui.FileField fastaFile;
    private System.Windows.Forms.ComboBox cbAccessNumberParser;
    private System.Windows.Forms.Label label2;
  }
}