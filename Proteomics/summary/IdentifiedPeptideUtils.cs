using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public static class IdentifiedPeptideUtils
  {
    public static HashSet<string> GetUniquePeptides(this IEnumerable<IIdentifiedPeptide> peptides)
    {
      HashSet<string> result = new HashSet<string>();
      HashSet<string> added = new HashSet<string>();
      foreach (IIdentifiedPeptide pep in peptides)
      {
        if (added.Contains(pep.PureILReplacedSequence))
        {
          continue;
        }
        result.Add(pep.PureSequence);
        added.Add(pep.PureILReplacedSequence);
      }
      return result;
    }

    public static HashSet<string> GetUniquePeptides(this IEnumerable<IIdentifiedPeptide> peptides, Func<IIdentifiedPeptide, bool> accept)
    {
      HashSet<string> result = new HashSet<string>();
      HashSet<string> added = new HashSet<string>();
      foreach (IIdentifiedPeptide pep in peptides)
      {
        if (accept(pep))
        {
          if (added.Contains(pep.PureILReplacedSequence))
          {
            continue;
          }
          result.Add(pep.PureSequence);
          added.Add(pep.PureILReplacedSequence);
        }
      }
      return result;
    }

    public static int GetUniquePeptideCount(this IEnumerable<IIdentifiedPeptide> peptides)
    {
      return GetUniquePeptides(peptides).Count;
    }

    public static int GetUniquePeptideCount(this IEnumerable<IIdentifiedPeptide> peptides, Func<IIdentifiedPeptide, bool> checkFunc)
    {
      return GetUniquePeptides(peptides, checkFunc).Count;
    }
  }
}
