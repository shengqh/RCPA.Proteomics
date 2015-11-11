using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using RCPA.Proteomics.Modification;
using RCPA.Seq;
using RCPA.Utils;
using RCPA.Proteomics.Database;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Summary
{
  public enum FalseDiscoveryRateLevel { Protein, Peptide, UniquePeptide, SimpleProtein }

  public enum FalseDiscoveryRateType { Target, Total }

  public static class FalseDiscoveryRateTypeExtension
  {
    private static IFalseDiscoveryRateCalculator TargetFdrCalc = new TargetFalseDiscoveryRateCalculator();
    private static IFalseDiscoveryRateCalculator GlobalFdrCalc = new TotalFalseDiscoveryRateCalculator();

    public static IFalseDiscoveryRateCalculator GetCalculator(this FalseDiscoveryRateType fdrType)
    {
      if (fdrType == FalseDiscoveryRateType.Target)
      {
        return TargetFdrCalc;
      }
      else
      {
        return GlobalFdrCalc;
      }
    }
  }

  public abstract class AbstractSummaryConfiguration
  {
    public AbstractSummaryConfiguration(string applicationTitle, string searchEngine, string fdrScoreType)
    {
      this.ApplicationTitle = applicationTitle;
      this.SearchEngine = searchEngine;
      this.FdrScoreType = fdrScoreType;

      this.FilterByFdr = true;
      this.FdrLevel = FalseDiscoveryRateLevel.Peptide;
      this.FdrType = FalseDiscoveryRateType.Target;
      this.FdrPeptideCount = 10;
      this.FdrValue = 0.01;
      this.MaxPeptideFdr = 0.01;

      this.ClassifyByCharge = true;
      this.ClassifyByMissCleavage = true;
      this.ClassifyByNumProteaseTermini = true;
      this.ClassifyByModification = false;
      this.ModifiedAminoacids = "";
      this.ClassifyByProteinTag = false;
      this.ProteinTag = "";

      this.FilterByPrecursor = false;
      this.FilterByPrecursorSecondIsotopic = true;
      this.PrecursorPPMTolerance = 10;
      this.MergeResult = true;

      this.MergeResultFromSameEngineButDifferentSearchParameters = true;

      this.FilterBySequenceLength = false;
      this.MinSequenceLength = 6;

      this.TargetDecoyConflictType = ResolveTargetDecoyConflictTypeFactory.Decoy;
    }

    public DatabaseOptions Database = new DatabaseOptions();

    public string ApplicationTitle { get; set; }

    public string SearchEngine { get; set; }

    public bool MergeResultFromSameEngineButDifferentSearchParameters { get; set; }

    //False Discovery Rate
    public bool FilterByFdr { get; set; }

    public FalseDiscoveryRateLevel FdrLevel { get; set; }

    public FalseDiscoveryRateType FdrType { get; set; }

    public int FdrPeptideCount { get; set; }

    public double MaxPeptideFdr { get; set; }

    public double FdrValue { get; set; }

    public string FdrScoreType { get; set; }

    public bool ClassifyByCharge { get; set; }

    public bool ClassifyByMissCleavage { get; set; }

    public bool ClassifyByNumProteaseTermini { get; set; }

    public bool ClassifyByModification { get; set; }

    public string ModifiedAminoacids { get; set; }

    public bool ClassifyByProteinTag { get; set; }
    
    public string ProteinTag { get; set; }

    public ITargetDecoyConflictType TargetDecoyConflictType { get; set; }

    //Peptide Filter
    public bool FilterByPrecursor { get; set; }

    public bool FilterByPrecursorSecondIsotopic { get; set; }

    public bool FilterByPrecursorDynamicTolerance { get; set; }

    public double PrecursorPPMTolerance { get; set; }

    public bool FilterBySequenceLength { get; set; }

    public int MinSequenceLength { get; set; }

    public bool MergeResult { get; set; }

    //Dataset
    private Dictionary<string, string> pathNameBin = new Dictionary<string, string>();

    public Dictionary<string, string> PathNameBin
    {
      get { return pathNameBin; }
    }
    protected virtual void LoadSpecialDefinition(XmlNode docRoot) { }

    protected virtual void LoadPeptideFilter(XmlNode peptideFilterXml)
    {
      var xmlHelper = new XmlHelper(peptideFilterXml.OwnerDocument);

      var filter = xmlHelper.GetFirstChildByNameAndAttribute(peptideFilterXml, "Filter", "Name", "PrecursorPPM");
      if (null != filter)
      {
        this.FilterByPrecursor = Convert.ToBoolean(filter.Attributes["Active"].Value);
        this.PrecursorPPMTolerance = MyConvert.ToDouble(filter.Attributes["Value"].Value);
      }

      filter = xmlHelper.GetFirstChildByNameAndAttribute(peptideFilterXml, "Filter", "Name", "PrecursorSecondIsotopic");
      if (null != filter)
      {
        this.FilterByPrecursorSecondIsotopic = Convert.ToBoolean(filter.Attributes["Active"].Value);
      }

      filter = xmlHelper.GetFirstChildByNameAndAttribute(peptideFilterXml, "Filter", "Name", "MinSequenceLength");
      if (null != filter)
      {
        this.FilterBySequenceLength = Convert.ToBoolean(filter.Attributes["Active"].Value);
        this.MinSequenceLength = Convert.ToInt32(filter.Attributes["Value"].Value);
      }
    }

    public virtual void LoadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException("Parameter file not found", fileName);
      }

      XmlDocument doc = new XmlDocument();
      doc.Load(fileName);
      XmlNode docRoot = doc.DocumentElement;

      var xmlHelper = new XmlHelper(doc);

      this.ApplicationTitle = xmlHelper.GetChildValue(docRoot, "Version");
      this.SearchEngine = xmlHelper.GetChildValue(docRoot, "SearchEngine");

      XmlNode fdrXml = xmlHelper.GetValidChild(docRoot, "FalseDiscoveryRate");
      if (null != fdrXml)
      {
        this.FilterByFdr = bool.Parse(xmlHelper.GetChildValue(fdrXml, "Filtered"));
        this.FdrLevel = (FalseDiscoveryRateLevel)Enum.Parse(FalseDiscoveryRateLevel.Peptide.GetType(), xmlHelper.GetChildValue(fdrXml, "Level"), true);
        this.FdrType = (FalseDiscoveryRateType)Enum.Parse(FalseDiscoveryRateType.Target.GetType(), xmlHelper.GetChildValue(fdrXml, "Type"), true);
        this.FdrValue = MyConvert.ToDouble(xmlHelper.GetChildValue(fdrXml, "Value"));
        this.MaxPeptideFdr = MyConvert.ToDouble(xmlHelper.GetChildValue(fdrXml, "MaxPeptideFdr", "0.01"));

        if (xmlHelper.HasChild(fdrXml, "FdrPeptideCount"))
        {
          this.FdrPeptideCount = Convert.ToInt32(xmlHelper.GetChildValue(fdrXml, "FdrPeptideCount"));
        }

        this.FdrScoreType = xmlHelper.GetChildValue(fdrXml, "ScoreType");
        this.ClassifyByCharge = bool.Parse(xmlHelper.GetChildValue(fdrXml, "ClassifyByCharge"));
        this.ClassifyByMissCleavage = bool.Parse(xmlHelper.GetChildValue(fdrXml, "ClassifyByMissCleavage"));
        this.ClassifyByModification = bool.Parse(xmlHelper.GetChildValue(fdrXml, "ClassifyByModification"));
        this.ClassifyByNumProteaseTermini = bool.Parse(xmlHelper.GetChildValue(fdrXml, "ClassifyByNumProteaseTermini", true.ToString()));
        this.ModifiedAminoacids = xmlHelper.GetChildValue(fdrXml, "ModifiedAminoacid");

        this.ClassifyByProteinTag = bool.Parse(xmlHelper.GetChildValue(fdrXml, "ClassifyByProteinTag", false.ToString()));
        this.ProteinTag = xmlHelper.GetChildValue(fdrXml, "ProteinTag", string.Empty);
        
        this.TargetDecoyConflictType = ResolveTargetDecoyConflictTypeFactory.Find(xmlHelper.GetChildValue(fdrXml, "TargetDecoyConflictType", ResolveTargetDecoyConflictTypeFactory.Decoy.Name));
      }

      XmlNode peptideFilterXml = xmlHelper.GetValidChild(docRoot, "PeptideFilter");
      if (null != peptideFilterXml)
      {
        LoadPeptideFilter(peptideFilterXml);
      }

      XmlNode databaseXml = xmlHelper.GetValidChild(docRoot, "Database");
      this.Database.Location = xmlHelper.GetChildValue(databaseXml, "Location");
      this.Database.AccessNumberPattern = xmlHelper.GetChildValue(databaseXml, "AccessNumberPattern").Replace("&gt;", ">");
      this.Database.DecoyPattern = xmlHelper.GetChildValue(databaseXml, "DecoyPattern").Replace("&gt;", ">");
      if (xmlHelper.HasChild(databaseXml, "ContaminationPattern"))
      {
        this.Database.ContaminationNamePattern = xmlHelper.GetChildValue(databaseXml, "ContaminationPattern").Replace("&gt;", ">");
      }

      this.Database.RemovePeptideFromDecoyDB = bool.Parse(xmlHelper.GetChildValue(databaseXml, "RemovePeptideFromDecoyDB"));

      LoadSpecialDefinition(docRoot);

      XmlNode filesXml = xmlHelper.GetValidChild(docRoot, "PathNames");
      if (null == filesXml)
      {
        throw new ArgumentException(MyConvert.Format("There is no PathNames section in parameter file {0}", fileName));
      }

      this.pathNameBin.Clear();
      XmlNode pathNameXml = filesXml.FirstChild;
      while (null != pathNameXml)
      {
        string bin = pathNameXml.Attributes["Bin"].Value;
        this.pathNameBin[pathNameXml.InnerText] = bin;
        pathNameXml = pathNameXml.NextSibling;
      }
    }

    public virtual void ConvertTo(BuildSummaryOptions result)
    {
      result.ApplicationTitle = this.ApplicationTitle;
      result.MergeResult = this.MergeResult;
      result.KeepTopPeptideFromSameEngineButDifferentSearchParameters = this.MergeResultFromSameEngineButDifferentSearchParameters;

      result.Classification.ClassifyByCharge = this.ClassifyByCharge;
      result.Classification.ClassifyByMissCleavage = this.ClassifyByMissCleavage;
      result.Classification.ClassifyByModification = this.ClassifyByModification;
      result.Classification.ClassifyByNumProteaseTermini = this.ClassifyByNumProteaseTermini;
      result.Classification.ModifiedAminoacids = this.ModifiedAminoacids;
      result.Classification.ClassifyByProteinTag = this.ClassifyByProteinTag;
      result.Classification.ProteinTag = this.ProteinTag;

      result.FalseDiscoveryRate.FilterByFdr = this.FilterByFdr;
      result.FalseDiscoveryRate.FdrLevel = this.FdrLevel;
      result.FalseDiscoveryRate.FdrType = this.FdrType;
      result.FalseDiscoveryRate.FdrValue = this.FdrValue;
      result.FalseDiscoveryRate.FdrPeptideCount = this.FdrPeptideCount;
      result.FalseDiscoveryRate.MaxPeptideFdr = this.MaxPeptideFdr;
      result.FalseDiscoveryRate.TargetDecoyConflictType = this.TargetDecoyConflictType;

      result.Database = new DatabaseOptions(this.Database);

      result.PeptideFilter.FilterBySequenceLength = this.FilterBySequenceLength;
      result.PeptideFilter.MinSequenceLength = this.MinSequenceLength;

      var dics = from s in this.PathNameBin
                 group s.Key by s.Value;

      result.DatasetList.Clear();
      foreach (var tag in dics)
      {
        var ds = DatasetListOptions.GetDatasetOption(this.SearchEngine);
        ds.Name = tag.Key;
        ds.Parent = result;
        ds.PathNames = (from t in tag
                        select t).ToList();
        ds.FilterByPrecursor = this.FilterByPrecursor;
        ds.FilterByPrecursorIsotopic = this.FilterByPrecursorSecondIsotopic;
        ds.FilterByPrecursorDynamicTolerance = this.FilterByPrecursorDynamicTolerance;

        ds.PrecursorPPMTolerance = this.PrecursorPPMTolerance;
        result.DatasetList.Add(ds);
      }
    }

    public virtual void ConvertFrom(BuildSummaryOptions result)
    {
      this.ApplicationTitle = result.ApplicationTitle;
      this.MergeResult = result.MergeResult;
      this.MergeResultFromSameEngineButDifferentSearchParameters = result.KeepTopPeptideFromSameEngineButDifferentSearchParameters;

      this.ClassifyByCharge = result.Classification.ClassifyByCharge;
      this.ClassifyByMissCleavage = result.Classification.ClassifyByMissCleavage;
      this.ClassifyByModification = result.Classification.ClassifyByModification;
      this.ClassifyByNumProteaseTermini = result.Classification.ClassifyByNumProteaseTermini;
      this.ModifiedAminoacids = result.Classification.ModifiedAminoacids;
      this.ClassifyByProteinTag = result.Classification.ClassifyByProteinTag;
      this.ProteinTag = result.Classification.ProteinTag;

      this.FilterByFdr = result.FalseDiscoveryRate.FilterByFdr;
      this.FdrLevel = result.FalseDiscoveryRate.FdrLevel;
      this.FdrType = result.FalseDiscoveryRate.FdrType;
      this.FdrValue = result.FalseDiscoveryRate.FdrValue;
      this.FdrPeptideCount = result.FalseDiscoveryRate.FdrPeptideCount;
      this.MaxPeptideFdr = result.FalseDiscoveryRate.MaxPeptideFdr;
      this.TargetDecoyConflictType = result.FalseDiscoveryRate.TargetDecoyConflictType;

      this.Database = new DatabaseOptions(result.Database);

      this.FilterBySequenceLength = result.PeptideFilter.FilterBySequenceLength;
      this.MinSequenceLength = result.PeptideFilter.MinSequenceLength;

      var ds = result.DatasetList.First(m => m.Enabled);
      if (ds != null)
      {
        this.FilterByPrecursor = ds.FilterByPrecursor;
        this.FilterByPrecursorSecondIsotopic = ds.FilterByPrecursorIsotopic;
        this.FilterByPrecursorDynamicTolerance = ds.FilterByPrecursorDynamicTolerance;
        this.PrecursorPPMTolerance = ds.PrecursorPPMTolerance;
      }
    }
  }
}
