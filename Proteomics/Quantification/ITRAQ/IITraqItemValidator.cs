using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IITraqItemValidator
  {
    void Validate(IEnumerable<IsobaricItem> items);
  }
}
