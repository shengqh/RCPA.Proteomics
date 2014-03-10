namespace RCPA.Proteomics.Quantification.ITraq
{
  partial class ITraqResultMultipleFileDistillerUI
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
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.progress = new RCPA.Gui.MultipleProgressField();
      this.saveDialog = new System.Windows.Forms.SaveFileDialog();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cbPlexType = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtMinPeakCount = new System.Windows.Forms.TextBox();
      this.txtIsotopeFileName = new System.Windows.Forms.ComboBox();
      this.btnIsotopeFile = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtPrecursorPPMTolerance = new System.Windows.Forms.TextBox();
      this.cbNormalizationType = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.cbNormalizeByIonInjectionTime = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbScanMode = new System.Windows.Forms.ComboBox();
      this.cbIndividual = new System.Windows.Forms.CheckBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 638);
      this.lblProgress.Size = new System.Drawing.Size(893, 1);
      this.lblProgress.Visible = false;
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 639);
      this.progressBar.Size = new System.Drawing.Size(893, 1);
      this.progressBar.Visible = false;
      // 
      // rawFiles
      // 
      this.rawFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "Raw/mzData/mzXml Files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "File";
      this.rawFiles.Location = new System.Drawing.Point(0, 0);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(893, 326);
      this.rawFiles.TabIndex = 7;
      // 
      // progress
      // 
      this.progress.AutoSize = true;
      this.progress.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.progress.Location = new System.Drawing.Point(0, 543);
      this.progress.Name = "progress";
      this.progress.Position = 0;
      this.progress.Size = new System.Drawing.Size(893, 95);
      this.progress.TabIndex = 8;
      // 
      // saveDialog
      // 
      this.saveDialog.DefaultExt = "itraq.xml";
      this.saveDialog.Filter = "ITraq Files|*.itraq.xml|All Files|*.*";
      this.saveDialog.Title = "Save ITraq Result To...";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.cbPlexType);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.txtMinPeakCount);
      this.groupBox1.Controls.Add(this.txtIsotopeFileName);
      this.groupBox1.Controls.Add(this.btnIsotopeFile);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.groupBox1.Location = new System.Drawing.Point(0, 429);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(893, 114);
      this.groupBox1.TabIndex = 23;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Plex related parameters";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(287, 56);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(50, 13);
      this.label3.TabIndex = 25;
      this.label3.Text = "Plex type";
      // 
      // cbPlexType
      // 
      this.cbPlexType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbPlexType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbPlexType.FormattingEnabled = true;
      this.cbPlexType.Location = new System.Drawing.Point(352, 53);
      this.cbPlexType.Name = "cbPlexType";
      this.cbPlexType.Size = new System.Drawing.Size(529, 21);
      this.cbPlexType.TabIndex = 24;
      this.cbPlexType.SelectedIndexChanged += new System.EventHandler(this.cbPlexType_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(239, 85);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(103, 13);
      this.label2.TabIndex = 23;
      this.label2.Text = "Minmum peak count";
      // 
      // txtMinPeakCount
      // 
      this.txtMinPeakCount.Location = new System.Drawing.Point(352, 81);
      this.txtMinPeakCount.Name = "txtMinPeakCount";
      this.txtMinPeakCount.Size = new System.Drawing.Size(42, 20);
      this.txtMinPeakCount.TabIndex = 22;
      this.txtMinPeakCount.Text = "4";
      // 
      // txtIsotopeFileName
      // 
      this.txtIsotopeFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtIsotopeFileName.FormattingEnabled = true;
      this.txtIsotopeFileName.Location = new System.Drawing.Point(352, 24);
      this.txtIsotopeFileName.Name = "txtIsotopeFileName";
      this.txtIsotopeFileName.Size = new System.Drawing.Size(529, 21);
      this.txtIsotopeFileName.TabIndex = 21;
      // 
      // btnIsotopeFile
      // 
      this.btnIsotopeFile.Location = new System.Drawing.Point(15, 22);
      this.btnIsotopeFile.Name = "btnIsotopeFile";
      this.btnIsotopeFile.Size = new System.Drawing.Size(331, 25);
      this.btnIsotopeFile.TabIndex = 20;
      this.btnIsotopeFile.Text = "button1";
      this.btnIsotopeFile.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.txtPrecursorPPMTolerance);
      this.groupBox2.Controls.Add(this.cbNormalizationType);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.cbNormalizeByIonInjectionTime);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.cbScanMode);
      this.groupBox2.Controls.Add(this.cbIndividual);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.groupBox2.Location = new System.Drawing.Point(0, 326);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(893, 103);
      this.groupBox2.TabIndex = 24;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Other parameters";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(431, 51);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(128, 13);
      this.label5.TabIndex = 30;
      this.label5.Text = "Precursor tolerance (ppm)";
      // 
      // txtPrecursorPPMTolerance
      // 
      this.txtPrecursorPPMTolerance.Location = new System.Drawing.Point(592, 48);
      this.txtPrecursorPPMTolerance.Name = "txtPrecursorPPMTolerance";
      this.txtPrecursorPPMTolerance.Size = new System.Drawing.Size(52, 20);
      this.txtPrecursorPPMTolerance.TabIndex = 29;
      this.txtPrecursorPPMTolerance.Text = "10";
      // 
      // cbNormalizationType
      // 
      this.cbNormalizationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbNormalizationType.FormattingEnabled = true;
      this.cbNormalizationType.Location = new System.Drawing.Point(144, 20);
      this.cbNormalizationType.Name = "cbNormalizationType";
      this.cbNormalizationType.Size = new System.Drawing.Size(278, 21);
      this.cbNormalizationType.TabIndex = 28;
      this.cbNormalizationType.SelectedIndexChanged += new System.EventHandler(this.cbNormalizationType_SelectedIndexChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(25, 23);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(93, 13);
      this.label4.TabIndex = 27;
      this.label4.Text = "Normalization type";
      // 
      // cbNormalizeByIonInjectionTime
      // 
      this.cbNormalizeByIonInjectionTime.AutoSize = true;
      this.cbNormalizeByIonInjectionTime.Location = new System.Drawing.Point(433, 22);
      this.cbNormalizeByIonInjectionTime.Name = "cbNormalizeByIonInjectionTime";
      this.cbNormalizeByIonInjectionTime.Size = new System.Drawing.Size(237, 17);
      this.cbNormalizeByIonInjectionTime.TabIndex = 26;
      this.cbNormalizeByIonInjectionTime.Text = "Considering ion injection time in normalization";
      this.cbNormalizeByIonInjectionTime.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(79, 51);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(61, 13);
      this.label1.TabIndex = 25;
      this.label1.Text = "Scan mode";
      // 
      // cbScanMode
      // 
      this.cbScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbScanMode.FormattingEnabled = true;
      this.cbScanMode.Location = new System.Drawing.Point(144, 48);
      this.cbScanMode.Name = "cbScanMode";
      this.cbScanMode.Size = new System.Drawing.Size(278, 21);
      this.cbScanMode.TabIndex = 24;
      // 
      // cbIndividual
      // 
      this.cbIndividual.AutoSize = true;
      this.cbIndividual.Location = new System.Drawing.Point(144, 76);
      this.cbIndividual.Name = "cbIndividual";
      this.cbIndividual.Size = new System.Drawing.Size(145, 17);
      this.cbIndividual.TabIndex = 23;
      this.cbIndividual.Text = "Generate individual result";
      this.cbIndividual.UseVisualStyleBackColor = true;
      // 
      // ITraqResultMultipleFileDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(893, 679);
      this.Controls.Add(this.rawFiles);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.progress);
      this.Name = "ITraqResultMultipleFileDistillerUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.progress, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.groupBox2, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RCPA.Gui.MultipleFileField rawFiles;
    private RCPA.Gui.MultipleProgressField progress;
    private System.Windows.Forms.SaveFileDialog saveDialog;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbPlexType;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMinPeakCount;
    private System.Windows.Forms.ComboBox txtIsotopeFileName;
    private System.Windows.Forms.Button btnIsotopeFile;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.ComboBox cbNormalizationType;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox cbNormalizeByIonInjectionTime;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbScanMode;
    private System.Windows.Forms.CheckBox cbIndividual;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtPrecursorPPMTolerance;
  }
}
