using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Mascot;
using System.IO;

namespace RCPA.Proteomics.Format
{
  public class MultipleMgfPrecursorCorrectionTaskProcessor : AbstractParallelTaskFileProcessor
  {
    private string targetDir;
    private List<Pair<string, double>> ppmList;

    public MultipleMgfPrecursorCorrectionTaskProcessor(string targetDir, List<Pair<string, double>> ppmList)
    {
      this.targetDir = targetDir;
      this.ppmList = ppmList;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      double offsetPPM;
      if (!FindOffset(fileName, out offsetPPM))
      {
        return new string[] { };
      }

      string resultFilename = new FileInfo(targetDir + "\\" + new FileInfo(fileName).Name).FullName;
      var writer = new MascotGenericFormatSqhWriter<Peak>();

      using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open)))
      {
        var iter = new MascotGenericFormatIterator<Peak>(sr);
        using (StreamWriter sw = new StreamWriter(resultFilename))
        {
          while (iter.HasNext())
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            if (IsLoopStopped)
            {
              return new string[] { };
            }

            var pkl = iter.Next();
            pkl.PrecursorMZ = pkl.PrecursorMZ + PrecursorUtils.ppm2mz(pkl.PrecursorMZ, offsetPPM);
            writer.Write(sw, pkl);
          }
        }
      }

      return new List<string>(new[] { resultFilename });
    }

    private bool FindOffset(string fileName, out double offsetPPM)
    {
      var filename = new FileInfo(fileName).Name;
      offsetPPM = 0.0;

      foreach (var exp in ppmList)
      {
        if (filename.Contains(exp.First))
        {
          if (filename[exp.First.Length] == '.')
          {
            offsetPPM = exp.Second;
            return true;
          }
        }
      }

      return false;
    }
  }
}
