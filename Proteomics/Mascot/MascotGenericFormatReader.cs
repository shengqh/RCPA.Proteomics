using RCPA.Gui;
using RCPA.Proteomics.IO;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Mascot
{
  public static class MascotGenericFormatConstants
  {
    public const string BEGIN_PEAK_LIST_TAG = "BEGIN IONS";
    public const string CHARGE_TAG = "CHARGE";
    public const string END_PEAK_LIST_TAG = "END IONS";
    public const string PEPMASS_TAG = "PEPMASS";
    public const string TITLE_TAG = "TITLE";
    public const string RETENTION_TIME_TAG = "RTINSECONDS";
    public const string SCAN_TAG = "SCANS";
    public const string RAWFILE_TAG = "RAWFILE";

    public const string FILENAME_TAG = "###FILENAME: ";
    public const string MS_SCAN_TAG = "###MS: ";
    public const string MSMS_SCAN_TAG = "###MSMS: ";
    public const string MSMS_PRECURSOR_OFFSET_TAG = "###PRECURSOROFFSET: ";
    public const string MSMS_PRODUCTION_OFFSET_TAG = "###PRODUCTIONROFFSET: ";
  }

  public class MascotGenericFormatIterator<T> : AbstractPeakListIterator<T> where T : IPeak, new()
  {
    private readonly Regex annotationRegex = new Regex(@"(\S*?)=(.*)");

    private readonly StreamReader reader;
    private readonly Regex scanRegex = new Regex(@"[0-9]+");

    public MascotGenericFormatIterator(StreamReader reader)
    {
      this.reader = reader;
      morePeakListAvailable = CheckHasNext(reader);
    }

    protected List<string> ReadHeader(ref string lastLine)
    {
      var result = new List<string>();
      while ((lastLine = this.reader.ReadLine()) != null)
      {
        if (lastLine.Length == 0)
        {
          continue;
        }

        if (lastLine.StartsWith(MascotGenericFormatConstants.BEGIN_PEAK_LIST_TAG))
        {
          break;
        }
        result.Add(lastLine);
      }

      return result;
    }

    protected Dictionary<string, string> ReadAnnotation(ref string lastLine)
    {
      var result = new Dictionary<string, string>();

      while ((lastLine = this.reader.ReadLine()) != null)
      {
        if (lastLine.Trim().StartsWith(MascotGenericFormatConstants.END_PEAK_LIST_TAG))
        {
          break;
        }

        if (lastLine.Trim().Length == 0)
        {
          continue;
        }

        if (Char.IsDigit(lastLine, 0))
        {
          break;
        }

        Match m = this.annotationRegex.Match(lastLine);
        if (m.Success)
        {
          if (result.ContainsKey(m.Groups[1].Value))
          {
            throw new Exception(string.Format("Duplicated key {0} in {1}", m.Groups[1].Value, lastLine));
          }
          result.Add(m.Groups[1].Value, m.Groups[2].Value);
        }
      }

      return result;
    }

    protected List<string> ReadPeak(ref string lastLine)
    {
      var result = new List<string>();
      if (null == lastLine)
      {
        return result;
      }

      if (lastLine.StartsWith(MascotGenericFormatConstants.END_PEAK_LIST_TAG))
      {
        return result;
      }

      if (lastLine.Length > 0)
      {
        result.Add(lastLine);
      }

      while ((lastLine = this.reader.ReadLine()) != null)
      {
        if (lastLine.Length == 0)
        {
          continue;
        }
        if (lastLine.StartsWith(MascotGenericFormatConstants.END_PEAK_LIST_TAG))
        {
          break;
        }
        result.Add(lastLine);
      }

      return result;
    }

    protected override PeakList<T> DoReadNextPeakList(out bool hasNext)
    {
      var result = new PeakList<T>();

      string lastLine = "";
      List<string> headers = ReadHeader(ref lastLine);
      Dictionary<string, string> annotations = ReadAnnotation(ref lastLine);
      List<string> peaks = ReadPeak(ref lastLine);

      hasNext = CheckHasNext(this.reader);

      foreach (string line in headers)
      {
        if (line.StartsWith(MascotGenericFormatConstants.MS_SCAN_TAG))
        {
          ParseScan(result, line);
        }
        else if (line.StartsWith(MascotGenericFormatConstants.MSMS_SCAN_TAG))
        {
          ParseScan(result, line);
        }
        else if (line.StartsWith(MascotGenericFormatConstants.FILENAME_TAG))
        {
          result.Experimental = line.Substring(line.IndexOf(':') + 1).Trim();
        }
      }

      foreach (var de in annotations)
      {
        if (de.Key.Equals(MascotGenericFormatConstants.PEPMASS_TAG))
        {
          string[] values = Regex.Split(de.Value, "\\s");
          result.PrecursorMZ = MyConvert.ToDouble(values[0]);
          if (values.Length > 1)
          {
            result.PrecursorIntensity = MyConvert.ToDouble(values[1]);
          }
        }
        else if (de.Key.Equals(MascotGenericFormatConstants.CHARGE_TAG))
        {
          string charge = de.Value;
          if (charge.Contains("and"))
          {
            result.Annotations.Add(de.Key, de.Value);
            continue;
          }

          if (charge.EndsWith("+"))
          {
            charge = charge.Substring(0, charge.Length - 1);
          }
          result.PrecursorCharge = int.Parse(charge);
        }
        else if (de.Key.Equals(MascotGenericFormatConstants.SCAN_TAG))
        {
          ParseScan(result, de.Value);
        }
        else if (de.Key.Equals(MascotGenericFormatConstants.RETENTION_TIME_TAG))
        {
          double rtInSecond = 0.0;
          if (MyConvert.TryParse(de.Value, out rtInSecond))
          {
            var rtInMin = rtInSecond / 60.0;
            if (result.ScanTimes.Count == 0)
            {
              result.ScanTimes.Add(new ScanTime(0, rtInMin));
            }
            else
            {
              result.ScanTimes[0].RetentionTime = rtInMin;
            }
          }
        }
        else
        {
          result.Annotations.Add(de.Key, de.Value);
        }
      }

      foreach (string line in peaks)
      {
        string[] values = Regex.Split(line, "\\s");
        try
        {
          var peak = new T()
          {
            Mz = MyConvert.ToDouble(values[0]),
            Intensity = MyConvert.ToDouble(values[1])
          };
          result.Add(peak);
        }
        catch (Exception)
        {
          throw new Exception(string.Format("Format error, cannot read peak info from {0}", line));
        }
      }

      return result;
    }

    private void ParseScan(PeakList<T> result, string line)
    {
      result.ScanTimes.Clear();
      MatchCollection matches = this.scanRegex.Matches(line);
      HashSet<int> scans = new HashSet<int>();
      for (int i = 0; i < matches.Count; i++)
      {
        scans.Add(int.Parse(matches[i].Value));
      }
      foreach (var scan in scans)
      {
        result.ScanTimes.Add(new ScanTime(scan, 0.0));
      }
    }

    public static bool CheckHasNext(StreamReader sr)
    {
      bool result = false;
      long position = sr.GetCharpos();
      try
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.StartsWith(MascotGenericFormatConstants.BEGIN_PEAK_LIST_TAG))
          {
            result = true;
            break;
          }
        }
      }
      finally
      {
        sr.SetCharpos(position);
      }

      return result;
    }

    public string GetNextTitle(StreamReader sr)
    {
      string result = string.Empty;

      long position = StreamUtils.GetCharpos(sr);
      try
      {
        string line;
        while ((line = this.reader.ReadLine()) != null)
        {
          if (line.StartsWith(MascotGenericFormatConstants.TITLE_TAG))
          {
            result = line.Substring(line.IndexOf('=') + 1);
          }
        }
      }
      finally
      {
        sr.SetCharpos(position);
      }

      return result;
    }

    public void SkipNext(StreamReader sr)
    {
      string lastLine;
      while ((lastLine = this.reader.ReadLine()) != null)
      {
        if (lastLine.Length == 0)
        {
          continue;
        }
        if (lastLine.StartsWith(MascotGenericFormatConstants.END_PEAK_LIST_TAG))
        {
          break;
        }
      }
    }

    public override string GetFormatName()
    {
      return "MascotGenericFormat";
    }
  }

  public class MascotGenericFormatReader<T> : ProgressClass, IPeakListReader<T> where T : IPeak, new()
  {
    #region IPeakListReader<T> Members

    public string GetFormatName()
    {
      return "MascotGenericFormat";
    }

    public List<PeakList<T>> ReadFromFile(string filename)
    {
      var fi = new FileInfo(filename);
      if (!fi.Exists)
      {
        throw new FileNotFoundException("Cannot find the file " + filename);
      }
      String experimental = FileUtils.ChangeExtension(fi.Name, "");

      var result = new List<PeakList<T>>();
      var sr = new StreamReader(new FileStream(filename, FileMode.Open));
      try
      {
        var iter = new MascotGenericFormatIterator<T>(sr);

        while (iter.HasNext())
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          var pkl = iter.Next();
          if (pkl.Experimental == null || pkl.Experimental.Length == 0)
          {
            pkl.Experimental = experimental;
          }
          result.Add(pkl);
        }
      }
      finally
      {
        sr.Close();
      }

      return result;
    }

    public static string GetTitleSample(string fileName)
    {
      using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open)))
      {
        var iter = new MascotGenericFormatIterator<Peak>(sr);
        return iter.GetNextTitle(sr);
      }
    }

    #endregion
  }
}