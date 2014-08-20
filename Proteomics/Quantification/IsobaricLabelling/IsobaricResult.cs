using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricResult : List<IsobaricItem>
  {
    public IsobaricResult() { }

    public IsobaricResult(IEnumerable<IsobaricItem> items)
      : base(items)
    { }

    public Dictionary<string, Dictionary<int, IsobaricItem>> ToExperimentalScanDictionary()
    {
      return this.ToDoubleDictionary((m => m.Experimental), (m => m.Scan.Scan));
    }

    public bool HasIonInjectionTime()
    {
      return this.Count > 0 && this[0].Scan != null && this[0].Scan.IonInjectionTime != 0.0;
    }

    public IsobaricType PlexType
    {
      get
      {
        if (this.Count == 0)
        {
          return null;
        }
        return this[0].PlexType;
      }
    }

    public String Mode { get; set; }
  }
}
