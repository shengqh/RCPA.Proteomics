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

namespace RCPA.Proteomics.Statistic
{
  public partial class BuildSummaryResultParserUI : AbstractFileProcessorUI
  {
    private static string title = "BuildSummary Result Parser";
    private static string version = "1.0.2";

    private RcpaTextField decoyPattern;

    private RcpaComboBox<FalseDiscoveryRateType> fdrType;

    public BuildSummaryResultParserUI()
    {
      InitializeComponent();

      this.decoyPattern = new RcpaTextField(this.txtDecoyPattern, "DecoyPattern", "Decoy Database Pattern", "^REVERSED_", false);
      AddComponent(this.decoyPattern);

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

      base.SetDirectoryArgument("Directory", "BuildSummary");

      Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      IFalseDiscoveryRateCalculator calc;
      if (fdrType.SelectedItem == FalseDiscoveryRateType.Target)
      {
        calc = new TargetFalseDiscoveryRateCalculator();
      }
      else
      {
        calc = new TotalFalseDiscoveryRateCalculator();
      }

      return new BuildSummaryResultParser(calc, decoyPattern.Text);
    }

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
        new BuildSummaryResultParserUI().MyShow();
      }

      #endregion
    }

  }
}