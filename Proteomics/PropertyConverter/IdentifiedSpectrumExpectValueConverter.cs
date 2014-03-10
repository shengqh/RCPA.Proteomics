using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumExpectValueConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "ExpectValue"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:E2}", t.ExpectValue);
    }

    public override void SetProperty(T t, string value)
    {
      t.ExpectValue = MyConvert.ToDouble(value);
    }
  }
}