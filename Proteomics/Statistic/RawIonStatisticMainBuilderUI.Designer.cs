namespace RCPA.Proteomics.Statistic
{
  partial class RawIonStatisticMainBuilderUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawIonStatisticMainBuilderUI));
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.label1 = new System.Windows.Forms.Label();
      this.txtProductIonPPM = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtMinimumIonRelativeIntensity = new System.Windows.Forms.TextBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(27, 421);
      this.pnlFile.Size = new System.Drawing.Size(840, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(594, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 466);
      this.lblProgress.Size = new System.Drawing.Size(897, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 487);
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
      // rawFiles
      // 
      this.rawFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.rawFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "Raw/mzData/mzXml files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "File";
      this.rawFiles.Location = new System.Drawing.Point(27, 23);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(840, 347);
      this.rawFiles.TabIndex = 53;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(25, 398);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(167, 12);
      this.label1.TabIndex = 79;
      this.label1.Text = "Product ion tolerance (ppm)";
      // 
      // txtProductIonPPM
      // 
      this.txtProductIonPPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtProductIonPPM.Location = new System.Drawing.Point(198, 394);
      this.txtProductIonPPM.Name = "txtProductIonPPM";
      this.txtProductIonPPM.Size = new System.Drawing.Size(132, 21);
      this.txtProductIonPPM.TabIndex = 78;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(351, 398);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(185, 12);
      this.label2.TabIndex = 81;
      this.label2.Text = "Minimum ion relative intensity";
      // 
      // txtMinimumIonRelativeIntensity
      // 
      this.txtMinimumIonRelativeIntensity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtMinimumIonRelativeIntensity.Location = new System.Drawing.Point(545, 394);
      this.txtMinimumIonRelativeIntensity.Name = "txtMinimumIonRelativeIntensity";
      this.txtMinimumIonRelativeIntensity.Size = new System.Drawing.Size(132, 21);
      this.txtMinimumIonRelativeIntensity.TabIndex = 80;
      // 
      // RawIonStatisticMainBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(897, 544);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtMinimumIonRelativeIntensity);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtProductIonPPM);
      this.Controls.Add(this.rawFiles);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "RawIonStatisticMainBuilderUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.Controls.SetChildIndex(this.txtProductIonPPM, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtMinimumIonRelativeIntensity, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Gui.MultipleFileField rawFiles;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtProductIonPPM;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMinimumIonRelativeIntensity;
  }
}