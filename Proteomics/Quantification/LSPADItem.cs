using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification
{
  public class LSPADItem
  {
    public double LogRatio { get; set; }
    public double Intensity { get; set; }
    public object Tag { get; set; }
    public double PValue { get; set; }
    public double Mean { get; set; }
    public double SD { get; set; }

    public static void CalculatePValue(List<LSPADItem> items)
    {
      items.Sort((m1, m2) => m1.Intensity.CompareTo(m2.Intensity));
      var oneSix = items.Count / 6;

      List<double> ratios = new List<double>();
      for (int i = 0; i < oneSix; i++)
      {
        ratios.Add(items[i].LogRatio);
      }
      int lastOne = 0;
      int firstOne = oneSix - 1;

      for (int i = 0; i < items.Count; i++)
      {
        int first = Math.Min(i + oneSix, items.Count);
        int last = Math.Max(i - oneSix, 1);

        for (int a = lastOne; a < last; a++)
        {
          ratios.RemoveAt(0);
        }

        for (int a = firstOne; a < first; a++)
        {
          ratios.Add(items[a].LogRatio);
        }

        var acc = new MeanStandardDeviation(ratios);
        items[i].Mean = acc.Mean;
        items[i].SD = acc.StdDev;

        Normal nd = new Normal(acc.Mean, acc.StdDev);
        items[i].PValue = nd.CumulativeDistribution(items[i].LogRatio);
        items[i].PValue = Math.Min(items[i].PValue, 1 - items[i].PValue);
      }
    }
  }
}
