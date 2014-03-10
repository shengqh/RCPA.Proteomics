using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Spectrum
{
  public class Envelope : List<double>
  {
    private int charge;

    private double gap;

    public Envelope(double monoIsotopicMZ, int charge, int initLength)
    {
      this.Add(monoIsotopicMZ);
      this.charge = charge;
      this.gap = ChargeDeconvolution.C_GAP / charge;
      Calculate(initLength);
    }

    public new double this[int index]
    {
      get
      {
        if (this.Count <= index)
        {
          Calculate(index);
        }
        return this[index];
      }
    }

    private void Calculate(int index)
    {
      while (this.Count < index)
      {
        this.Add(this.Last() + gap);
      }
    }
  }
}
