﻿using RCPA.Commandline;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RCPA.Tools.Summary
{
  /// <summary>
  /// uniform buildsummary GUI。
  /// 20110128，增加了对peptideprophet的兼容。
  /// 20140928，增加了对proteome discoverer的兼容。
  /// </summary>
  public partial class UniformSummaryBuilderUI : AbstractProcessorUI
  {
    public static string title = "BuildSummary - A general framework for assembling protein identifications";
    public static string version = "7.1.8";

    private BuildSummaryOptions Option;

    private readonly RcpaComboBox<IAccessNumberParser> acParsers;

    private readonly RcpaCheckBox classifyByCharge;
    private readonly RcpaCheckBox classifyByMissCleavage;
    private readonly RcpaCheckBox classifyByNumberOfProteaseTermini;
    private readonly RcpaCheckBox classifyByModification;
    private readonly RcpaTextField modifiedAminoacids;
    private readonly RcpaCheckBox classifyByProteinTag;
    private readonly RcpaTextField proteinTag;

    private RcpaIntegerField minimumSpectraPerGroup;

    private readonly RcpaFileField database;
    private readonly RcpaTextField decoyPattern;
    private readonly RcpaComboBox<FalseDiscoveryRateLevel> fdrLevel;
    private readonly RcpaComboBox<FalseDiscoveryRateType> fdrType;
    private readonly RcpaCheckBox filterByFdr;
    private RcpaComboBox<ITargetDecoyConflictType> conflictAsDecoy;
    private readonly RcpaCheckBox peptideRetrieval;

    private RcpaCheckBox removeContamination;
    private RcpaTextField contaminationNamePattern;
    private RcpaTextField contaminationDescriptionPattern;

    private readonly RcpaDoubleField maxFdr;
    private readonly RcpaDoubleField maxPeptideFdr;
    private readonly RcpaCheckBox filterProteinByPeptideCount;

    private readonly RcpaCheckBox removeDecoyEntry;

    private readonly RcpaCheckBox filterSequenceLength;
    private readonly RcpaIntegerField minSequenceLength;

    private readonly RcpaCheckBox filterMaxMissCleavage;
    private readonly RcpaIntegerField maxMissCleagage;

    private RcpaIntegerField minAgreeCount;

    private RcpaComboBox<IResolveSearchEngineConflictType> seConflictType;

    private readonly RcpaIntegerField minOneHitWonderPeptideCount;

    private readonly OpenFileArgument openParamFile = new OpenFileArgument("Parameter File", "param");
    private readonly SaveFileArgument saveParamFile = new SaveFileArgument("Parameter File", "param");

    private int tabCount = 0;

    [RcpaOption("ParamFile", RcpaOptionType.String)]
    public string ParamFile { get; set; }

    public UniformSummaryBuilderUI()
    {
      InitializeComponent();

      Option = new BuildSummaryOptions();

      InsertButton(2, btnNew);

      InsertButton(3, this.btnLoadParam);

      InsertButton(4, this.btnSaveParam);

      this.minDecoyScan.DefaultValue = MascotGenericFormatShiftPrecursorProcessorOptions.DEFAULT_ShiftScan.ToString();

      this.decoyPattern = new RcpaTextField(this.txtDecoyPattern, "DecoyPattern", "Decoy Database Pattern", "^REVERSED_", false);
      AddComponent(this.decoyPattern);

      this.removeContamination = new RcpaCheckBox(cbRemoveContamination, "RemoveContamination", false);
      AddComponent(this.removeContamination);

      this.contaminationNamePattern = new RcpaTextField(this.txtContaminantString, "ContaminationNamePattern", "Contaminant Name Pattern", "CON_", false);
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
                                                                    FalseDiscoveryRateLevel.SimpleProtein,
                                                                    FalseDiscoveryRateLevel.UniquePeptide
                                                                  }, 1);
      AddComponent(this.fdrLevel);

      this.peptideRetrieval = new RcpaCheckBox(this.cbPeptideRetrieval, "PeptideRetrieval", true);
      AddComponent(this.peptideRetrieval);

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

      this.classifyByCharge = new RcpaCheckBox(this.cbClassifyByCharge, "ClassifyByCharge", ClassificationOptions.DEFAULT_ClassifyByCharge);
      AddComponent(this.classifyByCharge);

      this.classifyByMissCleavage = new RcpaCheckBox(this.cbClassifyByMissCleavage, "ClassifyByMissCleavage", ClassificationOptions.DEFAULT_ClassifyByMissCleavage);
      AddComponent(this.classifyByMissCleavage);

      this.classifyByNumberOfProteaseTermini = new RcpaCheckBox(this.cbClassifyByPreteaseTermini, "ClassifyByNumberOfProteaseTermini", ClassificationOptions.DEFAULT_ClassifyByNumProteaseTermini);
      AddComponent(this.classifyByNumberOfProteaseTermini);

      this.classifyByModification = new RcpaCheckBox(this.cbClassifyByModification, "ClassifyByModification", ClassificationOptions.DEFAULT_ClassifyByModification);
      AddComponent(this.classifyByModification);

      this.modifiedAminoacids = new RcpaTextField(this.txtFdrModifiedAminoacids, "ModifiedAminoacids", "Modified Aminoacids", "STY", true);
      this.modifiedAminoacids.PreCondition = this.cbClassifyByModification;
      AddComponent(this.modifiedAminoacids);

      this.classifyByProteinTag = new RcpaCheckBox(this.cbClassifyByProteinTag, "ClassifyByProteinTag", ClassificationOptions.DEFAULT_ClassifyByProteinTag);
      AddComponent(this.classifyByProteinTag);

      this.proteinTag = new RcpaTextField(this.txtProteinTag, "ProteinTag", "Protein Tag", "", false);
      this.proteinTag.PreCondition = this.cbClassifyByProteinTag;
      AddComponent(this.proteinTag);

      this.minimumSpectraPerGroup = new RcpaIntegerField(this.txtMinimumSpectraPerGroup, "MinimumSpectraPerGroup", "MinimumSpectraPerGroup", ClassificationOptions.DEFAULT_MinimumSpectraPerGroup, true);
      AddComponent(this.minimumSpectraPerGroup);

      this.filterSequenceLength = new RcpaCheckBox(this.cbSequenceLength, "FilterSequenceLength", false);
      AddComponent(this.filterSequenceLength);

      this.minSequenceLength = new RcpaIntegerField(this.txtMinSequenceLength, "MinSequenceLength", "Minmum Sequence Length", PeptideFilterOptions.DEFAULT_MinSequenceLength, false);
      this.minSequenceLength.PreCondition = cbSequenceLength;
      AddComponent(this.minSequenceLength);

      this.filterMaxMissCleavage = new RcpaCheckBox(this.cbMaxMissCleavage, "FilterMaxMisscleavage", false);
      AddComponent(this.filterMaxMissCleavage);

      this.maxMissCleagage = new RcpaIntegerField(this.txtMaxMissCleavage, "MaxMissCleavage", "Maximum Number of Internal Missed Cleavage", PeptideFilterOptions.DEFAULT_MaxMissCleavage, false);
      this.maxMissCleagage.PreCondition = cbMaxMissCleavage;
      AddComponent(this.maxMissCleagage);

      this.removeDecoyEntry = new RcpaCheckBox(this.cbRemoveDecoyEntry, "RemovePeptideFromDecoyDB", false);
      AddComponent(this.removeDecoyEntry);

      this.database = new RcpaFileField(btnDatabase, txtDatabase, "Database", new OpenFileArgument("Protein Database", "fasta"), "", true);
      AddComponent(this.database);

      this.acParsers = new RcpaComboBox<IAccessNumberParser>(this.cbAccessNumberPattern, "AccessNumberPattern", AccessNumberParserFactory.GetParsers().ToArray(), 0);
      AddComponent(this.acParsers);

      this.seConflictType = new RcpaComboBox<IResolveSearchEngineConflictType>(cbConflict, "ConflictType", ResolveSearchEngineConflictTypeFactory.GetTypes(), 1);
      AddComponent(this.seConflictType);

      this.conflictAsDecoy = new RcpaComboBox<ITargetDecoyConflictType>(cbConflictAsDecoy, "ConflictAsDecoy", ResolveTargetDecoyConflictTypeFactory.GetTypes(), 0);
      AddComponent(this.conflictAsDecoy);

      this.minAgreeCount = new RcpaIntegerField(txtMinAgreeCount, "MinAgreeCount", "Minimum agree count of engines", 1, true);
      AddComponent(this.minAgreeCount);

      minOneHitWonderPeptideCount = new RcpaIntegerField(txtMinOneHitWonderPeptideCount, "MinOneHitWonderPeptideCount", "minimum one-hit-wonder peptide count", 2, false);
      AddComponent(minOneHitWonderPeptideCount);
      minOneHitWonderPeptideCount.PreCondition = FilterOneHitWonder;

      this.AfterLoadOption += DoAfterLoadOption;

      Text = Constants.GetSQHTitle(title, version);

      var engines = EnumUtils.EnumToArray<SearchEngineType>().OrderByDescending(m => m.ToString()).ToArray();
      foreach (var engine in engines)
      {
        if (engine.HasFactory())
        {
          var button = new Button();
          pnlAdd.Controls.Add(button);
          button.Dock = System.Windows.Forms.DockStyle.Top;
          button.UseVisualStyleBackColor = true;
          button.Text = "Add " + engine.ToString();
          button.Name = "btnAdd" + engine.ToString();
          button.Tag = engine;
          button.Click += button_Click;
        }
      }
      pnlAdd.Update();
    }

    void button_Click(object sender, EventArgs e)
    {
      var engine = (SearchEngineType)((sender as Button).Tag);
      DoAddDatasetOption(engine.GetFactory().GetOptions());
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();

      bool bFind = false;
      HashSet<string> names = new HashSet<string>();
      foreach (TabPage ts in tcDatasetList.TabPages)
      {
        var dsf = (ts.Tag as IDatasetFormat);
        dsf.ValidateComponents();
        if (names.Contains(dsf.DatasetName))
        {
          throw new Exception("Duplicated data set name " + dsf.DatasetName);
        }

        names.Add(dsf.DatasetName);

        if (dsf.DatasetEnabled)
        {
          if (rbUseSelectedOnly.Checked)
          {
            if (!dsf.HasValidFile(rbUseSelectedOnly.Checked))
            {
              throw new Exception("At least one file should be selected for dataset " + dsf.DatasetName);
            }
          }

          bFind = true;
        }
      }

      if (!bFind)
      {
        throw new Exception("At least one dataset should be set as enabled!");
      }
    }

    private bool _IsOneEngineMode = false;

    protected void SetOneEngineMode()
    {
      _IsOneEngineMode = true;
      tcDatasetList.Appearance = TabAppearance.FlatButtons;
      tcDatasetList.ItemSize = new Size(0, 1);
      tcDatasetList.SizeMode = TabSizeMode.Fixed;
      lblConflict.Visible = false;
      cbConflict.Visible = false;
    }

    private void DoAfterLoadOption(object sender, EventArgs e)
    {
      if (File.Exists(ParamFile))
      {
        LoadParamFromFile(ParamFile);
      }
    }

    private void DatasetNameChanged(object sender, EventArgs e)
    {
      foreach (TabPage tb in tcDatasetList.TabPages)
      {
        DatasetPanelBase panel = tb.Tag as DatasetPanelBase;
        tb.Text = panel.DatasetName;
      }
    }

    private void AddDatasetOption(IDatasetOptions dsOption, bool assignValue)
    {
      tabCount++;

      var panel = dsOption.CreateControl() as DatasetPanelBase;
      panel.SetShowName(!_IsOneEngineMode);

      if (dsOption.Name == null || dsOption.Name.Length == 0)
      {
        dsOption.Name = dsOption.SearchEngine + tabCount.ToString();
      }

      if (assignValue)
      {
        panel.LoadFromDataset();
      }
      else
      {
        panel.DatasetName = dsOption.Name;
      }

      tcDatasetList.TabPages.Add(dsOption.Name);

      panel.AddNameChangedEvent(DatasetNameChanged);

      tcDatasetList.SelectedIndex = tcDatasetList.TabPages.Count - 1;

      var tb = tcDatasetList.TabPages[tcDatasetList.TabPages.Count - 1];
      tb.Controls.Add(panel);
      tb.Tag = panel;

      panel.Dock = DockStyle.Fill;
    }

    #region Nested type: Command

    public class Command : AbstractCommandLineCommand<UniformSummaryBuilderOptions>, IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Summary;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return version;
      }

      public void Run()
      {
        new UniformSummaryBuilderUI().MyShow();
      }

      #endregion

      public override string Name
      {
        get { return "buildsummary"; }
      }

      public override string Description
      {
        get { return "Build summary from database searching result"; }
      }

      public override RCPA.IProcessor GetProcessor(UniformSummaryBuilderOptions options)
      {
        return new UniformSummaryBuilder(options);
      }
    }

    #endregion

    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (tcDatasetList.TabCount > 0)
      {
        if (MessageBox.Show(this, "Delete dataset " + tcDatasetList.TabPages[tcDatasetList.SelectedIndex].Text + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          var index = tcDatasetList.SelectedIndex;
          tcDatasetList.TabPages.RemoveAt(index);
          Option.DatasetList.RemoveAt(index);
        }
      }
    }

    private void btnLoadParam_Click(object sender, EventArgs e)
    {
      FileDialog dlg = this.openParamFile.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        LoadParamFromFile(dlg.FileName);
      }
    }

    private void LoadParamFromFile(string fileName)
    {
      tabCount = 0;

      ParamFile = fileName;

      BuildSummaryOptionsUtils.LoadFromFile(fileName, Option);

      AssignValueFromOption();
    }

    private void AssignValueFromOption()
    {
      this.minAgreeCount.Value = Option.MinimumEngineAgreeCount;
      this.seConflictType.SelectedItem = Option.ConflictType;

      this.cbIndividual.Checked = !Option.MergeResult;
      this.cbMergeResultFromSameEngineButDifferentSearchParameters.Checked = Option.KeepTopPeptideFromSameEngineButDifferentSearchParameters;

      //Database
      this.database.Text = Option.Database.Location;
      this.decoyPattern.Text = Option.Database.DecoyPattern;
      this.removeDecoyEntry.Checked = Option.Database.RemovePeptideFromDecoyDB;
      this.removeContamination.Checked = Option.Database.RemoveContamination;
      this.contaminationNamePattern.Text = Option.Database.ContaminationNamePattern;
      this.contaminationDescriptionPattern.Text = Option.Database.ContaminationDescriptionPattern;

      foreach (IAccessNumberParser obj in this.acParsers.Items)
      {
        if (obj.RegexPattern.Equals(Option.Database.AccessNumberPattern))
        {
          this.cbAccessNumberPattern.SelectedItem = obj;
          break;
        }

        if (obj.FormatName.Equals(Option.Database.AccessNumberPattern))
        {
          this.cbAccessNumberPattern.SelectedItem = obj;
          break;
        }
      }

      //False Discovery Rate
      this.filterByFdr.Checked = Option.FalseDiscoveryRate.FilterByFdr;
      this.conflictAsDecoy.SelectedItem = Option.FalseDiscoveryRate.TargetDecoyConflictType;
      this.peptideRetrieval.Checked = Option.PeptideRetrieval;
      if (Option.FalseDiscoveryRate.FilterByFdr)
      {
        this.maxFdr.Value = Option.FalseDiscoveryRate.FdrValue;
        this.fdrLevel.SelectedItem = Option.FalseDiscoveryRate.FdrLevel;
        this.fdrType.SelectedItem = Option.FalseDiscoveryRate.FdrType;
        this.maxPeptideFdr.Value = Option.FalseDiscoveryRate.MaxPeptideFdr;
        this.filterProteinByPeptideCount.Checked = Option.FalseDiscoveryRate.FdrPeptideCount > 0;
      }
      this.FilterOneHitWonder.Checked = Option.FalseDiscoveryRate.FilterOneHitWonder;
      this.minOneHitWonderPeptideCount.Value = Option.FalseDiscoveryRate.MinOneHitWonderPeptideCount;
      this.rbByDecoySpectra.Checked = Option.FalseDiscoveryRate.ByDecoySpectra;
      this.rbByDecoyDatabase.Checked = !this.rbByDecoySpectra.Checked;
      this.minDecoyScan.Value = Option.FalseDiscoveryRate.MinDecoyScan;
      this.minTargetDecoyRatio.Value = Option.FalseDiscoveryRate.MinTargetDecoySpectraRatio;

      //Classification
      this.classifyByCharge.Checked = Option.Classification.ClassifyByCharge;
      this.classifyByMissCleavage.Checked = Option.Classification.ClassifyByMissCleavage;
      this.classifyByNumberOfProteaseTermini.Checked = Option.Classification.ClassifyByNumProteaseTermini;
      this.classifyByModification.Checked = Option.Classification.ClassifyByModification;
      this.modifiedAminoacids.Text = Option.Classification.ModifiedAminoacids;
      this.classifyByProteinTag.Checked = Option.Classification.ClassifyByProteinTag;
      this.proteinTag.Text = Option.Classification.ProteinTag;

      //Peptide Filter
      this.filterSequenceLength.Checked = Option.PeptideFilter.FilterBySequenceLength;
      if (Option.PeptideFilter.FilterBySequenceLength)
      {
        this.minSequenceLength.Value = Option.PeptideFilter.MinSequenceLength;
      }
      this.filterMaxMissCleavage.Checked = Option.PeptideFilter.FilterByMaxMissCleavage;
      if (Option.PeptideFilter.FilterByMaxMissCleavage)
      {
        this.maxMissCleagage.Value = Option.PeptideFilter.MaxMissCleavage;
      }

      //Dataset
      tcDatasetList.TabPages.Clear();
      foreach (IDatasetOptions dsOption in Option.DatasetList)
      {
        AddDatasetOption(dsOption, true);
      }
    }

    private void SaveParamToFile(string fileName)
    {
      try
      {
        ValidateComponents();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Validation failed : " + ex.Message);
        return;
      }

      Option.ConflictType = this.seConflictType.SelectedItem;
      Option.MinimumEngineAgreeCount = this.minAgreeCount.Value;
      Option.MergeResult = !this.cbIndividual.Checked;
      Option.KeepTopPeptideFromSameEngineButDifferentSearchParameters = this.cbMergeResultFromSameEngineButDifferentSearchParameters.Checked;

      //Database
      Option.Database.Location = this.database.Text;
      Option.Database.DecoyPattern = this.decoyPattern.Text;
      Option.Database.ContaminationNamePattern = this.contaminationNamePattern.Text;
      Option.Database.RemovePeptideFromDecoyDB = this.removeDecoyEntry.Checked;
      Option.Database.AccessNumberPattern = this.acParsers.SelectedItem.RegexPattern;

      Option.Database.RemoveContamination = this.removeContamination.Checked;
      Option.Database.ContaminationNamePattern = this.contaminationNamePattern.Text;
      Option.Database.ContaminationDescriptionPattern = this.contaminationDescriptionPattern.Text;

      //False Discovery Rate
      Option.FalseDiscoveryRate.FilterByFdr = this.filterByFdr.Checked;
      Option.FalseDiscoveryRate.TargetDecoyConflictType = this.conflictAsDecoy.SelectedItem;
      Option.PeptideRetrieval = this.peptideRetrieval.Checked;

      if (Option.FalseDiscoveryRate.FilterByFdr)
      {
        Option.FalseDiscoveryRate.FdrValue = this.maxFdr.Value;
        Option.FalseDiscoveryRate.FdrLevel = this.fdrLevel.SelectedItem;
        Option.FalseDiscoveryRate.FdrType = this.fdrType.SelectedItem;
        Option.FalseDiscoveryRate.MaxPeptideFdr = this.maxPeptideFdr.Value;
        Option.FalseDiscoveryRate.FdrPeptideCount = this.filterProteinByPeptideCount.Checked ? 10 : 0;
        Option.FalseDiscoveryRate.FilterOneHitWonder = this.FilterOneHitWonder.Checked;
        Option.FalseDiscoveryRate.MinOneHitWonderPeptideCount = this.minOneHitWonderPeptideCount.Value;
        Option.FalseDiscoveryRate.ByDecoySpectra = this.rbByDecoySpectra.Checked;
        Option.FalseDiscoveryRate.MinDecoyScan = this.minDecoyScan.Value;
        Option.FalseDiscoveryRate.MinTargetDecoySpectraRatio = this.minTargetDecoyRatio.Value;
      }

      //Classification
      Option.Classification.ClassifyByCharge = this.classifyByCharge.Checked;
      Option.Classification.ClassifyByMissCleavage = this.classifyByMissCleavage.Checked;
      Option.Classification.ClassifyByNumProteaseTermini = this.classifyByNumberOfProteaseTermini.Checked;

      Option.Classification.ClassifyByModification = this.classifyByModification.Checked;
      Option.Classification.ModifiedAminoacids = this.modifiedAminoacids.Text;

      Option.Classification.ClassifyByProteinTag = this.classifyByProteinTag.Checked;
      Option.Classification.ProteinTag = this.proteinTag.Text;

      //Peptide Filter
      Option.PeptideFilter.FilterBySequenceLength = this.filterSequenceLength.Checked;
      if (Option.PeptideFilter.FilterBySequenceLength)
      {
        Option.PeptideFilter.MinSequenceLength = this.minSequenceLength.Value;
      }

      Option.PeptideFilter.FilterByMaxMissCleavage = this.filterMaxMissCleavage.Checked;
      if (Option.PeptideFilter.FilterByMaxMissCleavage)
      {
        Option.PeptideFilter.MaxMissCleavage = this.maxMissCleagage.Value;
      }

      //Dataset
      foreach (TabPage ts in tcDatasetList.TabPages)
      {
        (ts.Tag as IDatasetFormat).SaveToDataset(rbUseSelectedOnly.Checked);
      }

      Option.SaveToFile(fileName);
    }

    private void btnSaveParam_Click(object sender, EventArgs e)
    {
      try
      {
        ValidateComponents();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, "Validation failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      if (saveParamFile.GetFileDialog().ShowDialog() == DialogResult.OK)
      {
        ParamFile = saveParamFile.GetFileDialog().FileName;
        SaveParamToFile(ParamFile);
      }
    }

    protected override IProcessor GetProcessor()
    {
      FileDialog dlg = this.saveParamFile.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        ParamFile = dlg.FileName;
        SaveParamToFile(ParamFile);
        return new UniformSummaryBuilder(new UniformSummaryBuilderOptions()
        {
          InputFile = ParamFile
        });
      }
      else
      {
        throw new UserTerminatedException();
      }
    }

    protected void DoAddDatasetOption(IDatasetOptions dsOption)
    {
      Option.DatasetList.Add(dsOption);

      AddDatasetOption(dsOption, false);

      this.ResumeLayout();
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
      this.Option = new BuildSummaryOptions();

      AssignValueFromOption();
    }

    private void cbFdrLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
      cbPeptideRetrieval.Visible = cbFdrLevel.Text.Equals("Protein");
    }
  }
}
