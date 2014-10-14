using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics
{
  public class SequestFilename
  {
    private static readonly Regex fileNamePattern = new Regex(@"(.+?)\.\s*(\d+)\.\s*(\d+)\.\s*(\d{1,2})\.([^.]*)$");
    private static readonly Regex fileScanPattern = new Regex("\"{0,1}(.+),(.+)\"{0,1}$");
    private static readonly Regex scanPattern = new Regex(@"(\d+)(\s*-\s*(\d+)){0,1}");

    private int charge;
    private string experimental;
    private string extension;
    private int firstScan;
    private int lastScan;
    private string longFileName;
    private string shortFileName;

    private void initFileName()
    {
      longFileName = GetLongFilename();
      shortFileName = GetFileScan();
    }

    public SequestFilename()
    {
    }

    public SequestFilename(string experimental, int firstScan, int lastScan, int charge, string extension)
    {
      this.experimental = experimental;
      this.firstScan = firstScan;
      this.lastScan = lastScan;
      this.charge = charge;
      this.extension = extension;

      initFileName();
    }

    public string Experimental
    {
      get { return this.experimental; }
      set
      {
        this.experimental = value;
        initFileName();
      }
    }

    public int FirstScan
    {
      get { return this.firstScan; }
      set
      {
        this.firstScan = value;
        initFileName();
      }
    }

    public int LastScan
    {
      get { return this.lastScan; }
      set
      {
        this.lastScan = value;
        initFileName();
      }
    }

    public int Charge
    {
      get { return this.charge; }
      set
      {
        this.charge = value;
        initFileName();
      }
    }

    public string Extension
    {
      get { return this.extension; }
      set
      {
        this.extension = value;
        initFileName();
      }
    }

    /**
     * ShortFilename format:
     * JWH_SAX_25_050906,13426 - 13428
     * which means filename is JWH_SAX_25_050906, first scan is 13426 and last scan is 13428
     **/

    public string ShortFileName
    {
      get { return shortFileName; }
      set
      {
        SetFileScan(value);
        initFileName();
      }
    }

    public String Scan
    {
      get { return GetScan(); }
      set
      {
        SetScan(value);
        initFileName();
      }
    }

    public string LongFileName
    {
      get { return longFileName; }
      set
      {
        SetLongFilename(value);
        initFileName();
      }
    }

    public static SequestFilename Parse(string filename)
    {
      var result = new SequestFilename();
      result.LongFileName = filename;
      return result;
    }

    private string GetFileScan()
    {
      var result = new StringBuilder();
      if (!string.IsNullOrEmpty(this.experimental))
      {
        result.Append(this.experimental);
        result.Append(",");
      }
      result.Append(Scan);
      return result.ToString();
    }

    private void SetFileScan(string fileScan)
    {
      Match match = fileScanPattern.Match(fileScan);
      if (!match.Success)
      {
        try
        {
          LongFileName = fileScan;
          return;
        }
        catch (Exception)
        {
          throw new Exception(fileScan +
                              " is not a valid file scan line (for example :JWH_SAX_25_050906,12349 or JWH_SAX_25_050906,13426 - 13428)");
        }
      }

      this.experimental = match.Groups[1].Value.Trim();
      Scan = match.Groups[2].Value.Trim();
    }

    /***
     * Scan information:
     * 1100 (firstscan = lastscan = 1100)
     * or
     * 1100 - 1103 (firstscan = 1100, lastscan = 1103)
     **/

    private String GetScan()
    {
      var sb = new StringBuilder();
      sb.Append(this.firstScan);
      if (this.firstScan != this.lastScan)
      {
        sb.Append(" - " + this.lastScan);
      }
      return sb.ToString();
    }

    private void SetScan(string value)
    {
      Match match = scanPattern.Match(value);
      if (!match.Success)
      {
        throw new Exception(value + " is not a valid scan line (for example :12349 or 13426 - 13428)");
      }

      this.firstScan = int.Parse(match.Groups[1].Value);

      string endScanStr = match.Groups[3].Value;
      if (0 == endScanStr.Length)
      {
        this.lastScan = this.firstScan;
      }
      else
      {
        this.lastScan = int.Parse(endScanStr);
      }
    }

    /**
     * LongFilename format:
     * JWH_SAX_25_050906.13426.13428.2.dta
     * which means filename is JWH_SAX_25_050906, 
     * first scan is 13426,
     * last scan is 13428,
     * charge is 2
     * extension is dta
     **/

    private string GetLongFilename()
    {
      if (string.IsNullOrEmpty(this.extension))
      {
        return MyConvert.Format("{0}.{1}.{2}.{3}", this.experimental, this.firstScan, this.lastScan, this.charge);
      }
      else
      {
        return MyConvert.Format("{0}.{1}.{2}.{3}.{4}", this.experimental, this.firstScan, this.lastScan, this.charge,
                             this.extension);
      }
    }

    public static bool IsLongFilename(string filename)
    {
      return fileNamePattern.Match(filename).Success;
    }

    private void SetLongFilename(string filename)
    {
      Match match = fileNamePattern.Match(filename);
      if (!match.Success)
      {
        match = fileNamePattern.Match(filename + ".dta");
        if (!match.Success)
        {
          throw new ArgumentException("It's not a valid sequest dta/out name " + filename);
        }
      }
      this.experimental = match.Groups[1].Value.Trim();
      this.firstScan = int.Parse(match.Groups[2].Value.Trim());
      this.lastScan = int.Parse(match.Groups[3].Value.Trim());
      this.charge = int.Parse(match.Groups[4].Value.Trim());
      this.extension = match.Groups[5].Value.Trim();
    }

    public override string ToString()
    {
      return longFileName;
    }

    public bool EqualScanCharge(SequestFilename another)
    {
      return string.Equals(this.experimental, another.experimental) &&
             this.firstScan == another.firstScan &&
             this.charge == another.charge;
    }

    public bool EqualScan(SequestFilename another)
    {
      return string.Equals(this.experimental, another.experimental) &&
             this.firstScan == another.firstScan;
    }
  }
}