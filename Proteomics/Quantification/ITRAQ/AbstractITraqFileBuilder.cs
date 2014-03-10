using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Spectrum;
using MathNet.Numerics.Distributions;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqFileBuilder
  {
    public IsobaricDefinition Definition { get; private set; }

    public ITraqFileBuilder(IsobaricDefinition definition)
    {
      this.Definition = definition;
    }

    protected double GetPeakIntensity(Peak peak)
    {
      if (peak == null)
      {
        return ITraqConsts.NULL_INTENSITY;
      }
      else
      {
        return peak.Intensity;
      }
    }

    protected void CheckPeakNull(ref Peak peak, ref int nullCount)
    {
      if (peak == null)
      {
        nullCount++;
        peak = new Peak(0, ITraqConsts.NULL_INTENSITY);
      }
    }

    protected void AddToDistance(List<double> distances, PeakList<Peak> pkl, double theoreticalMz)
    {
      Peak peak = pkl.FindPeak(theoreticalMz, 0.45).FindMaxIntensityPeak();
      if (peak != null)
      {
        distances.Add(peak.Mz - theoreticalMz);
      }
    }

    protected List<MeanStandardDeviation> DoGetDistance(List<PeakList<Peak>> pkls, double[] ions)
    {
      var result = new List<MeanStandardDeviation>();
      foreach (double ion in ions)
      {
        List<double> distances = new List<double>();

        foreach (PeakList<Peak> pkl in pkls)
        {
          AddToDistance(distances, pkl, ion);
        }

        result.Add(new MeanStandardDeviation(distances));
      }

      return result;
    }

    public virtual List<MeanStandardDeviation> GetDistances(IEnumerable<PeakList<Peak>> pkls)
    {
      double[] ions = GetIons();

      var result = new List<MeanStandardDeviation>();
      foreach (double ion in ions)
      {
        List<double> distances = new List<double>();

        foreach (PeakList<Peak> pkl in pkls)
        {
          AddToDistance(distances, pkl, ion);
        }

        if (distances.Count <= 1)
        {
          throw new Exception(string.Format("There is no iTraq ion {0:0.##}.", ion));
        }
        else
        {
          result.Add(new MeanStandardDeviation(distances));
        }
      }

      return result;
    }

    public virtual IsobaricResult GetITraqResult(List<IsobaricItem> pkls, List<MeanStandardDeviation> accs, double peakFolderTolerance, int minPeakCount)
    {
      IsobaricResult result = new IsobaricResult();

      double[] ions = GetIons();
      List<double> means = new List<double>();
      List<double> peakTolerances = new List<double>();
      for (int i = 0; i < ions.Length; i++)
      {
        var ion = ions[i];
        means.Add(ion + accs[i].Mean);
        peakTolerances.Add(accs[i].StdDev * peakFolderTolerance);
      }

      List<Peak> peaks = new List<Peak>();
      foreach (var pkl in pkls)
      {
        peaks.Clear();
        int nullCount = 0;
        for (int i = 0; i < means.Count; i++)
        {
          Peak peak = pkl.RawPeaks.FindPeak(means[i], peakTolerances[i]).FindMaxIntensityPeak();
          CheckPeakNull(ref peak, ref nullCount);
          peaks.Add(peak);
        }

        if (nullCount > ions.Length - minPeakCount)
        {
          continue;
        }

        AssignItraqItem(pkl, peaks);

        result.Add(pkl);
      }
      return result;
    }

    private double[] GetIons()
    {
      return (from def in Definition.Items
              select def.Mass).ToArray();
    }

    private void AssignItraqItem(IsobaricItem pkl, List<Peak> peaks)
    {
      pkl.Scan = pkl.RawPeaks.ScanTimes[0];
      pkl.PlexType = Definition.PlexType;
      for (int i = 0; i < Definition.Items.Length; i++)
      {
        pkl[Definition.Items[i].Index] = GetPeakIntensity(peaks[i]);
      }
    }
  }
}
