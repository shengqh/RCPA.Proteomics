namespace RCPA.Proteomics.Statistic
{
  partial class BuildSummaryResultParserUI
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
      this.txtDecoyPattern = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cbFdrType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.inputDirectory = new RCPA.Gui.DirectoryField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 103);
      this.lblProgress.Size = new System.Drawing.Size(892, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 126);
      this.progressBar.Size = new System.Drawing.Size(892, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 149);
      this.pnlButton.Size = new System.Drawing.Size(892, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(505, 7);
      this.btnClose.Size = new System.Drawing.Size(97, 25);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(398, 7);
      this.btnCancel.Size = new System.Drawing.Size(97, 25);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(291, 7);
      this.btnGo.Size = new System.Drawing.Size(97, 25);
      // 
      // txtDecoyPattern
      // 
      this.txtDecoyPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDecoyPattern.Location = new System.Drawing.Point(240, 41);
      this.txtDecoyPattern.Name = "txtDecoyPattern";
      this.txtDecoyPattern.Size = new System.Drawing.Size(640, 20);
      this.txtDecoyPattern.TabIndex = 37;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(104, 44);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(130, 13);
      this.label3.TabIndex = 36;
      this.label3.Text = "Decoy database pattern =";
      // 
      // cbFdrType
      // 
      this.cbFdrType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbFdrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFdrType.FormattingEnabled = true;
      this.cbFdrType.Location = new System.Drawing.Point(240, 70);
      this.cbFdrType.Name = "cbFdrType";
      this.cbFdrType.Size = new System.Drawing.Size(640, 21);
      this.cbFdrType.TabIndex = 35;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(116, 73);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(117, 13);
      this.label1.TabIndex = 34;
      this.label1.Text = "False Discovery Rate =";
      // 
      // inputDirectory
      // 
      this.inputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.inputDirectory.FullName = "";
      this.inputDirectory.Key = "InputDirectory";
      this.inputDirectory.Location = new System.Drawing.Point(12, 12);
      this.inputDirectory.Name = "inputDirectory";
      this.inputDirectory.OpenButtonText = "Browse  BuildSummary Directory ...";
      this.inputDirectory.OpenButtonWidth = 226;
      this.inputDirectory.PreCondition = null;
      this.inputDirectory.Size = new System.Drawing.Size(868, 23);
      this.inputDirectory.TabIndex = 38;
      // 
      // BuildSummaryResultParserUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(892, 188);
      this.Controls.Add(this.inputDirectory);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtDecoyPattern);
      this.Controls.Add(this.cbFdrType);
      this.Controls.Add(this.label1);
      this.Name = "BuildSummaryResultParserUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbFdrType, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtDecoyPattern, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.inputDirectory, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtDecoyPattern;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbFdrType;
    private System.Windows.Forms.Label label1;
    private Gui.DirectoryField inputDirectory;
  }
}
