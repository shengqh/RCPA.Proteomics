﻿using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public partial class IsobaricLabelingEfficiencyCalculatorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Isobaric Labeling Efficiency Calculator";

    private static readonly string version = "1.0.0";

    public IsobaricLabelingEfficiencyCalculatorUI()
    {
      InitializeComponent();

      base.SetFileArgument("PeptidesFile", new OpenFileArgument("Peptides", "peptides"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new IsobaricLabelingEfficiencyCalculator(txtModifiedAminoacid.Text[0]);
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
        new IsobaricLabelingEfficiencyCalculatorUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return MenuCommandType.Quantification_IsobaricLabelling;
      }

      #endregion
    }
  }
}
