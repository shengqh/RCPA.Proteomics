using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Statistic
{
  public partial class PrecursorOffsetStatisticMainBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Precursor Offset Frequency Statistic Builder";

    private static readonly string version = "1.0.0";

    private RcpaDoubleField productIonPPM;

    private RcpaDoubleField minRelativeIntensity;

    public PrecursorOffsetStatisticMainBuilderUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.SetDirectoryArgument("TargetDir", "Target");

      this.productIonPPM = new RcpaDoubleField(txtProductIonPPM, "ProductIonPPMTolerance", "Product Ion PPM Tolerance", 20, true);
      AddComponent(this.productIonPPM);

      this.minRelativeIntensity = new RcpaDoubleField(txtMinimumIonRelativeIntensity, "MinimumIonRelativeIntensity", "Minimum Ion Relative Intensity", 0.05, true);
      AddComponent(this.minRelativeIntensity);

      this.rawFiles.FileArgument = new OpenFileArgument("Raw", RawFileFactory.GetSupportedRawFormats());
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new PrecursorOffsetStatisticMainBuilder(this.rawFiles.FileNames, productIonPPM.Value, minRelativeIntensity.Value, 0.05);
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
        new PrecursorOffsetStatisticMainBuilderUI().MyShow();
      }

      #endregion
    }
  }
}