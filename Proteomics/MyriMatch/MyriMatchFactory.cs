using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.MyriMatch
{
  public class MyriMatchFactory : AbstractSearchEngineFactory
  {
    public MyriMatchFactory() : base(SearchEngineType.MyriMatch) { }

    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      if (name.ToLower().EndsWith("mzid"))
      {
        return new MyriMatchMzIdentParser()
        {
          ExtractRank2 = extractRank2
        };
      }
      else
      {
        if (extractRank2)
        {
          throw new Exception("Extract rank2 PSM is not supported for MyriMatch pepXML format");
        }

        return new MyriMatchXmlParser();
      }
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Score > 100
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new MyriMatchDatasetOptions();
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new[] { new ScoreFunction("MyriMatch:MVH") };
    }
  }
}
