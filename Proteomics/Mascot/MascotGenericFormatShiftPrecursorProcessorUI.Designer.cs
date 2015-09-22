namespace RCPA.Proteomics.Mascot
{
  partial class MascotGenericFormatShiftPrecursorProcessorUI
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
      this.datFiles = new RCPA.Gui.MultipleFileField();
      this.label7 = new System.Windows.Forms.Label();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.shiftMass = new RCPA.Gui.DoubleField();
      this.targetDirectory = new RCPA.Gui.DirectoryField();
      this.shiftScan = new RCPA.Gui.IntegerField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 519);
      this.lblProgress.Size = new System.Drawing.Size(957, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 542);
      this.progressBar.Size = new System.Drawing.Size(957, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 565);
      this.pnlButton.Size = new System.Drawing.Size(957, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(526, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(441, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(356, 8);
      // 
      // datFiles
      // 
      this.datFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.datFiles.FileArgument = null;
      this.datFiles.FileDescription = "Import mascot mgf files";
      this.datFiles.FileNames = new string[0];
      this.datFiles.Key = "File";
      this.datFiles.Location = new System.Drawing.Point(26, 24);
      this.datFiles.Name = "datFiles";
      this.datFiles.SelectedIndex = -1;
      this.datFiles.SelectedItem = null;
      this.datFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.datFiles.Size = new System.Drawing.Size(906, 395);
      this.datFiles.TabIndex = 9;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(25, 428);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(98, 13);
      this.label7.TabIndex = 22;
      this.label7.Text = "Source title format :";
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(129, 425);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(803, 21);
      this.cbTitleFormat.TabIndex = 21;
      // 
      // shiftMass
      // 
      this.shiftMass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.shiftMass.Caption = "Input shift mass to precursor : ";
      this.shiftMass.CaptionWidth = 193;
      this.shiftMass.DefaultValue = "-10";
      this.shiftMass.Description = "";
      this.shiftMass.Key = "DeltaMass";
      this.shiftMass.Location = new System.Drawing.Point(12, 452);
      this.shiftMass.Name = "shiftMass";
      this.shiftMass.PreCondition = null;
      this.shiftMass.Required = false;
      this.shiftMass.Size = new System.Drawing.Size(277, 23);
      this.shiftMass.TabIndex = 32;
      this.shiftMass.TextWidth = 84;
      this.shiftMass.Value = -10D;
      // 
      // targetDirectory
      // 
      this.targetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.targetDirectory.FullName = "";
      this.targetDirectory.Key = "TargetDirectory";
      this.targetDirectory.Location = new System.Drawing.Point(26, 481);
      this.targetDirectory.Name = "targetDirectory";
      this.targetDirectory.OpenButtonText = "Browse Target Directory ...";
      this.targetDirectory.OpenButtonWidth = 226;
      this.targetDirectory.PreCondition = null;
      this.targetDirectory.Required = false;
      this.targetDirectory.Size = new System.Drawing.Size(906, 23);
      this.targetDirectory.TabIndex = 33;
      // 
      // shiftScan
      // 
      this.shiftScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.shiftScan.Caption = "Input shift scan : ";
      this.shiftScan.CaptionWidth = 193;
      this.shiftScan.DefaultValue = "1000000";
      this.shiftScan.Description = "";
      this.shiftScan.Key = "DeltaScan";
      this.shiftScan.Location = new System.Drawing.Point(295, 452);
      this.shiftScan.Name = "shiftScan";
      this.shiftScan.PreCondition = null;
      this.shiftScan.Required = false;
      this.shiftScan.Size = new System.Drawing.Size(296, 23);
      this.shiftScan.TabIndex = 34;
      this.shiftScan.TextWidth = 103;
      this.shiftScan.Value = 1000000;
      // 
      // MascotGenericFormatShiftPrecursorProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(957, 604);
      this.Controls.Add(this.shiftScan);
      this.Controls.Add(this.targetDirectory);
      this.Controls.Add(this.shiftMass);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.cbTitleFormat);
      this.Controls.Add(this.datFiles);
      this.Name = "MascotGenericFormatShiftPrecursorProcessorUI";
      this.TabText = "MascotGenericFormatSplitterUI";
      this.Text = "MascotGenericFormatSplitterUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.datFiles, 0);
      this.Controls.SetChildIndex(this.cbTitleFormat, 0);
      this.Controls.SetChildIndex(this.label7, 0);
      this.Controls.SetChildIndex(this.shiftMass, 0);
      this.Controls.SetChildIndex(this.targetDirectory, 0);
      this.Controls.SetChildIndex(this.shiftScan, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RCPA.Gui.MultipleFileField datFiles;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cbTitleFormat;
    private Gui.DoubleField shiftMass;
    private Gui.DirectoryField targetDirectory;
    private Gui.IntegerField shiftScan;

  }
}