using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public class SequestOptimalXcorrCalculator : OptimalResultCalculator
  {
    public SequestOptimalXcorrCalculator()
      : base(new SequestXcorrFunctions())
    { }
  }
}