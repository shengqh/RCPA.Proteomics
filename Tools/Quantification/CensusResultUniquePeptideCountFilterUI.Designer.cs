namespace RCPA.Tools.Sequest
{
  partial class CensusResultUniquePeptideCountFilterUI
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
      this.SuspendLayout();
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 119);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(33, 95);
      this.lblProgress.Size = new System.Drawing.Size(659, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(241, 170);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(411, 170);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(326, 170);
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
      // CensusResultUniquePeptideCountFilterUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(727, 211);
      this.Controls.Add(this.txtUniquePeptide);
      this.Controls.Add(this.label1);
      this.Name = "CensusResultUniquePeptideCountFilterUI";
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtUniquePeptide, 0);
      
      
      
      
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtUniquePeptide;
  }
}
