using RCPA;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TICDistiller
{
  public class TICFileDistiller : AbstractThreadFileProcessor
  {
    private bool ignoreWhenResultExist;
    public TICFileDistiller(bool ignoreWhenResultExist)
    {
      this.ignoreWhenResultExist = ignoreWhenResultExist;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      string resultFilename = fileName + ".tic";
      if (ignoreWhenResultExist && File.Exists(resultFilename))
      {
        return new string[] { resultFilename };
      }

      using (IRawFile reader = new RawFileImpl(fileName))
      {
        Progress.SetMessage(MyConvert.Format("Processing {0} ...", new FileInfo(fileName).Name));

        using (StreamWriter sw = new StreamWriter(resultFilename))
        {
          sw.WriteLine("Scan\tMode\tPrecursor\tTIC");
          int firstScan = reader.GetFirstSpectrumNumber();
          int lastScan = reader.GetLastSpectrumNumber();

          Progress.SetRange(firstScan, lastScan);
          for (int i = firstScan; i <= lastScan; i++)
          {
            if (Progress.IsCancellationPending())
            {
              sw.Close();
              File.Delete(resultFilename);

              throw new UserTerminatedException();
            }

            if (1 == reader.GetMsLevel(i))
            {
              continue;
            }

            PeakList<Peak> pkl = reader.GetPeakList(i);
            Peak precursor = reader.GetPrecursorPeak(i);

            double tic = (from p in pkl
                          select p.Intensity).Sum();

            string mode = reader.GetScanMode(i);
            sw.WriteLine("{0}\t{1}\t{2:0.0000}\t{3:0.0}", i, mode.ToUpper(), precursor.Mz, tic);

            Progress.SetPosition(i);
          }
          Progress.End();
        }
        Progress.SetMessage(MyConvert.Format("Processing {0} finished.", new FileInfo(fileName).Name));
      }

      return new string[] { resultFilename };
    }
  }
}
