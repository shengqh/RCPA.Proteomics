using RCPA.Proteomics.Spectrum;
using RCPA.Utils;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacPeakListPair
  {
    public SilacPeakListPair()
    {
      Enabled = true;
    }

    public SilacPeakListPair(PeakList<Peak> light, PeakList<Peak> heavy)
      : this()
    {
      Light = light;
      Heavy = heavy;
    }

    public int Scan
    {
      get { return Light.ScanTimes[0].Scan; }
    }

    /// <summary>
    /// �Ƿ�õ��˼�����
    /// </summary>
    public bool IsIdentified { get; set; }

    /// <summary>
    /// �Ƿ�ͨ����չ�õ��ļ����������ظ����飬һ����scan 100������һ����scan 110����������չ������ʱ��
    /// ��������μ��������һ������ʵ�ģ�һ���Ǵ���չ�õ��ġ�
    /// </summary>
    public bool IsExtendedIdentification { get; set; }

    public bool IsSelected { get; set; }

    public bool Enabled { get; set; }

    public PeakList<Peak> Light { get; set; }

    public PeakList<Peak> Heavy { get; set; }

    public double LightIntensity { get; set; }

    public double HeavyIntensity { get; set; }

    public double LightCorrelation { get; set; }

    public double HeavyCorrelation { get; set; }

    public void CalculateIntensity(List<Peak> lightProfile, List<Peak> heavyProfile)
    {
      this.LightIntensity = DoCalculateIntensity(lightProfile, Light);
      this.HeavyIntensity = DoCalculateIntensity(heavyProfile, Heavy);
    }

    public void CalculateCorrelation(List<Peak> lightProfile, List<Peak> heavyProfile)
    {
      this.LightCorrelation = DoCalculateCorrelation(Light, lightProfile);
      this.HeavyCorrelation = DoCalculateCorrelation(Heavy, heavyProfile);
    }

    private double DoCalculateCorrelation(PeakList<Peak> Light, List<Peak> lightProfile)
    {
      double[] real = (from p in Light
                       select p.Intensity).ToArray();
      double[] theo = (from p in lightProfile
                       select p.Intensity).ToArray();

      return StatisticsUtils.CosineAngle(real, theo);

      //double sumReal = real.Sum();

      //if (sumReal == 0)
      //{
      //  return 0;
      //}

      //real = (from r in real
      //        let s = r / sumReal
      //        select s).ToArray();

      //double[] pro = theo.Take(Light.Count).ToArray();
      //double sumPro = pro.Sum();
      //pro = (from p in pro
      //       let s = p / sumPro
      //       select s).ToArray();

      //return ScienceUtils.Correl(real, pro);
    }

    private double DoCalculateIntensity(List<Peak> profile, PeakList<Peak> light)
    {
      double result = 0.0;
      double percent = 0.0;
      for (int i = 0; i < light.Count; i++)
      {
        if (light[i].Intensity != 0)
        {
          result += light[i].Intensity;
          percent += profile[i].Intensity;
        }
      }

      if (0.0 == result)
      {
        return 0.0;
      }

      return result / percent;
    }

    public override string ToString()
    {
      if (Light != null)
      {
        return Light.ScanTimes[0].Scan.ToString();
      }
      else
      {
        return base.ToString();
      }
    }
  }
}