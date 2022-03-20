using RCPA.Proteomics.Quantification.SILAC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification
{
  public class SilacProteinQuantificationResultConverter2<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string _silacKey;

    public SilacProteinQuantificationResultConverter2()
    {
      this._silacKey = SilacQuantificationConstants.SILAC_KEY;
    }

    public override string Name
    {
      get { return _silacKey; }
    }

    public override string GetProperty(T t)
    {
      if (t.Annotations.ContainsKey(_silacKey))
      {
        var lrrr = t.Annotations[_silacKey] as ProteinQuantificationResult;
        return lrrr.Items.Count.ToString();
      }

      return "0";
    }

    public override void SetProperty(T t, string value)
    {
      if (!t.Annotations.ContainsKey(_silacKey))
      {
        t.Annotations[_silacKey] = new ProteinQuantificationResult();
      }
    }

    public override List<IPropertyConverter<T>> GetRelativeConverter(List<T> items)
    {
      foreach (var t in items)
      {
        if (t.Annotations.ContainsKey(_silacKey))
        {
          var lrrr = t.Annotations[_silacKey] as ProteinQuantificationResult;

          var datasets = lrrr.Items.Keys.ToList();

          return DoGetConverter(datasets);
        }
      }

      return null;
    }

    private List<IPropertyConverter<T>> DoGetConverter(IEnumerable<string> datasets)
    {
      List<IPropertyConverter<T>> result = new List<IPropertyConverter<T>>();
      foreach (var item in datasets)
      {
        //Enabled
        result.Add(new ProteinQuantificationResultBaseConverter<T>(_silacKey, "SE", item, m => m.Enabled.ToString(), (m, v) => m.Enabled = Convert.ToBoolean(v)));
        //Ratio
        result.Add(new ProteinQuantificationResultBaseConverter<T>(_silacKey, "SR", item, m => m.RatioStr, (m, v) => m.RatioStr = v));
        //RegressionCorrelation
        result.Add(new ProteinQuantificationResultBaseConverter<T>(_silacKey, "SRC", item, m => m.CorrelationStr, (m, v) => m.CorrelationStr = v));
        //SampleIntensity
        result.Add(new ProteinQuantificationResultBaseConverter<T>(_silacKey, "SSI", item, m => m.SampleIntensityStr, (m, v) => m.SampleIntensityStr = v));
        //ReferenceIntensity
        result.Add(new ProteinQuantificationResultBaseConverter<T>(_silacKey, "SRI", item, m => m.ReferenceIntensityStr, (m, v) => m.ReferenceIntensityStr = v));
      }
      return result;
    }

    public override List<IPropertyConverter<T>> GetRelativeConverter(string header, char delimiter)
    {
      string[] parts = header.Split(delimiter);

      string[] datasets = (from p in parts
                           where p.StartsWith("SR_")
                           select p.Substring(3)).ToArray();

      return DoGetConverter(datasets);
    }
  }
}