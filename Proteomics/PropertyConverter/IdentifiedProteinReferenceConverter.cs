using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter.Mascot
{
  public class IdentifiedProteinReferenceConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedProtein
  {
    public override string Name
    {
      get { return "Reference"; }
    }

    public override string GetProperty(T t)
    {
      return t.Reference;
    }

    public override void SetProperty(T t, string value)
    {
      t.Reference = value;
    }
  }
}