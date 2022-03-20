using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.MSAmanda
{
  public class MSAmandaFactory : AbstractSearchEngineFactory
  {
    public MSAmandaFactory() : base(SearchEngineType.MSAmanda) { }

    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      if (extractRank2)
      {
        return new MSAmandaRank2Parser();
      }
      else
      {
        return new MSAmandaParser();
      }
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new[] { new ScoreFunction("MSAmanda:Score") };
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Score > 50
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new MSAmandaDatasetOptions();
    }
  }
}
