using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Raw
{
  public class IsolationScanFinder
  {
    private Dictionary<int, ScanLevel> levels;

    public void BuildMap(IScanLevelBuilder builder, IRawFile file)
    {
      levels = builder.GetScanLevels(file).ToDictionary(m => m.Scan);
    }

    public int GetIsolationScan(int scan)
    {
      if (levels.ContainsKey(scan))
      {
        var parent = levels[scan].Parent;
        if (null != parent)
        {
          return parent.Scan;
        }
      }

      return scan;
    }
  }
}
