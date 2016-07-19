using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public abstract class AbstractObservedProfileOptimizationFinder : IObservedProfileFinder
  {
    public bool Find(PeakList<Peak> ms1, ChromatographProfile chro, double mzTolerancePPM, int minimumProfileLength, ref ChromatographProfileScan result)
    {
      var rawPeaks = ms1.GetRange(chro.IsotopicIons[0].MinimumMzWithinTolerance, chro.IsotopicIons[chro.IsotopicIons.Length - 1].MaximumMzWithinTolerance);
      if(rawPeaks.Count < minimumProfileLength)
      {
        return false;
      }

      List<List<Peak>> envelope = new List<List<Peak>>();
      int peakIndex = 0;
      foreach (var peak in chro.IsotopicIons)
      {
        List<Peak> findPeaks = new List<Peak>();
        while (peakIndex < rawPeaks.Count)
        {
          var curPeak = rawPeaks[peakIndex];
          if (curPeak.Mz < peak.MinimumMzWithinTolerance)
          {
            peakIndex++;
            continue;
          }

          if (curPeak.Mz > peak.MaximumMzWithinTolerance)
          {
            break;
          }

          findPeaks.Add(curPeak);
          peakIndex++;
        }

        if (findPeaks.Count == 0)
        {
          break;
        }
        else
        {
          envelope.Add(findPeaks);
        }
      }

      if (envelope.Count < minimumProfileLength)
      {
        return false;
      }

      result = new ChromatographProfileScan();
      result.Scan = ms1.FirstScan;
      result.RetentionTime = ms1.ScanTimes[0].RetentionTime;
      result.RawPeaks = new List<Peak>();

      List<Peak> resolved = Resolve(chro, envelope);

      foreach (var findPeak in rawPeaks)
      {
        var isotopicIndex = resolved.IndexOf(findPeak);
        if (isotopicIndex == -1)
        {
          result.RawPeaks.Add(findPeak);
        }
        else {
          var ion = chro.IsotopicIons[isotopicIndex];
          result.Add(new ChromatographProfileScanPeak()
          {
            Isotopic = isotopicIndex + 1,
            Mz = findPeak.Mz,
            Intensity = findPeak.Intensity,
            Noise = findPeak.Noise,
            PPMDistance = PrecursorUtils.mz2ppm(findPeak.Mz, findPeak.Mz - ion.Mz)
          });
        }
      }

      return true;
    }

    protected abstract List<Peak> Resolve(ChromatographProfile chro, List<List<Peak>> envelope);
  }
}
