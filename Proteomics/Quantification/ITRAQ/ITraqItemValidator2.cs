using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class EmptyValidator : IITraqItemValidator
  {
    #region IITraqItemValidator Members

    public void Validate(IEnumerable<IsobaricItem> items)
    {
      foreach (var item in items)
      {
        item.Valid = true;
        item.ValidProbability = 1.0;
      }
    }

    #endregion
  }

  public class ITraqItemValidator2 : IITraqItemValidator
  {
    private IsobaricIndex refFunc1;
    private IsobaricIndex refFunc2;
    private double minProbability;

    public ITraqItemValidator2(IsobaricIndex refFunc1, IsobaricIndex refFunc2, double minProbability)
    {
      this.refFunc1 = refFunc1;
      this.refFunc2 = refFunc2;
      this.minProbability = minProbability;
    }

    #region IITraqItemValidator Members

    public void Validate(IEnumerable<IsobaricItem> items)
    {
      List<double> logratio = new List<double>();
      foreach (var item in items)
      {
        var r1 = refFunc1.GetValue(item);
        var r2 = refFunc2.GetValue(item);

        item.Valid = r1 > ITraqConsts.NULL_INTENSITY && r2 > ITraqConsts.NULL_INTENSITY;
        if (item.Valid)
        {
          logratio.Add(Math.Log(r1 / r2));
        }
      }

      if (logratio.Count > 1)
      {
        MeanStandardDeviation acc = new MeanStandardDeviation(logratio);
        var nd = new Normal(acc.Mean, acc.StdDev);
        foreach (var item in items)
        {
          if (item.Valid)
          {
            var r1 = refFunc1.GetValue(item);
            var r2 = refFunc2.GetValue(item);
            var logr = Math.Log(r1 / r2);
            item.ValidProbability = nd.TwoTailProbability(logr);
            item.Valid = item.ValidProbability >= minProbability;
          }
        }
      }
    }

    #endregion
  }
}
