namespace RCPA.Proteomics.Format
{
  partial class ReplaceMascotGenericFormatProcessorUI
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
      this.filePeak = new RCPA.Gui.FileField();
      this.label7 = new System.Windows.Forms.Label();
      this.cbHeaderFormat = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbPeakFormat = new System.Windows.Forms.ComboBox();
      this.fileHeader = new RCPA.Gui.FileField();
      this.lblInfo = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(30, 183);
      this.pnlFile.Size = new System.Drawing.Size(991, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(250, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(741, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(250, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 280);
      this.lblProgress.Size = new System.Drawing.Size(1047, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 301);
      this.progressBar.Size = new System.Drawing.Size(1047, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(571, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(486, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(401, 7);
      // 
      // filePeak
      // 
      this.filePeak.FullName = "";
      this.filePeak.Key = "PeakFile";
      this.filePeak.Location = new System.Drawing.Point(30, 117);
      this.filePeak.Name = "filePeak";
      this.filePeak.OpenButtonText = "Browse All File ...";
      this.filePeak.WidthOpenButton = 250;
      this.filePeak.PreCondition = null;
      this.filePeak.Size = new System.Drawing.Size(991, 21);
      this.filePeak.TabIndex = 10;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(143, 78);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(131, 12);
      this.label7.TabIndex = 24;
      this.label7.Text = "Header title format :";
      // 
      // cbHeaderFormat
      // 
      this.cbHeaderFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbHeaderFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbHeaderFormat.FormattingEnabled = true;
      this.cbHeaderFormat.Location = new System.Drawing.Point(280, 75);
      this.cbHeaderFormat.Name = "cbHeaderFormat";
      this.cbHeaderFormat.Size = new System.Drawing.Size(741, 20);
      this.cbHeaderFormat.TabIndex = 23;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(155, 146);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(119, 12);
      this.label1.TabIndex = 26;
      this.label1.Text = "Peak title format :";
      // 
      // cbPeakFormat
      // 
      this.cbPeakFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbPeakFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbPeakFormat.FormattingEnabled = true;
      this.cbPeakFormat.Location = new System.Drawing.Point(280, 143);
      this.cbPeakFormat.Name = "cbPeakFormat";
      this.cbPeakFormat.Size = new System.Drawing.Size(741, 20);
      this.cbPeakFormat.TabIndex = 25;
      // 
      // fileHeader
      // 
      this.fileHeader.FullName = "";
      this.fileHeader.Key = "HeadFile";
      this.fileHeader.Location = new System.Drawing.Point(30, 49);
      this.fileHeader.Name = "fileHeader";
      this.fileHeader.OpenButtonText = "Browse All File ...";
      this.fileHeader.WidthOpenButton = 250;
      this.fileHeader.PreCondition = null;
      this.fileHeader.Size = new System.Drawing.Size(991, 21);
      this.fileHeader.TabIndex = 27;
      // 
      // lblInfo
      // 
      this.lblInfo.AutoSize = true;
      this.lblInfo.Location = new System.Drawing.Point(28, 21);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(0, 12);
      this.lblInfo.TabIndex = 28;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(143, 214);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(131, 12);
      this.label2.TabIndex = 30;
      this.label2.Text = "Target title format :";
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(280, 211);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(741, 20);
      this.cbTitleFormat.TabIndex = 29;
      // 
      // CombineMascotGenericFormatProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1047, 358);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cbTitleFormat);
      this.Controls.Add(this.lblInfo);
      this.Controls.Add(this.fileHeader);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbPeakFormat);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.cbHeaderFormat);
      this.Controls.Add(this.filePeak);
      this.Name = "CombineMascotGenericFormatProcessorUI";
      this.TabText = "CombineMascotGenericFormatProcessorUI";
      this.Text = "CombineMascotGenericFormatProcessorUI";
      this.Controls.SetChildIndex(this.filePeak, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.cbHeaderFormat, 0);
      this.Controls.SetChildIndex(this.label7, 0);
      this.Controls.SetChildIndex(this.cbPeakFormat, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.fileHeader, 0);
      this.Controls.SetChildIndex(this.lblInfo, 0);
      this.Controls.SetChildIndex(this.cbTitleFormat, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Gui.FileField filePeak;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cbHeaderFormat;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbPeakFormat;
    private Gui.FileField fileHeader;
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbTitleFormat;

  }
}