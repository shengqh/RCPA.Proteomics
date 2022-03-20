﻿using RCPA.Gui;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformSpectrumSimpleProteinFdrBuilder : ProgressClass, IIdentifiedSpectrumBuilder
  {
    private Func<Dataset, List<OptimalResultCondition>> f1 = m => (from oi in m.OptimalResults select oi.Condition).ToList();

    private Func<Dataset, OptimalResultCondition, OptimalItem> f2 = (m, n) => m.GetConditionItem(n);

    public BuildSummaryOptions Options { get; private set; }

    public DatasetList BuildResult { get; private set; }

    #region IIdentifiedSpectrumBuilder Members

    public IdentifiedSpectrumBuilderResult Build(string parameterFile)
    {
      Options = new BuildSummaryOptions(parameterFile);
      Options.DatasetList.RemoveDisabled();

      IIdentifiedProteinBuilder proteinBuilder = new IdentifiedProteinBuilder();
      IIdentifiedProteinGroupBuilder groupBuilder = new IdentifiedProteinGroupBuilder();

      var fdrCalc = Options.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

      BuildResult = new DatasetList();

      //从配置进行初始化
      BuildResult.InitFromOptions(Options.DatasetList, this.Progress, parameterFile);

      var totalCount = BuildResult.GetOptimalSpectrumCount();

      string optimalResultFile = FileUtils.ChangeExtension(parameterFile, ".optimal");
      using (var sw = new StreamWriter(optimalResultFile))
      {
        new OptimalFileTextWriter().WriteToStream(sw, BuildResult);

        UniformProteinFdrOptimalResultCalculator proteinCalc = new UniformProteinFdrOptimalResultCalculator(fdrCalc, Options.GetDecoyGroupFilter())
        {
          Progress = this.Progress
        };

        Progress.SetMessage("Filtering PSMs by protein fdr {0}, using peptide fdr {1}...", Options.FalseDiscoveryRate.FdrValue, Options.FalseDiscoveryRate.MaxPeptideFdr);

        var groupFilter = Options.FalseDiscoveryRate.FilterOneHitWonder ? new IdentifiedProteinGroupSingleWonderPeptideCountFilter(Options.FalseDiscoveryRate.MinOneHitWonderPeptideCount) : null;
        var ret = proteinCalc.GetOptimalResultForGroupFilter(BuildResult, Options.FalseDiscoveryRate.MaxPeptideFdr, Options.FalseDiscoveryRate.FdrValue, groupFilter);

        //只保留通过筛选的蛋白质包含的PSMs。
        BuildResult.KeepOptimalResultInSetOnly(ret.AcceptedSpectra);

        GC.Collect();
        GC.WaitForPendingFinalizers();

        sw.WriteLine("After SimpleProteinFDR filter {0} with condition {1}, required peptide fdr = {2} ", ret.ProteinFdr, ret.ProteinCondition, ret.PeptideFdr);
        BuildResult.ForEach(ds =>
        {
          sw.WriteLine("Dataset {0}", ds.Options.Name);
          OptimalResultConditionUtils.WriteSpectrumBin(sw, ds, f1, f2);
        });

        //sw.WriteLine();
        //new OptimalFileTextWriter().WriteToStream(sw, BuildResult);

        return new IdentifiedSpectrumBuilderResult()
        {
          Spectra = ret.AcceptedSpectra.ToList(),
          PeptideFDR = ret.PeptideFdr,
          ProteinFDR = ret.ProteinFdr
        };
      }
    }

    #endregion
  }
}
