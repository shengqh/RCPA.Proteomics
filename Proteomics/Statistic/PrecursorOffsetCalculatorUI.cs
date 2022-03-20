using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Statistic
{
  public partial class PrecursorOffsetCalculatorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Precursor Offset Calculator";
    private static readonly string version = "1.0.0";

    public PrecursorOffsetCalculatorUI()
    {
      InitializeComponent();

      base.SetFileArgument("PeptideFile", new OpenFileArgument("Identified Peptides", "peptides"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new PrecursorOffsetCalculator();
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
        new PrecursorOffsetCalculatorUI().MyShow();
      }

      #endregion
    }
  }
}

