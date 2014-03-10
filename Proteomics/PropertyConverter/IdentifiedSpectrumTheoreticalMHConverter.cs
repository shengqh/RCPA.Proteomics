using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumTheoreticalMHConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "MH+"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.00000}", t.TheoreticalMH);
    }

    public override void SetProperty(T t, string value)
    {
      t.TheoreticalMH = MyConvert.ToDouble(value);
    }
  }
}