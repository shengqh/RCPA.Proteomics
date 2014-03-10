using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Quantification;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Quantification.Census;

namespace RCPA.Tools.Quantification
{
  public class CensusResultUniquePeptideCountFilter : AbstractThreadFileProcessor
  {
    public int uniqueCount;

    public CensusResultUniquePeptideCountFilter(int uniqueCount)
    {
      this.uniqueCount = uniqueCount;
    }

    public override IEnumerable<string> Process(string filename)
    {
      CensusResultFormat crFormat = new CensusResultFormat(false);

      CensusResult cr = crFormat.ReadFromFile(filename);

      cr.Recalculate();

      CensusResult result = new CensusResult();
      result.Headers.AddRange(cr.Headers);

      foreach (CensusProteinItem cpi in cr.Proteins)
      {
        if (cpi.PeptideNumber >= uniqueCount)
        {
          result.Proteins.Add(cpi);
        }
      }

      string resultFilename = FileUtils.ChangeExtension(filename, "." + uniqueCount + new FileInfo(filename).Extension);

      crFormat.WriteToFile(resultFilename, result);

      return new[] { resultFilename };
    }
  }
}
