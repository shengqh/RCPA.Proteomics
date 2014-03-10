using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumTagConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Tag"; }
    }

    public override string GetProperty(T t)
    {
      if (null == t.Tag)
      {
        return "";
      }
      else
      {
        return t.Tag;
      }
    }

    public override void SetProperty(T t, string value)
    {
      t.Tag = value;
    }
  }
}