namespace RCPA.Tools.Summary
{
  partial class SummaryBuilder2UI
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
      this.btnLoadParam = new System.Windows.Forms.Button();
      this.btnSaveParam = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.filterGroup = new System.Windows.Forms.TabPage();
      this.txtMinOneHitWonderPeptideCount = new System.Windows.Forms.TextBox();
      this.FilterOneHitWonder = new RCPA.Gui.RcpaCheckField();
      this.cbPeptideCount = new System.Windows.Forms.CheckBox();
      this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters = new RCPA.Gui.RcpaCheckField();
      this.cbIndividual = new RCPA.Gui.RcpaCheckField();
      this.cbFilterByDynamicPrecursorTolerance = new System.Windows.Forms.CheckBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtContaminantDescriptionPattern = new System.Windows.Forms.TextBox();
      this.txtContaminantNamePattern = new System.Windows.Forms.TextBox();
      this.cbRemoveContamination = new System.Windows.Forms.CheckBox();
      this.cbConflictAsDecoy = new System.Windows.Forms.ComboBox();
      this.label9 = new System.Windows.Forms.Label();
      this.cbClassifyByPreteaseTermini = new System.Windows.Forms.CheckBox();
      this.cbFilterByPrecursorSecondIsotopic = new System.Windows.Forms.CheckBox();
      this.txtMinSequenceLength = new System.Windows.Forms.TextBox();
      this.cbSequenceLength = new System.Windows.Forms.CheckBox();
      this.lblMaxPeptideFdr = new System.Windows.Forms.Label();
      this.txtMaxPeptideFdr = new System.Windows.Forms.TextBox();
      this.cbClassifyByMissCleavage = new System.Windows.Forms.CheckBox();
      this.cbClassifyByCharge = new System.Windows.Forms.CheckBox();
      this.cbRemoveDecoyEntry = new System.Windows.Forms.CheckBox();
      this.txtDecoyPattern = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtPrecursorPPMTolerance = new System.Windows.Forms.TextBox();
      this.cbPrecursorPPMTolerance = new System.Windows.Forms.CheckBox();
      this.txtFdrModifiedAminoacids = new System.Windows.Forms.TextBox();
      this.cbClassifyByModification = new System.Windows.Forms.CheckBox();
      this.cbFdrLevel = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.cbFdrType = new System.Windows.Forms.ComboBox();
      this.txtMaxFdr = new System.Windows.Forms.TextBox();
      this.cbFilterByFDR = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.datafileGroup = new System.Windows.Forms.TabPage();
      this.databaseGroup = new System.Windows.Forms.TabPage();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.lstDatabases = new System.Windows.Forms.ListBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.cbAccessNumberPattern = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.btnTestDatabase = new System.Windows.Forms.Button();
      this.btnRemoveDatabase = new System.Windows.Forms.Button();
      this.btnAddDatabase = new System.Windows.Forms.Button();
      this.tabControl1.SuspendLayout();
      this.filterGroup.SuspendLayout();
      this.databaseGroup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 685);
      this.lblProgress.Size = new System.Drawing.Size(1230, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 708);
      this.progressBar.Size = new System.Drawing.Size(1230, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(674, 7);
      this.btnClose.Size = new System.Drawing.Size(97, 25);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(567, 7);
      this.btnCancel.Size = new System.Drawing.Size(97, 25);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(460, 7);
      this.btnGo.Size = new System.Drawing.Size(97, 25);
      // 
      // btnLoadParam
      // 
      this.btnLoadParam.Location = new System.Drawing.Point(691, 724);
      this.btnLoadParam.Name = "btnLoadParam";
      this.btnLoadParam.Size = new System.Drawing.Size(97, 25);
      this.btnLoadParam.TabIndex = 26;
      this.btnLoadParam.Text = "&Load param";
      this.btnLoadParam.UseVisualStyleBackColor = true;
      this.btnLoadParam.Click += new System.EventHandler(this.btnLoadParam_Click);
      // 
      // btnSaveParam
      // 
      this.btnSaveParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSaveParam.Location = new System.Drawing.Point(813, 724);
      this.btnSaveParam.Name = "btnSaveParam";
      this.btnSaveParam.Size = new System.Drawing.Size(97, 25);
      this.btnSaveParam.TabIndex = 27;
      this.btnSaveParam.Text = "&Save param";
      this.btnSaveParam.UseVisualStyleBackColor = true;
      this.btnSaveParam.Click += new System.EventHandler(this.btnSaveParam_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.filterGroup);
      this.tabControl1.Controls.Add(this.datafileGroup);
      this.tabControl1.Controls.Add(this.databaseGroup);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1230, 685);
      this.tabControl1.TabIndex = 28;
      // 
      // filterGroup
      // 
      this.filterGroup.Controls.Add(this.txtMinOneHitWonderPeptideCount);
      this.filterGroup.Controls.Add(this.FilterOneHitWonder);
      this.filterGroup.Controls.Add(this.cbPeptideCount);
      this.filterGroup.Controls.Add(this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters);
      this.filterGroup.Controls.Add(this.cbIndividual);
      this.filterGroup.Controls.Add(this.cbFilterByDynamicPrecursorTolerance);
      this.filterGroup.Controls.Add(this.label6);
      this.filterGroup.Controls.Add(this.txtContaminantDescriptionPattern);
      this.filterGroup.Controls.Add(this.txtContaminantNamePattern);
      this.filterGroup.Controls.Add(this.cbRemoveContamination);
      this.filterGroup.Controls.Add(this.cbConflictAsDecoy);
      this.filterGroup.Controls.Add(this.label9);
      this.filterGroup.Controls.Add(this.cbClassifyByPreteaseTermini);
      this.filterGroup.Controls.Add(this.cbFilterByPrecursorSecondIsotopic);
      this.filterGroup.Controls.Add(this.txtMinSequenceLength);
      this.filterGroup.Controls.Add(this.cbSequenceLength);
      this.filterGroup.Controls.Add(this.lblMaxPeptideFdr);
      this.filterGroup.Controls.Add(this.txtMaxPeptideFdr);
      this.filterGroup.Controls.Add(this.cbClassifyByMissCleavage);
      this.filterGroup.Controls.Add(this.cbClassifyByCharge);
      this.filterGroup.Controls.Add(this.cbRemoveDecoyEntry);
      this.filterGroup.Controls.Add(this.txtDecoyPattern);
      this.filterGroup.Controls.Add(this.label5);
      this.filterGroup.Controls.Add(this.label3);
      this.filterGroup.Controls.Add(this.txtPrecursorPPMTolerance);
      this.filterGroup.Controls.Add(this.cbPrecursorPPMTolerance);
      this.filterGroup.Controls.Add(this.txtFdrModifiedAminoacids);
      this.filterGroup.Controls.Add(this.cbClassifyByModification);
      this.filterGroup.Controls.Add(this.cbFdrLevel);
      this.filterGroup.Controls.Add(this.label4);
      this.filterGroup.Controls.Add(this.label2);
      this.filterGroup.Controls.Add(this.cbFdrType);
      this.filterGroup.Controls.Add(this.txtMaxFdr);
      this.filterGroup.Controls.Add(this.cbFilterByFDR);
      this.filterGroup.Controls.Add(this.label1);
      this.filterGroup.Location = new System.Drawing.Point(4, 22);
      this.filterGroup.Name = "filterGroup";
      this.filterGroup.Padding = new System.Windows.Forms.Padding(3);
      this.filterGroup.Size = new System.Drawing.Size(1222, 659);
      this.filterGroup.TabIndex = 0;
      this.filterGroup.Text = "Options";
      this.filterGroup.UseVisualStyleBackColor = true;
      // 
      // txtMinOneHitWonderPeptideCount
      // 
      this.txtMinOneHitWonderPeptideCount.Location = new System.Drawing.Point(865, 147);
      this.txtMinOneHitWonderPeptideCount.Name = "txtMinOneHitWonderPeptideCount";
      this.txtMinOneHitWonderPeptideCount.Size = new System.Drawing.Size(47, 20);
      this.txtMinOneHitWonderPeptideCount.TabIndex = 145;
      // 
      // FilterOneHitWonder
      // 
      this.FilterOneHitWonder.AutoSize = true;
      this.FilterOneHitWonder.Key = "FilterOneHitWonder";
      this.FilterOneHitWonder.Location = new System.Drawing.Point(579, 151);
      this.FilterOneHitWonder.Name = "FilterOneHitWonder";
      this.FilterOneHitWonder.PreCondition = null;
      this.FilterOneHitWonder.Size = new System.Drawing.Size(245, 17);
      this.FilterOneHitWonder.TabIndex = 147;
      this.FilterOneHitWonder.Text = "For one-hit-wonders, minimum peptide count = ";
      this.FilterOneHitWonder.UseVisualStyleBackColor = true;
      // 
      // cbPeptideCount
      // 
      this.cbPeptideCount.AutoSize = true;
      this.cbPeptideCount.Location = new System.Drawing.Point(579, 126);
      this.cbPeptideCount.Name = "cbPeptideCount";
      this.cbPeptideCount.Size = new System.Drawing.Size(397, 17);
      this.cbPeptideCount.TabIndex = 146;
      this.cbPeptideCount.Text = "For two hit proteins, iterate peptide limitation (10 -> 2) to find max protein gr" +
    "oups";
      this.cbPeptideCount.UseVisualStyleBackColor = true;
      // 
      // cbKeepTopPeptideFromSameEngineButDifferentSearchParameters
      // 
      this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters.Key = "MergeResultFromSameEngineButDifferentSearchParameters";
      this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters.Location = new System.Drawing.Point(519, 25);
      this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters.Name = "cbKeepTopPeptideFromSameEngineButDifferentSearchParameters";
      this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters.PreCondition = null;
      this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters.Size = new System.Drawing.Size(470, 21);
      this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters.TabIndex = 144;
      this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters.Text = "Keep top score peptide only from same spectrum and same engine";
      // 
      // cbIndividual
      // 
      this.cbIndividual.Key = "RunIndividual";
      this.cbIndividual.Location = new System.Drawing.Point(310, 25);
      this.cbIndividual.Name = "cbIndividual";
      this.cbIndividual.PreCondition = null;
      this.cbIndividual.Size = new System.Drawing.Size(222, 21);
      this.cbIndividual.TabIndex = 141;
      this.cbIndividual.Text = "Generate individual results";
      // 
      // cbFilterByDynamicPrecursorTolerance
      // 
      this.cbFilterByDynamicPrecursorTolerance.AutoSize = true;
      this.cbFilterByDynamicPrecursorTolerance.Location = new System.Drawing.Point(620, 270);
      this.cbFilterByDynamicPrecursorTolerance.Name = "cbFilterByDynamicPrecursorTolerance";
      this.cbFilterByDynamicPrecursorTolerance.Size = new System.Drawing.Size(187, 17);
      this.cbFilterByDynamicPrecursorTolerance.TabIndex = 140;
      this.cbFilterByDynamicPrecursorTolerance.Text = "using dynamic precursor tolerance";
      this.cbFilterByDynamicPrecursorTolerance.UseVisualStyleBackColor = true;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(410, 73);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(106, 13);
      this.label6.TabIndex = 139;
      this.label6.Text = "or description pattern";
      // 
      // txtContaminantDescriptionPattern
      // 
      this.txtContaminantDescriptionPattern.Location = new System.Drawing.Point(559, 68);
      this.txtContaminantDescriptionPattern.Name = "txtContaminantDescriptionPattern";
      this.txtContaminantDescriptionPattern.Size = new System.Drawing.Size(133, 20);
      this.txtContaminantDescriptionPattern.TabIndex = 138;
      // 
      // txtContaminantNamePattern
      // 
      this.txtContaminantNamePattern.Location = new System.Drawing.Point(310, 68);
      this.txtContaminantNamePattern.Name = "txtContaminantNamePattern";
      this.txtContaminantNamePattern.Size = new System.Drawing.Size(93, 20);
      this.txtContaminantNamePattern.TabIndex = 136;
      // 
      // cbRemoveContamination
      // 
      this.cbRemoveContamination.AutoSize = true;
      this.cbRemoveContamination.Location = new System.Drawing.Point(27, 70);
      this.cbRemoveContamination.Name = "cbRemoveContamination";
      this.cbRemoveContamination.Size = new System.Drawing.Size(241, 17);
      this.cbRemoveContamination.TabIndex = 137;
      this.cbRemoveContamination.Text = "Remove contaminant protein by name pattern";
      this.cbRemoveContamination.UseVisualStyleBackColor = true;
      // 
      // cbConflictAsDecoy
      // 
      this.cbConflictAsDecoy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbConflictAsDecoy.FormattingEnabled = true;
      this.cbConflictAsDecoy.Items.AddRange(new object[] {
            "Protein",
            "Peptide",
            "Unique peptide"});
      this.cbConflictAsDecoy.Location = new System.Drawing.Point(573, 244);
      this.cbConflictAsDecoy.Name = "cbConflictAsDecoy";
      this.cbConflictAsDecoy.Size = new System.Drawing.Size(119, 21);
      this.cbConflictAsDecoy.TabIndex = 135;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(51, 247);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(395, 13);
      this.label9.TabIndex = 134;
      this.label9.Text = "If one spectrum is matched with both target and decoy entries, it will be conside" +
    "red";
      // 
      // cbClassifyByPreteaseTermini
      // 
      this.cbClassifyByPreteaseTermini.AutoSize = true;
      this.cbClassifyByPreteaseTermini.Location = new System.Drawing.Point(262, 195);
      this.cbClassifyByPreteaseTermini.Name = "cbClassifyByPreteaseTermini";
      this.cbClassifyByPreteaseTermini.Size = new System.Drawing.Size(168, 17);
      this.cbClassifyByPreteaseTermini.TabIndex = 133;
      this.cbClassifyByPreteaseTermini.Text = "the number of protease termini";
      this.cbClassifyByPreteaseTermini.UseVisualStyleBackColor = true;
      // 
      // cbFilterByPrecursorSecondIsotopic
      // 
      this.cbFilterByPrecursorSecondIsotopic.AutoSize = true;
      this.cbFilterByPrecursorSecondIsotopic.Location = new System.Drawing.Point(379, 270);
      this.cbFilterByPrecursorSecondIsotopic.Name = "cbFilterByPrecursorSecondIsotopic";
      this.cbFilterByPrecursorSecondIsotopic.Size = new System.Drawing.Size(188, 17);
      this.cbFilterByPrecursorSecondIsotopic.TabIndex = 132;
      this.cbFilterByPrecursorSecondIsotopic.Text = "consider the second isotopic peak";
      this.cbFilterByPrecursorSecondIsotopic.UseVisualStyleBackColor = true;
      // 
      // txtMinSequenceLength
      // 
      this.txtMinSequenceLength.Location = new System.Drawing.Point(279, 294);
      this.txtMinSequenceLength.Name = "txtMinSequenceLength";
      this.txtMinSequenceLength.Size = new System.Drawing.Size(94, 20);
      this.txtMinSequenceLength.TabIndex = 131;
      // 
      // cbSequenceLength
      // 
      this.cbSequenceLength.AutoSize = true;
      this.cbSequenceLength.Location = new System.Drawing.Point(27, 296);
      this.cbSequenceLength.Name = "cbSequenceLength";
      this.cbSequenceLength.Size = new System.Drawing.Size(197, 17);
      this.cbSequenceLength.TabIndex = 130;
      this.cbSequenceLength.Text = "Filter by minmum sequence length = ";
      this.cbSequenceLength.UseVisualStyleBackColor = true;
      // 
      // lblMaxPeptideFdr
      // 
      this.lblMaxPeptideFdr.AutoSize = true;
      this.lblMaxPeptideFdr.Location = new System.Drawing.Point(504, 98);
      this.lblMaxPeptideFdr.Name = "lblMaxPeptideFdr";
      this.lblMaxPeptideFdr.Size = new System.Drawing.Size(115, 13);
      this.lblMaxPeptideFdr.TabIndex = 128;
      this.lblMaxPeptideFdr.Text = ", maximum peptide fdr=";
      // 
      // txtMaxPeptideFdr
      // 
      this.txtMaxPeptideFdr.Location = new System.Drawing.Point(645, 94);
      this.txtMaxPeptideFdr.Name = "txtMaxPeptideFdr";
      this.txtMaxPeptideFdr.Size = new System.Drawing.Size(47, 20);
      this.txtMaxPeptideFdr.TabIndex = 127;
      // 
      // cbClassifyByMissCleavage
      // 
      this.cbClassifyByMissCleavage.AutoSize = true;
      this.cbClassifyByMissCleavage.Location = new System.Drawing.Point(262, 171);
      this.cbClassifyByMissCleavage.Name = "cbClassifyByMissCleavage";
      this.cbClassifyByMissCleavage.Size = new System.Drawing.Size(166, 17);
      this.cbClassifyByMissCleavage.TabIndex = 125;
      this.cbClassifyByMissCleavage.Text = "missed internal cleavage sites";
      this.cbClassifyByMissCleavage.UseVisualStyleBackColor = true;
      // 
      // cbClassifyByCharge
      // 
      this.cbClassifyByCharge.AutoSize = true;
      this.cbClassifyByCharge.Location = new System.Drawing.Point(262, 147);
      this.cbClassifyByCharge.Name = "cbClassifyByCharge";
      this.cbClassifyByCharge.Size = new System.Drawing.Size(106, 17);
      this.cbClassifyByCharge.TabIndex = 126;
      this.cbClassifyByCharge.Text = "precursor charge";
      this.cbClassifyByCharge.UseVisualStyleBackColor = true;
      // 
      // cbRemoveDecoyEntry
      // 
      this.cbRemoveDecoyEntry.AutoSize = true;
      this.cbRemoveDecoyEntry.Location = new System.Drawing.Point(27, 47);
      this.cbRemoveDecoyEntry.Name = "cbRemoveDecoyEntry";
      this.cbRemoveDecoyEntry.Size = new System.Drawing.Size(223, 17);
      this.cbRemoveDecoyEntry.TabIndex = 124;
      this.cbRemoveDecoyEntry.Text = "Remove candidates from decoy database";
      this.cbRemoveDecoyEntry.UseVisualStyleBackColor = true;
      // 
      // txtDecoyPattern
      // 
      this.txtDecoyPattern.Location = new System.Drawing.Point(180, 22);
      this.txtDecoyPattern.Name = "txtDecoyPattern";
      this.txtDecoyPattern.Size = new System.Drawing.Size(100, 20);
      this.txtDecoyPattern.TabIndex = 123;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(51, 148);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(150, 13);
      this.label5.TabIndex = 121;
      this.label5.Text = "Group peptide identification by";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(25, 25);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(130, 13);
      this.label3.TabIndex = 122;
      this.label3.Text = "Decoy database pattern =";
      // 
      // txtPrecursorPPMTolerance
      // 
      this.txtPrecursorPPMTolerance.Location = new System.Drawing.Point(279, 268);
      this.txtPrecursorPPMTolerance.Name = "txtPrecursorPPMTolerance";
      this.txtPrecursorPPMTolerance.Size = new System.Drawing.Size(94, 20);
      this.txtPrecursorPPMTolerance.TabIndex = 120;
      // 
      // cbPrecursorPPMTolerance
      // 
      this.cbPrecursorPPMTolerance.AutoSize = true;
      this.cbPrecursorPPMTolerance.Location = new System.Drawing.Point(27, 270);
      this.cbPrecursorPPMTolerance.Name = "cbPrecursorPPMTolerance";
      this.cbPrecursorPPMTolerance.Size = new System.Drawing.Size(194, 17);
      this.cbPrecursorPPMTolerance.TabIndex = 119;
      this.cbPrecursorPPMTolerance.Text = "Filter by precursor tolerance (ppm) =";
      this.cbPrecursorPPMTolerance.UseVisualStyleBackColor = true;
      // 
      // txtFdrModifiedAminoacids
      // 
      this.txtFdrModifiedAminoacids.Location = new System.Drawing.Point(480, 217);
      this.txtFdrModifiedAminoacids.Name = "txtFdrModifiedAminoacids";
      this.txtFdrModifiedAminoacids.Size = new System.Drawing.Size(212, 20);
      this.txtFdrModifiedAminoacids.TabIndex = 118;
      // 
      // cbClassifyByModification
      // 
      this.cbClassifyByModification.AutoSize = true;
      this.cbClassifyByModification.Location = new System.Drawing.Point(262, 219);
      this.cbClassifyByModification.Name = "cbClassifyByModification";
      this.cbClassifyByModification.Size = new System.Drawing.Size(179, 17);
      this.cbClassifyByModification.TabIndex = 117;
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
      this.cbFdrLevel.Location = new System.Drawing.Point(322, 94);
      this.cbFdrLevel.Name = "cbFdrLevel";
      this.cbFdrLevel.Size = new System.Drawing.Size(144, 21);
      this.cbFdrLevel.TabIndex = 116;
      this.cbFdrLevel.SelectedIndexChanged += new System.EventHandler(this.cbFdrLevel_SelectedIndexChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(472, 98);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(29, 13);
      this.label4.TabIndex = 114;
      this.label4.Text = "level";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(299, 98);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(16, 13);
      this.label2.TabIndex = 115;
      this.label2.Text = "at";
      // 
      // cbFdrType
      // 
      this.cbFdrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFdrType.FormattingEnabled = true;
      this.cbFdrType.Location = new System.Drawing.Point(194, 124);
      this.cbFdrType.Name = "cbFdrType";
      this.cbFdrType.Size = new System.Drawing.Size(379, 21);
      this.cbFdrType.TabIndex = 111;
      // 
      // txtMaxFdr
      // 
      this.txtMaxFdr.Location = new System.Drawing.Point(246, 94);
      this.txtMaxFdr.Name = "txtMaxFdr";
      this.txtMaxFdr.Size = new System.Drawing.Size(47, 20);
      this.txtMaxFdr.TabIndex = 113;
      // 
      // cbFilterByFDR
      // 
      this.cbFilterByFDR.AutoSize = true;
      this.cbFilterByFDR.Location = new System.Drawing.Point(27, 96);
      this.cbFilterByFDR.Name = "cbFilterByFDR";
      this.cbFilterByFDR.Size = new System.Drawing.Size(181, 17);
      this.cbFilterByFDR.TabIndex = 112;
      this.cbFilterByFDR.Text = "Filter by False Discovery Rate <=";
      this.cbFilterByFDR.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(51, 127);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(117, 13);
      this.label1.TabIndex = 110;
      this.label1.Text = "False Discovery Rate =";
      // 
      // datafileGroup
      // 
      this.datafileGroup.Location = new System.Drawing.Point(4, 22);
      this.datafileGroup.Name = "datafileGroup";
      this.datafileGroup.Padding = new System.Windows.Forms.Padding(3);
      this.datafileGroup.Size = new System.Drawing.Size(1222, 659);
      this.datafileGroup.TabIndex = 2;
      this.datafileGroup.Text = "Data files";
      this.datafileGroup.UseVisualStyleBackColor = true;
      // 
      // databaseGroup
      // 
      this.databaseGroup.Controls.Add(this.splitContainer1);
      this.databaseGroup.Location = new System.Drawing.Point(4, 22);
      this.databaseGroup.Name = "databaseGroup";
      this.databaseGroup.Padding = new System.Windows.Forms.Padding(3);
      this.databaseGroup.Size = new System.Drawing.Size(1222, 659);
      this.databaseGroup.TabIndex = 1;
      this.databaseGroup.Text = "Database";
      this.databaseGroup.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(3, 3);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.lstDatabases);
      this.splitContainer1.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnTestDatabase);
      this.splitContainer1.Panel2.Controls.Add(this.btnRemoveDatabase);
      this.splitContainer1.Panel2.Controls.Add(this.btnAddDatabase);
      this.splitContainer1.Size = new System.Drawing.Size(1216, 653);
      this.splitContainer1.SplitterDistance = 1070;
      this.splitContainer1.TabIndex = 31;
      // 
      // lstDatabases
      // 
      this.lstDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstDatabases.FormattingEnabled = true;
      this.lstDatabases.Location = new System.Drawing.Point(0, 0);
      this.lstDatabases.Name = "lstDatabases";
      this.lstDatabases.Size = new System.Drawing.Size(1070, 628);
      this.lstDatabases.Sorted = true;
      this.lstDatabases.TabIndex = 29;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cbAccessNumberPattern);
      this.panel1.Controls.Add(this.label8);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 628);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1070, 25);
      this.panel1.TabIndex = 30;
      // 
      // cbAccessNumberPattern
      // 
      this.cbAccessNumberPattern.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cbAccessNumberPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbAccessNumberPattern.FormattingEnabled = true;
      this.cbAccessNumberPattern.Items.AddRange(new object[] {
            "(IPI\\d+)",
            "(gi:\\d+)",
            "(IPI\\d+|gi:\\d+)"});
      this.cbAccessNumberPattern.Location = new System.Drawing.Point(137, 0);
      this.cbAccessNumberPattern.Name = "cbAccessNumberPattern";
      this.cbAccessNumberPattern.Size = new System.Drawing.Size(933, 21);
      this.cbAccessNumberPattern.TabIndex = 38;
      // 
      // label8
      // 
      this.label8.Dock = System.Windows.Forms.DockStyle.Left;
      this.label8.Location = new System.Drawing.Point(0, 0);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(137, 25);
      this.label8.TabIndex = 37;
      this.label8.Text = "Access number format :";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // btnTestDatabase
      // 
      this.btnTestDatabase.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.btnTestDatabase.Location = new System.Drawing.Point(0, 626);
      this.btnTestDatabase.Name = "btnTestDatabase";
      this.btnTestDatabase.Size = new System.Drawing.Size(142, 27);
      this.btnTestDatabase.TabIndex = 37;
      this.btnTestDatabase.Text = "&Test";
      this.btnTestDatabase.UseVisualStyleBackColor = true;
      this.btnTestDatabase.Click += new System.EventHandler(this.btnTestDatabase_Click);
      // 
      // btnRemoveDatabase
      // 
      this.btnRemoveDatabase.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnRemoveDatabase.Location = new System.Drawing.Point(0, 25);
      this.btnRemoveDatabase.Name = "btnRemoveDatabase";
      this.btnRemoveDatabase.Size = new System.Drawing.Size(142, 25);
      this.btnRemoveDatabase.TabIndex = 36;
      this.btnRemoveDatabase.Text = "button2";
      this.btnRemoveDatabase.UseVisualStyleBackColor = true;
      // 
      // btnAddDatabase
      // 
      this.btnAddDatabase.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAddDatabase.Location = new System.Drawing.Point(0, 0);
      this.btnAddDatabase.Name = "btnAddDatabase";
      this.btnAddDatabase.Size = new System.Drawing.Size(142, 25);
      this.btnAddDatabase.TabIndex = 35;
      this.btnAddDatabase.Text = "button1";
      this.btnAddDatabase.UseVisualStyleBackColor = true;
      // 
      // SummaryBuilder2UI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1230, 770);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.btnSaveParam);
      this.Controls.Add(this.btnLoadParam);
      this.Name = "SummaryBuilder2UI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.btnLoadParam, 0);
      this.Controls.SetChildIndex(this.btnSaveParam, 0);
      this.Controls.SetChildIndex(this.tabControl1, 0);
      this.tabControl1.ResumeLayout(false);
      this.filterGroup.ResumeLayout(false);
      this.filterGroup.PerformLayout();
      this.databaseGroup.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnLoadParam;
    private System.Windows.Forms.Button btnSaveParam;
    protected System.Windows.Forms.TabControl tabControl1;
    protected System.Windows.Forms.TabPage filterGroup;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtContaminantDescriptionPattern;
    private System.Windows.Forms.TextBox txtContaminantNamePattern;
    private System.Windows.Forms.CheckBox cbRemoveContamination;
    private System.Windows.Forms.ComboBox cbConflictAsDecoy;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.CheckBox cbClassifyByPreteaseTermini;
    private System.Windows.Forms.CheckBox cbFilterByPrecursorSecondIsotopic;
    private System.Windows.Forms.TextBox txtMinSequenceLength;
    private System.Windows.Forms.CheckBox cbSequenceLength;
    private System.Windows.Forms.Label lblMaxPeptideFdr;
    private System.Windows.Forms.TextBox txtMaxPeptideFdr;
    private System.Windows.Forms.CheckBox cbClassifyByMissCleavage;
    private System.Windows.Forms.CheckBox cbClassifyByCharge;
    private System.Windows.Forms.CheckBox cbRemoveDecoyEntry;
    private System.Windows.Forms.TextBox txtDecoyPattern;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtPrecursorPPMTolerance;
    private System.Windows.Forms.CheckBox cbPrecursorPPMTolerance;
    private System.Windows.Forms.TextBox txtFdrModifiedAminoacids;
    private System.Windows.Forms.CheckBox cbClassifyByModification;
    private System.Windows.Forms.ComboBox cbFdrLevel;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbFdrType;
    private System.Windows.Forms.TextBox txtMaxFdr;
    private System.Windows.Forms.CheckBox cbFilterByFDR;
    private System.Windows.Forms.Label label1;
    protected System.Windows.Forms.TabPage datafileGroup;
    protected System.Windows.Forms.TabPage databaseGroup;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ListBox lstDatabases;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ComboBox cbAccessNumberPattern;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Button btnTestDatabase;
    private System.Windows.Forms.Button btnAddDatabase;
    private System.Windows.Forms.Button btnRemoveDatabase;
    private System.Windows.Forms.CheckBox cbFilterByDynamicPrecursorTolerance;
    private Gui.RcpaCheckField cbIndividual;
    private Gui.RcpaCheckField cbKeepTopPeptideFromSameEngineButDifferentSearchParameters;
    private System.Windows.Forms.TextBox txtMinOneHitWonderPeptideCount;
    private Gui.RcpaCheckField FilterOneHitWonder;
    private System.Windows.Forms.CheckBox cbPeptideCount;
  }
}
