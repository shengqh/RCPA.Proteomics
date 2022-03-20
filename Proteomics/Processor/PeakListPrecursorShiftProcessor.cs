using RCPA.Proteomics.Format.Offset;
using RCPA.Proteomics.Spectrum;
namespace RCPA.Proteomics.Processor
{
  public class PeakListShiftProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private IOffset offset;

    public PeakListShiftProcessor(IOffset offset)
    {
      this.offset = offset;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      var precursorPPM = offset.GetPrecursorOffset(t.Experimental, t.ScanTimes[0].Scan);
      if (precursorPPM != 0.0)
      {
        var shiftMz = PrecursorUtils.ppm2mz(t.PrecursorMZ, precursorPPM);
        t.PrecursorMZ = t.PrecursorMZ + shiftMz;
        t.PrecursorOffsetPPM = precursorPPM;
      }

      var productIonPPM = offset.GetProductIonOffset(t.Experimental, t.ScanTimes[0].Scan);
      if (productIonPPM != 0.0)
      {
        foreach (var peak in t)
        {
          var shiftMz = PrecursorUtils.ppm2mz(peak.Mz, productIonPPM);
          peak.Mz += shiftMz;
        }
        t.ProductOffsetPPM = productIonPPM;
      }
      return t;
    }

    #endregion

    public override string ToString()
    {
      return "ShiftOffset=" + offset.ToString();
    }
  }

  public class PeakListPrecursorShiftPPMProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    public double ShiftPPM { get; set; }

    public PeakListPrecursorShiftPPMProcessor(double shiftPPM)
    {
      this.ShiftPPM = shiftPPM;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      var shiftMz = PrecursorUtils.ppm2mz(t.PrecursorMZ, ShiftPPM);
      t.PrecursorMZ = t.PrecursorMZ + shiftMz;
      return t;
    }

    #endregion
    public override string ToString()
    {
      return string.Format("PrecursorShift={0:0.####}ppm", ShiftPPM);
    }
  }

  public class PeakListPrecursorShiftDaltonProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    public double ShiftDalton { get; set; }

    public PeakListPrecursorShiftDaltonProcessor(double shiftDalton)
    {
      this.ShiftDalton = shiftDalton;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      t.PrecursorMZ = t.PrecursorMZ + ShiftDalton / t.PrecursorCharge;
      t.Experimental = t.Experimental + "_SHIFTED";
      return t;
    }

    #endregion
    public override string ToString()
    {
      return string.Format("PrecursorShift={0:0.####}dalton", ShiftDalton);
    }
  }
}