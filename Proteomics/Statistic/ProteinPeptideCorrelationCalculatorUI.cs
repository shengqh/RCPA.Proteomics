using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RCPA.Proteomics.Statistic
{
  public partial class ProteinPeptideCorrelationCalculatorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Protein Peptide Correlation Calculator";

    private static readonly string version = "1.0.0";

    public ProteinPeptideCorrelationCalculatorUI()
    {
      InitializeComponent();

      base.SetFileArgument("SourceFile", new OpenFileArgument("Noredundant", "noredundant"));

      AddComponent(pnlClassification);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      var originalFile = string.Empty;
      try
      {
        originalFile = GetOriginFile();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error");
        return;
      }

      HashSet<string> experimentals = new IdentifiedResultExperimentalReader().ReadFromFile(originalFile);

      List<string> sortedExperimentals = new List<string>(experimentals);
      sortedExperimentals.Sort();

      if (sortedExperimentals.Count > 0)
      {
        pnlClassification.InitializeClassificationSet(sortedExperimentals);
      }
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new CorrleationCalculator()
      {
        ClassificationSet = pnlClassification.GetClassificationMap()
      };
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Statistic;
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
        new ProteinPeptideCorrelationCalculatorUI().MyShow();
      }

      #endregion
    }
  }
}
