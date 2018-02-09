namespace RCPA.Proteomics.Format
{
  partial class MultipleRaw2MSnProcessorUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleRaw2MSnProcessorUI));
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.pnlFile.SuspendLayout();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlFile.Location = new System.Drawing.Point(0, 549);
      this.pnlFile.Size = new System.Drawing.Size(1083, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(837, 20);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 573);
      this.lblProgress.Size = new System.Drawing.Size(1083, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 596);
      this.progressBar.Size = new System.Drawing.Size(1083, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 619);
      this.pnlButton.Size = new System.Drawing.Size(1083, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(589, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(504, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(419, 8);
      // 
      // rawFiles
      // 
      this.rawFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.rawFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "Thermo RAW files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "RawFiles";
      this.rawFiles.Location = new System.Drawing.Point(2, 3);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(1081, 508);
      this.rawFiles.TabIndex = 49;
      // 
      // MultipleRaw2MSnProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1083, 658);
      this.Controls.Add(this.rawFiles);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MultipleRaw2MSnProcessorUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private RCPA.Gui.MultipleFileField rawFiles;
  }
}