using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public partial class IsobaricLabelingEfficiencyCalculatorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Isobaric Labelling Efficiency Calculator";

    private static readonly string version = "1.0.0";

    public IsobaricLabelingEfficiencyCalculatorUI()
    {
      InitializeComponent();
      
      base.SetFileArgument("PeptidesFile", new OpenFileArgument("Peptides", "peptides"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new IsobaricLabelingEfficiencyCalculator();
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
