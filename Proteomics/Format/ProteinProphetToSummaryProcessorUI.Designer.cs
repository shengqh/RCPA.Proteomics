namespace RCPA.Proteomics.Format
{
  partial class ProteinProphetToSummaryProcessorUI
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
      this.txtMinProbability = new System.Windows.Forms.TextBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(51, 12);
      this.pnlFile.Size = new System.Drawing.Size(827, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(581, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 101);
      this.lblProgress.Size = new System.Drawing.Size(905, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 122);
      this.progressBar.Size = new System.Drawing.Size(905, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(500, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(415, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(330, 7);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(195, 60);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(95, 12);
      this.label1.TabIndex = 9;
      this.label1.Text = "min probability";
      // 
      // txtMinProbability
      // 
      this.txtMinProbability.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinProbability.Location = new System.Drawing.Point(296, 57);
      this.txtMinProbability.Name = "txtMinProbability";
      this.txtMinProbability.Size = new System.Drawing.Size(582, 21);
      this.txtMinProbability.TabIndex = 10;
      // 
      // ProteinProphetToSummaryProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(905, 179);
      this.Controls.Add(this.txtMinProbability);
      this.Controls.Add(this.label1);
      this.Name = "ProteinProphetToSummaryProcessorUI";
      this.TabText = "MascotGenericFormatSplitterUI";
      this.Text = "MascotGenericFormatSplitterUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtMinProbability, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtMinProbability;


  }
}