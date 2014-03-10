using System.Collections.Generic;
using System.Xml;
using System.Linq;
using RCPA.Utils;
using RCPA.Proteomics.Summary.Uniform;
using System;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractExpectValueSummaryConfiguration : AbstractSummaryConfiguration
  {
    public AbstractExpectValueSummaryConfiguration(string applicationTitle, string searchEngine, string fdrScoreType)
      : base(applicationTitle, searchEngine, fdrScoreType)
    {
    }

    public string TitleParserName { get; set; }

    public bool FilterByExpectValue { get; set; }

    public double MaxExpectValue { get; set; }

    public bool FilterByScore { get; set; }

    public double MinScore { get; set; }

    protected override void LoadSpecialDefinition(XmlNode docRoot)
    {
      base.LoadSpecialDefinition(docRoot);

      var xmlHelper = new XmlHelper(docRoot.OwnerDocument);

      TitleParserName = xmlHelper.GetChildValue(docRoot, "TitleFormat");
    }

    protected override void LoadPeptideFilter(XmlNode peptideFilterXml)
    {
      base.LoadPeptideFilter(peptideFilterXml);

      var xmlHelper = new XmlHelper(peptideFilterXml.OwnerDocument);

      List<XmlNode> filters = xmlHelper.GetChildren(peptideFilterXml, "Filter");
      foreach (XmlNode filter in filters)
      {
        string name = filter.Attributes["Name"].Value;
        if (name.Equals("Score"))
        {
          FilterByScore = bool.Parse(filter.Attributes["Active"].Value);
          MinScore = MyConvert.ToDouble(filter.Attributes["Value"].Value);
        }
        else if (name.Equals("ExpectValue"))
        {
          FilterByExpectValue = bool.Parse(filter.Attributes["Active"].Value);
          MaxExpectValue = MyConvert.ToDouble(filter.Attributes["Value"].Value);
        }
      }
    }

    public override void ConvertTo(BuildSummaryOptions result)
    {
      base.ConvertTo(result);

      foreach (AbstractExpectValueDatasetOptions ds in result.DatasetList)
      {
        ds.Enabled = true;
        ds.FilterByExpectValue = this.FilterByExpectValue;
        ds.FilterByScore = this.FilterByScore;
        ds.MaxExpectValue = this.MaxExpectValue;
        ds.MinScore = this.MinScore;
        ds.TitleParserName = this.TitleParserName;
      }
    }

    public override void ConvertFrom(BuildSummaryOptions result)
    {
      base.ConvertFrom(result);

      this.PathNameBin.Clear();

      var dsList = result.DatasetList.Where(m => m.Enabled).ToList();

      if (dsList.Count == 0)
      {
        return;
      }

      foreach (var ds in dsList)
      {
        if (ds.SearchEngine.ToString() == this.SearchEngine)
        {
          continue;
        }

        throw new Exception(MyConvert.Format("The data set {0} [{1}] is not {2} data set.", ds.Name,ds.SearchEngine.ToString(), this.SearchEngine));
      }

      AbstractExpectValueDatasetOptions ds2 = dsList[0] as AbstractExpectValueDatasetOptions;

      this.FilterByExpectValue = ds2.FilterByExpectValue;
      this.FilterByScore = ds2.FilterByScore;
      this.MaxExpectValue = ds2.MaxExpectValue;
      this.MinScore = ds2.MinScore;
      this.TitleParserName = ds2.TitleParserName;

      foreach (var ds3 in dsList)
      {
        foreach (var file in ds3.PathNames)
        {
          this.PathNameBin[file] = ds3.Name;
        }
      }
    }
  }
}