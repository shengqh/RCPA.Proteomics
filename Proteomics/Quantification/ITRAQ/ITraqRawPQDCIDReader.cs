using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.ITraq
{
  /// <summary>
  /// 从Raw文件中读取iTraq对应的minMz-maxMz区间的Peak，要求这区间至少包含minPeakCount离子
  /// </summary>
  public class ITraqRawPQDCIDReader : AbstractITraqRawReader
  {
    public ITraqRawPQDCIDReader() : base(2, "PQD-CID") { }

    #region IFileReader<List<PeakList<Peak>>> Members

    public override List<IsobaricItem> ReadFromFile(string fileName)
    {
      var result = new List<IsobaricItem>();

      RawReader.Open(fileName);
      try
      {
        int startScan = RawReader.GetFirstSpectrumNumber();
        int endScan = RawReader.GetLastSpectrumNumber();

        Progress.SetRange(startScan, endScan);

        for (int scan = startScan; scan <= endScan; scan++)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          Progress.SetPosition(scan);

          if (2 == RawReader.GetMsLevel(scan))
          {
            string scanMode = RawReader.GetScanMode(scan);
            if (string.IsNullOrEmpty(scanMode))
            {
              AppendScan(result, scan, "UNKNOWN");
              continue;
            }

            scanMode = scanMode.ToLower();
            if (scanMode.Equals("pqd"))
            {
              AppendScan(result, scan, "PQD");
            }
            else if (scanMode.Equals("cid"))
            {
              //如果上一个scan是pqd，那么，现在这个cid的结果从该pqd读取。
              if (result.Count > 0 && result[result.Count - 1].RawPeaks.ScanTimes[0].Scan == scan - 1 && result[result.Count - 1].RawPeaks.ScanMode == "PQD")
              {
                var lastItem = result[result.Count - 1];

                var item = new IsobaricItem(lastItem);
                item.Scan = RawReader.GetScanTime(scan);
                item.ScanMode = "CID";

                result.Add(item);
              }
              else//否则，从自己的peaklist中读取。
              {
                AppendScan(result, scan, "CID");
              }
            }
            else
            {
              Console.WriteLine("Scan {0} is skipped with mode {1}", scan, scanMode);
            }
          }
        }
      }
      finally
      {
        RawReader.Close();
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
