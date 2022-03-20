using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Modification
{
  public static class ModificationUtils
  {
    /// <summary>
    /// 从sequest的out文件中解析修饰定义
    /// </summary>
    /// <param name="line">out文件中包含修饰定义的一行，如下：
    /// <example>(STY* +79.96633) (M# +15.99492) (ST@ -18.00000) C=160.16523  Enzyme:Trypsin(KR) (1)</example>
    /// </param>
    /// <returns></returns>
    public static Dictionary<char, double> ParseFromOutFileLine(string line)
    {
      var result = new Dictionary<char, double>();

      var staticMod = new Regex(@"([A-Z])=(\d+\.{0,1}\d+)");
      var dynamicMod = new Regex(@"\(\S+(\S)\s([\+|-]\d+\.{0,1}\d+)\)");

      Match staticMatch = staticMod.Match(line);
      while (staticMatch.Success)
      {
        result[staticMatch.Groups[1].Value[0]] = MyConvert.ToDouble(staticMatch.Groups[2].Value);
        staticMatch = staticMatch.NextMatch();
      }

      Match dynamicMatch = dynamicMod.Match(line);
      while (dynamicMatch.Success)
      {
        result[dynamicMatch.Groups[1].Value[0]] = MyConvert.ToDouble(dynamicMatch.Groups[2].Value);
        dynamicMatch = dynamicMatch.NextMatch();
      }

      return result;
    }

    /// <summary>
    /// 判断给定字符是否表示修饰。当且仅当给定字符为大写字母（A-Z），才不会被判定为非修饰。也就是说，当给定一个
    /// 特殊字符，例如*,#等，或者给定一个小写字母（a,l,往往代表重标），都会被判定为修饰。
    /// </summary>
    /// <param name="c">给定字符</param>
    /// <returns>是否修饰字符</returns>
    public static bool IsModification(char c)
    {
      return !Char.IsUpper(c);
    }

    public static bool IsModifiedSequence(string sequence, string modifiedAminoacids)
    {
      for (int i = 0; i < sequence.Length; i++)
      {
        if (IsModification(sequence[i]))
        {
          if (char.IsLetter(sequence[i]))
          {
            if (modifiedAminoacids.IndexOf(char.ToUpper(sequence[i])) != -1)
            {
              return true;
            }
          }
          else
          {
            if (i > 0)
            {
              if (modifiedAminoacids.IndexOf(sequence[i - 1]) != -1)
              {
                return true;
              }
            }
          }
        }
      }

      return false;
    }

    /// <summary>
    /// Return zero-based modification site map
    /// </summary>
    /// <param name="modifiedSequence">The sequence without pre and post aminoacids</param>
    /// <returns></returns>
    public static Dictionary<int, char> GetModifiedAminiacids(string modifiedSequence)
    {
      var result = new Dictionary<int, char>();

      int pos = -1;
      for (int index = 0; index < modifiedSequence.Length; index++)
      {
        if (IsModification(modifiedSequence[index]))
        {
          if (index > 0)
          {
            result[pos] = modifiedSequence[index - 1];
          }
        }
        else
        {
          pos++;
        }
      }

      return result;
    }

    private static Regex siteReg = new Regex(@"(\S)\((\d+)\):\s*([\d.]+)");

    /// <summary>
    /// Get site probability from string such like 'S(1): 0.0; S(3): 0.4; S(4): 99.6' from ProteomeDiscoverer MSF file.
    /// </summary>
    /// <param name="siteProbability"></param>
    /// <returns></returns>
    public static List<ModificationSiteProbability> ParseProbability(string siteProbability)
    {
      var result = new List<ModificationSiteProbability>();

      foreach (var s in siteProbability.Split(';'))
      {
        var m = siteReg.Match(s);
        if (!m.Success)
        {
          throw new Exception("Unmatched site probability pattern " + s + " in " + siteProbability);
        }
        result.Add(new ModificationSiteProbability { Aminoacid = m.Groups[1].Value, Site = int.Parse(m.Groups[2].Value), Probability = double.Parse(m.Groups[3].Value) });
      }

      return result;
    }

    /// <summary>
    /// Keep the site probability that the matched amino acid exists in sequence.
    /// </summary>
    /// <param name="sequence">The sequence without pre and post amini acid</param>
    /// <param name="siteProbability">Site probability string from ProteomeDiscoverer MSF file</param>
    /// <returns></returns>
    public static string FilterSiteProbability(string sequence, string siteProbability)
    {
      var siteMap = GetModifiedAminiacids(sequence);

      List<ModificationSiteProbability> probs;
      if (siteProbability.StartsWith("Too many"))
      {
        probs = (from s in siteMap
                 orderby s.Key
                 select new ModificationSiteProbability()
                 {
                   Aminoacid = s.Value.ToString(),
                   Site = s.Key + 1,
                   Probability = 0.0
                 }).ToList();
      }
      else
      {
        probs = ParseProbability(siteProbability).Where(m => siteMap.ContainsKey(m.Site - 1)).ToList();
      }

      return probs.ConvertAll(m => m.ToString()).Merge("; ");
    }
  }
}