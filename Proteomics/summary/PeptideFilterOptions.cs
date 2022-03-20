using System.Collections.Generic;
using System.Xml.Linq;

namespace RCPA.Proteomics.Summary
{
  public class PeptideFilterOptions : IXml
  {
    public static int DEFAULT_MinSequenceLength = 7;
    public static int DEFAULT_MaxMissCleavage = 2;

    public PeptideFilterOptions()
    {
      FilterByMaxMissCleavage = true;
      MaxMissCleavage = DEFAULT_MaxMissCleavage;
      FilterBySequenceLength = true;
      MinSequenceLength = DEFAULT_MinSequenceLength;
    }

    private bool _filterBySequenceLength;
    public bool FilterBySequenceLength
    {
      get { return _filterBySequenceLength; }
      set { _filterBySequenceLength = value; }
    }

    private int _minSequenceLength;
    public int MinSequenceLength
    {
      get { return _minSequenceLength; }
      set { _minSequenceLength = value; }
    }

    private bool _filterByMaxMissCleavage;
    public bool FilterByMaxMissCleavage
    {
      get { return _filterByMaxMissCleavage; }
      set { _filterByMaxMissCleavage = value; }
    }

    private int _maxMissCleavage;
    public int MaxMissCleavage
    {
      get { return _maxMissCleavage; }
      set { _maxMissCleavage = value; }
    }

    public virtual IFilter<IIdentifiedSpectrum> GetFilter()
    {
      var result = new List<IFilter<IIdentifiedSpectrum>>();
      if (FilterBySequenceLength)
      {
        result.Add(new IdentifiedSpectrumSequenceLengthFilter(MinSequenceLength));
      }
      if (FilterByMaxMissCleavage)
      {
        result.Add(new IdentifiedSpectrumMaxMissCleavageFilter(MaxMissCleavage));
      }

      if (result.Count == 0)
      {
        return null;
      }

      if (result.Count == 1)
      {
        return result[0];
      }

      return new AndFilter<IIdentifiedSpectrum>(result);
    }

    #region IXml Members

    public void Save(XElement parentNode)
    {
      parentNode.Add(new XElement("PeptideFilter",
        OptionUtils.FilterToXml("MinSequenceLength", FilterBySequenceLength, MinSequenceLength),
        OptionUtils.FilterToXml("MaxMissCleavage", FilterByMaxMissCleavage, MaxMissCleavage)));
    }

    public void Load(XElement parentNode)
    {
      XElement xml = parentNode.Element("PeptideFilter");
      OptionUtils.XmlToFilter(xml, "MinSequenceLength", out _filterBySequenceLength, out _minSequenceLength);
      if (xml.Element("MaxMissCleavage") != null)
      {
        OptionUtils.XmlToFilter(xml, "MaxMissCleavage", out _filterByMaxMissCleavage, out _maxMissCleavage);
      }
      else
      {
        _filterByMaxMissCleavage = true;
        _maxMissCleavage = DEFAULT_MaxMissCleavage;
      }
    }

    #endregion

    public static PeptideFilterOptions LoadOptions(XElement parentNode)
    {
      var result = new PeptideFilterOptions();
      result.Load(parentNode);
      return result;
    }
  }
}
