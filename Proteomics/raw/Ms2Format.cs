using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Raw
{
  public class Ms2Format<T> : IFileWriter<List<PeakList<T>>> where T : Peak
  {
    public void WriteToFile(string fileName, List<PeakList<T>> pklList)
    {
      using (var sw = new StreamWriter(fileName))
      {
        sw.WriteLine("H\tCreationDate\t{0:yyyyMMdd}", DateTime.Now);
        sw.WriteLine("H\tExtractor\tProteomicsTools");

        foreach (var pkl in pklList)
        {
          int[] charges;
          if (0 != pkl.PrecursorCharge)
          {
            charges = new[] { pkl.PrecursorCharge };
          }
          else if (IsWiff(pkl))
          {
            charges = new[] { 1 };
          }
          else
          {
            charges = new[] { 2, 3 };
          }

          sw.WriteLine("S\t{0}\t{1}\t{2}", pkl.ScanTimes.First().Scan, pkl.ScanTimes.Last().Scan, pkl.PrecursorMZ);
          foreach (var charge in charges)
          {
            sw.WriteLine("Z\t{0}\t{1:0.#####}", charge, PrecursorUtils.MzToMH(pkl.PrecursorMZ, charge, true));
          }
          foreach (var peak in pkl)
          {
            sw.WriteLine("{0:0.#####}\t{1:0.#}", peak.Mz, peak.Intensity);
          }
          sw.WriteLine();
        }
      }
    }

    private bool IsWiff(PeakList<T> pkl)
    {
      if (pkl.Annotations.ContainsKey(MascotGenericFormatConstants.TITLE_TAG))
      {
        var title = (String)pkl.Annotations[MascotGenericFormatConstants.TITLE_TAG];
        return title.ToLower().Contains(".wiff");
      }
      return false;
    }
  }
}
