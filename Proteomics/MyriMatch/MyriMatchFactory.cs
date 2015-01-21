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
  public class MyriMatchFactory : AbstractSearchEngineFactory
  {
    public MyriMatchFactory() : base(SearchEngineType.MyriMatch) { }

    public override ISpectrumParser GetParser(string name)
    {
      if (name.ToLower().EndsWith("mzid"))
      {
        return new MyriMatchMzIdentParser();
      }
      else
      {
        return new MyriMatchPepXmlParser();
      }
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Score > 100
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new MyriMatchDatasetOptions();
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new[] { new ScoreFunction("MyriMatch:MVH") };
    }
  }
}
