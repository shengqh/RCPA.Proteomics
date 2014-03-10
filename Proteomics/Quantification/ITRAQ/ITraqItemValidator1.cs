using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqItemValidator1 : IITraqItemValidator
  {
    private IsobaricIndex refFunc;

    public ITraqItemValidator1(IsobaricIndex refFunc)
    {
      this.refFunc = refFunc;
    }

    #region IITraqItemValidator Members

    public void Validate(IEnumerable<IsobaricItem> items)
    {
      foreach (var item in items)
      {
        item.Valid = refFunc.GetValue(item) > ITraqConsts.NULL_INTENSITY;
      }
    }

    #endregion
  }
}
