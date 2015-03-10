
using RCPA.Commandline;
namespace RCPA.Proteomics.Statistic
{
  public class BuildSummaryResultParserCommand : AbstractCommandLineCommand<BuildSummaryResultParserOptions>
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
  }
}
