using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Format
{
  public partial class MultipleRaw2MSnProcessorUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Multiple RAW to MSn Processor";

    private static readonly string version = "1.0.0";

    public MultipleRaw2MSnProcessorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.SetDirectoryArgument("TargetDir", "Target");

      rawFiles.FileArgument = new OpenFileArgument("Thermo RAW", "raw");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MultipleRaw2MSnProcessor(new MultipleRaw2MSnProcessorOptions()
      {
        RawFiles = rawFiles.FileNames,
        TargetDirectory = GetOriginFile(),
        ParallelMode = true
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
        new MultipleRaw2MSnProcessorUI().MyShow();
      }

      #endregion
    }
  }
}