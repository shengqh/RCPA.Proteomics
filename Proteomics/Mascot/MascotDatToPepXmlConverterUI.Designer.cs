namespace RCPA.Proteomics.Mascot
{
  partial class MascotDatToPepXmlConverterUI
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
      this.label7 = new System.Windows.Forms.Label();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.datFiles = new RCPA.Gui.MultipleFileField();
      this.targetDir = new RCPA.Gui.DirectoryField();
      this.minPeptideLength = new RCPA.Gui.IntegerField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 544);
      this.lblProgress.Size = new System.Drawing.Size(984, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 567);
      this.progressBar.Size = new System.Drawing.Size(984, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 590);
      this.pnlButton.Size = new System.Drawing.Size(984, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(540, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(455, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(370, 8);
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(133, 485);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(98, 13);
      this.label7.TabIndex = 25;
      this.label7.Text = "Source title format :";
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(237, 482);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(733, 21);
      this.cbTitleFormat.TabIndex = 24;
      // 
      // datFiles
      // 
      this.datFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.datFiles.FileArgument = null;
      this.datFiles.FileDescription = "Import mascot dat files";
      this.datFiles.FileNames = new string[0];
      this.datFiles.Key = "File";
      this.datFiles.Location = new System.Drawing.Point(12, 21);
      this.datFiles.Name = "datFiles";
      this.datFiles.SelectedIndex = -1;
      this.datFiles.SelectedItem = null;
      this.datFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.datFiles.Size = new System.Drawing.Size(960, 426);
      this.datFiles.TabIndex = 23;
      // 
      // targetDir
      // 
      this.targetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.targetDir.FullName = "";
      this.targetDir.Key = "DirectoryField";
      this.targetDir.Location = new System.Drawing.Point(12, 453);
      this.targetDir.Name = "targetDir";
      this.targetDir.OpenButtonText = "Browse  Directory ...";
      this.targetDir.OpenButtonWidth = 226;
      this.targetDir.PreCondition = null;
      this.targetDir.Size = new System.Drawing.Size(958, 23);
      this.targetDir.TabIndex = 26;
      // 
      // minPeptideLength
      // 
      this.minPeptideLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.minPeptideLength.Caption = "Minimum peptide length :  ";
      this.minPeptideLength.CaptionWidth = 150;
      this.minPeptideLength.DefaultValue = "7";
      this.minPeptideLength.Description = "";
      this.minPeptideLength.Key = "MinLength";
      this.minPeptideLength.Location = new System.Drawing.Point(86, 509);
      this.minPeptideLength.Name = "minPeptideLength";
      this.minPeptideLength.PreCondition = null;
      this.minPeptideLength.Size = new System.Drawing.Size(222, 23);
      this.minPeptideLength.TabIndex = 27;
      this.minPeptideLength.TextWidth = 53;
      this.minPeptideLength.Value = 7;
      // 
      // MascotDatToPepXmlConverterUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(984, 629);
      this.Controls.Add(this.minPeptideLength);
      this.Controls.Add(this.targetDir);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.cbTitleFormat);
      this.Controls.Add(this.datFiles);
      this.Name = "MascotDatToPepXmlConverterUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.datFiles, 0);
      this.Controls.SetChildIndex(this.cbTitleFormat, 0);
      this.Controls.SetChildIndex(this.label7, 0);
      this.Controls.SetChildIndex(this.targetDir, 0);
      this.Controls.SetChildIndex(this.minPeptideLength, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cbTitleFormat;
    private Gui.MultipleFileField datFiles;
    private Gui.DirectoryField targetDir;
    private Gui.IntegerField minPeptideLength;
  }
}