using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification
{
  public class LabelFreeItem
  {
    public int Scan { get; set; }

    public double RetentionTime { get; set; }

    public double Mz { get; set; }

    public double DeltaMzPPM { get; set; }

    private double intensity;
    public double Intensity
    {
      get
      { return intensity; }
      set
      {
        this.intensity = value;
        this.AdjustIntensity = value;
      }
    }

    public double AdjustIntensity { get; set; }

    public bool Enabled { get; set; }

    public bool Identified { get; set; }
  }
}
