using RCPA.Proteomics.Spectrum;
namespace RCPA.Proteomics.Processor
{
  public class PeakListProductIonShiftProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private double shiftPPM;

    public PeakListProductIonShiftProcessor(double shiftPPM)
    {
      this.shiftPPM = shiftPPM;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      foreach (var peak in t)
      {
        var shiftMz = PrecursorUtils.ppm2mz(peak.Mz, shiftPPM);
        peak.Mz += shiftMz;
      }
      return t;
    }
    public override string ToString()
    {
      return string.Format("ProductShift={0:0.####}ppm", shiftPPM);
    }

    #endregion
  }
}