using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumChargeConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Charge"; }
    }

    public override string GetProperty(T t)
    {
      return t.Query.Charge.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.Query.Charge = int.Parse(value);
    }
  }
}