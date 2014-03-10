namespace RCPA.Proteomics.Statistic
{
  partial class ScanCountCalculatorUI
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
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cbMergeResult = new RCPA.Gui.RcpaCheckField();
      this.pnlFile.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlFile.Location = new System.Drawing.Point(0, 270);
      this.pnlFile.Size = new System.Drawing.Size(1040, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(794, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 292);
      this.lblProgress.Size = new System.Drawing.Size(1040, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 313);
      this.progressBar.Size = new System.Drawing.Size(1040, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(568, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(483, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(398, 7);
      // 
      // rawFiles
      // 
      this.rawFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "Files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "File";
      this.rawFiles.Location = new System.Drawing.Point(0, 0);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(1040, 238);
      this.rawFiles.TabIndex = 9;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cbMergeResult);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 238);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1040, 32);
      this.panel1.TabIndex = 10;
      // 
      // cbMergeResult
      // 
      this.cbMergeResult.AutoSize = true;
      this.cbMergeResult.Key = "MergeResult";
      this.cbMergeResult.Location = new System.Drawing.Point(3, 10);
      this.cbMergeResult.Name = "cbMergeResult";
      this.cbMergeResult.PreCondition = null;
      this.cbMergeResult.Size = new System.Drawing.Size(186, 16);
      this.cbMergeResult.TabIndex = 0;
      this.cbMergeResult.Text = "Calculate as whole data set";
      this.cbMergeResult.UseVisualStyleBackColor = true;
      // 
      // ScanCountCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1040, 370);
      this.Controls.Add(this.rawFiles);
      this.Controls.Add(this.panel1);
      this.Name = "ScanCountCalculatorUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.MultipleFileField rawFiles;
    private System.Windows.Forms.Panel panel1;
    private Gui.RcpaCheckField cbMergeResult;

  }
}
