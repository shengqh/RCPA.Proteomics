using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// Description of IsobaricChannel.
  /// </summary>
  public class IsobaricChannel : ICloneable
  {
    public IsobaricChannel()
    {
      this.Isotopics = new List<IsobaricIsotope>();
    }

    public string Name { get; set; }

    public double Mz { get; set; }

    public double Percentage { get; set; }

    public List<IsobaricIsotope> Isotopics { get; set; }

    public object Clone()
    {
      var result = new IsobaricChannel();

      result.Name = Name;
      result.Mz = Mz;
      result.Percentage = Percentage;
      result.Isotopics = Isotopics;

      return result;
    }
  }
}
