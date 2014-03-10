using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter.Mascot
{
  public class MascotProteinTotalScoreConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedProtein
  {
    public override string Name
    {
      get { return "TotalScore"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0}", t.Score);
    }

    public override void SetProperty(T t, string value)
    {
    }
  }
}