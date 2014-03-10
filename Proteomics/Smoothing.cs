using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics
{
  public interface ISmoothing
  {
    void Smooth(double[] values);
  }

  public class SavitzkyGolaySmoothing : ISmoothing
  {
    private static Dictionary<int, double[]> coefficientsMap;
    private readonly double[] coefficients;
    private readonly double coefficientsSum;

    public SavitzkyGolaySmoothing(int filterWidth)
    {
      if (null == coefficientsMap)
      {
        initCoefficients();
      }

      if (!coefficientsMap.ContainsKey(filterWidth))
      {
        var sb = new StringBuilder();
        foreach (int key in coefficientsMap.Keys)
        {
          if (sb.Length == 0)
          {
            sb.Append(key);
          }
          else
          {
            sb.Append("," + key);
          }
        }
        throw new ArgumentOutOfRangeException(MyConvert.Format("FilterWidth {0} out of range [{1}]", filterWidth, sb));
      }

      this.coefficients = coefficientsMap[filterWidth];
      this.coefficientsSum = 0.0;
      foreach (double c in this.coefficients)
      {
        this.coefficientsSum += c;
      }
    }

    #region ISmoothing Members

    public void Smooth(double[] values)
    {
      if (values.Length <= this.coefficients.Length)
      {
        return;
      }

      int startIndex = this.coefficients.Length/2;
      int endIndex = values.Length - startIndex;
      for (int i = startIndex; i < endIndex; i++)
      {
        double result = 0.0;

        for (int j = 0; j < this.coefficients.Length; j++)
        {
          result += this.coefficients[j]*values[i + j - startIndex];
        }
        values[i] = result/this.coefficientsSum;
      }
    }

    #endregion

    private static void initCoefficients()
    {
      coefficientsMap = new Dictionary<int, double[]>();
      coefficientsMap.Add(5, new double[] {-3, 12, 17, 12, -3});
      coefficientsMap.Add(7, new double[] {-2, 3, 6, 7, 6, 3, -2});
      coefficientsMap.Add(9, new double[] {-21, 14, 39, 54, 59, 54, 39, 14, -21});
      coefficientsMap.Add(11, new double[] {-36, 9, 44, 69, 84, 89, 84, 69, 44, 9, -36});
    }
  }
}