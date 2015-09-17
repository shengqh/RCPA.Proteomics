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
      this.pnlName = new System.Windows.Forms.Panel();
      this.cbEnabled = new System.Windows.Forms.CheckBox();
      this.txtDatasetName = new System.Windows.Forms.TextBox();
      this.lblName = new System.Windows.Forms.Label();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbScoreFunctions = new System.Windows.Forms.ComboBox();
      this.cbFilterByDynamicPrecursorTolerance = new System.Windows.Forms.CheckBox();
      this.cbFilterByPrecursorIsotopic = new System.Windows.Forms.CheckBox();
      this.txtPrecursorPPMTolerance = new System.Windows.Forms.TextBox();
      this.cbFilterByPrecursor = new System.Windows.Forms.CheckBox();
      this.cbSearchedByDifferentParameters = new System.Windows.Forms.CheckBox();
      this.pnlName.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlName
      // 
      this.pnlName.Controls.Add(this.cbEnabled);
      this.pnlName.Controls.Add(this.txtDatasetName);
      this.pnlName.Controls.Add(this.lblName);
      this.pnlName.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlName.Location = new System.Drawing.Point(0, 0);
      this.pnlName.Name = "pnlName";
      this.pnlName.Size = new System.Drawing.Size(1016, 38);
      this.pnlName.TabIndex = 55;
      // 
      // cbEnabled
      // 
      this.cbEnabled.AutoSize = true;
      this.cbEnabled.Checked = true;
      this.cbEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbEnabled.Location = new System.Drawing.Point(6, 11);
      this.cbEnabled.Name = "cbEnabled";
      this.cbEnabled.Size = new System.Drawing.Size(65, 17);
      this.cbEnabled.TabIndex = 5;
      this.cbEnabled.Text = "Enabled";
      this.cbEnabled.UseVisualStyleBackColor = true;
      // 
      // txtDatasetName
      // 
      this.txtDatasetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDatasetName.Location = new System.Drawing.Point(130, 9);
      this.txtDatasetName.Name = "txtDatasetName";
      this.txtDatasetName.Size = new System.Drawing.Size(880, 20);
      this.txtDatasetName.TabIndex = 4;
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(92, 12);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(35, 13);
      this.lblName.TabIndex = 3;
      this.lblName.Text = "Name";
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 38);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
      this.splitContainer2.Size = new System.Drawing.Size(1016, 394);
      this.splitContainer2.SplitterDistance = 95;
      this.splitContainer2.TabIndex = 56;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.cbScoreFunctions);
      this.groupBox1.Controls.Add(this.cbSearchedByDifferentParameters);
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
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(625, 25);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(163, 13);
      this.label1.TabIndex = 67;
      this.label1.Text = "Select score for FDR calculation:";
      // 
      // cbScoreFunctions
      // 
      this.cbScoreFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbScoreFunctions.FormattingEnabled = true;
      this.cbScoreFunctions.Location = new System.Drawing.Point(788, 22);
      this.cbScoreFunctions.Name = "cbScoreFunctions";
      this.cbScoreFunctions.Size = new System.Drawing.Size(179, 21);
      this.cbScoreFunctions.TabIndex = 66;
      // 
      // cbFilterByDynamicPrecursorTolerance
      // 
      this.cbFilterByDynamicPrecursorTolerance.AutoSize = true;
      this.cbFilterByDynamicPrecursorTolerance.Location = new System.Drawing.Point(396, 24);
      this.cbFilterByDynamicPrecursorTolerance.Name = "cbFilterByDynamicPrecursorTolerance";
      this.cbFilterByDynamicPrecursorTolerance.Size = new System.Drawing.Size(187, 17);
      this.cbFilterByDynamicPrecursorTolerance.TabIndex = 65;
      this.cbFilterByDynamicPrecursorTolerance.Text = "using dynamic precursor tolerance";
      this.cbFilterByDynamicPrecursorTolerance.UseVisualStyleBackColor = true;
      // 
      // cbFilterByPrecursorIsotopic
      // 
      this.cbFilterByPrecursorIsotopic.AutoSize = true;
      this.cbFilterByPrecursorIsotopic.Location = new System.Drawing.Point(263, 24);
      this.cbFilterByPrecursorIsotopic.Name = "cbFilterByPrecursorIsotopic";
      this.cbFilterByPrecursorIsotopic.Size = new System.Drawing.Size(137, 17);
      this.cbFilterByPrecursorIsotopic.TabIndex = 65;
      this.cbFilterByPrecursorIsotopic.Text = "consider isotopic peaks";
      this.cbFilterByPrecursorIsotopic.UseVisualStyleBackColor = true;
      // 
      // txtPrecursorPPMTolerance
      // 
      this.txtPrecursorPPMTolerance.Location = new System.Drawing.Point(207, 22);
      this.txtPrecursorPPMTolerance.Name = "txtPrecursorPPMTolerance";
      this.txtPrecursorPPMTolerance.Size = new System.Drawing.Size(50, 20);
      this.txtPrecursorPPMTolerance.TabIndex = 64;
      // 
      // cbFilterByPrecursor
      // 
      this.cbFilterByPrecursor.AutoSize = true;
      this.cbFilterByPrecursor.Location = new System.Drawing.Point(9, 24);
      this.cbFilterByPrecursor.Name = "cbFilterByPrecursor";
      this.cbFilterByPrecursor.Size = new System.Drawing.Size(194, 17);
      this.cbFilterByPrecursor.TabIndex = 63;
      this.cbFilterByPrecursor.Text = "Filter by precursor tolerance (ppm) =";
      this.cbFilterByPrecursor.UseVisualStyleBackColor = true;
      // 
      // cbSearchedByDifferentParameters
      // 
      this.cbSearchedByDifferentParameters.AutoSize = true;
      this.cbSearchedByDifferentParameters.Location = new System.Drawing.Point(628, 59);
      this.cbSearchedByDifferentParameters.Name = "cbSearchedByDifferentParameters";
      this.cbSearchedByDifferentParameters.Size = new System.Drawing.Size(340, 17);
      this.cbSearchedByDifferentParameters.TabIndex = 65;
      this.cbSearchedByDifferentParameters.Text = "Containing result from same sample but different search parameters";
      this.cbSearchedByDifferentParameters.UseVisualStyleBackColor = true;
      // 
      // DatasetPanelBase
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer2);
      this.Controls.Add(this.pnlName);
      this.Name = "DatasetPanelBase";
      this.Size = new System.Drawing.Size(1016, 432);
      this.pnlName.ResumeLayout(false);
      this.pnlName.PerformLayout();
      this.splitContainer2.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlName;
    private System.Windows.Forms.CheckBox cbEnabled;
    private System.Windows.Forms.TextBox txtDatasetName;
    private System.Windows.Forms.Label lblName;
    protected System.Windows.Forms.SplitContainer splitContainer2;
    protected System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox cbFilterByDynamicPrecursorTolerance;
    private System.Windows.Forms.CheckBox cbFilterByPrecursorIsotopic;
    private System.Windows.Forms.TextBox txtPrecursorPPMTolerance;
    private System.Windows.Forms.CheckBox cbFilterByPrecursor;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbScoreFunctions;
    private System.Windows.Forms.CheckBox cbSearchedByDifferentParameters;

  }
}
