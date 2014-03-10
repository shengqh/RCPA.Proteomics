using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqProteinStatisticOption : IXml
  {
    public string ProteinFileName { get; set; }

    public string ITraqFileName { get; set; }

    public IRatioPeptideToProteinBuilder RatioPeptideToProteinBuilder { get; set; }

    public List<IsobaricIndex> References { get; set; }

    public List<IsobaricIndex> GetSamples(IsobaricType sampleType)
    {
      var result = sampleType.GetFuncs();
      result.RemoveAll(m => References.Any(n => n.Name.Equals(m.Name)));
      return result;
    }

    public double MinimumProbability { get; set; }

    public Dictionary<string, List<string>> DatasetMap { get; set; }

    public bool NormalizeByMedianRatio { get; set; }

    public bool QuantifyModifiedPeptideOnly { get; set; }

    public string ModificationChars { get; set; }

    public IITraqProteinRatioCalculator GetRatioCalculator()
    {
      return new ITraqProteinRatioCalculator()
      {
        OutlierDetector = new MADOutlierDetector(),
        RatioBuilder = this.RatioPeptideToProteinBuilder,
        GetReference = this.GetReference
      };
    }

    public IITraqItemValidator GetValidator()
    {
      if (References.Count == 2)
      {
        return new ITraqItemValidator2(References[0], References[1], MinimumProbability);
      }
      else
      {
        return new EmptyValidator();
      }
    }

    public double GetReference(IsobaricItem item)
    {
      return (from f in References
              select f.GetValue(item)).Average();
    }

    #region IXml Members

    public void Save(System.Xml.Linq.XElement parentNode)
    {
      parentNode.Add(
        new XElement("ProteinFileName", ProteinFileName),
        new XElement("ITraqFileName", ITraqFileName),
        new XElement("RatioPeptideToProteinBuilderName", RatioPeptideToProteinBuilder.Name),
        new XElement("References", from refFunc in References
                                   select new XElement("Index", refFunc.Index)
        ),
        new XElement("MinProbability", MinimumProbability),
        new XElement("NormalizeByMedianRatio", NormalizeByMedianRatio),
        new XElement("QuantifyModifiedPeptideOnly", QuantifyModifiedPeptideOnly),
        new XElement("ModificationChars", ModificationChars),
        new XElement("DatasetMap",
          from ds in DatasetMap
          select new XElement("Dataset",
            new XElement("Name", ds.Key),
            new XElement("Values",
            from d in ds.Value
            select new XElement("Value", d)))));
    }

    public void Load(System.Xml.Linq.XElement parentNode)
    {
      ITraqFileName = parentNode.Element("ITraqFileName").Value;
      RatioPeptideToProteinBuilder = RatioPeptideToProteinBuilderFactory.FindBuilder(parentNode.Element("RatioPeptideToProteinBuilderName").Value);
      References = (from refname in parentNode.Element("References").Elements("Index")
                    let index = int.Parse(refname.Value)
                    select new IsobaricIndex(index)).ToList();
      MinimumProbability = MyConvert.ToDouble(parentNode.Element("MinProbability").Value);
      NormalizeByMedianRatio = bool.Parse(parentNode.Element("NormalizeByMedianRatio").Value);

      if (parentNode.Element("QuantifyModifiedPeptideOnly") != null)
      {
        QuantifyModifiedPeptideOnly = bool.Parse(parentNode.Element("QuantifyModifiedPeptideOnly").Value);
      }
      else
      {
        QuantifyModifiedPeptideOnly = false;
      }

      if (parentNode.Element("ModificationChars") != null)
      {
        ModificationChars = parentNode.Element("ModificationChars").Value;
      }
      else
      {
        ModificationChars = "!#";
      }

      DatasetMap = new Dictionary<string, List<string>>();
      foreach (var ds in parentNode.Element("DatasetMap").Elements("Dataset"))
      {
        var name = ds.Element("Name").Value;
        var value = (from v in ds.Element("Values").Elements("Value") select v.Value).ToList();
        DatasetMap[name] = value;
      }
    }

    #endregion
  }
}
