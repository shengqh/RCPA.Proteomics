namespace RCPA.Tools.Modification
{
  partial class AbstractMultipleLabellingSeparatorUI
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
      this.txtModifiedAminoacids = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Location = new System.Drawing.Point(35, 26);
      this.btnOriginalFile.Size = new System.Drawing.Size(232, 21);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(273, 26);
      this.txtOriginalFile.Size = new System.Drawing.Size(556, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 149);
      this.progressBar.Size = new System.Drawing.Size(794, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(33, 125);
      this.lblProgress.Size = new System.Drawing.Size(796, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(310, 200);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(480, 200);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(395, 200);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(33, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(485, 12);
      this.label1.TabIndex = 7;
      this.label1.Text = "Modified Amino Acids, Looks Like (K# R~ : K4R6) (K@ R^ : K10R8) (DEFAULT : K0R0)";
      // 
      // txtModifiedAminoacids
      // 
      this.txtModifiedAminoacids.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtModifiedAminoacids.Location = new System.Drawing.Point(35, 78);
      this.txtModifiedAminoacids.Name = "txtModifiedAminoacids";
      this.txtModifiedAminoacids.Size = new System.Drawing.Size(794, 21);
      this.txtModifiedAminoacids.TabIndex = 8;
      // 
      // IdentifiedResultMultipleLabellingSeparatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(864, 241);
      this.Controls.Add(this.txtModifiedAminoacids);
      this.Controls.Add(this.label1);
      this.Name = "IdentifiedResultMultipleLabellingSeparatorUI";
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtModifiedAminoacids, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      
      
      
      
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtModifiedAminoacids;
  }
}
