using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IITraqNormalizationBuilder
  {
    bool ByIonInjectionTime { get; set; }

    void Normalize(IsobaricResult result);
  }
}
