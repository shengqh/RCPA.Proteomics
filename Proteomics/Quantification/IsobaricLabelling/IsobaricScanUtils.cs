using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public static class IsobaricScanUtils
  {
    public static void MatchPeptideWithIsobaric(IsobaricResult itraq, List<IIdentifiedSpectrum> spectra)
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
    /// 从isobaricFile中读取spectra对应的isobaric labelling信息。
    /// </summary>
    /// <param name="spectra"></param>
    /// <param name="isobaricFile"></param>
    /// <param name="progress"></param>
    public static void Load(List<IIdentifiedSpectrum> spectra, string isobaricFile, bool readPeaks = false, IProgressCallback progress = null)
    {
      if (progress == null)
      {
        progress = new EmptyProgressCallback();
      }

      var fileNames = new HashSet<string>(from s in spectra
                                          let fs = s.Query.FileScan
                                          select fs.Experimental + "," + fs.FirstScan.ToString());

      using (var reader = IsobaricResultFileFormatFactory.GetXmlReader())
      {
        reader.ReadPeaks = readPeaks;
        reader.Progress = progress;

        var plexType = IsobaricScanXmlUtils.GetIsobaricType(isobaricFile);

        reader.Open(isobaricFile);

        progress.SetMessage("Reading Isobaric from {0} ...", isobaricFile);
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
            spectrum.SetIsobaricItem(reader.Read(fs.Experimental, fs.FirstScan, plexType));
          }
          else
          {
            spectrum.SetIsobaricItem(null);
          }
        }
      }
    }
  }
}
