using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumNumProteaseTerminiConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "NumProteaseTermini"; }
    }

    public override string GetProperty(T t)
    {
      return t.NumProteaseTermini.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.NumProteaseTermini = int.Parse(value);
    }
  }
}