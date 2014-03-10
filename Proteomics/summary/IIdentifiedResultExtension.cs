using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Utils;

namespace RCPA.Proteomics.Summary
{
  public static class IIdentifiedResultExtension
  {
    public static string GetProteinAnnotationHeader(this IIdentifiedResult ir)
    {
      return AnnotationUtils.GetAnnotationHeader(ir.GetProteins());
    }

    public static string GetSpectraAnnotationHeader(this IIdentifiedResult ir)
    {
      HashSet<string> keys = new HashSet<string>();

      foreach (IIdentifiedProteinGroup mpg in ir)
      {
        foreach (IIdentifiedSpectrum sp in mpg.GetPeptides())
        {
          keys.UnionWith(sp.Annotations.Keys);
        }
      }

      List<string> result = new List<string>(keys);
      result.Sort();

      return StringUtils.Merge(result, "\t");;
    }
  }
}
