using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Seq;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using System.IO;
using RCPA.Utils;

namespace RCPA.Proteomics.Distribution
{
  public class ProteinDistributionCalculator : AbstractDistributionCalculator
  {
    protected IAccessNumberParser parser;

    protected override void InitializeFromOption(string optionFile)
    {
      base.InitializeFromOption(optionFile);

      parser = AccessNumberParserFactory.GetAutoParser();
    }

    public ProteinDistributionCalculator(bool exportIndividual)
      : base("Protein", exportIndividual)
    { }

    private SequestResultTextFormat format = new SequestResultTextFormat();

    protected override void ParseToCalculationItems()
    {
      IIdentifiedResult sr = format.ReadFromFile(option.SourceFileName);

      calculationItems =
        (from proteinGroup in sr
         select new CalculationItem()
         {
           Key = proteinGroup,
           Peptides = proteinGroup[0].GetDistinctPeptides()
         }).ToList();
    }

    protected override void PrintHeader(StreamWriter pw)
    {
      pw.Write("Protein\tDescription");

      if (ExportPeptideCountOnly)
      {
        printClassifiedNames(pw, false);
      }
      else
      {
        printClassifiedNames(pw, "_UniPepCount");
        printClassifiedNames(pw, "_PepCount");
      }
    }

    protected void PrintClassifiedPeptideCount(StreamWriter pw, CalculationItem calculationItem)
    {
      if (!ExportPeptideCountOnly)
      {
        foreach (var s in option.GetClassifiedNames())
        {
          Count count = calculationItem.Classifications[s];
          pw.Write("\t" + count.UniquePeptideCount);
          pw.Write("\t" + GetRank(uniquePeptideCounts[s], count.UniquePeptideCount));
        }
      }

      foreach (var s in option.GetClassifiedNames())
      {
        Count count = calculationItem.Classifications[s];
        pw.Write("\t" + count.PeptideCount);
        if (!ExportPeptideCountOnly)
        {
          pw.Write("\t" + GetRank(peptideCounts[s], count.PeptideCount));
        }
      }
    }

    protected override void PrintItem(StreamWriter pw, CalculationItem calculationItem)
    {
      IIdentifiedProteinGroup progroup = (IIdentifiedProteinGroup)calculationItem.Key;

      pw.Write(StringUtils.Merge(
        from p in progroup select parser.GetValue(p.Name),
        " ! "));

      pw.Write("\t");

      pw.Write(StringUtils.Merge(
        from p in progroup select p.Description,
        " ! "));

      PrintClassifiedPeptideCount(pw, calculationItem);
    }

    /// <summary>
    /// 输出每次条件下，每个fraction的protein group文件
    /// </summary>
    protected override void ExportIndividualFractionFile()
    {
      DirectoryInfo individualDir = new DirectoryInfo(resultDir.FullName + "\\individual");
      FileInfo sourceFile = new FileInfo(option.SourceFileName);

      SequestResultTextFormat writeFormat = GetWriteFormat();

      for (int iMinCount = option.FilterFrom; iMinCount <= option.FilterTo; iMinCount += option.FilterStep)
      {
        List<CalculationItem> currentItems = GetFilteredItems(iMinCount);

        if (!individualDir.Exists)
        {
          individualDir.Create();
        }

        foreach (string keptClassifiedName in option.GetClassifiedNames())
        {
          string result_file = MyConvert.Format(@"{0}\{1}.{2}.{3}{4}",
            individualDir.FullName,
            FileUtils.ChangeExtension(sourceFile.Name, ""),
            GetOptionCondition(iMinCount),
            keptClassifiedName,
            sourceFile.Extension);

          List<IIdentifiedProteinGroup> groups = new List<IIdentifiedProteinGroup>();
          foreach (var item in currentItems)
          {
            if (item.GetClassifiedCount(keptClassifiedName) >= iMinCount)
            {
              IIdentifiedProteinGroup group = (IIdentifiedProteinGroup)item.Key;

              IIdentifiedProteinGroup clonedGroup = GetGroupContainClassifiedPeptideHitOnly(keptClassifiedName, group);

              groups.Add(clonedGroup);
            }
          }

          IdentifiedResult curResult = new IdentifiedResult();
          curResult.AddRange(groups);
          curResult.Sort();

          writeFormat.WriteToFile(result_file, curResult);
        }
      }
    }

    private IIdentifiedProteinGroup GetGroupContainClassifiedPeptideHitOnly(string keptClassifiedName, IIdentifiedProteinGroup group)
    {
      IIdentifiedProteinGroup result = (IIdentifiedProteinGroup)group.Clone();

      foreach (IIdentifiedProtein protein in result)
      {
        protein.Peptides.RemoveAll(m => !sphc.GetClassification(m).Equals(keptClassifiedName));
        protein.InitUniquePeptideCount();
        protein.CalculateCoverage();
      }

      return result;
    }

    private SequestResultTextFormat GetWriteFormat()
    {
      List<string> proteins = format.ProteinFormat.GetHeader().Split(new char[] { '\t' }).ToList();
      proteins.Remove("IdentifiedName");

      List<string> peptides = format.PeptideFormat.GetHeader().Split(new char[] { '\t' }).ToList();
      peptides.Remove("GroupCount");
      peptides.Remove("ProteinCount");

      return new SequestResultTextFormat(StringUtils.Merge(proteins, "\t"), StringUtils.Merge(peptides, "\t"));
    }
  }
}