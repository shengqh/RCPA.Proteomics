using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumMissCleavageConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "MissCleavage"; }
    }

    public override string GetProperty(T t)
    {
      return t.NumMissedCleavages.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.NumMissedCleavages = int.Parse(value);
    }
  }
}