using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using System.IO;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformSpectrumFixedCriteriaBuilder : AbstractIdentifiedSpectrumBuilder
  {
    #region IIdentifiedSpectrumBuilder Members

    protected override IdentifiedSpectrumBuilderResult DoBuild(string parameterFile)
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();
      Options.DatasetList.ForEach(m =>
      {
        var builder = m.GetBuilder();

        builder.Progress = this.Progress;

        m.Spectra = builder.ParseFromSearchResult();

        string optimalFile = FileUtils.ChangeExtension(parameterFile, ".optimal");
        using (var sw = new StreamWriter(optimalFile))
        {
          sw.WriteLine("After fixed criteria, there are {0} PSMs passed the filter.", m.Spectra.Count);
        }

        result.AddRange(m.Spectra);
      });

      return new IdentifiedSpectrumBuilderResult()
      {
        Spectra = result
      };
    }

    #endregion
  }
}
