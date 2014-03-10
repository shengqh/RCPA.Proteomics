using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class OptimalFileTextWriter : IFileWriter<DatasetList>
  {
    public void WriteToStream(StreamWriter sw, DatasetList dsList)
    {
      sw.WriteLine("Classification\tCharge\tNumMissCleavage\tNumProteaseTermini\tModification\tScore\tDeltaScore\tTargetCount\tDecoyCount\tFDR");
      foreach (var ds in dsList)
      {
        ds.OptimalResults.Sort((m1, m2) => m1.Condition.CompareTo(m2.Condition));

        foreach (var n in ds.OptimalResults)
        {
          var cond = n.Condition;
          var or = n.Result;
          sw.WriteLine("{0}\t{1}{2}\t{3}{4}\t{5}\t{6}\t{7:0.00}\t{8:0.00}\t{9}\t{10}\t{11:0.0000}",
            cond.Classification,
            cond.PrecursorCharge,
            cond.PrecursorCharge == 3 ? "+" : "",
            cond.MissCleavageSiteCount,
            cond.MissCleavageSiteCount == 3 ? "+" : "",
            cond.NumProteaseTermini,
            cond.ModificationCount,
            or.Score,
            or.DeltaScore,
            or.PeptideCountFromTargetDB,
            or.PeptideCountFromDecoyDB,
            or.FalseDiscoveryRate);
        }
      }
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
