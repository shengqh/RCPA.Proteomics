using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;

namespace RCPA.Proteomics.Quantification
{
  public partial class TurboCensusMs1IndexBuilderUI : AbstractTurboProcessorUI
  {
    private static readonly string title = "Turbo Census MS1 Index Builder";

    private static readonly string version = "1.0.0";

    public TurboCensusMs1IndexBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("Ms1File", new OpenFileArgument("Census MS1", "ms1"));

      base.SetDirectoryArgument("Ms1Directory", "Census MS1");

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (IsBatchMode())
      {
        return new CensusMs1IndexDirectoryBuilder();
      }

      return new CensusMs1IndexFileBuilder();
    }

    public class Command : IToolSecondLevelCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Quantification;
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
        new TurboCensusMs1IndexBuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "Census";
      }

      #endregion
    }
  }
}