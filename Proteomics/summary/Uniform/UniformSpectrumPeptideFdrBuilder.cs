using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Utils;
using System.IO;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformSpectrumPeptideFdrBuilder : ProgressClass, IIdentifiedSpectrumBuilder
  {
    public BuildSummaryOptions Options { get; private set; }

    public DatasetList BuildResult { get; private set; }

    #region IIdentifiedSpectrumBuilder Members

    public List<IIdentifiedSpectrum> Build(string parameterFile)
    {
      Options = new BuildSummaryOptions(parameterFile);
      Options.DatasetList.RemoveDisabled();

      var fdrCalc = Options.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

      BuildResult = new DatasetList();

      //从配置进行初始化
      BuildResult.InitFromOptions(Options.DatasetList, this.Progress);

      Progress.SetMessage("Building spectrum bin ...");
      BuildResult.BuildSpectrumBin();

      //根据最大的fdr进行筛选。
      Progress.SetMessage("Filtering spectra by fdr ...");
      var realFdr = BuildResult.FilterByFdr(Options.FalseDiscoveryRate.FdrValue);

      Progress.SetMessage("Keep spectra passed fdr filter ...");
      BuildResult.KeepOptimalResultInSetOnly(realFdr.Spectra);

      string optimalFile = FileUtils.ChangeExtension(parameterFile, ".optimal");

      new OptimalFileTextWriter().WriteToFile(optimalFile, BuildResult);

      Progress.SetMessage("Peptide fdr filter done ...");
      return realFdr.Spectra;
    }

    #endregion
  }
}
