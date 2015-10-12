using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformSpectrumFixedCriteriaBuilder : AbstractIdentifiedSpectrumBuilder
  {
    #region IIdentifiedSpectrumBuilder Members

    protected override List<IIdentifiedSpectrum> DoBuild(string parameterFile)
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();
      Options.DatasetList.ForEach(m =>
      {
        var builder = m.GetBuilder();

        builder.Progress = this.Progress;

        m.Spectra = builder.ParseFromSearchResult();

        result.AddRange(m.Spectra);
      });

      return result;
    }

    #endregion
  }
}
