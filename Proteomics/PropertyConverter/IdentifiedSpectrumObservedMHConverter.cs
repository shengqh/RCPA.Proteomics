using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumObservedMHConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "ExperimentMH"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.00000}", t.ExperimentalMH);
    }

    public override void SetProperty(T t, string value)
    {
      t.ExperimentalMH = MyConvert.ToDouble(value);
    }
  }
}