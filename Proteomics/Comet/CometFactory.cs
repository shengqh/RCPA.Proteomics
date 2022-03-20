using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Comet
{
  public class CometFactory : AbstractSequestFactory
  {
    public CometFactory() : base(SearchEngineType.Comet) { }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new IScoreFunction[] { new ScoreFunction("XCorr"), new ExpectValueFunction("ExpectValue") };
    }

    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      if (extractRank2)
      {
        return new CometXmlRank2Parser()
        {
          TitleParser = new DefaultTitleParser(TitleParserUtils.GetTitleParsers())
        };
      }
      else
      {
        return new CometXmlParser()
        {
          TitleParser = new DefaultTitleParser(TitleParserUtils.GetTitleParsers())
        };
      }
    }

    public override IDatasetOptions GetOptions()
    {
      return new CometDatasetOptions();
    }

    public override IIdentifiedPeptideTextFormat GetPeptideFormat(bool notExportSummary = false)
    {
      return new MascotPeptideTextFormat("\tFileScan\tSequence\tObs\tMH+\tDiff(MH+)\tDiffPPM\tCharge\tRank\tXCorr\tDeltaCn\tDeltaCnStar\tSpScore\tSpRank\tExpectValue\tIons\tReference\tMissCleavage\tModification\tMatchCount\tNumProteaseTermini")
      {
        NotExportSummary = notExportSummary
      };
    }
  }
}
