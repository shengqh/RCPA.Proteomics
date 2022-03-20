using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Sequest
{
  public abstract class AbstractSequestFactory : AbstractSearchEngineFactory
  {
    public AbstractSequestFactory(SearchEngineType set) : base(set) { }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      IdentifiedSpectrumChargeScoreFilter filter = new IdentifiedSpectrumChargeScoreFilter(new double[] { 3, 3.5, 4 });
      return (from pep in source
              where filter.Accept(pep)
              select pep).ToList();
    }
  }
}
