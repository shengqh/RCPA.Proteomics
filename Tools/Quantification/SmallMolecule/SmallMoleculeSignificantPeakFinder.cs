using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public class SmallMoleculeSignificantPeakFinder : AbstractThreadFileProcessor
  {
    private string sampleDataFile;

    private string refDataFile;

    public SmallMoleculeSignificantPeakFinder(string sampleDataFile, string refDataFile)
    {
      this.sampleDataFile = sampleDataFile;
      this.refDataFile = refDataFile;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      Dictionary<string, List<double>> sampleData = ReadData(sampleDataFile);
      Dictionary<string, List<double>> refData = ReadData(refDataFile);

      List<SmallMoleculePeakInfo> peakInfos = new List<SmallMoleculePeakInfo>();

      foreach (var key in sampleData.Keys)
      {
        peakInfos.Add(new SmallMoleculePeakInfo(sampleData[key].ToArray(), refData[key].ToArray(), key));
      }

      peakInfos.Sort((m1, m2) => m1.TwoTail.CompareTo(m2.TwoTail));

      double maxPValue = 0.01 / peakInfos.Count;
      peakInfos.RemoveAll(m => m.TwoTail >= maxPValue);

      new SmallMoleculePeakInfoListTextFormat().WriteToFile(fileName, peakInfos);

      return new string[] { fileName };
    }

    private Dictionary<string, List<double>> ReadData(string sampleDataFile)
    {
      Dictionary<string, List<double>> result = new Dictionary<string, List<double>>();
      using (StreamReader sr = new StreamReader(sampleDataFile))
      {
        sr.ReadLine();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Length == 0)
          {
            break;
          }

          string[] parts = line.Split('\t');
          List<double> intensities = new List<double>();
          for (int i = 1; i < parts.Length; i++)
          {
            intensities.Add(MyConvert.ToDouble(parts[i]));
          }

          result[parts[0]] = intensities;
        }
      }

      return result;
    }
  }
}
