using RCPA.Proteomics.Spectrum;
namespace RCPA.Proteomics.Processor
{
  public class PeakListMinIonCountProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private readonly int minIonCount;

    public PeakListMinIonCountProcessor(int minIonCount)
    {
      this.minIonCount = minIonCount;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      if (t.Count >= this.minIonCount)
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
      return string.Format("MinIonCount={0}", minIonCount);
    }
  }
}