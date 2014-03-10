using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification.ITraq
{
  //public delegate double TraqItemGetDelegate(ITraqItem item);

  public static class ITraqItemUtils
  {
    public static void MatchPeptideWithItraq(IsobaricResult itraq, List<IIdentifiedSpectrum> spectra)
    {
      var itraqMap = itraq.ToDictionary(m => m.Experimental + "##" + m.Scan.Scan.ToString());
      spectra.ForEach(m =>
      {
        var key = m.Query.FileScan.Experimental + "##" + m.Query.FileScan.FirstScan.ToString();
        if (itraqMap.ContainsKey(key))
        {
          m.SetIsobaricItem(itraqMap[key]);
        }
        else
        {
          m.SetIsobaricItem(null);
        }
      });
    }

    /// <summary>
    /// 从itraqFile中读取spectra对应的iTRAQ信息。
    /// </summary>
    /// <param name="spectra"></param>
    /// <param name="itraqFile"></param>
    /// <param name="progress"></param>
    public static void LoadITraq(List<IIdentifiedSpectrum> spectra, string itraqFile, bool readPeaks = false, IProgressCallback progress = null)
    {
      if (progress == null)
      {
        progress = new EmptyProgressCallback();
      }

      var fileNames = new HashSet<string>(from s in spectra
                                          let fs = s.Query.FileScan
                                          select fs.Experimental + "," + fs.FirstScan.ToString());

      var reader = ITraqResultFileFormatFactory.GetXmlReader();
      reader.ReadPeaks = readPeaks;
      reader.Progress = progress;

      reader.Open(itraqFile);

      progress.SetMessage("Reading iTRAQ from {0} ...", itraqFile);
      progress.SetRange(1, spectra.Count);

      foreach (var spectrum in spectra)
      {
        if (progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        progress.Increment(1);

        var fs = spectrum.Query.FileScan;
        if (reader.Has(fs.Experimental, fs.FirstScan))
        {
          spectrum.SetIsobaricItem(reader.Read(fs.Experimental, fs.FirstScan));
        }
        else
        {
          spectrum.SetIsobaricItem(null);
        }
      }
    }
  }
}
