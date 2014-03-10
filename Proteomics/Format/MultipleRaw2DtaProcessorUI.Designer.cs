namespace RCPA.Proteomics.Format
{
  partial class MultipleRaw2DtaProcessorUI
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cbMsLevel = new RCPA.Gui.RcpaCheckField();
      this.cbParallel = new RCPA.Gui.RcpaCheckField();
      this.txtMinIonIntensity = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.txtMWRangeTo = new System.Windows.Forms.TextBox();
      this.txtMinIonCount = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtMinIonIntensityThreshold = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtMWRangeFrom = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.pnlFile.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlFile.Location = new System.Drawing.Point(0, 282);
      this.pnlFile.Size = new System.Drawing.Size(897, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(651, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 468);
      this.lblProgress.Size = new System.Drawing.Size(897, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 489);
      this.progressBar.Size = new System.Drawing.Size(897, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(496, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(411, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(326, 7);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbMsLevel);
      this.groupBox1.Controls.Add(this.cbParallel);
      this.groupBox1.Controls.Add(this.txtMinIonIntensity);
      this.groupBox1.Controls.Add(this.label9);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.txtMWRangeTo);
      this.groupBox1.Controls.Add(this.txtMinIonCount);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.txtMinIonIntensityThreshold);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.txtMWRangeFrom);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.groupBox1.Location = new System.Drawing.Point(0, 304);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(897, 164);
      this.groupBox1.TabIndex = 23;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Extract Peaks Parameter";
      // 
      // cbMsLevel
      // 
      this.cbMsLevel.Key = "SplitMsLevel";
      this.cbMsLevel.Location = new System.Drawing.Point(254, 128);
      this.cbMsLevel.Name = "cbMsLevel";
      this.cbMsLevel.PreCondition = null;
      this.cbMsLevel.Size = new System.Drawing.Size(287, 30);
      this.cbMsLevel.TabIndex = 67;
      this.cbMsLevel.Text = "Extract MS2/MS3 to different directory";
      // 
      // cbParallel
      // 
      this.cbParallel.Key = "ParellelMode";
      this.cbParallel.Location = new System.Drawing.Point(568, 128);
      this.cbParallel.Name = "cbParallel";
      this.cbParallel.PreCondition = null;
      this.cbParallel.Size = new System.Drawing.Size(136, 30);
      this.cbParallel.TabIndex = 66;
      this.cbParallel.Text = "Parallel Mode";
      // 
      // txtMinIonIntensity
      // 
      this.txtMinIonIntensity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinIonIntensity.Location = new System.Drawing.Point(254, 50);
      this.txtMinIonIntensity.Name = "txtMinIonIntensity";
      this.txtMinIonIntensity.Size = new System.Drawing.Size(619, 21);
      this.txtMinIonIntensity.TabIndex = 46;
      this.txtMinIonIntensity.Text = "1.0";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(108, 53);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(131, 12);
      this.label9.TabIndex = 45;
      this.label9.Text = "Minimum Ion Intensity";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(438, 25);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(17, 12);
      this.label5.TabIndex = 34;
      this.label5.Text = "--";
      // 
      // txtMWRangeTo
      // 
      this.txtMWRangeTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMWRangeTo.Location = new System.Drawing.Point(465, 22);
      this.txtMWRangeTo.Name = "txtMWRangeTo";
      this.txtMWRangeTo.Size = new System.Drawing.Size(408, 21);
      this.txtMWRangeTo.TabIndex = 33;
      this.txtMWRangeTo.Text = "3500.00";
      // 
      // txtMinIonCount
      // 
      this.txtMinIonCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinIonCount.Location = new System.Drawing.Point(254, 78);
      this.txtMinIonCount.Name = "txtMinIonCount";
      this.txtMinIonCount.Size = new System.Drawing.Size(619, 21);
      this.txtMinIonCount.TabIndex = 30;
      this.txtMinIonCount.Text = "5";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(132, 81);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(107, 12);
      this.label4.TabIndex = 29;
      this.label4.Text = "Minimum Ion Count";
      // 
      // txtMinIonIntensityThreshold
      // 
      this.txtMinIonIntensityThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinIonIntensityThreshold.Location = new System.Drawing.Point(254, 105);
      this.txtMinIonIntensityThreshold.Name = "txtMinIonIntensityThreshold";
      this.txtMinIonIntensityThreshold.Size = new System.Drawing.Size(619, 21);
      this.txtMinIonIntensityThreshold.TabIndex = 26;
      this.txtMinIonIntensityThreshold.Text = "100";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 108);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(233, 12);
      this.label3.TabIndex = 25;
      this.label3.Text = "Absolute Total Ion Intensity Threshold";
      // 
      // txtMWRangeFrom
      // 
      this.txtMWRangeFrom.Location = new System.Drawing.Point(254, 22);
      this.txtMWRangeFrom.Name = "txtMWRangeFrom";
      this.txtMWRangeFrom.Size = new System.Drawing.Size(178, 21);
      this.txtMWRangeFrom.TabIndex = 22;
      this.txtMWRangeFrom.Text = "600.00";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(72, 25);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(167, 12);
      this.label2.TabIndex = 20;
      this.label2.Text = "Precursor Mass Weight Range";
      // 
      // rawFiles
      // 
      this.rawFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.rawFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "Raw/mzData/mzXml files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "RawFiles";
      this.rawFiles.Location = new System.Drawing.Point(0, 0);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(897, 282);
      this.rawFiles.TabIndex = 50;
      // 
      // MultipleRaw2DtaProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(897, 546);
      this.Controls.Add(this.rawFiles);
      this.Controls.Add(this.groupBox1);
      this.Name = "MultipleRaw2DtaProcessorUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtMWRangeTo;
    private System.Windows.Forms.TextBox txtMinIonCount;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtMinIonIntensityThreshold;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtMWRangeFrom;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMinIonIntensity;
    private System.Windows.Forms.Label label9;
    private Gui.MultipleFileField rawFiles;
    private Gui.RcpaCheckField cbMsLevel;
    private Gui.RcpaCheckField cbParallel;
  }
}