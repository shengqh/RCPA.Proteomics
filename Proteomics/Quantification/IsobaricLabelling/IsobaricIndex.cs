using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricIndex
  {
    public IsobaricIndex(IsobaricType itype, int index)
    {
      this.Index = index;
      this.Name = itype.Channels[index].Name;
    }

    public int Index { get; private set; }

    public double GetValue(IsobaricScan item)
    {
      return item[Index];
    }

    public string Name { get; set; }

    public override string ToString()
    {
      return Name;
    }

    public string ChannelRatioName
    {
      get { return this.Name + "/REF"; }
    }
  }
}
