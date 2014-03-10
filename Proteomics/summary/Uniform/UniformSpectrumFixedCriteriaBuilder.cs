using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformSpectrumFixedCriteriaBuilder : ProgressClass, IIdentifiedSpectrumBuilder
  {
    #region IIdentifiedSpectrumBuilder Members

    public List<IIdentifiedSpectrum> Build(string parameterFile)
    {
      BuildSummaryOptions conf = new BuildSummaryOptions(parameterFile);
      conf.DatasetList.RemoveDisabled();

      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();
      conf.DatasetList.ForEach(m =>
      {
        var builder = m.GetBuilder();

        builder.Progress = this.Progress;

        result.AddRange(builder.ParseFromSearchResult());
      });

      return result;
    }

    #endregion
  }
}
