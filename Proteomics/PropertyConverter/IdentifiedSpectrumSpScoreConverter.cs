using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumSpScoreConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Sp"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.00}", t.SpScore);
    }

    public override void SetProperty(T t, string value)
    {
      t.SpScore = MyConvert.ToDouble(value);
    }
  }
}