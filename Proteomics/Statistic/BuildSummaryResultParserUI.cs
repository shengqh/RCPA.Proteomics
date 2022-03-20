using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Statistic
{
  public partial class BuildSummaryResultParserUI : AbstractProcessorUI
  {
    public static string Title { get { return "BuildSummary Result Parser"; } }
    public static string Version { get { return "1.0.2"; } }

    private RcpaTextField decoyPattern;

    private RcpaComboBox<IFalseDiscoveryRateCalculator> fdrType;

    public BuildSummaryResultParserUI()
    {
      InitializeComponent();

      this.decoyPattern = new RcpaTextField(this.txtDecoyPattern, "DecoyPattern", "Decoy Database Pattern", "^REVERSED_", false);
      AddComponent(this.decoyPattern);

      this.fdrType = new RcpaComboBox<IFalseDiscoveryRateCalculator>(this.cbFdrType, "FdrType",
                                                              new IFalseDiscoveryRateCalculator[]
                                                                {
                                                                  new TargetFalseDiscoveryRateCalculator(),
                                                                  new TotalFalseDiscoveryRateCalculator()
                                                                },
                                                              new[]
                                                                {
                                                                  "Target : N(decoy) / N(target)",
                                                                  "Global : N(decoy) * 2 / (N(decoy) + N(target))"
                                                                }, 0);
      AddComponent(this.fdrType);

      Text = Constants.GetSQHTitle(Title, Version);
    }

    protected override IProcessor GetProcessor()
    {
      var options = new BuildSummaryResultParserOptions()
      {
        InputDirectory = inputDirectory.FullName,
        DecoyPattern = decoyPattern.Text,
        TargetFDR = fdrType.SelectedIndex == 0
      };

      return new BuildSummaryResultParser(options);
    }
  }
}