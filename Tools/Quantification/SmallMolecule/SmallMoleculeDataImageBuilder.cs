using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Tools.Quantification.SmallMolecule;
using System.IO;
using RCPA.Proteomics.Quantification.SmallMolecule;
using System.Drawing;
using RCPA;
using System.Text.RegularExpressions;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public class SmallMoleculeDataImageBuilder : AbstractThreadFileProcessor
  {
    private string dir1;
    private string dir2;
    private string targetDir;
    private Regex peakPattern;
    public SmallMoleculeDataImageBuilder(string sampleDir, string refDir, string targetDir, string peakPattern)
    {
      this.dir1 = sampleDir;
      this.dir2 = refDir;
      this.targetDir = targetDir;
      this.peakPattern = new Regex(peakPattern);
    }

    private static string[] GetFiles(string dir)
    {
      return Directory.GetFiles(dir, "*.d.*.txt");
    }

    private IEnumerable<string> GetPeakInfos(string fileName)
    {
      var sigs = new SmallMoleculePeakInfoListTextFormat().ReadFromFile(fileName);

      return from m in sigs
             where peakPattern.Match(m.Peak).Success
             select m.Peak;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var result = new List<string>();

      var peakNames = GetPeakInfos(fileName);
      var peaks = from p in peakNames
                  select p.Substring(1);

      string[] positiveSampleFiles = GetFiles(dir1);
      string[] positiveReferencesFiles = GetFiles(dir2);

      List<FileData2> sampleDatas = FileData2.ReadFiles(positiveSampleFiles, peaks);
      List<FileData2> referencesDatas = FileData2.ReadFiles(positiveReferencesFiles, peaks);

      foreach (var peakName in peakNames)
      {
        var file = targetDir + "\\" + peakName + ".png";
        if (File.Exists(file))
        {
          continue;
        }

        Console.WriteLine(file);

        var peak = peakName.Substring(1);
        var maxIntensity = (from m in sampleDatas
                            from p in m[peak]
                            select p.Intensity).Union(from m in referencesDatas
                                                      from p in m[peak]
                                                      select p.Intensity).Max();

        SmallMoleculeSignificantPeakImageBuilder2 builder2 = new SmallMoleculeSignificantPeakImageBuilder2(peak, sampleDatas, referencesDatas, Color.Red, Color.Blue);
        result.AddRange( builder2.Process(file));
      }

      sampleDatas.Clear();
      referencesDatas.Clear();
      GC.Collect();
      GC.WaitForFullGCComplete();

      return result;
    }
  }
}
