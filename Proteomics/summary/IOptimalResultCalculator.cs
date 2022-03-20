using System.Collections.Generic;

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
