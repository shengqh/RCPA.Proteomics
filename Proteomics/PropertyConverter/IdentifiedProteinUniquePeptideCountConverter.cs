using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter.Mascot
{
  public class IdentifiedProteinUniquePeptideCountConverter<T> : AbstractPropertyConverter<T>
    where T : IIdentifiedProtein
  {
    public override string Name
    {
      get { return "UniquePeptideCount"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0}", t.UniquePeptideCount);
    }

    public override void SetProperty(T t, string value)
    {
      t.UniquePeptideCount = int.Parse(value);
    }
  }
}