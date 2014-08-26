namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  partial class IsobaricResultMultipleFileDistillerUI
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
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cbPlexType = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtMinPeakCount = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtProductPPM = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtPrecursorPPMTolerance = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbScanMode = new System.Windows.Forms.ComboBox();
      this.cbIndividual = new System.Windows.Forms.CheckBox();
      this.channels = new RCPA.Proteomics.Quantification.IsobaricLabelling.IsobaricChannelField();
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
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(494, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(409, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(324, 8);
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
      this.rawFiles.Size = new System.Drawing.Size(893, 415);
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
      this.saveDialog.Filter = "Isobaric XML Files|*.isobaric.xml|All Files|*.*";
      this.saveDialog.Title = "Save ITraq Result To...";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.channels);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.cbPlexType);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.txtMinPeakCount);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.txtProductPPM);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.txtPrecursorPPMTolerance);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.cbScanMode);
      this.groupBox2.Controls.Add(this.cbIndividual);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.groupBox2.Location = new System.Drawing.Point(0, 415);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(893, 128);
      this.groupBox2.TabIndex = 24;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Options";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 52);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(67, 13);
      this.label3.TabIndex = 36;
      this.label3.Text = "Isobaric type";
      // 
      // cbPlexType
      // 
      this.cbPlexType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbPlexType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbPlexType.FormattingEnabled = true;
      this.cbPlexType.Location = new System.Drawing.Point(79, 49);
      this.cbPlexType.Name = "cbPlexType";
      this.cbPlexType.Size = new System.Drawing.Size(278, 21);
      this.cbPlexType.TabIndex = 35;
      this.cbPlexType.SelectedValueChanged += new System.EventHandler(this.cbPlexType_SelectedValueChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(421, 52);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(103, 13);
      this.label2.TabIndex = 34;
      this.label2.Text = "Minmum peak count";
      // 
      // txtMinPeakCount
      // 
      this.txtMinPeakCount.Location = new System.Drawing.Point(542, 46);
      this.txtMinPeakCount.Name = "txtMinPeakCount";
      this.txtMinPeakCount.Size = new System.Drawing.Size(52, 20);
      this.txtMinPeakCount.TabIndex = 33;
      this.txtMinPeakCount.Text = "4";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(613, 23);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(137, 13);
      this.label6.TabIndex = 32;
      this.label6.Text = "Product ion tolerance (ppm)";
      // 
      // txtProductPPM
      // 
      this.txtProductPPM.Location = new System.Drawing.Point(765, 20);
      this.txtProductPPM.Name = "txtProductPPM";
      this.txtProductPPM.Size = new System.Drawing.Size(52, 20);
      this.txtProductPPM.TabIndex = 31;
      this.txtProductPPM.Text = "10";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(379, 23);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(145, 13);
      this.label5.TabIndex = 30;
      this.label5.Text = "Precursor ion tolerance (ppm)";
      // 
      // txtPrecursorPPMTolerance
      // 
      this.txtPrecursorPPMTolerance.Location = new System.Drawing.Point(542, 20);
      this.txtPrecursorPPMTolerance.Name = "txtPrecursorPPMTolerance";
      this.txtPrecursorPPMTolerance.Size = new System.Drawing.Size(52, 20);
      this.txtPrecursorPPMTolerance.TabIndex = 29;
      this.txtPrecursorPPMTolerance.Text = "10";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 23);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(61, 13);
      this.label1.TabIndex = 25;
      this.label1.Text = "Scan mode";
      // 
      // cbScanMode
      // 
      this.cbScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbScanMode.FormattingEnabled = true;
      this.cbScanMode.Location = new System.Drawing.Point(79, 20);
      this.cbScanMode.Name = "cbScanMode";
      this.cbScanMode.Size = new System.Drawing.Size(278, 21);
      this.cbScanMode.TabIndex = 24;
      // 
      // cbIndividual
      // 
      this.cbIndividual.AutoSize = true;
      this.cbIndividual.Location = new System.Drawing.Point(616, 51);
      this.cbIndividual.Name = "cbIndividual";
      this.cbIndividual.Size = new System.Drawing.Size(145, 17);
      this.cbIndividual.TabIndex = 23;
      this.cbIndividual.Text = "Generate individual result";
      this.cbIndividual.UseVisualStyleBackColor = true;
      // 
      // channels
      // 
      this.channels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.channels.Description = "Required channels";
      this.channels.Location = new System.Drawing.Point(17, 76);
      this.channels.Name = "channels";
      this.channels.PlexType = null;
      this.channels.Required = false;
      this.channels.SelectedIons = "";
      this.channels.Size = new System.Drawing.Size(770, 30);
      this.channels.TabIndex = 38;
      // 
      // IsobaricResultMultipleFileDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(893, 679);
      this.Controls.Add(this.rawFiles);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.progress);
      this.Name = "IsobaricResultMultipleFileDistillerUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.progress, 0);
      this.Controls.SetChildIndex(this.groupBox2, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RCPA.Gui.MultipleFileField rawFiles;
    private RCPA.Gui.MultipleProgressField progress;
    private System.Windows.Forms.SaveFileDialog saveDialog;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbScanMode;
    private System.Windows.Forms.CheckBox cbIndividual;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtPrecursorPPMTolerance;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtProductPPM;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbPlexType;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMinPeakCount;
    private IsobaricChannelField channels;
  }
}
