using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricResult : List<IsobaricScan>
  {
    public IsobaricResult()
    {
      Comments = new Dictionary<string, string>();
    }

    public IsobaricResult(IEnumerable<IsobaricScan> items)
      : base(items)
    {
      Comments = new Dictionary<string, string>();
    }

    public Dictionary<string, Dictionary<int, IsobaricScan>> ToExperimentalScanDictionary()
    {
      return this.ToDoubleDictionary((m => m.Experimental), (m => m.Scan.Scan));
    }

    public bool HasIonInjectionTime()
    {
      return this.Count > 0 && this[0].Scan != null && this[0].Scan.IonInjectionTime != 0.0;
    }

    public IsobaricType PlexType { get; set; }

    public List<UsedChannel> UsedChannels { get; set; }

    //public String Mode { get; set; }

    public Dictionary<string, string> Comments { get; set; }

    public List<Spectrum.PeakList<Spectrum.Peak>> GetMassCalibrationPeaks()
    {
      var allpkls = (from r in this select r.RawPeaks).ToList();

      //Get all peak list with all used channel information
      var validpkls = allpkls.Where(r =>
      {
        foreach (var channel in this.UsedChannels)
        {
          if (!r.HasPeak(channel.Mz, channel.MinMz, channel.MaxMz))
          {
            return false;
          }
        }

        return true;
      }).ToList();

      return validpkls.Count >= this.Count / 2 ? validpkls : allpkls;
    }
  }
}
