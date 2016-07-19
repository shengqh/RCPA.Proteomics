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
        return Path.ChangeExtension(this.OutputFile, ".boundary.tsv");
      }
    }

    public string DeuteriumOutputFile
    {
      get
      {
        return Path.ChangeExtension(base.OutputFile, ".calc.tsv");
      }
    }

    public DeuteriumCalculatorOptions()
    {
      this.MinimumIsotopicPercentage = 0.01;
    }
  }
}
