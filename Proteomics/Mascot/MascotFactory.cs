using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Mascot
{
  public class MascotFactory : ISearchEngineFactory
  {
    public Summary.ISpectrumParser GetParser(string name)
    {
      if (name.ToLower().EndsWith(".msf"))
      {
        return new MsfDatabaseParser(Summary.SearchEngineType.MASCOT);
      }
      return new MascotDatSpectrumParser();
    }

    public IScoreFunctions GetScoreFunctions()
    {
      return new MascotScoreFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Score >= 30
              select pep).ToList();
    }

    public SearchEngineType EngineType
    {
      get { return SearchEngineType.MASCOT; }
    }
  }
}
