using RCPA.Proteomics.Mascot;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Statistic
{
  /// <summary>
  /// 用于对相同数据，针对两种不同策略进行数据预处理后搜索得到结果的比较
  /// </summary>
  public class ScoreComparisonBuilder : AbstractThreadFileProcessor
  {
    private string peptideFile1;

    private string peptideFile2;

    public ScoreComparisonBuilder(string peptideFile1, string peptideFile2)
    {
      this.peptideFile1 = peptideFile1;
      this.peptideFile2 = peptideFile2;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var pep1 = new MascotPeptideTextFormat().ReadFromFile(peptideFile1).ToDictionary(m => m.Query.FileScan.LongFileName);
      var pep2 = new MascotPeptideTextFormat().ReadFromFile(peptideFile2).ToDictionary(m => m.Query.FileScan.LongFileName);

      var commonSpectra = pep1.Keys.Intersect(pep2.Keys).ToList();
      commonSpectra.Sort();

      using (StreamWriter sw = new StreamWriter(fileName))
      {
        sw.WriteLine("FileScan\t" + Path.GetFileNameWithoutExtension(peptideFile1) + "\t" + Path.GetFileNameWithoutExtension(peptideFile2) + "\tDeltaScore");
        foreach (var spectrum in commonSpectra)
        {
          sw.WriteLine("{0}\t{1:0.00}\t{2:0.00}\t{3:0.00}", spectrum, pep1[spectrum].Score, pep2[spectrum].Score, pep2[spectrum].Score - pep1[spectrum].Score);
        }
      }

      return new string[] { fileName };
    }
  }
}
