using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;
using System.IO;

namespace RCPA.Tools.Misc
{
  public class IdentifiedPeptideSubtractor : AbstractThreadFileProcessor
  {
    private string sourceFileName;

    public IdentifiedPeptideSubtractor(string sourceFileName)
    {
      this.sourceFileName = sourceFileName;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      IFileFormat<List<IIdentifiedSpectrum>> format = new SequestPeptideTextFormat();

      Progress.SetMessage("Reading peptides from " + sourceFileName + " ...");
      List<IIdentifiedSpectrum> spectra = format.ReadFromFile(sourceFileName);

      HashSet<string> dtaFilenames = new HashSet<string>();
      spectra.ForEach(spectrum =>
      {
        dtaFilenames.Add(spectrum.Query.FileScan.LongFileName);
      });

      Progress.SetMessage("Reading peptides from " + fileName + " ...");
      List<IIdentifiedSpectrum> subtract = format.ReadFromFile(fileName);

      Progress.SetMessage("Subtracting peptides ...");
      subtract.RemoveAll(m =>
      {
        return dtaFilenames.Contains(m.Query.FileScan.LongFileName);
      });

      string resultFileName = sourceFileName + ".subtracted";

      format.WriteToFile(resultFileName, subtract);

      Progress.SetMessage("Finished.");

      return new[] { resultFileName };
    }
  }
}
