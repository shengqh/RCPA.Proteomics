using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumDeltaScoreConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "DeltaScore"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.0###}", t.DeltaScore);
    }

    public override void SetProperty(T t, string value)
    {
      t.DeltaScore = MyConvert.ToDouble(value);
    }
  }
}