using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumIonsConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    private char[] chars = new[] { '|' };

    public override string Name
    {
      get { return "Ions"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0}|{1}", t.MatchedIonCount, t.TheoreticalIonCount);
    }

    public override void SetProperty(T t, string value)
    {
      string[] parts = value.Split(chars);
      if (parts.Length == 2)
      {
        t.MatchedIonCount = int.Parse(parts[0]);
        t.TheoreticalIonCount = int.Parse(parts[1]);
      }
      else
      {
        t.MatchedIonCount = 0;
        t.TheoreticalIonCount = 0;
      }
    }
  }
}