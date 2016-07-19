namespace RCPA.Proteomics.Quantification.Labelfree
{
  partial class ChromatographProfileBuilderUI
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
      this.rbDrawImage = new RCPA.Gui.RcpaCheckField();
      this.maxThread = new RCPA.Gui.IntegerField();
      this.rbOverwrite = new RCPA.Gui.RcpaCheckField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 426);
      this.lblProgress.Size = new System.Drawing.Size(1311, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 449);
      this.progressBar.Size = new System.Drawing.Size(1311, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 472);
      this.pnlButton.Size = new System.Drawing.Size(1311, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(703, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(618, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(533, 8);
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
      this.txtPeptideFile.Size = new System.Drawing.Size(1059, 20);
      this.txtPeptideFile.TabIndex = 10;
      // 
      // txtRawDirectory
      // 
      this.txtRawDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRawDirectory.Location = new System.Drawing.Point(240, 50);
      this.txtRawDirectory.Name = "txtRawDirectory";
      this.txtRawDirectory.Size = new System.Drawing.Size(1059, 20);
      this.txtRawDirectory.TabIndex = 12;
      // 
      // btnRawDirectory
      // 
      this.btnRawDirectory.Location = new System.Drawing.Point(15, 47);
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
      this.txtLog.Location = new System.Drawing.Point(15, 127);
      this.txtLog.Name = "txtLog";
      this.txtLog.ReadOnly = true;
      this.txtLog.Size = new System.Drawing.Size(1284, 296);
      this.txtLog.TabIndex = 13;
      this.txtLog.Text = "";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 111);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(104, 13);
      this.label1.TabIndex = 14;
      this.label1.Text = "Running information:";
      // 
      // rbDrawImage
      // 
      this.rbDrawImage.AutoSize = true;
      this.rbDrawImage.Key = "rbDrawImage";
      this.rbDrawImage.Location = new System.Drawing.Point(240, 82);
      this.rbDrawImage.Name = "rbDrawImage";
      this.rbDrawImage.PreCondition = null;
      this.rbDrawImage.Size = new System.Drawing.Size(129, 17);
      this.rbDrawImage.TabIndex = 15;
      this.rbDrawImage.Text = "Draw individual image";
      this.rbDrawImage.UseVisualStyleBackColor = true;
      // 
      // maxThread
      // 
      this.maxThread.Caption = "Maximum thread (0 means using all)";
      this.maxThread.CaptionWidth = 193;
      this.maxThread.DefaultValue = "0";
      this.maxThread.Description = "";
      this.maxThread.Key = "MaximumThread";
      this.maxThread.Location = new System.Drawing.Point(554, 79);
      this.maxThread.Name = "maxThread";
      this.maxThread.PreCondition = null;
      this.maxThread.Required = false;
      this.maxThread.Size = new System.Drawing.Size(282, 23);
      this.maxThread.TabIndex = 16;
      this.maxThread.TextWidth = 89;
      this.maxThread.Value = 0;
      // 
      // overwrite
      // 
      this.rbOverwrite.AutoSize = true;
      this.rbOverwrite.Key = "rbOverwrite";
      this.rbOverwrite.Location = new System.Drawing.Point(411, 82);
      this.rbOverwrite.Name = "overwrite";
      this.rbOverwrite.PreCondition = null;
      this.rbOverwrite.Size = new System.Drawing.Size(116, 17);
      this.rbOverwrite.TabIndex = 17;
      this.rbOverwrite.Text = "Overwrite old result";
      this.rbOverwrite.UseVisualStyleBackColor = true;
      // 
      // ChromatographProfileBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1311, 511);
      this.Controls.Add(this.rbOverwrite);
      this.Controls.Add(this.maxThread);
      this.Controls.Add(this.rbDrawImage);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtLog);
      this.Controls.Add(this.txtRawDirectory);
      this.Controls.Add(this.btnRawDirectory);
      this.Controls.Add(this.txtPeptideFile);
      this.Controls.Add(this.btnPeptideFile);
      this.Name = "ChromatographProfileBuilderUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.btnPeptideFile, 0);
      this.Controls.SetChildIndex(this.txtPeptideFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.btnRawDirectory, 0);
      this.Controls.SetChildIndex(this.txtRawDirectory, 0);
      this.Controls.SetChildIndex(this.txtLog, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.rbDrawImage, 0);
      this.Controls.SetChildIndex(this.maxThread, 0);
      this.Controls.SetChildIndex(this.rbOverwrite, 0);
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
    private Gui.RcpaCheckField rbDrawImage;
    private Gui.IntegerField maxThread;
    private Gui.RcpaCheckField rbOverwrite;
  }
}
