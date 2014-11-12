using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.PFind
{
  public class PFindFactory : ISearchEngineFactory
  {
    public Summary.ISpectrumParser GetParser(string name)
    {
      return new PFindSpectrumParser();
    }

    public IScoreFunctions GetScoreFunctions()
    {
      return new PFindExpectValueFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from s in source
              where s.ExpectValue < 0.001
              select s).ToList();
    }

    public SearchEngineType EngineType
    {
      get { return SearchEngineType.PFind; }
    }

    public IDatasetOptions GetOptions()
    {
      return new PFindDatasetOptions();
    }
  }
}
