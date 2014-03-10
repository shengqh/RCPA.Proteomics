using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public abstract class AbstractITraqNormalizationBuilder : IITraqNormalizationBuilder
  {
    public bool ByIonInjectionTime { get; set; }

    protected abstract double GetIonBase(IsobaricResult tr, Func<IsobaricItem, double> func);

    #region IItraqNormalizationBuilder Members

    public void Normalize(IsobaricResult result)
    {
      if (result.Count == 0)
      {
        return;
      }

      if (ByIonInjectionTime && result.HasIonInjectionTime())
      {
        result.ForEach(m => m.MultiplyIntensityByInjectionTime());
      }

      var defs = result.First().Definition.Items;

      var sums = (from def in defs
                  select GetIonBase(result, m => m[def.Index])).ToArray();

      for (int i = 1; i < sums.Length; i++)
      {
        sums[i] = sums[0] / sums[i];
      }

      result.ForEach(m =>
      {
        for (int i = 1; i < defs.Length; i++)
        {
          m[defs[i].Index] *= sums[i];
        }
      });

      if (ByIonInjectionTime && result.HasIonInjectionTime())
      {
        result.ForEach(m => m.DevideIntensityByInjectionTime());
      }
    }

    #endregion
  }
}
