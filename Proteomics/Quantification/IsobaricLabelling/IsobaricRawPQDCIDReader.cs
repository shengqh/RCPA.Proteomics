using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricRawPQDCIDReader : AbstractIsobaricRawReader
  {
    public IsobaricRawPQDCIDReader() : base(2, "PQD-CID") { }

    #region IFileReader<List<PeakList<Peak>>> Members

    public override List<IsobaricScan> ReadFromFile(string fileName)
    {
      var result = new List<IsobaricScan>();

      using (var rawReader = RawFileFactory.GetRawFileReader(fileName))
      {
        int startScan = rawReader.GetFirstSpectrumNumber();
        int endScan = rawReader.GetLastSpectrumNumber();

        Progress.SetRange(startScan, endScan);

        for (int scan = startScan; scan <= endScan; scan++)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          Progress.SetPosition(scan);

          if (2 == rawReader.GetMsLevel(scan))
          {
            string scanMode = rawReader.GetScanMode(scan);
            if (string.IsNullOrEmpty(scanMode))
            {
              AppendScan(rawReader, result, scan, "UNKNOWN");
              continue;
            }

            scanMode = scanMode.ToLower();
            if (scanMode.Equals("pqd"))
            {
              AppendScan(rawReader, result, scan, "PQD");
            }
            else if (scanMode.Equals("cid"))
            {
              //如果上一个scan是pqd，那么，现在这个cid的结果从该pqd读取。
              if (result.Count > 0 && result[result.Count - 1].RawPeaks.ScanTimes[0].Scan == scan - 1 && result[result.Count - 1].RawPeaks.ScanMode == "PQD")
              {
                var lastItem = result[result.Count - 1];

                var item = new IsobaricScan(lastItem);
                item.Scan = rawReader.GetScanTime(scan);
                item.ScanMode = "CID";

                result.Add(item);
              }
              else//否则，从自己的peaklist中读取。
              {
                AppendScan(rawReader, result, scan, "CID");
              }
            }
            else
            {
              Console.WriteLine("Scan {0} is skipped with mode {1}", scan, scanMode);
            }
          }
        }
      }

      return result;
    }

    #endregion

    protected override string[] GetScanMode()
    {
      return new string[] { "PQD", "CID" };
    }

    public override string ToString()
    {
      return this.Name + " : MS1->PQD->CID->PQD->CID";
    }
  }
}
