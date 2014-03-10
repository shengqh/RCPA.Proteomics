using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedProteinCoverageConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedProtein
  {
    public override string Name
    {
      get { return "Coverage"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.00}%", t.Coverage);
    }

    public override void SetProperty(T t, string value)
    {
      t.Coverage = MyConvert.ToDouble(value.Substring(0, value.Length - 1));
    }
  }
}