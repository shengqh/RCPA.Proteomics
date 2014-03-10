using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Image
{
  public class BYPhosphoNeutralLossMatcher : AbstractPhosphoNeutralLossMatcher
  {
    private double minBYNeutralLossIntensityScale;

    public BYPhosphoNeutralLossMatcher(Dictionary<char, double> dynamicModifications, double peakMzTolerance, double minBYNeutralLossIntensityScale)
      : base(dynamicModifications, peakMzTolerance)
    {
      this.minBYNeutralLossIntensityScale = minBYNeutralLossIntensityScale;
      this.GetDisplayName = GetDisplayNameHelper.GetBYIonDisplayName;
    }

    public override void Match(IIdentifiedPeptideResult sr)
    {
      PeakList<MatchedPeak> peaks = sr.ExperimentalPeakList;

      double minBYNeutralLossIntensity = peaks.FindMaxIntensityPeak().Intensity * minBYNeutralLossIntensityScale;

      NeutralLossCandidates candidates = new NeutralLossCandidates(sr.Peptide);

      List<MatchedPeak> byPhosphoNeutralLossPeaks = GetPhosphoNeutralLossPeaks(sr, candidates);

      //byPhosphoNeutralLossPeaks.ForEach(m => Console.WriteLine(m.DisplayName + "," + m.Mz));

      MatchedPeakUtils.Match(peaks, byPhosphoNeutralLossPeaks, PeakMzTolerance, minBYNeutralLossIntensity);
    }

    public List<MatchedPeak> GetPhosphoNeutralLossPeaks(IIdentifiedPeptideResult sr, NeutralLossCandidates candidates)
    {
      List<MatchedPeak> result = new List<MatchedPeak>();

      Dictionary<int, string> bModifiedPosition = GetPhosphoModificationPositionMapB(sr.Peptide);
      Dictionary<int, string> yModifiedPosition = GetPhosphoModificationPositionMapY(sr.Peptide);

      result.AddRange(GetPhosphoNeutralLossPeaks(sr.GetIonSeries()[IonType.B], IonType.B_NEUTRAL_LOSS_PHOSPHO, 1, bModifiedPosition, candidates.BLossWater, candidates.BLossAmmonia));
      result.AddRange(GetPhosphoNeutralLossPeaks(sr.GetIonSeries()[IonType.Y], IonType.B_NEUTRAL_LOSS_PHOSPHO, 1, yModifiedPosition, candidates.YLossWater, candidates.YLossAmmonia));

      if (sr.ExperimentalPeakList.PrecursorCharge > 1)
      {
        result.AddRange(GetPhosphoNeutralLossPeaks(sr.GetIonSeries()[IonType.B2], IonType.B_NEUTRAL_LOSS_PHOSPHO, 2, bModifiedPosition, candidates.BLossWater, candidates.BLossAmmonia));
        result.AddRange(GetPhosphoNeutralLossPeaks(sr.GetIonSeries()[IonType.Y2], IonType.B_NEUTRAL_LOSS_PHOSPHO, 2, yModifiedPosition, candidates.YLossWater, candidates.YLossAmmonia));
      }

      return result;
    }

    /// <summary>
    /// 根据给定参数获取相应的b/y系列理论中性丢失离子列表
    /// </summary>
    /// <param name="theoreticalPeaks"></param>
    /// <param name="ionType"></param>
    /// <param name="charge"></param>
    /// <param name="phosphoAminoacidsMap"></param>
    /// <param name="canLossWater"></param>
    /// <param name="canLossAmmonia"></param>
    /// <returns></returns>
    public List<MatchedPeak> GetPhosphoNeutralLossPeaks(List<MatchedPeak> theoreticalPeaks, IonType ionType, int charge, Dictionary<int, string> phosphoAminoacidsMap, bool[] canLossWater, bool[] canLossAmmonia)
    {
      List<MatchedPeak> result = new List<MatchedPeak>();
      for (int i = 0; i < theoreticalPeaks.Count; i++)
      {
        string phosphoAminoacids = phosphoAminoacidsMap[i];

        if (phosphoAminoacids == null || phosphoAminoacids.Length == 0)
        {
          continue;
        }

        List<INeutralLossType> nlCandidates = GetPhosphoNeutralLossTypes(phosphoAminoacids, canLossWater[i], canLossAmmonia[i]);

        result.AddRange(GetNeutralLossPeaks(ionType, theoreticalPeaks[i], nlCandidates, (m => m.IsPhosphoNeutralLossType())));
      }

      return result;
    }

    /// <summary>
    /// 根据序列，得到b系列对应的磷酸化修饰map。
    /// key是氨基酸的index，
    /// value是在该氨基酸N端断裂得到的b离子所对应多肽中磷酸化修饰的氨基酸列表。
    /// </summary>
    /// <param name="sr"></param>
    /// <returns></returns>
    public Dictionary<int, string> GetPhosphoModificationPositionMapB(string peptide)
    {
      Dictionary<int, string> result = new Dictionary<int, string>();
      string matchedSequence = PeptideUtils.GetMatchedSequence(peptide);
      int index = -1;
      string lastModified = "";
      for (int i = 0; i < matchedSequence.Length; i++)
      {
        if (!PeptideUtils.IsAminoacid(matchedSequence[i]))
        {
          continue;
        }

        index++;
        if (IsPhospho(matchedSequence, i))
        {
          lastModified = lastModified + matchedSequence[i];
        }
        result[index] = lastModified;
      }
      return result;
    }

    /// <summary>
    /// 根据序列，得到y系列对应的磷酸化修饰map。
    /// key是氨基酸的index，
    /// value是在该氨基酸N端断裂得到的y离子所对应多肽中磷酸化修饰的氨基酸列表。
    /// </summary>
    /// <param name="sr"></param>
    /// <returns></returns>
    public Dictionary<int, string> GetPhosphoModificationPositionMapY(string peptide)
    {
      Dictionary<int, string> result = new Dictionary<int, string>();
      string matchedSequence = PeptideUtils.GetMatchedSequence(peptide);
      int index = -1;
      string lastModified = "";
      for (int i = matchedSequence.Length - 1; i >= 0; i--)
      {
        if (!PeptideUtils.IsAminoacid(matchedSequence[i]))
        {
          continue;
        }

        index++;
        if (IsPhospho(matchedSequence, i))
        {
          lastModified = lastModified + matchedSequence[i];
        }
        result[index] = lastModified;
      }
      return result;
    }
  }
}
