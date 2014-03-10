using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumPValueConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "PValue"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.0###}", t.PValue);
    }

    public override void SetProperty(T t, string value)
    {
      t.PValue = MyConvert.ToDouble(value);
    }
  }
}