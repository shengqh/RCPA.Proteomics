using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Raw
{
  public interface IRawScanFilter
  {
    //any|+|-
    string Polarity { get; }

    //1|2|...
    int MsLevel { get; }

    double PrecursorMZ { get; }

    double CollisionEnergy { get; }

    //Full|Zoom|...
    string ScanType { get; }

    //Centroided|Profile
    string SpectrumType { get; }

    string Filter { get; set; }
  } ;

  public class RawScanFilter : IRawScanFilter
  {
    private static readonly Regex filterRegex = new Regex(@"([a+-])\s([cp])\s(?:.+\s){0,1}(\S+)\sms([\s\d])");
    private static readonly Regex collisionEnergyRegex = new Regex(@"([\d.]+)");
    private double collisionEnergy;

    private string filter = "";
    private int msLevel = 1;
    private string polarity = "+";
    private double precursorMZ;
    private string scanType = "Full";
    private string spectrumType = "Profile";

    #region IRawScanFilter Members

    public int MsLevel
    {
      get { return this.msLevel; }
    }

    public double PrecursorMZ
    {
      get { return this.precursorMZ; }
    }

    public string Filter
    {
      get { return this.filter; }
      set { SetFilter(value); }
    }

    public string ScanType
    {
      get { return this.scanType; }
    }

    public string Polarity
    {
      get { return this.polarity; }
    }

    public string SpectrumType
    {
      get { return this.spectrumType; }
    }

    public double CollisionEnergy
    {
      get { return this.collisionEnergy; }
    }

    #endregion

    private void SetFilter(string filter)
    {
      this.filter = filter;
      //full scan example
      //+ c ESI Full ms [ 400.00-1800.00]
      //ms2 scan example
      //+ c ESI d Full ms2 1026.70@35.00 [ 270.00-2000.00]
      //zoom scan example
      //+ p NSI d Z ms [ 270.00-2000.00]
      //ecd example
      //+ p NSI !det Full ms2 842.00@0.00 ecd@5.00 [ 230.00-2000.00]
      //SRM example
      //+ c NSI SRM ms2

      Match match = filterRegex.Match(filter);
      if (!match.Success)
      {
        throw new ArgumentException("It's not a valid filter string : " + filter);
      }

      if ("a" == match.Groups[1].Value)
      {
        this.polarity = "any";
      }
      else
      {
        this.polarity = match.Groups[1].Value;
      }

      if ("c" == match.Groups[2].Value)
      {
        this.spectrumType = "Centroided";
      }
      else
      {
        this.spectrumType = "Profile";
      }

      if ("Z" == match.Groups[3].Value)
      {
        this.scanType = "zoom";
      }
      else
      {
        this.scanType = match.Groups[3].Value;
      }

      if (" " == match.Groups[4].Value)
      {
        this.msLevel = 1;
        this.precursorMZ = 0.0;
      }
      else
      {
        this.msLevel = int.Parse(match.Groups[4].Value);

        int pos = filter.IndexOf('[');
        if (pos == -1)
        {
          this.precursorMZ = 0.0;
        }
        else
        {
          var strs = filter.Substring(0, pos - 1).Trim().Split(' ');

          for (int i = strs.Length - 1; i >= 0; i--)
          {
            var precursorStr = strs[i];

            double precursorMz;
            if (precursorStr.Contains('@'))
            {
              var parts = precursorStr.Split('@');
              if (MyConvert.TryParse(parts[0], out precursorMz))
              {
                this.precursorMZ = precursorMz;

                var m = collisionEnergyRegex.Match(parts[1]);
                if (m.Success)
                {
                  this.collisionEnergy = MyConvert.ToDouble(m.Groups[1].Value);
                }
                break;
              }
            }
            else
            {
              if (MyConvert.TryParse(precursorStr, out precursorMz))
              {
                this.precursorMZ = precursorMz;
                this.collisionEnergy = 0;
                break;
              }
            }
          }
        }
      }
    }
  } ;
}