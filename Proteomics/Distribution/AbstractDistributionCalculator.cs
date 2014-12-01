using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Classification;
using RCPA.Utils;

namespace RCPA.Proteomics.Distribution
{
  public abstract class AbstractDistributionCalculator : AbstractThreadFileProcessor
  {
    protected int maxPeptideCountWidth;

    protected DistributionOption option;

    protected DirectoryInfo resultDir;

    protected IClassification<IIdentifiedPeptide> sphc;

    protected List<CalculationItem> calculationItems = new List<CalculationItem>();

    protected Dictionary<string, List<int>> uniquePeptideCounts = new Dictionary<string, List<int>>();

    protected Dictionary<string, List<int>> peptideCounts = new Dictionary<string, List<int>>();

    protected string typeTitle;

    private bool exportIndividual;

    public bool ExportPeptideCountOnly { get; set; }

    protected AbstractDistributionCalculator(string typeTitle, bool exportIndividual)
    {
      this.typeTitle = typeTitle;
      this.exportIndividual = exportIndividual;
      this.ExportPeptideCountOnly = true;
    }

    public override IEnumerable<string> Process(String optionFileName)
    {
      InitializeFromOption(optionFileName);

      ParseToCalculationItems();

      CalculatePeptideCount();

      DoStatisticBaseOnPeptideCount();

      DoDuplicationStatistic();

      if (exportIndividual)
      {
        ExportIndividualFractionFile();
      }

      return new List<string>();
    }

    protected virtual void InitializeFromOption(string optionFileName)
    {
      option = new DistributionOptionXmlFormat().ReadFromFile(optionFileName);

      option.FilterFrom = Math.Max(0, option.FilterFrom);

      if (option.DistributionType == DistributionType.Peptide)
      {
        option.FilterType = PeptideFilterType.PeptideCount;
      }

      FileInfo optionFile = new FileInfo(optionFileName);

      resultDir = optionFile.Directory;

      sphc = option.GetClassification();

      maxPeptideCountWidth = option.GetMaxPeptideCountWidth();
    }

    class RangeValue
    {
      public int uniqueCount;

      public int totalCount;

      public RangeValue()
      {
        uniqueCount = 0;
        totalCount = 0;
      }
    }

    protected void printClassifiedNames(StreamWriter pw, bool showRank)
    {
      foreach (var s in option.GetClassifiedNames())
      {
        pw.Write("\t" + s);
        if (showRank)
        {
          pw.Write("\t" + s + "_rank");
        }
      }
    }

    protected void printClassifiedNames(StreamWriter pw, string appendix)
    {
      foreach (var s in option.GetClassifiedNames())
      {
        pw.Write("\t" + s + appendix);
        pw.Write("\t" + s + appendix + "_rank");
      }
    }

    protected string GetRank(List<int> intList, int count)
    {
      if (count == 0)
      {
        return "-";
      }

      return (intList.FindIndex(m => m == count) + 1).ToString();
    }

    /**
     * 根据calculationItems中保存信息，计算在各个classifiedName中每个Object对应的肽段数量。
     */
    private void CalculatePeptideCount()
    {
      foreach (CalculationItem item in calculationItems)
      {
        item.ClassifyPeptideHit((m => sphc.GetClassification(m)), option.GetClassifiedNames());
      }

      CalculatePeptideCountDistribution();
    }

    private void CalculatePeptideCountDistribution()
    {
      uniquePeptideCounts.Clear();
      peptideCounts.Clear();

      foreach (string classifiedName in option.GetClassifiedNames())
      {
        peptideCounts[classifiedName] = new List<int>();
        uniquePeptideCounts[classifiedName] = new List<int>();
      }

      foreach (CalculationItem item in calculationItems)
      {
        foreach (string classifiedName in option.GetClassifiedNames())
        {
          Count pepcount = item.Classifications[classifiedName];

          uniquePeptideCounts[classifiedName].Add(pepcount.UniquePeptideCount);
          peptideCounts[classifiedName].Add(pepcount.PeptideCount);
        }
      }

      uniquePeptideCounts.Values.ToList().ForEach(m => m.Sort((x1, x2) => x2.CompareTo(x1)));
      peptideCounts.Values.ToList().ForEach(m => m.Sort((x1, x2) => x2.CompareTo(x1)));
    }

    private void PrintDuplicationCount(StreamWriter pw, List<CalculationItem> currentItems, int iMinPeptideCount)
    {
      iMinPeptideCount = Math.Max(iMinPeptideCount, 1);

      int[] iDuplicatedCount = new int[option.GetClassifiedNames().Count() + 1];
      foreach (var item in currentItems)
      {
        int iCount = 0;
        foreach (var c in option.GetClassifiedNames())
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          if (item.GetClassifiedCount(c) >= iMinPeptideCount)
          {
            iCount++;
          }
        }
        iDuplicatedCount[iCount]++;
      }

      pw.WriteLine();
      pw.WriteLine("OverlapCount\tMatchedCount\tPercent");
      for (int i = iDuplicatedCount.Length - 1; i > 0; i--)
      {
        pw.WriteLine("{0}\t{1}\t{2:0.##}%\t", i, iDuplicatedCount[i], (double)iDuplicatedCount[i] * 100 / (double)currentItems.Count());
      }
    }

    private void PrintDuplicationOverlap(StreamWriter pw, List<CalculationItem> currentItems, int iMinPeptideCount)
    {
      iMinPeptideCount = Math.Max(iMinPeptideCount, 1);

      foreach (string c1 in option.GetClassifiedNames())
      {
        pw.Write(c1);

        foreach (string c2 in option.GetClassifiedNames())
        {
          int iCount = 0;
          foreach (var item in currentItems)
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            int count_i = item.GetClassifiedCount(c1);
            int count_j = item.GetClassifiedCount(c2);
            if (count_i >= iMinPeptideCount && count_j >= iMinPeptideCount)
            {
              iCount++;
            }
          }
          pw.Write("\t" + iCount);
        }
        pw.WriteLine();
      }
    }

    protected string GetOptionCondition(int iMinCount)
    {
      string modifiedStr = option.ModifiedPeptideOnly ? "Modified_" + option.ModifiedPeptide + "." : "";

      return MyConvert.Format(@"{0}{1}_{2}.{3}",
        modifiedStr,
        option.DistributionType,
        option.ClassificationPrinciple,
        iMinCount.ToString().PadLeft(maxPeptideCountWidth, '0'));
    }

    protected string GetResultFilePrefix(int iMinCount)
    {
      FileInfo sourceFile = new FileInfo(option.SourceFileName);

      string modifiedStr = option.ModifiedPeptideOnly ? "Modified_" + option.ModifiedPeptide + "." : "";

      return MyConvert.Format(@"{0}\{1}.{2}",
        resultDir.FullName,
        sourceFile.Name,
        GetOptionCondition(iMinCount));
    }

    private void DoDuplicationStatistic()
    {
      FileInfo sourceFile = new FileInfo(option.SourceFileName);

      for (int iMinCount = option.FilterFrom; iMinCount <= option.FilterTo; iMinCount += option.FilterStep)
      {
        string result_file = GetResultFilePrefix(iMinCount) + ".duplication.comparation";

        List<CalculationItem> currentItems = GetFilteredItems(iMinCount);

        using (StreamWriter pw = new StreamWriter(result_file))
        {
          printClassifiedNames(pw, false);

          pw.WriteLine();

          PrintDuplicationOverlap(pw, currentItems, iMinCount);

          PrintDuplicationCount(pw, currentItems, iMinCount);
        }

        printDuplicationCorrespondingObjects(result_file, currentItems, iMinCount);
      }
    }

    private void printDuplicationCorrespondingObjects(string result_file, List<CalculationItem> curItems, int iMinPeptideCount)
    {
      //List<List<CalculationItem>> duplicatedObjs = new List<List<CalculationItem>>();

      //for (int i = 0; i < option.GetClassifiedNames().Length + 1; i++) {
      //  duplicatedObjs.Add(new List<CalculationItem>());
      //}

      //for (int k = 0; k < currentItems.size(); k++) {
      //  int iCount = 0;
      //  for (int j = 0; j < classifiedNames.length; j++) {
      //    int pepCount = currentItems.get(k).getClassifiedCount(
      //        classifiedNames[j]);
      //    if (pepCount >= iMinPeptideCount) {
      //      iCount++;
      //    }
      //  }
      //  duplicatedObjs.get(iCount).add(currentItems.get(k));
      //}

      //for (int i = 1; i < duplicatedObjs.size(); i++) {
      //  final File resultFile = new File(result_file.getAbsolutePath() + ".COVER"
      //      + i + ".fasta");
      //  writeItemObjectsFastaFormat(resultFile, duplicatedObjs.get(i));
      //}
    }

    private void DoStatisticBaseOnPeptideCount()
    {
      FileInfo sourceFile = new FileInfo(option.SourceFileName);

      for (int iMinCount = option.FilterFrom; iMinCount <= option.FilterTo; iMinCount += option.FilterStep)
      {
        string result_file = GetResultFilePrefix(iMinCount) + ".distribution";

        using (StreamWriter sw = new StreamWriter(result_file))
        {
          PrintHeader(sw);
          sw.WriteLine();

          List<CalculationItem> currentItems;

          currentItems = GetFilteredItems(iMinCount);

          foreach (var item in currentItems)
          {
            PrintItem(sw, item);
            sw.WriteLine();
          }
        }
      }
    }

    protected List<CalculationItem> GetFilteredItems(int iMinCount)
    {
      if (option.FilterType == PeptideFilterType.PeptideCount)
      {
        return
          (from c in calculationItems
           where c.Classifications.Values.Any(m => m.PeptideCount >= iMinCount)
           select c).ToList();
      }
      else
      {
        return
          (from c in calculationItems
           where c.Classifications.Values.Any(m => m.UniquePeptideCount >= iMinCount)
           select c).ToList();
      }
    }

    /// <summary>
    /// 根据option中指定的sourceFile，读取并且解析为CalculationItemList。
    /// </summary>
    protected abstract void ParseToCalculationItems();

    /// <summary>
    /// 打印distribution文件的标题行
    /// </summary>
    /// <param name="sw"></param>
    protected abstract void PrintHeader(StreamWriter sw);

    /// <summary>
    /// 打印item相关的信息，主要是标识、理论值、实验值、以及分布count数，应该与PrintHeader中定义的标题行一致。
    /// </summary>
    /// <param name="sw"></param>
    /// <param name="item"></param>
    protected abstract void PrintItem(StreamWriter sw, CalculationItem item);

    /// <summary>
    /// 输出根据classifiedNames拆分的结果为独立文件
    /// </summary>
    protected abstract void ExportIndividualFractionFile();
  }
}
