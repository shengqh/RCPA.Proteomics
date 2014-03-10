using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Image
{
  public delegate string GetDisplayNameFunc(INeutralLossType nlType, MatchedPeak sourcePeak, MatchedPeak targetPeak);

  public abstract class AbstractNeutralLossMatcher : IMatcher
  {
    public double PeakMzTolerance { get; set; }

    public int NeutralLossLevel { get; set; }

    public AbstractNeutralLossMatcher(double peakMzTolerance )
    {
      this.PeakMzTolerance = peakMzTolerance;
      this.NeutralLossLevel = 2;
    }

    public List<INeutralLossType> GetNeutralLossTypes(bool canLossWater, bool canLossAmmonia)
    {
      List<INeutralLossType> result = new List<INeutralLossType>();

      if (canLossWater)
      {
        result.Add(NeutralLossConstants.NL_WATER);
      }

      if (canLossAmmonia)
      {
        result.Add(NeutralLossConstants.NL_AMMONIA);
      }

      return result;
    }

    public List<MatchedPeak> GetNeutralLossPeaks(IonType targetPeakType, MatchedPeak sourcePeak, List<INeutralLossType> nlCandidates, Func<INeutralLossType, bool> filter)
    {
      List<INeutralLossType> nlTypes = new NeutralLossGenerator().GetTotalCombinationValues(nlCandidates, NeutralLossLevel);

      nlTypes.RemoveAll(m => !filter(m));

      List<MatchedPeak> result = new List<MatchedPeak>();

      foreach (INeutralLossType nlType in nlTypes)
      {
        double nlMass = sourcePeak.Mz - nlType.Mass / sourcePeak.Charge;

        MatchedPeak nlPeak = new MatchedPeak(nlMass, 1.0, sourcePeak.Charge)
        {
          PeakType = targetPeakType,
          PeakIndex = sourcePeak.PeakIndex
        };

        nlPeak.DisplayName = GetDisplayName(nlType, sourcePeak, nlPeak);

        result.Add(nlPeak);
      }

      result.Sort();

      return result;
    }

    public GetDisplayNameFunc GetDisplayName { get; set; }

    public abstract void Match(IIdentifiedPeptideResult sr);
  }
}
