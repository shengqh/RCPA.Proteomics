using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.O18
{
  public interface IO18QuantificationOption
  {
    IFileFormat<O18QuantificationSummaryItem> GetIndividualFileFormat();
    IGetRatioIntensity Func { get; }
    IProteinRatioCalculator GetProteinRatioCalculator();
    bool HasPeptideRatio(IIdentifiedSpectrum ann);
    bool IsPeptideRatioValid(IIdentifiedSpectrum ann);
    string GetRatioFile(IIdentifiedSpectrum mph, string summaryFileDirectory, string defaultDetailDirectory);
  }
}
