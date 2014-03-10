using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Quantification.Census;

namespace RCPA.Tools.Quantification
{
  public class CensusResultProteinMerger2 : AbstractThreadFileProcessor
  {
    private string[] sourceFilenames;

    public CensusResultProteinMerger2(string[] sourceFilenames)
    {
      this.sourceFilenames = sourceFilenames;
    }

    public override IEnumerable<string> Process(string targetFilename)
    {
      CensusStringResult target = new CensusStringResult();

      CensusStringResultFormat crf = new CensusStringResultFormat();
      Progress.SetRange(1, sourceFilenames.Length + 1);
      for (int i = 0; i < sourceFilenames.Length; i++)
      {
        string f = sourceFilenames[i];

        Progress.SetMessage("Reading from " + f + "...");
        Progress.SetPosition(i + 1);

        CensusStringResult cr = crf.ReadFromFile(f);

        target.Proteins.AddRange(cr.Proteins);
      }

      target.Proteins.Sort((p1, p2) => p2.Peptides.Count - p1.Peptides.Count);
      target.Headers = CensusUtils.ReadHeaders(sourceFilenames[0]);

      Progress.SetMessage("Writing to " + targetFilename + "...");
      crf.WriteToFile(targetFilename, target);
      Progress.SetMessage("Write to " + targetFilename + " finished.");
      Progress.SetPosition(sourceFilenames.Length + 1);

      return new[] { targetFilename };
    }
  }
}
