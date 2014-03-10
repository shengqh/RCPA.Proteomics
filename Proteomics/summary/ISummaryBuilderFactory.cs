using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Utils;

namespace RCPA.Proteomics.Summary
{
  public interface ISummaryBuilderFactory : ISummaryBuilderBaseFactory
  {
    AbstractSummaryConfiguration GetConfiguration(string parameterFile);

    IIdentifiedSpectrumBuilder GetSpectrumBuilder(IProgressCallback iProgressCallback);
    
    IScoreFunctions GetScoreFunctions();
  }
}
