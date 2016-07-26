using CommandLine;
using RCPA.Proteomics.Quantification.Labelfree;
using System.IO;

namespace RCPA.Proteomics.Deuterium
{
  public class DeuteriumCalculatorOptions : ChromatographProfileBuilderOptions
  {
    public string BoundaryOutputFile
    {
      get
      {
        return Path.Combine(GetDetailDirectory(), Path.ChangeExtension(Path.GetFileName(this.OutputFile), ".boundary.tsv"));
      }
    }

    public string DeuteriumOutputFile
    {
      get
      {
        return Path.Combine(GetDetailDirectory(), Path.ChangeExtension(Path.GetFileName(this.OutputFile), ".calc.tsv"));
      }
    }

    [Option("excludeIsotopic0", MetaValue = "BOOLEAN", HelpText = "Exclude isotopic 0 in formula")]
    public bool ExcludeIsotopic0 { get; set; }

    public DeuteriumCalculatorOptions()
    {
      this.MinimumIsotopicPercentage = 0.01;
    }

    private string GetDetailDirectory()
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
