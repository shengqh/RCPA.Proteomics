using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorFactory : ISearchEngineFactory
  {
    public Summary.ISpectrumParser GetParser(string name)
    {
      return new PercolatorFileParser(new MsfDatabaseParser(Summary.SearchEngineType.Percolator));
    }

    public IScoreFunctions GetScoreFunctions()
    {
      return new PercolatorScoreFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.SpScore >= 30
              select pep).ToList();
    }

    public SearchEngineType EngineType
    {
      get { return SearchEngineType.Percolator; }
    }

    public IDatasetOptions GetOptions()
    {
      return new PercolatorDatasetOptions();
    }
  }
}
