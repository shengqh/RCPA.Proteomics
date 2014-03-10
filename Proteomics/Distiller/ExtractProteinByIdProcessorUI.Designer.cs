namespace RCPA.Proteomics.Distiller
{
  partial class ExtractProteinByIdProcessorUI
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
      this.btnAccessNumberFile = new System.Windows.Forms.Button();
      this.txtAccessNumberFile = new System.Windows.Forms.TextBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(35, 12);
      this.pnlFile.Size = new System.Drawing.Size(861, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(219, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(642, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(219, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 122);
      this.lblProgress.Size = new System.Drawing.Size(931, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 143);
      this.progressBar.Size = new System.Drawing.Size(931, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(513, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(428, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(343, 7);
      // 
      // btnAccessNumberFile
      // 
      this.btnAccessNumberFile.Location = new System.Drawing.Point(35, 57);
      this.btnAccessNumberFile.Name = "btnAccessNumberFile";
      this.btnAccessNumberFile.Size = new System.Drawing.Size(219, 23);
      this.btnAccessNumberFile.TabIndex = 9;
      this.btnAccessNumberFile.Text = "button1";
      this.btnAccessNumberFile.UseVisualStyleBackColor = true;
      // 
      // txtAccessNumberFile
      // 
      this.txtAccessNumberFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtAccessNumberFile.Location = new System.Drawing.Point(260, 58);
      this.txtAccessNumberFile.Name = "txtAccessNumberFile";
      this.txtAccessNumberFile.Size = new System.Drawing.Size(636, 21);
      this.txtAccessNumberFile.TabIndex = 10;
      // 
      // ExtractProteinByIdProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(931, 200);
      this.Controls.Add(this.txtAccessNumberFile);
      this.Controls.Add(this.btnAccessNumberFile);
      this.Name = "ExtractProteinByIdProcessorUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.btnAccessNumberFile, 0);
      this.Controls.SetChildIndex(this.txtAccessNumberFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnAccessNumberFile;
    private System.Windows.Forms.TextBox txtAccessNumberFile;

  }
}
