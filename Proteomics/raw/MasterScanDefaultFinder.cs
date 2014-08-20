using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Raw
{
  /// <summary>
  /// Default master scan is the previous scan with higher level
  /// </summary>
  public class MasterScanDefaultFinder : IMasterScanFinder
  {
    public int Find(IRawFile reader, int scan)
    {
      int level = reader.GetMsLevel(scan);
      var masterLevel = level - 1;
      scan--;
      int firstScan = reader.GetFirstSpectrumNumber();
      while (scan >= firstScan)
      {
        var curlevel = reader.GetMsLevel(scan);
        if (curlevel == masterLevel)
        {
          return scan;
        }
        scan--;
      }

      return 0;
    }
  }
}
