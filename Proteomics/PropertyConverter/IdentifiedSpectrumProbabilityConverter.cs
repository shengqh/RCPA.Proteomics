using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumProbabilityConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Probability"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.0###}", t.Probability);
    }

    public override void SetProperty(T t, string value)
    {
      t.Probability = MyConvert.ToDouble(value);
    }
  }
}