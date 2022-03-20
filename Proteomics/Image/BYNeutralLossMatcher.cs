using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Image
{
  public class BYNeutralLossMatcher : AbstractNeutralLossMatcher
  {
    private double minBYNeutralLossIntensityScale;

    public BYNeutralLossMatcher(double peakMzTolerance, double minBYNeutralLossIntensityScale) :
      base(peakMzTolerance)
    {
      this.minBYNeutralLossIntensityScale = minBYNeutralLossIntensityScale;
      this.GetDisplayName = GetDisplayNameHelper.GetBYIonDisplayName;
    }

    public override void Match(IIdentifiedPeptideResult sr)
    {
      PeakList<MatchedPeak> peaks = sr.ExperimentalPeakList;

      double minBYNeutralLossIntensity = peaks.FindMaxIntensityPeak().Intensity * minBYNeutralLossIntensityScale;

      NeutralLossCandidates candidates = new NeutralLossCandidates(sr.Peptide);

      List<MatchedPeak> byNeutralLossPeaks = GetNeutralLossPeaks(sr.GetIonSeries(), sr.ExperimentalPeakList.PrecursorCharge, candidates);

      MatchedPeakUtils.Match(peaks, byNeutralLossPeaks, PeakMzTolerance, minBYNeutralLossIntensity);
    }

    public List<MatchedPeak> GetNeutralLossPeaks(Dictionary<IonType, List<MatchedPeak>> ionMaps, int precursorCharge, INeutralLossCandidates candidates)
    {
      List<MatchedPeak> result = new List<MatchedPeak>();

      result.AddRange(GetNeutralLossPeaks(ionMaps[IonType.B], IonType.B_NEUTRAL_LOSS, 1, candidates.BLossWater, candidates.BLossAmmonia));
      result.AddRange(GetNeutralLossPeaks(ionMaps[IonType.Y], IonType.B_NEUTRAL_LOSS, 1, candidates.YLossWater, candidates.YLossAmmonia));

      if (precursorCharge > 1)
      {
        result.AddRange(GetNeutralLossPeaks(ionMaps[IonType.B2], IonType.B_NEUTRAL_LOSS, 2, candidates.BLossWater, candidates.BLossAmmonia));
        result.AddRange(GetNeutralLossPeaks(ionMaps[IonType.Y2], IonType.B_NEUTRAL_LOSS, 2, candidates.YLossWater, candidates.YLossAmmonia));
      }

      result.Sort();

      return result;
    }

    /// <summary>
    /// 根据给定参数获取相应的b/y系列理论中性丢失离子列表
    /// </summary>
    /// <param name="theoreticalPeaks"></param>
    /// <param name="ionType"></param>
    /// <param name="charge"></param>
    /// <param name="canLossWater"></param>
    /// <param name="canLossAmmonia"></param>
    /// <returns></returns>
    public List<MatchedPeak> GetNeutralLossPeaks(List<MatchedPeak> theoreticalPeaks, IonType ionType, int charge, bool[] canLossWater, bool[] canLossAmmonia)
    {
      List<MatchedPeak> result = new List<MatchedPeak>();
      for (int i = 0; i < theoreticalPeaks.Count; i++)
      {
        List<INeutralLossType> nlCandidates = GetNeutralLossTypes(canLossWater[i], canLossAmmonia[i]);

        result.AddRange(GetNeutralLossPeaks(ionType, theoreticalPeaks[i], nlCandidates, (m => true)));
      }

      return result;
    }
  }
}
