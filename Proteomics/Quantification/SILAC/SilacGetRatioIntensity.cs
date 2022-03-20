namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacGetRatioIntensity : AbstractGetRatioIntensity
  {
    #region IGetRatioIntensity Members

    public override string RatioKey
    {
      get { return "SILAC_RATIO"; }
    }

    public override string ReferenceKey
    {
      get { return "Reference"; }
    }

    public override string SampleKey
    {
      get { return "Sample"; }
    }

    public override double GetRatio(IAnnotation si)
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

    public override double GetReferenceIntensity(IAnnotation si)
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

    public override double GetSampleIntensity(IAnnotation si)
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

    public override bool HasRatio(IAnnotation si)
    {
      return si.HasRatio();
    }

    #endregion

    public override string PValueKey
    {
      get { return "PValue"; }
    }

    public override double GetPValue(IAnnotation si)
    {
      return si.GetDoubleValue(PValueKey);
    }
  }
}