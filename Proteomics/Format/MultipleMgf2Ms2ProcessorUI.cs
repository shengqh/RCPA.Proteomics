using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Format
{
  public partial class MultipleMgf2Ms2ProcessorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Multiple MGF to MS2 Processor";

    private static readonly string version = "1.0.0";

    public MultipleMgf2Ms2ProcessorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.SetDirectoryArgument("TargetDir", "Target");

      mgfFiles.FileArgument = new OpenFileArgument("Mascot Generic Format", "mgf");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MultipleMgf2Ms2Processor(new MultipleMgf2Ms2ProcessorOptions()
      {
        InputFiles = mgfFiles.FileNames,
        TargetDir = GetOriginFile(),
        Parser = new DefaultTitleParser(TitleParserUtils.GetTitleParsers())
      });
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
        new MultipleMgf2Ms2ProcessorUI().MyShow();
      }

      #endregion
    }
  }
}