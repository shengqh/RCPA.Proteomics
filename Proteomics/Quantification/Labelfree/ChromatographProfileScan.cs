using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromatographProfileScan : List<ChromatographProfileScanPeak>
  {
    public ChromatographProfileScan() { }

    public int Scan { get; set; }

    public double RetentionTime { get; set; }

    public bool Identified { get; set; }

    public double CalculateProfileCorrelation(IsotopicIon[] ions)
    {
      var real = (from peak in this
                  select peak.Intensity).ToArray();
      var theo = (from ion in ions
                  select ion.Intensity).Take(real.Length).ToArray();
      return StatisticsUtils.CosineAngle(real, theo);
    }
  }
}
