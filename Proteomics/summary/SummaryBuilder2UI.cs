using System;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Proteomics.Summary.Uniform;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RCPA.Utils;

namespace RCPA.Tools.Summary
{
  public partial class SummaryBuilder2UI : AbstractProcessorFileUI
  {
    private readonly RcpaComboBox<IAccessNumberParser> acParsers;

    private RcpaCheckBox classifyByCharge;
    private RcpaCheckBox classifyByMissCleavage;
    private RcpaCheckBox classifyByNumberOfProteaseTermini;
    private RcpaCheckBox classifyByModification;

    private RcpaListBoxMultipleFileField databases;
    private RcpaTextField decoyPattern;
    private RcpaComboBox<FalseDiscoveryRateLevel> fdrLevel;
    private RcpaComboBox<FalseDiscoveryRateType> fdrType;
    private RcpaCheckBox filterByFdr;
    private RcpaComboBox<ITargetDecoyConflictType> conflictAsDecoy;

    private RcpaCheckBox removeContamination;
    private RcpaTextField contaminationNamePattern;
    private RcpaTextField contaminationDescriptionPattern;

    private RcpaDoubleField maxFdr;
    private RcpaDoubleField maxPeptideFdr;
    private RcpaCheckBox filterProteinByPeptideCount;

    private RcpaTextField modifiedAminoacids;
    private OpenFileArgument openParamFile = new OpenFileArgument("Parameter File", "param");

    private RcpaCheckBox filterPrecursorPPMTolerance;
    private RcpaCheckBox filterPrecursorSecondIsotopic;
    private RcpaCheckBox filterPrecursorByDynamicTolerance;
    private RcpaDoubleField precursorPPMTolerance;

    private RcpaCheckBox removeDecoyEntry;
    private SaveFileArgument saveParamFile = new SaveFileArgument("Parameter File", "param");

    private RcpaCheckBox filterSequenceLength;
    private RcpaIntegerField minSequenceLength;

    private RcpaIntegerField minOneHitWonderPeptideCount;

    private Dictionary<string, ITitleParser> savedMap = new Dictionary<string, ITitleParser>();

    protected FilePanel datFiles;

    public SummaryBuilder2UI()
    {
      InitializeComponent();

      InsertButton(2, this.btnLoadParam);

      InsertButton(3, this.btnSaveParam);

      this.decoyPattern = new RcpaTextField(this.txtDecoyPattern, "DecoyPattern", "Decoy Database Pattern", "^REVERSED_", false);
      AddComponent(this.decoyPattern);

      this.removeContamination = new RcpaCheckBox(cbRemoveContamination, "RemoveContamination", false);
      AddComponent(this.removeContamination);

      this.contaminationNamePattern = new RcpaTextField(this.txtContaminantNamePattern, "ContaminationNamePattern", "Contaminant Name Pattern", "CON_", false);
      AddComponent(this.contaminationNamePattern);

      this.contaminationDescriptionPattern = new RcpaTextField(this.txtContaminantDescriptionPattern, "ContaminantDescriptionPattern", "Contaminant Description Pattern", "KERATIN", false);
      AddComponent(this.contaminationDescriptionPattern);

      this.filterByFdr = new RcpaCheckBox(this.cbFilterByFDR, "FilterByFDR", true);
      AddComponent(this.filterByFdr);

      this.maxFdr = new RcpaDoubleField(this.txtMaxFdr, "MaxFdr", "Max False Discovery Rate", 0.01, true);
      AddComponent(this.maxFdr);

      this.maxPeptideFdr = new RcpaDoubleField(this.txtMaxPeptideFdr, "MaxPeptideFdr", "Max Peptide FDR", 0.01, true);
      AddComponent(this.maxPeptideFdr);

      this.filterProteinByPeptideCount = new RcpaCheckBox(this.cbPeptideCount, "FilterProteinByPeptideCount", false);
      AddComponent(this.filterProteinByPeptideCount);

      this.fdrLevel = new RcpaComboBox<FalseDiscoveryRateLevel>(this.cbFdrLevel, "FdrLevel",
                                                                new[]
                                                                  {
                                                                    FalseDiscoveryRateLevel.Peptide,
                                                                    FalseDiscoveryRateLevel.Protein,
                                                                    FalseDiscoveryRateLevel.UniquePeptide,
                                                                    FalseDiscoveryRateLevel.SimpleProtein
                                                                  }, 1);
      AddComponent(this.fdrLevel);

      this.fdrType = new RcpaComboBox<FalseDiscoveryRateType>(this.cbFdrType, "FdrType",
                                                              new[]
                                                                {
                                                                  FalseDiscoveryRateType.Target,
                                                                  FalseDiscoveryRateType.Total
                                                                },
                                                              new[]
                                                                {
                                                                  "Target : N(decoy) / N(target)",
                                                                  "Global : N(decoy) * 2 / (N(decoy) + N(target))"
                                                                }, 0);
      AddComponent(this.fdrType);

      this.classifyByCharge = new RcpaCheckBox(this.cbClassifyByCharge, "ClassifyByCharge", true);
      AddComponent(this.classifyByCharge);

      this.classifyByMissCleavage = new RcpaCheckBox(this.cbClassifyByMissCleavage, "ClassifyByMissCleavage", true);
      AddComponent(this.classifyByMissCleavage);

      this.classifyByNumberOfProteaseTermini = new RcpaCheckBox(this.cbClassifyByPreteaseTermini, "ClassifyByNumberOfProteaseTermini", true);
      AddComponent(this.classifyByNumberOfProteaseTermini);

      this.classifyByModification = new RcpaCheckBox(this.cbClassifyByModification, "ClassifyByModification", true);
      AddComponent(this.classifyByModification);

      this.modifiedAminoacids = new RcpaTextField(this.txtFdrModifiedAminoacids, "ModifiedAminoacids", "Modified Aminoacids", "STY", true);
      AddComponent(this.modifiedAminoacids);

      this.filterPrecursorPPMTolerance = new RcpaCheckBox(this.cbPrecursorPPMTolerance, "FilterPrecursor", false);
      AddComponent(this.filterPrecursorPPMTolerance);

      this.filterPrecursorSecondIsotopic = new RcpaCheckBox(this.cbFilterByPrecursorSecondIsotopic, "FilterPrecursorSecondIsotopic", true);
      AddComponent(this.filterPrecursorSecondIsotopic);

      this.filterPrecursorByDynamicTolerance = new RcpaCheckBox(this.cbFilterByDynamicPrecursorTolerance, "FilterPrecursorByDynamicTolerance", true);
      AddComponent(this.filterPrecursorByDynamicTolerance);

      this.precursorPPMTolerance = new RcpaDoubleField(this.txtPrecursorPPMTolerance, "PrecursorPPMTolerance", "Precursor Tolerance (ppm)", 10, false);
      AddComponent(this.precursorPPMTolerance);

      this.filterSequenceLength = new RcpaCheckBox(this.cbSequenceLength, "FilterSequenceLength", false);
      AddComponent(this.filterSequenceLength);

      this.minSequenceLength = new RcpaIntegerField(this.txtMinSequenceLength, "MinSequenceLength", "Minmum Sequence Length", 6, false);
      AddComponent(this.minSequenceLength);

      this.removeDecoyEntry = new RcpaCheckBox(this.cbRemoveDecoyEntry, "RemovePeptideFromDecoyDB", false);
      AddComponent(this.removeDecoyEntry);

      this.databases = new RcpaListBoxMultipleFileField(
        this.btnAddDatabase,
        this.btnRemoveDatabase,
        null, null, null,
        this.lstDatabases,
        "Databases",
        new OpenFileArgument("Protein Database", "fasta"),
        true,
        true);
      AddComponent(this.databases);

      this.acParsers = new RcpaComboBox<IAccessNumberParser>(this.cbAccessNumberPattern, "AccessNumberPattern",
                                                             AccessNumberParserFactory.GetParsers().ToArray(), 0);
      AddComponent(this.acParsers);

      this.conflictAsDecoy = new RcpaComboBox<ITargetDecoyConflictType>(cbConflictAsDecoy, "ConflictAsDecoy", ResolveTargetDecoyConflictTypeFactory.GetTypes(), 0);
      AddComponent(this.conflictAsDecoy);

      minOneHitWonderPeptideCount = new RcpaIntegerField(txtMinOneHitWonderPeptideCount, "MinOneHitWonderPeptideCount", "minimum one-hit-wonder peptide count", 2, false);
      AddComponent(minOneHitWonderPeptideCount);
      minOneHitWonderPeptideCount.PreCondition = FilterOneHitWonder;

      this.datFiles = CreateFilePanel();
      AddComponent(this.datFiles);

      this.datFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.datFiles.Location = new System.Drawing.Point(3, 16);
      this.datFiles.Name = "datFilePanel";
      this.datFiles.Size = new System.Drawing.Size(1010, 246);
      this.datFiles.TabIndex = 0;
      this.datafileGroup.Controls.Add(this.datFiles);
      this.datafileGroup.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    protected virtual FilePanel CreateFilePanel()
    {
      var result = new FilePanel();
      return result;
    }

    protected override void DoBeforeValidate()
    {
      this.decoyPattern.Required = this.cbFilterByFDR.Checked;

      this.maxFdr.Required = this.cbFilterByFDR.Checked;

      this.maxPeptideFdr.Required = this.filterByFdr.Checked &&
                                    (this.fdrLevel.SelectedItem == FalseDiscoveryRateLevel.Protein ||
                                    this.fdrLevel.SelectedItem == FalseDiscoveryRateLevel.SimpleProtein);

      this.modifiedAminoacids.Required = this.cbFilterByFDR.Checked & this.cbClassifyByModification.Checked;

      this.precursorPPMTolerance.Required = this.cbPrecursorPPMTolerance.Checked;

      this.minSequenceLength.Required = this.cbSequenceLength.Checked;
    }

    public void SaveParamToFile(string filename)
    {
      var options = new BuildSummaryOptions();

      options.ApplicationTitle = Text;

      options.MergeResult = !this.cbIndividual.Checked;
      options.KeepTopPeptideFromSameEngineButDifferentSearchParameters = this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters.Checked;

      options.FalseDiscoveryRate.FilterByFdr = this.filterByFdr.Checked;
      if (options.FalseDiscoveryRate.FilterByFdr)
      {
        options.FalseDiscoveryRate.FdrValue = this.maxFdr.Value;

        options.FalseDiscoveryRate.FdrLevel = this.fdrLevel.SelectedItem;

        options.FalseDiscoveryRate.FdrType = this.fdrType.SelectedItem;

        if (options.FalseDiscoveryRate.FdrLevel == FalseDiscoveryRateLevel.Protein)
        {
          options.FalseDiscoveryRate.MaxPeptideFdr = this.maxPeptideFdr.Value;
          if (this.filterProteinByPeptideCount.Checked)
          {
            options.FalseDiscoveryRate.FdrPeptideCount = 10;
          }
          else
          {
            options.FalseDiscoveryRate.FdrPeptideCount = 0;
          }
        }

        options.FalseDiscoveryRate.TargetDecoyConflictType = this.conflictAsDecoy.SelectedItem;

        options.FalseDiscoveryRate.FilterOneHitWonder = this.FilterOneHitWonder.Checked;

        if (options.FalseDiscoveryRate.FilterOneHitWonder)
        {
          options.FalseDiscoveryRate.MinOneHitWonderPeptideCount = this.minOneHitWonderPeptideCount.Value;
        }

        options.Classification.ClassifyByCharge = this.classifyByCharge.Checked;

        options.Classification.ClassifyByMissCleavage = this.classifyByMissCleavage.Checked;

        options.Classification.ClassifyByNumProteaseTermini = this.classifyByNumberOfProteaseTermini.Checked;

        options.Classification.ClassifyByModification = this.classifyByModification.Checked;

        options.Classification.ModifiedAminoacids = this.modifiedAminoacids.Text;
      }

      SaveDatasetList(options);

      foreach (var dataset in options.DatasetList)
      {
        dataset.FilterByPrecursor = this.filterPrecursorPPMTolerance.Checked;
        if (dataset.FilterByPrecursor)
        {
          dataset.FilterByPrecursorIsotopic = this.filterPrecursorSecondIsotopic.Checked;
          dataset.PrecursorPPMTolerance = this.precursorPPMTolerance.Value;
          dataset.FilterByPrecursorDynamicTolerance = this.filterPrecursorByDynamicTolerance.Checked;

        }
      }
      options.PeptideFilter.FilterBySequenceLength = this.filterSequenceLength.Checked;
      if (options.PeptideFilter.FilterBySequenceLength)
      {
        options.PeptideFilter.MinSequenceLength = this.minSequenceLength.Value;
      }

      options.Database.Location = this.databases.SelectedFileNames[0];
      options.Database.AccessNumberPattern = this.acParsers.SelectedItem.RegexPattern;
      options.Database.DecoyPattern = this.decoyPattern.Text;
      options.Database.RemoveContamination = this.removeContamination.Checked;
      options.Database.ContaminationNamePattern = this.contaminationNamePattern.Text;
      options.Database.ContaminationDescriptionPattern = this.contaminationDescriptionPattern.Text;
      options.Database.RemovePeptideFromDecoyDB = this.removeDecoyEntry.Checked;

      options.SaveToFile(filename);
    }

    public void LoadParamFromFile(string filename)
    {
      try
      {
        BuildSummaryOptions options = BuildSummaryOptionsUtils.LoadFromFile(filename);

        this.cbIndividual.Checked = !options.MergeResult;
        this.decoyPattern.Text = options.Database.DecoyPattern;
        this.removeContamination.Checked = options.Database.RemoveContamination;
        this.contaminationNamePattern.Text = options.Database.ContaminationNamePattern;
        this.contaminationDescriptionPattern.Text = options.Database.ContaminationDescriptionPattern;
        this.removeDecoyEntry.Checked = options.Database.RemovePeptideFromDecoyDB;
        this.cbKeepTopPeptideFromSameEngineButDifferentSearchParameters.Checked = options.KeepTopPeptideFromSameEngineButDifferentSearchParameters;

        this.filterByFdr.Checked = options.FalseDiscoveryRate.FilterByFdr;
        if (options.FalseDiscoveryRate.FilterByFdr)
        {
          this.maxFdr.Value = options.FalseDiscoveryRate.FdrValue;

          this.fdrLevel.SelectedItem = options.FalseDiscoveryRate.FdrLevel;

          this.fdrType.SelectedItem = options.FalseDiscoveryRate.FdrType;

          this.maxPeptideFdr.Value = options.FalseDiscoveryRate.MaxPeptideFdr;

          this.FilterOneHitWonder.Checked = options.FalseDiscoveryRate.FilterOneHitWonder;

          this.minOneHitWonderPeptideCount.Value = options.FalseDiscoveryRate.MinOneHitWonderPeptideCount;

          this.filterProteinByPeptideCount.Checked = options.FalseDiscoveryRate.FdrPeptideCount > 0;

          this.conflictAsDecoy.SelectedItem = options.FalseDiscoveryRate.TargetDecoyConflictType;

          this.classifyByCharge.Checked = options.Classification.ClassifyByCharge;

          this.classifyByMissCleavage.Checked = options.Classification.ClassifyByMissCleavage;

          this.classifyByNumberOfProteaseTermini.Checked = options.Classification.ClassifyByNumProteaseTermini;

          this.classifyByModification.Checked = options.Classification.ClassifyByModification;

          this.modifiedAminoacids.Text = options.Classification.ModifiedAminoacids;

        }

        LoadDatasetList(options);

        if (options.DatasetList.Count > 0)
        {
          var dataset = options.DatasetList[0];
          this.filterPrecursorPPMTolerance.Checked = dataset.FilterByPrecursor;
          if (dataset.FilterByPrecursor)
          {
            this.filterPrecursorSecondIsotopic.Checked = dataset.FilterByPrecursorIsotopic;
            this.precursorPPMTolerance.Value = dataset.PrecursorPPMTolerance;
            this.filterPrecursorByDynamicTolerance.Checked = dataset.FilterByPrecursorDynamicTolerance;
          }
        }

        this.filterSequenceLength.Checked = options.PeptideFilter.FilterBySequenceLength;
        if (options.PeptideFilter.FilterBySequenceLength)
        {
          this.minSequenceLength.Value = options.PeptideFilter.MinSequenceLength;
        }

        this.databases.SelectedFileNames = new[] { options.Database.Location };

        foreach (IAccessNumberParser obj in this.acParsers.Items)
        {
          if (obj.RegexPattern.Equals(options.Database.AccessNumberPattern))
          {
            this.acParsers.SelectedItem = obj;
            break;
          }

          if (obj.FormatName.Equals(options.Database.AccessNumberPattern))
          {
            this.acParsers.SelectedItem = obj;
            break;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    protected virtual void SaveDatasetList(BuildSummaryOptions conf)
    {
      throw new NotImplementedException();
    }

    protected virtual void LoadDatasetList(BuildSummaryOptions options)
    {
      throw new NotImplementedException();
    }

    private void btnLoadParam_Click(object sender, EventArgs e)
    {
      FileDialog dlg = this.openParamFile.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        LoadParamFromFile(dlg.FileName);
      }
    }

    private void btnSaveParam_Click(object sender, EventArgs e)
    {
      try
      {
        ValidateComponents();

        FileDialog dlg = this.saveParamFile.GetFileDialog();
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          SaveParamToFile(dlg.FileName);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void cbFdrLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.fdrLevel == null)
      {
        return;
      }

      bool bVisible = this.fdrLevel.SelectedItem == FalseDiscoveryRateLevel.Protein || this.fdrLevel.SelectedItem == FalseDiscoveryRateLevel.SimpleProtein;
      this.txtMaxPeptideFdr.Visible = bVisible;
      this.lblMaxPeptideFdr.Visible = bVisible;
      this.cbPeptideCount.Visible = bVisible;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new UniformIdentifiedResultBuilder();
    }

    protected override string GetOriginFile()
    {
      FileDialog dlg = this.saveParamFile.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SaveParamToFile(dlg.FileName);
        return dlg.FileName;
      }
      else
      {
        throw new UserTerminatedException();
      }
    }

    private void btnTestDatabase_Click(object sender, EventArgs e)
    {
      if (lstDatabases.SelectedIndex == -1)
      {
        MessageBox.Show(this, "Select database first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      var db = lstDatabases.SelectedItem as string;
      if (!File.Exists(db))
      {
        MessageBox.Show(this, MyConvert.Format("File not exist : {0}", db), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      Regex decoyReg = null;
      if (filterByFdr.Checked)
      {
        try
        {
          decoyReg = new Regex(decoyPattern.Text);
        }
        catch (Exception ex)
        {
          MessageBox.Show(this, MyConvert.Format("Decoy pattern error : {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }

      try
      {
        var targetNames = new List<string>();
        var decoyNames = new List<string>();

        bool matched = false;

        var parser = acParsers.SelectedItem;
        FastaFormat sf = new FastaFormat();
        using (StreamReader sr = new StreamReader(db))
        {
          Sequence seq;
          while ((seq = sf.ReadSequence(sr)) != null)
          {
            string value;
            string op;
            if (parser.TryParse(seq.Name, out value))
            {
              matched = true;
              op = " ==> ";
            }
            else
            {
              value = seq.Name;
              op = " --> ";
            }
            string savedValue = seq.Name + op + value;

            if (decoyReg != null)
            {
              bool isdecoy = decoyReg.Match(value).Success;
              if (isdecoy)
              {
                decoyNames.Add(savedValue);
              }
              else
              {
                targetNames.Add(savedValue);
              }
            }
            else
            {
              targetNames.Add(savedValue);
            }

            if (targetNames.Count >= 10 && (decoyReg == null || decoyNames.Count >= 10))
            {
              break;
            }
          }
        }

        if (targetNames.Count >= 10)
        {
          targetNames.RemoveRange(10, targetNames.Count - 10);
        }

        if (decoyNames.Count >= 10)
        {
          decoyNames.RemoveRange(10, decoyNames.Count - 10);
        }

        if (!matched)
        {
          MessageBox.Show(this, "Didn't find entry matched to access number pattern :\n\n" + targetNames.Merge("\n") + "\n\n" + decoyNames.Merge("\n"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else if (decoyReg != null && decoyNames.Count == 0)
        {
          MessageBox.Show(this, MyConvert.Format("Didn't find decoy entry with pattern {0}:\n\n{1}", decoyPattern.Text, targetNames.Merge("\n")), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
          MessageBox.Show(this, MyConvert.Format("Target entry names\n\n{0}\n\nDecoy entry names\n\n{1}", targetNames.Merge("\n"), decoyNames.Merge("\n")), "Congratulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }
  }
}