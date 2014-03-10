using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Analysis
{
  public class BuildSummaryResultParser : AbstractThreadFileProcessor
  {
    private IFalseDiscoveryRateCalculator calc;

    private string decoyPattern;

    public BuildSummaryResultParser(IFalseDiscoveryRateCalculator calc, string decoyPattern)
    {
      this.calc = calc;
      this.decoyPattern = decoyPattern;
    }

    public override IEnumerable<string> Process(string dir)
    {
      var noredundantFiles = Directory.GetFiles(dir, "*.noredundant").ToList();
      noredundantFiles.Sort();

      var result = dir + "\\summary.txt.xls";

      using (StreamWriter sw = new StreamWriter(result))
      {
        List<IdentificationSummary> summaries = new List<IdentificationSummary>();
        foreach (var file in noredundantFiles)
        {
          summaries.Add(IdentificationSummary.Parse(file, decoyPattern, calc));
        }

        bool hasSemi = summaries.Any(m => m.SemiSpectrumCount != 0);
        if (hasSemi)
        {
          sw.WriteLine("Dataset\tFull Spectrun Count\tFull Peptide Count\tSemi Spectrum Count\tSemi Peptide Count\tSpectrum Count\tPeptide Count\tProtein\tTwo-Hits\tTwo-Hits Percentage\tTarget Two-Hits\tTwo-Hits FDR\tOne-Hit Wonder\tTarget One-Hit Wonder\tOne-Hit Wonder FDR");
          foreach (var summary in summaries)
          {
            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9:0.00}%\t{10}\t{11:0.00}%\t{12}\t{13}\t{14:0.00}%",
              summary.FileName,
              summary.FullSpectrumCount,
              summary.FullPeptideCount,
              summary.SemiSpectrumCount,
              summary.SemiPeptideCount,
              summary.SpectrumCount,
              summary.UniquePeptideCount,
              summary.ProteinGroupCount,
              summary.Unique2ProteinGroupCount,
              summary.Unique2ProteinGroupPercentage,
              summary.Unique2ProteinGroupTargetCount,
              summary.Unique2ProteinFdr * 100,
              summary.ProteinGroupCount - summary.Unique2ProteinGroupCount,
              summary.Unique1ProteinGroupTargetCount,
              summary.Unique1ProteinFdr * 100);
          }
        }
        else
        {
          sw.WriteLine("Dataset\tPeptides\tUnique Peptides\tProteins\tTwo-hit proteins\tTwo-hit protein percentage\tTarget two-hit proteins\tTwo-hit protein FDR\tOne-hit-wonders\tTarget one-hit-wonders\tOne-hit-wonder FDR");
          foreach (var summary in summaries)
          {
            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5:0.00}%\t{6}\t{7:0.00}%\t{8}\t{9}\t{10:0.00}%",
              summary.FileName,
              summary.SpectrumCount,
              summary.UniquePeptideCount,
              summary.ProteinGroupCount,
              summary.Unique2ProteinGroupCount,
              summary.Unique2ProteinGroupPercentage,
              summary.Unique2ProteinGroupTargetCount,
              summary.Unique2ProteinFdr * 100,
              summary.ProteinGroupCount - summary.Unique2ProteinGroupCount,
              summary.Unique1ProteinGroupTargetCount,
              summary.Unique1ProteinFdr * 100);
          }
        }
      }

      return new string[] { result };
    }
  }
}
