using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumQValueConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "QValue"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.0###}", t.QValue);
    }

    public override void SetProperty(T t, string value)
    {
      t.QValue = MyConvert.ToDouble(value);
    }
  }
}