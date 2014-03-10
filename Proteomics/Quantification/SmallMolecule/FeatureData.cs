using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Quantification.SmallMolecule
{
  public class FeatureData
  {
    private Dictionary<string, FileData> map = new Dictionary<string, FileData>();

    public Dictionary<string, FileData> Intensities { get { return map; } }

    public void Clear()
    {
      map.Clear();
    }

    public void AddFiles(IEnumerable<string> fileNames)
    {
      foreach (var f in fileNames)
      {
        AddFile(f);
      }
    }

    public void AddFile(string fileName)
    {
      map[new FileInfo(fileName).Name] = FileData.ReadFromFile(fileName);
    }
  }
}
