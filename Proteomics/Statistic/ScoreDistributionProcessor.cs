using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Analysis
{
  public class ScoreFunctions : AbstractScoreFunctions
  {
    public ScoreFunctions()
      : base("Score")
    { }

    public override double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(spectrum.Score * 5) / 5;
    }
  }

  public class LogScoreFunctions : AbstractScoreFunctions
  {
    public LogScoreFunctions()
      : base("log(Score)")
    { }

    public override double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(Math.Log(spectrum.Score) * 5) / 5;
    }
  }

  public class ScoreDistributionProcessor : AbstractThreadFileProcessor
  {
    private string modificationAminoacids = "STY";

    private string decoyPattern = "^REV";

    public IFalseDiscoveryRateCalculator FdrCalc { get; set; }

    private IScoreFunctions getScore = new ScoreFunctions();

    private bool conflictAsDecoy = true;

    public ScoreDistributionProcessor()
    {
      FdrCalc = new TotalFalseDiscoveryRateCalculator();
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var format = new SequestPeptideTextFormat();
      var spectra = format.ReadFromFile(fileName);
      var result = new List<string>();

      if (!spectra.Any(m => m.FromDecoy))
      {
        DecoyPeptideBuilder.AssignDecoy<IIdentifiedSpectrum>(spectra, decoyPattern, conflictAsDecoy);
      }

      var tags = (from s in spectra
                  select s.Tag).Distinct().ToList();
      tags.Sort();

      if (tags.Count > 1)
      {
        string result1 = fileName + ".tags.xls";
        result.Add(result1);
        using (StreamWriter sw = new StreamWriter(result1))
        {
          sw.WriteLine("Tags\t\tFixed Features\t\t\t\tUngrouped\t\t\tGrouped\t\t\t");
          sw.WriteLine("Name\tValue\tCharge\tNMC\tMOD\tNPT\tPeptide\tUnique Peptide\tPeptide FDR\tPeptide\tUnique Peptide\tPeptide Increased\tUnique Peptide Increased");

          modificationAminoacids = "MSTY";
          DoAnalysis(spectra, sw, "Tag", 2, 0, 0, 2, tags.ConvertAll(m => (object)m).ToArray(), new TagFilter("", false));
        }
      }
      else
      {
        string result1 = fileName + ".features.xls";
        result.Add(result1);
        using (StreamWriter sw = new StreamWriter(result1))
        {
          sw.WriteLine("Test Features\t\tFixed Features\t\t\t\tUngrouped\t\t\tGrouped\t\t\t");
          sw.WriteLine("Name\tValue\tCharge\tNMC\tMOD\tNPT\tPeptide\tUnique Peptide\tPeptide FDR\tPeptide\tUnique Peptide\tPeptide Increased\tUnique Peptide Increased");

          modificationAminoacids = "STY";
          DoAnalysis(spectra, sw, "MOD[STY]", 3, 0, -1, 2, new object[] { true, false }, new IdentifiedSpectrumModificationFilter(true));

          modificationAminoacids = "K";
          DoAnalysis(spectra, sw, "MOD[K]", 3, 0, -1, 2, new object[] { true, false }, new IdentifiedSpectrumModificationFilter(true));

          modificationAminoacids = "M";
          DoAnalysis(spectra, sw, "MOD[M]", 3, 0, -1, 2, new object[] { true, false }, new IdentifiedSpectrumModificationFilter(true));

          modificationAminoacids = "MSTY";
          DoAnalysis(spectra, sw, "MOD[MSTY]", 3, 0, -1, 2, new object[] { true, false }, new IdentifiedSpectrumModificationFilter(true));

          modificationAminoacids = "MSTY";
          DoAnalysis(spectra, sw, "Charge", -1, 0, 0, 2, new object[] { 1, 2, 3 }, new ChargeFilter(1));

          modificationAminoacids = "MSTY";
          DoAnalysis(spectra, sw, "NMC", 2, -1, 0, 2, new object[] { 0, 1, 2 }, new NumOfMissCleavageFilter(0));

          modificationAminoacids = "MSTY";
          DoAnalysis(spectra, sw, "NPT", 2, 0, 0, -1, new object[] { 1, 2 }, new NumOfProteaseTerminiFilter(1));
        }

        string result2 = fileName + ".power.xls";
        using (StreamWriter sw = new StreamWriter(result2))
        {
          sw.WriteLine("Features\tCount");

          modificationAminoacids = "MSTY";
          IModificationCountCalculator calc = new ModificationCountCalculator(modificationAminoacids);
          spectra.ForEach(m => m.Annotations["Modified"] = (calc.Calculate(m.Sequence) > 0).ToString());

          var noneCount = FilterByFdr(spectra).Count;
          var nptCount = 0;
          var nptChargeCount = 0;
          var nptChargeNmcCount = 0;
          var nptChargeNmcModCount = 0;

          foreach (var npt in new int[] { 1, 2 })
          {
            var nptFilter = new NumOfProteaseTerminiFilter(npt);
            var nptSpectra = spectra.Where(m => nptFilter.Accept(m)).ToList();

            if (nptSpectra.Count == 0)
            {
              continue;
            }

            nptCount += FilterByFdr(nptSpectra).Count;

            foreach (var charge in new int[] { 1, 2, 3 })
            {
              var cFilter = new ChargeFilter(charge);
              var cSpectra = nptSpectra.Where(m => cFilter.Accept(m)).ToList();

              if (cSpectra.Count == 0)
              {
                continue;
              }

              nptChargeCount += FilterByFdr(cSpectra).Count;

              foreach (var nmc in new int[] { 0, 1, 2 })
              {
                var nmcFilter = new NumOfMissCleavageFilter(nmc);
                var nmcSpectra = cSpectra.Where(m => nmcFilter.Accept(m)).ToList();

                if (nmcSpectra.Count == 0)
                {
                  continue;
                }

                nptChargeNmcCount += FilterByFdr(nmcSpectra).Count;

                foreach (var mod in new bool[] { true, false })
                {
                  var modFilter = new IdentifiedSpectrumModificationFilter(mod);
                  var modSpectra = nmcSpectra.Where(m => modFilter.Accept(m)).ToList();

                  if (modSpectra.Count == 0)
                  {
                    continue;
                  }

                  nptChargeNmcModCount += FilterByFdr(modSpectra).Count;
                }
              }
            }
          }

          sw.WriteLine("None\t{0}", noneCount);
          sw.WriteLine("NPT\t{0}", nptCount);
          sw.WriteLine("NPT+C\t{0}", nptChargeCount);
          sw.WriteLine("NPT+C+NMC\t{0}", nptChargeNmcCount);
          sw.WriteLine("NPT+C+NMC+MOD\t{0}", nptChargeNmcModCount);
        }
      }

      return result;
    }

    private void DoAnalysis(List<IIdentifiedSpectrum> spectra, StreamWriter sw, string featureName, int fixCharge, int fixNMC, int fixMOD, int fixNPT, object[] values, ISpectrumFilter filter)
    {
      IModificationCountCalculator calc = new ModificationCountCalculator(modificationAminoacids);
      spectra.ForEach(m => m.Annotations["Modified"] = (calc.Calculate(m.Sequence) > 0).ToString());

      var filters = GetSpectrumFilter(fixCharge, fixNMC, fixMOD, fixNPT);
      var filteredSpectra = spectra.Where(m => filters.Accept(m)).ToList();

      List<IIdentifiedSpectrum> acceptedSpectra = FilterByFdr(filteredSpectra);

      bool isFirst = true;
      int totalGroupedCount = 0;

      HashSet<string> pep3 = new HashSet<string>();
      foreach (var value in values)
      {
        filter.SetCriteria(value);
        var s = acceptedSpectra.Where(m => filter.Accept(m)).ToList();
        var count = s.Count;
        var pepcount1 = IdentifiedSpectrumUtils.GetUniquePeptideCount(s);
        var fdr = GetFdr(s);

        var s2 = filteredSpectra.Where(m => filter.Accept(m)).ToList();
        var s3 = FilterByFdr(s2);
        var pepcount3 = IdentifiedSpectrumUtils.GetUniquePeptideCount(s3);
        totalGroupedCount += s3.Count;

        pep3.UnionWith(from ss in s3
                       select ss.Peptide.PureSequence);

        var increasedPrecentage = 100.0 * (s3.Count - count) / count;
        var increasedUnique = 100.0 * (pepcount3 - pepcount1) / pepcount1;

        string v;
        if (value is bool)
        {
          v = ((bool)value) ? "T" : "F";
        }
        else
        {
          v = value.ToString();
        }

        if (isFirst)
        {
          sw.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8:0.00}%\t{9}\t{10}\t{11:0.00}%\t{12:0.00}%",
            featureName,
            v,
            fixCharge == -1 ? "-" : fixCharge.ToString(),
            fixNMC == -1 ? "-" : fixNMC.ToString(),
            fixMOD == -1 ? "-" : fixMOD != 0 ? "T" : "F",
            fixNPT == -1 ? "-" : fixNPT.ToString(),
            count,
            pepcount1,
            fdr * 100,
            s3.Count,
            pepcount3,
            increasedPrecentage,
            increasedUnique);
          isFirst = false;
        }
        else
        {
          sw.Write("\t{0}\t\t\t\t\t{1}\t{2}\t{3:0.00}%\t{4}\t{5}\t{6:0.00}%\t{7:0.00}%",
            v,
            count,
            pepcount1,
            fdr * 100,
            s3.Count,
            pepcount3,
            increasedPrecentage,
            increasedUnique);
        }

        sw.WriteLine();
      }

      var uniall1 = IdentifiedSpectrumUtils.GetUniquePeptideCount(acceptedSpectra);
      var uniall3 = pep3.Count;
      sw.WriteLine("\tAll\t\t\t\t\t{0}\t{1}\t1.00%\t{2}\t{3}\t{4:0.00}%\t{5:0.00}%",
        acceptedSpectra.Count,
        uniall1,
        totalGroupedCount,
        uniall3,
        100.0 * (totalGroupedCount - acceptedSpectra.Count) / acceptedSpectra.Count,
        100.0 * (uniall3 - uniall1) / uniall1
        );
    }

    private List<IIdentifiedSpectrum> FilterByFdr(List<IIdentifiedSpectrum> filteredSpectra)
    {
      IdentifiedSpectrumUtils.CalculateQValue(filteredSpectra, getScore, FdrCalc);
      List<IIdentifiedSpectrum> acceptedSpectra = new List<IIdentifiedSpectrum>();
      for (int i = filteredSpectra.Count - 1; i >= 0; i--)
      {
        if (filteredSpectra[i].QValue <= 0.01)
        {
          acceptedSpectra = filteredSpectra.GetRange(0, i);
          break;
        }
      }
      return acceptedSpectra;
    }

    private double GetFdr(List<IIdentifiedSpectrum> s)
    {
      int decoyCount = (s.Where(m => m.FromDecoy).Count());
      int targetCount = (s.Count) - decoyCount;
      return FdrCalc.Calculate(decoyCount, targetCount);
    }

    private AndSpectrumFilter GetSpectrumFilter(int fixCharge, int fixNMC, int fixMOD, int fixNPT)
    {
      AndSpectrumFilter filters = new AndSpectrumFilter();

      if (fixCharge != -1)
      {
        filters.AddFilter(new ChargeFilter(fixCharge));
      }

      if (fixNMC != -1)
      {
        filters.AddFilter(new NumOfMissCleavageFilter(fixNMC));
      }

      if (fixMOD != -1)
      {
        filters.AddFilter(new IdentifiedSpectrumModificationFilter(fixMOD != 0));
      }

      if (fixNPT != -1)
      {
        filters.AddFilter(new NumOfProteaseTerminiFilter(fixNPT));
      }

      return filters;
    }
  }
}
