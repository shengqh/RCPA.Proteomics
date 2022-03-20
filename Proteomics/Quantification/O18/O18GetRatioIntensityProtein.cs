using System;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18GetRatioIntensityProtein : O18GetRatioIntensity
  {
    public override string RatioKey
    {
      get { return O18QuantificationConstants.O18_RATIO; }
    }

    public override string ReferenceKey
    {
      get { return O18QuantificationConstants.O16_INTENSITY; }
    }

    public override string SampleKey
    {
      get { return O18QuantificationConstants.O18_INTENSITY; }
    }

    public override string PValueKey
    {
      get { return O18QuantificationConstants.O18_RATIO_PVALUE; }
    }

    public override double GetRatio(IAnnotation si)
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

    public override double GetReferenceIntensity(IAnnotation si)
    {
      return si.GetDoubleValue(ReferenceKey);
    }

    public override double GetSampleIntensity(IAnnotation si)
    {
      return si.GetDoubleValue(SampleKey);
    }

    public override double GetPValue(IAnnotation si)
    {
      return si.GetDoubleValue(PValueKey);
    }

    public override bool HasRatio(IAnnotation si)
    {
      return si.HasDoubleValue(RatioKey);
    }
  }
}