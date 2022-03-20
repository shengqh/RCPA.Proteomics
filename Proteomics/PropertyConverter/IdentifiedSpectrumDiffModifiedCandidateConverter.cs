using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.PropertyConverter
{
  public class IdentifiedSpectrumDiffModifiedCandidateConverter<T> : AbstractPropertyConverter<T>
    where T : IIdentifiedSpectrum
  {
    private readonly Regex peptidePatternRegex = new Regex(@"(\S\.\S+?\.\S)\((\d+\.\d+),(\d+\.\d+)\)");

    public override string Name
    {
      get { return "DIFF_MODIFIED_CANDIDATE"; }
    }

    public override string GetProperty(T t)
    {
      var fcs = new List<string>();

      foreach (FollowCandidate fc in t.DiffModificationSiteCandidates)
      {
        fcs.Add(fc.ToString());
      }

      return StringUtils.Merge(fcs, " ! ");
    }

    public override void SetProperty(T t, string value)
    {
      MatchCollection matches = this.peptidePatternRegex.Matches(value);
      foreach (Match match in matches)
      {
        var fc = new FollowCandidate(match.Groups[1].Value,
                                     MyConvert.ToDouble(match.Groups[2].Value), MyConvert.ToDouble(match.Groups[3].Value));
        t.DiffModificationSiteCandidates.Add(fc);
      }
    }
  }
}