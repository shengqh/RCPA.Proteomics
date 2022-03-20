using RCPA.Proteomics.Spectrum;
using RCPA.Utils;
using System;
using System.Linq;

namespace RCPA.Tools.Quantification
{
  public class QuantificationEnvelope
  {
    public double Intensity { get; set; }

    public double Correlation { get; set; }

    public double[] Profile { get; private set; }

    public Peak[] Observed { get; private set; }

    public QuantificationEnvelope(double[] profile, Peak[] observed)
    {
      this.Profile = profile;
      this.Observed = observed;
      this.Intensity = CalculateIntensity();
      this.Correlation = CalculateCorrelation();
    }

    private double CalculateCorrelation()
    {
      double[] real = (from p in Observed
                       select p.Intensity).ToArray();

      double sumReal = real.Sum();

      if (sumReal == 0)
      {
        return 0;
      }

      real = (from r in real
              let s = r / sumReal
              select s).ToArray();

      double[] pro = Profile.Take(Observed.Length).ToArray();
      double sumPro = pro.Sum();
      pro = (from p in pro
             let s = p / sumPro
             select s).ToArray();

      return StatisticsUtils.PearsonCorrelation(real, pro);
    }

    private double CalculateIntensity()
    {
      double result = 0.0;
      double percent = 0.0;
      for (int i = 0; i < Observed.Length; i++)
      {
        if (Observed[i].Intensity != 0)
        {
          result += Observed[i].Intensity;
          percent += Profile[i];
        }
      }

      if (0.0 == result)
      {
        return 0.0;
      }

      return result / percent;
    }

  }
}
