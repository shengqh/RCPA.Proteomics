using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class OptimalFileTextWriter : IFileWriter<DatasetList>
  {
    public void WriteToStream(StreamWriter sw, DatasetList dsList)
    {
      foreach (var ds in dsList)
      {
        foreach (var sor in ds.SavedOptimalResults)
        {
          sw.WriteLine(sor.Item1);
          WriteOptimalItems(sw, sor.Item2);
        }

        //if (ds.SavedOptimalResults.All(l => l.Item2 != ds.OptimalResults))
        //{
        //  WriteOptimalItems(sw, ds.OptimalResults);
        //}
      }
    }

    private static void WriteHeader(StreamWriter sw)
    {
      sw.WriteLine("Classification\tCharge\tNumMissCleavage\tNumProteaseTermini\tModification\tProteinTag\tScore\tDeltaScore\tTargetCount\tDecoyCount\tFDR");
    }

    private static void WriteOptimalItems(StreamWriter sw, List<OptimalItem> oil)
    {
      WriteHeader(sw);
      oil.Sort((m1, m2) => m1.Condition.CompareTo(m2.Condition));
      foreach (var n in oil)
      {
        var cond = n.Condition;
        var or = n.Result;
        sw.WriteLine("{0}\t{1}{2}\t{3}{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}",
          cond.Classification,
          cond.PrecursorCharge,
          cond.PrecursorCharge == 3 ? "+" : "",
          cond.MissCleavageSiteCount,
          cond.MissCleavageSiteCount == 3 ? "+" : "",
          cond.NumProteaseTermini,
          cond.ModificationCount,
          cond.ProteinTag,
          or.Score,
          or.DeltaScore,
          or.PeptideCountFromTargetDB,
          or.PeptideCountFromDecoyDB,
          or.FalseDiscoveryRate);
      }
      sw.WriteLine();
    }

    #region IFileWriter<DatasetList> Members

    public void WriteToFile(string fileName, DatasetList dsList)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        WriteToStream(sw, dsList);
      }
    }

    #endregion
  }
}
