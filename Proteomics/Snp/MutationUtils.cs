using RCPA.Proteomics.Utils;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Snp
{
  public static class MutationUtils
  {
    private static Aminoacids aas = new Aminoacids();

    private delegate bool IsMutationOneDelegate(string fromPeptide, string toPeptide, ref int mutationSite);

    public static bool IsAminoacidEquals(this char a, char b)
    {
      if (a == b)
      {
        return true;
      }
      if (a == 'L' && b == 'I')
      {
        return true;
      }

      if (a == 'I' && b == 'L')
      {
        return true;
      }

      return false;
    }

    public static unsafe bool IsMutationOne(string fromPeptide, string toPeptide)
    {
      int len = fromPeptide.Length;
      if (len != toPeptide.Length)
      {
        return false;
      }

      fixed (char* p1 = fromPeptide)
      fixed (char* p2 = toPeptide)
      {
        for (int i = 0; i < len; i++)
        {
          if (p1[i] != p2[i])
          {
            for (int j = i + 1; j < len; j++)
            {
              if (p1[j] != p2[j])
              {
                return false;
              }
            }
            return true;
          }
        }
      }

      return false;
    }


    /// <summary>
    /// 是否是单氨基酸突变。
    /// </summary>
    /// <param name="fromPeptide"></param>
    /// <param name="toPeptide"></param>
    /// <param name="mutationSite"></param>
    /// <returns></returns>
    public static unsafe bool IsMutationOne(string fromPeptide, string toPeptide, ref int mutationSite)
    {
      int len = fromPeptide.Length;
      if (len != toPeptide.Length)
      {
        return false;
      }

      fixed (char* p1 = fromPeptide)
      fixed (char* p2 = toPeptide)
      {
        for (int i = 0; i < len; i++)
        {
          if (p1[i] != p2[i])
          {
            for (int j = i + 1; j < len; j++)
            {
              if (p1[j] != p2[j])
              {
                return false;
              }
            }
            mutationSite = i;
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// 是否是除了I/L突变之外的单氨基酸突变。
    /// </summary>
    /// <param name="fromPeptide"></param>
    /// <param name="toPeptide"></param>
    /// <param name="mutationSite"></param>
    /// <returns></returns>
    public static unsafe bool IsMutationOneIL(string fromPeptide, string toPeptide, ref int mutationSite)
    {
      mutationSite = -1;

      int len = fromPeptide.Length;
      if (len != toPeptide.Length)
      {
        return false;
      }

      fixed (char* p1 = fromPeptide)
      fixed (char* p2 = toPeptide)
      {
        for (int i = 0; i < len; i++)
        {
          if (!p1[i].IsAminoacidEquals(p2[i]))
          {
            for (int j = i + 1; j < len; j++)
            {
              if (!p1[j].IsAminoacidEquals(p2[j]))
              {
                return false;
              }
            }
            mutationSite = i;
            return true;
          }
        }
      }

      return false;
    }

    private static bool DoIsMutationOne(IsMutationOneDelegate func, string fromPeptide, string toPeptide, ref int mutationSite, bool ignoreNtermMutation, bool ignoreDeamidatedMutation, bool ignoreMultipleNucleotideMutation)
    {
      if (func(fromPeptide, toPeptide, ref mutationSite))
      {
        if (ignoreNtermMutation && 0 == mutationSite)
        {
          return false;
        }

        if (ignoreDeamidatedMutation && MutationUtils.IsDeamidatedMutation(fromPeptide[mutationSite], toPeptide[mutationSite]))
        {
          return false;
        }

        if (ignoreMultipleNucleotideMutation && IsSingleNucleotideMutation(fromPeptide[mutationSite], toPeptide[mutationSite]))
        {
          return false;
        }

        return true;
      }
      return false;
    }

    public static bool IsSingleNucleotideMutation(char fromAA, char toAA)
    {
      int mutationCount;
      var dnaMutation = aas[fromAA].TransferTo(aas[toAA], out mutationCount);
      return mutationCount == 1;
    }

    /// <summary>
    /// 判断是否单氨基酸突变。
    /// </summary>
    /// <param name="fromPeptide">原始序列</param>
    /// <param name="toPeptide">可能的突变序列</param>
    /// <param name="mutationSite">如果是单位点突变，返回基于0的突变位点</param>
    /// <param name="ignoreNtermMutation">是否忽略N末端突变</param>
    /// <param name="ignoreDeamidatedMutation">是否忽略N->D和Q->E突变</param>
    /// <param name="ignoreMultipleNucleotideMutation">是否忽略Multiple Nucleotide Mutation</param>
    /// <returns></returns>
    public static bool IsMutationOne2(string fromPeptide, string toPeptide, ref int mutationSite, bool ignoreNtermMutation, bool ignoreDeamidatedMutation, bool ignoreMultipleNucleotideMutation)
    {
      return DoIsMutationOne(IsMutationOne, fromPeptide, toPeptide, ref mutationSite, ignoreNtermMutation, ignoreDeamidatedMutation, ignoreMultipleNucleotideMutation);
    }

    /// <summary>
    /// 在认为I/L是相同氨基酸情况下，判断是否单氨基酸突变。
    /// </summary>
    /// <param name="fromPeptide">原始序列</param>
    /// <param name="toPeptide">可能的突变序列</param>
    /// <param name="mutationSite">如果是单位点突变，返回基于0的突变位点</param>
    /// <param name="ignoreNtermMutation">是否忽略N末端突变</param>
    /// <param name="ignoreDeamidatedMutation">是否忽略N->D和Q->E突变</param>
    /// <returns></returns>
    public static bool IsMutationOneIL2(string fromPeptide, string toPeptide, ref int mutationSite, bool ignoreNtermMutation, bool ignoreDeamidatedMutation, bool ignoreMultipleNucleotideMutation)
    {
      return DoIsMutationOne(IsMutationOneIL, fromPeptide, toPeptide, ref mutationSite, ignoreNtermMutation, ignoreDeamidatedMutation, ignoreMultipleNucleotideMutation);
    }

    /// <summary>
    /// 根据原始蛋白质的序列，替换搜库结果中突变序列中对应位置的L为I
    /// </summary>
    /// <param name="mutSeq">搜库结果突变序列</param>
    /// <param name="oriPureSeq">原始蛋白质纯序列</param>
    /// <returns>修正后的突变序列</returns>
    public static string ReplaceLToI(string mutSeq, string oriPureSeq)
    {
      if (!oriPureSeq.Contains('I'))
      {
        return mutSeq;
      }

      string mutPureSeq = PeptideUtils.GetPureSequence(mutSeq);
      StringBuilder mutPureSeqSb = new StringBuilder(mutPureSeq);
      for (int i = 0; i < oriPureSeq.Length && i < mutPureSeq.Length; i++)
      {
        if (oriPureSeq[i] == 'I' && mutPureSeqSb[i] == 'L')
        {
          mutPureSeqSb[i] = oriPureSeq[i];
        }
      }

      StringBuilder mutSeqSb = new StringBuilder(mutSeq);
      int curIndex = 0;
      if (mutSeq.Contains('.'))
      {
        for (int i = 2; i < mutSeqSb.Length - 2; i++)
        {
          if (char.IsLetter(mutSeqSb[i]) && char.IsUpper(mutSeqSb[i]))
          {
            mutSeqSb[i] = mutPureSeqSb[curIndex++];
          }
        }
      }
      else
      {
        for (int i = 0; i < mutSeqSb.Length; i++)
        {
          if (char.IsLetter(mutSeqSb[i]) && char.IsUpper(mutSeqSb[i]))
          {
            mutSeqSb[i] = mutPureSeqSb[curIndex++];
          }
        }
      }
      return mutSeqSb.ToString();
    }

    /// <summary>
    /// 是否是与脱酰胺基相似的突变（N->D, Q->E）。
    /// </summary>
    /// <param name="fromAminoacid"></param>
    /// <param name="toAminoacid"></param>
    /// <returns></returns>
    public static bool IsDeamidatedMutation(char fromAminoacid, char toAminoacid)
    {
      return ('N' == fromAminoacid && 'D' == toAminoacid) || ('Q' == fromAminoacid && 'E' == toAminoacid);
    }
  }
}
