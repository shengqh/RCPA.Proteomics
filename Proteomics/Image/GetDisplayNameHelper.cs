using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Image
{
  public static class GetDisplayNameHelper
  {
    public static string GetPrecursorDisplayName(INeutralLossType nlType, MatchedPeak sourcePeak, MatchedPeak targetPeak)
    {
      return MyConvert.Format("[MH{0}-{1}]", targetPeak.Charge == 1 ? "" : targetPeak.Charge.ToString(), nlType.Name);
    }

    public static string GetBYIonDisplayName(INeutralLossType nlType, MatchedPeak sourcePeak, MatchedPeak targetPeak)
    {
      return MyConvert.Format("[{0}{1}-{2}]", sourcePeak.PeakType.GetDisplayName(), sourcePeak.PeakIndex, nlType.Name);
    }

  }
}
