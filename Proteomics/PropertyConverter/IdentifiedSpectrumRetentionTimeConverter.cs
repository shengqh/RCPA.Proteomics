using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumRetentionTimeConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "RetentionTime"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.0###}", t.Query.FileScan.RetentionTime);
    }

    public override void SetProperty(T t, string value)
    {
      t.Query.FileScan.RetentionTime = MyConvert.ToDouble(value);
    }
  }
}