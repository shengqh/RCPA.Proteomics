using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationPeptideFileProcessor : AbstractThreadFileProcessor
  {
    private IIdentifiedPeptideTextFormat resultFormat;

    private SilacQuantificationMultipleFileBuilder builder;

    public double MinPeptideRegressionCorrelation { get; set; }

    private SilacQuantificationOption option;

    public SilacQuantificationPeptideFileProcessor(SilacQuantificationOption option)
    {
      this.option = option;
      this.builder = new SilacQuantificationMultipleFileBuilder(option, null);
      this.resultFormat = new MascotPeptideTextFormat();
      this.MinPeptideRegressionCorrelation = 0.5;
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

      var spectra = resultFormat.ReadFromFile(filename);

      builder.Quantify(spectra, detailDir.FullName);

      SilacQuantificationSummaryOption summaryOption = new SilacQuantificationSummaryOption() { MinimumPeptideRSquare = this.MinPeptideRegressionCorrelation };

      spectra.ForEach(m =>
      {
        summaryOption.SetPeptideRatioValid(m, summaryOption.HasPeptideRatio(m) && !summaryOption.IsPeptideOutlier(m));
      });

      resultFormat.Initialize(spectra);
      resultFormat.WriteToFile(resultFile, spectra);

      return new[] { resultFile };
    }
  }
}
