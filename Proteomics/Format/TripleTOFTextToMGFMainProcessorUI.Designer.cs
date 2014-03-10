namespace RCPA.Proteomics.Format
{
  partial class TripleTOFTextToMGFMainProcessorUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TripleTOFTextToMGFMainProcessorUI));
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.txtPrecursorTolerance = new System.Windows.Forms.TextBox();
      this.cbUsePrecursorTolerance = new RCPA.Gui.RcpaCheckField();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(27, 428);
      this.pnlFile.Size = new System.Drawing.Size(840, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(594, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 466);
      this.lblProgress.Size = new System.Drawing.Size(897, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 487);
      this.progressBar.Size = new System.Drawing.Size(897, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(496, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(411, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(326, 7);
      // 
      // rawFiles
      // 
      this.rawFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.rawFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "TripleTOF Peak List Text Files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "File";
      this.rawFiles.Location = new System.Drawing.Point(27, 19);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(840, 368);
      this.rawFiles.TabIndex = 49;
      // 
      // txtPrecursorTolerance
      // 
      this.txtPrecursorTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPrecursorTolerance.Location = new System.Drawing.Point(393, 401);
      this.txtPrecursorTolerance.Name = "txtPrecursorTolerance";
      this.txtPrecursorTolerance.Size = new System.Drawing.Size(474, 21);
      this.txtPrecursorTolerance.TabIndex = 51;
      // 
      // cbUsePrecursorTolerance
      // 
      this.cbUsePrecursorTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cbUsePrecursorTolerance.Key = "Key";
      this.cbUsePrecursorTolerance.Location = new System.Drawing.Point(27, 400);
      this.cbUsePrecursorTolerance.Name = "cbUsePrecursorTolerance";
      this.cbUsePrecursorTolerance.PreCondition = null;
      this.cbUsePrecursorTolerance.Size = new System.Drawing.Size(360, 22);
      this.cbUsePrecursorTolerance.TabIndex = 52;
      this.cbUsePrecursorTolerance.Text = "Rank by precursor intensity, precursor tolerance (ppm) :";
      // 
      // TripleTOFTextToMGFMainProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(897, 544);
      this.Controls.Add(this.cbUsePrecursorTolerance);
      this.Controls.Add(this.txtPrecursorTolerance);
      this.Controls.Add(this.rawFiles);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "TripleTOFTextToMGFMainProcessorUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.Controls.SetChildIndex(this.txtPrecursorTolerance, 0);
      this.Controls.SetChildIndex(this.cbUsePrecursorTolerance, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RCPA.Gui.MultipleFileField rawFiles;
    private System.Windows.Forms.TextBox txtPrecursorTolerance;
    private Gui.RcpaCheckField cbUsePrecursorTolerance;
  }
}