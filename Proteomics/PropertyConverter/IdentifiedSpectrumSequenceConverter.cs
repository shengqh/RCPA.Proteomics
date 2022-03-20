using RCPA.Proteomics.Summary;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumSequenceConverter<T> : AbstractPropertyConverter<T>
    where T : IIdentifiedSpectrumBase
  {
    private static Regex reg = new Regex(" ! ");

    public override string Name
    {
      get { return "Sequence"; }
    }

    public override string GetProperty(T t)
    {
      var sb = new StringBuilder();
      bool bfirst = true;
      foreach (IIdentifiedPeptide pep in t.Peptides)
      {
        if (bfirst)
        {
          sb.Append(pep.Sequence);
          bfirst = false;
        }
        else
        {
          sb.Append(" ! ");
          sb.Append(pep.Sequence);
        }
      }

      return sb.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      string[] peptides = reg.Split(value);

      if (t.Peptides.Count != peptides.Length)
      {
        t.ClearPeptides();
        foreach (string peptide in peptides)
        {
          IIdentifiedPeptide mp = t.NewPeptide();
          mp.Sequence = peptide;
        }
      }
      else
      {
        for (int i = 0; i < peptides.Length; i++)
        {
          t.Peptides[i].Sequence = peptides[i];
        }
      }
    }
  }
}