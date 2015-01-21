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
  public class OmssaFactory : AbstractSearchEngineFactory
  {
    public OmssaFactory() : base(SearchEngineType.OMSSA) { }

    public override ISpectrumParser GetParser(string name)
    {
      return new OmssaOmxParser();
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from s in source
              where s.ExpectValue < 0.001
              select s).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new OmssaDatasetOptions();
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new[] { new ExpectValueFunction() };
    }
  }
}
