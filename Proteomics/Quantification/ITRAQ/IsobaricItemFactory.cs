using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class IonDefinitionItem
  {
    public IonDefinitionItem(IsobaricType plexType, double mass)
    {
      this.PlexType = plexType;
      this.Mass = mass;
      this.Index = (int)(Math.Round(mass));
      this.Name = "I" + Index.ToString();
    }

    public IsobaricType PlexType { get; private set; }

    public double Mass { get; private set; }

    public int Index { get; private set; }

    public int AbsoluteIndex { get; set; }

    public string Name { get; private set; }
  }

  public class IsobaricDefinition
  {
    private Dictionary<int, IonDefinitionItem> itemMap;

    public IonDefinitionItem[] Items { get; private set; }

    public double[] TagMass { get; set; }

    public IsobaricDefinition(IonDefinitionItem[] items)
    {
      this.Items = items;
      this.itemMap = items.ToDictionary(m => m.Index);
      this.MinIndex = items.Min(m => m.Index);
      this.MaxIndex = items.Max(m => m.Index);
      foreach (var item in items)
      {
        item.AbsoluteIndex = item.Index - this.MinIndex;
      }
      this.TagMass = new double[] { };
    }

    public bool IsValid(int index)
    {
      return itemMap.ContainsKey(index);
    }

    public IonDefinitionItem this[int index]
    {
      get
      {
        return itemMap[index];
      }
    }

    public int MinIndex { get; private set; }

    public int MaxIndex { get; private set; }

    public IsobaricType PlexType
    {
      get
      {
        return Items[0].PlexType;
      }
    }
  }

  public static class IsobaricItemFactory
  {
    private static readonly IonDefinitionItem[] _ITraqPlex4 = new IonDefinitionItem[]{
      new IonDefinitionItem(IsobaricType.PLEX4,114.11068),
      new IonDefinitionItem(IsobaricType.PLEX4,115.114034),
      new IonDefinitionItem(IsobaricType.PLEX4,116.111069),
      new IonDefinitionItem(IsobaricType.PLEX4,117.114424)};

    public static readonly IsobaricDefinition ITraqPlex4 = new IsobaricDefinition(_ITraqPlex4)
    {
      TagMass = new double[] { 145.1076 }
    };

    private static readonly IonDefinitionItem[] _ITraqPlex8 = new IonDefinitionItem[]{
      new IonDefinitionItem(IsobaricType.PLEX8,113.107325),
      new IonDefinitionItem(IsobaricType.PLEX8,114.11068),
      new IonDefinitionItem(IsobaricType.PLEX8,115.107715),
      new IonDefinitionItem(IsobaricType.PLEX8,116.111069),
      new IonDefinitionItem(IsobaricType.PLEX8,117.114424),
      new IonDefinitionItem(IsobaricType.PLEX8,118.111459),
      new IonDefinitionItem(IsobaricType.PLEX8,119.114814),
      new IonDefinitionItem(IsobaricType.PLEX8,121.121524)
    };

    public static readonly IsobaricDefinition ITraqPlex8 = new IsobaricDefinition(_ITraqPlex8)
    {
      TagMass = new double[] { 305.2092 }
    };

    //private static readonly IonDefinitionItem[] _TMTPlex6 = new IonDefinitionItem[]{
    //  new IonDefinitionItem(IsobaricType.TMT6,126.127726),
    //  new IonDefinitionItem(IsobaricType.TMT6,127.131081),
    //  new IonDefinitionItem(IsobaricType.TMT6,128.134436),
    //  new IonDefinitionItem(IsobaricType.TMT6,129.13779),
    //  new IonDefinitionItem(IsobaricType.TMT6,130.141145),
    //  new IonDefinitionItem(IsobaricType.TMT6,131.13818)
    //};

    private static readonly IonDefinitionItem[] _TMTPlex6 = new IonDefinitionItem[]{
      new IonDefinitionItem(IsobaricType.TMT6,126.127725),
      new IonDefinitionItem(IsobaricType.TMT6,127.124760),
      new IonDefinitionItem(IsobaricType.TMT6,128.134433),
      new IonDefinitionItem(IsobaricType.TMT6,129.131468),
      new IonDefinitionItem(IsobaricType.TMT6,130.141141),
      new IonDefinitionItem(IsobaricType.TMT6,131.138176)
    };

    public static readonly IsobaricDefinition TMTPlex6 = new IsobaricDefinition(_TMTPlex6)
    {
      TagMass = new double[] { 230.1699 }
    };

    //private static readonly IonDefinitionItem[] _TMTPlex10 = new IonDefinitionItem[]{
    //  new IonDefinitionItem(IsobaricType.TMT10,126.127726),
    //  new IonDefinitionItem(IsobaricType.TMT10,127.124761),
    //  new IonDefinitionItem(IsobaricType.TMT10,127.131081),
    //  new IonDefinitionItem(IsobaricType.TMT10,128.128116),
    //  new IonDefinitionItem(IsobaricType.TMT10,128.134436),
    //  new IonDefinitionItem(IsobaricType.TMT10,129.131471),
    //  new IonDefinitionItem(IsobaricType.TMT10,129.13779),
    //  new IonDefinitionItem(IsobaricType.TMT10,130.134825),
    //  new IonDefinitionItem(IsobaricType.TMT10,130.141145),
    //  new IonDefinitionItem(IsobaricType.TMT10,131.13818)
    //};

    //public static readonly IsobaricDefinition TMTPlex10 = new IsobaricDefinition(_TMTPlex10)
    //{
    //  TagMass = new double[] { 230.1699 }
    //};

    public static IsobaricDefinition GetDefinition(this IsobaricType plexType)
    {
      if (plexType == IsobaricType.PLEX4)
      {
        return ITraqPlex4;
      }

      if (plexType == IsobaricType.PLEX8)
      {
        return ITraqPlex8;
      }

      if (plexType == IsobaricType.TMT6)
      {
        return TMTPlex6;
      }

      //if (plexType == IsobaricType.TMT10)
      //{
      //  return TMTPlex10;
      //}

      throw new ArgumentException("Unknown isobaric type : " + plexType.ToString());
    }

    public static IsobaricItemImpl CreateItem(this IsobaricType plexType, IsobaricItem parent)
    {
      return new IsobaricItemImpl(parent, plexType.GetDefinition());
    }
  }
}
