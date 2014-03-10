namespace RCPA.Proteomics.Summary.Uniform
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
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
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
      this.splitContainer3.Size = new System.Drawing.Size(1016, 254);
      this.splitContainer3.SplitterDistance = 224;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Size = new System.Drawing.Size(1016, 368);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Size = new System.Drawing.Size(1016, 328);
      this.splitContainer2.SplitterDistance = 70;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbFilterByPValue);
      this.groupBox1.Controls.Add(this.txtMinProbabilityValue);
      this.groupBox1.Size = new System.Drawing.Size(1016, 70);
      this.groupBox1.Controls.SetChildIndex(this.txtMinProbabilityValue, 0);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByPValue, 0);
      // 
      // xmlFiles
      // 
      this.xmlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.xmlFiles.FileArgument = null;
      this.xmlFiles.FileDescription = "PeptidePhophet Xml Files";
      this.xmlFiles.FileNames = new string[0];
      this.xmlFiles.Location = new System.Drawing.Point(0, 0);
      this.xmlFiles.Name = "xmlFiles";
      this.xmlFiles.SelectedIndex = -1;
      this.xmlFiles.SelectedItem = null;
      this.xmlFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.xmlFiles.Size = new System.Drawing.Size(1016, 224);
      this.xmlFiles.TabIndex = 0;
      // 
      // cbFilterByPValue
      // 
      this.cbFilterByPValue.AutoSize = true;
      this.cbFilterByPValue.Location = new System.Drawing.Point(9, 44);
      this.cbFilterByPValue.Name = "cbFilterByPValue";
      this.cbFilterByPValue.Size = new System.Drawing.Size(168, 16);
      this.cbFilterByPValue.TabIndex = 66;
      this.cbFilterByPValue.Text = "Filter by probability >=";
      this.cbFilterByPValue.UseVisualStyleBackColor = true;
      // 
      // txtMinProbabilityValue
      // 
      this.txtMinProbabilityValue.Location = new System.Drawing.Point(183, 42);
      this.txtMinProbabilityValue.Name = "txtMinProbabilityValue";
      this.txtMinProbabilityValue.Size = new System.Drawing.Size(100, 21);
      this.txtMinProbabilityValue.TabIndex = 67;
      // 
      // PeptideProphetDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.Name = "PeptideProphetDatasetPanel";
      this.Size = new System.Drawing.Size(1016, 368);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      this.splitContainer3.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
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
