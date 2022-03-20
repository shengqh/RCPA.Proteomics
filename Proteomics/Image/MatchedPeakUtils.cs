using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Image
{
  public static class MatchedPeakUtils
  {
    /// <summary>
    /// 设置所有离子为非匹配状态
    /// </summary>
    /// <param name="peaks">离子列表</param>
    public static void UnmatchAll(this List<MatchedPeak> peaks)
    {
      peaks.ForEach(m => m.Matched = false);
    }

    /// <summary>
    /// 将实际离子与理论离子进行匹配。对于已经被匹配上的实际离子或者强度小于给定阈值的实际离子，不进行匹配。
    /// 对于匹配到相同理论离子的不同实际离子，保留强度最大的为实际匹配结果。
    /// </summary>
    /// <param name="exprimentalPeaks">实际离子</param>
    /// <param name="theoreticalPeaks">理论离子</param>
    /// <param name="mzTolerance">离子误差</param>
    /// <param name="minIntensity">匹配离子最小强度</param>
    public static void Match(List<MatchedPeak> exprimentalPeaks, List<MatchedPeak> theoreticalPeaks, double mzTolerance, double minIntensity = 0.0)
    {
      foreach (MatchedPeak peak in exprimentalPeaks)
      {
        if (peak.Matched || peak.Intensity < minIntensity)
        {
          continue;
        }

        foreach (MatchedPeak thePeak in theoreticalPeaks)
        {
          if (Math.Abs(peak.Mz - thePeak.Mz) <= mzTolerance)
          {
            peak.Matched = true;
            thePeak.Matched = true;

            peak.MatchedMZ = thePeak.Mz;
            peak.PeakType = thePeak.PeakType;
            peak.Charge = thePeak.Charge;
            peak.DisplayName = thePeak.DisplayName;
            peak.PeakIndex = thePeak.PeakIndex;
            break;
          }
        }
      }

      RemoveDuplicateMatch(exprimentalPeaks);
    }

    /// <summary>
    /// 将实际离子与理论离子进行匹配。对于已经被匹配上的实际离子或者强度小于给定阈值的实际离子，不进行匹配。
    /// 对于匹配到相同理论离子的不同实际离子，保留强度最大的为实际匹配结果。
    /// </summary>
    /// <param name="exprimentalPeaks">实际离子</param>
    /// <param name="theoreticalPeaks">理论离子</param>
    /// <param name="mzTolerance">离子误差</param>
    /// <param name="minIntensity">匹配离子最小强度</param>
    public static void MatchPPM(List<MatchedPeak> exprimentalPeaks, List<MatchedPeak> theoreticalPeaks, double ppmTolerance, double minIntensity = 0.0)
    {
      foreach (MatchedPeak peak in exprimentalPeaks)
      {
        if (peak.Matched || peak.Intensity < minIntensity)
        {
          continue;
        }

        var mzTolerance = PrecursorUtils.ppm2mz(peak.Mz, ppmTolerance);

        foreach (MatchedPeak thePeak in theoreticalPeaks)
        {
          if (Math.Abs(peak.Mz - thePeak.Mz) <= mzTolerance)
          {
            peak.Matched = true;
            thePeak.Matched = true;

            peak.MatchedMZ = thePeak.Mz;
            peak.PeakType = thePeak.PeakType;
            peak.Charge = thePeak.Charge;
            peak.DisplayName = thePeak.DisplayName;
            peak.PeakIndex = thePeak.PeakIndex;
            break;
          }
        }
      }

      RemoveDuplicateMatch(exprimentalPeaks);
    }

    /// <summary>
    /// 移除重复匹配，保留强度最高的为真实匹配。这里的重复匹配，意思是，两个或者两个以上离子，匹配到了
    /// 另一个离子列表的同一个离子上，要求离子类型、离子标签、以及离子显示名都一样。
    /// </summary>
    /// <param name="peaks">离子列表</param>
    public static void RemoveDuplicateMatch(List<MatchedPeak> peaks)
    {
      for (int i = 0; i < peaks.Count; i++)
      {
        if (!peaks[i].Matched)
        {
          continue;
        }

        for (int j = i + 1; j < peaks.Count(); j++)
        {
          if (!peaks[j].Matched)
          {
            break;
          }

          if (peaks[j].PeakType == peaks[i].PeakType && peaks[j].PeakIndex == peaks[i].PeakIndex && peaks[j].DisplayName == peaks[i].DisplayName)
          {
            if (peaks[i].Intensity >= peaks[j].Intensity)
            {
              peaks[j].Matched = false;
              continue;
            }
            else
            {
              peaks[i].Matched = false;
              break;
            }
          }
        }
      }
    }
  }
}