namespace RCPA.Tools.Summary
{
  partial class UniformBuildSummaryUI
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
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.cbMergeResultFromSameEngineButDifferentSearchParameters = new RCPA.Gui.RcpaCheckField();
      this.cbIndividual = new RCPA.Gui.RcpaCheckField();
      this.txtMinAgreeCount = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtContaminantDescriptionPattern = new System.Windows.Forms.TextBox();
      this.cbConflictAsDecoy = new System.Windows.Forms.ComboBox();
      this.label9 = new System.Windows.Forms.Label();
      this.cbClassifyByPreteaseTermini = new System.Windows.Forms.CheckBox();
      this.cbConflict = new System.Windows.Forms.ComboBox();
      this.label7 = new System.Windows.Forms.Label();
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
      this.label3 = new System.Windows.Forms.Label();
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.btnAddPeptideProphet = new System.Windows.Forms.Button();
      this.btnPFind = new System.Windows.Forms.Button();
      this.btnXtandem = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.tcDatasetList = new System.Windows.Forms.TabControl();
      this.btnSaveParam = new System.Windows.Forms.Button();
      this.btnLoadParam = new System.Windows.Forms.Button();
      this.btnNew = new System.Windows.Forms.Button();
      this.FilterOneHitWonder = new RCPA.Gui.RcpaCheckField();
      this.txtMinOneHitWonderPeptideCount = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 697);
      this.lblProgress.Size = new System.Drawing.Size(1189, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 676);
      this.progressBar.Size = new System.Drawing.Size(1189, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(653, 6);
      this.btnClose.Size = new System.Drawing.Size(97, 23);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(546, 6);
      this.btnCancel.Size = new System.Drawing.Size(97, 23);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(439, 6);
      this.btnGo.Size = new System.Drawing.Size(97, 23);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.Location = new System.Drawing.Point(12, 12);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.txtMinOneHitWonderPeptideCount);
      this.splitContainer2.Panel1.Controls.Add(this.FilterOneHitWonder);
      this.splitContainer2.Panel1.Controls.Add(this.cbMergeResultFromSameEngineButDifferentSearchParameters);
      this.splitContainer2.Panel1.Controls.Add(this.cbIndividual);
      this.splitContainer2.Panel1.Controls.Add(this.txtMinAgreeCount);
      this.splitContainer2.Panel1.Controls.Add(this.label6);
      this.splitContainer2.Panel1.Controls.Add(this.txtContaminantDescriptionPattern);
      this.splitContainer2.Panel1.Controls.Add(this.cbConflictAsDecoy);
      this.splitContainer2.Panel1.Controls.Add(this.label9);
      this.splitContainer2.Panel1.Controls.Add(this.cbClassifyByPreteaseTermini);
      this.splitContainer2.Panel1.Controls.Add(this.cbConflict);
      this.splitContainer2.Panel1.Controls.Add(this.label7);
      this.splitContainer2.Panel1.Controls.Add(this.txtDatabase);
      this.splitContainer2.Panel1.Controls.Add(this.btnDatabase);
      this.splitContainer2.Panel1.Controls.Add(this.cbAccessNumberPattern);
      this.splitContainer2.Panel1.Controls.Add(this.label8);
      this.splitContainer2.Panel1.Controls.Add(this.txtMinSequenceLength);
      this.splitContainer2.Panel1.Controls.Add(this.cbSequenceLength);
      this.splitContainer2.Panel1.Controls.Add(this.txtContaminantString);
      this.splitContainer2.Panel1.Controls.Add(this.cbPeptideCount);
      this.splitContainer2.Panel1.Controls.Add(this.lblMaxPeptideFdr);
      this.splitContainer2.Panel1.Controls.Add(this.txtMaxPeptideFdr);
      this.splitContainer2.Panel1.Controls.Add(this.cbClassifyByMissCleavage);
      this.splitContainer2.Panel1.Controls.Add(this.cbClassifyByCharge);
      this.splitContainer2.Panel1.Controls.Add(this.cbRemoveDecoyEntry);
      this.splitContainer2.Panel1.Controls.Add(this.txtDecoyPattern);
      this.splitContainer2.Panel1.Controls.Add(this.label5);
      this.splitContainer2.Panel1.Controls.Add(this.label3);
      this.splitContainer2.Panel1.Controls.Add(this.txtFdrModifiedAminoacids);
      this.splitContainer2.Panel1.Controls.Add(this.cbClassifyByModification);
      this.splitContainer2.Panel1.Controls.Add(this.cbFdrLevel);
      this.splitContainer2.Panel1.Controls.Add(this.label4);
      this.splitContainer2.Panel1.Controls.Add(this.label2);
      this.splitContainer2.Panel1.Controls.Add(this.cbFdrType);
      this.splitContainer2.Panel1.Controls.Add(this.txtMaxFdr);
      this.splitContainer2.Panel1.Controls.Add(this.cbFilterByFDR);
      this.splitContainer2.Panel1.Controls.Add(this.label1);
      this.splitContainer2.Panel1.Controls.Add(this.cbRemoveContamination);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
      this.splitContainer2.Size = new System.Drawing.Size(1163, 645);
      this.splitContainer2.SplitterDistance = 296;
      this.splitContainer2.TabIndex = 8;
      // 
      // cbMergeResultFromSameEngineButDifferentSearchParameters
      // 
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Key = "MergeResultFromSameEngineButDifferentSearchParameters";
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Location = new System.Drawing.Point(669, 249);
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Name = "cbMergeResultFromSameEngineButDifferentSearchParameters";
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.PreCondition = null;
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Size = new System.Drawing.Size(470, 19);
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.TabIndex = 143;
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Text = "Keep top score peptide only from same spectrum and same engine";
      // 
      // cbIndividual
      // 
      this.cbIndividual.Key = "RunIndividual";
      this.cbIndividual.Location = new System.Drawing.Point(669, 274);
      this.cbIndividual.Name = "cbIndividual";
      this.cbIndividual.PreCondition = null;
      this.cbIndividual.Size = new System.Drawing.Size(222, 19);
      this.cbIndividual.TabIndex = 142;
      this.cbIndividual.Text = "Generate individual results";
      // 
      // txtMinAgreeCount
      // 
      this.txtMinAgreeCount.Location = new System.Drawing.Point(531, 272);
      this.txtMinAgreeCount.Name = "txtMinAgreeCount";
      this.txtMinAgreeCount.Size = new System.Drawing.Size(93, 21);
      this.txtMinAgreeCount.TabIndex = 105;
      this.txtMinAgreeCount.Text = "1";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(19, 275);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(497, 12);
      this.label6.TabIndex = 104;
      this.label6.Text = "If the data are searched by multiple engines, the minimum agree count of engines " +
    "=";
      // 
      // txtContaminantDescriptionPattern
      // 
      this.txtContaminantDescriptionPattern.Location = new System.Drawing.Point(531, 248);
      this.txtContaminantDescriptionPattern.Name = "txtContaminantDescriptionPattern";
      this.txtContaminantDescriptionPattern.Size = new System.Drawing.Size(93, 21);
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
      this.cbConflictAsDecoy.Location = new System.Drawing.Point(534, 105);
      this.cbConflictAsDecoy.Name = "cbConflictAsDecoy";
      this.cbConflictAsDecoy.Size = new System.Drawing.Size(178, 20);
      this.cbConflictAsDecoy.TabIndex = 101;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(19, 108);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(509, 12);
      this.label9.TabIndex = 100;
      this.label9.Text = "If one spectrum is matched with both target and decoy entries, it will be conside" +
    "red";
      // 
      // cbClassifyByPreteaseTermini
      // 
      this.cbClassifyByPreteaseTermini.AutoSize = true;
      this.cbClassifyByPreteaseTermini.Location = new System.Drawing.Point(544, 85);
      this.cbClassifyByPreteaseTermini.Name = "cbClassifyByPreteaseTermini";
      this.cbClassifyByPreteaseTermini.Size = new System.Drawing.Size(204, 16);
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
      this.cbConflict.Location = new System.Drawing.Point(603, 204);
      this.cbConflict.Name = "cbConflict";
      this.cbConflict.Size = new System.Drawing.Size(178, 20);
      this.cbConflict.TabIndex = 97;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(22, 207);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(575, 12);
      this.label7.TabIndex = 96;
      this.label7.Text = "If Peptide-Spectrum-Matches from different search engines are conflicted, what is" +
    " your perfer :";
      // 
      // txtDatabase
      // 
      this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDatabase.Location = new System.Drawing.Point(235, 155);
      this.txtDatabase.Name = "txtDatabase";
      this.txtDatabase.Size = new System.Drawing.Size(904, 21);
      this.txtDatabase.TabIndex = 95;
      // 
      // btnDatabase
      // 
      this.btnDatabase.Location = new System.Drawing.Point(21, 153);
      this.btnDatabase.Name = "btnDatabase";
      this.btnDatabase.Size = new System.Drawing.Size(212, 23);
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
      this.cbAccessNumberPattern.Location = new System.Drawing.Point(489, 182);
      this.cbAccessNumberPattern.Name = "cbAccessNumberPattern";
      this.cbAccessNumberPattern.Size = new System.Drawing.Size(650, 20);
      this.cbAccessNumberPattern.TabIndex = 93;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(346, 185);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(137, 12);
      this.label8.TabIndex = 92;
      this.label8.Text = "Access number format =";
      // 
      // txtMinSequenceLength
      // 
      this.txtMinSequenceLength.Location = new System.Drawing.Point(235, 129);
      this.txtMinSequenceLength.Name = "txtMinSequenceLength";
      this.txtMinSequenceLength.Size = new System.Drawing.Size(94, 21);
      this.txtMinSequenceLength.TabIndex = 91;
      // 
      // cbSequenceLength
      // 
      this.cbSequenceLength.AutoSize = true;
      this.cbSequenceLength.Location = new System.Drawing.Point(3, 131);
      this.cbSequenceLength.Name = "cbSequenceLength";
      this.cbSequenceLength.Size = new System.Drawing.Size(228, 16);
      this.cbSequenceLength.TabIndex = 90;
      this.cbSequenceLength.Text = "Filter by minmum sequence length =";
      this.cbSequenceLength.UseVisualStyleBackColor = true;
      // 
      // txtContaminantString
      // 
      this.txtContaminantString.Location = new System.Drawing.Point(282, 248);
      this.txtContaminantString.Name = "txtContaminantString";
      this.txtContaminantString.Size = new System.Drawing.Size(93, 21);
      this.txtContaminantString.TabIndex = 89;
      // 
      // cbPeptideCount
      // 
      this.cbPeptideCount.AutoSize = true;
      this.cbPeptideCount.Location = new System.Drawing.Point(556, 39);
      this.cbPeptideCount.Name = "cbPeptideCount";
      this.cbPeptideCount.Size = new System.Drawing.Size(534, 16);
      this.cbPeptideCount.TabIndex = 87;
      this.cbPeptideCount.Text = "For two hit proteins, iterate peptide limitation (10 -> 2) to find max protein gr" +
    "oups";
      this.cbPeptideCount.UseVisualStyleBackColor = true;
      // 
      // lblMaxPeptideFdr
      // 
      this.lblMaxPeptideFdr.AutoSize = true;
      this.lblMaxPeptideFdr.Location = new System.Drawing.Point(517, 16);
      this.lblMaxPeptideFdr.Name = "lblMaxPeptideFdr";
      this.lblMaxPeptideFdr.Size = new System.Drawing.Size(137, 12);
      this.lblMaxPeptideFdr.TabIndex = 86;
      this.lblMaxPeptideFdr.Text = ", maximum peptide fdr=";
      // 
      // txtMaxPeptideFdr
      // 
      this.txtMaxPeptideFdr.Location = new System.Drawing.Point(660, 12);
      this.txtMaxPeptideFdr.Name = "txtMaxPeptideFdr";
      this.txtMaxPeptideFdr.Size = new System.Drawing.Size(47, 21);
      this.txtMaxPeptideFdr.TabIndex = 85;
      // 
      // cbClassifyByMissCleavage
      // 
      this.cbClassifyByMissCleavage.AutoSize = true;
      this.cbClassifyByMissCleavage.Location = new System.Drawing.Point(337, 85);
      this.cbClassifyByMissCleavage.Name = "cbClassifyByMissCleavage";
      this.cbClassifyByMissCleavage.Size = new System.Drawing.Size(204, 16);
      this.cbClassifyByMissCleavage.TabIndex = 83;
      this.cbClassifyByMissCleavage.Text = "missed internal cleavage sites";
      this.cbClassifyByMissCleavage.UseVisualStyleBackColor = true;
      // 
      // cbClassifyByCharge
      // 
      this.cbClassifyByCharge.AutoSize = true;
      this.cbClassifyByCharge.Location = new System.Drawing.Point(211, 84);
      this.cbClassifyByCharge.Name = "cbClassifyByCharge";
      this.cbClassifyByCharge.Size = new System.Drawing.Size(120, 16);
      this.cbClassifyByCharge.TabIndex = 84;
      this.cbClassifyByCharge.Text = "precursor charge";
      this.cbClassifyByCharge.UseVisualStyleBackColor = true;
      // 
      // cbRemoveDecoyEntry
      // 
      this.cbRemoveDecoyEntry.AutoSize = true;
      this.cbRemoveDecoyEntry.Location = new System.Drawing.Point(3, 228);
      this.cbRemoveDecoyEntry.Name = "cbRemoveDecoyEntry";
      this.cbRemoveDecoyEntry.Size = new System.Drawing.Size(246, 16);
      this.cbRemoveDecoyEntry.TabIndex = 82;
      this.cbRemoveDecoyEntry.Text = "Remove candidates from decoy database";
      this.cbRemoveDecoyEntry.UseVisualStyleBackColor = true;
      // 
      // txtDecoyPattern
      // 
      this.txtDecoyPattern.Location = new System.Drawing.Point(235, 181);
      this.txtDecoyPattern.Name = "txtDecoyPattern";
      this.txtDecoyPattern.Size = new System.Drawing.Size(97, 21);
      this.txtDecoyPattern.TabIndex = 81;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(19, 85);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(191, 12);
      this.label5.TabIndex = 79;
      this.label5.Text = "Group peptide identification by";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(84, 184);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(149, 12);
      this.label3.TabIndex = 80;
      this.label3.Text = "Decoy database pattern =";
      // 
      // txtFdrModifiedAminoacids
      // 
      this.txtFdrModifiedAminoacids.Location = new System.Drawing.Point(971, 84);
      this.txtFdrModifiedAminoacids.Name = "txtFdrModifiedAminoacids";
      this.txtFdrModifiedAminoacids.Size = new System.Drawing.Size(134, 21);
      this.txtFdrModifiedAminoacids.TabIndex = 78;
      // 
      // cbClassifyByModification
      // 
      this.cbClassifyByModification.AutoSize = true;
      this.cbClassifyByModification.Location = new System.Drawing.Point(751, 86);
      this.cbClassifyByModification.Name = "cbClassifyByModification";
      this.cbClassifyByModification.Size = new System.Drawing.Size(222, 16);
      this.cbClassifyByModification.TabIndex = 77;
      this.cbClassifyByModification.Text = "modification state, amino acids =";
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
      this.cbFdrLevel.Location = new System.Drawing.Point(303, 13);
      this.cbFdrLevel.Name = "cbFdrLevel";
      this.cbFdrLevel.Size = new System.Drawing.Size(178, 20);
      this.cbFdrLevel.TabIndex = 76;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(487, 16);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(35, 12);
      this.label4.TabIndex = 74;
      this.label4.Text = "level";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(280, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(17, 12);
      this.label2.TabIndex = 75;
      this.label2.Text = "at";
      // 
      // cbFdrType
      // 
      this.cbFdrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFdrType.FormattingEnabled = true;
      this.cbFdrType.Location = new System.Drawing.Point(162, 37);
      this.cbFdrType.Name = "cbFdrType";
      this.cbFdrType.Size = new System.Drawing.Size(379, 20);
      this.cbFdrType.TabIndex = 71;
      // 
      // txtMaxFdr
      // 
      this.txtMaxFdr.Location = new System.Drawing.Point(227, 12);
      this.txtMaxFdr.Name = "txtMaxFdr";
      this.txtMaxFdr.Size = new System.Drawing.Size(47, 21);
      this.txtMaxFdr.TabIndex = 73;
      // 
      // cbFilterByFDR
      // 
      this.cbFilterByFDR.AutoSize = true;
      this.cbFilterByFDR.Location = new System.Drawing.Point(3, 15);
      this.cbFilterByFDR.Name = "cbFilterByFDR";
      this.cbFilterByFDR.Size = new System.Drawing.Size(222, 16);
      this.cbFilterByFDR.TabIndex = 72;
      this.cbFilterByFDR.Text = "Filter by False Discovery Rate <=";
      this.cbFilterByFDR.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(19, 40);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(137, 12);
      this.label1.TabIndex = 70;
      this.label1.Text = "False Discovery Rate =";
      // 
      // cbRemoveContamination
      // 
      this.cbRemoveContamination.AutoSize = true;
      this.cbRemoveContamination.Location = new System.Drawing.Point(3, 250);
      this.cbRemoveContamination.Name = "cbRemoveContamination";
      this.cbRemoveContamination.Size = new System.Drawing.Size(516, 16);
      this.cbRemoveContamination.TabIndex = 102;
      this.cbRemoveContamination.Text = "Remove contaminant protein by name pattern                  or description patter" +
    "n";
      this.cbRemoveContamination.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.splitContainer1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(1163, 345);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Dataset List";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(3, 17);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.btnAddPeptideProphet);
      this.splitContainer1.Panel1.Controls.Add(this.btnPFind);
      this.splitContainer1.Panel1.Controls.Add(this.btnXtandem);
      this.splitContainer1.Panel1.Controls.Add(this.button1);
      this.splitContainer1.Panel1.Controls.Add(this.btnDelete);
      this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tcDatasetList);
      this.splitContainer1.Size = new System.Drawing.Size(1157, 325);
      this.splitContainer1.SplitterDistance = 140;
      this.splitContainer1.TabIndex = 1;
      // 
      // btnAddPeptideProphet
      // 
      this.btnAddPeptideProphet.Location = new System.Drawing.Point(4, 120);
      this.btnAddPeptideProphet.Name = "btnAddPeptideProphet";
      this.btnAddPeptideProphet.Size = new System.Drawing.Size(131, 23);
      this.btnAddPeptideProphet.TabIndex = 11;
      this.btnAddPeptideProphet.Text = "Add PeptideProphet";
      this.btnAddPeptideProphet.UseVisualStyleBackColor = true;
      this.btnAddPeptideProphet.Click += new System.EventHandler(this.btnAddPeptideProphet_Click);
      // 
      // btnPFind
      // 
      this.btnPFind.Location = new System.Drawing.Point(4, 91);
      this.btnPFind.Name = "btnPFind";
      this.btnPFind.Size = new System.Drawing.Size(131, 23);
      this.btnPFind.TabIndex = 10;
      this.btnPFind.Text = "Add pFind";
      this.btnPFind.UseVisualStyleBackColor = true;
      this.btnPFind.Click += new System.EventHandler(this.btnPFind_Click);
      // 
      // btnXtandem
      // 
      this.btnXtandem.Location = new System.Drawing.Point(4, 61);
      this.btnXtandem.Name = "btnXtandem";
      this.btnXtandem.Size = new System.Drawing.Size(131, 23);
      this.btnXtandem.TabIndex = 9;
      this.btnXtandem.Text = "Add XTandem";
      this.btnXtandem.UseVisualStyleBackColor = true;
      this.btnXtandem.Click += new System.EventHandler(this.btnXtandem_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(3, 32);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(132, 23);
      this.button1.TabIndex = 8;
      this.button1.Text = "Add MASCOT";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.btnAddMascot_Click);
      // 
      // btnDelete
      // 
      this.btnDelete.Location = new System.Drawing.Point(4, 147);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(131, 23);
      this.btnDelete.TabIndex = 7;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(3, 3);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(132, 23);
      this.btnAdd.TabIndex = 6;
      this.btnAdd.Text = "Add SEQUEST";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAddSequest_Click);
      // 
      // tcDatasetList
      // 
      this.tcDatasetList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tcDatasetList.Location = new System.Drawing.Point(0, 0);
      this.tcDatasetList.Name = "tcDatasetList";
      this.tcDatasetList.SelectedIndex = 0;
      this.tcDatasetList.Size = new System.Drawing.Size(1013, 325);
      this.tcDatasetList.TabIndex = 0;
      // 
      // btnSaveParam
      // 
      this.btnSaveParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSaveParam.Location = new System.Drawing.Point(793, 711);
      this.btnSaveParam.Name = "btnSaveParam";
      this.btnSaveParam.Size = new System.Drawing.Size(97, 23);
      this.btnSaveParam.TabIndex = 29;
      this.btnSaveParam.Text = "&Save param";
      this.btnSaveParam.UseVisualStyleBackColor = true;
      this.btnSaveParam.Click += new System.EventHandler(this.btnSaveParam_Click);
      // 
      // btnLoadParam
      // 
      this.btnLoadParam.Location = new System.Drawing.Point(677, 664);
      this.btnLoadParam.Name = "btnLoadParam";
      this.btnLoadParam.Size = new System.Drawing.Size(97, 23);
      this.btnLoadParam.TabIndex = 28;
      this.btnLoadParam.Text = "&Load param";
      this.btnLoadParam.UseVisualStyleBackColor = true;
      this.btnLoadParam.Click += new System.EventHandler(this.btnLoadParam_Click);
      // 
      // btnNew
      // 
      this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnNew.Location = new System.Drawing.Point(896, 711);
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new System.Drawing.Size(97, 23);
      this.btnNew.TabIndex = 30;
      this.btnNew.Text = "&New";
      this.btnNew.UseVisualStyleBackColor = true;
      this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
      // 
      // FilterOneHitWonder
      // 
      this.FilterOneHitWonder.AutoSize = true;
      this.FilterOneHitWonder.Key = "FilterOneHitWonder";
      this.FilterOneHitWonder.Location = new System.Drawing.Point(556, 62);
      this.FilterOneHitWonder.Name = "FilterOneHitWonder";
      this.FilterOneHitWonder.PreCondition = null;
      this.FilterOneHitWonder.Size = new System.Drawing.Size(294, 16);
      this.FilterOneHitWonder.TabIndex = 144;
      this.FilterOneHitWonder.Text = "For one-hit-wonders, minimum peptide count = ";
      this.FilterOneHitWonder.UseVisualStyleBackColor = true;
      // 
      // txtMinOneHitWonderPeptideCount
      // 
      this.txtMinOneHitWonderPeptideCount.Location = new System.Drawing.Point(842, 59);
      this.txtMinOneHitWonderPeptideCount.Name = "txtMinOneHitWonderPeptideCount";
      this.txtMinOneHitWonderPeptideCount.Size = new System.Drawing.Size(47, 21);
      this.txtMinOneHitWonderPeptideCount.TabIndex = 85;
      // 
      // UniformBuildSummaryUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1189, 754);
      this.Controls.Add(this.btnNew);
      this.Controls.Add(this.btnSaveParam);
      this.Controls.Add(this.btnLoadParam);
      this.Controls.Add(this.splitContainer2);
      this.Name = "UniformBuildSummaryUI";
      this.TabText = "UniformBuildSummaryUI";
      this.Text = "UniformBuildSummaryUI";
      this.Controls.SetChildIndex(this.splitContainer2, 0);
      this.Controls.SetChildIndex(this.btnLoadParam, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.btnSaveParam, 0);
      this.Controls.SetChildIndex(this.btnNew, 0);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer2;
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
    private System.Windows.Forms.Label label3;
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
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.TabControl tcDatasetList;
    private System.Windows.Forms.Button btnSaveParam;
    private System.Windows.Forms.Button btnLoadParam;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button btnXtandem;
    private System.Windows.Forms.ComboBox cbConflict;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btnPFind;
    private System.Windows.Forms.Button btnNew;
    private System.Windows.Forms.CheckBox cbClassifyByPreteaseTermini;
    private System.Windows.Forms.Button btnAddPeptideProphet;
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
  }
}