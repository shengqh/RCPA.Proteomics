using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RCPA.Seq;
using RCPA.Proteomics.Utils;
using System.Collections.ObjectModel;
using RCPA.Proteomics.Modification;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public delegate void SortSpectrumFunc(List<IIdentifiedSpectrum> spectra);

  public delegate void CalculateQValueFunc(List<IIdentifiedSpectrum> spectra, IScoreFunction scoreFuncs, IFalseDiscoveryRateCalculator fdrCalc);

  public static class IdentifiedSpectrumUtils
  {
    public static Dictionary<string, List<T>> GetRawPeptideMap<T>(IEnumerable<T> peptides) where T : IIdentifiedSpectrum
    {
      Dictionary<string, List<T>> result = new Dictionary<string, List<T>>();
      foreach (T peptide in peptides)
      {
        string raw = peptide.Query.FileScan.Experimental;
        if (!result.ContainsKey(raw))
        {
          result[raw] = new List<T>();
        }
        result[raw].Add(peptide);
      }

      return result;
    }

    public static HashSet<string> GetUniquePeptide<T>(IEnumerable<T> peptides) where T : IIdentifiedSpectrum
    {
      HashSet<string> result = new HashSet<string>();
      foreach (T pep in peptides)
      {
        result.Add(pep.Peptide.PureSequence);
      }
      return result;
    }

    public static int GetUniquePeptideCount<T>(IEnumerable<T> peptides) where T : IIdentifiedSpectrum
    {
      return GetUniquePeptide(peptides).Count;
    }

    public static int GetSpectrumCount<T>(IEnumerable<T> peptides) where T : IIdentifiedSpectrum
    {
      return (from p in peptides
              select p.Query.FileScan.LongFileName).Distinct().Count();
    }

    /// <summary>
    /// 将来自同一个谱图，假设为相同电荷，根据不同搜索条件（例如根据SILAC的重标和轻标）得到的多个鉴定结果合并。
    /// 只保留Score最高的那个，其他的全部抛弃。
    /// 该函数用于进行肽段筛选之前去冗余。
    /// </summary>
    /// <param name="peptides">鉴定谱图列表</param>
    /// <returns></returns>
    public static void KeepTopPeptideFromSameEngineDifferentParameters<T>(List<T> peptides) where T : IIdentifiedSpectrum
    {
      SortByScanChargeScore(peptides);
      int index = 0;
      while (index < peptides.Count - 1)
      {
        //if (!peptides[index].Query.FileScan.ShortFileName.Equals("20111128_CLi_v_4-2k_2mg_TiO2_iTRAQ,4992"))
        //{
        //  index++;
        //  continue;
        //}

        if (peptides[index + 1].Query.FileScan.EqualScanCharge(peptides[index].Query.FileScan))
        {
          peptides.RemoveAt(index + 1);
        }
        else
        {
          index++;
        }
      }
    }

    /// <summary>
    /// 将来自同一个谱图，假设为相同电荷，根据不同搜索条件（例如根据SILAC的重标和轻标）得到的多个鉴定结果比较。
    /// 以Score最高的那个为准，如果其他的跟他一致，则保留，否则抛弃。
    /// 该函数用于进行肽段筛选之前去冗余。
    /// </summary>
    /// <param name="peptides">鉴定谱图列表</param>
    /// <returns></returns>
    public static void KeepUnconflictPeptidesFromSameEngineDifferentParameters<T>(List<T> peptides) where T : IIdentifiedSpectrum
    {
      SortByScanChargeScore(peptides);
      int index = 0;
      while (index < peptides.Count)
      {
        var topSeqs = peptides[index].GetSequencesSet();
        int j = index + 1;
        while (j < peptides.Count)
        {
          if (!peptides[j].Query.FileScan.EqualScanCharge(peptides[index].Query.FileScan))
          {
            index = j;
            break;
          }

          if (peptides[j].Peptides.Any(m => topSeqs.Contains(m.Sequence)))
          {
            j++;
          }
          else
          {
            peptides.RemoveAt(j);
          }
        }

        if (j == peptides.Count)
        {
          break;
        }
      }
    }

    /// <summary>
    /// 过滤同一个谱图，假设为不同电荷，但通过了所有过滤条件的结果，保留分数最高的一个，删除其他的。
    /// 对于同一个谱图，相同价位（例如通过不同搜索条件得到结果），本函数不做处理。
    /// 该函数用于进行肽段筛选之后去冗余。
    /// </summary>
    /// <param name="peptides">鉴定谱图列表</param>
    public static void FilterSameSpectrumWithDifferentCharge<T>(List<T> peptides) where T : IIdentifiedSpectrum
    {
      SortByScanScore(peptides);

      int index = 0;
      while (index < peptides.Count - 1)
      {
        int j = index + 1;

        //找到下一个与index不一样scan的条目j
        while (j < peptides.Count)
        {
          if ((peptides[j].Engine == peptides[index].Engine) && peptides[j].Query.FileScan.EqualScan(peptides[index].Query.FileScan))
          {
            if (peptides[j].Charge == peptides[index].Charge)
            {
              j++;
            }
            else
            {
              peptides.RemoveAt(j);
            }
          }
          else
          {
            break;
          }
        }
        index = j;
      }
    }

    private static bool IsAminoacidDifferent(char a1, char a2)
    {
      if (a1 != a2)
      {
        if ((a1 == 'I' && a2 == 'L') || (a1 == 'L' && a2 == 'I') || (a1 == 'Q' && a2 == 'K') || (a1 == 'K' && a2 == 'Q'))
        {
          return false;
        }
        return true;
      }

      return false;
    }

    /// <summary>
    /// 删除同一个谱图被多个序列不同的肽段匹配的结果，I/L以及Q/K差异算相同序列。
    /// </summary>
    /// <param name="peptides">谱图列表</param>
    public static void RemoveSpectrumWithAmbigiousAssignment<T>(List<T> peptides) where T : IIdentifiedSpectrum
    {
      peptides.RemoveAll(m =>
      {
        if (m.Peptides.Count > 1)
        {
          var pur1 = m.Peptides[0].PureSequence;
          for (int i = 1; i < m.Peptides.Count; i++)
          {
            var pur2 = m.Peptides[i].PureSequence;
            if (pur1.Length != pur2.Length)
            {
              return true;
            }

            for (int j = 0; j < pur1.Length; j++)
            {
              if (IsAminoacidDifferent(pur1[j], pur2[j]))
              {
                return true;
              }
            }
          }
        }

        return false;
      });
    }

    /// <summary>
    /// 过滤同一个谱图被多个序列不同的肽段匹配的结果，查找是否有这些肽段唯一匹配谱图，有则保留其中匹配次数最多的一个（也就是最可能的一个）。
    /// 该函数用于进行肽段筛选之后去冗余。
    /// </summary>
    /// <param name="peptides">鉴定谱图列表</param>
    /// <returns>被删除的谱图列表</returns>
    public static List<IIdentifiedPeptide> FilterSpectrumWithAmbigiousAssignment<T>(List<T> peptides) where T : IIdentifiedSpectrum
    {
      var ambigious = (from p in peptides
                       where p.Peptides.Count > 1
                       select p).ToList();

      Dictionary<string, int> unique = new Dictionary<string, int>();
      foreach (var p in peptides)
      {
        if (p.Peptides.Count == 1)
        {
          var seq = GetSequenceCharge(p.Peptide);
          if (!unique.ContainsKey(seq))
          {
            unique[seq] = 1;
          }
          else
          {
            unique[seq] = unique[seq] + 1;
          }
        }
      }

      List<IIdentifiedPeptide> result = new List<IIdentifiedPeptide>();
      foreach (var ambi in ambigious)
      {
        var uniqueSeqs = ambi.Peptides.ToDictionary(m => m.Sequence, m => GetSequenceCharge(m));

        //完全是修饰或者I/L，Q/K导致的，不考虑
        if (uniqueSeqs.Values.Distinct().Count() == 1)
        {
          continue;
        }

        var dic = (from p in ambi.Peptides
                   let pureSeq = uniqueSeqs[p.Sequence]
                   let a = unique.ContainsKey(pureSeq) ? unique[pureSeq] : 0
                   orderby a descending
                   select new { Pep = p, Count = a }).ToList();

        var counts = (from d in dic
                      select d.Count).Distinct().ToList();

        if (counts.Count == 1)
        {
          if (counts[0] == 0)
          {
            //直接去除该肽段
            result.AddRange(ambi.Peptides);
            peptides.Remove(ambi);
            continue;
          }
          else
          {
            //无法区分，不做处理。
            continue;
          }
        }

        for (int i = 1; i < dic.Count; i++)
        {
          //去除其他的肽段，保留第一个
          result.Add(dic[i].Pep);
          ambi.RemovePeptide(dic[i].Pep);
        }
      }

      return result;
    }

    private static string GetSequenceCharge(IIdentifiedPeptide n)
    {
      return n.PureSequence.Replace("I", "L").Replace("Q", "K") + "_" + n.Spectrum.Charge.ToString();
    }

    /// <summary>
    /// 用蛋白质的AccessNumber取代原来的蛋白名，以避免SEQUEST搜索中产生的相同蛋白质有长短不一样的名字导致的错误
    /// </summary>
    /// <typeparam name="T">鉴定谱图</typeparam>
    /// <param name="spectra">鉴定谱图列表</param>
    /// <param name="proteinAccessNumberParser">蛋白质AccessNumber解析器</param>
    public static void RefineIdentifiedProteinName<T>(List<T> spectra, IStringParser<string> proteinAccessNumberParser) where T : IIdentifiedSpectrum
    {
      foreach (T spectrum in spectra)
      {
        foreach (IIdentifiedPeptide peptide in spectrum.Peptides)
        {
          ReadOnlyCollection<string> proteinNames = peptide.Proteins;
          for (int i = 0; i < proteinNames.Count; i++)
          {
            peptide.SetProtein(i, proteinAccessNumberParser.GetValue(proteinNames[i]));
          }
        }
      }
    }

    /// <summary>
    /// 根据给定分数排序函数以及FDR计算器对鉴定谱图列表计算QValue。
    /// </summary>
    /// <param name="peptides">谱图列表</param>
    /// <param name="scoreFuncs">与分数提取、排序相关类</param>
    /// <param name="fdrCalc">FDR计算器</param>
    public static void CalculateQValue(List<IIdentifiedSpectrum> peptides, IScoreFunction scoreFuncs, IFalseDiscoveryRateCalculator fdrCalc)
    {
      if (peptides.Count == 0)
      {
        return;
      }

      scoreFuncs.SortSpectrum(peptides);

      int totalTarget = 0;
      int totalDecoy = 0;

      HashSet<string> filenames = new HashSet<string>();
      foreach (IIdentifiedSpectrum spectrum in peptides)
      {
        spectrum.QValue = 0.0;
        if (filenames.Contains(spectrum.Query.FileScan.LongFileName))
        {
          continue;
        }
        filenames.Add(spectrum.Query.FileScan.LongFileName);

        if (spectrum.FromDecoy)
        {
          totalDecoy++;
        }
        else
        {
          totalTarget++;
        }
      }

      double lastScore = scoreFuncs.GetScore(peptides[peptides.Count - 1]);
      double lastQvalue = fdrCalc.Calculate(totalDecoy, totalTarget);
      for (int i = peptides.Count - 1; i >= 0; i--)
      {
        double score = scoreFuncs.GetScore(peptides[i]);
        if (score != lastScore)
        {
          lastScore = score;
          lastQvalue = fdrCalc.Calculate(totalDecoy, totalTarget);
          if (lastQvalue == 0.0)
          {
            break;
          }
          peptides[i].QValue = lastQvalue;
        }
        else
        {
          peptides[i].QValue = lastQvalue;
        }

        if (peptides[i].FromDecoy)
        {
          totalDecoy--;
        }
        else
        {
          totalTarget--;
        }
      }
    }

    public static void CalculateUniqueQValue(List<IIdentifiedSpectrum> peptides, IScoreFunction scoreFuncs, IFalseDiscoveryRateCalculator fdrCalc)
    {
      if (peptides.Count == 0)
      {
        return;
      }

      scoreFuncs.SortSpectrum(peptides);

      List<IIdentifiedSpectrum> sameScores = new List<IIdentifiedSpectrum>();
      HashSet<string> targetSeq = new HashSet<string>();
      HashSet<string> decoySeq = new HashSet<string>();

      double lastScore = scoreFuncs.GetScore(peptides[0]);
      for (int i = 0; i < peptides.Count; i++)
      {
        IIdentifiedSpectrum spectrum = peptides[i];
        double score = scoreFuncs.GetScore(peptides[i]);
        if (score == lastScore)
        {
          sameScores.Add(spectrum);
          if (spectrum.FromDecoy)
          {
            decoySeq.Add(spectrum.Peptide.PureSequence);
          }
          else
          {
            targetSeq.Add(spectrum.Peptide.PureSequence);
          }
          continue;
        }
        else
        {
          double qValue = fdrCalc.Calculate(decoySeq.Count, targetSeq.Count);
          foreach (IIdentifiedSpectrum sameScoreSpectrum in sameScores)
          {
            sameScoreSpectrum.QValue = qValue;
          }

          sameScores.Clear();

          lastScore = score;
          sameScores.Add(spectrum);
          if (spectrum.FromDecoy)
          {
            decoySeq.Add(spectrum.Peptide.PureSequence);
          }
          else
          {
            targetSeq.Add(spectrum.Peptide.PureSequence);
          }
          continue;
        }
      }
      double lastQValue = fdrCalc.Calculate(decoySeq.Count, targetSeq.Count);
      foreach (IIdentifiedSpectrum sameScoreSpectrum in sameScores)
      {
        sameScoreSpectrum.QValue = lastQValue;
      }
    }

    /// <summary>
    /// Larger score is better.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spectra"></param>
    public static void SortByScore<T>(List<T> spectra) where T : IIdentifiedSpectrum
    {
      spectra.Sort(delegate(T s1, T s2)
      {
        return s2.Score.CompareTo(s1.Score);
      });
    }

    public static void SortBySpScore<T>(List<T> spectra) where T : IIdentifiedSpectrum
    {
      spectra.Sort(delegate(T s1, T s2)
      {
        return -s1.SpScore.CompareTo(s2.SpScore);
      });
    }

    /// <summary>
    /// Lower qvalue is better
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spectra"></param>
    public static void SortByQValue<T>(List<T> spectra) where T : IIdentifiedSpectrum
    {
      spectra.Sort(delegate(T s1, T s2)
      {
        return s1.QValue.CompareTo(s2.QValue);
      });
    }

    /// <summary>
    /// Lower expect value is better
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spectra"></param>
    public static void SortByExpectValue<T>(List<T> spectra) where T : IIdentifiedSpectrum
    {
      spectra.Sort(delegate(T s1, T s2)
      {
        return s1.ExpectValue.CompareTo(s2.ExpectValue);
      });
    }

    /// <summary>
    /// Larger pvalue is better
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spectra"></param>
    public static void SortByPValue<T>(List<T> spectra) where T : IIdentifiedSpectrum
    {
      spectra.Sort(delegate(T s1, T s2)
      {
        return s2.PValue.CompareTo(s1.PValue);
      });
    }

    public static void SortByScanScore<T>(List<T> spectra) where T : IIdentifiedSpectrum
    {
      spectra.Sort(delegate(T s1, T s2)
      {
        int result = s1.Query.FileScan.Experimental.CompareTo(s2.Query.FileScan.Experimental);
        if (0 != result)
        {
          return result;
        }

        result = s1.Query.FileScan.FirstScan.CompareTo(s2.Query.FileScan.FirstScan);
        if (0 != result)
        {
          return result;
        }

        result = s2.Score.CompareTo(s1.Score);
        if (0 != result)
        {
          return result;
        }

        return s1.Query.FileScan.Charge.CompareTo(s2.Query.FileScan.Charge);
      });
    }

    public static void SortByScanChargeScore<T>(List<T> spectra) where T : IIdentifiedSpectrum
    {
      spectra.Sort(delegate(T s1, T s2)
      {
        int result = s1.Query.FileScan.Experimental.CompareTo(s2.Query.FileScan.Experimental);
        if (0 != result)
        {
          return result;
        }

        result = s1.Query.FileScan.FirstScan.CompareTo(s2.Query.FileScan.FirstScan);
        if (0 != result)
        {
          return result;
        }

        result = s1.Query.FileScan.Charge.CompareTo(s2.Query.FileScan.Charge);
        if (0 != result)
        {
          return result;
        }

        result = s2.Score.CompareTo(s1.Score);
        if (0 != result)
        {
          return result;
        }

        result = s1.ExpectValue.CompareTo(s2.ExpectValue);
        if (0 != result)
        {
          return result;
        }

        result = s2.DeltaScore.CompareTo(s1.DeltaScore);
        if (0 != result)
        {
          return result;
        }

        return string.Compare(s1.Tag, s2.Tag);
      });
    }

    public static List<T> FilterPeptideByDeltaCn<T>(List<T> peptides, double score) where T : IIdentifiedSpectrum
    {
      List<T> result = new List<T>();

      foreach (T phit in peptides)
      {
        if (phit.DeltaScore >= score)
        {
          result.Add(phit);
        }
      }

      return result;
    }

    public static Dictionary<int, List<T>> GetChargeMap<T>(IEnumerable<T> sphs) where T : IIdentifiedSpectrum
    {
      Dictionary<int, List<T>> result = new Dictionary<int, List<T>>();
      foreach (T sph in sphs)
      {
        if (result.ContainsKey(sph.Query.Charge))
        {
          result[sph.Query.Charge].Add(sph);
        }
        else
        {
          List<T> sphList = new List<T>();
          sphList.Add(sph);
          result.Add(sph.Query.Charge, sphList);
        }
      }
      return result;
    }

    public static void ResetProteinByAccessNumberParser<T>(List<T> result, IAccessNumberParser acParser) where T : IIdentifiedSpectrum
    {
      string pro;
      foreach (T spectrum in result)
      {
        foreach (IIdentifiedPeptide peptide in spectrum.Peptides)
        {
          for (int i = 0; i < peptide.Proteins.Count; i++)
          {
            if (acParser.TryParse(peptide.Proteins[i], out pro))
            {
              peptide.SetProtein(i, pro);
            }
          }
        }
      }
    }

    public static MeanStandardDeviation GetDeltaPrecursorPPMAccumulator<T>(IEnumerable<T> sphs) where T : IIdentifiedSpectrum
    {
      var ppms = from pep in sphs
                 let ppm = PrecursorUtils.mz2ppm(pep.ExperimentalMass, pep.TheoreticalMinusExperimentalMass)
                 select ppm;

      int removed = ppms.Count() / 10;
      if (removed > 0)
      {
        var s2 = ppms.OrderBy(m => m).Skip(removed).OrderByDescending(m => m).Skip(removed).ToArray();

        return new MeanStandardDeviation(s2);
      }
      else
      {
        return new MeanStandardDeviation(ppms);
      }
    }

    public static bool SequenceEquals(this IIdentifiedSpectrum m1, IIdentifiedSpectrum m2)
    {
      foreach (var p1 in m1.Peptides)
      {
        foreach (var p2 in m2.Peptides)
        {
          if (p1.PureSequence.Equals(p2.PureSequence))
          {
            return true;
          }
        }
      }

      return false;
    }


    public static IIdentifiedResult DoBuildGroupByPeptide(List<IIdentifiedSpectrum> spectra, Func<IIdentifiedPeptide, string> func)
    {
      IdentifiedResult result = new IdentifiedResult();

      var singlePeptides = (from s in spectra
                            where s.Peptides.Count == 1
                            select s).GroupBy(m => func(m.Peptide));

      var multiplePeptides = (from s in spectra
                              where s.Peptides.Count > 1
                              select s).ToList();

      Dictionary<string, List<IIdentifiedPeptide>> dic = new Dictionary<string, List<IIdentifiedPeptide>>();
      foreach (var g in singlePeptides)
      {
        dic[g.Key] = new List<IIdentifiedPeptide>(from s in g
                                                  select s.Peptide);
      }

      foreach (var o in multiplePeptides)
      {
        var pc = (from p in o.Peptides
                  let c = dic.ContainsKey(func(p)) ? dic[func(p)].Count : 0
                  orderby c descending
                  select p).First();
        if (!dic.ContainsKey(func(pc)))
        {
          dic[func(pc)] = new List<IIdentifiedPeptide>();
        }
        dic[func(pc)].Add(pc);
      }

      var keys = new List<string>(dic.Keys);
      keys.Sort();

      foreach (var key in keys)
      {
        IdentifiedProtein protein = new IdentifiedProtein(key);
        protein.Peptides.AddRange(dic[key]);
        protein.UniquePeptideCount = 1;
        protein.Description = dic[key][0].Proteins.Merge('/');

        IdentifiedProteinGroup group = new IdentifiedProteinGroup();
        group.Add(protein);

        result.Add(group);
      }

      result.BuildGroupIndex();
      //result.Sort();

      return result;
    }

    public static IIdentifiedResult BuildGroupByPeptide(List<IIdentifiedSpectrum> spectra)
    {
      return DoBuildGroupByPeptide(spectra, m => m.Sequence);
    }

    public static IIdentifiedResult BuildGroupByUniquePeptide(List<IIdentifiedSpectrum> spectra)
    {
      return DoBuildGroupByPeptide(spectra, m => m.PureSequence);
    }

    /// <summary>
    /// Fill protein information of each identified spectrum from corresponding protein map file.
    /// The protein map file contains at least two columns, the first is protein name, the second is the peptide sequence
    /// No modification should be included in peptide sequence.
    /// Header of file is required.
    /// </summary>
    /// <param name="curPeptides">Spectrum of interest</param>
    /// <param name="proteinFile">Protein/Peptide map file</param>
    public static void FillProteinInformation(List<IIdentifiedSpectrum> curPeptides, string proteinFile)
    {
      var map = (from line in File.ReadAllLines(proteinFile).Skip(1)
                 let parts = line.Split('\t')
                 select new { Peptide = parts[1], Protein = parts[0] }).ToGroupDictionary(m => m.Peptide);

      foreach (var sph in curPeptides)
      {
        foreach (var pep in sph.Peptides)
        {
          var pureseq = pep.PureSequence;
          if (map.ContainsKey(pureseq))
          {
            var proteins = map[pureseq];
            foreach (var pro in proteins)
            {
              pep.AddProtein(pro.Protein);
            }
          }
        }

        if (sph.Peptides.All(m => m.Proteins.Count == 0))
        {
          throw new Exception(string.Format("Cannot find corresponding protein information of {0}:{1} from {2}",
            sph.Query.FileScan.LongFileName, sph.Peptide.PureSequence, proteinFile));
        }

        for (int i = sph.Peptides.Count - 1; i >= 0; i--)
        {
          if (sph.Peptides[i].Proteins.Count == 0)
          {
            sph.RemovePeptideAt(i);
          }
        }
      }
    }
  }

}
