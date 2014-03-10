using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.MaxQuant;
using System.IO;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Format
{
  public class MaxQuantToSingleMgfProcessor : AbstractThreadFileProcessor
  {
    private List<string> sourceFiles;

    public MaxQuantToSingleMgfProcessor(IEnumerable<string> sourceFiles)
    {
      this.sourceFiles = new List<string>(sourceFiles);
    }

    public override IEnumerable<string> Process(string targetFile)
    {
      using (StreamWriter sw = new StreamWriter(targetFile))
      {
        for (int i = 0; i < sourceFiles.Count; i++)
        {
          var sourceFile = sourceFiles[i];
          Progress.SetMessage("Converting {0}/{1} : {2} ...", i + 1, sourceFiles.Count, sourceFile);
          using (StreamReader sr = new StreamReader(sourceFile))
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
                Progress.SetPosition(sr.GetCharpos());
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
        }
      }

      return new string[] { targetFile };
    }
  }
}
