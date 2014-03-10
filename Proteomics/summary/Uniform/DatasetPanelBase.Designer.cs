namespace RCPA.Proteomics.Summary.Uniform
{
  partial class DatasetPanelBase
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.txtDatasetName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cbFilterByPrecursorIsotopic = new System.Windows.Forms.CheckBox();
      this.txtPrecursorPPMTolerance = new System.Windows.Forms.TextBox();
      this.cbFilterByPrecursor = new System.Windows.Forms.CheckBox();
      this.cbEnabled = new System.Windows.Forms.CheckBox();
      this.cbFilterByDynamicPrecursorTolerance = new System.Windows.Forms.CheckBox();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.cbEnabled);
      this.splitContainer1.Panel1.Controls.Add(this.txtDatasetName);
      this.splitContainer1.Panel1.Controls.Add(this.label1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1016, 399);
      this.splitContainer1.SplitterDistance = 36;
      this.splitContainer1.TabIndex = 54;
      // 
      // txtDatasetName
      // 
      this.txtDatasetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDatasetName.Location = new System.Drawing.Point(133, 12);
      this.txtDatasetName.Name = "txtDatasetName";
      this.txtDatasetName.Size = new System.Drawing.Size(880, 21);
      this.txtDatasetName.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(98, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(29, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "Name";
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
      this.splitContainer2.Size = new System.Drawing.Size(1016, 359);
      this.splitContainer2.SplitterDistance = 95;
      this.splitContainer2.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbFilterByDynamicPrecursorTolerance);
      this.groupBox1.Controls.Add(this.cbFilterByPrecursorIsotopic);
      this.groupBox1.Controls.Add(this.txtPrecursorPPMTolerance);
      this.groupBox1.Controls.Add(this.cbFilterByPrecursor);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(1016, 95);
      this.groupBox1.TabIndex = 55;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Peptide criteria";
      // 
      // cbFilterByPrecursorIsotopic
      // 
      this.cbFilterByPrecursorIsotopic.AutoSize = true;
      this.cbFilterByPrecursorIsotopic.Location = new System.Drawing.Point(376, 22);
      this.cbFilterByPrecursorIsotopic.Name = "cbFilterByPrecursorIsotopic";
      this.cbFilterByPrecursorIsotopic.Size = new System.Drawing.Size(162, 16);
      this.cbFilterByPrecursorIsotopic.TabIndex = 65;
      this.cbFilterByPrecursorIsotopic.Text = "consider isotopic peaks";
      this.cbFilterByPrecursorIsotopic.UseVisualStyleBackColor = true;
      // 
      // txtPrecursorPPMTolerance
      // 
      this.txtPrecursorPPMTolerance.Location = new System.Drawing.Point(261, 20);
      this.txtPrecursorPPMTolerance.Name = "txtPrecursorPPMTolerance";
      this.txtPrecursorPPMTolerance.Size = new System.Drawing.Size(94, 21);
      this.txtPrecursorPPMTolerance.TabIndex = 64;
      // 
      // cbFilterByPrecursor
      // 
      this.cbFilterByPrecursor.AutoSize = true;
      this.cbFilterByPrecursor.Location = new System.Drawing.Point(9, 22);
      this.cbFilterByPrecursor.Name = "cbFilterByPrecursor";
      this.cbFilterByPrecursor.Size = new System.Drawing.Size(246, 16);
      this.cbFilterByPrecursor.TabIndex = 63;
      this.cbFilterByPrecursor.Text = "Filter by precursor tolerance (ppm) =";
      this.cbFilterByPrecursor.UseVisualStyleBackColor = true;
      // 
      // cbEnabled
      // 
      this.cbEnabled.AutoSize = true;
      this.cbEnabled.Checked = true;
      this.cbEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbEnabled.Location = new System.Drawing.Point(9, 14);
      this.cbEnabled.Name = "cbEnabled";
      this.cbEnabled.Size = new System.Drawing.Size(66, 16);
      this.cbEnabled.TabIndex = 2;
      this.cbEnabled.Text = "Enabled";
      this.cbEnabled.UseVisualStyleBackColor = true;
      // 
      // cbFilterByDynamicPrecursorTolerance
      // 
      this.cbFilterByDynamicPrecursorTolerance.AutoSize = true;
      this.cbFilterByDynamicPrecursorTolerance.Location = new System.Drawing.Point(544, 22);
      this.cbFilterByDynamicPrecursorTolerance.Name = "cbFilterByDynamicPrecursorTolerance";
      this.cbFilterByDynamicPrecursorTolerance.Size = new System.Drawing.Size(222, 16);
      this.cbFilterByDynamicPrecursorTolerance.TabIndex = 65;
      this.cbFilterByDynamicPrecursorTolerance.Text = "using dynamic precursor tolerance";
      this.cbFilterByDynamicPrecursorTolerance.UseVisualStyleBackColor = true;
      // 
      // DatasetPanelBase
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "DatasetPanelBase";
      this.Size = new System.Drawing.Size(1016, 399);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    protected System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox txtDatasetName;
    private System.Windows.Forms.Label label1;
    protected System.Windows.Forms.SplitContainer splitContainer2;
    protected System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtPrecursorPPMTolerance;
    private System.Windows.Forms.CheckBox cbFilterByPrecursor;
    private System.Windows.Forms.CheckBox cbFilterByPrecursorIsotopic;
    private System.Windows.Forms.CheckBox cbEnabled;
    private System.Windows.Forms.CheckBox cbFilterByDynamicPrecursorTolerance;
  }
}
