namespace RCPA.Proteomics.PeptideProphet
{
  partial class PeptideProphetDatasetPanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.xmlFiles = new RCPA.Gui.MultipleFileField();
      this.cbFilterByPValue = new System.Windows.Forms.CheckBox();
      this.txtMinProbabilityValue = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer3
      // 
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.xmlFiles);
      this.splitContainer3.Size = new System.Drawing.Size(1016, 262);
      this.splitContainer3.SplitterDistance = 194;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.Location = new System.Drawing.Point(8, 4);
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.Location = new System.Drawing.Point(106, 1);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Size = new System.Drawing.Size(1016, 361);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbFilterByPValue);
      this.groupBox1.Controls.Add(this.txtMinProbabilityValue);
      this.groupBox1.Controls.SetChildIndex(this.txtMinProbabilityValue, 0);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByPValue, 0);
      // 
      // xmlFiles
      // 
      this.xmlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.xmlFiles.FileArgument = null;
      this.xmlFiles.FileDescription = "pepXml Files";
      this.xmlFiles.FileNames = new string[0];
      this.xmlFiles.Key = "File";
      this.xmlFiles.Location = new System.Drawing.Point(0, 0);
      this.xmlFiles.Name = "xmlFiles";
      this.xmlFiles.SelectedIndex = -1;
      this.xmlFiles.SelectedItem = null;
      this.xmlFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.xmlFiles.Size = new System.Drawing.Size(1016, 194);
      this.xmlFiles.TabIndex = 0;
      // 
      // cbFilterByPValue
      // 
      this.cbFilterByPValue.AutoSize = true;
      this.cbFilterByPValue.Location = new System.Drawing.Point(9, 48);
      this.cbFilterByPValue.Name = "cbFilterByPValue";
      this.cbFilterByPValue.Size = new System.Drawing.Size(127, 17);
      this.cbFilterByPValue.TabIndex = 66;
      this.cbFilterByPValue.Text = "Filter by probability >=";
      this.cbFilterByPValue.UseVisualStyleBackColor = true;
      // 
      // txtMinProbabilityValue
      // 
      this.txtMinProbabilityValue.Location = new System.Drawing.Point(183, 46);
      this.txtMinProbabilityValue.Name = "txtMinProbabilityValue";
      this.txtMinProbabilityValue.Size = new System.Drawing.Size(100, 20);
      this.txtMinProbabilityValue.TabIndex = 67;
      // 
      // PeptideProphetDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Name = "PeptideProphetDatasetPanel";
      this.Size = new System.Drawing.Size(1016, 399);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private RCPA.Gui.MultipleFileField xmlFiles;
    private System.Windows.Forms.CheckBox cbFilterByPValue;
    private System.Windows.Forms.TextBox txtMinProbabilityValue;

  }
}
