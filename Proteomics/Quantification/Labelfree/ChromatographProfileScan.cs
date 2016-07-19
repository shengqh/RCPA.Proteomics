using RCPA.Proteomics.Spectrum;
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

    public List<Peak> RawPeaks { get; set; }

    public double RetentionTime { get; set; }

    public bool Identified { get; set; }

    public double CalculateProfileCorrelation(double[] theo)
    {
      var real = (from peak in this select peak.Intensity).ToArray();
      return StatisticsUtils.PearsonCorrelation(real, theo, Math.Min(real.Length, theo.Length));
    }
  }
}
