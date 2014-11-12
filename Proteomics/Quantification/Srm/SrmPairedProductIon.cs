using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using RCPA.Utils;
using MathNet.Numerics.LinearAlgebra.Double;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmPairedProductIon
  {
    public string FileName { get; set; }

    public bool IsCurrent { get; set; }

    public bool IsDecoy
    {
      get
      {
        if (Light != null)
        {
          return Light.IsDecoy;
        }
        if (Heavy != null)
        {
          return Heavy.IsDecoy;
        }

        return false;
      }
      set
      {
        if (Light != null)
        {
          Light.IsDecoy = value;
        }
        if (Heavy != null)
        {
          Heavy.IsDecoy = value;
        }
      }
    }

    public double Qvalue { get; set; }

    public string ObjectName
    {
      get
      {
        if (Light != null)
        {
          return Light.ObjectName;
        }
        if (Heavy != null)
        {
          return Heavy.ObjectName;
        }

        return string.Empty;
      }
    }

    public string PrecursorFormula
    {
      get
      {
        if (Light != null)
        {
          return Light.PrecursorFormula;
        }
        if (Heavy != null)
        {
          return Heavy.PrecursorFormula;
        }

        return string.Empty;
      }
    }

    public int PrecursorCharge
    {
      get
      {
        if (Light != null)
        {
          return Light.PrecursorCharge;
        }
        if (Heavy != null)
        {
          return Heavy.PrecursorCharge;
        }

        return 0;
      }
    }

    public bool Enabled { get; set; }
    public double Ratio { get; set; }
    public double Distance { get; set; }
    public double RegressionCorrelation { get; set; }
    public double LightArea { get; set; }
    public double HeavyArea { get; set; }

    private bool _deductBaseLine = false;
    public bool DeductBaseLine { get { return _deductBaseLine; } }

    public bool IsPaired
    {
      get
      {
        return this.Light != null && this.Heavy != null;
      }
    }

    public double Noise
    {
      get
      {
        return this.Light == null ? 1 : this.Light.Noise;
      }
    }

    public double LightSignalToNoise
    {
      get
      {
        return this.Light == null ? 0 : this.Light.SignalToNoise;
      }
    }

    public double HeavySignalToNoise
    {
      get
      {
        return this.Heavy == null ? 0 : this.Heavy.SignalToNoise;
      }
    }

    public double LightMaxIntensity
    {
      get
      {
        return GetMaxIntensity(this.Light);
      }
    }

    public double HeavyMaxIntensity
    {
      get
      {
        return GetMaxIntensity(this.Heavy);
      }
    }

    public double LightMaxEnabledIntensity
    {
      get
      {
        return GetMaxIntensity(this.Light, m => m.Enabled);
      }
    }

    public double HeavyMaxEnabledIntensity
    {
      get
      {
        return GetMaxIntensity(this.Heavy, m => m.Enabled);
      }
    }

    public double MaxIntensity
    {
      get
      {
        return Math.Max(LightMaxIntensity, HeavyMaxIntensity);
      }
    }

    private double GetMaxIntensity(SrmTransition trans)
    {
      if (trans == null)
      {
        return 0.0;
      }

      return trans.Intensities.Max(m => m.Intensity);
    }

    private double GetMaxIntensity(SrmTransition trans, Func<SrmScan, bool> valid)
    {
      if (trans == null || trans.EnabledCount == 0)
      {
        return 0.0;
      }

      return (from scan in trans.Intensities
              where valid(scan)
              select scan.Intensity).Max();
    }

    public int EnabledScanCount
    {
      get
      {
        if (Light != null)
        {
          return Light.EnabledCount;
        }

        if (Heavy != null)
        {
          return Heavy.EnabledCount;
        }

        return 0;
      }
    }

    public double EnabledRetentionWindow
    {
      get
      {
        if (Light != null)
        {
          return Light.EnabledRetentionWindow;
        }
        if (Light != null)
        {
          return Light.EnabledRetentionWindow;
        }
        return 0;
      }
    }

    public SrmPairedProductIon()
    {
      this.Enabled = true;
    }

    public SrmPairedProductIon(SrmTransition light)
    {
      this.Enabled = true;
      this.Light = light;
    }

    public SrmPairedProductIon(SrmTransition light, SrmTransition heavy)
    {
      this.Enabled = true;

      this.Light = light;
      this.Heavy = heavy;
    }

    private SrmTransition _light;
    public SrmTransition Light
    {
      get
      {
        return _light;
      }
      set
      {
        _light = value;
        AlignLightAndHeavy();
      }
    }

    private SrmTransition _heavy;
    public SrmTransition Heavy
    {
      get
      {
        return _heavy;
      }
      set
      {
        _heavy = value;
        AlignLightAndHeavy();
      }
    }

    public void AlignLightAndHeavy()
    {
      if (Light == null || Heavy == null || Light.Intensities.Count == 0 || Heavy.Intensities.Count == 0)
      {
        return;
      }

      int i = 0;
      while (i < Light.Intensities.Count && i < Heavy.Intensities.Count)
      {
        double d00 = Math.Abs(Light.Intensities[i].RetentionTime - Heavy.Intensities[i].RetentionTime);
        double d01 = double.MaxValue;
        double d10 = double.MaxValue;

        if (i < Heavy.Intensities.Count - 1)
        {
          d01 = Math.Abs(Light.Intensities[i].RetentionTime - Heavy.Intensities[i + 1].RetentionTime);
        }

        if (i < Light.Intensities.Count - 1)
        {
          d10 = Math.Abs(Light.Intensities[i + 1].RetentionTime - Heavy.Intensities[i].RetentionTime);
        }

        if (d00 > d01)
        {
          Heavy.Intensities.RemoveAt(i);
          continue;
        }

        if (d00 > d10)
        {
          Light.Intensities.RemoveAt(i);
          continue;
        }

        i++;
      }

      if (Light.Intensities.Count != Heavy.Intensities.Count)
      {
        Light.Intensities.RemoveRange(i, Light.Intensities.Count - i);
        Heavy.Intensities.RemoveRange(i, Heavy.Intensities.Count - i);
      }
    }

    private double[][] GetIntensityArray()
    {
      CheckAlignment();

      var deductValue = _deductBaseLine ? Math.Min(Light.Noise, Heavy.Noise) : 0;
      List<double> lightIntensities = Light.GetEnabledIntensities(deductValue);
      List<double> heavyIntensities = Heavy.GetEnabledIntensities(deductValue);

      if (lightIntensities.Count != heavyIntensities.Count)
      {
        var minCount = Math.Min(lightIntensities.Count, heavyIntensities.Count);
        lightIntensities.RemoveRange(minCount, lightIntensities.Count - minCount);
        heavyIntensities.RemoveRange(minCount, heavyIntensities.Count - minCount);
      }

      var result = new double[2][];
      result[1] = lightIntensities.ToArray();
      result[0] = heavyIntensities.ToArray();

      return result;
    }

    private void CheckAlignment()
    {
      if (Light != null && Heavy != null && Light.Intensities.Count != Heavy.Intensities.Count)
      {
        AlignLightAndHeavy();
      }
    }

    private void CalculateRatioAndArea(bool deductBaseLine)
    {
      if (Light != null && Heavy != null)
      {
        this._deductBaseLine = deductBaseLine;

        CalculateRatio();
        CalculateArea();
      }
    }

    public void CalculateRatio(SrmOptions options)
    {
      if (this.IsPaired)
      {
        CalculateRatioAndArea(options.DeductBaseLine);

        if (options.RatioByArea)
        {
          this.Ratio = this.LightArea / this.HeavyArea;
        }
      }
    }

    private void CalculateRatio()
    {
      double[][] intensities = GetIntensityArray();

      if (intensities[0].Length <= 1)
      {
        Ratio = 1;
        Distance = 0;
        RegressionCorrelation = 0;
        return;
      }

      if (intensities[0].Distinct().Count() == 1 || intensities[1].Distinct().Count() == 1)
      {
        var avea = intensities[0].Average();
        var aveb = intensities[1].Average();
        if (avea == 0.0 || aveb == 0.0)
        {
          Ratio = 1;
          Distance = 0;
          RegressionCorrelation = 0;
          return;
        }

        Ratio = aveb / avea;
        Distance = 0;
        RegressionCorrelation = 0;
        return;
      }

      LinearRegressionRatioResult lrrr;
      if (_deductBaseLine)
      {
        lrrr = LinearRegressionRatioCalculator.CalculateRatio(intensities);
      }
      else
      {
        lrrr = LinearRegressionRatioCalculator.CalculateRatioDistance(intensities);
      }

      Ratio = lrrr.Ratio;
      Distance = lrrr.Distance;
      RegressionCorrelation = lrrr.RSquare;
    }

    private void CalculateArea()
    {
      LightArea = GetLightArea();
      HeavyArea = GetHeavyArea();
    }

    public void SetHeavyIon(SrmPairedProductIon heavy)
    {
      this.Heavy = heavy.Light;
    }

    public string GetFormula()
    {
      if (Light != null && Heavy != null)
      {
        string dist = "";
        if (!_deductBaseLine)
        {
          string sig = Distance >= 0 ? "+" : "-";
          dist = MyConvert.Format("{0} {1:0.0} ", sig, Math.Abs(Distance));
        }

        return MyConvert.Format("Light = {0:0.0000} * Heavy {1}; Correl = {2:0.0000}", Ratio, dist, RegressionCorrelation);
      }
      else
      {
        return null;
      }
    }

    public string GetSignalToNoise()
    {
      StringBuilder sb = new StringBuilder();
      if (Light != null)
      {
        sb.AppendFormat("Light(N={0:0.0}; S/N={1:0.00})", Light.Noise, Light.SignalToNoise);
      }

      if (Heavy != null)
      {
        if (sb.Length > 0)
        {
          sb.AppendFormat(" ; ");
        }
        sb.AppendFormat("Heavy(N={0:0.0}; S/N={1:0.00})", Heavy.Noise, Heavy.SignalToNoise);
      }

      return sb.ToString();
    }

    private double GetArea(SrmTransition trans)
    {
      if (trans != null)
      {
        var enabled = (from s in trans.Intensities
                       where s.Enabled
                       select s.Intensity).ToList();

        return _deductBaseLine ? enabled.Sum() - Noise * enabled.Count : enabled.Sum();
      }

      return 0.0;
    }

    private double GetLightArea()
    {
      return GetArea(Light);
    }

    private double GetHeavyArea()
    {
      return GetArea(Heavy);
    }

    private int GetEnabledScanCount()
    {
      if (Light != null)
      {
        return Light.Intensities.Count(m => m.Enabled);
      }
      else if (Heavy != null)
      {
        return Heavy.Intensities.Count(m => m.Enabled);
      }

      return 0;
    }

    public void CalculateSignalToNoise()
    {
      if (Light != null)
      {
        Light.CalculateSignalToNoise();
      }

      if (Heavy != null)
      {
        Heavy.CalculateSignalToNoise();
      }
    }

    public void PeakPicking(IRangeSelection peakPicking)
    {
      if (Light == null && Heavy == null)
      {
        return;
      }

      CheckAlignment();

      //找到轻重标记中最高峰所在transaction
      SrmTransition maxTrans = null;
      SrmTransition another = null;
      if (Light == null)
      {
        maxTrans = Heavy;
      }
      else if (Heavy == null)
      {
        maxTrans = Light;
      }
      else if (LightMaxIntensity > HeavyMaxIntensity)
      {
        maxTrans = Light;
        another = Heavy;
      }
      else
      {
        maxTrans = Heavy;
        another = Light;
      }

      peakPicking.Select(maxTrans);

      //设置另外一半相对应的区域。
      if (another != null)
      {
        for (int i = 0; i < maxTrans.Intensities.Count; i++)
        {
          another.Intensities[i].Enabled = maxTrans.Intensities[i].Enabled;
        }
      }
    }

    public void SetEnabledRetentionTimeRange(double minRT, double maxRT)
    {
      CheckAlignment();

      if (Light == null && Heavy == null)
      {
        return;
      }

      if (Light != null)
      {
        Light.Intensities.ForEach(m => m.Enabled = m.RetentionTime >= minRT && m.RetentionTime <= maxRT);
      }

      if (Heavy != null)
      {
        Heavy.Intensities.ForEach(m => m.Enabled = m.RetentionTime >= minRT && m.RetentionTime <= maxRT);
      }

      if (Light != null && Heavy != null)
      {
        for (int i = 0; i < Light.Intensities.Count; i++)
        {
          var enabled = Light.Intensities[i].Enabled && Heavy.Intensities[i].Enabled;
          Light.Intensities[i].Enabled = enabled;
          Heavy.Intensities[i].Enabled = enabled;
        }
      }

      CalculateSignalToNoise();
    }

    public bool ProductIonEquals(SrmPairedProductIon another, double mzTolerance)
    {
      if (this.Light != null && another.Light != null)
      {
        if (Math.Abs(this.Light.ProductIon - another.Light.ProductIon) > mzTolerance)
        {
          return false;
        }
      }
      else if (this.Light != null || another.Light != null)
      {
        return false;
      }

      if (this.Heavy != null && another.Heavy != null)
      {
        if (Math.Abs(this.Heavy.ProductIon - another.Heavy.ProductIon) > mzTolerance)
        {
          return false;
        }
      }
      else if (this.Heavy != null || another.Heavy != null)
      {
        return false;
      }

      return true;
    }

    public double LightProductIon
    {
      get
      {
        if (Light != null)
        {
          return Light.ProductIon;
        }
        return 0;
      }
    }

    public double HeavyProductIon
    {
      get
      {
        if (Heavy != null)
        {
          return Heavy.ProductIon;
        }
        return 0;
      }
    }
  }
}
