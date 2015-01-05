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
  public partial class IsobaricPeptideStatisticBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Isobaric Labeling Peptide Statistic Builder";
    private static readonly string version = "1.2.2";

    private RcpaFileField isobaricFile;
    private RcpaCheckBox normalize;
    private RcpaCheckBox modifiedPeptideOnly;
    private RcpaTextField modifiedChar;

    public IsobaricPeptideStatisticBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("PeptidesFile", new OpenFileArgument("Peptides", "peptides"));

      this.isobaricFile = new RcpaFileField(btnDesignFile, txtIsobaricXmlFile, "IsobaricDesignFile", new OpenFileArgument("Isobaric Labeling Experimental Design", "experimental.xml"), true);
      this.AddComponent(this.isobaricFile);

      normalize = new RcpaCheckBox(cbNormalize, "Normalize", false);
      AddComponent(normalize);

      modifiedPeptideOnly = new RcpaCheckBox(cbModifiedOnly, "ModifiedOnly", false);
      AddComponent(modifiedPeptideOnly);

      modifiedChar = new RcpaTextField(txtModifiedCharacter, "ModifiedChar", "Input modified characters which indicates isobaric labelling(such as @#)", "@#", false);
      modifiedChar.PreCondition = cbModifiedOnly;
      AddComponent(modifiedChar);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();
      new IsobaricLabelingExperimentalDesign().LoadFromFile(isobaricFile.FullName);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      var option = GetStatisticOption();

      return new IsobaricPeptideStatisticBuilder(option);
    }

    protected IsobaricPeptideStatisticBuilderOption GetStatisticOption()
    {
      var design = new IsobaricLabelingExperimentalDesign();
      design.LoadFromFile(isobaricFile.FullName);

      var option = new IsobaricPeptideStatisticBuilderOption();

      option.DatasetMap = design.DatasetMap;
      option.IsobaricFileName = design.IsobaricFileName;
      option.References = design.References;
      option.PlexType = IsobaricScanXmlUtils.GetIsobaricType(design.IsobaricFileName);

      option.QuantifyModifiedPeptideOnly = modifiedPeptideOnly.Checked;
      option.ModificationChars = modifiedChar.Text;
      option.PerformNormalizition = normalize.Checked;

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
