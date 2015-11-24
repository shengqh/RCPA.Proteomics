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
  public abstract class AbstractMS3LibraryPredictor : AbstractThreadProcessor
  {
    protected MS3LibraryPredictorOptions options;

    public AbstractMS3LibraryPredictor(MS3LibraryPredictorOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var expRawfileMap = options.RawFiles.ToDictionary(m => Path.GetFileNameWithoutExtension(m));

      Progress.SetMessage("Reading library file ...");
      var liblist = new MS2ItemXmlFormat().ReadFromFile(options.LibraryFile);
      PreprocessingMS2ItemList(liblist);

      var lib = liblist.GroupBy(m => m.Charge).ToDictionary(m => m.Key, m => m.ToList());

      Progress.SetMessage("Building library sequence amino acid composition ...");
      lib.ForEach(m => m.Value.ForEach(l => l.AminoacidCompsition = (from a in l.Peptide
                                                                     where options.SubstitutionDeltaMassMap.ContainsKey(a)
                                                                     select a).Distinct().OrderBy(k => k).ToArray()));

      var expScanMap = (from p in liblist
                        from sq in p.FileScans
                        select sq).ToList().GroupBy(m => m.Experimental).ToDictionary(m => m.Key, m => new HashSet<int>(from l in m select l.FirstScan));

      if (File.Exists(options.PeptidesFile))
      {
        Progress.SetMessage("Reading peptides file used for excluding scan ...");
        var peptides = new MascotPeptideTextFormat().ReadFromFile(options.PeptidesFile);
        foreach (var pep in peptides)
        {
          HashSet<int> scans;
          if (!expScanMap.TryGetValue(pep.Query.FileScan.Experimental, out scans))
          {
            scans = new HashSet<int>();
            expScanMap[pep.Query.FileScan.Experimental] = scans;
          }
          scans.Add(pep.Query.FileScan.FirstScan);
        }
      }

      Progress.SetMessage("Reading MS2/MS3 data ...");
      var result = GetCandidateMs2ItemList(expRawfileMap, expScanMap);
      PreprocessingMS2ItemList(result);

      //new MS2ItemXmlFormat().WriteToFile(options.OutputFile + ".xml", result);

      Progress.SetMessage("Finding SAP ...");
      List<SapPredicted> predicted = new List<SapPredicted>();

      var minDeltaMass = options.SubstitutionDeltaMassMap.Values.Min(l => l.Min(k => k.DeltaMass));
      var maxDeltaMass = options.SubstitutionDeltaMassMap.Values.Max(l => l.Max(k => k.DeltaMass));

      Progress.SetRange(0, result.Count);
      Progress.Begin();

      FindCandidates(lib, result, predicted, minDeltaMass, maxDeltaMass);

      var groups = predicted.ToGroupDictionary(m => m.Ms2.GetFileScans());
      predicted.Clear();
      foreach (var g in groups.Values)
      {
        var gg = g.ToGroupDictionary(m => m.LibMs2).Values.ToList();
        gg.Sort((m1, m2) =>
        {
          return CompareSapPrecitedList(m1, m2);
        });

        var expect = gg[0].FirstOrDefault(m => m.IsExpect);
        if (expect != null)
        {
          predicted.Add(expect);
        }
        else
        {
          predicted.AddRange(gg[0]);
          for (int i = 1; i < gg.Count; i++)
          {
            if (CompareSapPrecitedList(gg[0], gg[i]) == 0)
            {
              predicted.AddRange(gg[i]);
            }
            else
            {
              break;
            }
          }
        }
      }

      if (File.Exists(options.MatchedFile))
      {
        new SapPredictedValidationWriter(options.MatchedFile).WriteToFile(options.OutputFile, predicted);
      }
      else
      {
        new SapPredictedWriter().WriteToFile(options.OutputTableFile, predicted);

        Progress.SetMessage("Generating SAP sequence ...");
        List<Sequence> predictedSeq = new List<Sequence>();
        foreach (var predict in predicted)
        {
          var seq = PeptideUtils.GetPureSequence(predict.LibMs2.Peptide);
          if (predict.Target.TargetType == VariantType.SingleAminoacidPolymorphism)
          {
            for (int i = 0; i < seq.Length; i++)
            {
              if (seq[i] == predict.Target.Source[0])
              {
                foreach (var t in predict.Target.Target)
                {
                  string targetSeq;
                  if (i == 0)
                  {
                    targetSeq = t + seq.Substring(1);
                  }
                  else
                  {
                    targetSeq = seq.Substring(0, i) + t + seq.Substring(i + 1);
                  }

                  var reference = string.Format("sp|SAP_{0}_{1}|{2}_{3}_{4}_{5}", targetSeq, predict.Target.TargetType, seq, predict.Target.Source, i + 1, t);
                  predictedSeq.Add(new Sequence(reference, targetSeq));
                }
              }
            }
          }
          else
          {
            foreach (var tseq in predict.Target.Target)
            {
              string reference;
              if (predict.Target.TargetType == VariantType.NTerminalLoss)
              {
                reference = string.Format("sp|SAP_{0}_{1}|{2}_loss_{3}", tseq, predict.Target.TargetType, seq, seq.Substring(0, seq.Length - tseq.Length));
              }
              else if (predict.Target.TargetType == VariantType.CTerminalLoss)
              {
                reference = string.Format("sp|SAP_{0}_{1}|{2}_loss_{3}", tseq, predict.Target.TargetType, seq, seq.Substring(tseq.Length));
              }
              else if (predict.Target.TargetType == VariantType.NTerminalExtension)
              {
                reference = string.Format("sp|SAP_{0}_{1}|{2}_ext_{3}", tseq, predict.Target.TargetType, seq, tseq.Substring(0, tseq.Length - seq.Length));
              }
              else if (predict.Target.TargetType == VariantType.CTerminalExtension)
              {
                reference = string.Format("sp|SAP_{0}_{1}|{2}_ext_{3}", tseq, predict.Target.TargetType, seq, tseq.Substring(seq.Length));
              }
              else
              {
                throw new Exception("I don't know how to deal with " + predict.Target.TargetType.ToString());
              }

              predictedSeq.Add(new Sequence(reference, tseq));
            }
          }
        }

        predictedSeq = (from g in predictedSeq.GroupBy(m => m.SeqString)
                        select g.First()).ToList();

        Progress.SetMessage("Reading database {0} ...", options.DatabaseFastaFile);
        var databases = SequenceUtils.Read(options.DatabaseFastaFile);

        Progress.SetMessage("Removing variant sequences which are already existed in database ...");
        for (int i = predictedSeq.Count - 1; i >= 0; i--)
        {
          foreach (var db in databases)
          {
            if (db.SeqString.Contains(predictedSeq[i].SeqString))
            {
              predictedSeq.RemoveAt(i);
              break;
            }
          }
        }
        databases.AddRange(predictedSeq);

        Progress.SetMessage("Writing SAP sequence and original database to {0} ...", options.OutputFile);

        SequenceUtils.Write(new FastaFormat(), options.OutputFile, databases);
      }

      Progress.End();

      return new string[] { options.OutputFile, options.OutputTableFile };
    }

    private void PreprocessingMS2ItemList(List<MS2Item> items)
    {
      items.ForEach(m =>
      {
        m.MS3Spectra.RemoveAll(k => k.PrecursorMZ < options.MinimumMS3PrecursorMz);

        foreach (var s in m.MS3Spectra)
        {
          if (s.Count > options.MaximumFragmentPeakCount)
          {
            s.SortByIntensity();
            s.RemoveRange(options.MaximumFragmentPeakCount, s.Count - options.MaximumFragmentPeakCount);
            s.SortByMz();
          }
        }
      });

      items.RemoveAll(m => m.MS3Spectra.Count == 0);

      items.ForEach(m =>
      {
        var d2mass = PrecursorUtils.ppm2mz(m.Precursor, options.MS2PrecursorPPMTolerance);
        m.MinPrecursorMz = m.Precursor - d2mass;
        m.MaxPrecursorMz = m.Precursor + d2mass;

        m.MS3Spectra.ForEach(l =>
        {
          var d3mass = PrecursorUtils.ppm2mz(l.PrecursorMZ, options.MS3PrecursorPPMTolerance);
          l.MinPrecursorMz = l.PrecursorMZ - d3mass;
          l.MaxPrecursorMz = l.PrecursorMZ + d3mass;

          l.ForEach(p =>
          {
            var dmass = PrecursorUtils.ppm2mz(p.Mz, options.MS3FragmentIonPPMTolerance);
            p.MinMatchMz = p.Mz - dmass;
            p.MaxMatchMz = p.Mz + dmass;
          });
        });
      });
    }

    protected abstract void FindCandidates(Dictionary<int, List<MS2Item>> lib, List<MS2Item> result, List<SapPredicted> predicted, double minDeltaMass, double maxDeltaMass);

    protected void CheckSAP(List<SapPredicted> predicted, MS2Item query, MS2Item libms2, SapMatchedCount ms3match)
    {
      foreach (var aa in libms2.AminoacidCompsition)
      {
        var lst = options.SubstitutionDeltaMassMap[aa];
        //the list has been ordered by deltamass
        foreach (var ts in lst)
        {
          var targetMz = libms2.Precursor + ts.DeltaMass / query.Charge;
          if (targetMz < query.MinPrecursorMz)
          {
            continue;
          }

          if (targetMz > query.MaxPrecursorMz)
          {
            break;
          }

          var curp = new SapPredicted()
          {
            Ms2 = query,
            LibMs2 = libms2,
            Matched = ms3match,
            Target = new TargetVariant()
            {
              Source = ts.Source,
              Target = ts.Target,
              DeltaMass = ts.DeltaMass,
              TargetType = ts.TargetType
            }
          };

          predicted.Add(curp);
        }
      }
    }

    protected void CheckTerminalLoss(List<SapPredicted> predicted, MS2Item query, MS2Item libms2, SapMatchedCount ms3match)
    {
      foreach (var nl in libms2.TerminalLoss)
      {
        if (nl.Precursor >= query.MinPrecursorMz && nl.Precursor <= query.MaxPrecursorMz)
        {
          var curp = new SapPredicted()
          {
            Ms2 = query,
            LibMs2 = libms2,
            Matched = ms3match,
            Target = new TargetVariant()
            {
              Source = PeptideUtils.GetPureSequence(libms2.Peptide),
              Target = new HashSet<string>(new[] { nl.Sequence }),
              DeltaMass = (nl.Precursor - libms2.Precursor) * libms2.Charge,
              TargetType = nl.IsNterminal ? VariantType.NTerminalLoss : VariantType.CTerminalLoss
            }
          };

          predicted.Add(curp);
        }
      }
    }

    protected void CheckTerminalExtension(List<SapPredicted> predicted, MS2Item query, MS2Item libms2, SapMatchedCount ms3match)
    {
      var bNterminalValid = libms2.Peptide.StartsWith("-");
      var bCterminalValid = libms2.Peptide.EndsWith("-");

      if (bNterminalValid || bCterminalValid)
      {
        foreach (var ne in options.ExtensionDeltaMassList)
        {
          var neMz = libms2.Precursor + ne.DeltaMass / libms2.Charge;
          if (neMz >= query.MinPrecursorMz && neMz <= query.MaxPrecursorMz)
          {
            var seq = PeptideUtils.GetPureSequence(libms2.Peptide);

            if (bNterminalValid)
            {
              predicted.Add(new SapPredicted()
              {
                Ms2 = query,
                LibMs2 = libms2,
                Matched = ms3match,
                Target = new TargetVariant()
                {
                  Source = PeptideUtils.GetPureSequence(libms2.Peptide),
                  Target = new HashSet<string>(from t in ne.Target select t + seq),
                  DeltaMass = ne.DeltaMass,
                  TargetType = VariantType.NTerminalExtension
                }
              });
            }

            if (bCterminalValid)
            {
              predicted.Add(new SapPredicted()
              {
                Ms2 = query,
                LibMs2 = libms2,
                Matched = ms3match,
                Target = new TargetVariant()
                {
                  Source = PeptideUtils.GetPureSequence(libms2.Peptide),
                  Target = new HashSet<string>(from t in ne.Target select seq + t),
                  DeltaMass = ne.DeltaMass,
                  TargetType = VariantType.CTerminalExtension
                }
              });
            }
          }
        }
      }
    }

    protected static void OutputIntervalResult(StreamWriter sw, MS2Item query, MS2Item libms2, SapMatchedCount ms3match)
    {
      sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}",
        query.GetFileScans(),
        query.Precursor,
        query.Charge,
        libms2.Precursor,
        ms3match.PrecursorMatched.ConvertAll(m => m.ToString()).Merge(";"),
        ms3match.MS3Matched.ConvertAll(m => m.ToString()).Merge(";"),
        (query.Precursor - libms2.Precursor) * query.Charge,
        libms2.GetFileScans(),
        libms2.Peptide,
        libms2.Score,
        libms2.ExpectValue,
        libms2.Proteins);
    }

    protected static int CompareSapPrecitedList(List<SapPredicted> m1, List<SapPredicted> m2)
    {
      var p1 = m1.First();
      var p2 = m2.First();
      var res = p2.Matched.PrecursorMatched.Count.CompareTo(p1.Matched.PrecursorMatched.Count);
      if (res == 0)
      {
        res = p2.Matched.MS3Matched.Count(l => l > 0).CompareTo(p1.Matched.MS3Matched.Count(l => l > 0));
        if (res == 0)
        {
          res = p2.Matched.MS3Matched.Sum().CompareTo(p1.Matched.MS3Matched.Sum());
        }
      }

      return res;
    }

    protected bool IsValid(SapMatchedCount smc)
    {
      if (smc.MS3Matched.Count < options.MinimumMatchedMS3SpectrumCount)
      {
        return false;
      }

      if (smc.MS3Matched.All(l => l < options.MinimumMatchedMS3IonCount))
      {
        return false;
      }

      return true;
    }

    protected SapMatchedCount GetMS3MatchedCount(MS2Item libms2, MS2Item query)
    {
      var precursorMatched = new List<double>();
      var ms3Matched = new List<int>();
      foreach (var pLib in libms2.MS3Spectra)
      {
        foreach (var pQuery in query.MS3Spectra)
        {
          if (pQuery.PrecursorMZ >= pLib.MinPrecursorMz && pQuery.PrecursorMZ <= pLib.MaxPrecursorMz)
          {
            precursorMatched.Add(pLib.PrecursorMZ);

            int ionMatched = 0;

            var iLib = 0;
            var iQuery = 0;
            while (iLib < pLib.Count && iQuery < pQuery.Count)
            {
              if (pQuery[iQuery].Mz < pLib[iLib].MinMatchMz)
              {
                iQuery++;
                continue;
              }

              if (pQuery[iQuery].Mz > pLib[iLib].MaxMatchMz)
              {
                iLib++;
                continue;
              }

              ionMatched++;
              iLib++;
              iQuery++;
            }

            ms3Matched.Add(ionMatched);
            break;
          }
        }
      }

      return new SapMatchedCount()
      {
        Item1 = libms2,
        Item2 = query,
        PrecursorMatched = precursorMatched,
        MS3Matched = ms3Matched
      };
    }

    private List<MS2Item> GetCandidateMs2ItemList(Dictionary<string, string> expRawfileMap, Dictionary<string, HashSet<int>> expScanMap)
    {
      var result = new List<MS2Item>();
      foreach (var exp in expRawfileMap.Keys)
      {
        var rawfile = expRawfileMap[exp];
        var scans = expScanMap.ContainsKey(exp) ? expScanMap[exp] : new HashSet<int>();

        Progress.SetMessage("Reading MS2/MS3 from {0} ...", rawfile);
        using (var reader = RawFileFactory.GetRawFileReader(rawfile, false))
        {
          var firstScan = reader.GetFirstSpectrumNumber();
          var lastScan = reader.GetLastSpectrumNumber();

          Progress.SetRange(firstScan, lastScan);
          for (int scan = firstScan; scan < lastScan; scan++)
          {
            var msLevel = reader.GetMsLevel(scan);
            if (msLevel != 2)
            {
              continue;
            }

            if (scans.Contains(scan))
            {
              continue;
            }

            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            Progress.SetPosition(scan);

            var ms2precursor = reader.GetPrecursorPeak(scan);
            var ms2 = new MS2Item()
            {
              Precursor = ms2precursor.Mz,
              Charge = ms2precursor.Charge,
              FileScans = new SequestFilename[] { new SequestFilename(exp, scan, scan, ms2precursor.Charge, string.Empty) }.ToList()
            };

            for (int ms3scan = scan + 1; ms3scan < lastScan; ms3scan++)
            {
              var mslevel = reader.GetMsLevel(ms3scan);
              if (mslevel != 3)
              {
                scan = ms3scan - 1;
                break;
              }
              var pkl = reader.GetPeakList(ms3scan);
              if (pkl.Count == 0)
              {
                continue;
              }

              var ms3precursor = reader.GetPrecursorPeak(ms3scan);
              pkl.PrecursorMZ = ms3precursor.Mz;
              ms2.MS3Spectra.Add(new MS3Item(pkl));
            }

            if (ms2.MS3Spectra.Count > 0)
            {
              result.Add(ms2);
            }
          }
        }
      }

      return result;
    }
  }
}
