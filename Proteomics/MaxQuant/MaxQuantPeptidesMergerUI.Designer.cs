namespace RCPA.Proteomics.MaxQuant
{
  partial class MaxQuantPeptidesMergerUI
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
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.cbFilterByScore = new System.Windows.Forms.CheckBox();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.peptideFiles = new RCPA.Gui.MultipleFileField();
      this.peptideFileBin = new RCPA.Proteomics.ClassificationPanel();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 486);
      this.lblProgress.Size = new System.Drawing.Size(1027, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 507);
      this.progressBar.Size = new System.Drawing.Size(1027, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(561, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(476, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(391, 7);
      // 
      // txtMaxEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(185, 165);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 21);
      this.txtMaxEvalue.TabIndex = 47;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(14, 167);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(174, 16);
      this.cbFilterByEvalue.TabIndex = 46;
      this.cbFilterByEvalue.Text = "Filter by Expect value = ";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // txtMinScore
      // 
      this.txtMinScore.Location = new System.Drawing.Point(142, 141);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(94, 21);
      this.txtMinScore.TabIndex = 45;
      // 
      // cbFilterByScore
      // 
      this.cbFilterByScore.AutoSize = true;
      this.cbFilterByScore.Location = new System.Drawing.Point(14, 143);
      this.cbFilterByScore.Name = "cbFilterByScore";
      this.cbFilterByScore.Size = new System.Drawing.Size(132, 16);
      this.cbFilterByScore.TabIndex = 44;
      this.cbFilterByScore.Text = "Filter by Score = ";
      this.cbFilterByScore.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Location = new System.Drawing.Point(13, 12);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.peptideFiles);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.peptideFileBin);
      this.splitContainer1.Size = new System.Drawing.Size(1002, 438);
      this.splitContainer1.SplitterDistance = 219;
      this.splitContainer1.TabIndex = 10;
      // 
      // peptideFiles
      // 
      this.peptideFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.peptideFiles.FileArgument = null;
      this.peptideFiles.FileDescription = "Import identified peptide files";
      this.peptideFiles.FileNames = new string[0];
      this.peptideFiles.Key = "File";
      this.peptideFiles.Location = new System.Drawing.Point(0, 0);
      this.peptideFiles.Name = "peptideFiles";
      this.peptideFiles.SelectedIndex = -1;
      this.peptideFiles.SelectedItem = null;
      this.peptideFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.peptideFiles.Size = new System.Drawing.Size(1002, 219);
      this.peptideFiles.TabIndex = 8;
      // 
      // peptideFileBin
      // 
      this.peptideFileBin.Description = "Description";
      this.peptideFileBin.Dock = System.Windows.Forms.DockStyle.Fill;
      this.peptideFileBin.GetName = null;
      this.peptideFileBin.Location = new System.Drawing.Point(0, 0);
      this.peptideFileBin.Name = "peptideFileBin";
      this.peptideFileBin.Pattern = "(.*)";
      this.peptideFileBin.Size = new System.Drawing.Size(1002, 215);
      this.peptideFileBin.TabIndex = 10;
      this.peptideFileBin.GetData += new RCPA.Proteomics.GetDataEventHandler(this.peptideFileBin_GetData);
      // 
      // MaxQuantPeptidesMergerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1027, 564);
      this.Controls.Add(this.splitContainer1);
      this.Name = "MaxQuantPeptidesMergerUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.splitContainer1, 0);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.CheckBox cbFilterByScore;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private RCPA.Gui.MultipleFileField peptideFiles;
    private ClassificationPanel peptideFileBin;
  }
}
