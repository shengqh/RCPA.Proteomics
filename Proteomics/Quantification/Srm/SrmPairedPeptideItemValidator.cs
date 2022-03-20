using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public interface ISrmPairedProductIonFilter : IFilter<SrmPairedProductIon> { }

  public class SrmPairedProductIonRatioFilter : ISrmPairedProductIonFilter
  {
    public SrmPairedProductIonRatioFilter()
    {
    }

    #region IFilter<MRMPairedProductIon> Members

    public bool Accept(SrmPairedProductIon t)
    {
      return !t.IsPaired || t.Ratio > 0;
    }

    #endregion

    public override string ToString()
    {
      return MyConvert.Format("Ratio >= 0");
    }
  }

  public class SrmPairedProductIonSignalToNoiseFilter : ISrmPairedProductIonFilter
  {
    private double minSignalToNoise;

    public SrmPairedProductIonSignalToNoiseFilter(double minSignalToNoise)
    {
      this.minSignalToNoise = minSignalToNoise;
    }

    #region IFilter<MRMPairedProductIon> Members

    public bool Accept(SrmPairedProductIon t)
    {
      return (t.Light == null || t.LightSignalToNoise >= minSignalToNoise) &&
        (t.Heavy == null || t.HeavySignalToNoise >= minSignalToNoise);
    }

    #endregion

    public override string ToString()
    {
      return MyConvert.Format("SignalToNoise >= {0:0.0}", minSignalToNoise);
    }
  }

  public class SrmPairedProductIonRegressionCorrelationFilter : ISrmPairedProductIonFilter
  {
    private double minCorrel;

    public SrmPairedProductIonRegressionCorrelationFilter(double minCorrel)
    {
      this.minCorrel = minCorrel;
    }

    #region IFilter<MRMPairedProductIon> Members

    public bool Accept(SrmPairedProductIon t)
    {
      return !t.IsPaired || t.RegressionCorrelation >= minCorrel;
    }

    #endregion

    public override string ToString()
    {
      return MyConvert.Format("RegressionCorrelation >= {0:0.0}", minCorrel);
    }
  }

  public class SrmPairedProductIonMinimumRetentionWindowFilter : ISrmPairedProductIonFilter
  {
    private double minRetentionWindow;
    public SrmPairedProductIonMinimumRetentionWindowFilter(double minRetentionWindow)
    {
      this.minRetentionWindow = minRetentionWindow;
    }

    #region IFilter<SrmPairedProductIon> Members

    public bool Accept(SrmPairedProductIon t)
    {
      return t.EnabledRetentionWindow >= minRetentionWindow;
    }

    #endregion
  }

  public class SrmPairedProductIonMinimumEnabledScanFilter : ISrmPairedProductIonFilter
  {
    private int minEnabledScan;
    public SrmPairedProductIonMinimumEnabledScanFilter(int minEnabledScan)
    {
      this.minEnabledScan = minEnabledScan;
    }

    #region IFilter<SrmPairedProductIon> Members

    public bool Accept(SrmPairedProductIon t)
    {
      return t.EnabledScanCount >= minEnabledScan;
    }

    #endregion

    public override string ToString()
    {
      return MyConvert.Format("MinimumEnabledScan >= {0}", minEnabledScan);
    }
  }

  public class SrmPairedProductIonAndFilter : ISrmPairedProductIonFilter
  {
    private List<ISrmPairedProductIonFilter> filters;
    private string formula;

    public SrmPairedProductIonAndFilter(IEnumerable<ISrmPairedProductIonFilter> filters)
    {
      this.filters = new List<ISrmPairedProductIonFilter>(filters);
      StringBuilder sb = new StringBuilder();
      foreach (var filter in filters)
      {
        if (sb.Length > 0)
        {
          sb.Append(" and ");
        }
        sb.Append("(" + filter.ToString() + ")");
      }
      this.formula = sb.ToString();
    }

    #region IFilter<MRMPairedProductIon> Members

    public bool Accept(SrmPairedProductIon t)
    {
      foreach (var filter in filters)
      {
        if (!filter.Accept(t))
        {
          return false;
        }
      }

      return true;
    }

    #endregion

    public override string ToString()
    {
      return formula;
    }
  }

  public class SrmPairedProductIonOrFilter : ISrmPairedProductIonFilter
  {
    private List<ISrmPairedProductIonFilter> filters;
    private string formula;

    public SrmPairedProductIonOrFilter(IEnumerable<ISrmPairedProductIonFilter> filters)
    {
      this.filters = new List<ISrmPairedProductIonFilter>(filters);
      StringBuilder sb = new StringBuilder();
      foreach (var filter in filters)
      {
        if (sb.Length > 0)
        {
          sb.Append(" or ");
        }
        sb.Append("(" + filter.ToString() + ")");
      }
      this.formula = sb.ToString();
    }

    #region IFilter<MRMPairedProductIon> Members

    public bool Accept(SrmPairedProductIon t)
    {
      if (filters.Count == 0)
      {
        return true;
      }

      foreach (var filter in filters)
      {
        if (filter.Accept(t))
        {
          return true;
        }
      }

      return false;
    }

    #endregion

    public override string ToString()
    {
      return formula;
    }
  }
}
