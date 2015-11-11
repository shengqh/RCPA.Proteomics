
using RCPA.Commandline;
using RCPA.Gui.Command;
namespace RCPA.Proteomics.Statistic
{
  public class BuildSummaryResultParserCommand : AbstractCommandLineCommand<BuildSummaryResultParserOptions>, IToolCommand
  {
    #region ICommandLineCommand Members

    public override string Name
    {
      get { return "buildsummary_parser"; }
    }

    public override string Description
    {
      get { return "Parsing Build Summary result and generate statistic table"; }
    }

    public override RCPA.IProcessor GetProcessor(BuildSummaryResultParserOptions options)
    {
      return new BuildSummaryResultParser(options);
    }

    #endregion


    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Summary;
    }

    public string GetCaption()
    {
      return BuildSummaryResultParserUI.Title;
    }

    public string GetVersion()
    {
      return BuildSummaryResultParserUI.Version;
    }

    public void Run()
    {
      new BuildSummaryResultParserUI().MyShow();
    }

    #endregion
  }
}
