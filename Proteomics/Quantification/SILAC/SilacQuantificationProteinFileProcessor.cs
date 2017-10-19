using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RCPA;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using System.IO;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Isotopic;
using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Quantification.SILAC;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationProteinFileProcessor : AbstractThreadFileProcessor
  {
    private IIdentifiedResultTextFormat resultFormat;

    private SilacQuantificationMultipleFileBuilder builder;

    public double MinPeptideRegressionCorrelation { get; set; }

    private SilacQuantificationOption option;

    public SilacQuantificationProteinFileProcessor(SilacQuantificationOption option)
    {
      this.option = option;
      this.builder = new SilacQuantificationMultipleFileBuilder(option, null);
      this.resultFormat = new MascotResultTextFormat();
      this.MinPeptideRegressionCorrelation = 0.5;
    }

    private IProteinRatioCalculator calc = new SilacProteinRatioCalculator();

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

      SilacQuantificationSummaryOption summaryOption = new SilacQuantificationSummaryOption() { MinimumPeptideRSquare = this.MinPeptideRegressionCorrelation };

      spectra.ForEach(m =>
      {
        summaryOption.SetPeptideRatioValid(m, summaryOption.HasPeptideRatio(m) && !summaryOption.IsPeptideOutlier(m));
      });

      foreach (IIdentifiedProteinGroup mpg in sr)
      {
        calc.Calculate(mpg, m => true);
      }

      if (option.KeepPeptideWithMostScan)
      {
        foreach (var pgroup in sr)
        {
          var gspec = pgroup.GetPeptides().GroupBy(l => l.Query.FileScan.Experimental + "_" + l.Sequence + "_" + l.Query.Charge).ToList();
          HashSet<IIdentifiedSpectrum> kept = new HashSet<IIdentifiedSpectrum>();
          foreach (var spec in gspec)
          {
            if (spec.Count() == 1)
            {
              kept.Add(spec.First());
            }
            else
            {
              var maxScan = spec.Max(l => (l.Annotations["S_RATIO"] as QuantificationItem).ScanCount);
              var maxSpecs = spec.Where(l => (l.Annotations["S_RATIO"] as QuantificationItem).ScanCount == maxScan).ToList();
              foreach (var maxSpec in maxSpecs)
              {
                kept.Add(maxSpec);
              }
            }
          }

          foreach (var p in pgroup)
          {
            p.Peptides.RemoveAll(l => !kept.Contains(l.Spectrum));
          }
        }
      }

      resultFormat.InitializeByResult(sr);
      resultFormat.WriteToFile(resultFile, sr);

      return new[] { resultFile };
    }
  }
}
