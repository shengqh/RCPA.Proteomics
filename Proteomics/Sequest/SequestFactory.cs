using RCPA.Proteomics.Comet;
using RCPA.Proteomics.ProteomeDiscoverer;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Sequest
{
  public class SequestFactory : ISearchEngineFactory
  {
    public IScoreFunctions GetScoreFunctions()
    {
      return new SequestXcorrFunctions();
    }

    public List<IIdentifiedSpectrum> GetHighConfidentPeptides(List<IIdentifiedSpectrum> source)
    {
      IdentifiedSpectrumChargeScoreFilter filter = new IdentifiedSpectrumChargeScoreFilter(new double[] { 3, 3.5, 4 });
      return (from pep in source
              where filter.Accept(pep)
              select pep).ToList();
    }

    public virtual SearchEngineType EngineType
    {
      get { return SearchEngineType.SEQUEST; }
    }

    private static readonly double _modificationDeltaScore = 0.08;
    public ISpectrumParser GetParser(string name)
    {
      if (Directory.Exists(name))
      {
        var dir = new DirectoryInfo(name);

        if (dir.GetFiles("*.outs").Length > 0 || dir.GetFiles("*.outs.zip").Length > 0)
        {
          return new SequestOutsParser(true, _modificationDeltaScore);
        }
        else
        {
          return new SequestOutDirectoryParser(true, _modificationDeltaScore);
        }
      }

      if (name.ToLower().EndsWith(".xml"))
      {
        return new CometXmlParser()
        {
          TitleParser = new DefaultTitleParser(TitleParserUtils.GetTitleParsers())
        };
      }

      if (name.ToLower().EndsWith(".msf"))
      {
        return new MsfDatabaseParser(this.EngineType);
      }

      //zipfile
      if (ZipUtils.HasFile(name, m => m.ToLower().EndsWith(".out")))
      {
        return new SequestOutZipParser(true, _modificationDeltaScore);
      }
      else
      {
        return new SequestOutsParser(true, _modificationDeltaScore);
      }
    }
  }
}
