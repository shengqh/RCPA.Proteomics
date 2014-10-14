using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.MSGF;
using RCPA.Proteomics.MyriMatch;
using RCPA.Proteomics.Omssa;
using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.XTandem;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public enum SearchEngineType { NotFound, MASCOT, SEQUEST, Comet, XTandem, PFind, PeptidePhophet, MyriMatch, MSGF, OMSSA, MSAmanda }

  public static class SearchEngineTypeExtension
  {
    private static Dictionary<SearchEngineType, ISearchEngineFactory> _map;

    static SearchEngineTypeExtension()
    {
      _map = new Dictionary<SearchEngineType, ISearchEngineFactory>();
      _map[SearchEngineType.MASCOT] = new MascotFactory();
      _map[SearchEngineType.SEQUEST] = new SequestFactory();
      _map[SearchEngineType.Comet] = new SequestFactory();
      _map[SearchEngineType.XTandem] = new XTandemFactory();
      _map[SearchEngineType.PFind] = new PFindFactory();
      _map[SearchEngineType.PeptidePhophet] = new PeptideProphetFactory();
      _map[SearchEngineType.MyriMatch] = new MyriMatchFactory();
      _map[SearchEngineType.MSGF] = new MSGFFactory();
      _map[SearchEngineType.OMSSA] = new OmssaFactory();
    }

    public static ISearchEngineFactory GetFactory(this SearchEngineType seType)
    {
      if (_map.ContainsKey(seType))
      {
        return _map[seType];
      }

      return null;
    }
  }
}
