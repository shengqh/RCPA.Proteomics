using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqLogRatioCalculator : ITraqRatioCalculator
  {
    public ITraqLogRatioCalculator(string definition)
      : base(definition)
    { }

    protected override string GetRatio1Header()
    {
      return MyConvert.Format("Log({0})", base.GetRatio1Header());
    }

    protected override string GetRatio2Header()
    {
      return MyConvert.Format("Log({0})", base.GetRatio2Header());
    }

    protected override double GetRatio1(IsobaricItem item)
    {
      return Math.Log(base.GetRatio1(item));
    }

    protected override double GetRatio2(IsobaricItem item)
    {
      return Math.Log(base.GetRatio2(item));
    }
  }
}
