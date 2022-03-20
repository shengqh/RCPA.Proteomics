using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorPeptideDistiller : AbstractParallelTaskProcessor
  {
    private PercolatorPeptideDistillerOptions _options;

    public PercolatorPeptideDistiller(PercolatorPeptideDistillerOptions options)
    {
      this._options = options;
    }

    public override IEnumerable<string> Process()
    {
      var spectra = new PercolatorOutputXmlPsmReader().ReadFromFile(_options.PercolatorOutputFile);
      var inputspec = new PercolatorInputXmlPsmReader().ReadFromFile(_options.PercolatorInputFile);
      var scanMap = inputspec.ToDictionary(m => GetPsmId(m));
      spectra.ForEach(m =>
      {
        var psmid = GetPsmId(m);
        var inputScan = scanMap[psmid];
        m.Query.QueryId = inputScan.Query.QueryId;
        m.Query.FileScan.FirstScan = m.Query.QueryId;
        m.Query.FileScan.LastScan = m.Query.QueryId;
        m.Query.Charge = inputScan.Query.Charge;
        m.ExperimentalMH = inputScan.ExperimentalMH;
        m.TheoreticalMH = inputScan.TheoreticalMH;
        m.NumMissedCleavages = inputScan.NumMissedCleavages;
        m.Score = inputScan.Score;
      });
      var specMap = spectra.GroupBy(m => m.Query.QueryId).ToList();
      var result = new List<IIdentifiedSpectrum>();
      foreach (var spec in specMap)
      {
        if (spec.Count() == 1)
        {
          result.Add(spec.First());
        }
        else
        {
          var lst = spec.OrderByDescending(m => m.SpScore).ToList();
          if (lst[1].SpScore < lst[0].SpScore)
          {
            result.Add(lst[0]);
          }
          else
          {
            if (lst[0].FromDecoy)
            {
              result.Add(lst[0]);
            }
            else if (lst[1].FromDecoy)
            {
              result.Add(lst[1]);
            }
            else
            {
              lst[0].AddPeptide(lst[1].Peptide);
              result.Add(lst[0]);
            }
          }
        }
      }

      result.Sort((m1, m2) => m2.SpScore.CompareTo(m1.SpScore));

      var format = new MascotPeptideTextFormat("QueryId\tSpectrumId\tFileScan\tSequence\tCharge\tScore\tSvmScore\tMissCleavage\tQValue\tTheoreticalMH\tExperimentMH\tTarget/Decoy");

      var targetFile = _options.PercolatorOutputFile + ".peptides";
      format.WriteToFile(targetFile, result);

      new QValueCalculator(new PercolatorScoreFunction(), new TargetFalseDiscoveryRateCalculator()).CalculateQValue(result);
      result.RemoveAll(m => m.QValue >= 0.01);
      var target001file = FileUtils.ChangeExtension(targetFile, ".FDR0.01.peptides");
      format.WriteToFile(target001file, result);

      return new[] { targetFile };
    }

    private static string GetPsmId(IIdentifiedSpectrum m)
    {
      return (m.FromDecoy ? "decoy_" : "") + m.Id.ToString();
    }
  }
}