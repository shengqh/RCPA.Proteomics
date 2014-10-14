using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemFactory : ISearchEngineFactory
  {
    public Summary.ISpectrumParser GetParser(string name)
    {
      return new XTandemSpectrumXmlParser();
    }

    public IScoreFunctions GetScoreFunctions()
    {
      return new XTandemScoreFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.ExpectValue < 0.001
              select pep).ToList();
    }

    public SearchEngineType EngineType
    {
      get { return SearchEngineType.XTandem; }
    }
  }
}
