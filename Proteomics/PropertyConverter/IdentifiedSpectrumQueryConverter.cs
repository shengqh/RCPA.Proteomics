using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumQueryConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Query"; }
    }

    public override string GetProperty(T t)
    {
      return t.Query.QueryId.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.Query.QueryId = int.Parse(value);
    }
  }
}