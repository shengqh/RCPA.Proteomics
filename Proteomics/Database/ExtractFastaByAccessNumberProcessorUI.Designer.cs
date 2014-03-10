namespace RCPA.Proteomics.Database
{
  partial class ExtractFastaByAccessNumberProcessorUI
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
      this.btnFastaFile = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbAccessNumberParser = new System.Windows.Forms.ComboBox();
      this.cbReplaceName = new System.Windows.Forms.CheckBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(35, 34);
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
      this.lblProgress.Location = new System.Drawing.Point(0, 183);
      this.lblProgress.Size = new System.Drawing.Size(931, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 204);
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
      // btnFastaFile
      // 
      this.btnFastaFile.Location = new System.Drawing.Point(35, 60);
      this.btnFastaFile.Name = "btnFastaFile";
      this.btnFastaFile.Size = new System.Drawing.Size(219, 23);
      this.btnFastaFile.TabIndex = 9;
      this.btnFastaFile.Text = "button1";
      this.btnFastaFile.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(260, 62);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(636, 21);
      this.textBox1.TabIndex = 10;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(129, 95);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(125, 12);
      this.label1.TabIndex = 11;
      this.label1.Text = "Access number format";
      // 
      // cbAccessNumberParser
      // 
      this.cbAccessNumberParser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbAccessNumberParser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbAccessNumberParser.FormattingEnabled = true;
      this.cbAccessNumberParser.Location = new System.Drawing.Point(260, 92);
      this.cbAccessNumberParser.Name = "cbAccessNumberParser";
      this.cbAccessNumberParser.Size = new System.Drawing.Size(636, 20);
      this.cbAccessNumberParser.TabIndex = 12;
      // 
      // cbReplaceName
      // 
      this.cbReplaceName.AutoSize = true;
      this.cbReplaceName.Location = new System.Drawing.Point(260, 118);
      this.cbReplaceName.Name = "cbReplaceName";
      this.cbReplaceName.Size = new System.Drawing.Size(258, 16);
      this.cbReplaceName.TabIndex = 13;
      this.cbReplaceName.Text = "Replace protein name with access number";
      this.cbReplaceName.UseVisualStyleBackColor = true;
      // 
      // ExtractFastaByAccessNumberProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(931, 261);
      this.Controls.Add(this.btnFastaFile);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.cbAccessNumberParser);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbReplaceName);
      this.Name = "ExtractFastaByAccessNumberProcessorUI";
      this.Controls.SetChildIndex(this.cbReplaceName, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbAccessNumberParser, 0);
      this.Controls.SetChildIndex(this.textBox1, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.btnFastaFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnFastaFile;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbAccessNumberParser;
    private System.Windows.Forms.CheckBox cbReplaceName;


  }
}
