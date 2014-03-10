using RCPA.Proteomics.Summary;
using System;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumIsoelectricPointConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "PI"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format("{0:0.00}", t.IsoelectricPoint);
    }

    public override void SetProperty(T t, string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        t.IsoelectricPoint = 0.0;
      }
      else
      {
        t.IsoelectricPoint = MyConvert.ToDouble(value);
      }
    }
  }
}