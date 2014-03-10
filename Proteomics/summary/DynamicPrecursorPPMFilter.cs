using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;

namespace RCPA.Proteomics.Summary
{
  public class DynamicPrecursorPPMFilter
  {
    private class FilterItem
    {
      public double Offset;
      public int SigmaFold;
      public double Mean;
      public double Sigma;
    }

    private class Filter
    {
      private double ppmMean;
      private double ppmTolerance;
      public Filter(double mean, double sigma, int sigmaFold)
      {
        this.ppmMean = mean;
        this.ppmTolerance = sigma * sigmaFold;
      }

      public Filter(FilterItem item)
        : this(item.Mean, item.Sigma, item.SigmaFold)
      { }

      #region IFilter<IIdentifiedSpectrum> Members

      public bool Accept(double ppm)
      {
        return Math.Abs(ppm - ppmMean) <= ppmTolerance;
      }

      #endregion
    }

    private class FixedPrecursorPPMFilter : IFilter<IIdentifiedSpectrum>
    {
      private List<FilterItem> items;
      private List<Filter> filters;

      public FixedPrecursorPPMFilter(List<FilterItem> items)
      {
        this.items = new List<FilterItem>(items);
        this.filters = new List<Filter>();
        foreach (var item in items)
        {
          filters.Add(new Filter(item));
        }
      }
      #region IFilter<IIdentifiedSpectrum> Members

      public bool Accept(IIdentifiedSpectrum t)
      {
        if (this.items.Count == 0)
        {
          return true;
        }

        for (int i = 0; i < items.Count; i++)
        {
          double ppm = PrecursorUtils.mz2ppm(t.TheoreticalMH, t.TheoreticalMinusExperimentalMass + items[i].Offset);
          if (filters[i].Accept(ppm))
          {
            return true;
          }
        }

        return false;
      }

      #endregion
    }

    private double maxPPM;
    private bool filterByPrecursorIsotopic;

    public DynamicPrecursorPPMFilter(double maxPPM, bool filterByPrecursorIsotopic)
    {
      this.maxPPM = maxPPM;
      this.filterByPrecursorIsotopic = filterByPrecursorIsotopic;
    }

    public IFilter<IIdentifiedSpectrum> GetFilter(IEnumerable<IIdentifiedSpectrum> t)
    {
      var result = new List<FilterItem>();

      List<double> iso0 = new List<double>();
      List<double> iso1 = new List<double>();
      List<double> iso2 = new List<double>();
      foreach (var pep in t)
      {
        var iso0ppm = PrecursorUtils.mz2ppm(pep.TheoreticalMass, pep.TheoreticalMinusExperimentalMass);
        if (Math.Abs(iso0ppm) < this.maxPPM)
        {
          iso0.Add(iso0ppm);
          continue;
        }

        if (filterByPrecursorIsotopic)
        {
          var iso1ppm = PrecursorUtils.mz2ppm(pep.TheoreticalMass, pep.TheoreticalMinusExperimentalMass + IsotopicConsts.AVERAGE_ISOTOPIC_MASS);
          if (Math.Abs(iso1ppm) < this.maxPPM)
          {
            iso1.Add(iso1ppm);
            continue;
          }

          var iso2ppm = PrecursorUtils.mz2ppm(pep.TheoreticalMass, pep.TheoreticalMinusExperimentalMass + IsotopicConsts.DOUBLE_AVERAGE_ISOTOPIC_MASS);
          if (Math.Abs(iso2ppm) < this.maxPPM)
          {
            iso2.Add(iso2ppm);
          }
        }
      }

      List<FilterItem> items = new List<FilterItem>();

      AddFilter(items, iso0, 0.0, 3);
      if (filterByPrecursorIsotopic)
      {
        if (iso1.Count == 0)
        {
          iso1 = iso0;
        }

        if (iso2.Count == 0)
        {
          iso2 = iso1;
        }

        AddFilter(items, iso1, IsotopicConsts.AVERAGE_ISOTOPIC_MASS, 2);
        AddFilter(items, iso2, IsotopicConsts.DOUBLE_AVERAGE_ISOTOPIC_MASS, 1);
      }

      return new FixedPrecursorPPMFilter(items);
    }

    private static void AddFilter(List<FilterItem> items, List<double> iso, double offset, int sigmaFold)
    {
      if (iso.Count > 0)
      {
        MeanStandardDeviation acc = new MeanStandardDeviation(iso.ToArray());
        items.Add(new FilterItem()
        {
          Offset = offset,
          Mean = acc.Mean,
          Sigma = acc.StdDev,
          SigmaFold = sigmaFold
        });
      }
    }
  }
}
