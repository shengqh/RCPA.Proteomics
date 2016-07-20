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
      this.txtLog = new System.Windows.Forms.RichTextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbOverwrite = new RCPA.Gui.RcpaCheckField();
      this.cbDrawImage = new RCPA.Gui.RcpaCheckField();
      this.cbExcludeIsotopic0InFormula = new RCPA.Gui.RcpaCheckField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 474);
      this.lblProgress.Size = new System.Drawing.Size(1200, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 497);
      this.progressBar.Size = new System.Drawing.Size(1200, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 520);
      this.pnlButton.Size = new System.Drawing.Size(1200, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(648, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(563, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(478, 8);
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
      this.txtPeptideFile.Size = new System.Drawing.Size(948, 20);
      this.txtPeptideFile.TabIndex = 10;
      // 
      // txtRawDirectory
      // 
      this.txtRawDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRawDirectory.Location = new System.Drawing.Point(240, 59);
      this.txtRawDirectory.Name = "txtRawDirectory";
      this.txtRawDirectory.Size = new System.Drawing.Size(948, 20);
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
      // txtLog
      // 
      this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtLog.Location = new System.Drawing.Point(15, 139);
      this.txtLog.Name = "txtLog";
      this.txtLog.ReadOnly = true;
      this.txtLog.Size = new System.Drawing.Size(1173, 332);
      this.txtLog.TabIndex = 13;
      this.txtLog.Text = "";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 123);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(104, 13);
      this.label1.TabIndex = 14;
      this.label1.Text = "Running information:";
      // 
      // cbOverwrite
      // 
      this.cbOverwrite.AutoSize = true;
      this.cbOverwrite.Key = "Overwrite";
      this.cbOverwrite.Location = new System.Drawing.Point(240, 87);
      this.cbOverwrite.Name = "cbOverwrite";
      this.cbOverwrite.PreCondition = null;
      this.cbOverwrite.Size = new System.Drawing.Size(116, 17);
      this.cbOverwrite.TabIndex = 15;
      this.cbOverwrite.Text = "Overwrite old result";
      this.cbOverwrite.UseVisualStyleBackColor = true;
      // 
      // cbDrawImage
      // 
      this.cbDrawImage.AutoSize = true;
      this.cbDrawImage.Checked = true;
      this.cbDrawImage.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbDrawImage.Key = "DrawImage";
      this.cbDrawImage.Location = new System.Drawing.Point(388, 87);
      this.cbDrawImage.Name = "cbDrawImage";
      this.cbDrawImage.PreCondition = null;
      this.cbDrawImage.Size = new System.Drawing.Size(82, 17);
      this.cbDrawImage.TabIndex = 15;
      this.cbDrawImage.Text = "Draw image";
      this.cbDrawImage.UseVisualStyleBackColor = true;
      // 
      // cbExcludeIsotopic0InFormula
      // 
      this.cbExcludeIsotopic0InFormula.AutoSize = true;
      this.cbExcludeIsotopic0InFormula.Key = "ExcludeIsotopic0InFormula";
      this.cbExcludeIsotopic0InFormula.Location = new System.Drawing.Point(488, 87);
      this.cbExcludeIsotopic0InFormula.Name = "cbExcludeIsotopic0InFormula";
      this.cbExcludeIsotopic0InFormula.PreCondition = null;
      this.cbExcludeIsotopic0InFormula.Size = new System.Drawing.Size(160, 17);
      this.cbExcludeIsotopic0InFormula.TabIndex = 15;
      this.cbExcludeIsotopic0InFormula.Text = "Exclude isotopic 0 in formula";
      this.cbExcludeIsotopic0InFormula.UseVisualStyleBackColor = true;
      // 
      // DeuteriumCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1200, 559);
      this.Controls.Add(this.cbExcludeIsotopic0InFormula);
      this.Controls.Add(this.cbDrawImage);
      this.Controls.Add(this.cbOverwrite);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtLog);
      this.Controls.Add(this.txtRawDirectory);
      this.Controls.Add(this.btnRawDirectory);
      this.Controls.Add(this.txtPeptideFile);
      this.Controls.Add(this.btnPeptideFile);
      this.Name = "DeuteriumCalculatorUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.btnPeptideFile, 0);
      this.Controls.SetChildIndex(this.txtPeptideFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.btnRawDirectory, 0);
      this.Controls.SetChildIndex(this.txtRawDirectory, 0);
      this.Controls.SetChildIndex(this.txtLog, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbOverwrite, 0);
      this.Controls.SetChildIndex(this.cbDrawImage, 0);
      this.Controls.SetChildIndex(this.cbExcludeIsotopic0InFormula, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnPeptideFile;
    private System.Windows.Forms.TextBox txtPeptideFile;
    private System.Windows.Forms.TextBox txtRawDirectory;
    private System.Windows.Forms.Button btnRawDirectory;
    private System.Windows.Forms.RichTextBox txtLog;
    private System.Windows.Forms.Label label1;
    private Gui.RcpaCheckField cbOverwrite;
    private Gui.RcpaCheckField cbDrawImage;
    private Gui.RcpaCheckField cbExcludeIsotopic0InFormula;
  }
}
