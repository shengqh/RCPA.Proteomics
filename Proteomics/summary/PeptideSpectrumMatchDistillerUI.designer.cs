namespace RCPA.Proteomics.Summary
{
  partial class PeptideSpectrumMatchDistillerUI
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
      this.peptideFile = new RCPA.Gui.FileField();
      this.cbEngines = new System.Windows.Forms.ComboBox();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 167);
      this.lblProgress.Size = new System.Drawing.Size(931, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 190);
      this.progressBar.Size = new System.Drawing.Size(931, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 213);
      this.pnlButton.Size = new System.Drawing.Size(931, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(513, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(428, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(343, 8);
      // 
      // peptideFile
      // 
      this.peptideFile.AfterBrowseFileEvent = null;
      this.peptideFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.peptideFile.FullName = "";
      this.peptideFile.Key = "PeptideFile";
      this.peptideFile.Location = new System.Drawing.Point(12, 25);
      this.peptideFile.Name = "peptideFile";
      this.peptideFile.OpenButtonText = "Browse All File ...";
      this.peptideFile.PreCondition = null;
      this.peptideFile.Size = new System.Drawing.Size(907, 23);
      this.peptideFile.TabIndex = 9;
      this.peptideFile.WidthOpenButton = 226;
      // 
      // cbEngines
      // 
      this.cbEngines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbEngines.FormattingEnabled = true;
      this.cbEngines.Location = new System.Drawing.Point(236, 81);
      this.cbEngines.Name = "cbEngines";
      this.cbEngines.Size = new System.Drawing.Size(182, 21);
      this.cbEngines.TabIndex = 10;
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(236, 54);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(683, 21);
      this.cbTitleFormat.TabIndex = 11;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(117, 84);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(113, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Select Search Engine:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(132, 57);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(98, 13);
      this.label2.TabIndex = 12;
      this.label2.Text = "Select Title Format:";
      // 
      // PeptideSpectrumMatchDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(931, 252);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbTitleFormat);
      this.Controls.Add(this.cbEngines);
      this.Controls.Add(this.peptideFile);
      this.Name = "PeptideSpectrumMatchDistillerUI";
      this.Controls.SetChildIndex(this.peptideFile, 0);
      this.Controls.SetChildIndex(this.cbEngines, 0);
      this.Controls.SetChildIndex(this.cbTitleFormat, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Gui.FileField peptideFile;
    private System.Windows.Forms.ComboBox cbEngines;
    private System.Windows.Forms.ComboBox cbTitleFormat;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;

  }
}
