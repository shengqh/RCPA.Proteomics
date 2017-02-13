using CommandLine;
using RCPA.Proteomics.Quantification.Labelfree;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Deuterium
{
  public class DeuteriumCalculatorOptions : ChromatographProfileBuilderOptions
  {
    public string BoundaryOutputFile
    {
      get
      {
        return Path.Combine(GetDetailDirectory(), "boundary.tsv");
      }
    }

    public string DeuteriumOutputFile
    {
      get
      {
        return Path.Combine(GetDetailDirectory(), "calc.tsv");
      }
    }

    [Option("excludeIsotopic0", MetaValue = "BOOLEAN", HelpText = "Exclude isotopic 0 in formula")]
    public bool ExcludeIsotopic0 { get; set; }

    [Option("peptideInAllTimePointOnly", MetaValue = "BOOLEAN", HelpText = "Use peptide in all time point only")]
    public bool PeptideInAllTimePointOnly { get; set; }

    public Dictionary<string, double> ExperimentalTimeMap { get; set; }

    public DeuteriumCalculatorOptions()
    {
      this.MinimumIsotopicPercentage = 0.01;
      this.ExperimentalTimeMap = new Dictionary<string, double>();
    }

    public string GetDetailDirectory()
    {
      var result = Path.GetFullPath(Path.ChangeExtension(this.OutputFile, ".details"));
      if (!Directory.Exists(result))
      {
        Directory.CreateDirectory(result);
      }
      return result;
    }
  }
}
