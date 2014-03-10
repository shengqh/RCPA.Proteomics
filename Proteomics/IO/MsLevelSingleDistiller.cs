using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.IO
{
  public class MsLevelSingleDistiller : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string filename)
    {
      string msLevelFilename = filename + ".msLevel";
      using (IRawFile rawFile = new RawFileImpl(filename))
      {
        using (var sw = new StreamWriter(msLevelFilename))
        {
          int firstScan = rawFile.GetFirstSpectrumNumber();
          int lastScan = rawFile.GetLastSpectrumNumber();
          Progress.SetRange(rawFile.GetFirstSpectrumNumber(), rawFile.GetLastSpectrumNumber());
          Progress.SetMessage("Processing " + filename + "...");

          sw.WriteLine("Scan\tMsLevel");
          for (int scan = firstScan; scan <= lastScan; scan++)
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            sw.WriteLine(scan + "\t" + rawFile.GetMsLevel(scan));
            Progress.SetPosition(scan);
          }

          Progress.SetPosition(lastScan);
          Progress.SetMessage("Processing " + filename + " finished.");
        }
      }

      return new[] { msLevelFilename };
    }
  }
}