using RCPA.Proteomics.Summary;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using RCPA.Proteomics.PropertyConverter;
using RCPA.Converter;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqItemBaseConverter<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string name;

    private Func<IsobaricItem, string> GetValue;

    private Action<IsobaricItem, string> SetValue;

    private IsobaricType plexType = IsobaricType.PLEX4;

    private bool setPlexType = false;

    public ITraqItemBaseConverter(string name, Func<IsobaricItem, string> getValue, Action<IsobaricItem, string> setValue)
    {
      this.name = name;
      this.GetValue = getValue;
      this.SetValue = setValue;
    }

    public ITraqItemBaseConverter(string name, Func<IsobaricItem, string> getValue, Action<IsobaricItem, string> setValue, IsobaricType plexType, bool setPlexType)
      : this(name, getValue, setValue)
    {
      this.plexType = plexType;
      this.setPlexType = setPlexType;
    }

    public override string Name
    {
      get { return name; }
    }

    public override string GetProperty(T t)
    {
      var item = t.FindIsobaricItem();

      if (item == null)
      {
        return "";
      }

      return GetValue(item);
    }

    public override void SetProperty(T t, string value)
    {
      var item = t.FindIsobaricItem();

      if (string.IsNullOrEmpty(value))
      {
        if (null != item)
          t.RemoveIsobaricItem();
        return;
      }

      if (item == null)
      {
        item = new IsobaricItem();
        if (setPlexType)
        {
          item.PlexType = this.plexType;
        }
        t.SetIsobaricItem(item);
      }

      SetValue(item, value);
    }

    public override string ToString()
    {
      return Name;
    }
  }

  public class ITraqItemChannelBaseConverter<T> : ITraqItemBaseConverter<T> where T : IAnnotation
  {
    private IonDefinitionItem defItem;

    public ITraqItemChannelBaseConverter(IonDefinitionItem defItem, bool setPlexType = false)
      : base(defItem.Name, m => MyConvert.Format("{0:0.0}", m[defItem.Index]), (m, value) => m[defItem.Index] = MyConvert.ToDouble(value), defItem.PlexType, setPlexType)
    {
      this.defItem = defItem;
    }
  }

  public class IsobaricFirstChannelConverter<T> : ITraqItemChannelBaseConverter<T> where T : IAnnotation
  {
    private IsobaricDefinition _definition;

    public IsobaricFirstChannelConverter(IsobaricDefinition definition)
      : base(definition.Items[0], true)
    {
      this._definition = definition;
    }

    public override List<IPropertyConverter<T>> GetRelativeConverter(string header, char delimiter)
    {
      var result = GetConverters();

      var parts = header.Split(delimiter);
      result.RemoveAll(m => !parts.Contains(m.Name));

      return result;
    }

    public override List<IPropertyConverter<T>> GetRelativeConverter(List<T> items)
    {
      return GetConverters();
    }

    private List<IPropertyConverter<T>> GetConverters()
    {
      var result = new List<IPropertyConverter<T>>();

      for (int i = 1; i < _definition.Items.Length; i++)
      {
        result.Add(new ITraqItemChannelBaseConverter<T>(_definition.Items[i]));
      }

      result.Add(new ITraqItemValidConverter<T>());
      result.Add(new ITraqItemValidProbabilityConverter<T>());
      result.Add(new ITraqItemPrecursorPercentageConverter<T>());

      return result;
    }
  }

  public class ITraqItemI114Converter<T> : IsobaricFirstChannelConverter<T> where T : IAnnotation
  {
    public ITraqItemI114Converter()
      : base(IsobaricItemFactory.ITraqPlex4)
    { }
  }

  public class ITraqItemI113Converter<T> : IsobaricFirstChannelConverter<T> where T : IAnnotation
  {
    public ITraqItemI113Converter()
      : base(IsobaricItemFactory.ITraqPlex8)
    { }
  }

  public class TMTPlex6I126Converter<T> : IsobaricFirstChannelConverter<T> where T : IAnnotation
  {
    public TMTPlex6I126Converter()
      : base(IsobaricItemFactory.TMTPlex6)
    { }
  }

  public class ITraqItemValidConverter<T> : ITraqItemBaseConverter<T> where T : IAnnotation
  {
    public ITraqItemValidConverter()
      : base("IValid", m => m.Valid.ToString(), (m, value) => m.Valid = bool.Parse(value))
    { }
  }

  public class ITraqItemValidProbabilityConverter<T> : ITraqItemBaseConverter<T> where T : IAnnotation
  {
    public ITraqItemValidProbabilityConverter()
      : base("IValidProbability", m => MyConvert.Format("{0:E}", m.ValidProbability), (m, value) => m.ValidProbability = MyConvert.ToDouble(value))
    { }
  }

  public class ITraqItemPrecursorPercentageConverter<T> : ITraqItemBaseConverter<T> where T : IAnnotation
  {
    public ITraqItemPrecursorPercentageConverter()
      : base("IPrecursorPercentage", m => MyConvert.Format("{0:0.00}", m.PrecursorPercentage), (m, value) => m.PrecursorPercentage = MyConvert.ToDouble(value))
    { }
  }

  public class ITraqItemPlexConverter<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    public ITraqItemPlexConverter() { }

    public override string Name
    {
      get { return ITraqConsts.ITRAQ_TYPE; }
    }

    public override List<IPropertyConverter<T>> GetRelativeConverter(List<T> items)
    {
      var plexType = (from item in items
                      let itraq = item.FindIsobaricItem()
                      where null != itraq
                      select itraq.PlexType).FirstOrDefault();

      var result = new List<IPropertyConverter<T>>();
      switch (plexType)
      {
        case IsobaricType.PLEX4:
          result.Add(new ITraqItemI114Converter<T>());
          break;
        case IsobaricType.PLEX8:
          result.Add(new ITraqItemI113Converter<T>());
          break;
        case IsobaricType.TMT6:
          result.Add(new TMTPlex6I126Converter<T>());
          break;
      }
      result.AddRange(result[0].GetRelativeConverter(items));

      return result;
    }

    public override string GetProperty(T t)
    {
      var item = t.FindIsobaricItem();
      if (null == item)
      {
        return string.Empty;
      }

      return item.PlexType.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        t.RemoveIsobaricItem();
      }

      var item = t.FindOrCreateITsobaricItem();
      item.PlexType = IsobaricItemExtension.Find(value);
    }
  }
}