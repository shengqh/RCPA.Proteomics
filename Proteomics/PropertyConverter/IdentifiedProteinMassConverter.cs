using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedProteinMassConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedProtein
  {
    public override string Name
    {
      get { return "Mass"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.00}", t.MolecularWeight);
    }

    public override void SetProperty(T t, string value)
    {
      t.MolecularWeight = MyConvert.ToDouble(value);
    }
  }
}