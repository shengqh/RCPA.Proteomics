using System.Collections.Generic;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.IO
{
  public class DtaIO
  {
    public static PeakList<T> ReadFromDta<T>(string dtaFilename) where T:IPeak, new()
    {
      return new DtaFormat<T>().ReadFromFile(dtaFilename);
    }

    public static List<PeakList<T>> GetPeakListFromDtaDirectory<T>(string dtaDirectory) where T : IPeak, new()
    {
      return GetPeakListFromDtaDirectory<T>(dtaDirectory, new EmptyProgressCallback());
    }

    public static List<PeakList<T>> GetPeakListFromDtaDirectory<T>(string dtaDirectory, IProgressCallback progress) where T : IPeak, new()
    {
      var result = new List<PeakList<T>>();

      var ddi = new DtaDirectoryIterator<T>(dtaDirectory);

      progress.SetMessage("Reading peak list from dta files of directory " + new DirectoryInfo(dtaDirectory).Name +
                          ", total " + ddi.Count + " files ...");
      progress.SetRange(0, ddi.Count);

      progress.SetPosition(0);

      while (ddi.HasNext())
      {
        progress.Increment(1);
      
        if (progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        result.Add(ddi.Next());
      }

      return result;
    }
  }
}