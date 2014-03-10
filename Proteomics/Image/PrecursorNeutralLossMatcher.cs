using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Image
{
  public class PrecursorNeutralLossMatcher : AbstractNeutralLossMatcher
  {
    private double minPrecursorNeutralLossIntensityScale;

    public PrecursorNeutralLossMatcher(double peakMzTolerance, double minPrecursorNeutralLossIntensityScale)
      : base(peakMzTolerance)
    {
      this.minPrecursorNeutralLossIntensityScale = minPrecursorNeutralLossIntensityScale;
      this.GetDisplayName = GetDisplayNameHelper.GetPrecursorDisplayName;
    }

    public override void Match(IIdentifiedPeptideResult sr)
    {
      NeutralLossCandidates candidates = new NeutralLossCandidates(sr.Peptide);

      List<INeutralLossType> nlTypes = GetNeutralLossTypes(candidates.CanLossWater, candidates.CanLossAmmonia);

      PeakList<MatchedPeak> expPeaks = sr.ExperimentalPeakList;

      List<MatchedPeak> nlPeaks = GetNeutralLossPeaks(IonType.PRECURSOR_NEUTRAL_LOSS, new MatchedPeak(expPeaks.PrecursorMZ, 0.0, expPeaks.PrecursorCharge), nlTypes, (m => true));

      //nlPeaks.ForEach(m => Console.WriteLine(m.DisplayName + "," + m.Mz));

      double minNeutralLossIntensity = expPeaks.FindMaxIntensityPeak().Intensity * minPrecursorNeutralLossIntensityScale;

      MatchedPeakUtils.Match(expPeaks, nlPeaks, PeakMzTolerance, minNeutralLossIntensity);
    }
  }
}
