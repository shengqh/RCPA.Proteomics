using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Modification;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class ClassificationOptions : IXml
  {
    private static bool DEFAULT_ClassifyByCharge = true;
    private static bool DEFAULT_ClassifyByMissCleavage = true;
    private static bool DEFAULT_ClassifyByNumProteaseTermini = true;
    private static bool DEFAULT_ClassifyByModification = false;

    public ClassificationOptions()
    {
      this.ClassifyByCharge = DEFAULT_ClassifyByCharge;
      this.ClassifyByMissCleavage = DEFAULT_ClassifyByMissCleavage;
      this.ClassifyByNumProteaseTermini = DEFAULT_ClassifyByNumProteaseTermini;
      this.ClassifyByModification = DEFAULT_ClassifyByModification;
      this.ModifiedAminoacids = string.Empty;
    }

    public bool ClassifyByCharge { get; set; }

    public bool ClassifyByMissCleavage { get; set; }

    public bool ClassifyByModification { get; set; }

    public bool ClassifyByNumProteaseTermini { get; set; }

    public string ModifiedAminoacids { get; set; }

    delegate int GetInteger(IIdentifiedSpectrum spectrum);

    private IModificationCountCalculator GetModificationCountCalculator()
    {
      if (ClassifyByModification)
      {
        return new ModificationCountCalculator(ModifiedAminoacids, 1);
      }
      else
      {
        return new ModificationCountForwardCalculator(-1);
      }
    }

    private GetInteger GetChargeCalculator()
    {
      if (ClassifyByCharge)
      {
        return delegate(IIdentifiedSpectrum spectrum)
        {
          return spectrum.Query.Charge > 3 ? 3 : spectrum.Query.Charge;
        };
      }
      else
      {
        return delegate(IIdentifiedSpectrum spectrum)
        {
          return -1;
        };
      }
    }

    private GetInteger GetMissCleavageCalculator()
    {
      if (ClassifyByMissCleavage)
      {
        return delegate(IIdentifiedSpectrum spectrum)
        {
          return spectrum.NumMissedCleavages > 3 ? 3 : spectrum.NumMissedCleavages;
        };
      }
      else
      {
        return delegate(IIdentifiedSpectrum spectrum)
        {
          return -1;
        };
      }
    }

    private GetInteger GetNumProteaseTerminiCalculator()
    {
      if (ClassifyByNumProteaseTermini)
      {
        return m => m.NumProteaseTermini;
      }
      else
      {
        return m => -1;
      }
    }

    public List<OptimalItem> BuildSpectrumBin(IEnumerable<IIdentifiedSpectrum> peptides)
    {
      GetInteger chargeCalc = GetChargeCalculator();

      GetInteger missCleavageCalc = GetMissCleavageCalculator();

      IModificationCountCalculator modificationCalc = GetModificationCountCalculator();

      GetInteger nptCalc = GetNumProteaseTerminiCalculator();

      Dictionary<OptimalResultCondition, OptimalItem> resultMap = new Dictionary<OptimalResultCondition, OptimalItem>();

      foreach (IIdentifiedSpectrum mph in peptides)
      {
        int charge = chargeCalc(mph);

        int missCleavage = missCleavageCalc(mph);

        int modificationCount = modificationCalc.Calculate(mph.GetMatchSequence());

        int nptCount = nptCalc(mph);

        OptimalResultCondition cond = new OptimalResultCondition(charge, missCleavage, nptCount, modificationCount, mph.ClassificationTag);
        if (!resultMap.ContainsKey(cond))
        {
          OptimalItem item = new OptimalItem();
          item.Condition = cond;
          item.Spectra.Add(mph);
          resultMap[cond] = item;
        }
        else
        {
          resultMap[cond].Spectra.Add(mph);
        }
      }

      var result = resultMap.Values.ToList();
      result.Sort((m1, m2) => m1.Condition.CompareTo(m2.Condition));
      return result;
    }

    #region IXml Members

    public void Save(XElement parentNode)
    {
      parentNode.Add(new XElement("Classifications",
        new XElement("ClassifyByCharge", ClassifyByCharge.ToString()),
        new XElement("ClassifyByMissCleavage", ClassifyByMissCleavage.ToString()),
        new XElement("ClassifyByModification", ClassifyByModification.ToString()),
        new XElement("ClassifyByNumProteaseTermini", ClassifyByNumProteaseTermini.ToString()),
        new XElement("ModifiedAminoacid", ModifiedAminoacids)));
    }

    public void Load(XElement parentNode)
    {
      XElement xml = parentNode.Element("Classifications");

      this.ClassifyByCharge = xml.GetChildValue("ClassifyByCharge", DEFAULT_ClassifyByCharge);
      this.ClassifyByMissCleavage = xml.GetChildValue("ClassifyByMissCleavage", DEFAULT_ClassifyByMissCleavage);
      this.ClassifyByNumProteaseTermini = xml.GetChildValue("ClassifyByNumProteaseTermini", DEFAULT_ClassifyByNumProteaseTermini);
      this.ClassifyByModification = xml.GetChildValue("ClassifyByModification", DEFAULT_ClassifyByModification);
      this.ModifiedAminoacids = xml.Element("ModifiedAminoacid").Value;
    }

    #endregion

    public static ClassificationOptions LoadOptions(XElement parentNode)
    {
      var result = new ClassificationOptions();
      result.Load(parentNode);
      return result;
    }
  }
}
