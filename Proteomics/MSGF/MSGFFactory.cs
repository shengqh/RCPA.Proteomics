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
  public class MSGFFactory : ISearchEngineFactory
  {
    public Summary.ISpectrumParser GetParser(string name)
    {
      return new MSGFMzIdentParser();
    }

    public IScoreFunctions GetScoreFunctions()
    {
      return new MSGFScoreFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Score > 100
              select pep).ToList();
    }

    public SearchEngineType EngineType
    {
      get { return SearchEngineType.MSGF; }
    }

    public IDatasetOptions GetOptions()
    {
      return new MSGFDatasetOptions();
    }
  }
}
