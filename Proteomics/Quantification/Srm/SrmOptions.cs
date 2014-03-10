using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Utils;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmOptions
  {
    public Dictionary<string, string> RawFiles { get; set; }

    public static double NoisePercentage = 0.1;

    public string DistillerSoftware { get; set; }

    public string ValidationSoftware { get; set; }

    public string DefinitionFileFormat { get; set; }

    public string DefinitionFile { get; set; }

    public double[] AllowedGaps { get; private set; }

    private double[] _precursorMassDistance;
    public double[] PrecursorMassDistance
    {
      get
      {
        return _precursorMassDistance;
      }
      set
      {
        _precursorMassDistance = value;
        var charges = new[] { 1, 2, 3 };
        AllowedGaps = (from d in _precursorMassDistance
                       from charge in charges
                       let v = d / charge
                       orderby v
                       select v).ToArray();
      }
    }

    public double RetentionTimeToleranceInSecond { get; set; }

    /// <summary>
    /// 是否进行平滑
    /// </summary>
    public bool AskForSmooth { get; set; }

    /// <summary>
    /// 根据基线噪音进行区间选择，对于Triple Q数据，建议采用此项。对于TSQ数据，不建议。
    /// </summary>
    public bool RangeSelectionByNoise { get; set; }

    /// <summary>
    /// 取丰度最低的百分之几的峰进行噪音计算
    /// </summary>
    public double LowestPercentageForNoise { get; set; }

    /// <summary>
    /// 进行区间选择的最小信噪比
    /// </summary>
    public double MinPeakPickingSignalToNoise { get; set; }

    /// <summary>
    /// 如果不根据基线噪音进行区间选择，那么，就用最高峰的百分之几作为区间选择筛选标准
    /// </summary>
    public double PercentageOfHighestPeak { get; set; }

    /// <summary>
    /// 计算轻重比线性回归的时候是否去除基线。去除的话，就过原点，否则，就会有截距。
    /// 当基线较稳定时，例如Triple Q仪器数据，建议去除基线。
    /// </summary>
    public bool DeductBaseLine { get; set; }

    public double MzTolerance { get; set; }

    public double ValidationMinSignalToNoise { get; set; }

    public double ValidationMinRegressionCorrelation { get; set; }

    public int MinValidTransitionPair { get; set; }

    public double MaxPrecursorDistance { get; set; }

    //public double MinRetentionWindowInMinute { get; set; }

    public int MinEnabledScan { get; set; }

    public bool HasDecoy { get; set; }

    public string DecoyPattern { get; set; }

    public double TransitionFdr { get; set; }

    public double OutlierEvalue { get; set; }

    public bool RatioByArea { get; set; }

    public bool RefineData { get; set; }

    public SrmOptions()
    {
      this.DistillerSoftware = string.Empty;
      this.ValidationSoftware = string.Empty;
      this.DefinitionFileFormat = new SrmTransitionAtaqsReader().ToString();
      this.DefinitionFile = string.Empty;
      this.PrecursorMassDistance = new double[] { 7, 8, 10 };
      this.RetentionTimeToleranceInSecond = 1.0;
      this.RangeSelectionByNoise = true;
      this.LowestPercentageForNoise = 0.05;
      this.MinPeakPickingSignalToNoise = 3;
      this.PercentageOfHighestPeak = 0.05;
      this.AskForSmooth = true;
      this.DeductBaseLine = true;
      this.MzTolerance = 0.1;
      this.ValidationMinSignalToNoise = 3;
      this.ValidationMinRegressionCorrelation = 0.5;
      this.MinValidTransitionPair = 1;
      this.MaxPrecursorDistance = 20;
      //this.MinRetentionWindowInMinute = 0;
      this.MinEnabledScan = 0;
      this.HasDecoy = false;
      this.DecoyPattern = "DECOY";
      this.TransitionFdr = 0.05;
      this.OutlierEvalue = 0.0001;
      this.RatioByArea = false;
      this.RefineData = true;
    }

    public IFileReader2<List<SrmTransition>> GetReader()
    {
      var result = SrmFormatFactory.FindReader(this.DefinitionFileFormat);
      if (result == null)
      {
        result = SrmFormatFactory.GetDefaultReader();
      }
      return result;
    }

    private XElement ParameterToElement(string name, string value)
    {
      return new XElement("Param", new XAttribute("Name", name), new XAttribute("Value", value));
    }

    public XElement ToXml()
    {
      return ToXml(false);
    }

    public XElement ToXml(bool containsPathMap)
    {
      var gapstr = StringUtils.Merge(from a in this.PrecursorMassDistance
                                     let b = MyConvert.Format("{0:0.##}", a)
                                     select b, ',');

      var result = new XElement("Parameters",
        ParameterToElement("Software", DistillerSoftware),
        ParameterToElement("ValidationSoftware", ValidationSoftware),
        ParameterToElement("DefinitionFileFormat", DefinitionFileFormat),
        ParameterToElement("DefinitionFile", DefinitionFile),
        ParameterToElement("PrecursorMassDistance", gapstr),
        ParameterToElement("RetentionTimeToleranceInSecond", MyConvert.Format("{0:0.00}", this.RetentionTimeToleranceInSecond)),
        ParameterToElement("RangeSelectionByNoise", this.RangeSelectionByNoise.ToString()),
        ParameterToElement("LowestPercentageForNoise", MyConvert.Format("{0:0.0}", this.LowestPercentageForNoise)),
        ParameterToElement("MinPeakPickingSignalToNoise", MyConvert.Format("{0:0.0}", this.MinPeakPickingSignalToNoise)),
        ParameterToElement("PercentageOfHighestPeak", MyConvert.Format("{0:0.0}", this.PercentageOfHighestPeak)),
        ParameterToElement("SmoothType", this.AskForSmooth ? "SavitzkyGolay5Point" : "none"),
        ParameterToElement("DeductBaseLine", this.DeductBaseLine.ToString()),
        ParameterToElement("MzTolerance", MyConvert.Format("{0:0.0}", this.MzTolerance)),
        ParameterToElement("ValidationMinSignalToNoise", MyConvert.Format("{0:0.0}", this.ValidationMinSignalToNoise)),
        ParameterToElement("ValidationMinRegressionCorrelation", MyConvert.Format("{0:0.00}", this.ValidationMinRegressionCorrelation)),
        ParameterToElement("MinValidTransactionPair", this.MinValidTransitionPair.ToString()),
        ParameterToElement("MaxPrecursorDistance", MyConvert.Format("{0:0.0000}", this.MaxPrecursorDistance)),
        //ParameterToElement("MinRetentionWindowInMinute", MyConvert.Format("{0:0.0000}", this.MinRetentionWindowInMinute)),
        ParameterToElement("MinEnabledScan", this.MinEnabledScan.ToString()),
        ParameterToElement("HasDecoy", this.HasDecoy.ToString()),
        ParameterToElement("DecoyPattern", this.DecoyPattern),
        ParameterToElement("TransitionFdr", MyConvert.Format("{0:0.0000}", this.TransitionFdr)),
        ParameterToElement("OutlierEvalue", MyConvert.Format("{0:0.0000}", this.OutlierEvalue)),
        ParameterToElement("RatioByArea", this.RatioByArea.ToString()),
        ParameterToElement("RefineData", this.RefineData.ToString()));

      if (containsPathMap)
      {
        result.Add(new XElement("RawFiles",
          from file in RawFiles
          select new XElement("RawFile",
            new XElement("FileName", file.Key),
            new XElement("Sample", file.Value))));
      }

      return result;
    }

    public void FromXml(XElement root)
    {
      XElement paramEle;
      if (root.Name.LocalName.Equals("Parameters"))
      {
        paramEle = root;
      }
      else
      {
        paramEle = root.Element("Parameters");
      }

      if (paramEle == null)
      {
        return;
      }

      foreach (var ele in paramEle.Elements())
      {
        if (ele.Name.LocalName.Equals("RawFiles"))
        {
          this.RawFiles = new Dictionary<string, string>();
          foreach (var file in ele.Elements("RawFile"))
          {
            this.RawFiles[file.Element("FileName").Value] = file.Element("Sample").Value;
          }
          continue;
        }

        var name = ele.Attribute("Name").Value;
        var value = ele.Attribute("Value").Value;

        if (name.Equals("Software"))
        {
          this.DistillerSoftware = value;
        }
        if (name.Equals("ValidationSoftware"))
        {
          this.ValidationSoftware = value;
        }
        else if (name.Equals("DefinitionFileFormat"))
        {
          this.DefinitionFileFormat = value;
        }
        else if (name.Equals("DefinitionFile"))
        {
          this.DefinitionFile = value;
        }
        else if (name.Equals("PrecursorMassDistance"))
        {
          this.PrecursorMassDistance = (from p in value.Split(new char[] { ',', ';' })
                                        let m = MyConvert.ToDouble(p.Trim())
                                        select m).ToArray();
        }
        else if (name.Equals("RetentionTimeToleranceInSecond"))
        {
          this.RetentionTimeToleranceInSecond = MyConvert.ToDouble(value);
        }
        else if (name.Equals("RangeSelectionByNoise"))
        {
          this.RangeSelectionByNoise = Convert.ToBoolean(value);
        }
        else if (name.Equals("LowestPercentageForNoise"))
        {
          this.LowestPercentageForNoise = MyConvert.ToDouble(value);
        }
        else if (name.Equals("MinPeakPickingSignalToNoise"))
        {
          this.MinPeakPickingSignalToNoise = MyConvert.ToDouble(value);
        }
        else if (name.Equals("PercentageOfHighestPeak"))
        {
          this.PercentageOfHighestPeak = MyConvert.ToDouble(value);
        }
        else if (name.Equals("SmoothType"))
        {
          this.AskForSmooth = !value.ToLower().Equals("none");
        }
        else if (name.Equals("DeductBaseLine"))
        {
          this.DeductBaseLine = Convert.ToBoolean(value);
        }
        else if (name.Equals("MzTolerance"))
        {
          this.MzTolerance = MyConvert.ToDouble(value);
        }
        else if (name.Equals("ValidationMinSignalToNoise"))
        {
          this.ValidationMinSignalToNoise = MyConvert.ToDouble(value);
        }
        else if (name.Equals("ValidationMinRegressionCorrelation"))
        {
          this.ValidationMinRegressionCorrelation = MyConvert.ToDouble(value);
        }
        else if (name.Equals("MinValidTransactionPair"))
        {
          this.MinValidTransitionPair = Convert.ToInt32(value);
        }
        else if (name.Equals("MaxPrecursorDistance"))
        {
          this.MaxPrecursorDistance = MyConvert.ToDouble(value);
        }
        //else if (name.Equals("MinRetentionWindowInMinute"))
        //{
        //  this.MinRetentionWindowInMinute = MyConvert.ToDouble(value);
        //}
        else if (name.Equals("MinEnabledScan"))
        {
          this.MinEnabledScan = Convert.ToInt32(value);
        }
        else if (name.Equals("HasDecoy"))
        {
          this.HasDecoy = Convert.ToBoolean(value);
        }
        else if (name.Equals("DecoyPattern"))
        {
          this.DecoyPattern = value;
        }
        else if (name.Equals("TransitionFdr"))
        {
          this.TransitionFdr = MyConvert.ToDouble(value);
        }
        else if (name.Equals("OutlierEvalue"))
        {
          this.OutlierEvalue = MyConvert.ToDouble(value);
        }
        else if (name.Equals("RatioByArea"))
        {
          this.RatioByArea = Convert.ToBoolean(value);
        }
        else if (name.Equals("RefineData"))
        {
          this.RefineData = Convert.ToBoolean(value);
        }
      }
    }

    public ISrmPairedProductIonFilter GetFilter()
    {
      var filters = new ISrmPairedProductIonFilter[]
      {
        new SrmPairedProductIonRatioFilter(),
        new SrmPairedProductIonSignalToNoiseFilter(this.ValidationMinSignalToNoise),
        new SrmPairedProductIonRegressionCorrelationFilter(this.ValidationMinRegressionCorrelation),
        new SrmPairedProductIonMinimumEnabledScanFilter(this.MinEnabledScan)
        //new SrmPairedProductIonMinimumRetentionWindowFilter(this.MinRetentionWindowInMinute)
      };

      return new SrmPairedProductIonAndFilter(filters);
    }

    public IRangeSelection GetPeakPickingMethod()
    {
      if (this.RangeSelectionByNoise)
      {
        return new BaselineRangeSelection(this.LowestPercentageForNoise, this.MinPeakPickingSignalToNoise);
      }
      else
      {
        return new HighestPeakRangeSelection(this.PercentageOfHighestPeak);
      }
    }

    private class DecoyFilter : IFilter<SrmPairedProductIon>
    {
      private Regex reg;
      public DecoyFilter(string decoyPattern)
      {
        reg = new Regex(decoyPattern);
      }

      #region IFilter<SrmPairedProductIon> Members

      public bool Accept(SrmPairedProductIon t)
      {
        return reg.IsMatch(t.ObjectName) || reg.IsMatch(t.PrecursorFormula);
      }

      #endregion
    }

    public IFilter<SrmPairedProductIon> GetDecoyFilter()
    {
      if (HasDecoy)
      {
        return new DecoyFilter(this.DecoyPattern);
      }
      else
      {
        return new FilterFalse<SrmPairedProductIon>();
      }
    }
  }
}
