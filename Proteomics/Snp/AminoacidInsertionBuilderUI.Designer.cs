namespace RCPA.Proteomics.Snp
{
  partial class AminoacidInsertionBuilderUI
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
      this.txtPeptideFile = new System.Windows.Forms.TextBox();
      this.btnPeptideFile = new System.Windows.Forms.Button();
      this.txtOutputFile = new System.Windows.Forms.TextBox();
      this.btnDatabaseFile = new System.Windows.Forms.Button();
      this.txtDatabaseFile = new System.Windows.Forms.TextBox();
      this.btnOutputFile = new System.Windows.Forms.Button();
      this.rbReversed = new RCPA.Gui.RcpaCheckField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 175);
      this.lblProgress.Size = new System.Drawing.Size(1038, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 198);
      this.progressBar.Size = new System.Drawing.Size(1038, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 221);
      this.pnlButton.Size = new System.Drawing.Size(1038, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(567, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(482, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(397, 8);
      // 
      // txtPeptideFile
      // 
      this.txtPeptideFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPeptideFile.Location = new System.Drawing.Point(287, 36);
      this.txtPeptideFile.Name = "txtPeptideFile";
      this.txtPeptideFile.Size = new System.Drawing.Size(711, 20);
      this.txtPeptideFile.TabIndex = 12;
      // 
      // btnPeptideFile
      // 
      this.btnPeptideFile.Location = new System.Drawing.Point(36, 33);
      this.btnPeptideFile.Name = "btnPeptideFile";
      this.btnPeptideFile.Size = new System.Drawing.Size(245, 25);
      this.btnPeptideFile.TabIndex = 32;
      this.btnPeptideFile.Text = "Browse Peptide File ...";
      this.btnPeptideFile.UseVisualStyleBackColor = true;
      // 
      // txtOutputFile
      // 
      this.txtOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtOutputFile.Location = new System.Drawing.Point(287, 97);
      this.txtOutputFile.Name = "txtOutputFile";
      this.txtOutputFile.Size = new System.Drawing.Size(711, 20);
      this.txtOutputFile.TabIndex = 41;
      // 
      // btnDatabaseFile
      // 
      this.btnDatabaseFile.Location = new System.Drawing.Point(36, 63);
      this.btnDatabaseFile.Name = "btnDatabaseFile";
      this.btnDatabaseFile.Size = new System.Drawing.Size(245, 25);
      this.btnDatabaseFile.TabIndex = 45;
      this.btnDatabaseFile.Text = "Browse Database File ...";
      this.btnDatabaseFile.UseVisualStyleBackColor = true;
      // 
      // txtDatabaseFile
      // 
      this.txtDatabaseFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDatabaseFile.Location = new System.Drawing.Point(287, 66);
      this.txtDatabaseFile.Name = "txtDatabaseFile";
      this.txtDatabaseFile.Size = new System.Drawing.Size(711, 20);
      this.txtDatabaseFile.TabIndex = 44;
      // 
      // btnOutputFile
      // 
      this.btnOutputFile.Location = new System.Drawing.Point(36, 94);
      this.btnOutputFile.Name = "btnOutputFile";
      this.btnOutputFile.Size = new System.Drawing.Size(245, 25);
      this.btnOutputFile.TabIndex = 46;
      this.btnOutputFile.Text = "Save Database To ...";
      this.btnOutputFile.UseVisualStyleBackColor = true;
      // 
      // rbReversed
      // 
      this.rbReversed.AutoSize = true;
      this.rbReversed.Key = "rbReversed";
      this.rbReversed.Location = new System.Drawing.Point(287, 127);
      this.rbReversed.Name = "rbReversed";
      this.rbReversed.PreCondition = null;
      this.rbReversed.Size = new System.Drawing.Size(157, 17);
      this.rbReversed.TabIndex = 47;
      this.rbReversed.Text = "Generate reversed peptides";
      this.rbReversed.UseVisualStyleBackColor = true;
      // 
      // AminoacidInsertionBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1038, 260);
      this.Controls.Add(this.rbReversed);
      this.Controls.Add(this.btnOutputFile);
      this.Controls.Add(this.btnDatabaseFile);
      this.Controls.Add(this.txtDatabaseFile);
      this.Controls.Add(this.txtOutputFile);
      this.Controls.Add(this.btnPeptideFile);
      this.Controls.Add(this.txtPeptideFile);
      this.Name = "AminoacidInsertionBuilderUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtPeptideFile, 0);
      this.Controls.SetChildIndex(this.btnPeptideFile, 0);
      this.Controls.SetChildIndex(this.txtOutputFile, 0);
      this.Controls.SetChildIndex(this.txtDatabaseFile, 0);
      this.Controls.SetChildIndex(this.btnDatabaseFile, 0);
      this.Controls.SetChildIndex(this.btnOutputFile, 0);
      this.Controls.SetChildIndex(this.rbReversed, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtPeptideFile;
    private System.Windows.Forms.Button btnPeptideFile;
    private System.Windows.Forms.TextBox txtOutputFile;
    private System.Windows.Forms.Button btnDatabaseFile;
    private System.Windows.Forms.TextBox txtDatabaseFile;
    private System.Windows.Forms.Button btnOutputFile;
    private Gui.RcpaCheckField rbReversed;
  }
}
