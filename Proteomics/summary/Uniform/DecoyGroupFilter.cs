using System.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class DecoyGroupFilter : IIdentifiedProteinGroupFilter
  {
    private double minTargetDecoyRatio;
    public DecoyGroupFilter(double minTargetDecoyRatio)
    {
      this.minTargetDecoyRatio = minTargetDecoyRatio;
    }

    public bool Accept(IIdentifiedProteinGroup t)
    {
      var decoy = (from p in t[0].Peptides
                   where p.Spectrum.FromDecoy
                   select p.Spectrum.Query.FileScan.LongFileName).Distinct().Count();
      var target = (from p in t[0].Peptides
                    where !p.Spectrum.FromDecoy
                    select p.Spectrum.Query.FileScan.LongFileName).Distinct().Count();
      if (decoy == 0)
      {
        return false;
      }

      var tdratio = target * 1.0 / decoy;
      return tdratio < this.minTargetDecoyRatio;
    }

    public string FilterCondition
    {
      get { return "MinTargetDecoyRatio < " + minTargetDecoyRatio.ToString(); }
    }
  }
}
