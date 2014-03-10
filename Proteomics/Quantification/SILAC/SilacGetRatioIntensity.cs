using System;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacGetRatioIntensity : IGetRatioIntensity
  {
    #region IGetRatioIntensity Members

    public string RatioKey
    {
      get { throw new NotImplementedException(); }
    }

    public string ReferenceKey
    {
      get { return "Reference"; }
    }

    public string SampleKey
    {
      get { return "Sample"; }
    }

    public double GetRatio(IAnnotation si)
    {
      if (si.HasRatio())
      {
        return si.GetQuantificationItem().Ratio;
      }
      else
      {
        return 0.0;
      }
    }

    public double GetReferenceIntensity(IAnnotation si)
    {
      if (si.HasRatio())
      {
        return si.GetQuantificationItem().ReferenceIntensity;
      }
      else
      {
        return 0.0;
      }
    }

    public double GetSampleIntensity(IAnnotation si)
    {
      if (si.HasRatio())
      {
        return si.GetQuantificationItem().SampleIntensity;
      }
      else
      {
        return 0.0;
      }
    }

    public bool HasRatio(IAnnotation si)
    {
      return si.HasRatio();
    }

    #endregion
  }
}