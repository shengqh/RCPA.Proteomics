using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ExtendSilacQuantificationProteinFileProcessor : AbstractThreadFileProcessor
  {
    private IIdentifiedResultTextFormat resultFormat;

    private SilacQuantificationMultipleFileBuilder builder;

    public double MinPeptideRegressionCorrelation { get; set; }

    private Dictionary<string, List<string>> datasets;

    private SilacQuantificationOption option;

    public ExtendSilacQuantificationProteinFileProcessor(SilacQuantificationOption option, IIdentifiedResultTextFormat resultFormat, Dictionary<string, List<string>> datasets, Dictionary<string, List<string>> rawpairs)
    {
      this.option = option;
      this.resultFormat = resultFormat;
      this.builder = new SilacQuantificationMultipleFileBuilder(option, rawpairs);
      this.datasets = datasets;
    }

    public override IEnumerable<string> Process(string filename)
    {
      string resultFile = filename + ".SILACsummary";
      FileInfo resultFI = new FileInfo(resultFile);
      DirectoryInfo detailDir = new DirectoryInfo(resultFI.DirectoryName + "\\" + resultFI.Name + ".details");
      if (!detailDir.Exists)
      {
        detailDir.Create();
      }

      builder.Progress = this.Progress;

      IIdentifiedResult sr = resultFormat.ReadFromFile(filename);

      var spectra = sr.GetSpectra();

      builder.Quantify(spectra, detailDir.FullName);

      SilacQuantificationSummaryOption option = new SilacQuantificationSummaryOption() { MinimumPeptideRSquare = this.MinPeptideRegressionCorrelation };

      spectra.ForEach(m =>
      {
        m.SetEnabled(option.IsPeptideRatioValid(m) && !option.IsPeptideOutlier(m));

        var dupSpectra = m.GetDuplicatedSpectra();

        dupSpectra.ForEach(n => n.SetEnabled(option.IsPeptideRatioValid(n) && !option.IsPeptideOutlier(n)));
      });

      var calc = new ExtendSilacProteinRatioCalculator();

      Dictionary<string, string> datasetMap = new Dictionary<string, string>();
      foreach (var ds in datasets)
      {
        var lst = ds.Value;
        lst.ForEach(m => datasetMap[m] = ds.Key);
      }

      foreach (IIdentifiedProteinGroup mpg in sr)
      {
        var realSpectra = new List<IIdentifiedSpectrum>(mpg.GetPeptides());
        foreach (var spectrum in realSpectra)
        {
          var dupSpectra = spectrum.GetDuplicatedSpectra();
          mpg.AddIdentifiedSpectra(dupSpectra);
        }

        ProteinQuantificationResult pr = new ProteinQuantificationResult();
        foreach (var ds in datasets)
        {
          var lst = ds.Value;

          pr.Items[ds.Key] = calc.Calculate(mpg, m => lst.Contains(m.Query.FileScan.Experimental));
        }

        bool valid = pr.Items.Values.FirstOrDefault(m => m.Enabled) != null;
        foreach (IIdentifiedProtein protein in mpg)
        {
          protein.SetEnabled(valid);
          protein.Annotations[SilacQuantificationConstants.SILAC_KEY] = pr;
        }
      }

      var finalSpectra = sr.GetSpectra();
      finalSpectra.ForEach(m => m.SetDataset(datasetMap[m.Query.FileScan.Experimental]));

      resultFormat.InitializeByResult(sr);
      resultFormat.WriteToFile(resultFile, sr);

      return new[] { resultFile };
    }
  }
}
