using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Summary;
using System.IO;
using RCPA.Seq;
using RCPA.Proteomics.Classification;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Database;
using RCPA.Proteomics.PFind;

namespace RCPA.Proteomics.Snp
{
  public class MascotSAPValidator : AbstractThreadFileProcessor
  {
    private Regex mutationReg;
    private string fastaFile;
    private IAccessNumberParser acParser;
    private HashSet<int> charges;
    private string pNovoPeptideFile;

    public Dictionary<string, List<string>> ClassificationSet { get; set; }

    public bool IgnoreNtermMutation { get; set; }

    public bool IgnoreDeamidatedMutation { get; set; }

    public bool IgnoreMultipleNucleotideMutation { get; set; }

    private Regex upNameRegex = new Regex(@"[sp|tr]\|.+?\|(\S+)");
    private string _uniprotXmlFile;
    private UniprotXmlFormatRandomReader reader;
    private Dictionary<string, UniprotEntry> map = new Dictionary<string, UniprotEntry>();
    public string UniprotXmlFile
    {
      get
      {
        return _uniprotXmlFile;
      }
      set
      {
        _uniprotXmlFile = value;
        map = new Dictionary<string, UniprotEntry>();
        reader = null;
      }
    }

    public UniprotEntry GetUniprotEntry(string proteinName)
    {
      var m = upNameRegex.Match(proteinName);
      if (!m.Success)
      {
        return null;
      }

      var name = m.Groups[1].Value;
      if (!map.ContainsKey(name))
      {
        if (reader == null)
        {
          reader = new UniprotXmlFormatRandomReader();
          reader.Open(_uniprotXmlFile);
        }

        if (reader.Has(name))
        {
          var result = reader.Read(name);
          map[name] = result;
          return result;
        }
        else
        {
          return null;
        }
      }
      else
      {
        return map[name];
      }
    }

    public MascotSAPValidator(string mutationPattern, string fastaFile, IAccessNumberParser acParser, HashSet<int> charges, string pNovoPeptideFile)
    {
      IgnoreNtermMutation = true;
      IgnoreDeamidatedMutation = true;
      IgnoreMultipleNucleotideMutation = true;
      UniprotXmlFile = string.Empty;
      this.mutationReg = new Regex(mutationPattern);
      this.fastaFile = fastaFile;
      this.acParser = acParser;
      this.charges = charges;
      this.pNovoPeptideFile = pNovoPeptideFile;
    }

    public IClassification<IIdentifiedPeptide> GetClassification()
    {
      Dictionary<string, string> classificationMap = new Dictionary<string, string>();

      foreach (var s in ClassificationSet)
      {
        foreach (var exp in s.Value)
        {
          classificationMap[exp] = s.Key;
        }
      }

      return new IdentifiedPeptideMapClassification("EXP", classificationMap);
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var aas = new Aminoacids();

      Progress.SetMessage("reading pNovo result from " + pNovoPeptideFile + " ...");
      var pNovoSpectra = new MascotPeptideTextFormat().ReadFromFile(pNovoPeptideFile);
      var pNovoMap = new Dictionary<string, HashSet<string>>();
      foreach (var pep in pNovoSpectra)
      {
        var key = pep.Query.FileScan.LongFileName;
        if (!pNovoMap.ContainsKey(key))
        {
          pNovoMap[key] = new HashSet<string>();
        }
        pNovoMap[key].UnionWith(from p in pep.Peptides select p.PureSequence);
      }

      var format = new MascotPeptideTextFormat();
      Progress.SetMessage("reading peptide-spectra-matches from " + fileName + " ...");
      var spectra = format.ReadFromFile(fileName);
      //价位筛选
      spectra.RemoveAll(m => !charges.Contains(m.Charge));
      //对于有不确定的氨基酸，直接忽略。
      spectra.ForEach(m =>
      {
        for (int i = m.Peptides.Count - 1; i >= 0; i--)
        {
          if (m.Peptides[i].PureSequence.Any(n => aas[n].Codes.Length == 0))
          {
            m.RemovePeptideAt(i);
          }
        }
      });
      spectra.RemoveAll(m => m.Peptides.Count == 0);

      Progress.SetMessage("comparing peptide-spectra-matches with pNovo result...");
      //与pNovo判定的mutation是否一致？
      spectra.RemoveAll(m =>
      {
        if (!IsMutationPeptide(m))
        {
          return false;
        }

        var key = m.Query.FileScan.LongFileName;
        if (!pNovoMap.ContainsKey(key))
        {
          return true;
        }

        var set = pNovoMap[key];
        return !m.Peptides.Any(n => set.Contains(n.PureSequence.Replace('I', 'L')));
      });

      //Get spectra whose peptides are all from mutated version
      var mutSpectra = spectra.FindAll(m => IsMutationPeptide(m)).ToList();
      var mutPeptides = (from s in mutSpectra
                         from p in s.Peptides
                         select p).ToList();
      var mutGroup = mutPeptides.GroupBy(m => m.PureSequence);

      //Get specra whose peptides are all from wide version
      var fromSpectra = spectra.Except(mutSpectra).ToList();
      fromSpectra.RemoveAll(m => m.Proteins.Any(n => mutationReg.Match(n).Success));
      var fromPeptides = (from s in fromSpectra
                          from p in s.Peptides
                          select p).ToList();
      var fromGroup = fromPeptides.GroupBy(m => m.PureSequence).ToGroupDictionary(n => n.Key.Length);
      var minLength = fromGroup.Count == 0 ? 6 : fromGroup.Min(m => m.Key);
      var maxLength = fromGroup.Count == 0 ? 30 : fromGroup.Max(m => m.Key);

      //Check the mutation type
      var type1 = new List<List<IGrouping<string, IIdentifiedPeptide>>>();
      var type2 = new List<List<IGrouping<string, IIdentifiedPeptide>>>();
      var type3 = new List<List<IGrouping<string, IIdentifiedPeptide>>>();

      Progress.SetRange(0, mutGroup.Count());
      Progress.SetPosition(0);
      Progress.SetMessage("finding mutation-original pairs ...");

      foreach (var mut in mutGroup)
      {
        var matched = new List<IGrouping<string, IIdentifiedPeptide>>();
        matched.Add(mut);
        Progress.Increment(1);

        var protein = mut.First().Proteins[0];

        List<List<IGrouping<string, IIdentifiedPeptide>>> type;
        if (protein.EndsWith("type3"))
        {
          type = type3;
          var mutseq = mut.Key.Substring(0, mut.Key.Length - 1);
          for (int i = mut.Key.Length + 1; i <= maxLength; i++)
          {
            if (fromGroup.ContainsKey(i))
            {
              var others = fromGroup[i];
              foreach (var o in others)
              {
                if (o.Key.StartsWith(mutseq))
                {
                  matched.Add(o);
                }
              }
            }
          }
        }
        else if (protein.EndsWith("type2"))
        {
          type = type2;
          for (int i = minLength; i < mut.Key.Length; i++)
          {
            if (fromGroup.ContainsKey(i))
            {
              var others = fromGroup[i];
              foreach (var o in others)
              {
                var oseq = o.Key.Substring(0, o.Key.Length - 1);
                if (mut.Key.StartsWith(oseq))
                {
                  matched.Add(o);
                }
              }
            }
          }
        }
        else if (protein.EndsWith("type1"))
        {
          type = type1;

          if (fromGroup.ContainsKey(mut.Key.Length))
          {
            var oLength = fromGroup[mut.Key.Length];
            foreach (var o in oLength)
            {
              int mutationSite = -1;
              if (MutationUtils.IsMutationOneIL2(o.Key, mut.Key, ref mutationSite, IgnoreNtermMutation, IgnoreDeamidatedMutation, IgnoreMultipleNucleotideMutation))
              {
                matched.Add(o);
              }
            }
          }
        }
        else
        {
          throw new Exception("There is no mutation type information at protein name: " + protein + "\nIt should be like MUL_NHLGQK_type1, MUL_NHLGQK_type2 or MUL_NHLGQK_type3");
        }

        type.Add(matched);
      }

      type1.Sort((m1, m2) =>
      {
        var res = m1.Count.CompareTo(m2.Count);
        if (res == 0)
        {
          res = m2[0].Count().CompareTo(m1[0].Count());
        }
        return res;
      });

      Progress.SetMessage("reading protein sequences ...");
      var proteins = SequenceUtils.Read(new FastaFormat(), fastaFile);

      var proMap = proteins.ToDictionary(m =>
      {
        string ac;
        if (acParser.TryParse(m.Name, out ac))
        {
          return ac;
        }
        else
        {
          return m.Name;
        }
      });

      var classification = GetClassification();
      string mutHeader = "FileScan\tMH+\tDiff(MH+)\tCharge\tRank\tScore\tExpectValue\tModification";
      var mutPepFormat = new MascotPeptideTextFormat(mutHeader);

      Progress.SetMessage("writing result ...");
      var result1 = DoStatistic(fileName, aas, format, proMap, classification, mutHeader, mutPepFormat, type1, ".type1");
      var result2 = DoStatistic(fileName, aas, format, proMap, classification, mutHeader, mutPepFormat, type2, ".type2");
      var result3 = DoStatistic(fileName, aas, format, proMap, classification, mutHeader, mutPepFormat, type3, ".type3");

      return result1.Concat(result2).Concat(result3).ToArray();
    }

    private bool IsMutationPeptide(IIdentifiedSpectrum m)
    {
      return m.Proteins.All(n => mutationReg.Match(n).Success);
    }

    private string[] DoStatistic(string fileName, Aminoacids aas, MascotPeptideTextFormat format, Dictionary<string, Sequence> proMap, IClassification<IIdentifiedPeptide> classification, string mutHeader, MascotPeptideTextFormat mutPepFormat, List<List<IGrouping<string, IIdentifiedPeptide>>> curtype, string curname)
    {
      var pairedMut = (from r in curtype
                       where r.Count > 1
                       select r).ToList();

      var dic = pairedMut.GroupBy(m => GetMaxScore(m[0]).Spectrum.Query.FileScan.LongFileName);

      var pairedOne2OneMut = (from d in dic
                              where d.Count() == 1
                              from s in d
                              select s).ToList();

      var pairedOne2OneFile = fileName + curname + ".paired.one2one.mut";
      var pairedOne2OnePeptideFile = OutputPairedResult(aas, format, proMap, classification, mutHeader, mutPepFormat, pairedOne2OneMut, pairedOne2OneFile);

      var pairedOne2MultipleMut = pairedMut.Except(pairedOne2OneMut).OrderBy(m => GetMaxScore(m[0]).Spectrum.Query.FileScan.LongFileName).ToList();
      var pairedOne2MultipleFile = fileName + curname + ".paired.one2multiple.mut";
      var pairedOne2MultiplePeptideFile = OutputPairedResult(aas, format, proMap, classification, mutHeader, mutPepFormat, pairedOne2MultipleMut, pairedOne2MultipleFile);

      var unpairedFile = fileName + curname + ".unpaired.mut";
      var unpairedMut = (from r in curtype
                         where r.Count == 1
                         select r).ToList();

      using (StreamWriter sw = new StreamWriter(unpairedFile))
      {
        sw.WriteLine("Index\t" + mutHeader + "\tSequence\tPepCount");
        int resIndex = 0;
        foreach (var res in unpairedMut)
        {
          resIndex++;

          var curMutSpectrum = GetMaxScore(res[0]);
          var mutSeq = curMutSpectrum.PureSequence;
          sw.WriteLine("${0}\t{1}\t{2}\t{3}", resIndex, mutPepFormat.PeptideFormat.GetString(curMutSpectrum.Spectrum), mutSeq, res[0].Count());
        }
      }

      var unpairedPeptideFile = unpairedFile + ".peptides";
      SavePeptidesFile(unpairedMut, format, unpairedPeptideFile);

      return new string[] { pairedOne2OneFile, pairedOne2OnePeptideFile, pairedOne2MultipleFile, pairedOne2MultiplePeptideFile, unpairedFile, unpairedPeptideFile };
    }

    class TempResult
    {
      public int PepCount { get; set; }
      public string OriginalSequence { get; set; }
      public string Line { get; set; }
    }

    private SapPtmTable spTable = new SapPtmTable();

    private string OutputPairedResult(Aminoacids aas, MascotPeptideTextFormat format, Dictionary<string, Sequence> proMap, IClassification<IIdentifiedPeptide> classification, string mutHeader, MascotPeptideTextFormat mutPepFormat, List<List<IGrouping<string, IIdentifiedPeptide>>> pairedMut, string pairedFile)
    {
      bool dbAnnotation = File.Exists(_uniprotXmlFile);

      List<TempResult> tr = new List<TempResult>();

      int resIndex = 1;
      foreach (var res in pairedMut)
      {
        var mutCharges = (from r in res[0]
                          orderby r.Spectrum.Charge
                          select r.Spectrum.Charge).Distinct().ToList();

        var pepCounts = GetPepCount(classification, res[0]);

        int peplabel = 0;
        bool bFound = false;
        for (int pepIndex = 1; pepIndex < res.Count; pepIndex++)
        {
          var charges = (from r in res[pepIndex]
                         orderby r.Spectrum.Charge
                         select r.Spectrum.Charge).Distinct().ToList();

          var commonCharges = mutCharges.Intersect(charges).ToList();
          if (commonCharges.Count == 0)
          {
            continue;
          }

          if (!bFound)
          {
            bFound = true;
            resIndex++;
          }

          peplabel++;
          var curMutSpectrum = (from r in res[0]
                                where commonCharges.Contains(r.Spectrum.Charge)
                                orderby r.Spectrum.Score descending
                                select r).First();

          var mutText = mutPepFormat.PeptideFormat.GetString(curMutSpectrum.Spectrum);

          var curOriginalSpectrum = (from r in res[pepIndex]
                                     where r.Spectrum.Charge == curMutSpectrum.Spectrum.Charge
                                     orderby r.Spectrum.Score descending
                                     select r).First();

          var oriPureSeq = curOriginalSpectrum.PureSequence;

          var mutFixSeq = MutationUtils.ReplaceLToI(curMutSpectrum.Sequence, oriPureSeq);
          var mutFixPureSeq = PeptideUtils.GetPureSequence(mutFixSeq);

          int mutationSite = -1;
          string equalsToModification = string.Empty;
          string rnaediting = string.Empty;
          string databaseannotation = string.Empty;

          bool isType1 = mutFixPureSeq.Length == oriPureSeq.Length;
          if (isType1)
          {
            MutationUtils.IsMutationOneIL(mutFixPureSeq, oriPureSeq, ref mutationSite);
            equalsToModification = spTable.GetModification(oriPureSeq[mutationSite], mutFixPureSeq[mutationSite]);
            SnpCode.IsRnaEditing(aas[oriPureSeq[mutationSite]], aas[mutFixPureSeq[mutationSite]], out rnaediting);
          }
          else
          {
            mutationSite = Math.Min(mutFixPureSeq.Length, oriPureSeq.Length) - 1;
          }

          var pepMutation = MyConvert.Format("{0}{1}{2}", oriPureSeq[mutationSite], mutationSite + 1, mutFixPureSeq[mutationSite]);


          List<Sequence> seqs = new List<Sequence>();
          foreach (var p in curOriginalSpectrum.Proteins)
          {
            var ac = acParser.GetValue(p);
            if (!proMap.ContainsKey(ac))
            {
              throw new Exception("Cannot find protein " + p + " in sequence database!");
            }
            seqs.Add(proMap[ac]);
          }

          var proMutations = (from p in curOriginalSpectrum.Proteins
                              let ac = acParser.GetValue(p)
                              let seq = proMap[ac]
                              let pos = seq.SeqString.IndexOf(oriPureSeq)
                              let pmu = MyConvert.Format("{0}{1}{2}", oriPureSeq[mutationSite], pos + mutationSite + 1, mutFixPureSeq[mutationSite])
                              select new { ProteinName = p, Mutation = pmu }).ToList();
          var proMutation = (from pro in proMutations select pro.Mutation).Merge("/");

          if (isType1 && dbAnnotation)
          {
            //sequence variants
            foreach (var pro in proMutations)
            {
              var entry = GetUniprotEntry(pro.ProteinName);
              if (entry == null)
              {
                continue;
              }

              foreach (var sv in entry.SequenceVariants)
              {
                var mut = string.Format("{0}{1}{2}", sv.Original, sv.Position, sv.Variation);
                if (pro.Mutation.Equals(mut))
                {
                  databaseannotation = string.Format("{0}=SequenceVariant {1}", pro.ProteinName, sv.Description);
                  break;
                }
              }

              if (databaseannotation != string.Empty)
              {
                break;
              }
            }

            //sequence conflicts
            if (databaseannotation == string.Empty)
            {
              foreach (var pro in proMutations)
              {
                var entry = GetUniprotEntry(pro.ProteinName);
                if (entry == null)
                {
                  continue;
                }

                foreach (var sv in entry.SequenceConflicts)
                {
                  if ((sv.BeginPosition != sv.EndPosition) || sv.Original.Length != 1)
                  {
                    continue;
                  }

                  var mut = string.Format("{0}{1}{2}", sv.Original, sv.BeginPosition, sv.Variation);
                  if (pro.Mutation.Equals(mut))
                  {
                    databaseannotation = string.Format("{0}=SequenceConflict {1}", pro.ProteinName, sv.Description);
                    break;
                  }
                }

                if (databaseannotation != string.Empty)
                {
                  break;
                }
              }
            }
          }

          List<string> proRefs = seqs.ConvertAll(m => m.Description).ToList();

          int mutationCount;
          var dnaMutation = aas[oriPureSeq[mutationSite]].TransferTo(aas[mutFixPureSeq[mutationSite]], out mutationCount);

          var oriPepCounts = GetPepCount(classification, res[pepIndex]);

          var line = string.Format("${0}-{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}",
            resIndex,
            peplabel,
            mutText,
            mutFixSeq,
            mutFixPureSeq,
            (from p in pepCounts
             select p.ToString()).Merge("\t"),
            pepMutation,
            mutationCount,
            dnaMutation,
            curOriginalSpectrum.Spectrum.Query.FileScan.ShortFileName,
            oriPureSeq,
            curOriginalSpectrum.Proteins.Merge("/"),
            proRefs.Merge("/"),
            (from p in oriPepCounts
             select p.ToString()).Merge("\t"),
            proMutation,
            equalsToModification,
            rnaediting,
            databaseannotation
            );

          tr.Add(new TempResult()
          {
            Line = line,
            PepCount = pepCounts.Sum(),
            OriginalSequence = oriPureSeq
          });
        }

        var groups = tr.GroupBy(m => m.OriginalSequence).ToList();
        groups.Sort((m1, m2) => -m1.Max(n => n.PepCount).CompareTo(m2.Max(n => n.PepCount)));

        List<TempResult> lines = new List<TempResult>();
        groups.ForEach(g => lines.AddRange(from l in g
                                           orderby l.PepCount descending
                                           select l));

        using (StreamWriter sw = new StreamWriter(pairedFile))
        {
          string pepCountHeader = "";
          string originalPepCountHeader = "";
          foreach (var key in ClassificationSet.Keys)
          {
            pepCountHeader = pepCountHeader + "\t" + key + "_PepCount";
            originalPepCountHeader = originalPepCountHeader + "\t" + key + "_OriginalCount";
          }

          sw.WriteLine("Index\t" + mutHeader + "\tSequence\tPureSequence" + pepCountHeader + "\tPepMutation\tDNAMutationCount\tDNAMutation\tOriginalFileScan\tOriginalSequence\tOriginalProteins\tOriginalReferences"
            + originalPepCountHeader + "\tProMutation\tEqualsToModification\tRNA-Editing\tDatabaseAnnotation");

          lines.ForEach(m => sw.WriteLine(m.Line));
        }
      }

      var pairedPeptideFile = pairedFile + ".peptides";
      SavePeptidesFile(pairedMut, format, pairedPeptideFile);

      return pairedPeptideFile;
    }

    private List<int> GetPepCount(IClassification<IIdentifiedPeptide> classification, IEnumerable<IIdentifiedPeptide> curPeps)
    {
      var result = new List<int>();
      var pepGroup = curPeps.GroupBy(m => classification.GetClassification(m));
      foreach (var key in ClassificationSet.Keys)
      {
        int count = 0;
        foreach (var en in pepGroup)
        {
          if (en.Key == key)
          {
            count = en.Count();
            break;
          }
        }

        result.Add(count);
      }

      return result;
    }

    private void SavePeptidesFile(List<List<IGrouping<string, IIdentifiedPeptide>>> pairedMut, MascotPeptideTextFormat pepFormat, string pairedPeptideFile)
    {
      using (StreamWriter sw = new StreamWriter(pairedPeptideFile))
      {
        sw.WriteLine(pepFormat.PeptideFormat.GetHeader() + "\tIndex");
        int resIndex = 0;
        foreach (var res in pairedMut)
        {
          resIndex++;

          foreach (var peps in res)
          {
            var pepgroup = peps.GroupBy(m => m.Sequence + "_" + m.Spectrum.Charge.ToString());
            foreach (var pep in pepgroup)
            {
              var curSpectrum = GetMaxScore(pep);
              sw.WriteLine("{0}\t{1}", pepFormat.PeptideFormat.GetString(curSpectrum.Spectrum), resIndex);
            }
          }
        }
      }
    }

    private IIdentifiedPeptide GetMaxScore(IEnumerable<IIdentifiedPeptide> source, int charge = 0)
    {
      IEnumerable<IIdentifiedPeptide> target;

      if (0 == charge)
      {
        target = source;
      }
      else
      {
        target = (from s in source
                  where s.Spectrum.Charge == charge
                  select s).ToList();
      }

      var maxScore = target.Max(m => m.Spectrum.Score);
      return target.First(m => m.Spectrum.Score == maxScore);
    }
  }
}
