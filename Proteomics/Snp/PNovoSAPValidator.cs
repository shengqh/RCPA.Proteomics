using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace RCPA.Proteomics.Snp
{
  public class PNovoSAPValidator : AbstractThreadProcessor
  {
    private PNovoSAPValidatorOptions options;

    private Aminoacids aas = new Aminoacids();

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

    public PNovoSAPValidator(PNovoSAPValidatorOptions options)
    {
      this.options = options;
      if (this.options.ThreadCount < 1)
      {
        this.options.ThreadCount = 1;
      }
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

        //是否有一个mutation的匹配，不改变酶切位点。
        if (FindMutationOneType1(pnovoseq, ref source, ref site))
        {
          var reference = string.Format("sp|MUL_{0}|type1 source={1} mutation={2}{3}{4}", pnovoseq, source, source[site], site + 1, pnovoseq[site]);
          param.Sequences.Add(new Sequence(reference, pnovoseq));
          param.Type1Count++;
          continue;
        }

        //是否有一个mutation的匹配，从酶切位点突变为其他氨基酸，导致pnovo解析得到序列更长。
        if (FindMutationOneType2(pnovoseq, ref source))
        {
          param.Sequences.Add(new Sequence("sp|MUL_" + pnovoseq + "|type2 source=" + source, pnovoseq));
          param.Type2Count++;
          continue;
        }

        //是否有一个mutation的匹配，从其他氨基酸突变为酶切位点，导致pnovo解析得到序列更短。
        if (FindMutationOneType3(pnovoseq, ref source))
        {
          param.Sequences.Add(new Sequence("sp|MUL_" + pnovoseq + "|type3 source=" + source, pnovoseq));
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

    private Dictionary<string, string> miss0 = new Dictionary<string, string>();
    private Dictionary<string, string> miss1 = new Dictionary<string, string>();
    private Dictionary<string, List<Type2Sequence>> miss1type2_1 = new Dictionary<string, List<Type2Sequence>>();
    private Dictionary<string, List<string>> miss1type2_2 = new Dictionary<string, List<string>>();
    private Dictionary<int, List<string>> miss0group = new Dictionary<int, List<string>>();
    private Dictionary<string, List<string>> miss0type3 = new Dictionary<string, List<string>>();

    /// <summary>
    /// 读取fasta文件，进行数据处理。
    /// </summary>
    /// <param name="fileName">fasta</param>
    /// <returns>result file</returns>
    public override IEnumerable<string> Process()
    {
      HashSet<string> pnovoseqs = new HashSet<string>();

      var pnovoParser = new PNovoPlusParser(options.TitleParser);
      pnovoParser.Progress = this.Progress;

      //找到一个非酶切位点的氨基酸，可代表denovo序列前后氨基酸。
      var anotheraa = 'A';
      for (int i = 0; i < 26; i++)
      {
        anotheraa = (char)('A' + i);
        if (options.Enzyme.CleaveageResidues.Contains(anotheraa) || options.Enzyme.NotCleaveResidues.Contains(anotheraa))
        {
          continue;
        }
        break;
      }

      Progress.SetRange(0, options.PnovoFiles.Length);
      int totalSpectrumCount = 0;
      int totalSpectrumPassScore = 0;

      foreach (var pnovoFile in options.PnovoFiles)
      {
        Progress.SetMessage("Reading " + pnovoFile + " ...");
        int spectrumCount = pnovoParser.GetSpectrumCount(pnovoFile);
        var curSpectra = pnovoParser.ParsePeptides(pnovoFile, 10, options.MinScore);

        totalSpectrumCount += spectrumCount;
        totalSpectrumPassScore += curSpectra.Count;

        RemoveMissCleavagePeptides(anotheraa, curSpectra);

        pnovoseqs.UnionWith(from c in curSpectra
                            from p in c.Peptides
                            select p.PureSequence);
        Progress.Increment(1);
      }


      var pNovoStat = Path.Combine(options.TargetDirectory, "pNovo.SAP.stat");
      using (StreamWriter sw = new StreamWriter(pNovoStat))
      {
        sw.WriteLine("Total Spectrum Count\t" + totalSpectrumCount.ToString());
        sw.WriteLine("Total Peptide-Spectrum-Match Passed Score Filter\t" + totalSpectrumPassScore.ToString());
      }

      Progress.SetPosition(0);
      Progress.SetMessage("Reading " + options.TargetFastaFile + " ...");
      var seqs = SequenceUtils.Read(new FastaFormat(), options.TargetFastaFile);

      Progress.SetMessage("Digesting sequences ...");

      GetDigestPeptide(seqs);

      seqs.Clear();
      seqs.TrimExcess();
      GC.Collect();
      GC.WaitForFullGCComplete();

      //清除所有跟理论库一样的肽段。
      Progress.SetMessage("Removing identical peptides ...");
      pnovoseqs.ExceptWith(miss0.Keys);

      var pnovoArray = pnovoseqs.ToArray();
      pnovoseqs.Clear();
      GC.Collect();
      GC.WaitForFullGCComplete();

      miss0group = miss0.Keys.ToGroupDictionary(m => m.Length);

      var type2seqs = new List<Type2Sequence>();
      var type2_2 = new List<string>();
      foreach (var m in miss1.Keys)
      {
        int maxpos = -1;
        for (int i = 1; i < m.Length; i++)
        {
          if (options.Enzyme.IsCleavageSite(m[i - 1], m[i], anotheraa))
          {
            maxpos = i - 1;
            break;
          }
        }

        if (maxpos == -1)
        {
          throw new Exception("There is no misscleavage in " + m);
        }

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

      miss0type3 = miss0.Keys.ToGroupDictionary(m => GetType3Key(m));

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
      var binSize = totalCount / options.ThreadCount;
      List<FindParam> fparams = new List<FindParam>();
      List<Thread> threads = new List<Thread>();
      var startPos = 0;
      for (int i = 0; i < options.ThreadCount; i++)
      {
        int count;
        if (i == options.ThreadCount - 1)
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

      string newFastaFile = new FileInfo(options.TargetDirectory + "/" + FileUtils.ChangeExtension(new FileInfo(options.DatabaseFastaFile).Name, "mutation.fasta")).FullName;
      using (StreamWriter sw = new StreamWriter(newFastaFile))
      {
        using (StreamReader sr = new StreamReader(options.DatabaseFastaFile))
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

      Progress.SetRange(0, options.PnovoFiles.Length);
      var sapSequences = new HashSet<string>(singleMutation.ConvertAll(m => m.SeqString));
      List<IIdentifiedSpectrum> allSpectra = new List<IIdentifiedSpectrum>();
      foreach (var pnovoFile in options.PnovoFiles)
      {
        Progress.SetMessage("Reading " + pnovoFile + " ...");
        var curSpectra = pnovoParser.ParsePeptides(pnovoFile, 10, options.MinScore);

        RemoveMissCleavagePeptides(anotheraa, curSpectra);

        curSpectra.RemoveAll(m => !m.Peptides.Any(n => sapSequences.Contains(n.PureSequence)));
        allSpectra.AddRange(curSpectra);
        Progress.Increment(1);
      }

      var pNovoPeptides = Path.Combine(options.TargetDirectory, "pNovo.SAP.peptides");
      new MascotPeptideTextFormat("\tFileScan\tSequence\tCharge\tScore\tDeltaScore").WriteToFile(pNovoPeptides, allSpectra);

      Progress.SetMessage("Finished.");
      Progress.End();

      return new string[] { newFastaFile };
    }

    private void RemoveMissCleavagePeptides(char anotheraa, List<IIdentifiedSpectrum> curSpectra)
    {
      foreach (var spectrum in curSpectra)
      {
        //先设置denovo结果肽段的前后氨基酸，以防止末端氨基酸不是酶切位点的情况。
        var pureseq = spectrum.Peptide.PureSequence;
        spectrum.NumMissedCleavages = options.Enzyme.GetMissCleavageSiteCount(pureseq);

        if (options.Enzyme.IsEndoProtease && !options.Enzyme.IsCleavageSite(pureseq.Last(), anotheraa, '-'))
        {
          spectrum.NumMissedCleavages++;
        }
        else if (!options.Enzyme.IsEndoProtease && !options.Enzyme.IsCleavageSite(anotheraa, pureseq.First(), '-'))
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

      if (this.options.Enzyme.CleaveageResidues.Contains(c))
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
            source = miss0[m];
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
            source = miss1[m.Sequence];
            //Console.WriteLine("TYPE2 : {0} ==> {1}", source, pnovoseq);
            return true;
          }
        }
      }

      var subs = pnovoseq.Substring(1);
      if (miss1type2_2.ContainsKey(subs))
      {
        source = miss1[miss1type2_2[subs][0]];
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
        if (MutationUtils.IsMutationOne2(m, pnovoseq, ref site, options.IgnoreNtermMutation, options.IgnoreDeamidatedMutation, options.IgnoreMultipleNucleotideMutation))
        {
          source = miss0[m];
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

    private void GetDigestPeptide(List<Sequence> seqs)
    {
      miss0 = new Dictionary<string, string>();
      miss1 = new Dictionary<string, string>();

      foreach (var seq in seqs)
      {
        Digest dig = new Digest();
        dig.DigestProtease = options.Enzyme;
        dig.ProteinSequence = seq;
        dig.MaxMissedCleavages = 1;
        dig.AddDigestFeatures();
        var features = seq.GetDigestPeptideInfo();
        foreach (var feature in features)
        {
          if (feature.PeptideSeq.Length < options.MinLength)
          {
            continue;
          }

          var pepseq = feature.PeptideSeq.Replace('I', 'L');
          if (feature.MissCleavage == 0)
          {
            miss0[pepseq] = feature.PeptideSeq;
          }
          else
          {
            miss1[pepseq] = feature.PeptideSeq;
          }
        }
      }
    }
  }
}
