using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformSpectrumProteinFdrBuilder : ProgressClass, IIdentifiedSpectrumBuilder
  {
    private Func<Dataset, List<OptimalResultCondition>> f1 = m => (from oi in m.OptimalResults select oi.Condition).ToList();

    private Func<Dataset, OptimalResultCondition, OptimalItem> f2 = (m, n) => m.GetConditionItem(n);

    public BuildSummaryOptions Options { get; private set; }

    public DatasetList BuildResult { get; private set; }

    #region IIdentifiedSpectrumBuilder Members

    public List<IIdentifiedSpectrum> Build(string parameterFile)
    {
      Options = new BuildSummaryOptions(parameterFile);
      Options.DatasetList.RemoveDisabled();

      IIdentifiedProteinBuilder proteinBuilder = new IdentifiedProteinBuilder();

      IIdentifiedProteinGroupBuilder groupBuilder = new IdentifiedProteinGroupBuilder();

      IIdentifiedProteinGroupFilter conFilter = Options.Database.GetNotContaminationDescriptionFilter(this.Progress);

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

        sw.WriteLine(OptimalFilteredItem.GetHeader());

        var uniqueFilter = new IdentifiedProteinGroupUniquePeptideCountFilter(2);

        OptimalFilteredItem finalItem = null;

        int fdrPeptideCount = Options.FalseDiscoveryRate.FdrPeptideCount > 2 ? Options.FalseDiscoveryRate.FdrPeptideCount : 2;
        double firstStepFdr = Options.FalseDiscoveryRate.MaxPeptideFdr;
        bool bFirst = true;
        for (int curPeptideCount = fdrPeptideCount; curPeptideCount >= 2; curPeptideCount--)
        {
          //重新根据保留的Spectra构建SpectrumBin。
          if (!bFirst)
          {
            BuildResult.BuildSpectrumBin();
          }
          bFirst = false;

          var curItem = new OptimalFilteredItem();

          IIdentifiedProteinGroupFilter groupFilter;

          bool bNeedFirstStep = curPeptideCount > 2;
          if (bNeedFirstStep)
          {
            Progress.SetMessage("Filtering PSMs by protein fdr {0}, unique peptide count >= 2 and peptide count >= {1} using peptide fdr {2}...", Options.FalseDiscoveryRate.FdrValue, curPeptideCount, firstStepFdr);

            //第一步，根据UniquePeptideCount和PeptideCount进行筛选，得到满足蛋白质Fdr要求所对应的肽段fdr。
            var countFilter = new IdentifiedProteinGroupPeptideCountFilter(curPeptideCount);

            if (conFilter != null)
            {
              groupFilter = new AndIdentifiedProteinGroupFilter(new IIdentifiedProteinGroupFilter[] { conFilter, uniqueFilter, countFilter });
            }
            else
            {
              groupFilter = new AndIdentifiedProteinGroupFilter(new IIdentifiedProteinGroupFilter[] { uniqueFilter, countFilter });
            }

            curItem.Unique2CountResult = proteinCalc.GetOptimalResultForGroupFilter(BuildResult, firstStepFdr, Options.FalseDiscoveryRate.FdrValue, groupFilter);
            firstStepFdr = curItem.Unique2CountResult.PeptideFdr;

            //只保留没有被通过筛选的蛋白质包含的PSMs。
            BuildResult.KeepOptimalResultInSetOnly(new HashSet<IIdentifiedSpectrum>(curItem.Unique2CountResult.RejectedSpectra));

            GC.Collect();
            GC.WaitForPendingFinalizers();
          }
          else
          {
            curItem.Unique2CountResult = new ProteinFdrFilteredItem();
          }

          Progress.SetMessage("Filtering PSMs by protein fdr {0}, unique peptide count >= 2 using peptide fdr {1}...", Options.FalseDiscoveryRate.FdrValue, firstStepFdr);

          //第二步，根据UniquePeptideCount进行筛选，计算得到满足给定蛋白质fdr的结果。
          double secondStepFdr = bNeedFirstStep ? Options.FalseDiscoveryRate.MaxPeptideFdr : firstStepFdr;

          if (conFilter != null)
          {
            groupFilter = new AndIdentifiedProteinGroupFilter(new IIdentifiedProteinGroupFilter[] { conFilter, uniqueFilter });
          }
          else
          {
            groupFilter = uniqueFilter;
          }

          curItem.Unique2Result = proteinCalc.GetOptimalResultForGroupFilter(BuildResult, secondStepFdr, Options.FalseDiscoveryRate.FdrValue, groupFilter);

          //只保留没有被通过筛选的蛋白质包含的PSMs。
          BuildResult.KeepOptimalResultInSetOnly(new HashSet<IIdentifiedSpectrum>(curItem.Unique2Result.RejectedSpectra));
          GC.Collect();
          GC.WaitForPendingFinalizers();

          curItem.Unique1Result = FilterOneHitWonders(conFilter, proteinCalc);

          GC.Collect();
          GC.WaitForPendingFinalizers();

          sw.WriteLine(curItem.ToString());

          if (finalItem == null || finalItem.TotalProteinCount < curItem.TotalProteinCount)
          {
            finalItem = curItem;
          }

          curItem = null;

          GC.Collect();
          GC.WaitForPendingFinalizers();

          Console.WriteLine(MyConvert.Format("Filtering PSMs by protein fdr {0}, unique peptide count >= 2 and peptide count >= {1} using peptide fdr {2}...cost {3}.", Options.FalseDiscoveryRate.FdrValue, curPeptideCount, firstStepFdr, SystemUtils.CostMemory()));
        }

        Progress.SetMessage("Filtering PSMs by protein fdr {0} finished, free memory...", Options.FalseDiscoveryRate.FdrValue);

        if (finalItem != null)
        {
          sw.WriteLine();
          sw.WriteLine("Final result : ");

          WriteScoreMap(sw, BuildResult, finalItem.Unique2CountResult);
          WriteScoreMap(sw, BuildResult, finalItem.Unique2Result);
          WriteScoreMap(sw, BuildResult, finalItem.Unique1Result);

          BuildResult.ClearSpectra();
          GC.Collect();
          GC.WaitForPendingFinalizers();

          return finalItem.GetSpectra();
        }
        else
        {
          return new List<IIdentifiedSpectrum>();
        }
      }
    }

    #endregion

    protected ProteinFdrFilteredItem FilterOneHitWonders(IIdentifiedProteinGroupFilter conFilter, UniformProteinFdrOptimalResultCalculator proteinCalc)
    {
      var oldLevel = Options.FalseDiscoveryRate.FdrLevel;
      try
      {
        List<IIdentifiedProteinGroupFilter> filters = new List<IIdentifiedProteinGroupFilter>();
        if (conFilter != null)
        {
          filters.Add(conFilter);
        }

        if (Options.FalseDiscoveryRate.FilterOneHitWonder && Options.FalseDiscoveryRate.MinOneHitWonderPeptideCount > 1)
        {
          filters.Add(new IdentifiedProteinGroupPeptideCountFilter(Options.FalseDiscoveryRate.MinOneHitWonderPeptideCount));
        }

        AndIdentifiedProteinGroupFilter groupFilter = new AndIdentifiedProteinGroupFilter(filters);
        
        Progress.SetMessage("Filtering PSMs by protein fdr {0} using unique peptide fdr {0} ...", Options.FalseDiscoveryRate.FdrValue);

        Options.FalseDiscoveryRate.FdrLevel = FalseDiscoveryRateLevel.UniquePeptide;

        return proteinCalc.GetOptimalResultForGroupFilter(BuildResult, Options.FalseDiscoveryRate.FdrValue, Options.FalseDiscoveryRate.FdrValue, groupFilter);
      }
      finally
      {
        Options.FalseDiscoveryRate.FdrLevel = oldLevel;
      }
    }

    private void WriteScoreMap(StreamWriter sw, DatasetList BuildResult, ProteinFdrFilteredItem item)
    {
      if (item.ProteinCount == 0)
      {
        return;
      }

      BuildResult.BuildSpectrumBin();
      BuildResult.KeepOptimalResultInSetOnly(item.AcceptedSpectra);

      sw.WriteLine(MyConvert.Format("Filtering condition = {0}, PeptideFdr = {1}", item.ProteinCondition, item.PeptideFdr));
      BuildResult.ForEach(ds =>
      {
        sw.WriteLine("Dataset {0}", ds.Options.Name);
        OptimalResultConditionUtils.WriteSpectrumBin(sw, ds, f1, f2);
      });
      sw.WriteLine();
    }

  }
}
