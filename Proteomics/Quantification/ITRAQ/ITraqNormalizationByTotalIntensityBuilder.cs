using System;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqNormalizationByTotalIntensityBuilder : AbstractITraqNormalizationBuilder
  {
    protected override double GetIonBase(IsobaricResult tr, Func<IsobaricItem, double> func)
    {
      var ions = (from item in tr
                  let intensity = func(item)
                  where intensity != ITraqConsts.NULL_INTENSITY
                  select intensity).ToList();

      if (ions.Count == 0)
      {
        return ITraqConsts.NULL_INTENSITY;
      }

      return ions.Sum();
    }

    public override string ToString()
    {
      return "Normalization by total intensity";
    }
  }
}
