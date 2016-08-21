namespace RCPA.Proteomics.Deuterium
{
  partial class DeuteriumCalculatorUI
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
      this.btnNoredundantFile = new System.Windows.Forms.Button();
      this.txtNoredundantFile = new System.Windows.Forms.TextBox();
      this.txtLog = new System.Windows.Forms.RichTextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbOverwrite = new RCPA.Gui.RcpaCheckField();
      this.cbDrawImage = new RCPA.Gui.RcpaCheckField();
      this.cbExcludeIsotopic0InFormula = new RCPA.Gui.RcpaCheckField();
      this.precursorPPMTolerance = new RCPA.Gui.IntegerField();
      this.minimumProfileLength = new RCPA.Gui.IntegerField();
      this.minimumIsotopicPercentage = new RCPA.Gui.DoubleField();
      this.maximumProfileDistance = new RCPA.Gui.DoubleField();
      this.threadCount = new RCPA.Gui.IntegerField();
      this.btnLoad = new System.Windows.Forms.Button();
      this.rbPeptideInAllTimePointOnly = new RCPA.Gui.RcpaCheckField();
      this.pnlClassification = new RCPA.Proteomics.ClassificationPanel();
      this.rawFiles = new RCPA.Gui.MultipleFileField();
      this.minimumProfileCorrelation = new RCPA.Gui.DoubleField();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 683);
      this.lblProgress.Size = new System.Drawing.Size(1231, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 706);
      this.progressBar.Size = new System.Drawing.Size(1231, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 729);
      this.pnlButton.Size = new System.Drawing.Size(1231, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(663, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(578, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(493, 8);
      // 
      // btnNoredundantFile
      // 
      this.btnNoredundantFile.Location = new System.Drawing.Point(15, 21);
      this.btnNoredundantFile.Name = "btnNoredundantFile";
      this.btnNoredundantFile.Size = new System.Drawing.Size(219, 25);
      this.btnNoredundantFile.TabIndex = 9;
      this.btnNoredundantFile.Text = "button1";
      this.btnNoredundantFile.UseVisualStyleBackColor = true;
      // 
      // txtNoredundantFile
      // 
      this.txtNoredundantFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtNoredundantFile.Location = new System.Drawing.Point(240, 24);
      this.txtNoredundantFile.Name = "txtNoredundantFile";
      this.txtNoredundantFile.Size = new System.Drawing.Size(895, 20);
      this.txtNoredundantFile.TabIndex = 10;
      // 
      // txtLog
      // 
      this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtLog.Location = new System.Drawing.Point(15, 392);
      this.txtLog.Name = "txtLog";
      this.txtLog.ReadOnly = true;
      this.txtLog.Size = new System.Drawing.Size(1204, 280);
      this.txtLog.TabIndex = 13;
      this.txtLog.Text = "";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 376);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(104, 13);
      this.label1.TabIndex = 14;
      this.label1.Text = "Running information:";
      // 
      // cbOverwrite
      // 
      this.cbOverwrite.AutoSize = true;
      this.cbOverwrite.Key = "Overwrite";
      this.cbOverwrite.Location = new System.Drawing.Point(708, 324);
      this.cbOverwrite.Name = "cbOverwrite";
      this.cbOverwrite.PreCondition = null;
      this.cbOverwrite.Size = new System.Drawing.Size(116, 17);
      this.cbOverwrite.TabIndex = 15;
      this.cbOverwrite.Text = "Overwrite old result";
      this.cbOverwrite.UseVisualStyleBackColor = true;
      // 
      // cbDrawImage
      // 
      this.cbDrawImage.AutoSize = true;
      this.cbDrawImage.Checked = true;
      this.cbDrawImage.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbDrawImage.Key = "DrawImage";
      this.cbDrawImage.Location = new System.Drawing.Point(708, 295);
      this.cbDrawImage.Name = "cbDrawImage";
      this.cbDrawImage.PreCondition = null;
      this.cbDrawImage.Size = new System.Drawing.Size(82, 17);
      this.cbDrawImage.TabIndex = 15;
      this.cbDrawImage.Text = "Draw image";
      this.cbDrawImage.UseVisualStyleBackColor = true;
      // 
      // cbExcludeIsotopic0InFormula
      // 
      this.cbExcludeIsotopic0InFormula.AutoSize = true;
      this.cbExcludeIsotopic0InFormula.Key = "ExcludeIsotopic0InFormula";
      this.cbExcludeIsotopic0InFormula.Location = new System.Drawing.Point(841, 295);
      this.cbExcludeIsotopic0InFormula.Name = "cbExcludeIsotopic0InFormula";
      this.cbExcludeIsotopic0InFormula.PreCondition = null;
      this.cbExcludeIsotopic0InFormula.Size = new System.Drawing.Size(177, 17);
      this.cbExcludeIsotopic0InFormula.TabIndex = 15;
      this.cbExcludeIsotopic0InFormula.Text = "Exclude isotopic 0 in calculation";
      this.cbExcludeIsotopic0InFormula.UseVisualStyleBackColor = true;
      // 
      // precursorPPMTolerance
      // 
      this.precursorPPMTolerance.Caption = "Precursor tolerance (ppm):";
      this.precursorPPMTolerance.CaptionWidth = 150;
      this.precursorPPMTolerance.DefaultValue = "20";
      this.precursorPPMTolerance.Description = "";
      this.precursorPPMTolerance.Key = "precursorPPMTolerance";
      this.precursorPPMTolerance.Location = new System.Drawing.Point(13, 295);
      this.precursorPPMTolerance.Name = "precursorPPMTolerance";
      this.precursorPPMTolerance.PreCondition = null;
      this.precursorPPMTolerance.Required = false;
      this.precursorPPMTolerance.Size = new System.Drawing.Size(212, 23);
      this.precursorPPMTolerance.TabIndex = 16;
      this.precursorPPMTolerance.TextWidth = 62;
      this.precursorPPMTolerance.Value = 20;
      // 
      // minimumProfileLength
      // 
      this.minimumProfileLength.Caption = "Minimum profile length:";
      this.minimumProfileLength.CaptionWidth = 150;
      this.minimumProfileLength.DefaultValue = "3";
      this.minimumProfileLength.Description = "";
      this.minimumProfileLength.Key = "minimumProfileLength";
      this.minimumProfileLength.Location = new System.Drawing.Point(231, 295);
      this.minimumProfileLength.Name = "minimumProfileLength";
      this.minimumProfileLength.PreCondition = null;
      this.minimumProfileLength.Required = false;
      this.minimumProfileLength.Size = new System.Drawing.Size(212, 23);
      this.minimumProfileLength.TabIndex = 16;
      this.minimumProfileLength.TextWidth = 62;
      this.minimumProfileLength.Value = 3;
      // 
      // minimumIsotopicPercentage
      // 
      this.minimumIsotopicPercentage.Caption = "Minimum isotopic percentage:";
      this.minimumIsotopicPercentage.CaptionWidth = 150;
      this.minimumIsotopicPercentage.DefaultValue = "0.01";
      this.minimumIsotopicPercentage.Description = "";
      this.minimumIsotopicPercentage.Key = "minimumIsobaricPercentage";
      this.minimumIsotopicPercentage.Location = new System.Drawing.Point(13, 324);
      this.minimumIsotopicPercentage.Name = "minimumIsotopicPercentage";
      this.minimumIsotopicPercentage.PreCondition = null;
      this.minimumIsotopicPercentage.Required = false;
      this.minimumIsotopicPercentage.Size = new System.Drawing.Size(212, 23);
      this.minimumIsotopicPercentage.TabIndex = 16;
      this.minimumIsotopicPercentage.TextWidth = 62;
      this.minimumIsotopicPercentage.Value = 0.01D;
      // 
      // maximumProfileDistance
      // 
      this.maximumProfileDistance.Caption = "Maximum profile distance:";
      this.maximumProfileDistance.CaptionWidth = 150;
      this.maximumProfileDistance.DefaultValue = "0.1";
      this.maximumProfileDistance.Description = "";
      this.maximumProfileDistance.Key = "minimumProfileDistance";
      this.maximumProfileDistance.Location = new System.Drawing.Point(449, 324);
      this.maximumProfileDistance.Name = "maximumProfileDistance";
      this.maximumProfileDistance.PreCondition = null;
      this.maximumProfileDistance.Required = false;
      this.maximumProfileDistance.Size = new System.Drawing.Size(212, 23);
      this.maximumProfileDistance.TabIndex = 16;
      this.maximumProfileDistance.TextWidth = 62;
      this.maximumProfileDistance.Value = 0.1D;
      this.maximumProfileDistance.Visible = false;
      // 
      // threadCount
      // 
      this.threadCount.Caption = "Thread count:";
      this.threadCount.CaptionWidth = 150;
      this.threadCount.DefaultValue = "1";
      this.threadCount.Description = "";
      this.threadCount.Key = "threadCount";
      this.threadCount.Location = new System.Drawing.Point(449, 295);
      this.threadCount.Name = "threadCount";
      this.threadCount.PreCondition = null;
      this.threadCount.Required = false;
      this.threadCount.Size = new System.Drawing.Size(212, 23);
      this.threadCount.TabIndex = 16;
      this.threadCount.TextWidth = 62;
      this.threadCount.Value = 1;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(1139, 22);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(80, 23);
      this.btnLoad.TabIndex = 18;
      this.btnLoad.Text = "&Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // rbPeptideInAllTimePointOnly
      // 
      this.rbPeptideInAllTimePointOnly.AutoSize = true;
      this.rbPeptideInAllTimePointOnly.Key = "PeptideInAllTimePointOnly";
      this.rbPeptideInAllTimePointOnly.Location = new System.Drawing.Point(841, 324);
      this.rbPeptideInAllTimePointOnly.Name = "rbPeptideInAllTimePointOnly";
      this.rbPeptideInAllTimePointOnly.PreCondition = null;
      this.rbPeptideInAllTimePointOnly.Size = new System.Drawing.Size(177, 17);
      this.rbPeptideInAllTimePointOnly.TabIndex = 15;
      this.rbPeptideInAllTimePointOnly.Text = "Use peptide in all time point only";
      this.rbPeptideInAllTimePointOnly.UseVisualStyleBackColor = true;
      // 
      // pnlClassification
      // 
      this.pnlClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlClassification.Description = "Time point setup";
      this.pnlClassification.GetName = null;
      this.pnlClassification.Location = new System.Drawing.Point(759, 51);
      this.pnlClassification.Name = "pnlClassification";
      this.pnlClassification.Pattern = "(.*)";
      this.pnlClassification.Size = new System.Drawing.Size(460, 229);
      this.pnlClassification.TabIndex = 17;
      // 
      // rawFiles
      // 
      this.rawFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.rawFiles.FileArgument = null;
      this.rawFiles.FileDescription = "Raw files";
      this.rawFiles.FileNames = new string[0];
      this.rawFiles.Key = "File";
      this.rawFiles.Location = new System.Drawing.Point(15, 55);
      this.rawFiles.Name = "rawFiles";
      this.rawFiles.SelectedIndex = -1;
      this.rawFiles.SelectedItem = null;
      this.rawFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.rawFiles.Size = new System.Drawing.Size(709, 228);
      this.rawFiles.TabIndex = 19;
      // 
      // minimumProfileCorrelation
      // 
      this.minimumProfileCorrelation.Caption = "Minimum profile correlation:";
      this.minimumProfileCorrelation.CaptionWidth = 150;
      this.minimumProfileCorrelation.DefaultValue = "0.0";
      this.minimumProfileCorrelation.Description = "";
      this.minimumProfileCorrelation.Key = "minimumProfileCorrelation";
      this.minimumProfileCorrelation.Location = new System.Drawing.Point(231, 324);
      this.minimumProfileCorrelation.Name = "minimumProfileCorrelation";
      this.minimumProfileCorrelation.PreCondition = null;
      this.minimumProfileCorrelation.Required = false;
      this.minimumProfileCorrelation.Size = new System.Drawing.Size(212, 23);
      this.minimumProfileCorrelation.TabIndex = 16;
      this.minimumProfileCorrelation.TextWidth = 62;
      this.minimumProfileCorrelation.Value = 0.9D;
      // 
      // DeuteriumCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1231, 768);
      this.Controls.Add(this.rawFiles);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.pnlClassification);
      this.Controls.Add(this.minimumProfileCorrelation);
      this.Controls.Add(this.maximumProfileDistance);
      this.Controls.Add(this.minimumIsotopicPercentage);
      this.Controls.Add(this.threadCount);
      this.Controls.Add(this.minimumProfileLength);
      this.Controls.Add(this.precursorPPMTolerance);
      this.Controls.Add(this.rbPeptideInAllTimePointOnly);
      this.Controls.Add(this.cbExcludeIsotopic0InFormula);
      this.Controls.Add(this.cbDrawImage);
      this.Controls.Add(this.cbOverwrite);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtLog);
      this.Controls.Add(this.txtNoredundantFile);
      this.Controls.Add(this.btnNoredundantFile);
      this.Name = "DeuteriumCalculatorUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.btnNoredundantFile, 0);
      this.Controls.SetChildIndex(this.txtNoredundantFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtLog, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbOverwrite, 0);
      this.Controls.SetChildIndex(this.cbDrawImage, 0);
      this.Controls.SetChildIndex(this.cbExcludeIsotopic0InFormula, 0);
      this.Controls.SetChildIndex(this.rbPeptideInAllTimePointOnly, 0);
      this.Controls.SetChildIndex(this.precursorPPMTolerance, 0);
      this.Controls.SetChildIndex(this.minimumProfileLength, 0);
      this.Controls.SetChildIndex(this.threadCount, 0);
      this.Controls.SetChildIndex(this.minimumIsotopicPercentage, 0);
      this.Controls.SetChildIndex(this.maximumProfileDistance, 0);
      this.Controls.SetChildIndex(this.minimumProfileCorrelation, 0);
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.rawFiles, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnNoredundantFile;
    private System.Windows.Forms.TextBox txtNoredundantFile;
    private System.Windows.Forms.RichTextBox txtLog;
    private System.Windows.Forms.Label label1;
    private Gui.RcpaCheckField cbOverwrite;
    private Gui.RcpaCheckField cbDrawImage;
    private Gui.RcpaCheckField cbExcludeIsotopic0InFormula;
    private Gui.IntegerField precursorPPMTolerance;
    private Gui.IntegerField minimumProfileLength;
    private Gui.DoubleField minimumIsotopicPercentage;
    private Gui.DoubleField maximumProfileDistance;
    private Gui.IntegerField threadCount;
    private ClassificationPanel pnlClassification;
    private System.Windows.Forms.Button btnLoad;
    private Gui.RcpaCheckField rbPeptideInAllTimePointOnly;
    private Gui.MultipleFileField rawFiles;
    private Gui.DoubleField minimumProfileCorrelation;
  }
}
