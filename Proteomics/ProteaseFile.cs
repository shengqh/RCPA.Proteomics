using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics
{
  public class ProteaseFile
  {
    private Protease defaultProtease;
    private Dictionary<string, Protease> dtaProteaseMap;

    private void Read(string filename)
    {
      this.dtaProteaseMap = new Dictionary<string, Protease>();

      using (var filein = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
      {
        var enzymeMap = new Dictionary<string, Protease>();
        string sline;
        while ((sline = filein.ReadLine()) != null && sline.Length > 0)
        {
          Protease enzyme = ProteaseManager.ValueOf(sline);
          string[] lines = Regex.Split(sline, "\t");
          enzymeMap[lines[0]] = enzyme;
        }

        while ((sline = filein.ReadLine()) != null)
        {
          if (sline.StartsWith("DEFAULT_ENZYME"))
          {
            string[] lines = Regex.Split(sline, "\t");
            this.defaultProtease = enzymeMap[lines[1]];
            break;
          }
        }

        while ((sline = filein.ReadLine()) != null)
        {
          if (sline.Length == 0)
          {
            continue;
          }

          string[] lines = Regex.Split(sline, "\t");
          if (lines.Length < 2)
          {
            break;
          }

          this.dtaProteaseMap[lines[0]] = enzymeMap[lines[1]];
        }
      }
    }

    public void Fill(string enzymeFile, List<IIdentifiedSpectrum> sequestPeptideList)
    {
      Read(enzymeFile);

      foreach (IdentifiedSpectrum sp in sequestPeptideList)
      {
        if (this.dtaProteaseMap.ContainsKey(sp.Query.FileScan.LongFileName))
        {
          sp.DigestProtease = this.dtaProteaseMap[sp.Query.FileScan.LongFileName];
        }
        else
        {
          sp.DigestProtease = this.defaultProtease;
        }
      }
    }

    public void Write(string filename, List<IIdentifiedSpectrum> peptides)
    {
      if (peptides.Count == 0)
      {
        return;
      }

      if (peptides[0].DigestProtease == null)
      {
        return;
      }

      var proteaseMap = new Dictionary<string, Protease>();

      var enzymeNames = new Dictionary<string, int>();
      foreach (IIdentifiedSpectrum spectrum in peptides)
      {
        if (spectrum.DigestProtease != null)
        {
          proteaseMap[spectrum.DigestProtease.Name] = spectrum.DigestProtease;
          if (!enzymeNames.ContainsKey(spectrum.DigestProtease.Name))
          {
            enzymeNames[spectrum.DigestProtease.Name] = 1;
          }
          else
          {
            enzymeNames[spectrum.DigestProtease.Name] = enzymeNames[spectrum.DigestProtease.Name] + 1;
          }
        }
      }

      string maxEnzyme = "";
      int maxCount = -1;
      foreach (string key in enzymeNames.Keys)
      {
        if (enzymeNames[key] > maxCount)
        {
          maxCount = enzymeNames[key];
          maxEnzyme = key;
        }
      }

      using (var sw = new StreamWriter(filename))
      {
        foreach (Protease protease in proteaseMap.Values)
        {
          sw.WriteLine("{0}\t\t\t\t{1}\t{2}\t\t{3}", protease.Name, protease.IsEndoProtease ? 1 : 0,
                       protease.CleaveageResidues, protease.NotCleaveResidues);
        }
        sw.WriteLine();

        sw.WriteLine("DEFAULT_ENZYME\t" + maxEnzyme);

        foreach (IIdentifiedSpectrum spectrum in peptides)
        {
          if (spectrum.DigestProtease == null)
          {
            continue;
          }

          if (!spectrum.DigestProtease.Name.Equals(maxEnzyme))
          {
            sw.WriteLine(spectrum.Query.FileScan.LongFileName + "\t" + spectrum.DigestProtease.Name);
          }
        }
      }
    }
  }
}