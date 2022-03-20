using RCPA.Proteomics.Spectrum;
namespace RCPA.Proteomics.Processor
{
  public class PeakListMassRangeProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private readonly int[] defaultCharges;
    private readonly double maxMass;
    private readonly double minMass;

    public PeakListMassRangeProcessor(double minMass, double maxMass, int[] defaultCharges)
    {
      this.minMass = minMass;
      this.maxMass = maxMass;
      this.defaultCharges = defaultCharges;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      int[] checkCharges;
      if (0 == t.PrecursorCharge)
      {
        if (0 == this.defaultCharges.Length)
        {
          return t;
        }

        checkCharges = this.defaultCharges;
      }
      else
      {
        checkCharges = new[] { t.PrecursorCharge };
      }

      foreach (int charge in checkCharges)
      {
        double dMass = t.PrecursorMZ * charge;
        if (dMass >= this.minMass && dMass <= this.maxMass)
        {
          return t;
        }
      }
      return null;
    }

    public override string ToString()
    {
      return string.Format("PrecursorMassRange={0:0.####}-{1:0.####}", minMass, maxMass);
    }

    #endregion
  }
}