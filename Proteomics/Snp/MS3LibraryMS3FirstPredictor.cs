using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Snp
{
  public class MS3LibraryMS3FirstPredictor : AbstractMS3LibraryPredictor
  {
    public MS3LibraryMS3FirstPredictor(MS3LibraryPredictorOptions options) : base(options) { }

    protected override void FindCandidates(Dictionary<int, List<MS2Item>> lib, List<MS2Item> queries, List<SapPredicted> predicted, double minDeltaMass, double maxDeltaMass)
    {
      using (var sw = new StreamWriter(Path.ChangeExtension(options.OutputFile, ".interval.tsv")))
      {
        sw.WriteLine("FileScan\tPrecursor\tCharge\tLibPrecursor\tMatchedMs3Precursor\tMatchedMs3Ions\tDeltaMass\tLibFileScan\tLibSequence\tLibScore\tLibExpectValue\tLibProteins");

        foreach (var query in queries)
        {
          Progress.Increment(1);

          List<MS2Item> libms2List;

          if (!lib.TryGetValue(query.Charge, out libms2List))
          {
            continue;
          }

          foreach (var libms2 in libms2List)
          {
            var ms3match = GetMS3MatchedCount(libms2, query);
            if (!IsValid(ms3match))
            {
              continue;
            }

            OutputIntervalResult(sw, query, libms2, ms3match);

            var diff = (query.Precursor - libms2.Precursor) * query.Charge;
            if (diff >= minDeltaMass && diff <= maxDeltaMass) //subsitution
            {
              CheckSAP(predicted, query, libms2, ms3match);
            }

            if (options.AllowTerminalLoss)
            {
              CheckTerminalLoss(predicted, query, libms2, ms3match);
            }

            if (options.AllowTerminalExtension)
            {
              CheckTerminalExtension(predicted, query, libms2, ms3match);
            }
          }
        }
      }
    }
  }
}
