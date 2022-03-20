using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.IO;

namespace RCPA.Tools.Distiller
{
  public partial class TurboMsLevelDistillerUI : AbstractTurboProcessorUI
  {
    public static string title = "Turbo MsLevel Distiller";
    public static string version = "1.0.0";

    public TurboMsLevelDistillerUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      base.SetFileArgument("RawFile", new OpenFileArgument("Thermo Raw", "raw"));
      base.SetDirectoryArgument("RawDir", "Raw");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (IsBatchMode())
      {
        return new MsLevelMultipleDistiller();
      }
      else
      {
        return new MsLevelSingleDistiller();
      }
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
        return title;
      }

      public string GetVersion()
      {
        return version;
      }

      public void Run()
      {
        new TurboMsLevelDistillerUI().MyShow();
      }

      #endregion
    }
  }
}