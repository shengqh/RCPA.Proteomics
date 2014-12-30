using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.MaxQuant;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuant2MascotPeptideProcessor2 : AbstractThreadFileProcessor
  {
    private double minProbability;
      private double minDeltaScore;

      public MaxQuant2MascotPeptideProcessor2(double minProbability, double minDeltaScore)
    {
      this.minDeltaScore = minDeltaScore;
      this.minProbability = minProbability;
    }

    public class PeptideComparer : IEqualityComparer<IIdentifiedSpectrum>
    {
      bool IEqualityComparer<IIdentifiedSpectrum>.Equals(IIdentifiedSpectrum x, IIdentifiedSpectrum y)
      {
        // Check whether the compared objects reference the same data.
        if (Object.ReferenceEquals(x, y))
          return true;

        // Check whether any of the compared objects is null.
        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
          return false;

        return x.Query.FileScan.ShortFileName == y.Query.FileScan.ShortFileName;
      }

      int IEqualityComparer<IIdentifiedSpectrum>.GetHashCode(IIdentifiedSpectrum obj)
      {
        return obj.Query.FileScan.ShortFileName.GetHashCode();
      }
    }

    public override IEnumerable<string> Process(string filename)
    {
      var mr = new MaxQuantPeptideTextReader().ReadFromFile(filename);
      mr.RemoveAll(m => m.DeltaScore < minDeltaScore || m.PValue < minProbability);

      mr = mr.Distinct(new PeptideComparer()).ToList();

      string resultFilename = filename + ".peptides";
      new MascotPeptideTextFormat("\t\"File, Scan(s)\"\tSequence\tCharge\tScore\tDeltaScore\tExpectValue\tPValue\tModification").WriteToFile(resultFilename, mr);

      return new [] { resultFilename };
    }
  }
}
