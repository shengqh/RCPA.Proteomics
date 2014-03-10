using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;

namespace RCPA.Proteomics.Format
{
  public class BuildSummaryWebFormatConverter : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string fileName)
    {
      var ir = new MascotResultTextFormat().ReadFromFile(fileName);

      var oldFormat = new SequestResultTextFormat("\tReference\tPepCount\tUniquePepCount\tCoverPercent\tMW\tPI", "\t\"File, Scan(s)\"\tSequence\tMH+\tDiff(MH+)\tCharge\tRank\tXC\tDeltaCn\tSp\tRSp\tIons\tReference\tDIFF_MODIFIED_CANDIDATE\tPI\tGroupCount\tProteinCount");

      var result = fileName + ".tmp";
      oldFormat.WriteToFile(result, ir);

      return new string[] { result };
    }
  }
}
