using System.Collections.Generic;
using RCPA.Gui;
using RCPA.Utils;
using System;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractBeforeFdrSpectrumBuilder : ProgressClass, IIdentifiedSpectrumBuilder
  {
    public AbstractBeforeFdrSpectrumBuilder()
    {
    }

    public AbstractBeforeFdrSpectrumBuilder(IProgressCallback progress)
    {
      Progress = progress;
    }

    #region IIdentifiedSpectrumBuilder Members

    public abstract IdentifiedSpectrumBuilderResult Build(string parameterFile);

    #endregion
  }
}