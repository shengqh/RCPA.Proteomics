using MathNet.Numerics;
using RCPA.Proteomics.Spectrum;
using RCPA.Utils;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Isotopic
{
  public class CarbonProfileBuilder : AbstractIsotopicProfileBuilder
  {
    private const double C13_ABUNDANCE = 0.01108;
    private const double MIN_ISOTOPIC_PERCENT = 0.001;

    private readonly Dictionary<int, List<double>> profiles = new Dictionary<int, List<double>>();

    public List<double> GetProfile(int carbonNumber)
    {
      if (!this.profiles.ContainsKey(carbonNumber))
      {
        this.profiles.Add(carbonNumber, CalculateProfile(carbonNumber));
      }

      return this.profiles[carbonNumber];
    }

    protected List<double> GetProfile(int carbonNumber, int profileLength)
    {
      int maxLength = Math.Min(carbonNumber, profileLength);
      if (!this.profiles.ContainsKey(carbonNumber))
      {
        this.profiles.Add(carbonNumber, CalculateProfile(carbonNumber, profileLength));
      }
      else if (this.profiles[carbonNumber].Count < maxLength)
      {
        this.profiles[carbonNumber] = CalculateProfile(carbonNumber, profileLength);
      }

      return this.profiles[carbonNumber];
    }

    public double GetAbundance(List<IPeak> isotopics, int carbonNumber)
    {
      if (0 == isotopics.Count || 0 == carbonNumber)
      {
        return 0.0;
      }

      List<double> profiles = GetProfile(carbonNumber);
      double percent = 0.0;
      double intensity = 0.0;
      int count = Math.Min(isotopics.Count, profiles.Count);
      for (int i = 0; i < count; i++)
      {
        percent += profiles[i];
        intensity += isotopics[i].Intensity;
      }

      return intensity / percent;
    }

    private List<double> CalculateProfile(int carbonNumber)
    {
      var result = new List<double>();
      int j = 0;
      while (j <= carbonNumber)
      {
        double comb = StatisticsUtils.GetCombinationProbability(j, carbonNumber, C13_ABUNDANCE, 1 - C13_ABUNDANCE);
        result.Add(comb);
        if (j > 0 && result[j] < result[j - 1] && result[j] < MIN_ISOTOPIC_PERCENT)
        {
          break;
        }
        j++;
      }
      return result;
    }

    public List<double> CalculateProfile(int carbonNumber, int profileLength)
    {
      var result = new List<double>();
      int j = 0;
      int maxCount = Math.Min(carbonNumber, profileLength);
      while (j <= maxCount)
      {
        double comb = Combinatorics.Combinations(carbonNumber, j);
        comb = comb * Math.Pow(C13_ABUNDANCE, j) * Math.Pow((1 - C13_ABUNDANCE), (carbonNumber - j));
        result.Add(comb);
        j++;
      }
      return result;
    }

    public override List<double> GetProfile(AtomComposition ac, int profileLength)
    {
      return GetProfile(ac.GetAtomCount(Atom.C), profileLength);
    }
  }
}