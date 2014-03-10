using RCPA.Proteomics.Summary;
using System;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedProteinSpectrumCountConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedProtein
  {
    public override string Name
    {
      get { return "PepCount"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0}", t.PeptideCount);
    }

    public override void SetProperty(T t, string value)
    {
      t.PeptideCount = Convert.ToInt32(value);
    }
  }
}