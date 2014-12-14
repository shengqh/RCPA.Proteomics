using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MSAmanda
{
  public class MSAmandaFactory : ISearchEngineFactory
  {
    public Summary.ISpectrumParser GetParser(string name)
    {
      return new MSAmandaParser();
    }

    public IScoreFunctions GetScoreFunctions()
    {
      return new MSAmandaScoreFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Score > 50
              select pep).ToList();
    }

    public SearchEngineType EngineType
    {
      get { return SearchEngineType.MSAmanda; }
    }

    public IDatasetOptions GetOptions()
    {
      return new MSAmandaDatasetOptions();
    }
  }
}
