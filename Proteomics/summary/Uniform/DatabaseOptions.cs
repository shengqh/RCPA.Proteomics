using RCPA.Seq;
using RCPA.Utils;
using System.Xml.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class DatabaseOptions : IXml
  {
    private string _location;
    public string Location
    {
      get
      {
        return _location;
      }
      set
      {
        _location = value;
        _contaminationGroupFilter = null;
      }
    }

    private string _accessNumberPattern;
    public string AccessNumberPattern
    {
      get
      {
        return _accessNumberPattern;
      }
      set
      {
        _accessNumberPattern = value;
        _contaminationGroupFilter = null;
      }
    }

    public string DecoyPattern { get; set; }

    public bool DecoyPatternDefined
    {
      get
      {
        return !string.IsNullOrEmpty(DecoyPattern);
      }
    }

    public bool RemovePeptideFromDecoyDB { get; set; }

    public bool RemoveContamination { get; set; }

    public string ContaminationNamePattern { get; set; }

    private string _contaminationDescriptionPattern;
    public string ContaminationDescriptionPattern
    {
      get
      {
        return _contaminationDescriptionPattern;
      }
      set
      {
        _contaminationDescriptionPattern = value;
        _contaminationGroupFilter = null;
      }
    }

    public DatabaseOptions()
    {
      Location = string.Empty;
      AccessNumberPattern = string.Empty;
      DecoyPattern = string.Empty;
      RemovePeptideFromDecoyDB = false;
      RemoveContamination = false;
      ContaminationNamePattern = string.Empty;
      ContaminationDescriptionPattern = string.Empty;
    }

    public DatabaseOptions(DatabaseOptions source)
    {
      this.Location = source.Location;
      this.AccessNumberPattern = source.AccessNumberPattern;
      this.DecoyPattern = source.DecoyPattern;
      this.RemovePeptideFromDecoyDB = source.RemovePeptideFromDecoyDB;
      this.RemoveContamination = source.RemoveContamination;
      this.ContaminationNamePattern = source.ContaminationNamePattern;
      this.ContaminationDescriptionPattern = source.ContaminationDescriptionPattern;
    }

    public IFilter<IIdentifiedSpectrum> GetContaminationNameFilter()
    {
      if (RemoveContamination && !string.IsNullOrEmpty(this.ContaminationNamePattern))
      {
        return new IdentifiedSpectrumProteinNameRegexFilter(this.ContaminationNamePattern, false);
      }
      return null;
    }

    private IIdentifiedProteinGroupFilter _contaminationGroupFilter;

    public bool HasContaminationDescriptionFilter()
    {
      return RemoveContamination && !string.IsNullOrEmpty(ContaminationDescriptionPattern);
    }

    public IIdentifiedProteinGroupFilter GetContaminationDescriptionFilter(IProgressCallback progress)
    {
      if (HasContaminationDescriptionFilter())
      {
        if (_contaminationGroupFilter == null)
        {
          var acParser = GetAccessNumberParser();

          var map = IdentifiedResultUtils.GetContaminationAccessNumbers(acParser, Location, ContaminationDescriptionPattern, progress);

          _contaminationGroupFilter = new IdentifiedProteinGroupContaminationMapFilter(acParser, map);
        }
        return _contaminationGroupFilter;
      }

      return null;
    }

    public IIdentifiedProteinGroupFilter GetNotContaminationDescriptionFilter(IProgressCallback progress)
    {
      var result = GetContaminationDescriptionFilter(progress);

      if (result != null)
      {
        return new IdentifiedProteinGroupNotFilter(result);
      }

      return null;
    }

    public IAccessNumberParser GetAccessNumberParser()
    {
      return AccessNumberParserFactory.FindOrCreateParser(AccessNumberPattern, AccessNumberPattern);
    }

    #region IXml Members

    public void Save(XElement parentNode)
    {
      parentNode.Add(new XElement("Database",
        new XElement("Location", Location),
        new XElement("AccessNumberPattern", AccessNumberPattern),
        new XElement("DecoyPattern", DecoyPattern),
        new XElement("RemoveContamination", RemoveContamination.ToString()),
        new XElement("ContaminationNamePattern", ContaminationNamePattern),
        new XElement("ContaminationDescriptionPattern", ContaminationDescriptionPattern),
        new XElement("RemovePeptideFromDecoyDB", RemovePeptideFromDecoyDB.ToString())));
    }

    public void Load(XElement parentNode)
    {
      XElement databaseXml = parentNode.Element("Database");
      this.Location = databaseXml.GetChildValue("Location", this.Location);
      this.AccessNumberPattern = databaseXml.GetChildValue("AccessNumberPattern", this.AccessNumberPattern).Replace("&gt;", ">");
      this.DecoyPattern = databaseXml.GetChildValue("DecoyPattern", this.DecoyPattern).Replace("&gt;", ">");

      if (databaseXml.Element("ContaminationPattern") != null)
      {//compatible to old version
        this.ContaminationNamePattern = databaseXml.GetChildValue("ContaminationPattern", this.ContaminationNamePattern).Replace("&gt;", ">");
        this.RemoveContamination = !string.IsNullOrEmpty(ContaminationNamePattern);
      }
      else
      {
        this.RemoveContamination = databaseXml.GetChildValue("RemoveContamination", this.RemoveContamination);
        this.ContaminationNamePattern = databaseXml.GetChildValue("ContaminationNamePattern", this.ContaminationNamePattern).Replace("&gt;", ">");
      }

      this.ContaminationDescriptionPattern = databaseXml.GetChildValue("ContaminationDescriptionPattern", this.ContaminationDescriptionPattern).Replace("&gt;", ">");
      this.RemovePeptideFromDecoyDB = databaseXml.GetChildValue("RemovePeptideFromDecoyDB", this.RemovePeptideFromDecoyDB);
    }

    #endregion

    public static DatabaseOptions LoadOptions(XElement parentNode)
    {
      var result = new DatabaseOptions();
      result.Load(parentNode);
      return result;
    }
  }
}
