using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumMatchCountConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "MatchCount"; }
    }

    public override string GetProperty(T t)
    {
      return t.Query.MatchCount.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.Query.MatchCount = int.Parse(value);
    }
  }
}