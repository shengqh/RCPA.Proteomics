using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.XTandem;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.MyriMatch;
using RCPA.Proteomics.MSGF;
using RCPA.Proteomics.Omssa;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class DatasetListOptions : List<IDatasetOptions>, IXml
  {
    #region IXml Members

    public void Save(XElement parentNode)
    {
      var doc = new XElement("Datasets");
      this.ForEach(m =>
      {
        var optionEle = new XElement("Dataset");
        m.Save(optionEle);
        doc.Add(optionEle);
      });
      parentNode.Add(doc);
    }

    public void Load(XElement parentNode)
    {
      this.Clear();

      var doc = parentNode.Element("Datasets");
      var options = doc.Elements("Dataset");
      foreach (var option in options)
      {
        var engine = option.Element("SearchEngine").Value;
        IDatasetOptions op = GetDatasetOption(engine);
        op.Load(option);
        Add(op);
      }
    }

    public void RemoveDisabled()
    {
      this.RemoveAll(m => !m.Enabled);
    }

    public static IDatasetOptions GetDatasetOption(string engine)
    {
      SearchEngineType set = EnumUtils.StringToEnum(engine, SearchEngineType.Unknown);

      IDatasetOptions op = set.GetFactory().GetOptions();
      if (op == null)
      {
        throw new ArgumentException("It's not defined that how to get dataset option for search engine " + engine);
      }
      return op;
    }

    #endregion

    public static DatasetListOptions LoadOptions(XElement parentNode)
    {
      var result = new DatasetListOptions();
      result.Load(parentNode);
      return result;
    }

    public BuildSummaryOptions Options
    {
      get
      {
        if (this.Count == 0)
        {
          return null;
        }
        return this[0].Parent;
      }
    }
  }
}
