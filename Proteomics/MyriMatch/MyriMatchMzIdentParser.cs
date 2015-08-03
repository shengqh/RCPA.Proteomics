using RCPA.Proteomics.MzIdent;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MyriMatch
{
  public class MyriMatchMzIdentParser : AbstractMzIdentParser
  {
    public MyriMatchMzIdentParser(ITitleParser parser, bool extractRank2) : base(parser, extractRank2) { }

    public MyriMatchMzIdentParser(ITitleParser parser) : base(parser) { }

    public MyriMatchMzIdentParser() : base() { }

    /// <summary>
    /// For MyriMatch
    /// Score=MyriMatch:MVH
    /// SpScore=MyriMatch:mzFidelity
    /// </summary>
    /// <param name="spectrum"></param>
    /// <param name="cvParams"></param>
    protected override void ParseScore(Summary.IdentifiedSpectrum spectrum, Dictionary<string, string> cvParams)
    {
      string score;
      if (cvParams.TryGetValue("MS:1001589", out score))
      {
        spectrum.Score = double.Parse(score);
      }

      if (cvParams.TryGetValue("MS:1001590", out score))
      {
        spectrum.SpScore = double.Parse(score);
      }
    }

    public override SearchEngineType Engine
    {
      get { return SearchEngineType.MyriMatch; }
    }
  }
}
