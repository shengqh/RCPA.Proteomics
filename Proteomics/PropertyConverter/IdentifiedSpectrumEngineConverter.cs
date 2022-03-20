using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumEngineConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Engine"; }
    }

    public override string GetProperty(T t)
    {
      return t.Engine;
    }

    public override void SetProperty(T t, string value)
    {
      t.Engine = value;
    }
  }
}