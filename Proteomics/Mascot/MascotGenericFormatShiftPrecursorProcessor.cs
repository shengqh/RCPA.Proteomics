using System.Collections.Generic;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  public class MascotGenericFormatShiftPrecursorProcessor : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string filename)
    {
      Progress.SetMessage("Reading peak list from " + filename + "...");
      List<PeakList<Peak>> pklList = new MascotGenericFormatReader<Peak>().ReadFromFile(filename);

      foreach (var pkl in pklList)
      {
        pkl.PrecursorMZ = pkl.PrecursorMZ + 10.0;
      }

      var writer = new MascotGenericFormatWriter<Peak>();

      string resultFilename = FileUtils.ChangeExtension(filename, ".shift10.mgf");
      Progress.SetMessage("Writing peak list to " + resultFilename + "...");
      writer.WriteToFile(resultFilename, pklList);

      Progress.End();

      return new List<string>(new[] {resultFilename});
    }
  }
}