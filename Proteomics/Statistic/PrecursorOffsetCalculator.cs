using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using MathNet.Numerics.Statistics;
using System.IO;

namespace RCPA.Proteomics.Statistic
{
  public class PrecursorOffsetCalculator : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string fileName)
    {
      Progress.SetMessage("Reading peptides from " + fileName);

      var peptides = new MascotPeptideTextFormat().ReadFromFile(fileName);

      var expMap = peptides.GroupBy(m => m.Query.FileScan.Experimental).ToDictionary(m => m.Key);

      var expPPMMap = (from exp in expMap
                       let allppm = (from pep in exp.Value select PrecursorUtils.mz2ppm(pep.TheoreticalMass, pep.TheoreticalMinusExperimentalMass)).ToList()
                       let mean = Statistics.Mean(allppm)
                       let median = Statistics.Median(allppm)
                       orderby exp.Key descending
                       select new PrecursorOffsetEntry(exp.Key, mean, median)).ToList();

      var result = fileName + ".offset";

      PrecursorOffsetEntry.WriteToFile(result, expPPMMap);

      return new string[] { result };
    }
  }
}
