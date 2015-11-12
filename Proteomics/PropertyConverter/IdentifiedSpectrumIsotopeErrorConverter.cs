using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumIsotopeErrorConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "IsotopeError"; }
    }

    public override string GetProperty(T t)
    {
      return t.IsotopeError.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.IsotopeError = int.Parse(value);
    }
  }
}