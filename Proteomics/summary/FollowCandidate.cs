using System;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Summary
{
  public class FollowCandidate
  {
    public FollowCandidate(string info)
    {
      string pattern = @"(\S+)\((.+),(.+)\)";
      Match match = Regex.Match(info, pattern);
      if (!match.Success)
      {
        throw new Exception(info + " is not a valid FollowCandidate line");
      }

      Sequence = match.Groups[1].Value;
      Score = MyConvert.ToDouble(match.Groups[2].Value);
      DeltaScore = MyConvert.ToDouble(match.Groups[3].Value);
    }

    public FollowCandidate(string sequence, double score, double deltaScore)
    {
      this.Sequence = sequence;
      this.Score = score;
      this.DeltaScore = deltaScore;
    }

    public override string ToString()
    {
      return MyConvert.Format("{0}({1:0.0000},{2:0.0000})", Sequence, Score, DeltaScore);
    }

    public string Sequence { get; set; }

    public double Score { get; set; }

    public double DeltaScore { get; set; }
  };
}
