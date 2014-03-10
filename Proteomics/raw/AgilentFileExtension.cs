using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agilent.MassSpectrometry.DataAnalysis;
using RCPA.Proteomics.Spectrum;
using RCPA.Gui;
using RCPA.Utils;

namespace RCPA.Proteomics.Raw
{
  public static class AgilentFileExtension
  {
    public static IBDAChromData GetChromData(this IMsdrDataReader reader)
    {
      IBDAChromFilter chroFilter = new BDAChromFilter();
      chroFilter.ChromatogramType = ChromType.TotalIon;
      chroFilter.DoCycleSum = false;
      chroFilter.DesiredMSStorageType = DesiredMSStorageType.PeakElseProfile;

      IBDAChromData[] chro = reader.GetChromatogram(chroFilter);

      return chro[0];
    }

    public static double[] GetRetentionTimes(this IMsdrDataReader reader)
    {
      return GetChromData(reader).XArray;
    }

    public static int GetMsLevel(this IMsdrDataReader reader, double retentionTime)
    {
      switch (reader.GetMSScanInformation(retentionTime).MSLevel)
      {
        case MSLevel.MS: return 1;
        case MSLevel.MSMS: return 2;
        default: return 0;
      }
    }

    public static List<PeakList<Peak>> GetAllPeakList(this IMsdrDataReader reader, double[] retentionTimes, IProgressCallback progress)
    {
      List<PeakList<Peak>> result = new List<PeakList<Peak>>(retentionTimes.Length);

      progress.SetRange(1, retentionTimes.Length);
      for (int i = 0; i < retentionTimes.Length; i++)
      {
        progress.SetPosition(i);
        result.Add(GetPeakList(reader, retentionTimes[i], 1));
      }

      return result;
    }

    public static PeakList<Peak> GetPeakList(this IMsdrDataReader reader, double retentionTime)
    {
      return GetPeakList(reader, retentionTime, 1);
    }

    public static PeakList<Peak> GetPeakList(this IMsdrDataReader reader, double retentionTime, double minPeakIntensity)
    {
      return GetPeakList(reader, retentionTime, minPeakIntensity, 0);
    }

    public static PeakList<Peak> GetPeakList(this IMsdrDataReader reader, double retentionTime, double minPeakIntensity, int maxNumPeaks)
    {
      IMsdrPeakFilter peakFilter = new MsdrPeakFilter();
      peakFilter.AbsoluteThreshold = minPeakIntensity;
      peakFilter.RelativeThreshold = 0;
      peakFilter.MaxNumPeaks = maxNumPeaks;

      return GetPeakList(reader, retentionTime, peakFilter);
    }

    public static PeakList<Peak> GetPeakList(this IMsdrDataReader reader, double retentionTime, IMsdrPeakFilter peakFilter)
    {
      PeakList<Peak> result = new PeakList<Peak>();

      IBDASpecData s = reader.GetSpectrum(retentionTime, MSScanType.All, IonPolarity.Mixed, IonizationMode.Unspecified, peakFilter, true);
      for (int i = 0; i < s.XArray.Length; i++)
      {
        result.Add(new Peak(s.XArray[i], s.YArray[i]));
      }

      if (s.MZOfInterest.Length > 0)
      {
        result.PrecursorMZ = s.MZOfInterest[0].Start;
      }

      result.ScanTimes.Add(new ScanTime(0, retentionTime));

      return result;
    }

    //public static List<PeakList<Peak>> GetAllSpectrum(this IMsdrDataReader reader, IBDASpecFilter filter)
    //{
    //  List<PeakList<Peak>> arrays = new List<PeakList<Peak>>();

    //  IBDASpecData[] pkls = reader.GetSpectrum(filter);
    //  foreach (var s in pkls)
    //  {
    //    var result = new PeakList<Peak>();

    //    for (int i = 0; i < s.XArray.Length; i++)
    //    {
    //      result.Add(new Peak(s.XArray[i], s.YArray[i]));
    //    }

    //    if (s.MZOfInterest.Length > 0)
    //    {
    //      result.PrecursorMZ = s.MZOfInterest[0].Start;
    //    }

    //    //result.ScanTimes.Add(new ScanTime(0, s.x));

    //    arrays.Add(result);
    //  }

    //  return arrays;
    //}
  }
}
