using RCPA.Proteomics.Mascot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Format
{
  public class MaxQuantToMgfProcessor : AbstractThreadFileProcessor
  {
    private IEnumerable<string> sourceFiles;

    public MaxQuantToMgfProcessor(IEnumerable<string> sourceFiles)
    {
      this.sourceFiles = sourceFiles.ToList();
    }

    public override IEnumerable<string> Process(string targetDir)
    {
      var result = new List<string>();
      foreach (var sourceFile in sourceFiles)
      {
        Progress.SetMessage("Converting " + sourceFile + "...");
        var targetFile = targetDir + "\\" + Path.GetFileNameWithoutExtension(sourceFile) + ".mgf";
        using (StreamReader sr = new StreamReader(sourceFile))
        using (StreamWriter sw = new StreamWriter(targetFile))
        {
          Progress.SetRange(0, sr.BaseStream.Length);

          int count = 0;
          string line;

          List<string> spectrum = new List<string>();
          bool bComment = true;
          while ((line = sr.ReadLine()) != null)
          {
            count++;
            if (count % 100 == 0)
            {
              Progress.SetPosition(sr.BaseStream.Position);
            }

            if (line.StartsWith("peaklist start"))
            {
              bComment = false;
              spectrum.Add(MascotGenericFormatConstants.BEGIN_PEAK_LIST_TAG);
            }
            else if (line.StartsWith("mz="))
            {
              spectrum.Add(line.Replace("mz", MascotGenericFormatConstants.PEPMASS_TAG));
            }
            else if (line.StartsWith("charge"))
            {
              var charges = line.Substring(7);
              if (charges.ToLower().Contains("and"))
              {
                var chargeParts = charges.Split(new string[] { "and" }, StringSplitOptions.RemoveEmptyEntries);
                spectrum.Add(MascotGenericFormatConstants.CHARGE_TAG + "=" + (from cp in chargeParts
                                                                              select cp.Trim() + "+").Merge(" and "));
              }
              else
              {
                spectrum.Add(line.Replace("charge", MascotGenericFormatConstants.CHARGE_TAG) + "+");
              }
            }
            else if (line.StartsWith("header"))
            {
              spectrum.Add(line.Replace("header=", MascotGenericFormatConstants.TITLE_TAG + "="));
            }
            else if (line.StartsWith("peaklist end"))
            {
              spectrum.Add(MascotGenericFormatConstants.END_PEAK_LIST_TAG);

              spectrum.ForEach(m => sw.WriteLine(m));
              spectrum.Clear();
              bComment = true;
            }
            else if (bComment)
            {
              sw.WriteLine(line);
            }
            else if (line.Length > 0 && Char.IsLetter(line[0]))
            {
              spectrum.Insert(0, "###" + line);
            }
            else
            {
              spectrum.Add(line);
            }
          }
        }

        result.Add(targetFile);
      }

      return result;
    }
  }
}
