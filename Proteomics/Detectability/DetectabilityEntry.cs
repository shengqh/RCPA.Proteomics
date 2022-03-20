using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Detectability
{
  public class DetectabilityEntry
  {
    private string peptide;
    public string Protein { get; set; }

    public string Peptide
    {
      get { return this.peptide; }
      set { this.peptide = value; }
    }

    public double Detectability { get; set; }

    public int Position { get; set; }

    public static List<DetectabilityEntry> ReadFromDirectory(string dir)
    {
      List<string> peptides = FileUtils.ReadFile(dir + "\\peppro.txt", true);
      List<string> detectability = FileUtils.ReadFile(dir + "\\detectabilityres.txt", true);
      List<string> positions = FileUtils.ReadFile(dir + "\\positions.txt", true);

      var deList = new List<DetectabilityEntry>();
      for (int iPep = 0; iPep < peptides.Count; iPep++)
      {
        string pep = peptides[iPep];
        string[] pp = Regex.Split(pep, "\\s+");
        if (pp.Length < 2)
        {
          throw new Exception("Wrong peptide/protein line : " + pep);
        }

        double d = MyConvert.ToDouble(detectability[iPep].Trim());
        int position = int.Parse(positions[iPep].Trim());

        var de = new DetectabilityEntry();
        de.Protein = pp[1];
        de.Peptide = pp[0];
        de.Detectability = d;
        de.Position = position;
        deList.Add(de);
      }
      return deList;
    }

    public static void DeleteFromDirectory(string dir)
    {
      File.Delete(dir + "\\peppro.txt");
      File.Delete(dir + "\\detectabilityres.txt");
      File.Delete(dir + "\\positions.txt");
    }

    public static List<DetectabilityEntry> ReadFromFile(string filename)
    {
      var result = new List<DetectabilityEntry>();

      List<string> lines = FileUtils.ReadFile(filename, true);
      for (int i = 1; i < lines.Count; i++)
      {
        String[] parts = Regex.Split(lines[i], "\t");
        if (4 == parts.Length)
        {
          var de = new DetectabilityEntry();
          de.Protein = parts[0];
          de.Peptide = parts[1];
          de.Detectability = MyConvert.ToDouble(parts[2].Trim());
          de.Position = int.Parse(parts[3].Trim());
          result.Add(de);
        }
      }

      return result;
    }

    public static void WriteToFile(string filename, List<DetectabilityEntry> deList)
    {
      using (var sw = new StreamWriter(filename))
      {
        sw.WriteLine("Protein\tPeptide\tDetectability\tPosition");
        foreach (DetectabilityEntry de in deList)
        {
          sw.WriteLine("{0}\t{1}\t{2:0.0000}\t{3}",
                       de.Protein,
                       de.peptide,
                       de.Detectability,
                       de.Position);
        }
      }
    }
  }
}