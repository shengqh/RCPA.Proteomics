using RCPA.Proteomics.Spectrum;
namespace RCPA.Proteomics.Processor
{
  public class PeakListMinIonIntensityProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private readonly double minIonIntensity;

    public PeakListMinIonIntensityProcessor(double minIonIntensity)
    {
      this.minIonIntensity = minIonIntensity;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      for (int i = t.Count - 1; i >= 0; i--)
      {
        if (t[i].Intensity < this.minIonIntensity)
        {
          t.RemoveAt(i);
        }
      }

      if (0 == t.Count)
      {
        return null;
      }
      else
      {
        return t;
      }
    }

    #endregion
    public override string ToString()
    {
      return string.Format("MinIonIntensity={0}", minIonIntensity);
    }
  }
}