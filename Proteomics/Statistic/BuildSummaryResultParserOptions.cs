using CommandLine;
using RCPA.Commandline;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Statistic
{
  public class BuildSummaryResultParserOptions : AbstractOptions
  {
    [Option('i', "inputDirectory", Required = true, MetaValue = "DIRECTORY", HelpText = "BuildSummary directory")]
    public string InputDirectory { get; set; }

    [Option("byBuildSummaryOptions", Required = false, MetaValue = "BOOLEAN", HelpText = "Get decoy definition and FDR calculation from BuildSummary options")]
    public bool ByBuildSummaryOptions { get; set; }

    [Option('t', "targetFDR", MetaValue = "BOOLEAN", HelpText = "Calculate FDR by target formula (decoy / target), default is global formula (decoy * 2 / (target + decoy))")]
    public bool TargetFDR { get; set; }

    public BuildSummaryResultParserOptions() { }

    public IFalseDiscoveryRateCalculator Calculator
    {
      get
      {
        if (TargetFDR)
        {
          return new TargetFalseDiscoveryRateCalculator();
        }
        else
        {
          return new TotalFalseDiscoveryRateCalculator();
        }
      }
    }

    [Option('p', "decoyPattern", Required = false, MetaValue = "REGEX", HelpText = "decoy pattern")]
    public string DecoyPattern { get; set; }

    private string _outputFile;

    [Option('o', "outputFile", Required = true, MetaValue = "FILE", HelpText = "output file")]
    public string OutputFile
    {
      get
      {
        if (string.IsNullOrEmpty(_outputFile))
        {
          return Path.Combine(this.InputDirectory, "summary.txt.xls");
        }
        else
        {
          return _outputFile;
        }
      }
      set
      {
        _outputFile = value;
      }
    }

    public override bool PrepareOptions()
    {
      CheckDirectory("Build Summary", InputDirectory);

      if (!ByBuildSummaryOptions)
      {
        CheckPattern("Decoy", DecoyPattern);
      }

      return ParsingErrors.Count == 0;
    }
  }
}
