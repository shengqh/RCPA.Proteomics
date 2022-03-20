using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace RCPA.Proteomics.Sequest
{
  public class SequestSummaryConfiguration : AbstractSummaryConfiguration
  {
    public SequestSummaryConfiguration(string applicationTitle)
      : base(applicationTitle, "SEQUEST", "Xcorr")
    {
      FilterByXcorr = false;
      MinXcorr1 = 1.9;
      MinXcorr2 = 2.2;
      MinXcorr3 = 3.75;
      FilterByDeltaCn = true;
      MinDeltaCn = 0.1;
      FilterBySpRank = false;
      MaxSpRank = 1;
      SkipSamePeptideButDifferentModificationSite = true;
      MaxModificationDeltaCn = 0.08;
      FixedDeltaCn = true;
    }

    public bool FilterByXcorr { get; set; }

    public double MinXcorr1 { get; set; }

    public double MinXcorr2 { get; set; }

    public double MinXcorr3 { get; set; }

    public bool FilterByDeltaCn { get; set; }

    public double MinDeltaCn { get; set; }

    public bool FilterBySpRank { get; set; }

    public int MaxSpRank { get; set; }

    public bool SkipSamePeptideButDifferentModificationSite { get; set; }

    public double MaxModificationDeltaCn { get; set; }

    public bool FixedDeltaCn { get; set; }

    protected override void LoadPeptideFilter(XmlNode peptideFilterXml)
    {
      base.LoadPeptideFilter(peptideFilterXml);

      var xmlHelper = new XmlHelper(peptideFilterXml.OwnerDocument);

      List<XmlNode> filters = xmlHelper.GetChildren(peptideFilterXml, "Filter");
      foreach (XmlNode filter in filters)
      {
        string name = filter.Attributes["Name"].Value;
        if (name.Equals("Xcorr1"))
        {
          FilterByXcorr = bool.Parse(filter.Attributes["Active"].Value);
          MinXcorr1 = MyConvert.ToDouble(filter.Attributes["Value"].Value);
        }
        else if (name.Equals("Xcorr2"))
        {
          MinXcorr2 = MyConvert.ToDouble(filter.Attributes["Value"].Value);
        }
        else if (name.Equals("Xcorr3"))
        {
          MinXcorr3 = MyConvert.ToDouble(filter.Attributes["Value"].Value);
        }
        else if (name.Equals("DeltaCn"))
        {
          FilterByDeltaCn = bool.Parse(filter.Attributes["Active"].Value);
          MinDeltaCn = MyConvert.ToDouble(filter.Attributes["Value"].Value);
        }
        else if (name.Equals("SpRank"))
        {
          FilterBySpRank = bool.Parse(filter.Attributes["Active"].Value);
          MaxSpRank = int.Parse(filter.Attributes["Value"].Value);
        }
      }
    }

    protected override void LoadSpecialDefinition(XmlNode docRoot)
    {
      base.LoadSpecialDefinition(docRoot);

      var xmlHelper = new XmlHelper(docRoot.OwnerDocument);

      XmlNode deltaCnCalc = xmlHelper.GetFirstChild(docRoot, "DeltaCnCalculation");
      if (null != deltaCnCalc)
      {
        SkipSamePeptideButDifferentModificationSite =
          bool.Parse(xmlHelper.GetChildValue(deltaCnCalc, "SkipSamePeptideButDifferentModificationSite"));

        MaxModificationDeltaCn = MyConvert.ToDouble(xmlHelper.GetChildValue(deltaCnCalc, "MaxModificationDeltaCn"));
      }
    }

    public override void LoadFromFile(string fileName)
    {
      var doc = new XmlDocument();
      doc.Load(fileName);

      XmlNode root = doc.DocumentElement;

      var xmlHelper = new XmlHelper(doc);

      if (xmlHelper.HasChild(root, "FalseDiscoveryRate"))
      {
        base.LoadFromFile(fileName);
      }
      else
      {
        LoadFromOldFile(fileName);
      }
    }

    private void LoadFromOldFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException("Parameter file not found", fileName);
      }

      var doc = new XmlDocument();
      doc.Load(fileName);

      var xmlHelper = new XmlHelper(doc);

      XmlNode docRoot = doc.DocumentElement;

      ApplicationTitle = xmlHelper.GetChildValue(docRoot, "Version");
      SearchEngine = "SEQUEST";

      XmlNode pepFilter = xmlHelper.GetValidChild(docRoot, "PeptideFilter");

      //false discovery rate filter
      XmlNode fdrXml = xmlHelper.GetValidChild(pepFilter, "FalsePositiveProbability");
      if (null != fdrXml)
      {
        FilterByFdr = bool.Parse(xmlHelper.GetChildValue(fdrXml, "Filtered"));

        bool proteinLevel = bool.Parse(xmlHelper.GetChildValue(fdrXml, "FilterAtProteinLevel"));
        bool uniqueLevel = bool.Parse(xmlHelper.GetChildValue(fdrXml, "UniquePeptideType"));
        if (proteinLevel)
        {
          FdrLevel = FalseDiscoveryRateLevel.Protein;
        }
        else if (uniqueLevel)
        {
          FdrLevel = FalseDiscoveryRateLevel.UniquePeptide;
        }
        else
        {
          FdrLevel = FalseDiscoveryRateLevel.Peptide;
        }

        bool targetFdr = bool.Parse(xmlHelper.GetChildValue(fdrXml, "SqhFormulaType"));
        if (targetFdr)
        {
          FdrType = FalseDiscoveryRateType.Target;
        }
        else
        {
          FdrType = FalseDiscoveryRateType.Total;
        }

        FdrValue = MyConvert.ToDouble(xmlHelper.GetChildValue(fdrXml, "Max"));
        FdrScoreType = "Xcorr";

        ClassifyByCharge = true;
        ClassifyByMissCleavage = true;
        ClassifyByModification = bool.Parse(xmlHelper.GetChildValue(fdrXml, "CalculateModifiedFDR"));
        ModifiedAminoacids = xmlHelper.GetChildValue(fdrXml, "FdrModifiedAminoacids");
        FixedDeltaCn = bool.Parse(xmlHelper.GetChildValue(fdrXml, "FixedDeltaCN"));

        Database.DecoyPattern = xmlHelper.GetChildValue(fdrXml, "ReversedDbPattern");
      }

      //fixed peptide filter
      XmlNode filter = xmlHelper.GetValidChild(pepFilter, "XCorr");
      FilterByXcorr = bool.Parse(xmlHelper.GetChildValue(filter, "Filtered"));
      MinXcorr1 = MyConvert.ToDouble(xmlHelper.GetChildValue(filter, "Min1XCorr"));
      MinXcorr2 = MyConvert.ToDouble(xmlHelper.GetChildValue(filter, "Min2XCorr"));
      MinXcorr3 = MyConvert.ToDouble(xmlHelper.GetChildValue(filter, "Min3XCorr"));

      filter = xmlHelper.GetValidChild(pepFilter, "DeltaCN");
      FilterByDeltaCn = bool.Parse(xmlHelper.GetChildValue(filter, "Filtered"));
      MinDeltaCn = MyConvert.ToDouble(xmlHelper.GetChildValue(filter, "MinDeltaCN"));

      filter = xmlHelper.GetValidChild(pepFilter, "SpRank");
      FilterBySpRank = bool.Parse(xmlHelper.GetChildValue(filter, "Filtered"));
      MaxSpRank = int.Parse(xmlHelper.GetChildValue(filter, "MaxSpRank"));

      filter = xmlHelper.GetValidChild(pepFilter, "Precursor");
      FilterByPrecursor = bool.Parse(xmlHelper.GetChildValue(filter, "Filtered"));
      PrecursorPPMTolerance = MyConvert.ToDouble(xmlHelper.GetChildValue(filter, "Tolerance"));

      Database.RemovePeptideFromDecoyDB = bool.Parse(xmlHelper.GetChildValue(pepFilter, "RemovePeptideFromReversedDB"));

      //skip -- labelled, duplicated scan, different charge as different peptide, 

      //modification deltacn calculation
      SkipSamePeptideButDifferentModificationSite =
        bool.Parse(xmlHelper.GetChildValue(pepFilter, "SkipSamePeptideButDifferentModificationSite"));
      MaxModificationDeltaCn = MyConvert.ToDouble(xmlHelper.GetChildValue(pepFilter, "MaxModificationDeltaCn"));

      XmlNode runNode = xmlHelper.GetValidChild(docRoot, "RunOption");
      Database.Location = xmlHelper.GetChildValue(runNode, "Database");
      Database.AccessNumberPattern = @"(IPI\d+|gi\|\d+|gi:\d+)";

      XmlNode filesXml = xmlHelper.GetValidChild(runNode, "DirectorySet");
      PathNameBin.Clear();
      List<XmlNode> pathNames = xmlHelper.GetChildren(filesXml, "Directory");
      foreach (XmlNode pathName in pathNames)
      {
        PathNameBin[pathName.InnerText] = "";
      }
    }

    public override void ConvertTo(BuildSummaryOptions result)
    {
      base.ConvertTo(result);

      foreach (SequestDatasetOptions ds in result.DatasetList)
      {
        ds.Enabled = true;
        ds.FilterByXcorr = this.FilterByXcorr;
        ds.FilterByDeltaCn = this.FilterByDeltaCn;
        ds.FilterBySpRank = this.FilterBySpRank;
        ds.MaxModificationDeltaCn = this.MaxModificationDeltaCn;
        ds.MaxSpRank = this.MaxSpRank;
        ds.MinDeltaCn = this.MinDeltaCn;
        ds.MinXcorr1 = this.MinXcorr1;
        ds.MinXcorr2 = this.MinXcorr2;
        ds.MinXcorr3 = this.MinXcorr3;
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

      foreach (var ds1 in dsList)
      {
        if (ds1 is SequestDatasetOptions)
        {
          continue;
        }

        throw new Exception(MyConvert.Format("The data set {0} is not Sequest data set.", ds1.Name));
      }

      SequestDatasetOptions ds = dsList[0] as SequestDatasetOptions;
      this.FilterByXcorr = ds.FilterByXcorr;
      this.FilterByDeltaCn = ds.FilterByDeltaCn;
      this.FilterBySpRank = ds.FilterBySpRank;
      this.MaxModificationDeltaCn = ds.MaxModificationDeltaCn;
      this.MaxSpRank = ds.MaxSpRank;
      this.MinDeltaCn = ds.MinDeltaCn;
      this.MinXcorr1 = ds.MinXcorr1;
      this.MinXcorr2 = ds.MinXcorr2;
      this.MinXcorr3 = ds.MinXcorr3;

      foreach (SequestDatasetOptions ds2 in dsList)
      {
        foreach (var file in ds2.PathNames)
        {
          this.PathNameBin[file] = ds2.Name;
        }
      }
    }
  }
}