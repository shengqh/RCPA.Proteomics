using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractIdentifiedSpectrumBuilder : ProgressClass, IIdentifiedSpectrumBuilder
  {
    #region IIdentifiedSpectrumBuilder Members

    public BuildSummaryOptions Options { get; private set; }

    public IdentifiedSpectrumBuilderResult Build(string parameterFile)
    {
      Options = new BuildSummaryOptions(parameterFile);
      Options.DatasetList.RemoveDisabled();

      return DoBuild(parameterFile);
    }

    protected abstract IdentifiedSpectrumBuilderResult DoBuild(string parameterFile);

    #endregion
  }
}
