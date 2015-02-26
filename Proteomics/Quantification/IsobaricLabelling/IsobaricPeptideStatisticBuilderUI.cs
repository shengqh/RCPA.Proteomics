using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using RCPA.Gui.Image;
using ZedGraph;
using RCPA.Utils;
using RCPA.Proteomics.Quantification;
using System.IO;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;
using RCPA.Proteomics.Summary;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public partial class IsobaricPeptideStatisticBuilderUI : AbstractProcessorUI
  {
    private static readonly string title = "Isobaric Labeling Peptide Statistic Builder";
    private static readonly string version = "1.2.5";

    private RcpaFileField peptideFile;
    private RcpaFileField designFile;
    private RcpaCheckBox normalize;
    private RcpaTextField modifiedAminoacids;
    private RcpaDoubleField minimumSiteProbability;
    private RcpaComboBox<QuantifyMode> modes;

    public IsobaricPeptideStatisticBuilderUI()
    {
      InitializeComponent();

      peptideFile = new RcpaFileField(btnPeptideFile, txtPeptideFile, "PeptideFile", new OpenFileArgument("Peptides", "peptides"), true);
      AddComponent(this.peptideFile);

      designFile = new RcpaFileField(btnDesignFile, txtDesignFile, "IsobaricDesignFile", new OpenFileArgument("Isobaric Labeling Experimental Design", "experimental.xml"), true);
      AddComponent(this.designFile);

      normalize = new RcpaCheckBox(cbNormalize, "Normalize", false);
      AddComponent(normalize);

      modes = new RcpaComboBox<QuantifyMode>(cbQuantifyMode, "QuantifyMode", EnumUtils.EnumToArray<QuantifyMode>(), 0, true);
      AddComponent(modes);

      modifiedAminoacids = new RcpaTextField(txtModifiedAminoacids, "ModifiedAminoacids", "Input modified amino acids", "STY", false);
      AddComponent(modifiedAminoacids);

      minimumSiteProbability = new RcpaDoubleField(txtMinimumSiteProbability, "MinimumSiteProbability", "Input minimum phosphylation site probability", 0.9, false);
      AddComponent(minimumSiteProbability);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      modifiedAminoacids.Required = modes.SelectedItem != QuantifyMode.qmAll;
      minimumSiteProbability.Required = modes.SelectedItem == QuantifyMode.qmModificationSite;
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();

      try
      {
        new IsobaricLabelingExperimentalDesign().LoadFromFile(designFile.FullName);
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("Parsing design file {0} error : {1}", designFile.FullName, ex.Message));
      }
    }

    protected override IProcessor GetProcessor()
    {
      var option = GetStatisticOption();

      return new IsobaricPeptideStatisticBuilder(option);
    }

    protected IsobaricPeptideStatisticBuilderOptions GetStatisticOption()
    {
      var option = new IsobaricPeptideStatisticBuilderOptions();

      option.PeptideFile = peptideFile.FullName;
      option.DesignFile = designFile.FullName;
      option.Mode = modes.SelectedItem;
      option.ModifiedAminoacids = modifiedAminoacids.Text;
      option.PerformNormalizition = normalize.Checked;
      if (option.Mode == QuantifyMode.qmModificationSite)
      {
        option.MinimumSiteProbability = minimumSiteProbability.Value;
      }

      return option;
    }

    public class Command : IToolSecondLevelCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Quantification;
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
        new IsobaricPeptideStatisticBuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return MenuCommandType.Quantification_IsobaricLabelling_NEW;
      }

      #endregion
    }
  }
}
