using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Mascot
{
  public class MascotFactory : AbstractSearchEngineFactory
  {
    public MascotFactory() : base(SearchEngineType.MASCOT) { }

    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      if (extractRank2)
      {
        throw new Exception("Extract rank2 PSM is not supported for mascot");
      }
      if (name.ToLower().EndsWith(".msf"))
      {
        return new MsfDatabaseParser(Summary.SearchEngineType.MASCOT);
      }
      return new MascotDatSpectrumParser();
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Score >= 30
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new MascotDatasetOptions();
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new IScoreFunction[] { new ScoreFunction(), new ExpectValueFunction() };
    }
  }
}
