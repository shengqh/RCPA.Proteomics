using RCPA.Seq;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Database
{
  public class TheoreticalDigestionStatisticCalculator : AbstractThreadFileProcessor
  {
    private List<Protease> proteases;
    private int minLength;
    private double minMass, maxMass;
    private int maxMissCleavage;
    private string ignoreAminoacids;

    public TheoreticalDigestionStatisticCalculator(IEnumerable<Protease> proteases, int minLength, double minMass, double maxMass, int maxMissCleavage, string ignoreAminoacids)
    {
      this.proteases = proteases.ToList();
      this.minLength = minLength;
      this.minMass = minMass;
      this.maxMass = maxMass;
      this.maxMissCleavage = maxMissCleavage;
      this.ignoreAminoacids = ignoreAminoacids;
    }

    /// <summary>
    /// 读取fasta文件，进行数据处理。
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public override IEnumerable<string> Process(string fastaFile)
    {
      HashSet<string> result = new HashSet<string>();

      var ff = new FastaFormat();
      using (StreamReader sr = new StreamReader(fastaFile))
      {
        Progress.SetRange(0, sr.BaseStream.Length);

        var aas = new Aminoacids();

        Predicate<string> aaFilter = m =>
        {
          foreach (var aa in ignoreAminoacids)
          {
            if (m.Contains(aa))
            {
              return false;
            }
          }
          return true;
        };

        Predicate<string> lengthFilter = m => m.Length >= minLength;

        Predicate<string> massFilter = m =>
        {
          var mass = aas.MonoPeptideMass(m);
          return mass >= minMass && mass <= maxMass;
        };

        Predicate<string> filter = m => aaFilter(m) && lengthFilter(m) && massFilter(m);

        List<Digest> digs = new List<Digest>();
        foreach (var protease in proteases)
        {
          var dig = new Digest();
          dig.DigestProtease = protease;
          dig.MaxMissedCleavages = maxMissCleavage;
          digs.Add(dig);
        }

        Sequence seq;
        Progress.SetMessage("Digesting sequences ...");
        while ((seq = ff.ReadSequence(sr)) != null)
        {
          Progress.SetPosition(sr.GetCharpos());

          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          HashSet<string> curseqs = new HashSet<string>();
          curseqs.Add(seq.SeqString);

          foreach (var dig in digs)
          {
            var last = curseqs;
            curseqs = new HashSet<string>();

            foreach (var curseq in last)
            {
              var pro = new Sequence(curseq, curseq);
              dig.ProteinSequence = pro;
              dig.AddDigestFeatures();
              var infos = pro.GetDigestPeptideInfo();

              infos.ForEach(m =>
              {
                if (filter(m.PeptideSeq))
                {
                  curseqs.Add(m.PeptideSeq);
                }
              });
            }
          }

          result.UnionWith(curseqs);
        }
      }

      Progress.SetMessage("Sorting sequences ...");
      var peps = new List<string>(result);
      peps.Sort((m1, m2) =>
      {
        var res = m1.Length.CompareTo(m2.Length);
        if (res == 0)
        {
          res = m1.CompareTo(m2);
        }
        return res;
      });

      var resultFile = fastaFile + ".pep";
      using (StreamWriter sw = new StreamWriter(resultFile))
      {
        peps.ForEach(m => sw.WriteLine(m));
      }

      return new[] { resultFile };
    }
  }
}
