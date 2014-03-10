namespace RCPA.Proteomics.Format
{
  partial class MergeMgfFileProcessorUI
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
      this.sourceFiles = new RCPA.Gui.MultipleFileField();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlFile.Location = new System.Drawing.Point(0, 333);
      this.pnlFile.Size = new System.Drawing.Size(926, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(680, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 355);
      this.lblProgress.Size = new System.Drawing.Size(926, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 376);
      this.progressBar.Size = new System.Drawing.Size(926, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(511, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(426, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(341, 7);
      // 
      // sourceFiles
      // 
      this.sourceFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sourceFiles.FileArgument = null;
      this.sourceFiles.FileDescription = "Source Mascot Generic Format Files";
      this.sourceFiles.FileNames = new string[0];
      this.sourceFiles.Key = "File";
      this.sourceFiles.Location = new System.Drawing.Point(0, 0);
      this.sourceFiles.Name = "sourceFiles";
      this.sourceFiles.SelectedIndex = -1;
      this.sourceFiles.SelectedItem = null;
      this.sourceFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.sourceFiles.Size = new System.Drawing.Size(926, 333);
      this.sourceFiles.TabIndex = 9;
      // 
      // MergeMgfFileProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(926, 433);
      this.Controls.Add(this.sourceFiles);
      this.Name = "MergeMgfFileProcessorUI";
      this.TabText = "MergeMgfFileProcessorUI";
      this.Text = "MergeMgfFileProcessorUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.sourceFiles, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.MultipleFileField sourceFiles;
  }
}