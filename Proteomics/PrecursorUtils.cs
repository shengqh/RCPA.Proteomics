using RCPA.Proteomics.Spectrum;
namespace RCPA.Proteomics
{
  public class PrecursorUtils
  {
    public static double ppm2mz(double precursorMz, double ppmTolerance)
    {
      return precursorMz * ppmTolerance / 1000000;
    }

    public static double mz2ppm(double precursorMz, double mzTolerance)
    {
      return mzTolerance * 1000000 / precursorMz;
    }

    public static double MHToMz(double mass, int charge, bool isMonoisotopic)
    {
      double massH = isMonoisotopic ? Atom.H.MonoMass : Atom.H.AverageMass;
      return (mass + massH * (charge - 1)) / charge;
    }

    public static double MzToMH(double mz, int charge, bool isMonoisotopic)
    {
      if (charge == 0)
      {
        return mz;
      }

      double massH = isMonoisotopic ? Atom.H.MonoMass : Atom.H.AverageMass;
      return mz * charge - massH * (charge - 1);
    }

    public static int GuessPrecursorCharge<T>(PeakList<T> pkl, double precursorMz) where T : IPeak
    {
      double fSumTotal = 0.0;
      double fSumBelow = 0.0;
      foreach (T peak in pkl)
      {
        if (peak.Mz < precursorMz)
        {
          fSumBelow += peak.Intensity;
        }

        fSumTotal += peak.Intensity;
      }

      /*
       * If greater than 95% signal is below precursor, then
       * it looks like singly charged peptide.
       */
      if (fSumTotal == 0.0 || fSumBelow / fSumTotal > 0.95)
      {
        return 1;
      }

      return 0;
    }
  }
}