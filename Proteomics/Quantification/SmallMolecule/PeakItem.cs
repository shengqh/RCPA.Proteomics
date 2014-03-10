using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.SmallMolecule
{
  public class PeakItem
  {
    public PeakItem(int scan, double mz, double intensity)
    {
      this.Scan = scan;
      this.Mz = mz;
      this.Intensity = intensity;
      this.SmoothedIntensity = intensity;
      this.PPM = 0.0;
    }

    public int Scan { get; set; }

    public double Mz { get; set; }

    public double Intensity { get; set; }

    public double SmoothedIntensity { get; set; }

    public double PPM { get; set; }
  }
}
