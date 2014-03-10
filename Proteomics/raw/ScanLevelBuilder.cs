using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Raw
{
  /// <summary>
  /// For each scan with level (L), the master scan is the previous scan with level (L-1)
  /// </summary>
  public class ScanLevelBuilder : AbstractScanLevelBuilder
  {
    public override List<ScanLevel> GetScanLevels(IRawFile rawFile)
    {
      var result = GetRawScanLevels(rawFile);

      for (int i = result.Count - 1; i > 0; i--)
      {
        if (result[i].Level == 1)
        {
          continue;
        }

        var masterLevel = result[i].Level - 1;
        for (int j = i - 1; j >= 0; j--)
        {
          if (result[j].Level == masterLevel)
          {
            result[i].Parent = result[j];
            break;
          }
        }
      }

      return result;
    }
  }
}
