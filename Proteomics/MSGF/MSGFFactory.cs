using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.MSGF
{
  public class MSGFFactory : AbstractSearchEngineFactory
  {
    public MSGFFactory() : base(SearchEngineType.MSGF) { }

    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      return new MSGFMzIdentParser()
      {
        ExtractRank2 = extractRank2
      };
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new IScoreFunction[] { new ExpectValueFunction(), new ScoreFunction() };
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Score > 100
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new MSGFDatasetOptions();
    }

    public override IIdentifiedPeptideTextFormat GetPeptideFormat(bool notExportSummary = false)
    {
      return new MascotPeptideTextFormat("\tFileScan\tSequence\tObs\tMH+\tDiff(MH+)\tDiffPPM\tCharge\tRank\tScore\tExpectValue\tQValue\tReference\tMissCleavage\tModification\tMatchCount\tNumProteaseTermini\tIsotopeError\tMSGF:DeNovoScore\tMSGF:DatabaseEValue\tMSGF:MS2IonCurrent\tMSGF:Id")
      {
        NotExportSummary = notExportSummary
      };
    }
  }
}
