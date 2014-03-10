using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumModificationConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Modification"; }
    }

    public override string GetProperty(T t)
    {
      if (null == t.Modifications)
      {
        return "";
      }
      else
      {
        return t.Modifications;
      }
    }

    public override void SetProperty(T t, string value)
    {
      t.Modifications = value;
    }
  }
}