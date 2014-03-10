using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public class SmallMoleculePeakInfo
  {
    public SmallMoleculePeakInfo(double[] samples, double[] references, string peak)
    {
      this.samples = samples;
      this.references = references;
      this.Peak = peak;
      CalculateSignificance();
    }

    private double[] samples;
    public double[] Samples
    {
      get
      {
        return samples;
      }
      set
      {
        samples = value;
      }
    }

    private double[] references;
    public double[] References
    {
      get
      {
        return references;
      }
      set
      {
        references = value;
      }
    }

    public string Peak { get; set; }
    public double TwoTail { get; set; }
    public double LeftTail { get; set; }
    public double RightTail { get; set; }

    private void CalculateSignificance()
    {
      if (samples != null && references != null)
      {
        double twoTail = 0;
        double leftTail = 0;
        double rightTail = 0;

        var s = Get90Percent(samples);
        var r = Get90Percent(references);

        alglib.studentttests.studentttest2(s, s.Length, r, r.Length, ref twoTail, ref leftTail, ref rightTail);
        this.TwoTail = twoTail;
        this.LeftTail = leftTail;
        this.RightTail = rightTail;
      }
    }

    private double[] Get90Percent(double[] data)
    {
      int count = (int)(data.Length * 0.05);
      return (from s in data
              orderby s
              select s).Take(data.Length - count).Skip(count).ToArray();
    }
  }
}
