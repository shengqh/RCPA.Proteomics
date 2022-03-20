using System;

namespace RCPA.Proteomics.Image
{
  public class ScaleTransform
  {
    private double ratioAB;

    public ScaleTransform(double maxSizeA, double maxSizeB)
    {
      if (maxSizeA <= 0)
      {
        throw new ArgumentOutOfRangeException("maxSizeA", "maxSizeA cannot less than or equals to zero!");
      }

      if (maxSizeB <= 0)
      {
        throw new ArgumentOutOfRangeException("maxSizeB", "maxSizeB cannot less than or equals to zero!");
      }

      this.ratioAB = maxSizeA / maxSizeB;
    }

    public double AtoB(double sizeA)
    {
      return sizeA / ratioAB;
    }

    public double BtoA(double sizeB)
    {
      return sizeB * ratioAB;
    }
  }
}
