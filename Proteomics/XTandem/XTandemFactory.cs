using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemFactory : AbstractSearchEngineFactory
  {
    public XTandemFactory() : base(SearchEngineType.XTandem) { }

    public override ISpectrumParser GetParser(string name, bool extractRank2)
    {
      if (extractRank2)
      {
        throw new Exception("Extract rank2 PSM is not supported for XTandem");
      }

      if (name.ToLower().EndsWith(".pepxml"))
      {
        return new XTandemPepXmlParser();
      }
      else
      {
        return new XTandemSpectrumXmlParser();
      }
    }

    public override IScoreFunction[] GetScoreFunctions()
    {
      return new IScoreFunction[] { new ExpectValueFunction(), new ScoreFunction() };
    }

    public override List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      return (from pep in source
              where pep.ExpectValue < 0.001
              select pep).ToList();
    }

    public override IDatasetOptions GetOptions()
    {
      return new XTandemDatasetOptions();
    }
  }
}
