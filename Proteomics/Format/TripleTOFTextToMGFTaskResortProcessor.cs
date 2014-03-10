using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Format
{
  public class TripleTOFTextToMGFTaskResortProcessor : AbstractParallelTaskProcessor
  {
    private string software;

    private string targetDir;

    private double precursorTolerance;

    public TripleTOFTextToMGFTaskResortProcessor(string targetDir, string software, double precursorTolerance)
    {
      this.software = software;
      this.targetDir = targetDir;
      this.precursorTolerance = precursorTolerance;
    }

    //"Label: C1, Spot_Id: 1770483, Peak_List_Id: 2040341, MSMS Job_Run_Id: 41733, Comment: "
    private Regex reg = new Regex(@"Label: (\S+), Spot_Id: (\d+), Peak_List_Id: (\d+), MSMS Job_Run_Id: (\d+)");
    private Regex nameReg = new Regex(@"(\w)(\d+)");
    private Regex chargeReg = new Regex(@"CHARGE=(\d+)\+");

    public override IEnumerable<string> Process(string fileName)
    {
      var result = new FileInfo(targetDir + "\\" + Path.ChangeExtension(new FileInfo(fileName).Name, ".mgf")).FullName;

      int charge = 1;
      PeakList<Peak> ms1 = new PeakList<Peak>();

      //读取ms1
      List<string> comments = new List<string>();
      comments.Add("COM=" + software);
      using (StreamReader sr = new StreamReader(fileName))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.StartsWith("COM="))
          {
            comments.Add(line);
            continue;
          }

          if (line.StartsWith("BEGIN IONS"))
          {
            break;
          }

          comments.Add(line);
          var parts = line.Split('\t');
          if (parts.Length == 2)
          {
            try
            {
              ms1.Add(new Peak(double.Parse(parts[0]), double.Parse(parts[1])));
            }
            catch (Exception) { }
          }
        }
      }

      MascotGenericFormatReader<Peak> reader = new MascotGenericFormatReader<Peak>();
      var pkls = reader.ReadFromFile(fileName);

      foreach (var pkl in pkls)
      {
        var mzTolerance = PrecursorUtils.ppm2mz(pkl.PrecursorMZ, precursorTolerance);
        var peak = ms1.FindPeak(pkl.PrecursorMZ, mzTolerance).FindMaxIntensityPeak();

        if (peak == null)
        {
          pkl.PrecursorIntensity = ms1.Min(m => m.Intensity);
        }
        else
        {
          pkl.PrecursorIntensity = peak.Intensity;
        }
      }

      pkls.Sort((m1, m2) => m2.PrecursorIntensity.CompareTo(m1.PrecursorIntensity));

      for (int i = 0; i < pkls.Count; i++)
      {
        var line = pkls[i].Annotations[MascotGenericFormatConstants.TITLE_TAG] as string;
        var m = reg.Match(line);
        string experimental;
        if (m.Success)
        {
          experimental = m.Groups[1].Value;
          var m2 = nameReg.Match(experimental);
          if (m2.Success)
          {
            if (m2.Groups[2].Value.Length == 1)
            {
              experimental = m2.Groups[1].Value + "0" + m2.Groups[2].Value;
            }
          }
        }
        else
        {
          experimental = pkls[i].Experimental;
        }
        pkls[i].Annotations[MascotGenericFormatConstants.TITLE_TAG] = string.Format("{0}.{1}.{2:0}.{3}.dta", experimental, i + 1, pkls[i].PrecursorIntensity, charge);
      }

      var writer = new MascotGenericFormatSqhWriter<Peak>();
      writer.Comments.AddRange(comments);
      writer.WriteToFile(result, pkls);

      return new string[] { result };
    }
  }
}
