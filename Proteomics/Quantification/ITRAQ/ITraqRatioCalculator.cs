using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqRatioCalculator : IITraqRatioCalculator
  {
    private Regex reg = new Regex(@"(11[4-7])\s*/(11[4-7])\s*,\s*(11[4-7])\s*/(11[4-7])");

    private Func<IsobaricItem, double> ratio1func1, ratio1func2, ratio2func1, ratio2func2;

    private string ratio1Header, ratio2Header;

    private Func<IsobaricItem, bool> validator;

    public ITraqRatioCalculator(string definition, Func<IsobaricItem, bool> validator)
    {
      if (definition == null)
      {
        throw new ArgumentNullException("definition");
      }

      Match m = reg.Match(definition);
      if (!m.Success)
      {
        throw new ArgumentException(MyConvert.Format("definition should like 117/114, 116/115 but not {0}", definition), "definition");
      }

      ratio1func1 = GetFunc(m.Groups[1].Value);
      ratio1func2 = GetFunc(m.Groups[2].Value);
      ratio2func1 = GetFunc(m.Groups[3].Value);
      ratio2func2 = GetFunc(m.Groups[4].Value);

      ratio1Header = MyConvert.Format("{0}/{1}", m.Groups[1].Value, m.Groups[2].Value);
      ratio2Header = MyConvert.Format("{0}/{1}", m.Groups[3].Value, m.Groups[4].Value);

      this.validator = validator;
    }

    public ITraqRatioCalculator(string definition)
      : this(definition, (m => true))
    { }

    private Func<IsobaricItem, double> GetFunc(string value)
    {
      var index = int.Parse(value);
      return m => m[index];
    }

    protected virtual string GetRatio1Header()
    {
      return ratio1Header;
    }

    protected virtual string GetRatio2Header()
    {
      return ratio2Header;
    }

    protected virtual double GetRatio1(IsobaricItem item)
    {
      return ratio1func1(item) / ratio1func2(item);
    }

    protected virtual double GetRatio2(IsobaricItem item)
    {
      return ratio2func1(item) / ratio2func2(item);
    }

    #region IITraqRatioCalculator Members

    public virtual string GetRatioHeader()
    {
      return GetRatio1Header() + "\t" + GetRatio2Header();
    }

    public virtual string GetRatioValue(IsobaricItem item)
    {
      return MyConvert.Format("{0:0.0000}\t{1:0.0000}", GetRatio1(item), GetRatio2(item));
    }

    public virtual bool Valid(IsobaricItem item)
    {
      return validator(item);
    }

    #endregion
  }
}
