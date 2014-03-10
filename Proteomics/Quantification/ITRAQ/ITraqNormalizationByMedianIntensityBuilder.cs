using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqNormalizationByMedianIntensityBuilder : AbstractITraqNormalizationBuilder
  {
    protected override double GetIonBase(IsobaricResult tr, Func<IsobaricItem, double> func)
    {
      var ions = (from item in tr
                  let intensity = func(item)
                  where intensity != ITraqConsts.NULL_INTENSITY
                  orderby intensity
                  select intensity).ToList();

      if (ions.Count == 0)
      {
        return ITraqConsts.NULL_INTENSITY;
      }

      return ions[ions.Count / 2];
    }

    public override string ToString()
    {
      return "Normalization by median intensity";
    }
  }
}
