﻿namespace RCPA.Proteomics.Quantification
{
  partial class ForwardReverseRatioCalibratorUI
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
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlFile.Location = new System.Drawing.Point(0, 0);
      this.pnlFile.Size = new System.Drawing.Size(956, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(302, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(654, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(302, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 35);
      this.lblProgress.Size = new System.Drawing.Size(956, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 56);
      this.progressBar.Size = new System.Drawing.Size(956, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(526, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(441, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(356, 7);
      // 
      // ForwardReverseRatioCalibratorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(956, 113);
      this.Name = "ForwardReverseRatioCalibratorUI";
      this.TabText = "MaxQuantPeptideRatioDistillerUI";
      this.Text = "MaxQuantPeptideRatioDistillerUI";
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

  }
}