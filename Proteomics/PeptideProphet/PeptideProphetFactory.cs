using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PeptideProphetFactory : ISearchEngineFactory
  {
    public Summary.ISpectrumParser GetParser(string name)
    {
      return new PeptideProphetXmlParser();
    }

    public IScoreFunctions GetScoreFunctions()
    {
      return new PeptideProphetScoreFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.PValue >= 0.99
              select pep).ToList();
    }

    public SearchEngineType EngineType
    {
      get { return SearchEngineType.PeptidePhophet; }
    }

    public IDatasetOptions GetOptions()
    {
      return new PeptideProphetDatasetOptions();
    }
  }
}
