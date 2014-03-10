namespace RCPA.Tools.Glycan
{
  partial class NGlycanPeptideMassCalculatorUI
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

    private System.Windows.Forms.TextBox txtPeptideSequence;
    private System.Windows.Forms.TextBox txtPeptideModification;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtGlycanStructure;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtResultInfo;
    private System.Windows.Forms.Label label1;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.txtPeptideSequence = new System.Windows.Forms.TextBox();
      this.txtPeptideModification = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtGlycanStructure = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtResultInfo = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(241, 163);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(410, 163);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(326, 163);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(35, 28);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(95, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Peptide Sequence";
      // 
      // txtPeptideSequence
      // 
      this.txtPeptideSequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPeptideSequence.Location = new System.Drawing.Point(150, 25);
      this.txtPeptideSequence.Name = "txtPeptideSequence";
      this.txtPeptideSequence.Size = new System.Drawing.Size(545, 20);
      this.txtPeptideSequence.TabIndex = 8;
      // 
      // txtPeptideModification
      // 
      this.txtPeptideModification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPeptideModification.Location = new System.Drawing.Point(150, 56);
      this.txtPeptideModification.Name = "txtPeptideModification";
      this.txtPeptideModification.Size = new System.Drawing.Size(545, 20);
      this.txtPeptideModification.TabIndex = 9;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(44, 91);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(86, 13);
      this.label2.TabIndex = 10;
      this.label2.Text = "Glycan Structure";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(27, 56);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(103, 13);
      this.label3.TabIndex = 11;
      this.label3.Text = "Peptide Modification";
      // 
      // txtGlycanStructure
      // 
      this.txtGlycanStructure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtGlycanStructure.Location = new System.Drawing.Point(150, 88);
      this.txtGlycanStructure.Name = "txtGlycanStructure";
      this.txtGlycanStructure.Size = new System.Drawing.Size(545, 20);
      this.txtGlycanStructure.TabIndex = 12;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(93, 125);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(37, 13);
      this.label4.TabIndex = 13;
      this.label4.Text = "Result";
      // 
      // txtResultInfo
      // 
      this.txtResultInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtResultInfo.Location = new System.Drawing.Point(150, 122);
      this.txtResultInfo.Name = "txtResultInfo";
      this.txtResultInfo.Size = new System.Drawing.Size(545, 20);
      this.txtResultInfo.TabIndex = 14;
      // 
      // NGlycanPeptideMassCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(727, 206);
      this.Controls.Add(this.txtResultInfo);
      this.Controls.Add(this.txtPeptideSequence);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtGlycanStructure);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtPeptideModification);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label4);
      this.Name = "NGlycanPeptideMassCalculatorUI";
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtPeptideModification, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.txtGlycanStructure, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtPeptideSequence, 0);
      
      
      
      this.Controls.SetChildIndex(this.txtResultInfo, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
  }
}
