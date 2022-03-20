using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.MaxQuant
{
  public class IntensityEntry
  {
    public string Name { get; set; }
    public double Light { get; set; }
    public double Heavy { get; set; }
    public double Intensity { get; set; }
  }

  public class IntensityEntries : List<IntensityEntry>
  { }

  public class IntensityEntryParser
  {
    public IntensityEntry ParseHeader(string header)
    {
      var parts = header.Split('\t');
      var samples = (from p in parts
                     where p.StartsWith("Intensity L ")
                     select p.Substring(12)).ToList();
      return null;
    }
  }

  public class IntensityEntriesParser
  {
    public void ParseHeader(string header)
    {
      var parts = header.Split('\t');
      var samples = (from p in parts
                     where p.StartsWith("Intensity L ")
                     select p.Substring(12)).ToList();
    }

    public IntensityEntries Parse(string line)
    {
      return null;
    }
  }
}
