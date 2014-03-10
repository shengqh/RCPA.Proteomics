using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumMatchedTICConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "MatchedTIC"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.0###}", t.MatchedTIC);
    }

    public override void SetProperty(T t, string value)
    {
      t.MatchedTIC = MyConvert.ToDouble(value);
    }
  }
}