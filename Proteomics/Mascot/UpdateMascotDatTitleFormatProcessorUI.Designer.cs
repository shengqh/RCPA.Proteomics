namespace RCPA.Proteomics.Mascot
{
  partial class UpdateMascotDatTitleFormatProcessorUI
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
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Location = new System.Drawing.Point(24, 440);
      this.pnlFile.Size = new System.Drawing.Size(853, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(266, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(587, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(266, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 483);
      this.lblProgress.Size = new System.Drawing.Size(905, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 504);
      this.progressBar.Size = new System.Drawing.Size(905, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(500, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(415, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(330, 7);
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
      this.datFiles.Location = new System.Drawing.Point(23, 14);
      this.datFiles.Name = "datFiles";
      this.datFiles.SelectedIndex = -1;
      this.datFiles.SelectedItem = null;
      this.datFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.datFiles.Size = new System.Drawing.Size(854, 394);
      this.datFiles.TabIndex = 9;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(22, 417);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(131, 12);
      this.label7.TabIndex = 22;
      this.label7.Text = "Source title format :";
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(159, 414);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(718, 20);
      this.cbTitleFormat.TabIndex = 21;
      // 
      // UpdateMascotDatTitleFormatProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(905, 561);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.cbTitleFormat);
      this.Controls.Add(this.datFiles);
      this.Name = "UpdateMascotDatTitleFormatProcessorUI";
      this.TabText = "MascotGenericFormatSplitterUI";
      this.Text = "MascotGenericFormatSplitterUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.datFiles, 0);
      this.Controls.SetChildIndex(this.cbTitleFormat, 0);
      this.Controls.SetChildIndex(this.label7, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RCPA.Gui.MultipleFileField datFiles;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cbTitleFormat;

  }
}