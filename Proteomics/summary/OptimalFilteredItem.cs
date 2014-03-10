using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public class OptimalFilteredItem
  {
    public ProteinFdrFilteredItem Unique2CountResult { get; set; }

    public ProteinFdrFilteredItem Unique2Result { get; set; }

    public ProteinFdrFilteredItem Unique1Result { get; set; }

    public int TotalPeptideCount
    {
      get
      {
        return Unique2CountResult.AcceptedSpectra.Count + Unique2Result.AcceptedSpectra.Count +
               Unique1Result.AcceptedSpectra.Count;
      }
    }

    public int TotalProteinCount
    {
      get { return Unique2CountResult.ProteinCount + Unique2Result.ProteinCount + Unique1Result.ProteinCount; }
    }

    public List<IIdentifiedSpectrum> GetSpectra()
    {
      var result = new List<IIdentifiedSpectrum>();
      result.AddRange(Unique2CountResult.AcceptedSpectra);
      result.AddRange(Unique2Result.AcceptedSpectra);
      result.AddRange(Unique1Result.AcceptedSpectra);
      return result;
    }

    public static string GetHeader()
    {
      return "PeptideFdr\tPassPeptideCount\tCond1\tProteinFdr\tProteinCount\tPepCount\t\tPeptideFdr\tPassPeptideCount\tCond2\tProteinFdr\tProteinCount\tPepCount\t\tUniquePeptideFdr\tPassPeptideCount\tProteinFdr\tProteinCount\tPepCount\t\tTotalPeptideCount\tTotalProteinCount";
    }

    public override string ToString()
    {
      return MyConvert.Format("{0}\t\t{1}\t\t{2}\t\t{3}\t{4}",
            this.Unique2CountResult,
            this.Unique2Result,
            this.Unique1Result,
            this.TotalPeptideCount,
            this.TotalProteinCount);
    }
  }
}
