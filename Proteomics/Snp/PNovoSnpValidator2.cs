using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.PFind;
using RCPA.Seq;
using System.Text.RegularExpressions;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Mascot;
using System.Threading;

namespace RCPA.Proteomics.Snp
{
  public enum MutationType
  {
    stNot_KR_Mutation, //是否有一个mutation的匹配，非KR突变
    stKR_To_Other, //是否有一个mutation的匹配，从KR突变为其他氨基酸，导致pnovo解析得到序列更长 
    stOther_To_KR //是否有一个mutation的匹配，从其他氨基酸突变为KR，导致pnovo解析得到序列更短。
  };

  public class ProteinLoc
  {
    public string PeptideSequence { get; set; }

    public string ProteinName { get; set; }

    public string Mutation { get; set; }
  }

  public class ProteinSeq
  {
    public ProteinSeq()
    {
      Locations = new List<ProteinLoc>();
    }

    public string ILReplacedSequence { get; set; }

    public List<ProteinLoc> Locations { get; private set; }
  }

  public class DenovoSeq
  {
    public DenovoSeq()
    {
      this.Proteins = new List<ProteinSeq>();
    }

    public string Sequence { get; set; }

    public MutationType SAPType { get; set; }

    public List<ProteinSeq> Proteins { get; private set; }
  }

  public class PNovoSnpValidator2 : AbstractThreadFileProcessor
  {
    private string[] pnovoFiles;
    private string targetFastaFile;
    private string dbFastaFile;
    private ITitleParser parser;
    private IAccessNumberParser acParser;
    private Protease protease;
    private double minScore;
    private int threadCount;

    private Aminoacids aas = new Aminoacids();

    public bool IgnoreNtermMutation { get; set; }

    public bool IgnoreDeamidatedMutation { get; set; }

    public bool IgnoreMultipleNucleotideMutation { get; set; }

    private class FindParam
    {
      public FindParam()
      {
        this.Type1Count = 0;
        this.Type2Count = 0;
        this.Type3Count = 0;
        this.FinishedCount = 0;
        this.Sequences = new List<Sequence>();
      }

      public int Type1Count { get; set; }

      public int Type2Count { get; set; }

      public int Type3Count { get; set; }

      public List<string> PnovoSeqs { get; set; }

      public List<Sequence> Sequences { get; set; }

      public int FinishedCount { get; set; }
    }

    public PNovoSnpValidator2(string[] pnovoFiles, string targetFastaFile, string dbFastaFile, ITitleParser parser, IAccessNumberParser acParser, Protease protease, double minScore, int threadCount)
    {
      this.pnovoFiles = pnovoFiles;
      this.targetFastaFile = targetFastaFile;
      this.dbFastaFile = dbFastaFile;
      this.parser = parser;
      this.acParser = acParser;
      this.protease = protease;
      this.minScore = minScore;
      this.threadCount = threadCount;
      if (this.threadCount < 1)
      {
        this.threadCount = 1;
      }

      IgnoreNtermMutation = true;
      IgnoreDeamidatedMutation = false;
      IgnoreMultipleNucleotideMutation = true;
    }

    private void FindMutation(object obj)
    {
      FindParam param = (FindParam)obj;
      for (int i = 0; i < param.PnovoSeqs.Count; i++)
      {
        if (Progress.IsCancellationPending())
        {
          return;
        }

        param.FinishedCount++;

        var pnovoseq = param.PnovoSeqs[i];
        string source = string.Empty;
        int site = -1;

        //是否有一个mutation的匹配，非KR突变。
        if (FindMutationOneType1(pnovoseq, ref source, ref site))
        {
          var reference = string.Format("MUL_{0}_type1 source={1} mutation={2}{3}{4}", pnovoseq, source, source[site], site + 1, pnovoseq[site]);
          param.Sequences.Add(new Sequence(reference, pnovoseq));
          param.Type1Count++;
          continue;
        }

        //是否有一个mutation的匹配，从KR突变为其他氨基酸，导致pnovo解析得到序列更长。
        if (FindMutationOneType2(pnovoseq, ref source))
        {
          param.Sequences.Add(new Sequence("MUL_" + pnovoseq + "_type2 source=" + source, pnovoseq));
          param.Type2Count++;
          continue;
        }

        //是否有一个mutation的匹配，从其他氨基酸突变为KR，导致pnovo解析得到序列更短。
        if (FindMutationOneType3(pnovoseq, ref source))
        {
          param.Sequences.Add(new Sequence("MUL_" + pnovoseq + "_type3 source=" + source, pnovoseq));
          param.Type3Count++;
          continue;
        }
      }
    }

    private class Type2Sequence
    {
      public string Sequence { get; set; }

      public string PriorSequence { get; set; }

      public string PostSequence { get; set; }

      public bool Match(string pnovo)
      {
        return pnovo.StartsWith(PriorSequence) && pnovo.EndsWith(PostSequence);
      }
    }

    private List<string> miss0 = new List<string>();
    private List<string> miss1 = new List<string>();
    private Dictionary<string, List<Type2Sequence>> miss1type2_1 = new Dictionary<string, List<Type2Sequence>>();
    private Dictionary<string, List<string>> miss1type2_2 = new Dictionary<string, List<string>>();
    private Dictionary<int, List<string>> miss0group = new Dictionary<int, List<string>>();
    private Dictionary<string, List<string>> miss0type3 = new Dictionary<string, List<string>>();

    /// <summary>
    /// 读取fasta文件，进行数据处理。
    /// </summary>
    /// <param name="fileName">fasta</param>
    /// <returns>result file</returns>
    public override IEnumerable<string> Process(string targetDir)
    {
      HashSet<string> pnovoseqs = new HashSet<string>();

      PNovoParser pnovoParser = new PNovoParser(this.parser);
      pnovoParser.Progress = this.Progress;

      //找到一个非酶切位点的氨基酸，可代表denovo序列前后氨基酸。
      var anotheraa = 'A';
      for (int i = 0; i < 26; i++)
      {
        anotheraa = (char)('A' + i);
        if (protease.CleaveageResidues.Contains(anotheraa) || protease.NotCleaveResidues.Contains(anotheraa))
        {
          continue;
        }
        break;
      }

      Progress.SetRange(0, pnovoFiles.Length);
      int totalSpectrumCount = 0;
      int totalSpectrumPassScore = 0;

      foreach (var pnovoFile in pnovoFiles)
      {
        Progress.SetMessage("Reading " + pnovoFile + " ...");
        int spectrumCount = pnovoParser.GetSpectrumCount(pnovoFile);
        var curSpectra = pnovoParser.ParsePeptides(pnovoFile, 10, minScore);

        totalSpectrumCount += spectrumCount;
        totalSpectrumPassScore += curSpectra.Count;

        RemoveMissCleavagePeptides(anotheraa, curSpectra);

        pnovoseqs.UnionWith(from c in curSpectra
                            from p in c.Peptides
                            select p.PureSequence);
        Progress.Increment(1);
      }


      var pNovoStat = targetDir + "\\pNovo.SAP.stat";
      using (StreamWriter sw = new StreamWriter(pNovoStat))
      {
        sw.WriteLine("Total Spectrum Count\t" + totalSpectrumCount.ToString());
        sw.WriteLine("Total Peptide-Spectrum-Match Passed Score Filter\t" + totalSpectrumPassScore.ToString());
      }

      Progress.SetPosition(0);
      Progress.SetMessage("Reading " + targetFastaFile + " ...");
      var seqs = SequenceUtils.Read(new FastaFormat(), targetFastaFile);

      Progress.SetMessage("Digesting sequences ...");
      miss0 = new List<string>();
      miss1 = new List<string>();

      GetDigestPeptide(seqs, miss0, miss1);

      seqs.Clear();
      seqs.TrimExcess();
      GC.Collect();
      GC.WaitForFullGCComplete();

      //清除所有跟理论库一样的肽段。
      Progress.SetMessage("Removing identical peptides ...");
      pnovoseqs.ExceptWith(miss0);

      var pnovoArray = pnovoseqs.ToArray();
      pnovoseqs.Clear();
      GC.Collect();
      GC.WaitForFullGCComplete();

      miss0group = miss0.ToGroupDictionary(m => m.Length);

      var type2seqs = new List<Type2Sequence>();
      var type2_2 = new List<string>();
      foreach (var m in miss1)
      {
        var kpos = m.IndexOf('K');
        var rpos = m.IndexOf('R');
        var maxpos = Math.Max(kpos, rpos);

        if (maxpos == 0)
        {
          type2_2.Add(m);
        }
        else
        {
          type2seqs.Add(new Type2Sequence()
          {
            Sequence = m,
            PriorSequence = m.Substring(0, maxpos),
            PostSequence = m.Substring(maxpos + 1)
          });
        }
      }
      miss1type2_1 = type2seqs.ToGroupDictionary(m => GetType2Key(m.Sequence));
      miss1type2_2 = type2_2.ToGroupDictionary(m => m.Substring(1));

      miss0type3 = miss0.ToGroupDictionary(m => GetType3Key(m));

      type2seqs.Clear();
      GC.Collect();
      GC.WaitForFullGCComplete();

      Progress.SetMessage("Finding mutation ...");
      Progress.SetRange(0, pnovoArray.Length);

      var pre100 = pnovoArray.Length / 100;
      var pre10000 = pnovoArray.Length / 10000;
      if (pre10000 == 0)
      {
        pre10000 = 1;
      }

      var totalCount = pnovoArray.Length;
      var binSize = totalCount / this.threadCount;
      List<FindParam> fparams = new List<FindParam>();
      List<Thread> threads = new List<Thread>();
      var startPos = 0;
      for (int i = 0; i < this.threadCount; i++)
      {
        int count;
        if (i == this.threadCount - 1)
        {
          count = pnovoArray.Length - startPos;
        }
        else
        {
          count = binSize;
        }
        List<string> binSeq = new List<string>();
        binSeq.AddRange(pnovoArray.Skip(startPos).Take(count));
        startPos = startPos + count;

        var aparam = new FindParam()
        {
          PnovoSeqs = binSeq
        };
        fparams.Add(aparam);

        Thread at = new Thread(this.FindMutation);
        threads.Add(at);
        at.IsBackground = true;
        at.Start(aparam);
      }

      pnovoArray = null;
      GC.Collect();
      GC.WaitForFullGCComplete();

      var startTime = DateTime.Now;

      Progress.SetRange(0, totalCount);
      while (true)
      {
        Thread.Sleep(1000);

        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        int finishedCount = fparams.Sum(m => m.FinishedCount);
        Progress.SetPosition(finishedCount);

        if (finishedCount == 0)
        {
          continue;
        }

        var curTime = DateTime.Now;
        var costTime = curTime - startTime;
        var totalCostTime = new TimeSpan(costTime.Ticks * totalCount / finishedCount);
        var finishTime = curTime + new TimeSpan(costTime.Ticks * (totalCount - finishedCount) / finishedCount);

        StringBuilder costFormat = new StringBuilder();
        if (totalCostTime.TotalHours >= 2.0)
        {
          costFormat.Append(Math.Truncate(totalCostTime.TotalHours).ToString() + " hours and ");
        }
        else if (totalCostTime.TotalHours >= 1.0)
        {
          costFormat.Append("one hour and ");
        }
        costFormat.Append(totalCostTime.Minutes.ToString() + " minutes");

        Progress.SetMessage("Finding mutation {0} / {1}, will cost {2} and finish at {3} ...", finishedCount, totalCount, costFormat, finishTime);

        int finishedThreadCount = threads.Count(m => !m.IsAlive);
        if (finishedThreadCount == threads.Count)
        {
          break;
        }
      }

      int type1 = fparams.Sum(m => m.Type1Count);
      int type2 = fparams.Sum(m => m.Type2Count);
      int type3 = fparams.Sum(m => m.Type3Count);

      using (StreamWriter sw = new StreamWriter(pNovoStat, true))
      {
        sw.WriteLine("Type1 Count\t" + type1.ToString());
        sw.WriteLine("Type2 Count\t" + type2.ToString());
        sw.WriteLine("Type3 Count\t" + type3.ToString());
      }

      var singleMutation = (from f in fparams
                            from s in f.Sequences
                            select s).ToList();

      string newFastaFile = new FileInfo(targetDir + "/" + FileUtils.ChangeExtension(new FileInfo(dbFastaFile).Name, "mutation.fasta")).FullName;
      using (StreamWriter sw = new StreamWriter(newFastaFile))
      {
        using (StreamReader sr = new StreamReader(dbFastaFile))
        {
          string line = sr.ReadToEnd();
          sw.WriteLine(line);

          foreach (var seq in singleMutation)
          {
            sw.WriteLine(">" + seq.Reference);
            sw.WriteLine(seq.SeqString);
          }
        }
      }

      Progress.SetRange(0, pnovoFiles.Length);
      var sapSequences = new HashSet<string>(singleMutation.ConvertAll(m => m.SeqString));
      List<IIdentifiedSpectrum> allSpectra = new List<IIdentifiedSpectrum>();
      foreach (var pnovoFile in pnovoFiles)
      {
        Progress.SetMessage("Reading " + pnovoFile + " ...");
        var curSpectra = pnovoParser.ParsePeptides(pnovoFile, 10, minScore);

        RemoveMissCleavagePeptides(anotheraa, curSpectra);

        curSpectra.RemoveAll(m => !m.Peptides.Any(n => sapSequences.Contains(n.PureSequence)));
        allSpectra.AddRange(curSpectra);
        Progress.Increment(1);
      }

      var pNovoPeptides = targetDir + "\\pNovo.SAP.peptides";
      new MascotPeptideTextFormat().WriteToFile(pNovoPeptides, allSpectra);

      var matchSeqs = BuildMatchedSeq(singleMutation, dbFastaFile);

      Progress.SetMessage("Finished.");
      Progress.End();

      return new string[] { newFastaFile };
    }

    private object BuildMatchedSeq(List<Sequence> singleMutation, string dbFastaFile)
    {
      throw new NotImplementedException();
    }

    private void RemoveMissCleavagePeptides(char anotheraa, List<IIdentifiedSpectrum> curSpectra)
    {
      foreach (var spectrum in curSpectra)
      {
        //先设置denovo结果肽段的前后氨基酸，以防止末端氨基酸不是酶切位点的情况。
        var pureseq = spectrum.Peptide.PureSequence;
        spectrum.NumMissedCleavages = protease.GetMissCleavageSiteCount(pureseq);

        if (protease.IsEndoProtease && !protease.IsCleavageSite(pureseq.Last(), anotheraa, '-'))
        {
          spectrum.NumMissedCleavages++;
        }
        else if (!protease.IsEndoProtease && !protease.IsCleavageSite(anotheraa, pureseq.First(), '-'))
        {
          spectrum.NumMissedCleavages++;
        }
      }
      curSpectra.RemoveAll(m => m.NumMissedCleavages > 0);
    }

    private static string GetType3Key(string m)
    {
      return m.Substring(0, 2);
    }

    private static string GetType2Key(string m)
    {
      return MyConvert.Format("{0}{1}{2}", m[0], m.Length, m[m.Length - 1]);
    }

    private bool FindMutationOneType3(string pnovoseq, ref string source)
    {
      var c = pnovoseq.Last();

      if (c == 'K' || c == 'R')
      {
        var key = GetType3Key(pnovoseq);
        if (!miss0type3.ContainsKey(key))
        {
          return false;
        }

        var values = miss0type3[key];
        var p3 = pnovoseq.Substring(0, pnovoseq.Length - 1);
        foreach (var m in values)
        {
          if (m.StartsWith(p3))
          {
            source = m;
            //Console.WriteLine("TYPE3 : {0} ==> {1}", m, pnovoseq);
            return true;
          }
        }
      }

      return false;
    }

    private bool FindMutationOneType2(string pnovoseq, ref string source)
    {
      var key = GetType2Key(pnovoseq);
      if (miss1type2_1.ContainsKey(key))
      {
        var type2 = miss1type2_1[key];
        foreach (var m in type2)
        {
          if (pnovoseq.StartsWith(m.PriorSequence) && pnovoseq.EndsWith(m.PostSequence))
          {
            source = m.Sequence;
            //Console.WriteLine("TYPE2 : {0} ==> {1}", source, pnovoseq);
            return true;
          }
        }
      }

      var subs = pnovoseq.Substring(1);
      if (miss1type2_2.ContainsKey(subs))
      {
        source = miss1type2_2[subs][0];
        //Console.WriteLine("TYPE2 : {0} ==> {1}", source, pnovoseq);
        return true;
      }

      return false;
    }

    private bool FindMutationOneType1(string pnovoseq, ref string source, ref int site)
    {
      if (!miss0group.ContainsKey(pnovoseq.Length))
      {
        return false;
      }

      var lst = miss0group[pnovoseq.Length];
      foreach (var m in lst)
      {
        if (MutationUtils.IsMutationOne2(m, pnovoseq, ref site, IgnoreNtermMutation, IgnoreDeamidatedMutation, IgnoreMultipleNucleotideMutation))
        {
          source = m;
          //Console.WriteLine("TYPE1 : {0} ==> {1}", m, pnovoseq);
          return true;
        }
      }

      return false;
    }

    private bool FindIdentical(string pnovoseq, Dictionary<int, List<string>> miss0group)
    {
      if (!miss0group.ContainsKey(pnovoseq.Length))
      {
        return false;
      }

      return miss0group[pnovoseq.Length].Contains(pnovoseq);
    }

    private void GetDigestPeptide(List<Sequence> seqs, List<string> miss0, List<string> miss1)
    {
      miss0.Clear();
      miss1.Clear();

      var m0 = new HashSet<string>();
      var m1 = new HashSet<string>();

      foreach (var seq in seqs)
      {
        Digest dig = new Digest();
        dig.DigestProtease = this.protease;
        dig.ProteinSequence = seq;
        dig.MaxMissedCleavages = 1;
        dig.AddDigestFeatures();
        var features = seq.GetDigestPeptideInfo();

        features.RemoveAll(m => m.PeptideSeq.Length < 6);

        foreach (var feature in features)
        {
          var pepseq = feature.PeptideSeq.Replace('I', 'L');
          if (feature.MissCleavage == 0)
          {
            m0.Add(pepseq);
          }
          else
          {
            m1.Add(pepseq);
          }
        }
      }

      miss0.AddRange(m0);
      miss1.AddRange(m1);
    }
  }
}
