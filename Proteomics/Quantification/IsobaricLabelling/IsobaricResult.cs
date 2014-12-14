using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricResult : List<IsobaricScan>
  {
    public IsobaricResult() { }

    public IsobaricResult(IEnumerable<IsobaricScan> items)
      : base(items)
    { }

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

    public String Mode { get; set; }
  }
}
