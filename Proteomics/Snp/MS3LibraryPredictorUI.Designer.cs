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
      this.libraryFile = new RCPA.Gui.FileField();
      this.fastaFile = new RCPA.Gui.FileField();
      this.ignoreDeamidatedMutation = new RCPA.Gui.RcpaCheckField();
      this.cbAllowTerminalLoss = new RCPA.Gui.RcpaCheckField();
      this.ignoreMultipleNucleotideMutation = new RCPA.Gui.RcpaCheckField();
      this.txtMinimumMatchedMs3IonCount = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtMinMs3PrecursorMz = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtMaxFragmentPeakCount = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtFragmentPPM = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtPrecursorPPM = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.peptideFile = new RCPA.Gui.FileField();
      this.outputFile = new RCPA.Gui.FileField();
      this.cbMatchMS3First = new RCPA.Gui.RcpaCheckField();
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
      this.rawFiles.Size = new System.Drawing.Size(1215, 466);
      this.rawFiles.TabIndex = 13;
      this.rawFiles.ValidateSelectedItemOnly = true;
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.libraryFile);
      this.panel1.Controls.Add(this.fastaFile);
      this.panel1.Controls.Add(this.ignoreDeamidatedMutation);
      this.panel1.Controls.Add(this.cbAllowTerminalLoss);
      this.panel1.Controls.Add(this.cbMatchMS3First);
      this.panel1.Controls.Add(this.ignoreMultipleNucleotideMutation);
      this.panel1.Controls.Add(this.txtMinimumMatchedMs3IonCount);
      this.panel1.Controls.Add(this.label5);
      this.panel1.Controls.Add(this.txtMinMs3PrecursorMz);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.txtMaxFragmentPeakCount);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.txtFragmentPPM);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txtPrecursorPPM);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.peptideFile);
      this.panel1.Controls.Add(this.outputFile);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 466);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1215, 178);
      this.panel1.TabIndex = 41;
      // 
      // libraryFile
      // 
      this.libraryFile.AfterBrowseFileEvent = null;
      this.libraryFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.libraryFile.FullName = "";
      this.libraryFile.Key = "LibraryFile";
      this.libraryFile.Location = new System.Drawing.Point(3, 92);
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
      this.fastaFile.Location = new System.Drawing.Point(3, 121);
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
      this.ignoreDeamidatedMutation.Location = new System.Drawing.Point(807, 6);
      this.ignoreDeamidatedMutation.Name = "ignoreDeamidatedMutation";
      this.ignoreDeamidatedMutation.PreCondition = null;
      this.ignoreDeamidatedMutation.Size = new System.Drawing.Size(191, 23);
      this.ignoreDeamidatedMutation.TabIndex = 66;
      this.ignoreDeamidatedMutation.Text = "Ignore N->D, Q->E Mutation";
      // 
      // cbAllowTerminalLoss
      // 
      this.cbAllowTerminalLoss.Checked = true;
      this.cbAllowTerminalLoss.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbAllowTerminalLoss.Key = "AllowTerminalLoss";
      this.cbAllowTerminalLoss.Location = new System.Drawing.Point(562, 32);
      this.cbAllowTerminalLoss.Name = "cbAllowTerminalLoss";
      this.cbAllowTerminalLoss.PreCondition = null;
      this.cbAllowTerminalLoss.Size = new System.Drawing.Size(136, 21);
      this.cbAllowTerminalLoss.TabIndex = 65;
      this.cbAllowTerminalLoss.Text = "Allow Terminal Loss";
      // 
      // ignoreMultipleNucleotideMutation
      // 
      this.ignoreMultipleNucleotideMutation.Checked = true;
      this.ignoreMultipleNucleotideMutation.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ignoreMultipleNucleotideMutation.Key = "KeepSingleNucleotideMutationOnly";
      this.ignoreMultipleNucleotideMutation.Location = new System.Drawing.Point(705, 31);
      this.ignoreMultipleNucleotideMutation.Name = "ignoreMultipleNucleotideMutation";
      this.ignoreMultipleNucleotideMutation.PreCondition = null;
      this.ignoreMultipleNucleotideMutation.Size = new System.Drawing.Size(222, 21);
      this.ignoreMultipleNucleotideMutation.TabIndex = 65;
      this.ignoreMultipleNucleotideMutation.Text = "Keep Single Nucleotide Mutation Only";
      // 
      // txtMinimumMatchedMs3IonCount
      // 
      this.txtMinimumMatchedMs3IonCount.Location = new System.Drawing.Point(451, 32);
      this.txtMinimumMatchedMs3IonCount.Name = "txtMinimumMatchedMs3IonCount";
      this.txtMinimumMatchedMs3IonCount.Size = new System.Drawing.Size(75, 20);
      this.txtMinimumMatchedMs3IonCount.TabIndex = 64;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(272, 35);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(173, 13);
      this.label5.TabIndex = 63;
      this.label5.Text = "Minimum Matched MS3 Ion Count :";
      // 
      // txtMinMs3PrecursorMz
      // 
      this.txtMinMs3PrecursorMz.Location = new System.Drawing.Point(176, 32);
      this.txtMinMs3PrecursorMz.Name = "txtMinMs3PrecursorMz";
      this.txtMinMs3PrecursorMz.Size = new System.Drawing.Size(75, 20);
      this.txtMinMs3PrecursorMz.TabIndex = 64;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(22, 35);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(148, 13);
      this.label3.TabIndex = 63;
      this.label3.Text = "Minimum MS3 Precursor m/z :";
      // 
      // txtMaxFragmentPeakCount
      // 
      this.txtMaxFragmentPeakCount.Location = new System.Drawing.Point(705, 6);
      this.txtMaxFragmentPeakCount.Name = "txtMaxFragmentPeakCount";
      this.txtMaxFragmentPeakCount.Size = new System.Drawing.Size(75, 20);
      this.txtMaxFragmentPeakCount.TabIndex = 64;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(559, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(139, 13);
      this.label2.TabIndex = 63;
      this.label2.Text = "Max Fragment Peak Count :";
      // 
      // txtFragmentPPM
      // 
      this.txtFragmentPPM.Location = new System.Drawing.Point(451, 6);
      this.txtFragmentPPM.Name = "txtFragmentPPM";
      this.txtFragmentPPM.Size = new System.Drawing.Size(75, 20);
      this.txtFragmentPPM.TabIndex = 62;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(310, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(134, 13);
      this.label1.TabIndex = 61;
      this.label1.Text = "Fragment PPM Tolerance :";
      // 
      // txtPrecursorPPM
      // 
      this.txtPrecursorPPM.Location = new System.Drawing.Point(176, 6);
      this.txtPrecursorPPM.Name = "txtPrecursorPPM";
      this.txtPrecursorPPM.Size = new System.Drawing.Size(75, 20);
      this.txtPrecursorPPM.TabIndex = 59;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(35, 9);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(135, 13);
      this.label4.TabIndex = 58;
      this.label4.Text = "Precursor PPM Tolerance :";
      // 
      // peptideFile
      // 
      this.peptideFile.AfterBrowseFileEvent = null;
      this.peptideFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.peptideFile.FullName = "";
      this.peptideFile.Key = "PeptideFile";
      this.peptideFile.Location = new System.Drawing.Point(3, 63);
      this.peptideFile.Name = "peptideFile";
      this.peptideFile.OpenButtonText = "Browse Peptide File ...";
      this.peptideFile.PreCondition = null;
      this.peptideFile.Size = new System.Drawing.Size(1199, 23);
      this.peptideFile.TabIndex = 57;
      this.peptideFile.WidthOpenButton = 250;
      // 
      // outputFile
      // 
      this.outputFile.AfterBrowseFileEvent = null;
      this.outputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.outputFile.FullName = "";
      this.outputFile.Key = "OutputFile";
      this.outputFile.Location = new System.Drawing.Point(4, 150);
      this.outputFile.Name = "outputFile";
      this.outputFile.OpenButtonText = "Browse All File ...";
      this.outputFile.PreCondition = null;
      this.outputFile.Size = new System.Drawing.Size(1198, 23);
      this.outputFile.TabIndex = 56;
      this.outputFile.WidthOpenButton = 250;
      // 
      // cbMatchMS3First
      // 
      this.cbMatchMS3First.Key = "MatchMS3First";
      this.cbMatchMS3First.Location = new System.Drawing.Point(933, 32);
      this.cbMatchMS3First.Name = "cbMatchMS3First";
      this.cbMatchMS3First.PreCondition = null;
      this.cbMatchMS3First.Size = new System.Drawing.Size(117, 21);
      this.cbMatchMS3First.TabIndex = 65;
      this.cbMatchMS3First.Text = "Match MS3 First";
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
    private Gui.FileField peptideFile;
    private Gui.FileField outputFile;
    private System.Windows.Forms.TextBox txtPrecursorPPM;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtMaxFragmentPeakCount;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtFragmentPPM;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtMinMs3PrecursorMz;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtMinimumMatchedMs3IonCount;
    private System.Windows.Forms.Label label5;
    private Gui.RcpaCheckField ignoreDeamidatedMutation;
    private Gui.RcpaCheckField ignoreMultipleNucleotideMutation;
    private Gui.FileField fastaFile;
    private Gui.FileField libraryFile;
    private Gui.RcpaCheckField cbAllowTerminalLoss;
    private Gui.RcpaCheckField cbMatchMS3First;
  }
}
