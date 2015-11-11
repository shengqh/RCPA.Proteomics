using System.Collections.Generic;
using System.IO;
using RCPA.Gui;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractFdrSpectrumBuilder : ProgressClass, IIdentifiedSpectrumBuilder
  {
    protected ISummaryBuilderFactory factory;

    public AbstractFdrSpectrumBuilder(ISummaryBuilderFactory factory)
    {
      this.factory = factory;
    }

    #region IIdentifiedSpectrumBuilder Members

    public abstract IdentifiedSpectrumBuilderResult Build(string parameterFile);

    #endregion
  }
}