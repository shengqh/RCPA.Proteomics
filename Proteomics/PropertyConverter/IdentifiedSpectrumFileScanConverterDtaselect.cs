using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumFileScanConverterDtaselect<T> : AbstractPropertyConverter<T>
    where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "FileName"; }
    }

    public override string Version
    {
      get { return "Dtaselect"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0}.{1}.{2}.{3}", t.Query.FileScan.Experimental, t.Query.FileScan.FirstScan,
                           t.Query.FileScan.LastScan, t.Query.Charge);
    }

    public override void SetProperty(T t, string value)
    {
      t.Query.FileScan.LongFileName = value;
    }
  }
}