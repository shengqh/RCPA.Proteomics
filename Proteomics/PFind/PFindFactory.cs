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
  public class PFindFactory : AbstractSearchEngineFactory
  {
    public PFindFactory() : base(SearchEngineType.PFind) { }

    public override ISpectrumParser GetParser(string name)
    {
      return new PFindSpectrumParser();
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from s in source
              where s.ExpectValue < 0.001
              select s).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new PFindDatasetOptions();
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new IScoreFunction[] { new ExpectValueFunction() };
    }
  }
}
