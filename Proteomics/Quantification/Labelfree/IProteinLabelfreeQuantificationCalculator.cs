using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public interface IProteinLabelfreeQuantificationCalculator
  {
    void Calculate(IIdentifiedResult result, Dictionary<string, List<string>> datasets);

    string GetExtension();
  }
}
