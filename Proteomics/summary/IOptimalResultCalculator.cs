using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Summary
{
  public interface IOptimalResultCalculator
  {
    string OptimalResultHeader { get; }

    string OptimalResultToString(OptimalResult optimalResult);

    double FdrValue { get; set; }

    List<IIdentifiedSpectrum> Calculate(List<IIdentifiedSpectrum> peptides, OptimalResult optimalResult, bool hasDuplicatedSpectrum);
  }
}
