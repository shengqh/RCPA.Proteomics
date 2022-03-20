using RCPA.Proteomics.Quantification.SILAC;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification
{
  public abstract class AbstractSilacProteinQuantificationResultConverter<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string _silacKey;
    private string _dsName;
    public AbstractSilacProteinQuantificationResultConverter(string silacKey, string dsName)
    {
      this._silacKey = silacKey;
      this._dsName = dsName;
    }

    protected QuantificationItem FindItem(T t)
    {
      if (t.Annotations.ContainsKey(_silacKey))
      {
        var lrrr = t.Annotations[_silacKey] as ProteinQuantificationResult;
        if (lrrr.Items.ContainsKey(_dsName))
        {
          return lrrr.Items[_dsName];
        }
      }

      return null;
    }

    protected QuantificationItem FindOrCreateItem(T t)
    {
      if (!t.Annotations.ContainsKey(_silacKey))
      {
        t.Annotations[_silacKey] = new ProteinQuantificationResult();
      }

      var lrrr = t.Annotations[_silacKey] as ProteinQuantificationResult;
      if (!lrrr.Items.ContainsKey(_dsName))
      {
        var item = new QuantificationItem();
        lrrr.Items[_dsName] = item;
        return item;
      }
      else
      {
        return lrrr.Items[_dsName];
      }
    }
  }

  public class SilacProteinQuantificationResultConverter_Enabled<T> : AbstractSilacProteinQuantificationResultConverter<T> where T : IAnnotation
  {
    private string _name;
    public SilacProteinQuantificationResultConverter_Enabled(string silacKey, string dsName)
      : base(silacKey, dsName)
    {
      this._name = "SE_" + dsName;
    }

    public override string Name
    {
      get { return _name; }
    }

    public override string GetProperty(T t)
    {
      var spqr = FindItem(t);

      if (spqr == null)
      {
        return false.ToString();
      }

      return spqr.Enabled.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      var spqr = FindOrCreateItem(t);

      if (value.Length == 0)
      {
        spqr.Enabled = false;
      }
      else
      {
        spqr.Enabled = bool.Parse(value);
      }
    }
  }

  public class SilacProteinQuantificationResultConverter_Ratio<T> : AbstractSilacProteinQuantificationResultConverter<T> where T : IAnnotation
  {
    private string _name;
    public SilacProteinQuantificationResultConverter_Ratio(string silacKey, string dsName)
      : base(silacKey, dsName)
    {
      this._name = "SR_" + dsName;
    }

    public override string Name
    {
      get { return _name; }
    }

    public override string GetProperty(T t)
    {
      var spqr = FindItem(t);

      if (spqr == null)
      {
        return "";
      }

      return MyConvert.Format("{0:0.0000}", spqr.Ratio);
    }

    public override void SetProperty(T t, string value)
    {
      var spqr = FindOrCreateItem(t);

      if (value.Length == 0)
      {
        spqr.Ratio = 0.0;
      }
      else
      {
        spqr.Ratio = MyConvert.ToDouble(value);
      }
    }
  }

  public class SilacProteinQuantificationResultConverter_RegressionCorrelation<T> : AbstractSilacProteinQuantificationResultConverter<T> where T : IAnnotation
  {
    private string _name;
    public SilacProteinQuantificationResultConverter_RegressionCorrelation(string silacKey, string dsName)
      : base(silacKey, dsName)
    {
      this._name = "SRC_" + dsName;
    }

    public override string Name
    {
      get { return _name; }
    }

    public override string GetProperty(T t)
    {
      var spqr = FindItem(t);

      if (spqr == null)
      {
        return "";
      }

      return MyConvert.Format("{0:0.0000}", spqr.Correlation);
    }

    public override void SetProperty(T t, string value)
    {
      var spqr = FindOrCreateItem(t);

      if (value.Length == 0)
      {
        spqr.Correlation = 0.0;
      }
      else
      {
        spqr.Correlation = MyConvert.ToDouble(value);
      }
    }
  }

  public class SilacProteinQuantificationResultConverter_SampleIntensity<T> : AbstractSilacProteinQuantificationResultConverter<T> where T : IAnnotation
  {
    private string _name;
    public SilacProteinQuantificationResultConverter_SampleIntensity(string silacKey, string dsName)
      : base(silacKey, dsName)
    {
      this._name = "SSI_" + dsName;
    }

    public override string Name
    {
      get { return _name; }
    }

    public override string GetProperty(T t)
    {
      var spqr = FindItem(t);

      if (spqr == null)
      {
        return "";
      }

      return MyConvert.Format("{0:0.0000}", spqr.SampleIntensity);
    }

    public override void SetProperty(T t, string value)
    {
      var spqr = FindOrCreateItem(t);

      if (value.Length == 0)
      {
        spqr.SampleIntensity = 0.0;
      }
      else
      {
        spqr.SampleIntensity = MyConvert.ToDouble(value);
      }
    }
  }

  public class SilacProteinQuantificationResultConverter_ReferenceIntensity<T> : AbstractSilacProteinQuantificationResultConverter<T> where T : IAnnotation
  {
    private string _name;
    public SilacProteinQuantificationResultConverter_ReferenceIntensity(string silacKey, string dsName)
      : base(silacKey, dsName)
    {
      this._name = "SRI_" + dsName;
    }

    public override string Name
    {
      get { return _name; }
    }

    public override string GetProperty(T t)
    {
      var spqr = FindItem(t);

      if (spqr == null)
      {
        return "";
      }

      return MyConvert.Format("{0:0.0000}", spqr.ReferenceIntensity);
    }

    public override void SetProperty(T t, string value)
    {
      var spqr = FindOrCreateItem(t);

      if (value.Length == 0)
      {
        spqr.ReferenceIntensity = 0.0;
      }
      else
      {
        spqr.ReferenceIntensity = MyConvert.ToDouble(value);
      }
    }
  }

  public class SilacProteinQuantificationResultConverter<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string _silacKey;

    public SilacProteinQuantificationResultConverter()
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
        result.Add(new SilacProteinQuantificationResultConverter_Enabled<T>(_silacKey, item));
        result.Add(new SilacProteinQuantificationResultConverter_Ratio<T>(_silacKey, item));
        result.Add(new SilacProteinQuantificationResultConverter_RegressionCorrelation<T>(_silacKey, item));
        result.Add(new SilacProteinQuantificationResultConverter_SampleIntensity<T>(_silacKey, item));
        result.Add(new SilacProteinQuantificationResultConverter_ReferenceIntensity<T>(_silacKey, item));
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