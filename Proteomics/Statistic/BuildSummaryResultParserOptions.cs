using CommandLine;
using RCPA.Commandline;
using RCPA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Statistic
{
  public class BuildSummaryResultParserOptions : AbstractOptions
  {
    [Option('i', "inputDirectory", Required = true, MetaValue = "DIRECTORY", HelpText = "BuildSummary directory")]
    public string InputDirectory { get; set; }

    public IFalseDiscoveryRateCalculator Calculator { get; set; }

    [Option('p', "decoyPattern", Required = true, MetaValue = "REGEX", HelpText = "decoy pattern")]
    public string DecoyPattern { get; set; }

    private string _outputFile;

    [Option('o', "outputFile", Required = false, MetaValue = "FILE", HelpText = "output file")]
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

    public BuildSummaryResultParserOptions()
    {
      Calculator = new TargetFalseDiscoveryRateCalculator();
    }

    public override bool PrepareOptions()
    {
      CheckDirectory("Build Summary", InputDirectory);

      CheckPattern("Decoy", DecoyPattern);

      return ParsingErrors.Count == 0;
    }
  }
}
