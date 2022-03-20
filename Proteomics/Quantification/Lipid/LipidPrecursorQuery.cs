using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Lipid
{
  /// <summary>
  /// 根据给定的QueryItem，查询文件中对应的Precursor列表
  /// </summary>
  public class LipidPrecursorQuery : ProgressClass
  {
    private IRawFile reader;

    private Dictionary<int, PeakList<Peak>> pklMap = new Dictionary<int, PeakList<Peak>>();

    public LipidPrecursorQuery(IRawFile reader)
    {
      this.reader = reader;
    }

    private PeakList<Peak> ReadScan(int scan)
    {
      if (pklMap.ContainsKey(scan))
      {
        return pklMap[scan];
      }

      PeakList<Peak> pkl = reader.GetPeakList(scan);
      pkl.ScanTimes.Clear();
      pkl.ScanTimes.Add(new ScanTime(scan, reader.ScanToRetentionTime(scan)));
      pklMap[scan] = pkl;
      return pkl;
    }

    public List<PrecursorItem> QueryPrecursorFromProductIon(QueryItem productIon, double ppmProductTolerance, double ppmPrecursorTolerance)
    {
      List<PrecursorItem> result = new List<PrecursorItem>();

      int firstScan = reader.GetFirstSpectrumNumber();
      int lastScan = reader.GetLastSpectrumNumber();

      double mzTolerance = PrecursorUtils.ppm2mz(productIon.ProductIonMz, ppmProductTolerance);

      PeakList<Peak> lastFullScan = new PeakList<Peak>();

      Progress.SetRange(firstScan, lastScan);
      for (int scan = firstScan; scan <= lastScan; scan++)
      {
        Progress.SetPosition(scan);

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        //ignore ms 1
        if (reader.GetMsLevel(scan) == 1)
        {
          lastFullScan = ReadScan(scan);
          continue;
        }

        //read all peaks
        PeakList<Peak> pkl = ReadScan(scan);

        //find product ions
        PeakList<Peak> productIons = pkl.FindPeak(productIon.ProductIonMz, mzTolerance);

        if (productIons.Count == 0)
        {
          continue;
        }

        //get minimum product ion intensity
        double maxIntensity = pkl.FindMaxIntensityPeak().Intensity;
        double relativeIntensity = productIons.FindMaxIntensityPeak().Intensity / maxIntensity;
        if (relativeIntensity < productIon.MinRelativeIntensity)
        {
          continue;
        }

        Peak peak = reader.GetPrecursorPeak(scan);

        double precursorMzTolerance = PrecursorUtils.ppm2mz(peak.Mz, ppmPrecursorTolerance);

        var precursorPkl = lastFullScan.FindPeak(peak.Mz, precursorMzTolerance);

        bool isotopic = false;
        if (precursorPkl.Count == 0)
        {
          isotopic = true;
          precursorPkl = lastFullScan.FindPeak(peak.Mz - 1.0, precursorMzTolerance);
        }

        if (precursorPkl.Count == 0)
        {
          precursorPkl = lastFullScan.FindPeak(peak.Mz + 1.0, precursorMzTolerance);
        }

        var precursorInFullMs = precursorPkl.FindMaxIntensityPeak();

        var precursor = new PrecursorItem()
        {
          Scan = scan,
          ProductIonRelativeIntensity = relativeIntensity
        };

        if (isotopic && precursorInFullMs != null)
        {
          precursor.PrecursorMZ = precursorInFullMs.Mz;
          precursor.IsIsotopic = true;
        }
        else
        {
          precursor.PrecursorMZ = peak.Mz;
          precursor.IsIsotopic = false;
        }

        if (precursorInFullMs != null)
        {
          precursor.PrecursorIntensity = precursorInFullMs.Intensity;
        }

        result.Add(precursor);
      }

      var precursorMzs = from item in result
                         group item by item.PrecursorMZ into mzGroup
                         let mzcount = mzGroup.Where(m => m.PrecursorIntensity > 0).Count()
                         orderby mzcount descending
                         select mzGroup.Key;

      foreach (var mz in precursorMzs)
      {
        double mzPrecursorTolerance = PrecursorUtils.ppm2mz(mz, ppmPrecursorTolerance);
        foreach (var item in result)
        {
          if (!item.IsIsotopic)
          {
            continue;
          }

          if (Math.Abs(item.PrecursorMZ - mz) <= mzPrecursorTolerance)
          {
            item.PrecursorMZ = mz;
          }
        }
      }

      return result;
    }

    public List<LabelFreeItem> QueryChromotograph(double precursorMz, double ppmTolerance)
    {
      var result = new List<LabelFreeItem>();

      int firstScan = reader.GetFirstSpectrumNumber();
      int lastScan = reader.GetLastSpectrumNumber();

      double mzTolerance = PrecursorUtils.ppm2mz(precursorMz, ppmTolerance);

      Progress.SetRange(firstScan, lastScan);

      bool bAppend = false;
      for (int scan = firstScan; scan <= lastScan; scan++)
      {
        Progress.SetPosition(scan);

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        //ignore ms 2
        if (reader.GetMsLevel(scan) != 1)
        {
          continue;
        }

        //read all peaks
        PeakList<Peak> pkl = ReadScan(scan);

        //find product ions
        PeakList<Peak> precursorIons = pkl.FindPeak(precursorMz, mzTolerance);

        if (precursorIons.Count == 0)
        {
          if (bAppend)
          {
            result.Add(new LabelFreeItem()
            {
              Scan = scan,
              RetentionTime = pkl.ScanTimes[0].RetentionTime,
              Mz = precursorMz,
              Intensity = 0.0
            });
          }
        }
        else
        {
          bAppend = true;
          var peak = precursorIons.FindMaxIntensityPeak();

          result.Add(new LabelFreeItem()
          {
            Scan = scan,
            RetentionTime = pkl.ScanTimes[0].RetentionTime,
            Mz = peak.Mz,
            Intensity = peak.Intensity
          });
        }
      }

      while (result.Count > 0 && result[result.Count - 1].Intensity == 0)
      {
        result.RemoveAt(result.Count - 1);
      }

      return result;
    }
  }
}
