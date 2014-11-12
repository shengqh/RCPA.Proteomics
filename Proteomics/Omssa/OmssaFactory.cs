using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Omssa
{
  public class OmssaFactory : ISearchEngineFactory
  {
    public Summary.ISpectrumParser GetParser(string name)
    {
      return new OmssaOmxParser();
    }

    public IScoreFunctions GetScoreFunctions()
    {
      return new OmssaScoreFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from s in source
              where s.ExpectValue < 0.001
              select s).ToList();
    }

    public SearchEngineType EngineType
    {
      get { return SearchEngineType.OMSSA; }
    }

    public IDatasetOptions GetOptions()
    {
      return new OmssaDatasetOptions();
    }
  }
}
