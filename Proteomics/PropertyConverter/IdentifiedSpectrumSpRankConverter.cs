using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumSpRankConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "RSp"; }
    }

    public override string GetProperty(T t)
    {
      return t.SpRank.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.SpRank = int.Parse(value);
    }
  }
}