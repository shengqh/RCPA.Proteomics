namespace RCPA.Proteomics.MaxQuant
{
  partial class MaxQuantPeptideRatioDistillerUI
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
      this.txtCount = new System.Windows.Forms.TextBox();
      this.lbFuncItems = new System.Windows.Forms.CheckedListBox();
      this.label2 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(39, 25);
      this.pnlFile.Size = new System.Drawing.Size(890, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(302, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(588, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(302, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 300);
      this.lblProgress.Size = new System.Drawing.Size(956, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 321);
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
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(118, 56);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(215, 12);
      this.label1.TabIndex = 9;
      this.label1.Text = "Minimum quantified experiment count";
      // 
      // txtCount
      // 
      this.txtCount.Location = new System.Drawing.Point(339, 53);
      this.txtCount.Name = "txtCount";
      this.txtCount.Size = new System.Drawing.Size(100, 21);
      this.txtCount.TabIndex = 10;
      // 
      // lbFuncItems
      // 
      this.lbFuncItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbFuncItems.FormattingEnabled = true;
      this.lbFuncItems.Location = new System.Drawing.Point(339, 80);
      this.lbFuncItems.Name = "lbFuncItems";
      this.lbFuncItems.Size = new System.Drawing.Size(590, 180);
      this.lbFuncItems.TabIndex = 11;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(208, 80);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(125, 12);
      this.label2.TabIndex = 12;
      this.label2.Text = "Select output entris";
      // 
      // MaxQuantPeptideRatioDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(956, 378);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lbFuncItems);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtCount);
      this.Name = "MaxQuantPeptideRatioDistillerUI";
      this.TabText = "MaxQuantPeptideRatioDistillerUI";
      this.Text = "MaxQuantPeptideRatioDistillerUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.txtCount, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.lbFuncItems, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtCount;
    private System.Windows.Forms.CheckedListBox lbFuncItems;
    private System.Windows.Forms.Label label2;
  }
}