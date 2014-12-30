using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Proteomics.Mascot;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Gui;

namespace RCPA.Proteomics.MaxQuant
{
  public partial class MaxQuantSiteToPeptideProcessorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "MaxQuant Sites To Mascot Peptide Converter";
    public static readonly string version = "1.0.2";

    private RcpaDoubleField minLocalizationProbability;
    private RcpaDoubleField minScoreDiff;

    public MaxQuantSiteToPeptideProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("MaxQuantSites", new OpenFileArgument("MaxQuant Sites", new string[] { "txt" }));

      this.minLocalizationProbability = new RcpaDoubleField(txtMinProbability, "minLocalizationProbability", "Minmum Localization Probability", 0.75, true);
      AddComponent(this.minLocalizationProbability);

      this.minScoreDiff = new RcpaDoubleField(txtMinScoreDiff, "minScoreDiff", "Minmum Score Diff", 5.0, true);
      AddComponent(this.minScoreDiff);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MaxQuant2MascotPeptideProcessor2(minLocalizationProbability.Value, minScoreDiff.Value);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.MaxQuant;
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
        new MaxQuantSiteToPeptideProcessorUI().MyShow();
      }

      #endregion
    }
  }
}

