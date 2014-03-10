using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumIdConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Id"; }
    }

    public override string GetProperty(T t)
    {
      return t.Id.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.Id = int.Parse(value);
    }
  }
}