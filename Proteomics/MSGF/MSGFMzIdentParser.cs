using RCPA.Proteomics.MzIdent;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MSGF
{
  public class MSGFMzIdentParser : AbstractMzIdentParser
  {
    public MSGFMzIdentParser(ITitleParser parser, bool extractRank2) : base(parser, extractRank2) { }

    public MSGFMzIdentParser(ITitleParser parser) : base(parser) { }

    public MSGFMzIdentParser() : base() { }

    /// <summary>
    /// For MSGF
    /// Score=MS-GF:RawScore
    /// ExpectValue=MS-GF:SpecEValue
    /// 
    /// MS-GF:RawScore: MS-GF+ raw score of the peptide-spectrum match 
    /// MS-GF:DeNovoScore: the score of the optimal scoring peptide for the spectrum (not necessary in the database) (MS-GF:RawScore <= MS-GF:DeNovoScore)
    /// MS-GF:SpecEValue: spectral E-value (spectrum level E-value) of the peptide-spectrum match - the lower the better
    /// MS-GF:EValue: database level E-value (expected number of peptides in a random database having equal or better scores than the PSM score) - the lower the better
    /// MS-GF:QValue
    ///  PSM-level Q-value estimated using the target-decoy approach.
    ///  MS-GF:QValue is computed solely based on MS-GF:SpecEValue.
    /// </summary>
    /// <param name="spectrum"></param>
    /// <param name="cvParams"></param>
    protected override void ParseScore(Summary.IdentifiedSpectrum spectrum, Dictionary<string, string> cvParams)
    {
      string score;
      if (cvParams.TryGetValue("MS:1002049", out score))
      {
        spectrum.Score = double.Parse(score);
      }

      if (cvParams.TryGetValue("MS:1002050", out score))
      {
        spectrum.SpScore = double.Parse(score);
      }

      if (cvParams.TryGetValue("MS:1002052", out score))
      {
        spectrum.ExpectValue = double.Parse(score);
      }

      if (cvParams.TryGetValue("MS:1002053", out score))
      {
        spectrum.Probability = double.Parse(score);
      }

      if (cvParams.TryGetValue("MS:1002054", out score))
      {
        spectrum.QValue = double.Parse(score);
      }
    }

    protected override void ParseUserParams(IdentifiedSpectrum spectrum, Dictionary<string, string> userParams)
    {
      base.ParseUserParams(spectrum, userParams);
      string value;
      if (userParams.TryGetValue("IsotopeError", out value))
      {
        spectrum.IsotopeError = int.Parse(value);
      }
      if (userParams.TryGetValue("MS2IonCurrent", out value))
      {
        spectrum.MatchedTIC = double.Parse(value);
      }
    }

    public override SearchEngineType Engine
    {
      get
      {
        return SearchEngineType.MSGF;
      }
    }
  }
}
