using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public class ScoreDistributionXmlFormat : IFileFormat<ScoreDistribution>
  {
    #region IFileWriter<ScoreDistribution> Members

    public void WriteToFile(string fileName, ScoreDistribution scoreOrMap)
    {
      XElement root = new XElement("ScoreDistribution",
        from cond in scoreOrMap.Keys
        let ors = scoreOrMap[cond]
        orderby cond.PrecursorCharge, cond.MissCleavageSiteCount, cond.ModificationCount, cond.Classification
        select new XElement("Condition",
          new XElement("PrecursorCharge", cond.PrecursorCharge),
          new XElement("MissCleavageSiteCount", cond.MissCleavageSiteCount),
          new XElement("ModificationCount", cond.ModificationCount),
          new XElement("Classification", cond.Classification),
          new XElement("Distribution",
            from or in ors
            orderby or.Score
            select new XElement("Item",
              new XElement("Score", or.Score),
              new XElement("Target", or.PeptideCountFromTargetDB),
              new XElement("Decoy", or.PeptideCountFromDecoyDB)))));
      root.Save(fileName);
    }

    #endregion

    #region IFileReader<ScoreDistribution> Members

    public ScoreDistribution ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(fileName);
      }

      XElement root = XElement.Load(fileName);

      ScoreDistribution result = ReadFromXml(root);

      return result;
    }

    #endregion

    public ScoreDistribution ReadFromXml(XElement root)
    {
      var conds =
        (from c in root.Descendants("Condition")
         select c).ToList();

      ScoreDistribution result = new ScoreDistribution();

      foreach (var c in conds)
      {
        var key = new OptimalResultCondition(
          Convert.ToInt32(c.Element("PrecursorCharge").Value),
          Convert.ToInt32(c.Element("MissCleavageSiteCount").Value),
          2,
          Convert.ToInt32(c.Element("ModificationCount").Value),
          c.Element("Classification").Value);

        var values =
          (from d in c.Element("Distribution").Descendants("Item")
           select new OptimalResult()
           {
             Score = MyConvert.ToDouble(d.Element("Score").Value),
             PeptideCountFromTargetDB = Convert.ToInt32(d.Element("Target").Value),
             PeptideCountFromDecoyDB = Convert.ToInt32(d.Element("Decoy").Value)
           }
           ).ToList();

        result[key] = values;
      }
      return result;
    }
  }
}
