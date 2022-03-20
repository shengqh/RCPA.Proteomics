using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification
{
  public class QuantificationItemBaseConverter<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string name;

    private Func<QuantificationItem, string> GetValue;

    private Action<QuantificationItem, string> SetValue;

    public QuantificationItemBaseConverter(string name, Func<QuantificationItem, string> getValue, Action<QuantificationItem, string> setValue)
    {
      this.name = name;
      this.GetValue = getValue;
      this.SetValue = setValue;
    }

    public override string Name
    {
      get { return name; }
    }

    public override string GetProperty(T t)
    {
      var item = t.GetQuantificationItem();

      if (item == null)
      {
        return "";
      }

      return GetValue(item);
    }

    public override void SetProperty(T t, string value)
    {
      var item = t.GetQuantificationItem();

      if (item == null)
      {
        item = new QuantificationItem();
        t.SetQuantificationItem(item);
      }

      SetValue(item, value);
    }

    public override string ToString()
    {
      return Name;
    }
  }

  public class QuantificationItemRatioConverter<T> : QuantificationItemBaseConverter<T> where T : IAnnotation
  {
    public QuantificationItemRatioConverter()
      : base("S_RATIO", m => m.RatioStr, (m, value) => m.RatioStr = value)
    { }

    public override List<IPropertyConverter<T>> GetRelativeConverter(string header, char delimiter)
    {
      return GetRelativeConverter();
    }

    private static List<IPropertyConverter<T>> GetRelativeConverter()
    {
      var result = new List<IPropertyConverter<T>>();

      result.Add(new QuantificationItemBaseConverter<T>("Enabled", m => m.Enabled.ToString(), (m, value) => m.Enabled = Convert.ToBoolean(value)));
      result.Add(new QuantificationItemBaseConverter<T>("S_CORR", m => m.CorrelationStr, (m, value) => m.CorrelationStr = value));
      result.Add(new QuantificationItemBaseConverter<T>("INT_REF", m => m.ReferenceIntensityStr, (m, value) => m.ReferenceIntensityStr = value));
      result.Add(new QuantificationItemBaseConverter<T>("INT_SAM", m => m.SampleIntensityStr, (m, value) => m.SampleIntensityStr = value));
      result.Add(new QuantificationItemBaseConverter<T>("S_FILE", m => m.Filename, (m, value) => m.Filename = value));
      result.Add(new QuantificationItemBaseConverter<T>("S_SCANS", m => m.ScanCountStr, (m, value) => m.ScanCountStr = value));

      return result;
    }

    public override List<IPropertyConverter<T>> GetRelativeConverter(List<T> items)
    {
      return GetRelativeConverter();
    }
  }
}