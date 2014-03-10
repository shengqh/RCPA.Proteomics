namespace RCPA.Proteomics.Format
{
  partial class MaxQuantToMgfProcessorUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaxQuantToMgfProcessorUI));
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.cbMerge = new System.Windows.Forms.CheckBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlFile.Location = new System.Drawing.Point(0, 388);
      this.pnlFile.Size = new System.Drawing.Size(897, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(651, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 410);
      this.lblProgress.Size = new System.Drawing.Size(897, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 431);
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
      this.rawFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.rawFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "MaxQuant apl files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "File";
      this.rawFiles.Location = new System.Drawing.Point(0, 0);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(897, 372);
      this.rawFiles.TabIndex = 49;
      // 
      // cbMerge
      // 
      this.cbMerge.AutoSize = true;
      this.cbMerge.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.cbMerge.Location = new System.Drawing.Point(0, 372);
      this.cbMerge.Name = "cbMerge";
      this.cbMerge.Size = new System.Drawing.Size(897, 16);
      this.cbMerge.TabIndex = 50;
      this.cbMerge.Text = "Merge to single MGF file";
      this.cbMerge.UseVisualStyleBackColor = true;
      this.cbMerge.CheckedChanged += new System.EventHandler(this.cbMerge_CheckedChanged);
      // 
      // MaxQuantToMgfProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(897, 488);
      this.Controls.Add(this.rawFiles);
      this.Controls.Add(this.cbMerge);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MaxQuantToMgfProcessorUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.cbMerge, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RCPA.Gui.MultipleFileField rawFiles;
    private System.Windows.Forms.CheckBox cbMerge;
  }
}