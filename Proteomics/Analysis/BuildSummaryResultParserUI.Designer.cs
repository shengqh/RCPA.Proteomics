namespace RCPA.Proteomics.Analysis
{
  partial class BuildSummaryResultParserUI
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
      this.txtDecoyPattern = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cbFdrType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(0, 84);
      this.pnlFile.Size = new System.Drawing.Size(892, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(272, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(620, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(272, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 106);
      this.lblProgress.Size = new System.Drawing.Size(892, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 127);
      this.progressBar.Size = new System.Drawing.Size(892, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(505, 6);
      this.btnClose.Size = new System.Drawing.Size(97, 23);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(398, 6);
      this.btnCancel.Size = new System.Drawing.Size(97, 23);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(291, 6);
      this.btnGo.Size = new System.Drawing.Size(97, 23);
      // 
      // txtDecoyPattern
      // 
      this.txtDecoyPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDecoyPattern.Location = new System.Drawing.Point(183, 18);
      this.txtDecoyPattern.Name = "txtDecoyPattern";
      this.txtDecoyPattern.Size = new System.Drawing.Size(697, 21);
      this.txtDecoyPattern.TabIndex = 37;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(28, 21);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(149, 12);
      this.label3.TabIndex = 36;
      this.label3.Text = "Decoy database pattern =";
      // 
      // cbFdrType
      // 
      this.cbFdrType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbFdrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFdrType.FormattingEnabled = true;
      this.cbFdrType.Location = new System.Drawing.Point(183, 45);
      this.cbFdrType.Name = "cbFdrType";
      this.cbFdrType.Size = new System.Drawing.Size(697, 20);
      this.cbFdrType.TabIndex = 35;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(40, 48);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(137, 12);
      this.label1.TabIndex = 34;
      this.label1.Text = "False Discovery Rate =";
      // 
      // BuildSummaryResultParserUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(892, 184);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtDecoyPattern);
      this.Controls.Add(this.cbFdrType);
      this.Controls.Add(this.label1);
      this.Name = "BuildSummaryResultParserUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbFdrType, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtDecoyPattern, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtDecoyPattern;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbFdrType;
    private System.Windows.Forms.Label label1;
  }
}
