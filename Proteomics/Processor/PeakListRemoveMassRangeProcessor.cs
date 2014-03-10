using RCPA.Proteomics.Spectrum;
namespace RCPA.Proteomics.Processor
{
  public class PeakListRemoveMassRangeProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private double maxMass;
    private double minMass;

    public PeakListRemoveMassRangeProcessor(double minMass, double maxMass)
    {
      this.minMass = minMass;
      this.maxMass = maxMass;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      t.RemoveAll(m => m.Mz >= minMass && m.Mz <= maxMass);
      return t;
    }

    public override string ToString()
    {
      return string.Format("RemoveMassRange={0:0.####}-{1:0.####}", minMass, maxMass);
    }

    #endregion
  }
}