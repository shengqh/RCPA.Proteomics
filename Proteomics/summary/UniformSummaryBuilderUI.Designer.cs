namespace RCPA.Tools.Summary
{
  partial class UniformSummaryBuilderUI
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
      this.txtMinOneHitWonderPeptideCount = new System.Windows.Forms.TextBox();
      this.FilterOneHitWonder = new RCPA.Gui.RcpaCheckField();
      this.cbMergeResultFromSameEngineButDifferentSearchParameters = new RCPA.Gui.RcpaCheckField();
      this.cbIndividual = new RCPA.Gui.RcpaCheckField();
      this.txtMinAgreeCount = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtContaminantDescriptionPattern = new System.Windows.Forms.TextBox();
      this.cbConflictAsDecoy = new System.Windows.Forms.ComboBox();
      this.label9 = new System.Windows.Forms.Label();
      this.cbClassifyByPreteaseTermini = new System.Windows.Forms.CheckBox();
      this.cbConflict = new System.Windows.Forms.ComboBox();
      this.lblConflict = new System.Windows.Forms.Label();
      this.txtDatabase = new System.Windows.Forms.TextBox();
      this.btnDatabase = new System.Windows.Forms.Button();
      this.cbAccessNumberPattern = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.txtMinSequenceLength = new System.Windows.Forms.TextBox();
      this.cbSequenceLength = new System.Windows.Forms.CheckBox();
      this.txtContaminantString = new System.Windows.Forms.TextBox();
      this.cbPeptideCount = new System.Windows.Forms.CheckBox();
      this.lblMaxPeptideFdr = new System.Windows.Forms.Label();
      this.txtMaxPeptideFdr = new System.Windows.Forms.TextBox();
      this.cbClassifyByMissCleavage = new System.Windows.Forms.CheckBox();
      this.cbClassifyByCharge = new System.Windows.Forms.CheckBox();
      this.cbRemoveDecoyEntry = new System.Windows.Forms.CheckBox();
      this.txtDecoyPattern = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtFdrModifiedAminoacids = new System.Windows.Forms.TextBox();
      this.cbClassifyByModification = new System.Windows.Forms.CheckBox();
      this.cbFdrLevel = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.cbFdrType = new System.Windows.Forms.ComboBox();
      this.txtMaxFdr = new System.Windows.Forms.TextBox();
      this.cbFilterByFDR = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbRemoveContamination = new System.Windows.Forms.CheckBox();
      this.tcDatasetList = new System.Windows.Forms.TabControl();
      this.pnlAdd = new System.Windows.Forms.Panel();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnSaveParam = new System.Windows.Forms.Button();
      this.btnLoadParam = new System.Windows.Forms.Button();
      this.btnNew = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tpGeneral = new System.Windows.Forms.TabPage();
      this.minDecoyScan = new RCPA.Gui.IntegerField();
      this.minTargetDecoyRatio = new RCPA.Gui.DoubleField();
      this.rbByDecoyDatabase = new RCPA.Gui.RcpaRadioField();
      this.rbByDecoySpectra = new RCPA.Gui.RcpaRadioField();
      this.txtMaxMissCleavage = new System.Windows.Forms.TextBox();
      this.cbMaxMissCleavage = new System.Windows.Forms.CheckBox();
      this.label10 = new System.Windows.Forms.Label();
      this.txtMinimumSpectraPerGroup = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.txtProteinTag = new System.Windows.Forms.TextBox();
      this.cbClassifyByProteinTag = new System.Windows.Forms.CheckBox();
      this.tpDatasets = new System.Windows.Forms.TabPage();
      this.panel1 = new System.Windows.Forms.Panel();
      this.rbUseSelectedOnly = new RCPA.Gui.RcpaRadioField();
      this.rbUseAll = new RCPA.Gui.RcpaRadioField();
      this.pnlButton.SuspendLayout();
      this.pnlAdd.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tpGeneral.SuspendLayout();
      this.tpDatasets.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 679);
      this.lblProgress.Size = new System.Drawing.Size(1189, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 656);
      this.progressBar.Size = new System.Drawing.Size(1189, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Controls.Add(this.btnLoadParam);
      this.pnlButton.Controls.Add(this.btnNew);
      this.pnlButton.Controls.Add(this.btnSaveParam);
      this.pnlButton.Location = new System.Drawing.Point(0, 702);
      this.pnlButton.Size = new System.Drawing.Size(1189, 39);
      this.pnlButton.Controls.SetChildIndex(this.btnGo, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnCancel, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnSaveParam, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnClose, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnNew, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnLoadParam, 0);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(653, 7);
      this.btnClose.Size = new System.Drawing.Size(97, 25);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(546, 7);
      this.btnCancel.Size = new System.Drawing.Size(97, 25);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(439, 7);
      this.btnGo.Size = new System.Drawing.Size(97, 25);
      // 
      // txtMinOneHitWonderPeptideCount
      // 
      this.txtMinOneHitWonderPeptideCount.Location = new System.Drawing.Point(264, 91);
      this.txtMinOneHitWonderPeptideCount.Name = "txtMinOneHitWonderPeptideCount";
      this.txtMinOneHitWonderPeptideCount.Size = new System.Drawing.Size(47, 20);
      this.txtMinOneHitWonderPeptideCount.TabIndex = 85;
      // 
      // FilterOneHitWonder
      // 
      this.FilterOneHitWonder.AutoSize = true;
      this.FilterOneHitWonder.Key = "FilterOneHitWonder";
      this.FilterOneHitWonder.Location = new System.Drawing.Point(25, 93);
      this.FilterOneHitWonder.Name = "FilterOneHitWonder";
      this.FilterOneHitWonder.PreCondition = null;
      this.FilterOneHitWonder.Size = new System.Drawing.Size(245, 17);
      this.FilterOneHitWonder.TabIndex = 144;
      this.FilterOneHitWonder.Text = "For one-hit-wonders, minimum peptide count = ";
      this.FilterOneHitWonder.UseVisualStyleBackColor = true;
      // 
      // cbMergeResultFromSameEngineButDifferentSearchParameters
      // 
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Key = "MergeResultFromSameEngineButDifferentSearchParameters";
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Location = new System.Drawing.Point(25, 509);
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Name = "cbMergeResultFromSameEngineButDifferentSearchParameters";
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.PreCondition = null;
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Size = new System.Drawing.Size(470, 21);
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.TabIndex = 143;
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Text = "Keep top score peptide only from same spectrum and same engine";
      // 
      // cbIndividual
      // 
      this.cbIndividual.Key = "RunIndividual";
      this.cbIndividual.Location = new System.Drawing.Point(6, 615);
      this.cbIndividual.Name = "cbIndividual";
      this.cbIndividual.PreCondition = null;
      this.cbIndividual.Size = new System.Drawing.Size(222, 21);
      this.cbIndividual.TabIndex = 142;
      this.cbIndividual.Text = "Generate individual results";
      // 
      // txtMinAgreeCount
      // 
      this.txtMinAgreeCount.Location = new System.Drawing.Point(421, 530);
      this.txtMinAgreeCount.Name = "txtMinAgreeCount";
      this.txtMinAgreeCount.Size = new System.Drawing.Size(93, 20);
      this.txtMinAgreeCount.TabIndex = 105;
      this.txtMinAgreeCount.Text = "1";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(25, 533);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(397, 13);
      this.label6.TabIndex = 104;
      this.label6.Text = "If the data are searched by multiple engines, the minimum agree count of engines " +
    "=";
      // 
      // txtContaminantDescriptionPattern
      // 
      this.txtContaminantDescriptionPattern.Location = new System.Drawing.Point(481, 592);
      this.txtContaminantDescriptionPattern.Name = "txtContaminantDescriptionPattern";
      this.txtContaminantDescriptionPattern.Size = new System.Drawing.Size(93, 20);
      this.txtContaminantDescriptionPattern.TabIndex = 103;
      // 
      // cbConflictAsDecoy
      // 
      this.cbConflictAsDecoy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbConflictAsDecoy.FormattingEnabled = true;
      this.cbConflictAsDecoy.Items.AddRange(new object[] {
            "Protein",
            "Peptide",
            "Unique peptide"});
      this.cbConflictAsDecoy.Location = new System.Drawing.Point(799, 294);
      this.cbConflictAsDecoy.Name = "cbConflictAsDecoy";
      this.cbConflictAsDecoy.Size = new System.Drawing.Size(178, 21);
      this.cbConflictAsDecoy.TabIndex = 101;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(419, 297);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(379, 13);
      this.label9.TabIndex = 100;
      this.label9.Text = "If one spectrum is matched with both target and decoy entries, it will be treated" +
    " ";
      // 
      // cbClassifyByPreteaseTermini
      // 
      this.cbClassifyByPreteaseTermini.AutoSize = true;
      this.cbClassifyByPreteaseTermini.Location = new System.Drawing.Point(194, 167);
      this.cbClassifyByPreteaseTermini.Name = "cbClassifyByPreteaseTermini";
      this.cbClassifyByPreteaseTermini.Size = new System.Drawing.Size(168, 17);
      this.cbClassifyByPreteaseTermini.TabIndex = 98;
      this.cbClassifyByPreteaseTermini.Text = "the number of protease termini";
      this.cbClassifyByPreteaseTermini.UseVisualStyleBackColor = true;
      // 
      // cbConflict
      // 
      this.cbConflict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbConflict.FormattingEnabled = true;
      this.cbConflict.Items.AddRange(new object[] {
            "Protein",
            "Peptide",
            "Unique peptide"});
      this.cbConflict.Location = new System.Drawing.Point(454, 482);
      this.cbConflict.Name = "cbConflict";
      this.cbConflict.Size = new System.Drawing.Size(178, 21);
      this.cbConflict.TabIndex = 97;
      // 
      // lblConflict
      // 
      this.lblConflict.AutoSize = true;
      this.lblConflict.Location = new System.Drawing.Point(3, 485);
      this.lblConflict.Name = "lblConflict";
      this.lblConflict.Size = new System.Drawing.Size(436, 13);
      this.lblConflict.TabIndex = 96;
      this.lblConflict.Text = "If Peptide-Spectrum-Matches from different search engines are conflict, what is y" +
    "our perfer :";
      // 
      // txtDatabase
      // 
      this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDatabase.Location = new System.Drawing.Point(224, 383);
      this.txtDatabase.Name = "txtDatabase";
      this.txtDatabase.Size = new System.Drawing.Size(949, 20);
      this.txtDatabase.TabIndex = 95;
      // 
      // btnDatabase
      // 
      this.btnDatabase.Location = new System.Drawing.Point(6, 380);
      this.btnDatabase.Name = "btnDatabase";
      this.btnDatabase.Size = new System.Drawing.Size(212, 25);
      this.btnDatabase.TabIndex = 94;
      this.btnDatabase.Text = "Browse database ...";
      this.btnDatabase.UseVisualStyleBackColor = true;
      // 
      // cbAccessNumberPattern
      // 
      this.cbAccessNumberPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbAccessNumberPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbAccessNumberPattern.FormattingEnabled = true;
      this.cbAccessNumberPattern.Items.AddRange(new object[] {
            "(IPI\\d+)",
            "(gi:\\d+)",
            "(IPI\\d+|gi:\\d+)"});
      this.cbAccessNumberPattern.Location = new System.Drawing.Point(158, 414);
      this.cbAccessNumberPattern.Name = "cbAccessNumberPattern";
      this.cbAccessNumberPattern.Size = new System.Drawing.Size(1015, 21);
      this.cbAccessNumberPattern.TabIndex = 93;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(22, 417);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(121, 13);
      this.label8.TabIndex = 92;
      this.label8.Text = "Access number format =";
      // 
      // txtMinSequenceLength
      // 
      this.txtMinSequenceLength.Location = new System.Drawing.Point(199, 340);
      this.txtMinSequenceLength.Name = "txtMinSequenceLength";
      this.txtMinSequenceLength.Size = new System.Drawing.Size(94, 20);
      this.txtMinSequenceLength.TabIndex = 91;
      // 
      // cbSequenceLength
      // 
      this.cbSequenceLength.AutoSize = true;
      this.cbSequenceLength.Location = new System.Drawing.Point(6, 342);
      this.cbSequenceLength.Name = "cbSequenceLength";
      this.cbSequenceLength.Size = new System.Drawing.Size(194, 17);
      this.cbSequenceLength.TabIndex = 90;
      this.cbSequenceLength.Text = "Filter by minmum sequence length =";
      this.cbSequenceLength.UseVisualStyleBackColor = true;
      // 
      // txtContaminantString
      // 
      this.txtContaminantString.Location = new System.Drawing.Point(247, 590);
      this.txtContaminantString.Name = "txtContaminantString";
      this.txtContaminantString.Size = new System.Drawing.Size(93, 20);
      this.txtContaminantString.TabIndex = 89;
      // 
      // cbPeptideCount
      // 
      this.cbPeptideCount.AutoSize = true;
      this.cbPeptideCount.Location = new System.Drawing.Point(25, 70);
      this.cbPeptideCount.Name = "cbPeptideCount";
      this.cbPeptideCount.Size = new System.Drawing.Size(397, 17);
      this.cbPeptideCount.TabIndex = 87;
      this.cbPeptideCount.Text = "For two hit proteins, iterate peptide limitation (10 -> 2) to find max protein gr" +
    "oups";
      this.cbPeptideCount.UseVisualStyleBackColor = true;
      // 
      // lblMaxPeptideFdr
      // 
      this.lblMaxPeptideFdr.AutoSize = true;
      this.lblMaxPeptideFdr.Location = new System.Drawing.Point(492, 20);
      this.lblMaxPeptideFdr.Name = "lblMaxPeptideFdr";
      this.lblMaxPeptideFdr.Size = new System.Drawing.Size(109, 13);
      this.lblMaxPeptideFdr.TabIndex = 86;
      this.lblMaxPeptideFdr.Text = "maximum peptide fdr=";
      // 
      // txtMaxPeptideFdr
      // 
      this.txtMaxPeptideFdr.Location = new System.Drawing.Point(607, 17);
      this.txtMaxPeptideFdr.Name = "txtMaxPeptideFdr";
      this.txtMaxPeptideFdr.Size = new System.Drawing.Size(47, 20);
      this.txtMaxPeptideFdr.TabIndex = 85;
      // 
      // cbClassifyByMissCleavage
      // 
      this.cbClassifyByMissCleavage.AutoSize = true;
      this.cbClassifyByMissCleavage.Location = new System.Drawing.Point(194, 144);
      this.cbClassifyByMissCleavage.Name = "cbClassifyByMissCleavage";
      this.cbClassifyByMissCleavage.Size = new System.Drawing.Size(166, 17);
      this.cbClassifyByMissCleavage.TabIndex = 83;
      this.cbClassifyByMissCleavage.Text = "missed internal cleavage sites";
      this.cbClassifyByMissCleavage.UseVisualStyleBackColor = true;
      // 
      // cbClassifyByCharge
      // 
      this.cbClassifyByCharge.AutoSize = true;
      this.cbClassifyByCharge.Location = new System.Drawing.Point(194, 121);
      this.cbClassifyByCharge.Name = "cbClassifyByCharge";
      this.cbClassifyByCharge.Size = new System.Drawing.Size(106, 17);
      this.cbClassifyByCharge.TabIndex = 84;
      this.cbClassifyByCharge.Text = "precursor charge";
      this.cbClassifyByCharge.UseVisualStyleBackColor = true;
      // 
      // cbRemoveDecoyEntry
      // 
      this.cbRemoveDecoyEntry.AutoSize = true;
      this.cbRemoveDecoyEntry.Location = new System.Drawing.Point(6, 569);
      this.cbRemoveDecoyEntry.Name = "cbRemoveDecoyEntry";
      this.cbRemoveDecoyEntry.Size = new System.Drawing.Size(223, 17);
      this.cbRemoveDecoyEntry.TabIndex = 82;
      this.cbRemoveDecoyEntry.Text = "Remove candidates from decoy database";
      this.cbRemoveDecoyEntry.UseVisualStyleBackColor = true;
      // 
      // txtDecoyPattern
      // 
      this.txtDecoyPattern.Location = new System.Drawing.Point(304, 294);
      this.txtDecoyPattern.Name = "txtDecoyPattern";
      this.txtDecoyPattern.Size = new System.Drawing.Size(97, 20);
      this.txtDecoyPattern.TabIndex = 81;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(22, 122);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(166, 13);
      this.label5.TabIndex = 79;
      this.label5.Text = "Group peptide-spectrum-match by";
      // 
      // txtFdrModifiedAminoacids
      // 
      this.txtFdrModifiedAminoacids.Location = new System.Drawing.Point(372, 188);
      this.txtFdrModifiedAminoacids.Name = "txtFdrModifiedAminoacids";
      this.txtFdrModifiedAminoacids.Size = new System.Drawing.Size(134, 20);
      this.txtFdrModifiedAminoacids.TabIndex = 78;
      // 
      // cbClassifyByModification
      // 
      this.cbClassifyByModification.AutoSize = true;
      this.cbClassifyByModification.Location = new System.Drawing.Point(194, 190);
      this.cbClassifyByModification.Name = "cbClassifyByModification";
      this.cbClassifyByModification.Size = new System.Drawing.Size(633, 17);
      this.cbClassifyByModification.TabIndex = 77;
      this.cbClassifyByModification.Text = "modification state, amino acids =                                                " +
    "      , \'[\' indicates N-terminal and \']\' indicates C-terminal modification";
      this.cbClassifyByModification.UseVisualStyleBackColor = true;
      // 
      // cbFdrLevel
      // 
      this.cbFdrLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFdrLevel.FormattingEnabled = true;
      this.cbFdrLevel.Items.AddRange(new object[] {
            "Protein",
            "Peptide",
            "Unique peptide"});
      this.cbFdrLevel.Location = new System.Drawing.Point(264, 17);
      this.cbFdrLevel.Name = "cbFdrLevel";
      this.cbFdrLevel.Size = new System.Drawing.Size(178, 21);
      this.cbFdrLevel.TabIndex = 76;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(451, 20);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(32, 13);
      this.label4.TabIndex = 74;
      this.label4.Text = "level,";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(244, 20);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(16, 13);
      this.label2.TabIndex = 75;
      this.label2.Text = "at";
      // 
      // cbFdrType
      // 
      this.cbFdrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFdrType.FormattingEnabled = true;
      this.cbFdrType.Location = new System.Drawing.Point(145, 43);
      this.cbFdrType.Name = "cbFdrType";
      this.cbFdrType.Size = new System.Drawing.Size(379, 21);
      this.cbFdrType.TabIndex = 71;
      // 
      // txtMaxFdr
      // 
      this.txtMaxFdr.Location = new System.Drawing.Point(187, 17);
      this.txtMaxFdr.Name = "txtMaxFdr";
      this.txtMaxFdr.Size = new System.Drawing.Size(47, 20);
      this.txtMaxFdr.TabIndex = 73;
      // 
      // cbFilterByFDR
      // 
      this.cbFilterByFDR.AutoSize = true;
      this.cbFilterByFDR.Location = new System.Drawing.Point(6, 19);
      this.cbFilterByFDR.Name = "cbFilterByFDR";
      this.cbFilterByFDR.Size = new System.Drawing.Size(181, 17);
      this.cbFilterByFDR.TabIndex = 72;
      this.cbFilterByFDR.Text = "Filter by False Discovery Rate <=";
      this.cbFilterByFDR.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(22, 46);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(117, 13);
      this.label1.TabIndex = 70;
      this.label1.Text = "False Discovery Rate =";
      // 
      // cbRemoveContamination
      // 
      this.cbRemoveContamination.AutoSize = true;
      this.cbRemoveContamination.Location = new System.Drawing.Point(6, 592);
      this.cbRemoveContamination.Name = "cbRemoveContamination";
      this.cbRemoveContamination.Size = new System.Drawing.Size(472, 17);
      this.cbRemoveContamination.TabIndex = 102;
      this.cbRemoveContamination.Text = "Remove contaminant protein by name pattern                                       " +
    "     or description pattern";
      this.cbRemoveContamination.UseVisualStyleBackColor = true;
      // 
      // tcDatasetList
      // 
      this.tcDatasetList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tcDatasetList.Location = new System.Drawing.Point(142, 35);
      this.tcDatasetList.Name = "tcDatasetList";
      this.tcDatasetList.SelectedIndex = 0;
      this.tcDatasetList.Size = new System.Drawing.Size(1036, 592);
      this.tcDatasetList.TabIndex = 3;
      this.tcDatasetList.DoubleClick += new System.EventHandler(this.btnDelete_Click);
      // 
      // pnlAdd
      // 
      this.pnlAdd.Controls.Add(this.btnDelete);
      this.pnlAdd.Dock = System.Windows.Forms.DockStyle.Left;
      this.pnlAdd.Location = new System.Drawing.Point(3, 35);
      this.pnlAdd.Name = "pnlAdd";
      this.pnlAdd.Size = new System.Drawing.Size(139, 592);
      this.pnlAdd.TabIndex = 2;
      // 
      // btnDelete
      // 
      this.btnDelete.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnDelete.Location = new System.Drawing.Point(0, 0);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(139, 25);
      this.btnDelete.TabIndex = 13;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnSaveParam
      // 
      this.btnSaveParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSaveParam.Location = new System.Drawing.Point(859, 7);
      this.btnSaveParam.Name = "btnSaveParam";
      this.btnSaveParam.Size = new System.Drawing.Size(97, 25);
      this.btnSaveParam.TabIndex = 29;
      this.btnSaveParam.Text = "&Save param";
      this.btnSaveParam.UseVisualStyleBackColor = true;
      this.btnSaveParam.Click += new System.EventHandler(this.btnSaveParam_Click);
      // 
      // btnLoadParam
      // 
      this.btnLoadParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnLoadParam.Location = new System.Drawing.Point(756, 7);
      this.btnLoadParam.Name = "btnLoadParam";
      this.btnLoadParam.Size = new System.Drawing.Size(97, 25);
      this.btnLoadParam.TabIndex = 28;
      this.btnLoadParam.Text = "&Load param";
      this.btnLoadParam.UseVisualStyleBackColor = true;
      this.btnLoadParam.Click += new System.EventHandler(this.btnLoadParam_Click);
      // 
      // btnNew
      // 
      this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnNew.Location = new System.Drawing.Point(962, 7);
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new System.Drawing.Size(97, 25);
      this.btnNew.TabIndex = 30;
      this.btnNew.Text = "&New";
      this.btnNew.UseVisualStyleBackColor = true;
      this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tpGeneral);
      this.tabControl1.Controls.Add(this.tpDatasets);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1189, 656);
      this.tabControl1.TabIndex = 31;
      // 
      // tpGeneral
      // 
      this.tpGeneral.Controls.Add(this.minDecoyScan);
      this.tpGeneral.Controls.Add(this.minTargetDecoyRatio);
      this.tpGeneral.Controls.Add(this.rbByDecoyDatabase);
      this.tpGeneral.Controls.Add(this.rbByDecoySpectra);
      this.tpGeneral.Controls.Add(this.txtMaxMissCleavage);
      this.tpGeneral.Controls.Add(this.cbMaxMissCleavage);
      this.tpGeneral.Controls.Add(this.label10);
      this.tpGeneral.Controls.Add(this.txtMinimumSpectraPerGroup);
      this.tpGeneral.Controls.Add(this.label7);
      this.tpGeneral.Controls.Add(this.txtMinOneHitWonderPeptideCount);
      this.tpGeneral.Controls.Add(this.FilterOneHitWonder);
      this.tpGeneral.Controls.Add(this.cbMergeResultFromSameEngineButDifferentSearchParameters);
      this.tpGeneral.Controls.Add(this.cbIndividual);
      this.tpGeneral.Controls.Add(this.txtMinAgreeCount);
      this.tpGeneral.Controls.Add(this.label6);
      this.tpGeneral.Controls.Add(this.txtContaminantDescriptionPattern);
      this.tpGeneral.Controls.Add(this.cbConflictAsDecoy);
      this.tpGeneral.Controls.Add(this.label9);
      this.tpGeneral.Controls.Add(this.cbClassifyByPreteaseTermini);
      this.tpGeneral.Controls.Add(this.cbConflict);
      this.tpGeneral.Controls.Add(this.lblConflict);
      this.tpGeneral.Controls.Add(this.txtDatabase);
      this.tpGeneral.Controls.Add(this.btnDatabase);
      this.tpGeneral.Controls.Add(this.cbAccessNumberPattern);
      this.tpGeneral.Controls.Add(this.label8);
      this.tpGeneral.Controls.Add(this.txtMinSequenceLength);
      this.tpGeneral.Controls.Add(this.cbSequenceLength);
      this.tpGeneral.Controls.Add(this.txtContaminantString);
      this.tpGeneral.Controls.Add(this.cbPeptideCount);
      this.tpGeneral.Controls.Add(this.lblMaxPeptideFdr);
      this.tpGeneral.Controls.Add(this.txtMaxPeptideFdr);
      this.tpGeneral.Controls.Add(this.cbClassifyByMissCleavage);
      this.tpGeneral.Controls.Add(this.cbClassifyByCharge);
      this.tpGeneral.Controls.Add(this.cbRemoveDecoyEntry);
      this.tpGeneral.Controls.Add(this.txtDecoyPattern);
      this.tpGeneral.Controls.Add(this.label5);
      this.tpGeneral.Controls.Add(this.txtProteinTag);
      this.tpGeneral.Controls.Add(this.txtFdrModifiedAminoacids);
      this.tpGeneral.Controls.Add(this.cbClassifyByProteinTag);
      this.tpGeneral.Controls.Add(this.cbClassifyByModification);
      this.tpGeneral.Controls.Add(this.cbFdrLevel);
      this.tpGeneral.Controls.Add(this.label4);
      this.tpGeneral.Controls.Add(this.label2);
      this.tpGeneral.Controls.Add(this.cbFdrType);
      this.tpGeneral.Controls.Add(this.txtMaxFdr);
      this.tpGeneral.Controls.Add(this.cbFilterByFDR);
      this.tpGeneral.Controls.Add(this.label1);
      this.tpGeneral.Controls.Add(this.cbRemoveContamination);
      this.tpGeneral.Location = new System.Drawing.Point(4, 22);
      this.tpGeneral.Name = "tpGeneral";
      this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
      this.tpGeneral.Size = new System.Drawing.Size(1181, 630);
      this.tpGeneral.TabIndex = 0;
      this.tpGeneral.Text = "General";
      this.tpGeneral.UseVisualStyleBackColor = true;
      // 
      // minDecoyScan
      // 
      this.minDecoyScan.Caption = "";
      this.minDecoyScan.CaptionWidth = 0;
      this.minDecoyScan.DefaultValue = "10000000";
      this.minDecoyScan.Description = "";
      this.minDecoyScan.Key = "IntegerField";
      this.minDecoyScan.Location = new System.Drawing.Point(350, 271);
      this.minDecoyScan.Name = "minDecoyScan";
      this.minDecoyScan.PreCondition = null;
      this.minDecoyScan.Required = false;
      this.minDecoyScan.Size = new System.Drawing.Size(89, 23);
      this.minDecoyScan.TabIndex = 151;
      this.minDecoyScan.TextWidth = 89;
      this.minDecoyScan.Value = 10000000;
      // 
      // minTargetDecoyRatio
      // 
      this.minTargetDecoyRatio.Caption = "minimum target/decoy ratio for target protein assignment:";
      this.minTargetDecoyRatio.CaptionWidth = 300;
      this.minTargetDecoyRatio.DefaultValue = "1";
      this.minTargetDecoyRatio.Description = "";
      this.minTargetDecoyRatio.Key = "MinTargetDecoyRatio";
      this.minTargetDecoyRatio.Location = new System.Drawing.Point(421, 271);
      this.minTargetDecoyRatio.Name = "minTargetDecoyRatio";
      this.minTargetDecoyRatio.PreCondition = null;
      this.minTargetDecoyRatio.Required = false;
      this.minTargetDecoyRatio.Size = new System.Drawing.Size(371, 23);
      this.minTargetDecoyRatio.TabIndex = 153;
      this.minTargetDecoyRatio.TextWidth = 71;
      this.minTargetDecoyRatio.Value = 1D;
      // 
      // rbByDecoyDatabase
      // 
      this.rbByDecoyDatabase.AutoSize = true;
      this.rbByDecoyDatabase.Checked = true;
      this.rbByDecoyDatabase.Key = "ByDecoyDatabase";
      this.rbByDecoyDatabase.Location = new System.Drawing.Point(28, 294);
      this.rbByDecoyDatabase.Name = "rbByDecoyDatabase";
      this.rbByDecoyDatabase.PreCondition = null;
      this.rbByDecoyDatabase.Size = new System.Drawing.Size(270, 17);
      this.rbByDecoyDatabase.TabIndex = 152;
      this.rbByDecoyDatabase.TabStop = true;
      this.rbByDecoyDatabase.Text = "Calculate FDR by decoy database, decoy pattern = ";
      this.rbByDecoyDatabase.UseVisualStyleBackColor = true;
      // 
      // rbByDecoySpectra
      // 
      this.rbByDecoySpectra.AutoSize = true;
      this.rbByDecoySpectra.Key = "ByDecoySpectra";
      this.rbByDecoySpectra.Location = new System.Drawing.Point(28, 271);
      this.rbByDecoySpectra.Name = "rbByDecoySpectra";
      this.rbByDecoySpectra.PreCondition = null;
      this.rbByDecoySpectra.Size = new System.Drawing.Size(318, 17);
      this.rbByDecoySpectra.TabIndex = 152;
      this.rbByDecoySpectra.Text = "Calculate FDR by decoy spectra, the minimum scan number = ";
      this.rbByDecoySpectra.UseVisualStyleBackColor = true;
      // 
      // txtMaxMissCleavage
      // 
      this.txtMaxMissCleavage.Location = new System.Drawing.Point(620, 340);
      this.txtMaxMissCleavage.Name = "txtMaxMissCleavage";
      this.txtMaxMissCleavage.Size = new System.Drawing.Size(94, 20);
      this.txtMaxMissCleavage.TabIndex = 149;
      // 
      // cbMaxMissCleavage
      // 
      this.cbMaxMissCleavage.AutoSize = true;
      this.cbMaxMissCleavage.Location = new System.Drawing.Point(338, 342);
      this.cbMaxMissCleavage.Name = "cbMaxMissCleavage";
      this.cbMaxMissCleavage.Size = new System.Drawing.Size(289, 17);
      this.cbMaxMissCleavage.TabIndex = 148;
      this.cbMaxMissCleavage.Text = "Filter by max number of missed internal cleavage sites = ";
      this.cbMaxMissCleavage.UseVisualStyleBackColor = true;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(190, 240);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(183, 13);
      this.label10.TabIndex = 147;
      this.label10.Text = "maximum spectrum count per group =";
      // 
      // txtMinimumSpectraPerGroup
      // 
      this.txtMinimumSpectraPerGroup.Location = new System.Drawing.Point(374, 238);
      this.txtMinimumSpectraPerGroup.Name = "txtMinimumSpectraPerGroup";
      this.txtMinimumSpectraPerGroup.Size = new System.Drawing.Size(47, 20);
      this.txtMinimumSpectraPerGroup.TabIndex = 146;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.label7.Location = new System.Drawing.Point(25, 447);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(704, 13);
      this.label7.TabIndex = 145;
      this.label7.Text = "if your data is MSF format with decoy searching, set \"REVERSED_\" as decoy databas" +
    "e pattern and select \"Whole Name\" as access number format";
      // 
      // txtProteinTag
      // 
      this.txtProteinTag.Location = new System.Drawing.Point(278, 211);
      this.txtProteinTag.Name = "txtProteinTag";
      this.txtProteinTag.Size = new System.Drawing.Size(134, 20);
      this.txtProteinTag.TabIndex = 78;
      // 
      // cbClassifyByProteinTag
      // 
      this.cbClassifyByProteinTag.AutoSize = true;
      this.cbClassifyByProteinTag.Location = new System.Drawing.Point(194, 213);
      this.cbClassifyByProteinTag.Name = "cbClassifyByProteinTag";
      this.cbClassifyByProteinTag.Size = new System.Drawing.Size(688, 17);
      this.cbClassifyByProteinTag.TabIndex = 77;
      this.cbClassifyByProteinTag.Text = "protein tag =                                                 , not regex pattern" +
    ", the PSM whose proteins all contain this tag will be classified as 1, otherwise" +
    " 0";
      this.cbClassifyByProteinTag.UseVisualStyleBackColor = true;
      // 
      // tpDatasets
      // 
      this.tpDatasets.Controls.Add(this.tcDatasetList);
      this.tpDatasets.Controls.Add(this.pnlAdd);
      this.tpDatasets.Controls.Add(this.panel1);
      this.tpDatasets.Location = new System.Drawing.Point(4, 22);
      this.tpDatasets.Name = "tpDatasets";
      this.tpDatasets.Padding = new System.Windows.Forms.Padding(3);
      this.tpDatasets.Size = new System.Drawing.Size(1181, 630);
      this.tpDatasets.TabIndex = 1;
      this.tpDatasets.Text = "Datasets";
      this.tpDatasets.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.rbUseSelectedOnly);
      this.panel1.Controls.Add(this.rbUseAll);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1175, 32);
      this.panel1.TabIndex = 4;
      // 
      // rbUseSelectedOnly
      // 
      this.rbUseSelectedOnly.AutoSize = true;
      this.rbUseSelectedOnly.Key = "rcpaRadioField1";
      this.rbUseSelectedOnly.Location = new System.Drawing.Point(257, 9);
      this.rbUseSelectedOnly.Name = "rbUseSelectedOnly";
      this.rbUseSelectedOnly.PreCondition = null;
      this.rbUseSelectedOnly.Size = new System.Drawing.Size(130, 17);
      this.rbUseSelectedOnly.TabIndex = 2;
      this.rbUseSelectedOnly.Text = "Use selected files only";
      this.rbUseSelectedOnly.UseVisualStyleBackColor = true;
      // 
      // rbUseAll
      // 
      this.rbUseAll.AutoSize = true;
      this.rbUseAll.Checked = true;
      this.rbUseAll.Key = "rbUseAll";
      this.rbUseAll.Location = new System.Drawing.Point(139, 9);
      this.rbUseAll.Name = "rbUseAll";
      this.rbUseAll.PreCondition = null;
      this.rbUseAll.Size = new System.Drawing.Size(78, 17);
      this.rbUseAll.TabIndex = 1;
      this.rbUseAll.TabStop = true;
      this.rbUseAll.Text = "Use all files";
      this.rbUseAll.UseVisualStyleBackColor = true;
      // 
      // UniformSummaryBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1189, 741);
      this.Controls.Add(this.tabControl1);
      this.Name = "UniformSummaryBuilderUI";
      this.TabText = "UniformBuildSummaryUI";
      this.Text = "UniformBuildSummaryUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.tabControl1, 0);
      this.pnlButton.ResumeLayout(false);
      this.pnlAdd.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tpGeneral.ResumeLayout(false);
      this.tpGeneral.PerformLayout();
      this.tpDatasets.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtMinSequenceLength;
    private System.Windows.Forms.CheckBox cbSequenceLength;
    private System.Windows.Forms.TextBox txtContaminantString;
    private System.Windows.Forms.CheckBox cbPeptideCount;
    private System.Windows.Forms.Label lblMaxPeptideFdr;
    private System.Windows.Forms.TextBox txtMaxPeptideFdr;
    private System.Windows.Forms.CheckBox cbClassifyByMissCleavage;
    private System.Windows.Forms.CheckBox cbClassifyByCharge;
    private System.Windows.Forms.CheckBox cbRemoveDecoyEntry;
    private System.Windows.Forms.TextBox txtDecoyPattern;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtFdrModifiedAminoacids;
    private System.Windows.Forms.CheckBox cbClassifyByModification;
    private System.Windows.Forms.ComboBox cbFdrLevel;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbFdrType;
    private System.Windows.Forms.TextBox txtMaxFdr;
    private System.Windows.Forms.CheckBox cbFilterByFDR;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtDatabase;
    private System.Windows.Forms.Button btnDatabase;
    private System.Windows.Forms.ComboBox cbAccessNumberPattern;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Button btnSaveParam;
    private System.Windows.Forms.Button btnLoadParam;
    private System.Windows.Forms.ComboBox cbConflict;
    private System.Windows.Forms.Label lblConflict;
    private System.Windows.Forms.Button btnNew;
    private System.Windows.Forms.CheckBox cbClassifyByPreteaseTermini;
    private System.Windows.Forms.ComboBox cbConflictAsDecoy;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtContaminantDescriptionPattern;
    private System.Windows.Forms.CheckBox cbRemoveContamination;
    private System.Windows.Forms.TextBox txtMinAgreeCount;
    private System.Windows.Forms.Label label6;
    private Gui.RcpaCheckField cbIndividual;
    private Gui.RcpaCheckField cbMergeResultFromSameEngineButDifferentSearchParameters;
    private System.Windows.Forms.TextBox txtMinOneHitWonderPeptideCount;
    private Gui.RcpaCheckField FilterOneHitWonder;
    private System.Windows.Forms.TabControl tcDatasetList;
    protected System.Windows.Forms.Panel pnlAdd;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tpGeneral;
    private System.Windows.Forms.TabPage tpDatasets;
    private System.Windows.Forms.Panel panel1;
    private Gui.RcpaRadioField rbUseSelectedOnly;
    private Gui.RcpaRadioField rbUseAll;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox txtMinimumSpectraPerGroup;
    private System.Windows.Forms.TextBox txtMaxMissCleavage;
    private System.Windows.Forms.CheckBox cbMaxMissCleavage;
    private Gui.RcpaRadioField rbByDecoyDatabase;
    private Gui.RcpaRadioField rbByDecoySpectra;
    private Gui.IntegerField minDecoyScan;
    private Gui.DoubleField minTargetDecoyRatio;
    private System.Windows.Forms.TextBox txtProteinTag;
    private System.Windows.Forms.CheckBox cbClassifyByProteinTag;
  }
}