using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// Description of IsobaricChannel.
  /// </summary>
  public class IsobaricChannel
  {
    public IsobaricChannel()
    {
      this.Isotopics = new List<IsobaricIsotope>();
    }

    public string Name { get; set; }

    public double Mz { get; set; }

    public double Percentage { get; set; }

    public List<IsobaricIsotope> Isotopics { get; set; }
  }
}
