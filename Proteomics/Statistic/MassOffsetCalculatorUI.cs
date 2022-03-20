using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Statistic
{
  public partial class MassOffsetCalculatorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Mass Offset Calculator";

    private static readonly string version = "1.0.0";
    private RcpaDoubleField maxShiftPPM, precursorPPM, rtWindow;

    public MassOffsetCalculatorUI()
    {
      InitializeComponent();

      base.SetFileArgument("RawFile", new OpenFileArgument("RAW", "raw"));
      this.Text = Constants.GetSQHTitle(title, version);

      maxShiftPPM = new RcpaDoubleField(txtInitPPM, "InitPPM", "Maximum shift ppm", 50, true);
      AddComponent(maxShiftPPM);

      precursorPPM = new RcpaDoubleField(txtPrecursorPPM, "PrecursorPPM", "Maximum precursor ppm", 10, true);
      AddComponent(precursorPPM);

      rtWindow = new RcpaDoubleField(txtRtWindow, "RtWindow", "Smoothing retention time window in mintue", 0.5, true);
      AddComponent(rtWindow);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MassOffsetCalculator(siliconePolymerIons.GetSelectedPloymers().ToArray(), maxShiftPPM.Value, rtWindow.Value);
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
        new MassOffsetCalculatorUI().MyShow();
      }

      #endregion
    }
  }

}
