using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedProteinNameConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedProtein
  {
    public override string Name
    {
      get { return "Name"; }
    }

    public override string GetProperty(T t)
    {
      return t.Name;
    }

    public override void SetProperty(T t, string value)
    {
      t.Name = value;
    }
  }
}