using System.Collections.Generic;

namespace RCPA.Proteomics.Raw
{
  /// <summary>
  /// For MS2 scan, master scan is the previous MS1 scan.
  /// For MS3 scan, master scan is the corresponding sequence index MS2 scan.
  /// For example, 10000 (MS1) -> 10001 (MS2) -> 10002 (MS2) -> 10003 ->(MS3) -> 10004 (MS3) -> 10005 (MS1),
  /// the master scan of 10003 is 10001 and master scan of 10004 is 10002.
  /// </summary>
  public class ScanLevelParallelMS3Builder : AbstractScanLevelBuilder
  {
    public override List<ScanLevel> GetScanLevels(IRawFile rawFile)
    {
      var result = GetRawScanLevels(rawFile);

      var level3Count = -1;
      ScanLevel lastLevel1 = null;
      for (int i = 0; i < result.Count; i++)
      {
        var level = result[i];

        switch (level.Level)
        {
          case 1:
            lastLevel1 = level;
            level3Count = -1;
            break;
          case 2:
            level.Parent = lastLevel1;
            break;
          case 3:
            level3Count++;
            level.Parent = lastLevel1.Children[level3Count];
            break;
          default:
            var masterlevel = level.Level - 1;
            for (int j = i - 1; j >= 0; j--)
            {
              if (result[j].Level == masterlevel)
              {
                level.Parent = result[j];
                break;
              }
            }
            break;
        }
      }

      return result;
    }
  }
}
