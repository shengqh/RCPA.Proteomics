namespace RCPA.Tools.Sequest
{
  partial class One2AllProcessorUI
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
      this.btnTargetDirectory = new System.Windows.Forms.Button();
      this.txtTargetDirectory = new System.Windows.Forms.TextBox();
      this.cbExtractToSameDirectory = new System.Windows.Forms.CheckBox();
      this.btnLoad = new System.Windows.Forms.Button();
      this.selectDirectoryDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.lbDtaFiles = new RCPA.Gui.MultipleFileField();
      this.btnAddAll = new System.Windows.Forms.Button();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblSecondProgress
      // 
      this.lblSecondProgress.Location = new System.Drawing.Point(0, 501);
      this.lblSecondProgress.Size = new System.Drawing.Size(1006, 21);
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(31, 26);
      this.pnlFile.Size = new System.Drawing.Size(937, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(691, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 522);
      this.lblProgress.Size = new System.Drawing.Size(1006, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 543);
      this.progressBar.Size = new System.Drawing.Size(1006, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(551, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(466, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(381, 7);
      // 
      // btnTargetDirectory
      // 
      this.btnTargetDirectory.Location = new System.Drawing.Point(31, 54);
      this.btnTargetDirectory.Name = "btnTargetDirectory";
      this.btnTargetDirectory.Size = new System.Drawing.Size(198, 23);
      this.btnTargetDirectory.TabIndex = 29;
      this.btnTargetDirectory.Text = "button1";
      this.btnTargetDirectory.UseVisualStyleBackColor = true;
      // 
      // txtTargetDirectory
      // 
      this.txtTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTargetDirectory.Location = new System.Drawing.Point(235, 56);
      this.txtTargetDirectory.Name = "txtTargetDirectory";
      this.txtTargetDirectory.Size = new System.Drawing.Size(736, 21);
      this.txtTargetDirectory.TabIndex = 30;
      // 
      // cbExtractToSameDirectory
      // 
      this.cbExtractToSameDirectory.AutoSize = true;
      this.cbExtractToSameDirectory.Location = new System.Drawing.Point(31, 93);
      this.cbExtractToSameDirectory.Name = "cbExtractToSameDirectory";
      this.cbExtractToSameDirectory.Size = new System.Drawing.Size(174, 16);
      this.cbExtractToSameDirectory.TabIndex = 32;
      this.cbExtractToSameDirectory.Text = "Extract To Same Directory";
      this.cbExtractToSameDirectory.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(895, 308);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(73, 23);
      this.btnLoad.TabIndex = 36;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // selectDirectoryDialog
      // 
      this.selectDirectoryDialog.Description = "Select directory contains Sequest outs file";
      this.selectDirectoryDialog.ShowNewFolderButton = false;
      // 
      // lbDtaFiles
      // 
      this.lbDtaFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbDtaFiles.FileArgument = null;
      this.lbDtaFiles.FileDescription = "Sequest Dtas Files (All files in list will be used, no matter selected or not)";
      this.lbDtaFiles.FileNames = new string[0];
      this.lbDtaFiles.Key = "File";
      this.lbDtaFiles.Location = new System.Drawing.Point(31, 106);
      this.lbDtaFiles.Name = "lbDtaFiles";
      this.lbDtaFiles.SelectedIndex = -1;
      this.lbDtaFiles.SelectedItem = null;
      this.lbDtaFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lbDtaFiles.Size = new System.Drawing.Size(940, 344);
      this.lbDtaFiles.TabIndex = 37;
      // 
      // btnAddAll
      // 
      this.btnAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddAll.Location = new System.Drawing.Point(895, 337);
      this.btnAddAll.Name = "btnAddAll";
      this.btnAddAll.Size = new System.Drawing.Size(73, 23);
      this.btnAddAll.TabIndex = 38;
      this.btnAddAll.Text = "Add All";
      this.btnAddAll.UseVisualStyleBackColor = true;
      this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
      // 
      // One2AllProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1006, 600);
      this.Controls.Add(this.btnAddAll);
      this.Controls.Add(this.txtTargetDirectory);
      this.Controls.Add(this.btnTargetDirectory);
      this.Controls.Add(this.cbExtractToSameDirectory);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.lbDtaFiles);
      this.Name = "One2AllProcessorUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.lbDtaFiles, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.cbExtractToSameDirectory, 0);
      this.Controls.SetChildIndex(this.btnTargetDirectory, 0);
      this.Controls.SetChildIndex(this.txtTargetDirectory, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.lblSecondProgress, 0);
      this.Controls.SetChildIndex(this.btnAddAll, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnTargetDirectory;
    private System.Windows.Forms.TextBox txtTargetDirectory;
    private System.Windows.Forms.CheckBox cbExtractToSameDirectory;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.FolderBrowserDialog selectDirectoryDialog;
    private RCPA.Gui.MultipleFileField lbDtaFiles;
    private System.Windows.Forms.Button btnAddAll;
  }
}
