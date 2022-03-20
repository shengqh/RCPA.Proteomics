using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Format
{
  public partial class BuildSummaryWebFormatConverterUI : AbstractFileProcessorUI
  {
    private static string title = "BuildSummary Web Format Converter";
    private static string version = "1.0.0";

    public BuildSummaryWebFormatConverterUI()
    {
      InitializeComponent();

      this.SetFileArgument("SourceFile", new OpenFileArgument("BuildSummary Noredundant", "noredundant"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new BuildSummaryWebFormatConverter();
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Format;
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
        new BuildSummaryWebFormatConverterUI().MyShow();
      }

      #endregion
    }
  }
}
