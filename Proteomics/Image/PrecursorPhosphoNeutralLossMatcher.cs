using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Image
{
  public class PrecursorPhosphoNeutralLossMatcher : AbstractPhosphoNeutralLossMatcher
  {
    private double minPrecursorNeutralLossIntensityScale;

    public PrecursorPhosphoNeutralLossMatcher(Dictionary<char, double> dynamicModifications, double mzTolerance, double minPrecursorNeutralLossIntensityScale)
      : base(dynamicModifications, mzTolerance)
    {
      this.minPrecursorNeutralLossIntensityScale = minPrecursorNeutralLossIntensityScale;
      this.GetDisplayName = GetDisplayNameHelper.GetPrecursorDisplayName;
    }

    public PrecursorPhosphoNeutralLossMatcher(Dictionary<char, double> dynamicModifications, double mzTolerance)
      : this(dynamicModifications, mzTolerance, 0.3)
    { }

    public override void Match(IIdentifiedPeptideResult sr)
    {
      NeutralLossCandidates candidates = new NeutralLossCandidates(sr.Peptide);

      string phosphoString = GetPhosphoModificationString(sr.Peptide);

      List<INeutralLossType> nlCandidates = GetPhosphoNeutralLossTypes(phosphoString, candidates.CanLossWater, candidates.CanLossAmmonia);

      PeakList<MatchedPeak> expPeaks = sr.ExperimentalPeakList;

      List<MatchedPeak> nlPeaks = GetNeutralLossPeaks(IonType.PRECURSOR_NEUTRAL_LOSS_PHOSPHO, new MatchedPeak(expPeaks.PrecursorMZ, 0.0, expPeaks.PrecursorCharge), nlCandidates, (m => m.IsPhosphoNeutralLossType()));

      //nlPeaks.ForEach(m => Console.WriteLine(m.DisplayName + "," + m.Mz));

      double minNeutralLossIntensity = expPeaks.FindMaxIntensityPeak().Intensity * minPrecursorNeutralLossIntensityScale;

      MatchedPeakUtils.Match(expPeaks, nlPeaks, PeakMzTolerance, minNeutralLossIntensity);
    }

    public string GetPhosphoModificationString(string peptide)
    {
      string matchedSequence = PeptideUtils.GetMatchedSequence(peptide);
      StringBuilder result = new StringBuilder();
      for (int i = 0; i < matchedSequence.Length; i++)
      {
        if (!PeptideUtils.IsAminoacid(matchedSequence[i]))
        {
          continue;
        }

        if (IsPhospho(matchedSequence, i))
        {
          result.Append(matchedSequence[i]);
        }
      }
      return result.ToString();
    }
  }
}
