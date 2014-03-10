using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Raw
{
  public partial class RawScanParentChildDistillerUI : AbstractProcessorUI
  {
    public static string Title = "Raw Scan Parent Children Distiller";
    public static string Version = "1.0.0";

    public RawScanParentChildDistillerUI()
    {
      InitializeComponent();

      rawFile.FileArgument = new OpenFileArgument("RAW", ".raw");

      Text = Constants.GetSQHTitle(Title, Version);
    }

    protected override IProcessor GetProcessor()
    {
      var options = new RawScanParentChildDistillerOptions()
      {
        InputFile = rawFile.FullName,
        OutputFile = rawFile.FullName + ".scan"
      };

      return new RawScanParentChildDistiller(options);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Distiller;
      }

      public string GetCaption()
      {
        return Title;
      }

      public string GetVersion()
      {
        return Version;
      }

      public void Run()
      {
        new RawScanParentChildDistillerUI().MyShow();
      }

      #endregion
    }
  }
}