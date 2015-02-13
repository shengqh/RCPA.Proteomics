using System;
using System.Linq;
using System.Collections.Generic;
using RCPA.Proteomics.Spectrum;
using System.IO;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// Description of IsobaricDefinition.
  /// </summary>
  public class IsobaricType : ICloneable
  {
    private Dictionary<double, Dictionary<IsobaricChannel, double>> mzToleranceMap = new Dictionary<double, Dictionary<IsobaricChannel, double>>();

    public IsobaricType()
    {
      this.Channels = new List<IsobaricChannel>();
      this.TagMassH = new List<double>();
      this.NameIndexMap = new Dictionary<string, int>();
    }

    public string Name { get; set; }

    public List<double> TagMassH { get; private set; }

    public List<IsobaricChannel> Channels { get; private set; }

    public List<UsedChannel> ToUsedChannels()
    {
      var result = new List<UsedChannel>();
      foreach (var cha in Channels)
      {
        result.Add(new UsedChannel()
        {
          Index = cha.Index,
          Mz = cha.Mz,
          Name = cha.Name
        });
      }
      return result;
    }

    public double[,] IsotopicTable { get; private set; }

    public Dictionary<string, int> NameIndexMap { get; private set; }

    public void Initialize()
    {
      this.NameIndexMap = new Dictionary<string, int>();
      for (int i = 0; i < Channels.Count; i++)
      {
        this.NameIndexMap[this.Channels[i].Name] = i;
      }

      this.IsotopicTable = new double[this.Channels.Count, this.Channels.Count];
      for (int i = 0; i < Channels.Count; i++)
      {
        var ci = Channels[i];
        for (int j = 0; j < Channels.Count; j++)
        {
          IsotopicTable[i, j] = 0.0;

          var cj = Channels[j];
          if (i == j)
          {
            IsotopicTable[i, j] = ci.Percentage;
            continue;
          }

          foreach (var iso in ci.Isotopics)
          {
            if (iso.Name.Equals(cj.Name))
            {
              IsotopicTable[i, j] = iso.Percentage;
              break;
            }
          }
        }
      }
    }

    public bool IsValid(string name)
    {
      return NameIndexMap.ContainsKey(name);
    }

    public int GetIndex(string name)
    {
      return NameIndexMap[name];
    }

    public Dictionary<IsobaricChannel, double> GetChannelMzToleranceMap(double ppmTolerance)
    {
      Dictionary<IsobaricChannel, double> result;

      if (!mzToleranceMap.TryGetValue(ppmTolerance, out result))
      {
        result = new Dictionary<IsobaricChannel, double>();
        foreach (var cha in this.Channels)
        {
          result[cha] = PrecursorUtils.ppm2mz(cha.Mz, ppmTolerance);
        }
        mzToleranceMap[ppmTolerance] = result;
      }

      return result;
    }

    public override string ToString()
    {
      return this.Name;
    }

    public object Clone()
    {
      var result = new IsobaricType();

      result.Name = Name;

      result.TagMassH = TagMassH;

      result.Channels = (from channel in Channels
                         select channel.Clone() as IsobaricChannel).ToList();

      result.IsotopicTable = IsotopicTable;

      result.NameIndexMap = NameIndexMap;

      return result;
    }
  }
}
