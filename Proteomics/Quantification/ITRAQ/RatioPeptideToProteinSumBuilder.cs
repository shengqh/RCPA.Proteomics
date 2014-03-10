using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class RatioPeptideToProteinSumBuilder : IRatioPeptideToProteinBuilder
  {
    #region IRatioPeptideToProteinBuilder Members

    public ITraqProteinRatioItem Calculate(IEnumerable<ITraqPeptideRatioItem> items)
    {
      var avgIonInjectionTime = (from item in items
                                 where item.IonInjectTime > 0.0
                                 select item.IonInjectTime).Average();
      foreach (var item in items)
      {
        if (item.IonInjectTime <= 0.0)
        {
          item.IonInjectTime = avgIonInjectionTime;
        }
      }

      return new ITraqProteinRatioItem()
      {
        Ratio = items.Sum(m => m.Sample / m.IonInjectTime) / items.Sum(m => m.Reference / m.IonInjectTime)
      };
    }

    public string Name
    {
      get
      {
        return "Sum(sample) / Sum(reference)";
      }
    }

    #endregion

    public override string ToString()
    {
      return Name;
    }
  }
}
