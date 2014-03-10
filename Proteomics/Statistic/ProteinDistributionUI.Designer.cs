namespace RCPA.Proteomics.Statistic
{
  partial class ProteinDistributionUI
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
      this.cbPeptideCountOnly = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // cbFilterType
      // 
      this.cbFilterType.Size = new System.Drawing.Size(374, 20);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 574);
      this.lblProgress.Size = new System.Drawing.Size(1035, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 595);
      this.progressBar.Size = new System.Drawing.Size(1035, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(565, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(480, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(395, 7);
      // 
      // cbPeptideCountOnly
      // 
      this.cbPeptideCountOnly.AutoSize = true;
      this.cbPeptideCountOnly.Location = new System.Drawing.Point(637, 90);
      this.cbPeptideCountOnly.Name = "cbPeptideCountOnly";
      this.cbPeptideCountOnly.Size = new System.Drawing.Size(174, 16);
      this.cbPeptideCountOnly.TabIndex = 22;
      this.cbPeptideCountOnly.Text = "Export peptide count only";
      this.cbPeptideCountOnly.UseVisualStyleBackColor = true;
      // 
      // ProteinDistributionUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1035, 652);
      this.Controls.Add(this.cbPeptideCountOnly);
      this.Name = "ProteinDistributionUI";
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.txtProteinFile, 0);
      this.Controls.SetChildIndex(this.txtModifiedAminoacids, 0);
      this.Controls.SetChildIndex(this.btnProteinFile, 0);
      this.Controls.SetChildIndex(this.txtClassificationTitle, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.cbFilterType, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.cbPeptideCountOnly, 0);
      this.Controls.SetChildIndex(this.txtLoopFrom, 0);
      this.Controls.SetChildIndex(this.txtLoopTo, 0);
      this.Controls.SetChildIndex(this.txtLoopStep, 0);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.label6, 0);
      this.Controls.SetChildIndex(this.cbModifiedOnly, 0);
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox cbPeptideCountOnly;
  }
}
