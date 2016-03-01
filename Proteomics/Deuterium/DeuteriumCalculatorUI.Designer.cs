namespace RCPA.Proteomics.Deuterium
{
  partial class DeuteriumCalculatorUI
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
      this.btnPeptideFile = new System.Windows.Forms.Button();
      this.txtPeptideFile = new System.Windows.Forms.TextBox();
      this.txtRawDirectory = new System.Windows.Forms.TextBox();
      this.btnRawDirectory = new System.Windows.Forms.Button();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 101);
      this.lblProgress.Size = new System.Drawing.Size(931, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 124);
      this.progressBar.Size = new System.Drawing.Size(931, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 147);
      this.pnlButton.Size = new System.Drawing.Size(931, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(513, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(428, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(343, 8);
      // 
      // btnPeptideFile
      // 
      this.btnPeptideFile.Location = new System.Drawing.Point(15, 21);
      this.btnPeptideFile.Name = "btnPeptideFile";
      this.btnPeptideFile.Size = new System.Drawing.Size(219, 25);
      this.btnPeptideFile.TabIndex = 9;
      this.btnPeptideFile.Text = "button1";
      this.btnPeptideFile.UseVisualStyleBackColor = true;
      // 
      // txtPeptideFile
      // 
      this.txtPeptideFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPeptideFile.Location = new System.Drawing.Point(240, 24);
      this.txtPeptideFile.Name = "txtPeptideFile";
      this.txtPeptideFile.Size = new System.Drawing.Size(679, 20);
      this.txtPeptideFile.TabIndex = 10;
      // 
      // txtRawDirectory
      // 
      this.txtRawDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRawDirectory.Location = new System.Drawing.Point(240, 59);
      this.txtRawDirectory.Name = "txtRawDirectory";
      this.txtRawDirectory.Size = new System.Drawing.Size(679, 20);
      this.txtRawDirectory.TabIndex = 12;
      // 
      // btnRawDirectory
      // 
      this.btnRawDirectory.Location = new System.Drawing.Point(15, 56);
      this.btnRawDirectory.Name = "btnRawDirectory";
      this.btnRawDirectory.Size = new System.Drawing.Size(219, 25);
      this.btnRawDirectory.TabIndex = 11;
      this.btnRawDirectory.Text = "button1";
      this.btnRawDirectory.UseVisualStyleBackColor = true;
      // 
      // DeuteriumBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(931, 186);
      this.Controls.Add(this.txtRawDirectory);
      this.Controls.Add(this.btnRawDirectory);
      this.Controls.Add(this.txtPeptideFile);
      this.Controls.Add(this.btnPeptideFile);
      this.Name = "DeuteriumBuilderUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.btnPeptideFile, 0);
      this.Controls.SetChildIndex(this.txtPeptideFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.btnRawDirectory, 0);
      this.Controls.SetChildIndex(this.txtRawDirectory, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnPeptideFile;
    private System.Windows.Forms.TextBox txtPeptideFile;
    private System.Windows.Forms.TextBox txtRawDirectory;
    private System.Windows.Forms.Button btnRawDirectory;
  }
}
