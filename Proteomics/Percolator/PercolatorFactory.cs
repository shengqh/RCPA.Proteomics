using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorFactory : AbstractSearchEngineFactory
  {
    public PercolatorFactory() : base(SearchEngineType.Percolator) { }

    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      if (extractRank2)
      {
        throw new Exception("Extract rank2 PSM is not supported for Percolator");
      }
      return new PercolatorFileParser(new MsfDatabaseParser(Summary.SearchEngineType.Percolator));
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new[] { new PercolatorScoreFunction() };
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.SpScore >= 30
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new PercolatorDatasetOptions();
    }
  }
}
