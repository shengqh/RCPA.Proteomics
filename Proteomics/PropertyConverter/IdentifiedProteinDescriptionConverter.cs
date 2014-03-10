using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedProteinDescriptionConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedProtein
  {
    public override string Name
    {
      get { return "Description"; }
    }

    public override string GetProperty(T t)
    {
      return t.Description;
    }

    public override void SetProperty(T t, string value)
    {
      t.Description = value;
    }
  }
}