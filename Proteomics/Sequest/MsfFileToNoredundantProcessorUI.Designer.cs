namespace RCPA.Proteomics.Sequest.Format
{
  partial class MsfFileToNoredundantProcessorUI
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
      this.cbExcelFormat = new System.Windows.Forms.CheckBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Size = new System.Drawing.Size(897, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(211, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(686, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(211, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 72);
      this.lblProgress.Size = new System.Drawing.Size(897, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 93);
      this.progressBar.Size = new System.Drawing.Size(897, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(496, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(411, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(326, 7);
      // 
      // cbExcelFormat
      // 
      this.cbExcelFormat.AutoSize = true;
      this.cbExcelFormat.Location = new System.Drawing.Point(211, 49);
      this.cbExcelFormat.Name = "cbExcelFormat";
      this.cbExcelFormat.Size = new System.Drawing.Size(114, 16);
      this.cbExcelFormat.TabIndex = 9;
      this.cbExcelFormat.Text = "Is excel format";
      this.cbExcelFormat.UseVisualStyleBackColor = true;
      // 
      // MsfFileToNoredundantProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(897, 150);
      this.Controls.Add(this.cbExcelFormat);
      this.Name = "MsfFileToNoredundantProcessorUI";
      this.TabText = "Convert MSF File To BuildSummary Result File";
      this.Text = "Convert MSF File To BuildSummary Result File";
      this.Controls.SetChildIndex(this.cbExcelFormat, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox cbExcelFormat;

  }
}