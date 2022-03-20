using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public interface IProteinLabelfreeQuantificationCalculator
  {
    void Calculate(IIdentifiedResult result, Dictionary<string, List<string>> datasets);

    string GetExtension();
  }
}
