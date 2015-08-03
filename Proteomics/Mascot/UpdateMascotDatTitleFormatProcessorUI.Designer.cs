﻿namespace RCPA.Proteomics.Mascot
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
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Location = new System.Drawing.Point(24, 477);
      this.pnlFile.Size = new System.Drawing.Size(853, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(266, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(587, 20);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(266, 24);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 523);
      this.lblProgress.Size = new System.Drawing.Size(905, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 546);
      this.progressBar.Size = new System.Drawing.Size(905, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 569);
      this.pnlButton.Size = new System.Drawing.Size(905, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(500, 9);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(415, 9);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(330, 9);
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
      this.datFiles.Location = new System.Drawing.Point(23, 15);
      this.datFiles.Name = "datFiles";
      this.datFiles.SelectedIndex = -1;
      this.datFiles.SelectedItem = null;
      this.datFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.datFiles.Size = new System.Drawing.Size(854, 427);
      this.datFiles.TabIndex = 9;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(22, 452);
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
      this.cbTitleFormat.Location = new System.Drawing.Point(159, 449);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(718, 21);
      this.cbTitleFormat.TabIndex = 21;
      // 
      // UpdateMascotDatTitleFormatProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(905, 608);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.cbTitleFormat);
      this.Controls.Add(this.datFiles);
      this.Name = "UpdateMascotDatTitleFormatProcessorUI";
      this.TabText = "MascotGenericFormatSplitterUI";
      this.Text = "MascotGenericFormatSplitterUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.datFiles, 0);
      this.Controls.SetChildIndex(this.cbTitleFormat, 0);
      this.Controls.SetChildIndex(this.label7, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RCPA.Gui.MultipleFileField datFiles;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cbTitleFormat;

  }
}