using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public class MascotOptimalScoreCalculator : OptimalResultCalculator
  {
    public MascotOptimalScoreCalculator()
      : base(new MascotScoreFunctions())
    { }
  }
}