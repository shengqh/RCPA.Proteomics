using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Image
{
  public class PeakIonSeriesMatcher : IMatcher
  {
    private IonType ionType;

    private double mzTolerance;

    private double minIntensityScale;

    public PeakIonSeriesMatcher(IonType ionType, double mzTolerance)
      : this(ionType, mzTolerance, 0.0)
    { }

    public PeakIonSeriesMatcher(IonType ionType, double mzTolerance, double minIntensityScale)
    {
      this.ionType = ionType;
      this.mzTolerance = mzTolerance;
      this.minIntensityScale = minIntensityScale;
    }

    public void Match(IIdentifiedPeptideResult sr)
    {
      List<MatchedPeak> expPeaks = sr.ExperimentalPeakList;

      double minIntensity = sr.ExperimentalPeakList.FindMaxIntensityPeak().Intensity * minIntensityScale;

      MatchedPeakUtils.Match(expPeaks, sr.GetIonSeries()[ionType], mzTolerance, minIntensity);
    }

  }
}
