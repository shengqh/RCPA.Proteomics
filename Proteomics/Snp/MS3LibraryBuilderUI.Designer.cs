namespace RCPA.Proteomics.Snp
{
  partial class MS3LibraryBuilderUI
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
      this.txtMinIdentificationCount = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtMaxFragmentPeakCount = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtFragmentPPM = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtPrecursorPPM = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.peptideFile = new RCPA.Gui.FileField();
      this.outputFile = new RCPA.Gui.FileField();
      this.txtModification = new RCPA.Gui.TextField();
      this.maxTerminalLoss = new RCPA.Gui.IntegerField();
      this.minSequenceLength = new RCPA.Gui.IntegerField();
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
      this.rawFiles.Size = new System.Drawing.Size(1215, 471);
      this.rawFiles.TabIndex = 13;
      this.rawFiles.ValidateSelectedItemOnly = true;
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.minSequenceLength);
      this.panel1.Controls.Add(this.maxTerminalLoss);
      this.panel1.Controls.Add(this.txtModification);
      this.panel1.Controls.Add(this.txtMinIdentificationCount);
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
      this.panel1.Location = new System.Drawing.Point(0, 471);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1215, 173);
      this.panel1.TabIndex = 41;
      // 
      // txtMinIdentificationCount
      // 
      this.txtMinIdentificationCount.Location = new System.Drawing.Point(944, 8);
      this.txtMinIdentificationCount.Name = "txtMinIdentificationCount";
      this.txtMinIdentificationCount.Size = new System.Drawing.Size(75, 20);
      this.txtMinIdentificationCount.TabIndex = 64;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(727, 11);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(212, 13);
      this.label3.TabIndex = 63;
      this.label3.Text = "Minimum Identification Spectra Per Peptide:";
      // 
      // txtMaxFragmentPeakCount
      // 
      this.txtMaxFragmentPeakCount.Location = new System.Drawing.Point(630, 8);
      this.txtMaxFragmentPeakCount.Name = "txtMaxFragmentPeakCount";
      this.txtMaxFragmentPeakCount.Size = new System.Drawing.Size(75, 20);
      this.txtMaxFragmentPeakCount.TabIndex = 64;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(484, 11);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(139, 13);
      this.label2.TabIndex = 63;
      this.label2.Text = "Max Fragment Peak Count :";
      // 
      // txtFragmentPPM
      // 
      this.txtFragmentPPM.Location = new System.Drawing.Point(394, 8);
      this.txtFragmentPPM.Name = "txtFragmentPPM";
      this.txtFragmentPPM.Size = new System.Drawing.Size(75, 20);
      this.txtFragmentPPM.TabIndex = 62;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(253, 11);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(134, 13);
      this.label1.TabIndex = 61;
      this.label1.Text = "Fragment PPM Tolerance :";
      // 
      // txtPrecursorPPM
      // 
      this.txtPrecursorPPM.Location = new System.Drawing.Point(159, 8);
      this.txtPrecursorPPM.Name = "txtPrecursorPPM";
      this.txtPrecursorPPM.Size = new System.Drawing.Size(73, 20);
      this.txtPrecursorPPM.TabIndex = 59;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(18, 11);
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
      this.peptideFile.Location = new System.Drawing.Point(3, 109);
      this.peptideFile.Name = "peptideFile";
      this.peptideFile.OpenButtonText = "Browse Peptide File ...";
      this.peptideFile.PreCondition = null;
      this.peptideFile.Size = new System.Drawing.Size(1201, 23);
      this.peptideFile.TabIndex = 57;
      this.peptideFile.WidthOpenButton = 250;
      // 
      // outputFile
      // 
      this.outputFile.AfterBrowseFileEvent = null;
      this.outputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.outputFile.FullName = "";
      this.outputFile.Key = "DatabaseFile";
      this.outputFile.Location = new System.Drawing.Point(2, 138);
      this.outputFile.Name = "outputFile";
      this.outputFile.OpenButtonText = "Browse Output File ...";
      this.outputFile.PreCondition = null;
      this.outputFile.Size = new System.Drawing.Size(1201, 23);
      this.outputFile.TabIndex = 56;
      this.outputFile.WidthOpenButton = 250;
      // 
      // txtModification
      // 
      this.txtModification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtModification.Caption = "Input Modification : ";
      this.txtModification.CaptionWidth = 100;
      this.txtModification.DefaultValue = "";
      this.txtModification.Description = ", for example: (* +79.96633) (# +15.99492) (@ -18.00000) C=160.16523";
      this.txtModification.Key = "Modification";
      this.txtModification.Location = new System.Drawing.Point(71, 66);
      this.txtModification.Name = "txtModification";
      this.txtModification.PreCondition = null;
      this.txtModification.Required = false;
      this.txtModification.Size = new System.Drawing.Size(1132, 23);
      this.txtModification.TabIndex = 65;
      this.txtModification.TextWidth = 600;
      // 
      // maxTerminalLoss
      // 
      this.maxTerminalLoss.Caption = "Max Terminal Loss : ";
      this.maxTerminalLoss.CaptionWidth = 110;
      this.maxTerminalLoss.DefaultValue = "2";
      this.maxTerminalLoss.Description = "";
      this.maxTerminalLoss.Key = "MaxTerminalLoss";
      this.maxTerminalLoss.Location = new System.Drawing.Point(12, 37);
      this.maxTerminalLoss.Name = "maxTerminalLoss";
      this.maxTerminalLoss.PreCondition = null;
      this.maxTerminalLoss.Required = false;
      this.maxTerminalLoss.Size = new System.Drawing.Size(179, 23);
      this.maxTerminalLoss.TabIndex = 66;
      this.maxTerminalLoss.TextWidth = 50;
      this.maxTerminalLoss.Value = 2;
      // 
      // minSequenceLength
      // 
      this.minSequenceLength.Caption = "Min Terminal Lossed Sequence Length : ";
      this.minSequenceLength.CaptionWidth = 220;
      this.minSequenceLength.DefaultValue = "7";
      this.minSequenceLength.Description = "";
      this.minSequenceLength.Key = "MinSequenceLength";
      this.minSequenceLength.Location = new System.Drawing.Point(186, 37);
      this.minSequenceLength.Name = "minSequenceLength";
      this.minSequenceLength.PreCondition = null;
      this.minSequenceLength.Required = false;
      this.minSequenceLength.Size = new System.Drawing.Size(283, 23);
      this.minSequenceLength.TabIndex = 66;
      this.minSequenceLength.TextWidth = 63;
      this.minSequenceLength.Value = 7;
      // 
      // MS3LibraryBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1215, 729);
      this.Controls.Add(this.rawFiles);
      this.Controls.Add(this.panel1);
      this.Name = "MS3LibraryBuilderUI";
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
    private System.Windows.Forms.TextBox txtMinIdentificationCount;
    private System.Windows.Forms.Label label3;
    private Gui.TextField txtModification;
    private Gui.IntegerField minSequenceLength;
    private Gui.IntegerField maxTerminalLoss;
  }
}
