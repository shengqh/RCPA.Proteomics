using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Tools.Distiller
{
  public partial class DistinctResultDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Distinct Identification Result Distiller";

    public static readonly string version = "1.0.0";

    public DistinctResultDistillerUI()
    {
      InitializeComponent();

      SetFileArgument("NoredundantFile", new OpenFileArgument("noredundant", "noredundant"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new DistinctResultDistiller();
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
        new DistinctResultDistillerUI().MyShow();
      }

      #endregion
    }
  }
}

