using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using RCPA.Utils;
using System.Text;
using System;

namespace RCPA.Tools.Quantification
{
  public class CensusChroSlimBuilder : AbstractThreadFileProcessor
  {
    private static string FormatString(Match m)
    {
      return MyConvert.Format("{0:E2}", MyConvert.ToDouble(m.Value));
    }

    public override IEnumerable<string> Process(string filename)
    {
      var xml = new XmlDocument();
      xml.Load(filename);

      SlimDocument(xml, Progress);

      string result = filename + ".slim";
      xml.Save(result);

      return new[] { result };
    }

    public static void SlimDocument(XmlDocument xml, IProgressCallback progress)
    {
      var xmlHelper = new XmlHelper(xml);

      List<XmlNode> proteins = xmlHelper.GetChildren(xml.DocumentElement, "protein");
      progress.SetRange(0, proteins.Count);
      int index = 0;

      var reg = new Regex(@"(\S{8,})");
      foreach (XmlNode protein in proteins)
      {
        progress.SetPosition(index++);

        List<XmlNode> peptides = xmlHelper.GetChildren(protein, "peptide");
        foreach (XmlNode peptide in peptides)
        {
          XmlNode chro = xmlHelper.GetValidChild(peptide, "chro");

          string oldValue = chro.InnerText;
          string[] parts = oldValue.Split(new[] { ';' });
          for (int i = 0; i < parts.Length; i++)
          {
            parts[i] = reg.Replace(parts[i], new MatchEvaluator(FormatString));
          }
          string newValue = StringUtils.Merge(parts, ";");

          if (oldValue.Equals(newValue)) //不需要进行slim
          {
            return;
          }

          double lightMass, heavyMass;
          if (peptide.Attributes["lightMass"] != null)
          {
            lightMass = MyConvert.ToDouble(peptide.Attributes["lightMass"].Value);
            heavyMass = MyConvert.ToDouble(peptide.Attributes["heavyMass"].Value);
            peptide.Attributes["lightMass"].Value = MyConvert.Format("{0:0.0000}", lightMass);
            peptide.Attributes["heavyMass"].Value = MyConvert.Format("{0:0.0000}", heavyMass);
          }
          else if (peptide.Attributes["lightStartMass"] != null)
          {
            lightMass = MyConvert.ToDouble(peptide.Attributes["lightStartMass"].Value);
            heavyMass = MyConvert.ToDouble(peptide.Attributes["heavyStartMass"].Value);
            peptide.Attributes["lightStartMass"].Value = MyConvert.Format("{0:0.0000}", lightMass);
            peptide.Attributes["heavyStartMass"].Value = MyConvert.Format("{0:0.0000}", heavyMass);
          }
        }
      }
    }
  }
}