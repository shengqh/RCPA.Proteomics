using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumFileScanConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "\"File, Scan(s)\""; }
    }

    public override string GetProperty(T t)
    {
      return t.Query.FileScan.ShortFileName;
    }

    public override void SetProperty(T t, string value)
    {
      t.Query.FileScan.ShortFileName = value;
    }
  }
}