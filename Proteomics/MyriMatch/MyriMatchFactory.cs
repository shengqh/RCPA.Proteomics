using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MyriMatch
{
  public class MyriMatchFactory : ISearchEngineFactory
  {
    public Summary.ISpectrumParser GetParser(string name)
    {
      return new MyriMatchMzIdentParser();
    }

    public IScoreFunctions GetScoreFunctions()
    {
      return new MyriMatchScoreFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Score > 100
              select pep).ToList();
    }

    public SearchEngineType EngineType
    {
      get { return SearchEngineType.MyriMatch; }
    }
  }
}
