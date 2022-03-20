﻿using RCPA.Proteomics.Comet;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.MSAmanda;
using RCPA.Proteomics.MSFlagger;
using RCPA.Proteomics.MSGF;
using RCPA.Proteomics.MyriMatch;
using RCPA.Proteomics.Omssa;
using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Percolator;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.XTandem;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public enum SearchEngineType { Unknown, MASCOT, SEQUEST, Comet, XTandem, PFind, PeptidePhophet, InterProphet, MyriMatch, MSGF, OMSSA, MSAmanda, Percolator, ProLuCID, MSFlagger }

  public static class SearchEngineTypeExtension
  {
    private static Dictionary<SearchEngineType, ISearchEngineFactory> _map;

    static SearchEngineTypeExtension()
    {
      _map = new Dictionary<SearchEngineType, ISearchEngineFactory>();
      _map[SearchEngineType.MASCOT] = new MascotFactory();
      _map[SearchEngineType.SEQUEST] = new SequestFactory();
      _map[SearchEngineType.Comet] = new CometFactory();
      _map[SearchEngineType.ProLuCID] = new SequestFactory();
      _map[SearchEngineType.XTandem] = new XTandemFactory();
      _map[SearchEngineType.PFind] = new PFindFactory();
      _map[SearchEngineType.PeptidePhophet] = new PeptideProphetFactory();
      _map[SearchEngineType.InterProphet] = new InterProphetFactory();
      _map[SearchEngineType.MyriMatch] = new MyriMatchFactory();
      _map[SearchEngineType.MSGF] = new MSGFFactory();
      _map[SearchEngineType.OMSSA] = new OmssaFactory();
      _map[SearchEngineType.Percolator] = new PercolatorFactory();
      _map[SearchEngineType.MSAmanda] = new MSAmandaFactory();
      _map[SearchEngineType.MSFlagger] = new MSFlaggerFactory();
    }

    public static bool HasFactory(this SearchEngineType seType)
    {
      return _map.ContainsKey(seType);
    }

    public static ISearchEngineFactory GetFactory(this SearchEngineType seType)
    {
      if (_map.ContainsKey(seType))
      {
        return _map[seType];
      }

      throw new Exception(string.Format("Undefined factory of engine {0}", seType));
    }
  }
}
