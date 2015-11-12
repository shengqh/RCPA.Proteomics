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
      this.cbEngines = new System.Windows.Forms.ComboBox();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.searchResultFiles = new RCPA.Gui.MultipleFileField();
      this.rbRank2 = new RCPA.Gui.RcpaCheckField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 507);
      this.lblProgress.Size = new System.Drawing.Size(931, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 530);
      this.progressBar.Size = new System.Drawing.Size(931, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 553);
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
      // cbEngines
      // 
      this.cbEngines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cbEngines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbEngines.FormattingEnabled = true;
      this.cbEngines.Location = new System.Drawing.Point(128, 447);
      this.cbEngines.Name = "cbEngines";
      this.cbEngines.Size = new System.Drawing.Size(182, 21);
      this.cbEngines.TabIndex = 10;
      this.cbEngines.SelectedIndexChanged += new System.EventHandler(this.cbEngines_SelectedIndexChanged);
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(128, 474);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(791, 21);
      this.cbTitleFormat.TabIndex = 11;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 450);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(113, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Select Search Engine:";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 477);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(98, 13);
      this.label2.TabIndex = 12;
      this.label2.Text = "Select Title Format:";
      // 
      // searchResultFiles
      // 
      this.searchResultFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.searchResultFiles.FileArgument = null;
      this.searchResultFiles.FileDescription = "Search Result Files";
      this.searchResultFiles.FileNames = new string[0];
      this.searchResultFiles.Key = "File";
      this.searchResultFiles.Location = new System.Drawing.Point(12, 12);
      this.searchResultFiles.Name = "searchResultFiles";
      this.searchResultFiles.SelectedIndex = -1;
      this.searchResultFiles.SelectedItem = null;
      this.searchResultFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.searchResultFiles.Size = new System.Drawing.Size(907, 425);
      this.searchResultFiles.TabIndex = 13;
      // 
      // rbRank2
      // 
      this.rbRank2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.rbRank2.AutoSize = true;
      this.rbRank2.Key = "Rank2";
      this.rbRank2.Location = new System.Drawing.Point(343, 449);
      this.rbRank2.Name = "rbRank2";
      this.rbRank2.PreCondition = null;
      this.rbRank2.Size = new System.Drawing.Size(150, 17);
      this.rbRank2.TabIndex = 14;
      this.rbRank2.Text = "Extract Rank2 Candidates";
      this.rbRank2.UseVisualStyleBackColor = true;
      // 
      // PeptideSpectrumMatchDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(931, 592);
      this.Controls.Add(this.rbRank2);
      this.Controls.Add(this.searchResultFiles);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbTitleFormat);
      this.Controls.Add(this.cbEngines);
      this.Name = "PeptideSpectrumMatchDistillerUI";
      this.Controls.SetChildIndex(this.cbEngines, 0);
      this.Controls.SetChildIndex(this.cbTitleFormat, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.searchResultFiles, 0);
      this.Controls.SetChildIndex(this.rbRank2, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cbEngines;
    private System.Windows.Forms.ComboBox cbTitleFormat;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private Gui.MultipleFileField searchResultFiles;
    private Gui.RcpaCheckField rbRank2;

  }
}
