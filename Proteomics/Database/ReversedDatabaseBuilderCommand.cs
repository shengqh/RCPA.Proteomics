using RCPA.Commandline;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.Database
{
  public class ReversedDatabaseBuilderCommand : AbstractCommandLineCommand<ReversedDatabaseBuilderOptions>, IToolCommand
  {
    #region AbstractCommandLineCommand
    public override string Name
    {
      get
      {
        return "decoy_database";
      }
    }

    public override string Description
    {
      get
      {
        return "Build Target-Decoy Database";
      }
    }

    public override IProcessor GetProcessor(ReversedDatabaseBuilderOptions options)
    {
      return new ReversedDatabaseBuilder(options);
    }

    #endregion

    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Database;
    }

    public string GetCaption()
    {
      return ReversedDatabaseBuilderUI.title;
    }

    public string GetVersion()
    {
      return ReversedDatabaseBuilderUI.version;
    }

    public void Run()
    {
      new ReversedDatabaseBuilderUI().MyShow();
    }

    #endregion
  }
}
