using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Classification;
using System.IO;

namespace RCPA.Proteomics.Distribution
{
  public class DistributionOption
  {
    public string SourceFileName { get; set; }

    public DistributionType DistributionType { get; set; }

    public string ClassificationPrinciple { get; set; }

    public PeptideFilterType FilterType { get; set; }

    public int FilterFrom { get; set; }

    public int FilterTo { get; set; }

    public int FilterStep { get; set; }

    public bool ModifiedPeptideOnly { get; set; }

    public string ModifiedPeptide { get; set; }

    public bool ClassifiedByTag { get; set; }

    public Dictionary<string, List<string>> ClassificationSet { get ;set; }

    public DistributionOption()
    {
      this.SourceFileName = string.Empty;
      this.DistributionType = DistributionType.Protein;
      this.ClassificationPrinciple = "SAMPLE";
      this.FilterType = PeptideFilterType.PeptideCount;
      this.FilterFrom = 1;
      this.FilterTo = 1;
      this.FilterStep = 1;
      this.ModifiedPeptideOnly = false;
      this.ModifiedPeptide = string.Empty;
      this.ClassifiedByTag = false;
      this.ClassificationSet = new Dictionary<string, List<string>>();
    }

    public IClassification<IIdentifiedPeptide> GetClassification()
    {
      if (this.ClassifiedByTag)
      {
        return new IdentifiedPeptideTagClassification(ClassificationPrinciple);
      }
      else
      {
        Dictionary<string, string> classificationMap = new Dictionary<string, string>();

        foreach (var s in ClassificationSet)
        {
          foreach (var exp in s.Value)
          {
            classificationMap[exp] = s.Key;
          }
        }

        return new IdentifiedPeptideMapClassification(ClassificationPrinciple, classificationMap);
      }
    }

    public string[] GetClassifiedNames()
    {
      return ClassificationSet.Keys.ToArray();
    }

    public int GetMaxPeptideCountWidth()
    {
      int iMinCount = this.FilterFrom;

      for (int i = this.FilterFrom; i <= this.FilterTo; i += this.FilterStep)
      {
        iMinCount = i;
      }

      return iMinCount.ToString().Length;
    }
  }
}
