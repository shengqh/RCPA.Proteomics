using RCPA.Gui;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationMultipleFileBuilder : ProgressClass
  {
    private SilacQuantificationOption option;

    public string SoftwareVersion { get; set; }

    private Dictionary<string, List<string>> rawpairs;

    /// <summary>
    /// At least MinScanNumber scans will be extract near the scan identified, even all zero intensity.
    /// </summary>
    public int MinScanNumber { get; set; }

    public SilacQuantificationMultipleFileBuilder(SilacQuantificationOption option, Dictionary<string, List<string>> rawpairs)
    {
      this.option = option;
      this.rawpairs = rawpairs;
      this.MinScanNumber = 3;
    }

    public void Quantify(List<IIdentifiedSpectrum> spectra, string detailDir)
    {
      if (!Directory.Exists(detailDir))
      {
        Directory.CreateDirectory(detailDir);
      }

      spectra.ForEach(m => m.SetExtendedIdentification(false));

      DoQuantify(detailDir, spectra);

      if (rawpairs != null)
      {
        var querys = DuplicateSpectrum(spectra, detailDir);

        DoQuantify(detailDir, querys);

        spectra.ForEach(n => n.GetDuplicatedSpectra().RemoveAll(m => !m.HasRatio()));
      }
    }

    private void DoQuantify(string detailDir, List<IIdentifiedSpectrum> querys)
    {
      Dictionary<string, List<IIdentifiedSpectrum>> filePepMap = IdentifiedSpectrumUtils.GetRawPeptideMap(querys);

      int fileCount = 0;
      foreach (string experimental in filePepMap.Keys)
      {
        fileCount++;

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        string rawFilename = option.RawFormat.GetRawFile(option.RawDir, experimental);

        Progress.SetMessage(MyConvert.Format("{0}/{1} : Processing {2} ...", fileCount, filePepMap.Keys.Count, rawFilename));

        SilacQuantificationFileBuilder builder = new SilacQuantificationFileBuilder(option);
        builder.Progress = this.Progress;
        builder.SoftwareVersion = this.SoftwareVersion;
        builder.MinScanNumber = this.MinScanNumber;

        List<IIdentifiedSpectrum> peps = filePepMap[experimental];
        peps.Sort((m1, m2) => m1.IsExtendedIdentification().CompareTo(m2.IsExtendedIdentification()));

        builder.Quantify(rawFilename, peps, detailDir);
      }
    }

    private List<IIdentifiedSpectrum> DuplicateSpectrum(List<IIdentifiedSpectrum> spectra, string detailDir)
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();

      Dictionary<string, List<string>> rawmap = new Dictionary<string, List<string>>();
      foreach (var raws in rawpairs.Values)
      {
        foreach (var raw in raws)
        {
          rawmap[raw] = raws;
        }
      }

      var format = new MascotPeptideTextFormat();
      foreach (var spectrum in spectra)
      {
        if (spectrum.HasRatio())
        {
          var silacFile = spectrum.GetRatioFile(detailDir);
          var silacResult = new SilacQuantificationSummaryItemXmlFormat().ReadFromFile(silacFile);
          var maxIntensity = silacResult.ObservedEnvelopes.Max(m => Math.Max(m.LightIntensity, m.HeavyIntensity));
          var scan = silacResult.ObservedEnvelopes.Find(m => m.LightIntensity == maxIntensity || m.HeavyIntensity == maxIntensity).Scan;

          var str = format.PeptideFormat.GetString(spectrum);
          var oldraw = spectrum.Query.FileScan.Experimental;
          var lst = rawmap[oldraw];
          foreach (var otherraw in lst)
          {
            if (otherraw.Equals(oldraw))
            {
              continue;
            }

            var newspectrum = format.PeptideFormat.ParseString(str);
            newspectrum.Query.FileScan.Experimental = otherraw;
            newspectrum.Query.FileScan.FirstScan = scan;
            newspectrum.Query.FileScan.LastScan = scan;
            newspectrum.SetExtendedIdentification(true);

            result.Add(newspectrum);
            spectrum.AddDuplicatedSpectrum(newspectrum);
          }
        }
      }

      return result;
    }
  }
}
