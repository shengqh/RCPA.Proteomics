namespace RCPA.Proteomics.Mascot
{
  partial class MascotGenericFormatSplitterUI
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
      this.txtFileSize = new System.Windows.Forms.TextBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlFile.Location = new System.Drawing.Point(0, 0);
      this.pnlFile.Size = new System.Drawing.Size(905, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(266, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(639, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(266, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 77);
      this.lblProgress.Size = new System.Drawing.Size(905, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 98);
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
      this.label1.Location = new System.Drawing.Point(139, 54);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(281, 12);
      this.label1.TabIndex = 9;
      this.label1.Text = "Limit each file less than                   MB";
      // 
      // txtFileSize
      // 
      this.txtFileSize.Location = new System.Drawing.Point(296, 51);
      this.txtFileSize.Name = "txtFileSize";
      this.txtFileSize.Size = new System.Drawing.Size(100, 21);
      this.txtFileSize.TabIndex = 10;
      this.txtFileSize.Text = "100";
      // 
      // MascotGenericFormatSplitterUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(905, 155);
      this.Controls.Add(this.txtFileSize);
      this.Controls.Add(this.label1);
      this.Name = "MascotGenericFormatSplitterUI";
      this.TabText = "MascotGenericFormatSplitterUI";
      this.Text = "MascotGenericFormatSplitterUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtFileSize, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtFileSize;
  }
}