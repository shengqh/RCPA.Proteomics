using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.O18
{
  public interface IO18QuantificationOptions : IPairQuantificationOptions
  {
    IFileFormat<O18QuantificationSummaryItem> GetIndividualFileFormat();
  }
}
