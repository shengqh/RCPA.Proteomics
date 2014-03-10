using System.Text;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumEngineConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Engine"; }
    }

    public override string GetProperty(T t)
    {
      return t.Engine;
    }

    public override void SetProperty(T t, string value)
    {
      t.Engine = value;
    }
  }
}