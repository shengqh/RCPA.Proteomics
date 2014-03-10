using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class MyIsobaricType
  {
    public string Name { get; set; }

    public string FileName { get; set; }

    public IsobaricDefinition Definition { get; set; }

    public List<IsobaricIndex> GetFuncs()
    {
      return (from item in this.Definition.Items
              select new IsobaricIndex(item.Index)).ToList();
    }

    public override string ToString()
    {
      return Name;
    }
  }
}
