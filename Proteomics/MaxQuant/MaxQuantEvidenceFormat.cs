using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantEvidenceFormat:MascotPeptideTextFormat
  {
    private double ParseDouble(IAnnotation ann, string key)
    {
      var value = ann.Annotations[key] as string;
      if (value.Equals("NaN"))
      {
        return 0;
      }
      return double.Parse(value);
    }

    protected override void DoAfterRead(List<IIdentifiedSpectrum> spectra)
    {
      foreach (var sp in spectra)
      {
        sp.Id = int.Parse(sp.Annotations["id"] as string);
        sp.Query.FileScan.Experimental = sp.Annotations["Raw File"] as string;
        sp.Query.FileScan.FirstScan = int.Parse(sp.Annotations["MS/MS Scan Number"] as string);
        sp.Query.FileScan.LastScan = sp.Query.FileScan.FirstScan;
        sp.Query.RetentionTime = ParseDouble(sp, "Calibrated Retention Time");
        sp.Query.RetentionLength = ParseDouble(sp, "Retention Length");
      }
    }

    public static List<string> GetExperimental(string fileName)
    {
      var result = new List<string>();
      using (StreamReader sr = new StreamReader(fileName))
      {
        var line = sr.ReadLine();
        var parts = line.Split('\t');
        var rawfileindex = Array.IndexOf(parts, "Raw File");
        while ((line = sr.ReadLine()) != null)
        {
          var newparts = line.Split('\t');
          result.Add(newparts[rawfileindex]);
        }
      }
      return (from r in result
              orderby r
              select r).Distinct().ToList();
    }
  }
}
