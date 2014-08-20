namespace RCPA.Proteomics.Format
{
  partial class MultipleMgf2Ms2ProcessorUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleMgfPrecursorCorrectionProcessorUI));
      this.mgfFiles = new RCPA.Gui.MultipleFileField();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlFile.Location = new System.Drawing.Point(0, 507);
      this.pnlFile.Size = new System.Drawing.Size(1083, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(837, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 529);
      this.lblProgress.Size = new System.Drawing.Size(1083, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 550);
      this.progressBar.Size = new System.Drawing.Size(1083, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(589, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(504, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(419, 7);
      // 
      // mgfFiles
      // 
      this.mgfFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.mgfFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.mgfFiles.FileArgument = null;
      this.mgfFiles.FileDescription = "Mascot Generic Format Files";
      this.mgfFiles.FileNames = new string[0];
      this.mgfFiles.Key = "MgfFiles";
      this.mgfFiles.Location = new System.Drawing.Point(2, 3);
      this.mgfFiles.Name = "mgfFiles";
      this.mgfFiles.SelectedIndex = -1;
      this.mgfFiles.SelectedItem = null;
      this.mgfFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.mgfFiles.Size = new System.Drawing.Size(1081, 469);
      this.mgfFiles.TabIndex = 49;
      // 
      // MultipleMgfPrecursorCorrectionProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1083, 607);
      this.Controls.Add(this.mgfFiles);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MultipleMgfPrecursorCorrectionProcessorUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.mgfFiles, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RCPA.Gui.MultipleFileField mgfFiles;
  }
}