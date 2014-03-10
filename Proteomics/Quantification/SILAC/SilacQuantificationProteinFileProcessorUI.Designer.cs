namespace RCPA.Proteomics.Quantification.SILAC
{
  partial class SilacQuantificationProteinFileProcessorUI
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
      this.btnRawDirectory = new System.Windows.Forms.Button();
      this.txtRawDirectory = new System.Windows.Forms.TextBox();
      this.txtSilacFile = new System.Windows.Forms.TextBox();
      this.txtPrecursorTolerance = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnSilacFile = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtMinPepRegCorrelation = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtProfileLength = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(34, 27);
      this.pnlFile.Size = new System.Drawing.Size(943, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(302, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(641, 20);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(302, 24);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 279);
      this.lblProgress.Size = new System.Drawing.Size(1012, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 302);
      this.progressBar.Size = new System.Drawing.Size(1012, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(554, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(469, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(384, 8);
      // 
      // btnRawDirectory
      // 
      this.btnRawDirectory.Location = new System.Drawing.Point(34, 57);
      this.btnRawDirectory.Name = "btnRawDirectory";
      this.btnRawDirectory.Size = new System.Drawing.Size(302, 23);
      this.btnRawDirectory.TabIndex = 7;
      this.btnRawDirectory.Text = "Raw Directory";
      this.btnRawDirectory.UseVisualStyleBackColor = true;
      // 
      // txtRawDirectory
      // 
      this.txtRawDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRawDirectory.Location = new System.Drawing.Point(336, 59);
      this.txtRawDirectory.Name = "txtRawDirectory";
      this.txtRawDirectory.Size = new System.Drawing.Size(641, 20);
      this.txtRawDirectory.TabIndex = 8;
      // 
      // txtSilacFile
      // 
      this.txtSilacFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSilacFile.Location = new System.Drawing.Point(336, 118);
      this.txtSilacFile.Name = "txtSilacFile";
      this.txtSilacFile.Size = new System.Drawing.Size(641, 20);
      this.txtSilacFile.TabIndex = 10;
      // 
      // txtPrecursorTolerance
      // 
      this.txtPrecursorTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPrecursorTolerance.Location = new System.Drawing.Point(336, 150);
      this.txtPrecursorTolerance.Name = "txtPrecursorTolerance";
      this.txtPrecursorTolerance.Size = new System.Drawing.Size(641, 20);
      this.txtPrecursorTolerance.TabIndex = 12;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(207, 153);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(129, 13);
      this.label2.TabIndex = 11;
      this.label2.Text = "Precursor PPM Tolerance";
      // 
      // btnSilacFile
      // 
      this.btnSilacFile.Location = new System.Drawing.Point(34, 117);
      this.btnSilacFile.Name = "btnSilacFile";
      this.btnSilacFile.Size = new System.Drawing.Size(302, 23);
      this.btnSilacFile.TabIndex = 7;
      this.btnSilacFile.Text = "Silac File";
      this.btnSilacFile.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(336, 180);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(641, 20);
      this.textBox1.TabIndex = 16;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(47, 183);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(289, 13);
      this.label3.TabIndex = 15;
      this.label3.Text = "Ignore modifications (such like @ for Heavy labelling of Leu)";
      // 
      // comboBox1
      // 
      this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(336, 88);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(641, 21);
      this.comboBox1.TabIndex = 18;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(272, 91);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(64, 13);
      this.label4.TabIndex = 17;
      this.label4.Text = "Raw Format";
      // 
      // txtMinPepRegCorrelation
      // 
      this.txtMinPepRegCorrelation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinPepRegCorrelation.Location = new System.Drawing.Point(336, 209);
      this.txtMinPepRegCorrelation.Name = "txtMinPepRegCorrelation";
      this.txtMinPepRegCorrelation.Size = new System.Drawing.Size(641, 20);
      this.txtMinPepRegCorrelation.TabIndex = 20;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(147, 212);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(189, 13);
      this.label5.TabIndex = 19;
      this.label5.Text = "Minimum peptide regression correlation";
      // 
      // txtProfileLength
      // 
      this.txtProfileLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtProfileLength.Location = new System.Drawing.Point(336, 238);
      this.txtProfileLength.Name = "txtProfileLength";
      this.txtProfileLength.Size = new System.Drawing.Size(641, 20);
      this.txtProfileLength.TabIndex = 22;
      this.txtProfileLength.Text = "3";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(268, 241);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(68, 13);
      this.label6.TabIndex = 21;
      this.label6.Text = "Profile length";
      // 
      // SilacQuantificationProteinFileProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1012, 364);
      this.Controls.Add(this.txtProfileLength);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.txtMinPepRegCorrelation);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtPrecursorTolerance);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnSilacFile);
      this.Controls.Add(this.btnRawDirectory);
      this.Controls.Add(this.txtRawDirectory);
      this.Controls.Add(this.txtSilacFile);
      this.Name = "SilacQuantificationProteinFileProcessorUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.txtSilacFile, 0);
      this.Controls.SetChildIndex(this.txtRawDirectory, 0);
      this.Controls.SetChildIndex(this.btnRawDirectory, 0);
      this.Controls.SetChildIndex(this.btnSilacFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtPrecursorTolerance, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.textBox1, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.comboBox1, 0);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.txtMinPepRegCorrelation, 0);
      this.Controls.SetChildIndex(this.label6, 0);
      this.Controls.SetChildIndex(this.txtProfileLength, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnRawDirectory;
    private System.Windows.Forms.TextBox txtRawDirectory;
    private System.Windows.Forms.TextBox txtSilacFile;
    private System.Windows.Forms.TextBox txtPrecursorTolerance;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnSilacFile;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtMinPepRegCorrelation;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtProfileLength;
    private System.Windows.Forms.Label label6;
  }
}
