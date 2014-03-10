using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public static class IdentifiedPeptideUtils
  {
    public static HashSet<string> GetUniquePeptides(this IEnumerable<IIdentifiedPeptide> peptides)
    {
      HashSet<string> result = new HashSet<string>();
      foreach (IIdentifiedPeptide pep in peptides)
      {
        result.Add(pep.PureSequence);
      }
      return result;
    }

    public static HashSet<string> GetUniquePeptides(this IEnumerable<IIdentifiedPeptide> peptides, Func<IIdentifiedPeptide, bool> accept)
    {
      HashSet<string> result = new HashSet<string>();
      foreach (IIdentifiedPeptide pep in peptides)
      {
        if (accept(pep))
        {
          result.Add(pep.PureSequence);
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
