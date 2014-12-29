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

    public SilacQuantificationProteinFileProcessor(IRawFormat rawFormat, string rawDir, string silacParamFile, double ppmTolerance, string ignoreModifications, int profileLength)
    {
      this.builder = new SilacQuantificationMultipleFileBuilder(rawFormat, rawDir, silacParamFile, ppmTolerance, ignoreModifications, profileLength, null);
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

      SilacQuantificationSummaryOption option = new SilacQuantificationSummaryOption() { MinimumRSquare = this.MinPeptideRegressionCorrelation };

      spectra.ForEach(m =>
      {
        option.SetPeptideRatioValid(m, option.HasPeptideRatio(m) && !option.IsPeptideOutlier(m));
      });

      foreach (IIdentifiedProteinGroup mpg in sr)
      {
        calc.Calculate(mpg, m => true);
      }

      resultFormat.InitializeByResult(sr);
      resultFormat.WriteToFile(resultFile, sr);

      return new[] { resultFile };
    }
  }
}
