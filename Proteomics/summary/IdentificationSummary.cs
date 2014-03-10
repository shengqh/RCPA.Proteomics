using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Mascot;
using RCPA.Utils;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Summary
{
  public class IdentificationSummary
  {
    public string FileName { get; set; }

    public int FullSpectrumCount { get; set; }

    public int SemiSpectrumCount { get; set; }

    public int FullPeptideCount { get; set; }

    public int SemiPeptideCount { get; set; }

    public int ProteinGroupCount { get; set; }

    public int Unique2ProteinGroupCount { get; set; }

    public int Unique2ProteinGroupTargetCount { get; set; }

    public double Unique2ProteinFdr { get; set; }

    public int Unique1ProteinGroupTargetCount { get; set; }

    public double Unique1ProteinFdr { get; set; }

    public int SpectrumCount
    {
      get
      {
        return FullSpectrumCount + SemiSpectrumCount;
      }
    }

    public int UniquePeptideCount
    {
      get
      {
        return FullPeptideCount + SemiPeptideCount;
      }
    }

    public double Unique2ProteinGroupPercentage
    {
      get
      {
        if (ProteinGroupCount == 0)
        {
          return 0;
        }

        return 100.0 * Unique2ProteinGroupCount / ProteinGroupCount;
      }
    }

    public static IdentificationSummary Parse(string proteinFile, string defaultDecoyPattern, IFalseDiscoveryRateCalculator defaultCalc)
    {
      IdentificationSummary result = new IdentificationSummary();

      result.FileName = FileUtils.ChangeExtension(new FileInfo(proteinFile).Name, "");

      Regex decoyReg = new Regex(defaultDecoyPattern);

      IIdentifiedProteinGroupFilter decoyFilter = null;
      IFalseDiscoveryRateCalculator curCalc = null;

      var paramFile = FileUtils.ChangeExtension(proteinFile, ".param");
      if (File.Exists(paramFile))
      {
        BuildSummaryOptions options = BuildSummaryOptionsUtils.LoadFromFile(paramFile);
        if (options.FalseDiscoveryRate.FilterByFdr)
        {
          decoyFilter = options.GetDecoyGroupFilter();
          curCalc = options.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();
        }
      }

      if (decoyFilter == null)
      {
        decoyFilter = new IdentifiedProteinGroupNameRegexFilter(defaultDecoyPattern, false);
        curCalc = defaultCalc;
      }

      var peptideFile = FileUtils.ChangeExtension(proteinFile, ".peptides");
      if (File.Exists(peptideFile))
      {
        var peptides = new MascotPeptideTextFormat().ReadFromFile(peptideFile);

        var fullSpectra = from p in peptides
                          where p.NumProteaseTermini == 2
                          select p;

        result.FullSpectrumCount = (from p in fullSpectra
                                    select p.Query.FileScan.LongFileName).Distinct().Count();

        var semiSpectra = from p in peptides
                          where p.NumProteaseTermini == 1
                          select p;

        result.SemiSpectrumCount = (from p in semiSpectra
                                    select p.Query.FileScan.LongFileName).Distinct().Count();

        result.FullPeptideCount = IdentifiedSpectrumUtils.GetUniquePeptideCount(fullSpectra);

        result.SemiPeptideCount = IdentifiedSpectrumUtils.GetUniquePeptideCount(semiSpectra);
      }

      if (File.Exists(proteinFile))
      {
        var ir = new MascotResultTextFormat().ReadFromFile(proteinFile);
        ir.InitUniquePeptideCount();

        var u2proteins = (from p in ir
                          where p[0].UniquePeptideCount > 1
                          select p).ToList();

        var u1proteins = (from p in ir
                          where p[0].UniquePeptideCount == 1
                          select p).ToList();

        result.ProteinGroupCount = ir.Count;
        result.Unique2ProteinGroupCount = u2proteins.Count;

        int targetCount;
        result.Unique2ProteinFdr = CalculateProteinFdr(u2proteins, decoyFilter, defaultCalc, out targetCount);
        result.Unique2ProteinGroupTargetCount = (int)targetCount;

        result.Unique1ProteinFdr = CalculateProteinFdr(u1proteins, decoyFilter, defaultCalc, out targetCount);
        result.Unique1ProteinGroupTargetCount = (int)targetCount;
      }

      return result;
    }

    private static double CalculateProteinFdr(List<IIdentifiedProteinGroup> groups, IIdentifiedProteinGroupFilter decoyFilter, IFalseDiscoveryRateCalculator calc, out int targetCount)
    {
      targetCount = 0;
      int decoyCount = 0;
      foreach (var group in groups)
      {
        if (decoyFilter.Accept(group))
        {
          decoyCount++;
        }
        else
        {
          targetCount++;
        }
      }
      return calc.Calculate(decoyCount, targetCount);
    }
  }
}
