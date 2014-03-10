using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Quantification.SmallMolecule;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Utils;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public class SmallMoleculeDataMostIntensityBuilder : AbstractThreadFileProcessor
  {
    private class Item
    {
      public double TotalIntensity { get; set; }
      public string MZ { get; set; }
      public string FileName { get; set; }
      public double Intensity { get; set; }
    }

    private Regex nameReg;

    private bool normalizeByTotalIntensity;

    public SmallMoleculeDataMostIntensityBuilder(string namePattern, bool normalizeByTotalIntensity)
    {
      this.nameReg = new Regex(namePattern);
      this.normalizeByTotalIntensity = normalizeByTotalIntensity;
    }

    public override IEnumerable<string> Process(string rootDirectory)
    {
      string[] files = Directory.GetFiles(rootDirectory, "*.d.100.400.txt");

      List<Item> items = new List<Item>();

      Progress.SetRange(1, files.Length);
      int count = 0;
      double totalIntensity = 0.0;
      foreach (var file in files)
      {
        Progress.SetPosition(count++);

        var pureName = new FileInfo(file).Name;
        Match m = nameReg.Match(pureName);
        if (!m.Success || m.Groups.Count < 2)
        {
          throw new ArgumentException("Regex pattern doesn't match to file : " + file);
        }

        string name = "";
        for (int i = 1; i < m.Groups.Count; i++)
        {
          name = name + m.Groups[i].Value;
        }
        if (name.StartsWith("_"))
        {
          name = name.Substring(1);
        }

        Progress.SetMessage("Parsing " + file + " for " + name + "...");

        string mode = pureName.Contains("neg") ? "N" : "P";

        FileData2 data = new FileData2();
        data.Read(file);

        if (totalIntensity == 0.0)
        {
          totalIntensity = data.TotalIntensity;
        }

        double ratio = 1;
        if (normalizeByTotalIntensity)
        {
          ratio = totalIntensity / data.TotalIntensity;
        }

        var map = data.MaxIntensityMap;
        foreach (var entry in map)
        {
          items.Add(new Item()
          {
            FileName = name,
            MZ = mode + entry.Key,
            Intensity = entry.Value * ratio
          });
        }
      }

      var dic = items.ToDoubleDictionary(m => m.MZ, m => m.FileName);
      dic.Remove("P400");
      dic.Remove("N400");

      var peaks = dic.Keys.ToList();
      peaks.Sort();
      var fileNames = dic.Values.FirstOrDefault().Keys.ToList();
      fileNames.Sort();

      var resultFile = new DirectoryInfo(rootDirectory).FullName;
      if (resultFile.EndsWith(@"\"))
      {
        resultFile = resultFile.Substring(0, resultFile.Length - 1);
      }
      resultFile = resultFile + ".data";

      using (StreamWriter sw = new StreamWriter(resultFile))
      {
        sw.Write("Peak/File");
        foreach (var file in fileNames)
        {
          sw.Write("\t" + file);
        }
        sw.WriteLine();

        foreach (var peak in peaks)
        {
          sw.Write(peak);
          var dic2 = dic[peak];

          foreach (var file in fileNames)
          {
            if (!dic2.ContainsKey(file))
            {
              throw new Exception(MyConvert.Format("Cannot find peak {0} of file {1}", peak, file));
            }

            sw.Write("\t{0:0.0}", dic2[file].Intensity);
          }
          sw.WriteLine();
        }
      }

      return new string[] { resultFile };
    }
  }
}
