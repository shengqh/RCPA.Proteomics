using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumRankConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Rank"; }
    }

    public override string GetProperty(T t)
    {
      return t.Rank.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.Rank = int.Parse(value);
    }
  }
}