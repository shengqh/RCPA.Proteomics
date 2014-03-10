using RCPA.Proteomics.Spectrum;
namespace RCPA.Proteomics.Processor
{
  public class PeakListTopXProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private int topX;

    public PeakListTopXProcessor(int topX)
    {
      this.topX = topX;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      t.KeepTopXInWindow(topX, 100);
      return t;
    }

    public override string ToString()
    {
      return "KeepTopX=" + topX.ToString();
    }

    #endregion
  }
}