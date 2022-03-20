using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Raw
{
  public class ScanLevelFormat : IFileFormat<List<ScanLevel>>
  {
    public List<ScanLevel> ReadFromFile(string fileName)
    {
      Dictionary<int, ScanLevel> map = new Dictionary<int, ScanLevel>();
      foreach (var line in File.ReadAllLines(fileName).Skip(1))
      {
        var parts = line.Split('\t');
        var scanLevel = new ScanLevel()
        {
          Scan = int.Parse(parts[0]),
          Level = int.Parse(parts[1])
        };
        map[scanLevel.Scan] = scanLevel;
        var parent = int.Parse(parts[2]);
        if (parent != 0)
        {
          scanLevel.Parent = map[parent];
        }
      }

      return map.Values.OrderBy(m => m.Scan).ToList();
    }

    public void WriteToFile(string fileName, List<ScanLevel> levels)
    {
      using (var sw = new StreamWriter(fileName))
      {
        sw.WriteLine("Scan\tLevel\tParent");
        foreach (var level in levels)
        {
          sw.WriteLine("{0}\t{1}\t{2}",
            level.Scan, level.Level, level.Parent == null ? 0 : level.Parent.Scan);
        }
      }
    }
  }
}
