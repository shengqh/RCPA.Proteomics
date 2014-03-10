using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Gui.Command;
using RCPA.Seq;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Tools.Summary
{
  /// <summary>
  /// uniform buildsummary GUI。
  /// 20110128，增加了对peptideprophet的兼容。
  /// </summary>
  public partial class UniformBuildSummaryUI : AbstractProcessorFileUI
  {
    public static string title = "BuildSummary - A general framework for assembling protein identifications";
    public static string version = "7.0.3";

    private BuildSummaryOptions Option;

    private readonly RcpaComboBox<IAccessNumberParser> acParsers;

    private readonly RcpaCheckBox classifyByCharge;

    private readonly RcpaCheckBox classifyByMissCleavage;
    private readonly RcpaCheckBox classifyByNumberOfProteaseTermini;

    private readonly RcpaCheckBox classifyByModification;
    private readonly RcpaFileField database;
    private readonly RcpaTextField decoyPattern;
    private readonly RcpaComboBox<FalseDiscoveryRateLevel> fdrLevel;
    private readonly RcpaComboBox<FalseDiscoveryRateType> fdrType;
    private readonly RcpaCheckBox filterByFdr;
    private RcpaComboBox<ITargetDecoyConflictType> conflictAsDecoy;

    private RcpaCheckBox removeContamination;
    private RcpaTextField contaminationNamePattern;
    private RcpaTextField contaminationDescriptionPattern;

    private readonly RcpaDoubleField maxFdr;
    private readonly RcpaDoubleField maxPeptideFdr;
    private readonly RcpaCheckBox filterProteinByPeptideCount;

    private readonly RcpaTextField modifiedAminoacids;

    private readonly RcpaCheckBox removeDecoyEntry;

    private readonly RcpaCheckBox filterSequenceLength;
    private readonly RcpaIntegerField minSequenceLength;
    private RcpaIntegerField minAgreeCount;

    private RcpaComboBox<IResolveSearchEngineConflictType> seConflictType;

    private readonly RcpaIntegerField minOneHitWonderPeptideCount;

    private readonly OpenFileArgument openParamFile = new OpenFileArgument("Parameter File", "param");
    private readonly SaveFileArgument saveParamFile = new SaveFileArgument("Parameter File", "param");

    private int tabCount = 0;

    [RcpaOption("ParamFile", RcpaOptionType.String)]
    public string ParamFile { get; set; }

    public UniformBuildSummaryUI()
    {
      InitializeComponent();

      Option = new BuildSummaryOptions();

      InsertButton(2, btnNew);

      InsertButton(3, this.btnLoadParam);

      InsertButton(4, this.btnSaveParam);

      this.decoyPattern = new RcpaTextField(this.txtDecoyPattern, "DecoyPattern", "Decoy Database Pattern", "^REVERSED_", false);
      AddComponent(this.decoyPattern);

      this.removeContamination = new RcpaCheckBox (cbRemoveContamination,"RemoveContamination", false);
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
                                                                    FalseDiscoveryRateLevel.UniquePeptide
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

      this.filterSequenceLength = new RcpaCheckBox(this.cbSequenceLength, "FilterSequenceLength", false);
      AddComponent(this.filterSequenceLength);

      this.minSequenceLength = new RcpaIntegerField(this.txtMinSequenceLength, "MinSequenceLength", "Minmum Sequence Length", 6, false);
      AddComponent(this.minSequenceLength);

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

      //DatasetFactory.GetInstance();
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();

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
      }
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

    public class Command : IToolCommand
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
        new UniformBuildSummaryUI().MyShow();
      }

      #endregion
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

      //Classification
      this.classifyByCharge.Checked = Option.Classification.ClassifyByCharge;
      this.classifyByMissCleavage.Checked = Option.Classification.ClassifyByMissCleavage;
      this.classifyByNumberOfProteaseTermini.Checked = Option.Classification.ClassifyByNumProteaseTermini;
      this.classifyByModification.Checked = Option.Classification.ClassifyByModification;
      this.modifiedAminoacids.Text = Option.Classification.ModifiedAminoacids;

      //Peptide Filter
      this.filterSequenceLength.Checked = Option.PeptideFilter.FilterBySequenceLength;
      if (Option.PeptideFilter.FilterBySequenceLength)
      {
        this.minSequenceLength.Value = Option.PeptideFilter.MinSequenceLength;
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
      if (Option.FalseDiscoveryRate.FilterByFdr)
      {
        Option.FalseDiscoveryRate.FdrValue = this.maxFdr.Value;
        Option.FalseDiscoveryRate.FdrLevel = this.fdrLevel.SelectedItem;
        Option.FalseDiscoveryRate.FdrType = this.fdrType.SelectedItem;
        Option.FalseDiscoveryRate.MaxPeptideFdr = this.maxPeptideFdr.Value;
        Option.FalseDiscoveryRate.FdrPeptideCount = this.filterProteinByPeptideCount.Checked ? 10 : 0;
        Option.FalseDiscoveryRate.FilterOneHitWonder = this.FilterOneHitWonder.Checked;
        Option.FalseDiscoveryRate.MinOneHitWonderPeptideCount = this.minOneHitWonderPeptideCount.Value;
      }

      //Classification
      Option.Classification.ClassifyByCharge = this.classifyByCharge.Checked;
      Option.Classification.ClassifyByMissCleavage = this.classifyByMissCleavage.Checked;
      Option.Classification.ClassifyByNumProteaseTermini = this.classifyByNumberOfProteaseTermini.Checked;

      Option.Classification.ClassifyByModification = this.classifyByModification.Checked;
      Option.Classification.ModifiedAminoacids = this.modifiedAminoacids.Text;

      //Peptide Filter
      Option.PeptideFilter.FilterBySequenceLength = this.filterSequenceLength.Checked;
      if (Option.PeptideFilter.FilterBySequenceLength)
      {
        Option.PeptideFilter.MinSequenceLength = this.minSequenceLength.Value;
      }

      //Dataset
      foreach (TabPage ts in tcDatasetList.TabPages)
      {
        (ts.Tag as IDatasetFormat).SaveToDataset();
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

    protected override IFileProcessor GetFileProcessor()
    {
      return new UniformIdentifiedResultBuilder();
    }

    protected override string GetOriginFile()
    {
      FileDialog dlg = this.saveParamFile.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        ParamFile = dlg.FileName;
        SaveParamToFile(ParamFile);
        return dlg.FileName;
      }
      else
      {
        throw new UserTerminatedException();
      }
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
      this.Option = new BuildSummaryOptions();

      AssignValueFromOption();
    }

    private void btnXtandem_Click(object sender, EventArgs e)
    {
      DoAddDatasetOption(new XtandemDatasetOptions());
    }

    private void btnPFind_Click(object sender, EventArgs e)
    {
      DoAddDatasetOption(new PFindDatasetOptions());
    }

    private void btnAddPeptideProphet_Click(object sender, EventArgs e)
    {
      DoAddDatasetOption(new PeptideProphetDatasetOptions());
    }

    private void btnAddSequest_Click(object sender, EventArgs e)
    {
      DoAddDatasetOption(new SequestDatasetOptions());
    }

    private void btnAddMascot_Click(object sender, EventArgs e)
    {
      DoAddDatasetOption(new MascotDatasetOptions());
    }

    private void DoAddDatasetOption(IDatasetOptions dsOption)
    {
      Option.DatasetList.Add(dsOption);

      AddDatasetOption(dsOption, false);

      this.ResumeLayout();
    }
  }
}
