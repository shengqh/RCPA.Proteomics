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
  public class PeptideProphetFactory : AbstractSearchEngineFactory
  {
    public PeptideProphetFactory() : base(SearchEngineType.PeptidePhophet) { }

    public override ISpectrumParser GetParser(string name)
    {
      return new PeptideProphetXmlParser();
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.PValue >= 0.99
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new PeptideProphetDatasetOptions();
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new[] { new PValueFunction() };
    }
  }
}
