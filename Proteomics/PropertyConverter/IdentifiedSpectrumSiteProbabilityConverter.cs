using System.Text;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Linq;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumSiteProbabilityConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrumBase
  {
    private static Regex reg = new Regex(" ! ");

    public override string Name
    {
      get { return "SiteProbability"; }
    }

    public override string GetProperty(T t)
    {
      if (t.Peptides.All(l => string.IsNullOrEmpty(l.SiteProbability)))
      {
        return string.Empty;
      }

      return (from l in t.Peptides
              select l.SiteProbability).Merge(" ! ");
    }

    public override void SetProperty(T t, string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return;
      }

      string[] siteProbs = reg.Split(value);

      if (t.Peptides.Count != siteProbs.Length)
      {
        t.ClearPeptides();

        for (int i = 0; i < siteProbs.Length; i++)
        {
          IIdentifiedPeptide mp = new IdentifiedPeptide(t);
          mp.SiteProbability = siteProbs[i];
        }
      }
      else
      {
        for (int i = 0; i < siteProbs.Length; i++)
        {
          t.Peptides[i].SiteProbability = siteProbs[i];
        }
      }
    }
  }
}