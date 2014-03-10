namespace RCPA.Seq
{
  partial class ShuffleDatabaseBuilderUI
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
      this.cbReversedDatabaseOnly = new System.Windows.Forms.CheckBox();
      this.btnContaminantFile = new System.Windows.Forms.Button();
      this.txtContaminantFile = new System.Windows.Forms.TextBox();
      this.cbContaminantFile = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtKlet = new System.Windows.Forms.TextBox();
      this.txtRepeat = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(31, 12);
      this.pnlFile.Size = new System.Drawing.Size(806, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(235, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(571, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(235, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 151);
      this.lblProgress.Size = new System.Drawing.Size(872, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 172);
      this.progressBar.Size = new System.Drawing.Size(872, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(484, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(399, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(314, 7);
      // 
      // cbReversedDatabaseOnly
      // 
      this.cbReversedDatabaseOnly.AutoSize = true;
      this.cbReversedDatabaseOnly.Location = new System.Drawing.Point(681, 82);
      this.cbReversedDatabaseOnly.Name = "cbReversedDatabaseOnly";
      this.cbReversedDatabaseOnly.Size = new System.Drawing.Size(156, 16);
      this.cbReversedDatabaseOnly.TabIndex = 7;
      this.cbReversedDatabaseOnly.Text = "Shuffled database only";
      this.cbReversedDatabaseOnly.UseVisualStyleBackColor = true;
      // 
      // btnContaminantFile
      // 
      this.btnContaminantFile.Location = new System.Drawing.Point(229, 48);
      this.btnContaminantFile.Name = "btnContaminantFile";
      this.btnContaminantFile.Size = new System.Drawing.Size(37, 23);
      this.btnContaminantFile.TabIndex = 8;
      this.btnContaminantFile.Text = "...";
      this.btnContaminantFile.UseVisualStyleBackColor = true;
      // 
      // txtContaminantFile
      // 
      this.txtContaminantFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtContaminantFile.Location = new System.Drawing.Point(287, 50);
      this.txtContaminantFile.Name = "txtContaminantFile";
      this.txtContaminantFile.Size = new System.Drawing.Size(550, 21);
      this.txtContaminantFile.TabIndex = 9;
      // 
      // cbContaminantFile
      // 
      this.cbContaminantFile.AutoSize = true;
      this.cbContaminantFile.Location = new System.Drawing.Point(31, 52);
      this.cbContaminantFile.Name = "cbContaminantFile";
      this.cbContaminantFile.Size = new System.Drawing.Size(192, 16);
      this.cbContaminantFile.TabIndex = 10;
      this.cbContaminantFile.Text = "Include contaminant Proteins";
      this.cbContaminantFile.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(227, 80);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 12);
      this.label1.TabIndex = 11;
      this.label1.Text = "k-let";
      // 
      // txtKlet
      // 
      this.txtKlet.Location = new System.Drawing.Point(287, 77);
      this.txtKlet.Name = "txtKlet";
      this.txtKlet.Size = new System.Drawing.Size(130, 21);
      this.txtKlet.TabIndex = 12;
      // 
      // txtRepeat
      // 
      this.txtRepeat.Location = new System.Drawing.Point(513, 80);
      this.txtRepeat.Name = "txtRepeat";
      this.txtRepeat.Size = new System.Drawing.Size(130, 21);
      this.txtRepeat.TabIndex = 14;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(453, 83);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(41, 12);
      this.label2.TabIndex = 13;
      this.label2.Text = "repeat";
      // 
      // ShuffleDatabaseBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(872, 229);
      this.Controls.Add(this.txtRepeat);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtKlet);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtContaminantFile);
      this.Controls.Add(this.cbContaminantFile);
      this.Controls.Add(this.btnContaminantFile);
      this.Controls.Add(this.cbReversedDatabaseOnly);
      this.Name = "ShuffleDatabaseBuilderUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.cbReversedDatabaseOnly, 0);
      this.Controls.SetChildIndex(this.btnContaminantFile, 0);
      this.Controls.SetChildIndex(this.cbContaminantFile, 0);
      this.Controls.SetChildIndex(this.txtContaminantFile, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtKlet, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtRepeat, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox cbReversedDatabaseOnly;
    private System.Windows.Forms.Button btnContaminantFile;
    private System.Windows.Forms.TextBox txtContaminantFile;
    private System.Windows.Forms.CheckBox cbContaminantFile;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtKlet;
    private System.Windows.Forms.TextBox txtRepeat;
    private System.Windows.Forms.Label label2;
  }
}
