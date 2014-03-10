using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RCPA.Proteomics.Summary
{
  public class PeptideFilterOptions : IXml
  {
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

    public virtual IFilter<IIdentifiedSpectrum> GetFilter()
    {
      if (FilterBySequenceLength)
      {
        return new IdentifiedSpectrumSequenceLengthFilter(MinSequenceLength);
      }

      return null;
    }

    #region IXml Members

    public void Save(XElement parentNode)
    {
      parentNode.Add(new XElement("PeptideFilter",
        OptionUtils.FilterToXml("MinSequenceLength", FilterBySequenceLength, MinSequenceLength)));
    }

    public void Load(XElement parentNode)
    {
      XElement xml = parentNode.Element("PeptideFilter");
      OptionUtils.XmlToFilter(xml, "MinSequenceLength", out _filterBySequenceLength, out _minSequenceLength);
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
