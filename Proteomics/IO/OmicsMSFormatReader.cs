using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.IO
{
  public class OmicsMSFormatReader<T> : IPeakListReader<T> where T : IPeak, new()
  {
    #region IPeakListReader<Peak> Members

    public List<PeakList<T>> ReadFromFile(string filename)
    {
      //Console.WriteLine(filename);
      var fi = new FileInfo(filename);
      if (!fi.Exists)
      {
        throw new FileNotFoundException("Cannot find the file " + filename);
      }

      var result = new List<PeakList<T>>();
      var filein = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read));
      try
      {
        PeakList<T> pl;
        string lastline = "";
        while ((pl = ReadNextPeakList(filein, ref lastline)) != null)
        {
          pl.Experimental = fi.Name;
          result.Add(pl);
        }
        return result;
      }
      finally
      {
        filein.Close();
      }
    }

    public string GetFormatName()
    {
      return "OmicsMSFormat";
    }

    #endregion

    private PeakList<T> ReadNextPeakList(StreamReader filein, ref string lastline)
    {
      //Find the scan/retentionTime line (start with '>')
      if (lastline == null || lastline.Length == 0 || lastline[0] != '>')
      {
        while ((lastline = filein.ReadLine()) != null)
        {
          if (lastline.Length > 0 && lastline[0] == '>')
          {
            break;
          }
        }
        if (lastline == null)
        {
          return null;
        }
      }

      string[] parts = Regex.Split(lastline.Substring(1), @"\s");
      var result = new PeakList<T>();
      result.ScanTimes.Add(new ScanTime(int.Parse(parts[0]), MyConvert.ToDouble(parts[1])));
      if (parts.Length > 2)
      {
        result.MsLevel = int.Parse(parts[2]);
      }

      while ((lastline = filein.ReadLine()) != null)
      {
        if (lastline.Length == 0)
        {
          continue;
        }

        if (lastline[0] == '>')
        {
          break;
        }

        string[] peakParts = Regex.Split(lastline, @"\s");
        var peak = new T();
        peak.Mz = MyConvert.ToDouble(peakParts[0]);
        peak.Intensity = MyConvert.ToDouble(peakParts[1]);
        if (peakParts.Length > 2)
        {
          peak.Charge = int.Parse(peakParts[2]);
        }

        result.Add(peak);
      }

      return result;
    }
  }
}