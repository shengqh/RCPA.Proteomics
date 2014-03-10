using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Utils
{
  public static class PeptideUtils
  {
    public static string GetPureSequence(string peptide)
    {
      StringBuilder sb = new StringBuilder();
      bool isFirstDot = true;
      foreach (char c in peptide)
      {
        if ('.' == c)
        {
          if (isFirstDot)
          {
            sb.Remove(0, sb.Length);
            isFirstDot = false;
          }
          else
          {
            break;
          }
        }
        else if (Char.IsLetter(c) && Char.IsUpper(c))
        {
          sb.Append(c);
        }
      }

      return sb.ToString();
    }

    public static string GetMatchedSequence(string peptide)
    {
      StringBuilder sb = new StringBuilder();
      bool isFirstDot = true;
      foreach (char c in peptide)
      {
        if ('.' == c)
        {
          if (isFirstDot)
          {
            sb.Remove(0, sb.Length);
            isFirstDot = false;
          }
          else
          {
            break;
          }
        }
        else
        {
          sb.Append(c);
        }
      }

      return sb.ToString();
    }

    public static bool IsAminoacid(char c)
    {
      return char.IsLetter(c) && char.IsUpper(c);
    }

    public static bool IsModified(string matchPeptide, int index)
    {
      if (null == matchPeptide)
      {
        throw new ArgumentNullException("matchPeptide");
      }

      if (index < 0 || index >= matchPeptide.Length)
      {
        throw new ArgumentOutOfRangeException("Index " + index + " out of bound, peptide = " + matchPeptide);
      }

      return IsAminoacid(matchPeptide[index])
          && (index < matchPeptide.Length - 1)
          && !IsAminoacid(matchPeptide[index + 1]);
    }

    public static char[] GetModifiedChar(string p)
    {
      return (from c in p
              where !(Char.IsLetter(c) && Char.IsUpper(c))
              select c).Distinct().ToArray();
    }
  }
}
