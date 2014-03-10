using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PFind
{
  public class PFindOptimalExpectValueCalculator : OptimalResultCalculator
  {
    public PFindOptimalExpectValueCalculator()
      : base(new PFindExpectValueFunctions())
    { }
  }
}