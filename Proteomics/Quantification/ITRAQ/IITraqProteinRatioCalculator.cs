using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IITraqProteinRatioCalculator
  {
    string DatasetName { get; set; }

    string ChannelName { get; set; }

    Func<IsobaricItem, double> GetSample { get; set; }

    Func<IsobaricItem, double> GetReference { get; set; }

    List<ITraqPeptideRatioItem> Calculate(IIdentifiedProteinGroup protein);

    Predicate<IIdentifiedSpectrum> Filter { get; set; }

    //double OutlierEValue { get; set; }

    IOutlierDetector OutlierDetector { get; set; }

    IRatioPeptideToProteinBuilder RatioBuilder { get; set; }
  }
}
