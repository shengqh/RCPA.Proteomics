using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using RCPA;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Isotopic;
using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;
using RCPA.Gui;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationMultipleFileBuilder : ProgressClass
  {
    private IRawFormat rawFormat;

    private string _rawDir;

    private double _ppmTolerance;

    private string _silacParamFile;

    private string _ignoreModifications;

    private int _profileLength;

    public string SoftwareVersion { get; set; }

    private Dictionary<string, List<string>> _rawpairs;

    public SilacQuantificationMultipleFileBuilder(IRawFormat rawFormat, string rawDir, string silacParamFile, double ppmTolerance, string ignoreModifications, int profileLength, Dictionary<string, List<string>> rawpairs)
    {
      this.rawFormat = rawFormat;
      this._rawDir = rawDir;
      this._silacParamFile = silacParamFile;
      this._ppmTolerance = ppmTolerance;
      this._ignoreModifications = ignoreModifications;
      this._profileLength = profileLength;
      this._rawpairs = rawpairs;
    }

    public void Quantify(List<IIdentifiedSpectrum> spectra, string detailDir)
    {
      if (!Directory.Exists(detailDir))
      {
        Directory.CreateDirectory(detailDir);
      }

      spectra.ForEach(m => m.SetExtendedIdentification(false));

      DoQuantify(detailDir, spectra);

      if (_rawpairs != null)
      {
        var querys = DuplicateSpectrum(spectra, detailDir);

        DoQuantify(detailDir, querys);

        //删除那些扩展定量但是没有结果的spectrum。
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

        string rawFilename = rawFormat.GetRawFile(_rawDir, experimental);

        Progress.SetMessage(MyConvert.Format("{0}/{1} : Processing {2} ...", fileCount, filePepMap.Keys.Count, rawFilename));

        SilacQuantificationFileBuilder builder = new SilacQuantificationFileBuilder(rawFormat.GetRawFile(), _silacParamFile, _ppmTolerance, _ignoreModifications, _profileLength);
        builder.Progress = this.Progress;
        builder.SoftwareVersion = this.SoftwareVersion;

        List<IIdentifiedSpectrum> peps = filePepMap[experimental];
        peps.Sort((m1, m2) => m1.IsExtendedIdentification().CompareTo(m2.IsExtendedIdentification()));

        builder.Quantify(rawFilename, peps, detailDir);
      }
    }

    private List<IIdentifiedSpectrum> DuplicateSpectrum(List<IIdentifiedSpectrum> spectra, string detailDir)
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();

      Dictionary<string, List<string>> rawmap = new Dictionary<string, List<string>>();
      foreach (var raws in _rawpairs.Values)
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
