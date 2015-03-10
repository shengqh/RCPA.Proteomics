using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Statistic
{
  public class BuildSummaryResultParser : AbstractThreadProcessor
  {
    private BuildSummaryResultParserOptions options;

    public BuildSummaryResultParser(BuildSummaryResultParserOptions options)
    {
      this.options = options;
    }

    public BuildSummaryResultParser(IFalseDiscoveryRateCalculator calc, string decoyPattern)
    {
      this.options = new BuildSummaryResultParserOptions()
      {
        Calculator = calc,
        DecoyPattern = decoyPattern
      };
    }

    public IEnumerable<string> Process(string dir)
    {
      this.options.InputDirectory = dir;
      return Process();
    }

    public override IEnumerable<string> Process()
    {
      var noredundantFiles = Directory.GetFiles(options.InputDirectory, "*.noredundant").ToList();
      noredundantFiles.Sort();

      using (StreamWriter sw = new StreamWriter(options.OutputFile))
      {
        List<IdentificationSummary> summaries = new List<IdentificationSummary>();
        foreach (var file in noredundantFiles)
        {
          summaries.Add(IdentificationSummary.Parse(file, options.DecoyPattern, options.Calculator));
        }

        bool hasSemi = summaries.Any(m => m.SemiSpectrumCount != 0);
        if (hasSemi)
        {
          sw.WriteLine("Dataset\tFull Spectrum Count\tFull Spectrum FDR\tFull Peptide Count\tFull Peptide FDR\tSemi Spectrum Count\tSemi Spectrum FDR\tSemi Peptide Count\tSemi PeptideFDR\tSpectrum Count\tPeptide Count\tProtein\tTwo-Hits\tTwo-Hits Percentage\tTarget Two-Hits\tTwo-Hits FDR\tOne-Hit Wonder\tTarget One-Hit Wonder\tOne-Hit Wonder FDR");
          foreach (var summary in summaries)
          {
            sw.WriteLine("{0}\t{1}\t{2}\t{3:0.00}%\t{4}\t{5}\t{6:0.00}%\t{7}\t{8}\t{9:0.00}%\t{10}\t{11}\t{12:0.00}%\t{13}\t{14}\t{15}\t{16}\t{17:0.00}%\t{18}\t{19:0.00}%\t{20}\t{21}\t{22:0.00}%",
              summary.FileName,
              summary.FullSpectrumCount,
              summary.FullTargetSpectrumCount,
              summary.FullSpectrumFdr * 100,
              summary.FullPeptideCount,
              summary.FullTargetPeptideCount,
              summary.FullPeptideFdr * 100,
              summary.SemiSpectrumCount,
              summary.SemiTargetSpectrumCount,
              summary.SemiSpectrumFdr * 100,
              summary.SemiPeptideCount,
              summary.SemiTargetPeptideCount,
              summary.SemiPeptideFdr * 100,
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
          sw.WriteLine("Dataset\tPeptides\tTarget Peptides\tPeptide FDR\tUnique Peptides\tTarget Unique Peptides\tUnique Peptides FDR\tProteins\tTwo-hit proteins\tTwo-hit protein percentage\tTarget two-hit proteins\tTwo-hit protein FDR\tOne-hit-wonders\tTarget one-hit-wonders\tOne-hit-wonder FDR");
          foreach (var summary in summaries)
          {
            sw.WriteLine("{0}\t{1}\t{2}\t{3:0.00}%\t{4}\t{5}\t{6:0.00}%\t{7}\t{8}\t{9:0.00}%\t{10}\t{11:0.00}%\t{12}\t{13}\t{14:0.00}%",
              summary.FileName,
              summary.FullSpectrumCount,
              summary.FullTargetSpectrumCount,
              summary.FullSpectrumFdr * 100,
              summary.FullPeptideCount,
              summary.FullTargetPeptideCount,
              summary.FullPeptideFdr * 100,
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

      return new string[] { options.OutputFile };
    }
  }
}
