using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.XTandem;
using RCPA.Proteomics.PFind;

namespace RCPA.Proteomics.Summary.Uniform
{
  public static class BuildSummaryOptionsUtils
  {
    public static BuildSummaryOptions LoadFromFile(string fileName)
    {
      BuildSummaryOptions result = new BuildSummaryOptions();
      LoadFromFile(fileName, result);
      return result;
    }

    public static void LoadFromFile(string fileName, BuildSummaryOptions result)
    {
      XElement root = XElement.Load(fileName);

      var engineNode = root.Element("SearchEngine");
      if (engineNode != null)
      {
        var engine = engineNode.Value;
        SearchEngineType set = EnumUtils.StringToEnum(engine, SearchEngineType.Unknown);

        AbstractSummaryConfiguration conf;
        switch (set)
        {
          case SearchEngineType.Unknown:
            throw new ArgumentException("Unknown search engine " + engine);
          case SearchEngineType.MASCOT:
            conf = new MascotDatSummaryConfiguration("");
            break;
          case SearchEngineType.SEQUEST:
            conf = new SequestSummaryConfiguration("");
            break;
          case SearchEngineType.XTandem:
            conf = new XTandemXmlSummaryConfiguration("");
            break;
          case SearchEngineType.PFind:
            conf = new PFindSummaryConfiguration("");
            break;
          //case SearchEngineType.PEPTIDEPHOPHET:
          //  op = new PeptideProphetDatasetOptions();
          //  break;
          default:
            throw new ArgumentException("It's not defined that how to get SummaryConfiguration for search engine " + engine);
        }

        conf.LoadFromFile(fileName);
        conf.ConvertTo(result);
      }
      else
      {
        result.LoadFromFile(fileName);
      }
    }
  }
}
