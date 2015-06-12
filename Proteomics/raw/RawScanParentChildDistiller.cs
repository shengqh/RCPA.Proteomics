using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Raw
{
  /// <summary>
  /// 产生MSLevel的对应表
  /// </summary>
  public class RawScanParentChildDistiller : AbstractThreadProcessor
  {
    private readonly RawScanParentChildDistillerOptions _options;

    public RawScanParentChildDistiller(RawScanParentChildDistillerOptions options)
    {
      _options = options;
    }

    public override IEnumerable<string> Process()
    {
      using (var rawFile = RawFileFactory.GetRawFileReader(_options.InputFile))
      {
        var levels = GetScanLevels(rawFile);
        new ScanLevelFormat().WriteToFile(_options.OutputFile, levels);

      }

      return new[] { _options.OutputFile };
    }

    public static List<ScanLevel> GetScanLevels(IRawFile rawFile)
    {
      var levels = new List<ScanLevel>();
      FillScanLevels(levels, rawFile);
      BuildScanLevels(levels);
      return levels;
    }

    public static void FillScanLevels(List<ScanLevel> levels, IRawFile rawFile)
    {
      var firstScan = rawFile.GetFirstSpectrumNumber();
      var lastScan = rawFile.GetLastSpectrumNumber();
      for (var scan = firstScan; scan <= lastScan; scan++)
      {
        var mslevel = rawFile.GetMsLevel(scan);
        levels.Add(new ScanLevel()
        {
          Scan = scan,
          Level = mslevel
        });
      }
    }

    public static void BuildScanLevels(List<ScanLevel> levels)
    {
      var level1 = new List<ScanLevel>();

      var ms2count = levels.Count(m => m.Level == 2);
      var ms3count = levels.Count(m => m.Level == 3);
      var one_ms2_multiple_ms3 = ms3count > ms2count;
      
      var level3Count = -1;
      ScanLevel last = null;
      foreach (var level in levels)
      {
        switch (level.Level)
        {
          case 1:
            level1.Add(level);
            level3Count = -1;
            break;
          case 2:
            level.Parent = level1.Last();
            break;
          case 3:
            level3Count++;
            if (one_ms2_multiple_ms3)
            {
              level.Parent = level1.Last().Children.Last();
            }
            else
            {
              level.Parent = level1.Last().Children[level3Count];
            }
            break;
          default:
            if (last.Level == level.Level)
            {
              level.Parent = last.Parent;
            }
            else
            {
              level.Parent = last;
            }
            break;
        }
        last = level;
      }
    }
  }
}
