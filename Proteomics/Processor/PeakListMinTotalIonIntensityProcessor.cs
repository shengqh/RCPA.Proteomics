using RCPA.Proteomics.Spectrum;
namespace RCPA.Proteomics.Processor
{
  public class PeakListMinTotalIonIntensityProcessor<T> : IProcessor<PeakList<T>> where T : IPeak
  {
    private readonly double minTotalIonIntensity;

    public PeakListMinTotalIonIntensityProcessor(double minTotalIonIntensity)
    {
      this.minTotalIonIntensity = minTotalIonIntensity;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      double totalIntensity = t.GetTotalIntensity();
      if (totalIntensity >= this.minTotalIonIntensity)
      {
        return t;
      }
      else
      {
        return null;
      }
    }

    #endregion
    public override string ToString()
    {
      return string.Format("MinTotalIonIntensity={0}", minTotalIonIntensity);
    }
  }
}