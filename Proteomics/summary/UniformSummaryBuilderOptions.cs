using CommandLine;
using RCPA.Commandline;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public class UniformSummaryBuilderOptions : AbstractOptions
  {
    [Option('i', "inputFile", Required = true, MetaValue = "FILE", HelpText = "Input parameter file")]
    public string InputFile { get; set; }

    [Option('p', "peptideFile", Required = false, MetaValue = "FILE", HelpText = "Build summary from prefiltered peptide file")]
    public string PeptideFile { get; set; }

    public override bool PrepareOptions()
    {
      if (!File.Exists(this.InputFile))
      {
        ParsingErrors.Add(string.Format("File not exists {0}.", this.InputFile));
      }

      if (!string.IsNullOrEmpty(this.PeptideFile) && !File.Exists(this.InputFile))
      {
        ParsingErrors.Add(string.Format("File not exists {0}.", this.PeptideFile));
      }

      return ParsingErrors.Count == 0;
    }
  }
}
