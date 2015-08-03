using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
  }
}
