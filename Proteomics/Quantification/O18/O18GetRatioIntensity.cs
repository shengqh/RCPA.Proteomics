using System;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18GetRatioIntensity : IGetRatioIntensity
  {
    public double GetRatio(IAnnotation si)
    {
      try
      {
        return MyConvert.ToDouble(si.Annotations[RatioKey]);
      }
      catch (Exception)
      {
        return double.NaN;
      }
    }

    public double GetReferenceIntensity(IAnnotation si)
    {
      return GetIntensity(si, ReferenceKey);
    }

    public double GetSampleIntensity(IAnnotation si)
    {
      return GetIntensity(si, SampleKey);
    }

    private static double GetIntensity(IAnnotation si, string key)
    {
      if (!si.Annotations.ContainsKey(key))
      {
        throw new Exception("There is no intensity information of " + key);
      }
      return MyConvert.ToDouble(si.Annotations[key]);
    }

    public string RatioKey
    {
      get { return O18QuantificationConstants.O18_RATIO; }
    }

    public string ReferenceKey
    {
      get { return O18QuantificationConstants.O16_INTENSITY; }
    }

    public string SampleKey
    {
      get { return O18QuantificationConstants.O18_INTENSITY; }
    }

    #region IGetRatioIntensity Members


    public bool HasRatio(IAnnotation si)
    {
      if (!si.Annotations.ContainsKey(RatioKey))
      {
        return false;
      }

      double value;
      return MyConvert.TryParse(si.Annotations[RatioKey], out value);
    }

    #endregion
  }
}