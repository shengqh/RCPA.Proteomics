using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.PeptideProphet
{
  public class InterProphetFactory : AbstractSearchEngineFactory
  {
    public InterProphetFactory() : base(SearchEngineType.InterProphet) { }

    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      if (extractRank2)
      {
        throw new Exception("Extract rank2 PSM is not supported for iProphet");
      }
      return new InterProphetXmlParser();
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Probability >= 0.99
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new InterProphetDatasetOptions();
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new[] { new ProbabilityFunction() };
    }
  }
}
