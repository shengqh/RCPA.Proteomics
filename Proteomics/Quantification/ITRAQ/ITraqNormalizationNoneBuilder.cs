using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqNormalizationNoneBuilder : IITraqNormalizationBuilder
  {
    #region IITraqNormalizationBuilder Members

    public bool ByIonInjectionTime { get; set; }

    public void Normalize(IsobaricResult result)
    { }

    #endregion

    public override string ToString()
    {
      return "None";
    }
  }
}
