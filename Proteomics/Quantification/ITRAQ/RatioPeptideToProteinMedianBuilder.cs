using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class RatioPeptideToProteinMedianBuilder : IRatioPeptideToProteinBuilder
  {
    #region IRatioPeptideToProteinBuilder Members

    public ITraqProteinRatioItem Calculate(IEnumerable<ITraqPeptideRatioItem> items)
    {
      var ratios = (from item in items select item.Ratio).ToArray();
      return new ITraqProteinRatioItem()
      {
        Ratio = Statistics.Median(ratios)
      };
    }

    public string Name
    {
      get
      {
        return "Median(ratio)";
      }
    }

    #endregion

    public override string ToString()
    {
      return Name;
    }
  }
}
