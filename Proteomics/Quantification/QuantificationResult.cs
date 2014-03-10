using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification
{
  public interface IQuantificationResult
  {
    List<double> Intensities { get; }
    double Error { get; set; }
    double Ratio();
  }

  public abstract class AbstractQuantificationResult : IQuantificationResult
  {
    private readonly List<double> intensities = new List<double>();

    private double error = Double.MaxValue;

    #region IQuantificationResult Members

    public List<double> Intensities
    {
      get { return this.intensities; }
    }

    public double Error
    {
      get { return this.error; }
      set { this.error = value; }
    }

    public abstract double Ratio();

    #endregion
  }

  public class O18QuantificationResult : AbstractQuantificationResult
  {
    public override double Ratio()
    {
      if (Intensities.Count == 2)
      {
        if (Intensities[1] == 0.0)
        {
          return Double.MaxValue;
        }
        else
        {
          return Intensities[0]/Intensities[1];
        }
      }
      else if (Intensities.Count == 3)
      {
        double O18 = Intensities[1] + Intensities[2];
        if (O18 == 0.0)
        {
          return Double.MaxValue;
        }
        else
        {
          return Intensities[0]/O18;
        }
      }
      else
      {
        throw new Exception("Intensities were not assigned or count of intensities not equals to 2 or 3!");
      }
    }
  }

  //public class SilacQuantificationResult : AbstractQuantificationResult
  //{
  //  public RangeLocation RangeLoc { get; set; }

  //  public void AssignIntensity(double sampleIntensity, double referenceIntensity)
  //  {
  //    Intensities.Clear();
  //    Intensities.Add(sampleIntensity);
  //    Intensities.Add(referenceIntensity);
  //  }

  //  public override double Ratio()
  //  {
  //    if (Intensities.Count != 2)
  //    {
  //      throw new Exception("Intensities were not assigned or count of intensities not equals to 2!");
  //    }

  //    if (Intensities[1] == 0.0)
  //    {
  //      return Double.MaxValue;
  //    }
  //    else
  //    {
  //      return Intensities[0]/Intensities[1];
  //    }
  //  }
  //}
}