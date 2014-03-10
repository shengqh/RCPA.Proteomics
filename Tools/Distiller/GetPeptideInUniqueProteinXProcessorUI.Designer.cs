namespace RCPA.Tools.Distiller
{
  partial class ProteinXPeptideDistillerUI
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
      this.txtUniquePeptide = new System.Windows.Forms.TextBox();
      this.cbUniquePeptideOnly = new System.Windows.Forms.CheckBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(42, 12);
      this.pnlFile.Size = new System.Drawing.Size(869, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(194, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(675, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(194, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 154);
      this.lblProgress.Size = new System.Drawing.Size(923, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 133);
      this.progressBar.Size = new System.Drawing.Size(923, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(509, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(424, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(339, 7);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(103, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(125, 12);
      this.label1.TabIndex = 7;
      this.label1.Text = "Unique Peptide Count";
      // 
      // txtUniquePeptide
      // 
      this.txtUniquePeptide.Location = new System.Drawing.Point(234, 58);
      this.txtUniquePeptide.Name = "txtUniquePeptide";
      this.txtUniquePeptide.Size = new System.Drawing.Size(72, 21);
      this.txtUniquePeptide.TabIndex = 8;
      // 
      // cbUniquePeptideOnly
      // 
      this.cbUniquePeptideOnly.AutoSize = true;
      this.cbUniquePeptideOnly.Location = new System.Drawing.Point(312, 60);
      this.cbUniquePeptideOnly.Name = "cbUniquePeptideOnly";
      this.cbUniquePeptideOnly.Size = new System.Drawing.Size(48, 16);
      this.cbUniquePeptideOnly.TabIndex = 9;
      this.cbUniquePeptideOnly.Text = "Only";
      this.cbUniquePeptideOnly.UseVisualStyleBackColor = true;
      // 
      // ProteinXPeptideDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(923, 211);
      this.Controls.Add(this.txtUniquePeptide);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbUniquePeptideOnly);
      this.Name = "ProteinXPeptideDistillerUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.cbUniquePeptideOnly, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtUniquePeptide, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtUniquePeptide;
    private System.Windows.Forms.CheckBox cbUniquePeptideOnly;
  }
}
