using RCPA.Proteomics.Summary;
using System;
using System.Globalization;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumScoreConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    private string _format;

    public IdentifiedSpectrumScoreConverter(string format = "{0:0.0###}")
    {
      this._format = format;
    }

    public override string Name
    {
      get { return "Score"; }
    }

    public override string GetProperty(T t)
    {
      return MyConvert.Format(this._format, t.Score);
    }

    public override void SetProperty(T t, string value)
    {
      t.Score = MyConvert.ToDouble(value);
    }
  }
}