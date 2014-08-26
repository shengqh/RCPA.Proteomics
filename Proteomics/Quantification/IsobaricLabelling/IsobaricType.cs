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
      this.TagMass = new List<double>();
      this.NameIndexMap = new Dictionary<string, int>();
    }

    public string Name { get; set; }

    public List<double> TagMass { get; private set; }

    public List<IsobaricChannel> Channels { get; private set; }

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

    private int FindMinimumDistanceIndex(IsobaricChannel[] channels, double mz, HashSet<int> ignore)
    {
      var minDistance = double.MaxValue;
      var result = -1;
      for (int i = 0; i < channels.Length; i++)
      {
        if (ignore.Contains(i))
        {
          continue;
        }
        var dis = Math.Abs(channels[i].Mz - mz);
        if (dis < minDistance)
        {
          result = i;
          minDistance = dis;
        }
      }

      return result;
    }

    /// <summary>
    /// Calibrate the mass of each ion
    /// </summary>
    /// <typeparam name="T">Peak</typeparam>
    /// <param name="peaks">List of peak list</param>
    public void CalibrateMass<T>(IEnumerable<PeakList<T>> peaks) where T : Peak
    {
      var c = (from g in Channels.GroupBy(m => Math.Round(m.Mz))
               select new { Mz = g.Key, Channels = (from l in g orderby l.Mz select l).ToArray() }).OrderBy(m => m.Mz).ToList();
      foreach (var cc in c)
      {
        var ccList = new List<List<Peak>>();
        foreach (var ccc in cc.Channels)
        {
          ccList.Add(new List<Peak>());
        }

        HashSet<int> assigned = new HashSet<int>();
        foreach (var pkl in peaks)
        {
          var findpeaks = (from p in pkl.FindPeak(cc.Mz, 0.5) orderby p.Intensity descending select p).ToList();

          if (findpeaks.Count >= cc.Channels.Length)
          {
            var assignPeaks = findpeaks.Take(cc.Channels.Length).OrderBy(m => m.Mz).ToArray();
            for (int i = 0; i < assignPeaks.Length; i++)
            {
              assignPeaks[i].Tag = pkl.FirstScan;
              ccList[i].Add(assignPeaks[i]);
            }
            continue;
          }

          assigned.Clear();
          while (findpeaks.Count > 0)
          {
            findpeaks[0].Tag = pkl.FirstScan;
            var index = FindMinimumDistanceIndex(cc.Channels, findpeaks[0].Mz, assigned);
            ccList[index].Add(findpeaks[0]);
            assigned.Add(index);
            findpeaks.RemoveAt(0);
          }
        }

        for (int i = 0; i < cc.Channels.Length; i++)
        {
          //var newmz = MathNet.Numerics.Statistics.Statistics.Median(from peak in ccList[i] select peak.Mz);
          var newmz = ccList[i].Sum(l => l.Mz * l.Intensity) / ccList[i].Sum(l => l.Intensity);
          Console.WriteLine("{0} : {1} => {2}", cc.Channels[i].Name,
            cc.Channels[i].Mz,
            newmz);
          cc.Channels[i].Mz = newmz;

          using (var sw = new StreamWriter(string.Format(@"H:\{0}.tsv", cc.Channels[i].Name)))
          {
            ccList[i].ForEach(m => sw.WriteLine("{0}\t{1}\t{2}",m.Tag, m.Mz, m.Intensity));
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

      result.TagMass = TagMass;

      result.Channels = (from channel in Channels
                         select channel.Clone() as IsobaricChannel).ToList();

      result.IsotopicTable = IsotopicTable;

      result.NameIndexMap = NameIndexMap;

      return result;
    }
  }
}
