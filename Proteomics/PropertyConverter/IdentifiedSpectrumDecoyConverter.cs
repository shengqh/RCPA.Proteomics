using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumDecoyConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Decoy"; }
    }

    public override string GetProperty(T t)
    {
      return t.FromDecoy ? "D" : "T";
    }

    public override void SetProperty(T t, string value)
    {
      if (value.Equals("T"))
      {
        t.FromDecoy = false;
      }
      else if (value.Equals("D"))
      {
        t.FromDecoy = true;
      }
      else
      {
        t.FromDecoy = bool.Parse(value);
      }
    }
  }


}