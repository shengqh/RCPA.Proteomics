using System.Text;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumReferenceConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrumBase
  {
    private static Regex reg = new Regex(" ! ");

    private static char[] chars = new char[] { '/' };

    public override string Name
    {
      get { return "Reference"; }
    }

    public override string GetProperty(T t)
    {
      if (t.Peptides.Count == 1 && t.Peptides[0].Proteins.Count == 1)
      {
        return t.Peptides[0].Proteins[0];
      }

      var sb = new StringBuilder();
      bool bfirst = true;
      foreach (IIdentifiedPeptide pep in t.Peptides)
      {
        if (bfirst)
        {
          bfirst = false;
        }
        else
        {
          sb.Append(" ! ");
        }
        StringUtils.AddTo(sb, pep.Proteins, "/");
      }

      return sb.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      string[] proteins = reg.Split(value);

      if (t.Peptides.Count != proteins.Length)
      {
        t.ClearPeptides();

        for (int i = 0; i < proteins.Length; i++)
        {
          IIdentifiedPeptide mp = new IdentifiedPeptide(t);

          string[] parts = proteins[i].Split(chars);
          foreach (string part in parts)
          {
            mp.AddProtein(part);
          }
        }
      }
      else
      {
        for (int i = 0; i < proteins.Length; i++)
        {
          string[] parts = proteins[i].Split(chars);
          t.Peptides[i].ClearProteins();
          foreach (string part in parts)
          {
            t.Peptides[i].AddProtein(part);
          }
        }
      }
    }
  }
}