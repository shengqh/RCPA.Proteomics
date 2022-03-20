using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Processor
{
  public class PeakListRemovePrecursorProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private PeakListRemovePrecursorProcessorOptions options;
    private double[] offsets;

    public PeakListRemovePrecursorProcessor(PeakListRemovePrecursorProcessorOptions options)
    {
      this.options = options;
      this.offsets = options.ParseOffsets();
    }

    #region IProcessor<PeakList<T>> Members

    /// <summary>
    /// Find peak with exact charge or no charge information available
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="mz"></param>
    /// <param name="charge"></param>
    /// <param name="mzTolerance"></param>
    /// <returns></returns>
    private List<T> FindPeakConsideringCharge(PeakList<T> lst, double mz, int charge, double mzTolerance)
    {
      var result = new List<T>();

      double minMz = mz - mzTolerance;
      double maxMz = mz + mzTolerance;

      foreach (var p in lst)
      {
        if (p.Mz < minMz)
        {
          continue;
        }

        if (p.Mz > maxMz)
        {
          break;
        }

        if (p.Charge == charge || p.Charge == 0)
        {
          result.Add(p);
        }
      }

      return result;
    }

    public PeakList<T> Process(PeakList<T> t)
    {
      if (t.PrecursorCharge > 0)
      {
        var isobaricList = new List<T>();
        var mzPrecursor = PrecursorUtils.ppm2mz(t.PrecursorMZ, options.PPMTolerance);
        var precursurIons = FindPeakConsideringCharge(t, t.PrecursorMZ, t.PrecursorCharge, mzPrecursor);

        //First of all, we need to find precursor
        if (precursurIons.Count > 0)
        {
          precursurIons.ForEach(m => { t.Remove(m); });

          isobaricList.Add(new T()
          {
            Mz = t.PrecursorMZ,
            Charge = t.PrecursorCharge,
            Intensity = mzPrecursor
          });

          if (options.RemoveChargeMinus1Precursor && t.PrecursorCharge > 1)
          {
            //Get charge-1 precursor
            var p1mz = PrecursorUtils.MHToMz(PrecursorUtils.MzToMH(t.PrecursorMZ, t.PrecursorCharge, true), t.PrecursorCharge - 1, true);
            var p1Window = PrecursorUtils.ppm2mz(p1mz, options.PPMTolerance);
            var p1Ions = FindPeakConsideringCharge(t, p1mz, t.PrecursorCharge - 1, p1Window);
            if (p1Ions.Count > 0)
            {
              p1Ions.ForEach(m => { t.Remove(m); });

              isobaricList.Add(new T()
              {
                Mz = p1mz,
                Charge = t.PrecursorCharge - 1,
                Intensity = p1Window
              });
            }
          }

          if (options.RemoveIsotopicIons)
          {
            foreach (var ion in isobaricList)
            {
              RemoveIsotopicIons(t, ion);
            }
          }
        }

        foreach (var offset in offsets)
        {
          var mz = t.PrecursorMZ + offset / t.PrecursorCharge;
          var mzWindow = PrecursorUtils.ppm2mz(mz, options.PPMTolerance);

          var minMz = mz - mzWindow;
          var maxMz = mz + mzWindow;

          t.RemoveAll(m =>
          {
            if (m.Charge == t.PrecursorCharge || m.Charge == 0)
            {
              return m.Mz >= minMz && m.Mz <= maxMz;
            }
            else
            {
              return false;
            }
          });
        }

        if (options.RemoveIonLargerThanPrecursor)
        {
          var pmass = PrecursorUtils.MzToMH(t.PrecursorMZ, t.PrecursorCharge, true);
          t.RemoveAll(m =>
          {
            var mass = m.Charge > 0 ? PrecursorUtils.MzToMH(m.Mz, m.Charge, true) : m.Mz;
            return mass >= pmass;
          });
        }
      }

      return t;
    }

    private void RemoveIsotopicIons(PeakList<T> t, T ion)
    {
      var is1 = ion.Mz + Atom.C13_GAP / ion.Charge;
      var is1Ions = FindPeakConsideringCharge(t, is1, ion.Charge, ion.Intensity);
      if (is1Ions.Count > 0)
      {
        is1Ions.ForEach(m => t.Remove(m));

        var is2 = ion.Mz + Atom.C13_GAP * 2 / ion.Charge;
        var is2Ions = FindPeakConsideringCharge(t, is2, ion.Charge, ion.Intensity);

        is2Ions.ForEach(m => t.Remove(m));
      }
    }

    #endregion
    public override string ToString()
    {
      return options.ToString();
    }
  }
}