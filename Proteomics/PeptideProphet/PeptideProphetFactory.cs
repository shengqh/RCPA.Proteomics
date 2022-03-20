using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PeptideProphetFactory : AbstractSearchEngineFactory
  {
    public PeptideProphetFactory() : base(SearchEngineType.PeptidePhophet) { }

    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      if (extractRank2)
      {
        throw new Exception("Extract rank2 PSM is not supported for PeptideProphet");
      }
      return new PeptideProphetXmlParser();
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.Probability >= 0.99
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new PeptideProphetDatasetOptions();
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new[] { new ProbabilityFunction() };
    }
  }
}
