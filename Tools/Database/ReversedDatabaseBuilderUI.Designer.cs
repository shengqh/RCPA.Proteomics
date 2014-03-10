namespace RCPA.Tools.Database
{
  partial class ReversedDatabaseBuilderUI
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
      this.cbSwitch = new System.Windows.Forms.CheckBox();
      this.txtTermini = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbPrior = new System.Windows.Forms.ComboBox();
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
      this.lblProgress.Location = new System.Drawing.Point(0, 176);
      this.lblProgress.Size = new System.Drawing.Size(872, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 197);
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
      this.cbReversedDatabaseOnly.Location = new System.Drawing.Point(110, 109);
      this.cbReversedDatabaseOnly.Name = "cbReversedDatabaseOnly";
      this.cbReversedDatabaseOnly.Size = new System.Drawing.Size(156, 16);
      this.cbReversedDatabaseOnly.TabIndex = 7;
      this.cbReversedDatabaseOnly.Text = "Reversed database only";
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
      // cbSwitch
      // 
      this.cbSwitch.AutoSize = true;
      this.cbSwitch.Location = new System.Drawing.Point(104, 79);
      this.cbSwitch.Name = "cbSwitch";
      this.cbSwitch.Size = new System.Drawing.Size(162, 16);
      this.cbSwitch.TabIndex = 11;
      this.cbSwitch.Text = "Switch protease termini";
      this.cbSwitch.UseVisualStyleBackColor = true;
      // 
      // txtTermini
      // 
      this.txtTermini.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTermini.Location = new System.Drawing.Point(287, 77);
      this.txtTermini.Name = "txtTermini";
      this.txtTermini.Size = new System.Drawing.Size(140, 21);
      this.txtTermini.TabIndex = 12;
      this.txtTermini.Text = "KR";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(433, 80);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(29, 12);
      this.label1.TabIndex = 13;
      this.label1.Text = "with";
      // 
      // cbPrior
      // 
      this.cbPrior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbPrior.FormattingEnabled = true;
      this.cbPrior.Items.AddRange(new object[] {
            "previous",
            "next"});
      this.cbPrior.Location = new System.Drawing.Point(468, 77);
      this.cbPrior.Name = "cbPrior";
      this.cbPrior.Size = new System.Drawing.Size(121, 20);
      this.cbPrior.TabIndex = 14;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(595, 80);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(65, 12);
      this.label2.TabIndex = 15;
      this.label2.Text = "amino acid";
      // 
      // ReversedDatabaseBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(872, 254);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cbPrior);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtTermini);
      this.Controls.Add(this.cbSwitch);
      this.Controls.Add(this.txtContaminantFile);
      this.Controls.Add(this.cbContaminantFile);
      this.Controls.Add(this.btnContaminantFile);
      this.Controls.Add(this.cbReversedDatabaseOnly);
      this.Name = "ReversedDatabaseBuilderUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.cbReversedDatabaseOnly, 0);
      this.Controls.SetChildIndex(this.btnContaminantFile, 0);
      this.Controls.SetChildIndex(this.cbContaminantFile, 0);
      this.Controls.SetChildIndex(this.txtContaminantFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.cbSwitch, 0);
      this.Controls.SetChildIndex(this.txtTermini, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbPrior, 0);
      this.Controls.SetChildIndex(this.label2, 0);
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
    private System.Windows.Forms.CheckBox cbSwitch;
    private System.Windows.Forms.TextBox txtTermini;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbPrior;
    private System.Windows.Forms.Label label2;
  }
}
