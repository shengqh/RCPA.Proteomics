using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public static class IsobaricExtension
  {
    public static List<IsobaricIndex> GetFuncs(this IsobaricType plexType)
    {
      return (from item in plexType.GetDefinition().Items
              select new IsobaricIndex(item.Index)).ToList();
    }
  }
}
