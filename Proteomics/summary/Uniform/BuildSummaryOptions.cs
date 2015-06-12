using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using RCPA.Proteomics.Modification;
using RCPA.Seq;
using RCPA.Utils;
using RCPA.Proteomics.Database;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Mascot;
using RCPA.Gui;
using System.Windows.Forms;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class BuildSummaryOptions
  {
    public BuildSummaryOptions()
    {
      MergeResult = true;
      KeepTopPeptideFromSameEngineButDifferentSearchParameters = true;
      SavePeptidesFile = true;
      IsSemiPeptideConsiderAsUnique = true;
      Database = new DatabaseOptions();
      FalseDiscoveryRate = new FalseDiscoveryRateOptions();
      Classification = new ClassificationOptions();
      PeptideFilter = new PeptideFilterOptions();
      DatasetList = new DatasetListOptions();
      ConflictType = ResolveSearchEngineConflictTypeFactory.QValue;
      MinimumEngineAgreeCount = 1;
    }

    public BuildSummaryOptions(string fileName)
      : this()
    {
      LoadFromFile(fileName);
    }

    public string ApplicationTitle { get; set; }

    /// <summary>
    /// 对于多引擎搜索结果，保留至少被MultipleEngineAgreeCount引擎相同鉴定的PSM。
    /// </summary>
    public int MinimumEngineAgreeCount { get; set; }

    public bool MergeResult { get; set; }

    public bool KeepTopPeptideFromSameEngineButDifferentSearchParameters { get; set; }

    public bool SavePeptidesFile { get; set; }

    public DatabaseOptions Database { get; set; }

    public bool IsSemiPeptideConsiderAsUnique { get; set; }

    public FalseDiscoveryRateOptions FalseDiscoveryRate { get; set; }

    public ClassificationOptions Classification { get; set; }

    public PeptideFilterOptions PeptideFilter { get; set; }

    public DatasetListOptions DatasetList { get; set; }

    public IResolveSearchEngineConflictType ConflictType { get; set; }

    public void LoadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException("Parameter file not found", fileName);
      }

      XElement docRoot = XElement.Load(fileName);

      ApplicationTitle = docRoot.Element("Version").Value;

      MergeResult = Convert.ToBoolean(docRoot.Element("MergeResult").Value);

      ConflictType = ResolveSearchEngineConflictTypeFactory.Find(docRoot.GetChildValue("ConflictType", ResolveSearchEngineConflictTypeFactory.DiscardAll.Name));

      if (docRoot.Element("MinimumEngineAgreeCount") != null)
      {
        MinimumEngineAgreeCount = int.Parse(docRoot.Element("MinimumEngineAgreeCount").Value);
      }

      if (docRoot.Element("MergeResultFromSameEngineButDifferentSearchParameters") != null)
      {
        KeepTopPeptideFromSameEngineButDifferentSearchParameters = bool.Parse(docRoot.Element("MergeResultFromSameEngineButDifferentSearchParameters").Value);
      }

      Database.Load(docRoot);

      FalseDiscoveryRate.Load(docRoot);

      Classification.Load(docRoot);

      PeptideFilter.Load(docRoot);

      try
      {
        DatasetList.Load(docRoot);
        DatasetList.ForEach(m => m.Parent = this);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Load dataset error :" + ex.Message);
      }
    }

    public virtual void SaveToFile(string fileName)
    {
      if (File.Exists(fileName))
      {
        File.Delete(fileName);
      }

      var docRoot = ToXml();

      docRoot.Save(fileName);
    }

    public XElement ToXml()
    {
      XElement docRoot = new XElement("BuildSummaryOption",
        new XElement("Version", ApplicationTitle),
        new XElement("MergeResult", MergeResult),
        new XElement("ConflictType", ConflictType),
        new XElement("MinimumEngineAgreeCount", MinimumEngineAgreeCount),
        new XElement("MergeResultFromSameEngineButDifferentSearchParameters", KeepTopPeptideFromSameEngineButDifferentSearchParameters)
        );

      Database.Save(docRoot);

      FalseDiscoveryRate.Save(docRoot);

      Classification.Save(docRoot);

      PeptideFilter.Save(docRoot);

      DatasetList.Save(docRoot);

      return docRoot;
    }

    internal List<string> GetFilterString()
    {
      return (new string[] { }).ToList();
    }

    public IFilter<IIdentifiedSpectrum> GetDecoySpectrumFilter()
    {
      return FalseDiscoveryRate.TargetDecoyConflictType.GetSpectrumFilter(this.Database.DecoyPattern);
    }

    public IIdentifiedProteinGroupFilter GetDecoyGroupFilter()
    {
      return FalseDiscoveryRate.TargetDecoyConflictType.GetGroupFilter(this.Database.DecoyPattern);
    }

    public IIdentifiedSpectrumBuilder GetSpectrumBuilder()
    {
      return FalseDiscoveryRate.GetSpectrumBuilder();
    }

    public IIdentifiedResultBuilder GetIdentifiedResultBuilder()
    {
      if (DatasetList.All(m => m.PathNames.All(l => l.ToLower().EndsWith("msf"))))
      {
        return new IdentifiedResultMsfBuilder((from ds in DatasetList
                                               from file in ds.PathNames
                                               select file).ToArray(), Database.GetAccessNumberParser());
      }

      return new IdentifiedResultBuilder(Database.GetAccessNumberParser(), Database.Location);
    }

    public IFileFormat<List<IIdentifiedSpectrum>> GetIdentifiedSpectrumFormat()
    {
      return new MascotPeptideTextFormat(UniformHeader.PEPTIDE_HEADER);
    }

    //public IFileFormat<IIdentifiedResult> GetIdetifiedResultFormat()
    //{
    //  return new MascotResultTextFormat(UniformHeader.PROTEIN_HEADER, UniformHeader.PEPTIDE_HEADER);
    //}

    public IConflictProcessor GetConflictFunc()
    {
      return ConflictType.GetProcessor();
    }

    public IFileFormat<IIdentifiedResult> GetIdetifiedResultFormat(IIdentifiedResult finalResult, IProgressCallback progress)
    {
      //保存非冗余蛋白质列表文件
      var peptideHeader = GetPeptideHeader(finalResult);
      return new MascotResultTextFormat(UniformHeader.PROTEIN_HEADER, peptideHeader)
      {
        Progress = progress
      };
    }

    private static string GetPeptideHeader(IIdentifiedResult finalResult)
    {
      var peptideHeader = UniformHeader.PEPTIDE_HEADER;
      if (finalResult.All(m => m.All(l => l.Peptides.All(k => k.Spectrum.Query.FileScan.RetentionTime == 0.0))))
      {
        peptideHeader = peptideHeader.Replace("\tRetentionTime", "");
      }
      return peptideHeader;
    }
  }
}
