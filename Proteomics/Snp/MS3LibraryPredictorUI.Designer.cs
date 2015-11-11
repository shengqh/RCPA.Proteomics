namespace RCPA.Proteomics.Snp
{
  partial class MS3LibraryPredictorUI
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
      this.components = new System.ComponentModel.Container();
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.txtMinimumMatchedMS3SpectrumCount = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.libraryFile = new RCPA.Gui.FileField();
      this.fastaFile = new RCPA.Gui.FileField();
      this.ignoreDeamidatedMutation = new RCPA.Gui.RcpaCheckField();
      this.allowTerminalExtension = new RCPA.Gui.RcpaCheckField();
      this.allowTerminalLoss = new RCPA.Gui.RcpaCheckField();
      this.isSingleNucleotideMutationOnly = new RCPA.Gui.RcpaCheckField();
      this.txtMinimumMatchedMS3FragmentIonCount = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtMinimumMS3PrecursorMz = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtMinimumSubstitutionDeltaMass = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.txtMaximumMS3FragmentIonCount = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtMS3PrecursorPPM = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.txtMS3FragmentPPM = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtMS2PrecursorPPM = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.outputFile = new RCPA.Gui.FileField();
      this.pnlButton.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 644);
      this.lblProgress.Size = new System.Drawing.Size(1215, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 667);
      this.progressBar.Size = new System.Drawing.Size(1215, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 690);
      this.pnlButton.Size = new System.Drawing.Size(1215, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(655, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(570, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(485, 8);
      // 
      // rawFiles
      // 
      this.rawFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "Raw Files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "RawFiles";
      this.rawFiles.Location = new System.Drawing.Point(0, 0);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(1215, 427);
      this.rawFiles.TabIndex = 13;
      this.rawFiles.ValidateSelectedItemOnly = true;
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.txtMinimumMatchedMS3SpectrumCount);
      this.panel1.Controls.Add(this.label6);
      this.panel1.Controls.Add(this.libraryFile);
      this.panel1.Controls.Add(this.fastaFile);
      this.panel1.Controls.Add(this.ignoreDeamidatedMutation);
      this.panel1.Controls.Add(this.allowTerminalExtension);
      this.panel1.Controls.Add(this.allowTerminalLoss);
      this.panel1.Controls.Add(this.isSingleNucleotideMutationOnly);
      this.panel1.Controls.Add(this.txtMinimumMatchedMS3FragmentIonCount);
      this.panel1.Controls.Add(this.label5);
      this.panel1.Controls.Add(this.txtMinimumMS3PrecursorMz);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.txtMinimumSubstitutionDeltaMass);
      this.panel1.Controls.Add(this.label8);
      this.panel1.Controls.Add(this.txtMaximumMS3FragmentIonCount);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.txtMS3PrecursorPPM);
      this.panel1.Controls.Add(this.label7);
      this.panel1.Controls.Add(this.txtMS3FragmentPPM);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txtMS2PrecursorPPM);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.outputFile);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 427);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1215, 217);
      this.panel1.TabIndex = 41;
      // 
      // txtMinimumMatchedMS3SpectrumCount
      // 
      this.txtMinimumMatchedMS3SpectrumCount.Location = new System.Drawing.Point(657, 62);
      this.txtMinimumMatchedMS3SpectrumCount.Name = "txtMinimumMatchedMS3SpectrumCount";
      this.txtMinimumMatchedMS3SpectrumCount.Size = new System.Drawing.Size(75, 20);
      this.txtMinimumMatchedMS3SpectrumCount.TabIndex = 70;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(448, 65);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(203, 13);
      this.label6.TabIndex = 69;
      this.label6.Text = "Minimum Matched MS3 Precursor Count :";
      // 
      // libraryFile
      // 
      this.libraryFile.AfterBrowseFileEvent = null;
      this.libraryFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.libraryFile.FullName = "";
      this.libraryFile.Key = "LibraryFile";
      this.libraryFile.Location = new System.Drawing.Point(3, 114);
      this.libraryFile.Name = "libraryFile";
      this.libraryFile.OpenButtonText = "Browse Library File ...";
      this.libraryFile.PreCondition = null;
      this.libraryFile.Size = new System.Drawing.Size(1198, 23);
      this.libraryFile.TabIndex = 68;
      this.libraryFile.WidthOpenButton = 250;
      // 
      // fastaFile
      // 
      this.fastaFile.AfterBrowseFileEvent = null;
      this.fastaFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.fastaFile.FullName = "";
      this.fastaFile.Key = "DatabaseFile";
      this.fastaFile.Location = new System.Drawing.Point(3, 143);
      this.fastaFile.Name = "fastaFile";
      this.fastaFile.OpenButtonText = "Browse Database File ...";
      this.fastaFile.PreCondition = null;
      this.fastaFile.Size = new System.Drawing.Size(1198, 23);
      this.fastaFile.TabIndex = 68;
      this.fastaFile.WidthOpenButton = 250;
      // 
      // ignoreDeamidatedMutation
      // 
      this.ignoreDeamidatedMutation.Checked = true;
      this.ignoreDeamidatedMutation.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ignoreDeamidatedMutation.Key = "IgnoreDeamidatedMutation";
      this.ignoreDeamidatedMutation.Location = new System.Drawing.Point(756, 58);
      this.ignoreDeamidatedMutation.Name = "ignoreDeamidatedMutation";
      this.ignoreDeamidatedMutation.PreCondition = null;
      this.ignoreDeamidatedMutation.Size = new System.Drawing.Size(164, 23);
      this.ignoreDeamidatedMutation.TabIndex = 66;
      this.ignoreDeamidatedMutation.Text = "Ignore N->D, Q->E Mutation";
      // 
      // cbAllowTerminalGain
      // 
      this.allowTerminalExtension.Key = "AllowTerminalLoss";
      this.allowTerminalExtension.Location = new System.Drawing.Point(756, 32);
      this.allowTerminalExtension.Name = "cbAllowTerminalGain";
      this.allowTerminalExtension.PreCondition = null;
      this.allowTerminalExtension.Size = new System.Drawing.Size(185, 21);
      this.allowTerminalExtension.TabIndex = 65;
      this.allowTerminalExtension.Text = "Allow Terminal Extension";
      // 
      // cbAllowTerminalLoss
      // 
      this.allowTerminalLoss.Key = "AllowTerminalLoss";
      this.allowTerminalLoss.Location = new System.Drawing.Point(756, 8);
      this.allowTerminalLoss.Name = "cbAllowTerminalLoss";
      this.allowTerminalLoss.PreCondition = null;
      this.allowTerminalLoss.Size = new System.Drawing.Size(136, 21);
      this.allowTerminalLoss.TabIndex = 65;
      this.allowTerminalLoss.Text = "Allow Terminal Loss";
      // 
      // ignoreMultipleNucleotideMutation
      // 
      this.isSingleNucleotideMutationOnly.Checked = true;
      this.isSingleNucleotideMutationOnly.CheckState = System.Windows.Forms.CheckState.Checked;
      this.isSingleNucleotideMutationOnly.Key = "KeepSingleNucleotideMutationOnly";
      this.isSingleNucleotideMutationOnly.Location = new System.Drawing.Point(756, 82);
      this.isSingleNucleotideMutationOnly.Name = "ignoreMultipleNucleotideMutation";
      this.isSingleNucleotideMutationOnly.PreCondition = null;
      this.isSingleNucleotideMutationOnly.Size = new System.Drawing.Size(222, 23);
      this.isSingleNucleotideMutationOnly.TabIndex = 65;
      this.isSingleNucleotideMutationOnly.Text = "Keep Single Nucleotide Mutation Only";
      // 
      // txtMinimumMatchedMS3FragmentIonCount
      // 
      this.txtMinimumMatchedMS3FragmentIonCount.Location = new System.Drawing.Point(657, 86);
      this.txtMinimumMatchedMS3FragmentIonCount.Name = "txtMinimumMatchedMS3FragmentIonCount";
      this.txtMinimumMatchedMS3FragmentIonCount.Size = new System.Drawing.Size(75, 20);
      this.txtMinimumMatchedMS3FragmentIonCount.TabIndex = 64;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(293, 89);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(358, 13);
      this.label5.TabIndex = 63;
      this.label5.Text = "At Least One MS3 Spectrum With Minimum Matched Fragment Ion Count :";
      // 
      // txtMinimumMS3PrecursorMz
      // 
      this.txtMinimumMS3PrecursorMz.Location = new System.Drawing.Point(192, 6);
      this.txtMinimumMS3PrecursorMz.Name = "txtMinimumMS3PrecursorMz";
      this.txtMinimumMS3PrecursorMz.Size = new System.Drawing.Size(75, 20);
      this.txtMinimumMS3PrecursorMz.TabIndex = 64;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(40, 8);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(148, 13);
      this.label3.TabIndex = 63;
      this.label3.Text = "Minimum MS3 Precursor m/z :";
      // 
      // txtMinimumSubstitutionDeltaMass
      // 
      this.txtMinimumSubstitutionDeltaMass.Location = new System.Drawing.Point(657, 35);
      this.txtMinimumSubstitutionDeltaMass.Name = "txtMinimumSubstitutionDeltaMass";
      this.txtMinimumSubstitutionDeltaMass.Size = new System.Drawing.Size(75, 20);
      this.txtMinimumSubstitutionDeltaMass.TabIndex = 64;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(428, 38);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(220, 13);
      this.label8.TabIndex = 63;
      this.label8.Text = "Minimum Aminoacid Substitution Delta Mass :";
      // 
      // txtMaximumMS3FragmentIonCount
      // 
      this.txtMaximumMS3FragmentIonCount.Location = new System.Drawing.Point(657, 8);
      this.txtMaximumMS3FragmentIonCount.Name = "txtMaximumMS3FragmentIonCount";
      this.txtMaximumMS3FragmentIonCount.Size = new System.Drawing.Size(75, 20);
      this.txtMaximumMS3FragmentIonCount.TabIndex = 64;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(510, 13);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(141, 13);
      this.label2.TabIndex = 63;
      this.label2.Text = "Maximum MS3 Peak Count :";
      // 
      // txtMS3PrecursorPPM
      // 
      this.txtMS3PrecursorPPM.Location = new System.Drawing.Point(192, 59);
      this.txtMS3PrecursorPPM.Name = "txtMS3PrecursorPPM";
      this.txtMS3PrecursorPPM.Size = new System.Drawing.Size(75, 20);
      this.txtMS3PrecursorPPM.TabIndex = 62;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(28, 62);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(160, 13);
      this.label7.TabIndex = 61;
      this.label7.Text = "MS3 Precursor PPM Tolerance :";
      // 
      // txtMS3FragmentPPM
      // 
      this.txtMS3FragmentPPM.Location = new System.Drawing.Point(192, 86);
      this.txtMS3FragmentPPM.Name = "txtMS3FragmentPPM";
      this.txtMS3FragmentPPM.Size = new System.Drawing.Size(75, 20);
      this.txtMS3FragmentPPM.TabIndex = 62;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 89);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(177, 13);
      this.label1.TabIndex = 61;
      this.label1.Text = "MS3 Fragment Ion PPM Tolerance :";
      // 
      // txtMS2PrecursorPPM
      // 
      this.txtMS2PrecursorPPM.Location = new System.Drawing.Point(192, 32);
      this.txtMS2PrecursorPPM.Name = "txtMS2PrecursorPPM";
      this.txtMS2PrecursorPPM.Size = new System.Drawing.Size(75, 20);
      this.txtMS2PrecursorPPM.TabIndex = 59;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(28, 35);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(160, 13);
      this.label4.TabIndex = 58;
      this.label4.Text = "MS2 Precursor PPM Tolerance :";
      // 
      // outputFile
      // 
      this.outputFile.AfterBrowseFileEvent = null;
      this.outputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.outputFile.FullName = "";
      this.outputFile.Key = "OutputFile";
      this.outputFile.Location = new System.Drawing.Point(4, 172);
      this.outputFile.Name = "outputFile";
      this.outputFile.OpenButtonText = "Save Output File ...";
      this.outputFile.PreCondition = null;
      this.outputFile.Size = new System.Drawing.Size(1197, 23);
      this.outputFile.TabIndex = 56;
      this.outputFile.WidthOpenButton = 250;
      // 
      // MS3LibraryPredictorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1215, 729);
      this.Controls.Add(this.rawFiles);
      this.Controls.Add(this.panel1);
      this.Name = "MS3LibraryPredictorUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.pnlButton.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.MultipleFileField rawFiles;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Panel panel1;
    private Gui.FileField outputFile;
    private System.Windows.Forms.TextBox txtMS2PrecursorPPM;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtMaximumMS3FragmentIonCount;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMS3FragmentPPM;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtMinimumMS3PrecursorMz;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtMinimumMatchedMS3FragmentIonCount;
    private System.Windows.Forms.Label label5;
    private Gui.RcpaCheckField ignoreDeamidatedMutation;
    private Gui.RcpaCheckField isSingleNucleotideMutationOnly;
    private Gui.FileField fastaFile;
    private Gui.FileField libraryFile;
    private Gui.RcpaCheckField allowTerminalLoss;
    private System.Windows.Forms.TextBox txtMinimumMatchedMS3SpectrumCount;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtMS3PrecursorPPM;
    private System.Windows.Forms.Label label7;
    private Gui.RcpaCheckField allowTerminalExtension;
    private System.Windows.Forms.TextBox txtMinimumSubstitutionDeltaMass;
    private System.Windows.Forms.Label label8;
  }
}
