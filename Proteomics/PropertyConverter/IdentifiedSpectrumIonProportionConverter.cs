using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumIonProportionConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "IonProportion"; }
    }

    public override string GetProperty(T t)
    {
      if (t.Annotations.ContainsKey(Name))
      {
        return t.Annotations[Name].ToString();
      }

      if (t.TheoreticalIonCount != 0)
      {
        return MyConvert.Format("{0:0.00}", t.MatchedIonCount*100.0/t.TheoreticalIonCount);
      }

      return "0.0";
    }

    public override void SetProperty(T t, string value)
    {
      t.Annotations[Name] = value;
    }
  }
}