using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Snp
{
  public static class SnpCode
  {
    private static int TransferTo(string from, string to)
    {
      int result = 0;

      for (int i = 0; i < 3; i++)
      {
        if (from[i] != to[i])
          result++;
      }

      return result;
    }

    public static string TransferTo(this Aminoacid from, Aminoacid to, out int mutationCount)
    {
      var fromCodes = from.Codes;
      var toCodes = to.Codes;
      if (fromCodes.Length == 0 || toCodes.Length == 0)
      {
        mutationCount = int.MaxValue;
        return string.Empty;
      }

      var lst = (from f in fromCodes
                 from t in toCodes
                 let m = TransferTo(f, t)
                 orderby m
                 select new { From = f, To = t, TransCost = m }).ToList();

      var min = (from l in lst
                 where l.TransCost == lst[0].TransCost
                 select l).ToList();

      mutationCount = min[0].TransCost;

      return (from m in min
              let r = MyConvert.Format("{0}->{1}", m.From, m.To)
              select r).Merge(" ! ");
    }

    private static Aminoacids aas = new Aminoacids();
    private static Dictionary<Pair<char, char>, string> RnaEditingMap = null;

    public static bool IsRnaEditing(this char from, char to, out string mutationStr)
    {
      return aas[from].IsRnaEditing(aas[to], out mutationStr);
    }

    public static bool IsRnaEditing(this Aminoacid from, Aminoacid to, out string mutationStr)
    {
      if (RnaEditingMap == null)
      {
        PrepareRnaEditingMap();
      }

      var pair = new Pair<char, char>(from.OneName, to.OneName);
      if (RnaEditingMap.ContainsKey(pair))
      {
        mutationStr = RnaEditingMap[pair];
        return true;
      }

      mutationStr = string.Empty;
      return false;
    }

    private static void PrepareRnaEditingMap()
    {
      RnaEditingMap = new Dictionary<Pair<char, char>, string>();

      var aasstr = aas.GetVisibleAminoacids();
      for (int i = 0; i < aasstr.Length; i++)
      {
        for (int j = 0; j < aasstr.Length; j++)
        {
          if (i == j)
          {
            continue;
          }

          string mutationstr = null;

          if (DoIsRnaEditing(aas[aasstr[i]], aas[aasstr[j]], ref mutationstr))
          {
            RnaEditingMap[new Pair<char, char>(aasstr[i], aasstr[j])] = mutationstr;
          }
        }
      }
    }

    private static bool DoIsRnaEditing(Aminoacid from, Aminoacid to, ref string mutationstr)
    {
      var fromCodes = from.Codes;

      var toCodes = to.Codes;

      var lst = (from f in fromCodes
                 from t in toCodes
                 let m = TransferTo(f, t)
                 where m == 1
                 select new { From = f, To = t, TransCost = m }).ToList();

      foreach (var m in lst)
      {
        for (int i = 0; i < m.From.Length; i++)
        {
          //if (m.From[i] == 'T' && m.To[i] == 'C')
          if (m.From[i] == 'A' && m.To[i] == 'G')
          {
            mutationstr = string.Format("{0}->{1}", m.From, m.To);
            return true;
          }
        }
      }

      return false;
    }
  }
}
