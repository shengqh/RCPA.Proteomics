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
  public class MS3LibraryMS2FirstPredictor : AbstractMS3LibraryPredictor
  {
    public MS3LibraryMS2FirstPredictor(MS3LibraryPredictorOptions options):base(options){}

    protected override void FindCandidates(Dictionary<int, List<MS2Item>> lib, List<MS2Item> result, List<SapPredicted> predicted, double minDeltaMass, double maxDeltaMass)
    {
      using (var sw = new StreamWriter(Path.ChangeExtension(options.OutputFile, ".interval.tsv")))
      {
        sw.WriteLine("FileScan\tPrecursor\tCharge\tLibPrecursor\tMatchedMs3Precursor\tMatchedMs3Ions\tLibFileScan\tLibSequence\tDeltaMass");

        foreach (var ms2 in result)
        {
          Progress.Increment(1);

          if (lib.ContainsKey(ms2.Charge))
          {
            var mzTolerance = PrecursorUtils.ppm2mz(ms2.Precursor, options.PrecursorPPMTolerance);
            var minMz = ms2.Precursor - mzTolerance;
            var maxMz = ms2.Precursor + mzTolerance;

            var libms2s = lib[ms2.Charge];
            foreach (var libms2 in libms2s)
            {
              bool bFound = false;
              var diff = (ms2.Precursor - libms2.Precursor) * ms2.Charge;
              if (diff >= minDeltaMass && diff <= maxDeltaMass) //subsitution
              {
                var ms3match = GetMS3MatchedCount(libms2, ms2, options.MinimumMs3PrecursorMz);
                if (ms3match.MS3Matched.Any(l => l > 1))
                {
                  bFound = true;
                  OutputIntervalResult(sw, ms2, libms2, ms3match);
                  CheckSAP(predicted, ms2, minMz, maxMz, libms2, ms3match);
                }
              }

              if (options.AllowTerminalLoss)
              {
                foreach (var nl in libms2.TerminalLoss)
                {
                  if (nl.Precursor >= minMz && nl.Precursor <= maxMz)
                  {
                    var ms3match = GetMS3MatchedCount(libms2, ms2, options.MinimumMs3PrecursorMz);
                    if (ms3match.MS3Matched.Any(l => l > 1))
                    {
                      if (!bFound)
                      {
                        OutputIntervalResult(sw, ms2, libms2, ms3match);
                      }

                      var curp = new SapPredicted()
                      {
                        Ms2 = ms2,
                        LibMs2 = libms2,
                        Matched = ms3match,
                        Target = new TargetSAP()
                        {
                          IsNterminalLoss = nl.IsNterminal,
                          Source = PeptideUtils.GetPureSequence(libms2.Peptide),
                          Target = nl.Sequence,
                          DeltaMass = (nl.Precursor - libms2.Precursor) * libms2.Charge
                        }
                      };

                      predicted.Add(curp);
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
