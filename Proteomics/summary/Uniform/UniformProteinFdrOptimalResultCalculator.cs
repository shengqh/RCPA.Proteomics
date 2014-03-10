using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Seq;
using System.Text.RegularExpressions;
using RCPA.Gui;
using System.IO;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class UniformProteinFdrOptimalResultCalculator : ProgressClass
  {
    private IFalseDiscoveryRateCalculator fdrCalc;

    private IIdentifiedProteinBuilder proteinBuilder;

    private IIdentifiedProteinGroupBuilder groupBuilder;

    private IIdentifiedProteinGroupFilter decoyFilter;

    public UniformProteinFdrOptimalResultCalculator(IFalseDiscoveryRateCalculator fdrCalc, IIdentifiedProteinGroupFilter decoyFilter)
    {
      this.fdrCalc = fdrCalc;

      this.decoyFilter = decoyFilter;

      this.proteinBuilder = new IdentifiedProteinBuilder();

      this.groupBuilder = new IdentifiedProteinGroupBuilder();
    }

    public ProteinFdrFilteredItem GetOptimalResultForGroupFilter(DatasetList dsList, double initFdr, double maxProteinFdr, IIdentifiedProteinGroupFilter groupFilter)
    {
      double curFdr = initFdr;

      var result = new ProteinFdrFilteredItem();
      result.PeptideBeforeFdr = dsList.GetOptimalSpectrumCount();

      while (true)
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        string condition = groupFilter == null ? "[]" : "[" + groupFilter.FilterCondition + "]";
        string task = MyConvert.Format("Filtering {0} protein ... PeptideFdr={1:0.0000}", condition, curFdr);
        Progress.SetMessage(task);

        GC.Collect();
        GC.WaitForPendingFinalizers();

        var filteredSpectra = dsList.FilterByFdr(curFdr).Spectra;
        filteredSpectra.TrimExcess();
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Progress.SetMessage(task + ", building protein list from peptides...");
        List<IIdentifiedProtein> proteins = proteinBuilder.Build(filteredSpectra);

        Progress.SetMessage(task + ", building protein group from protein list...");
        List<IIdentifiedProteinGroup> groups = groupBuilder.Build(proteins);

        List<IIdentifiedProteinGroup> filteredGroups = groupFilter == null ? groups : groups.FindAll(g => groupFilter.Accept(g));

        Progress.SetMessage(task + ", calculating protein fdr...");
        double proteinFdr = CalculateProteinGroupFdr(filteredGroups);

        if (proteinFdr <= maxProteinFdr)
        {
          //using (StreamWriter sw = new StreamWriter(@"e:\temp\protein.txt", true))
          //{
          //  sw.WriteLine(task + " kept proteins");
          //  foreach (var g in filteredGroups)
          //  {
          //    foreach (var p in g)
          //    {
          //      sw.WriteLine(p.Name);
          //    }
          //  }
          //  sw.WriteLine();
          //}

          result.ProteinCondition = condition;
          result.PeptideFdr = curFdr;
          result.ProteinFdr = proteinFdr;
          result.ProteinCount = filteredGroups.Count;

          Progress.SetMessage(task + ", accepted, processing corresponding PSMs...");
          foreach (IIdentifiedProteinGroup group in filteredGroups)
          {
            result.AcceptedSpectra.UnionWith(group[0].GetSpectra());
          }
          result.AcceptedSpectra.TrimExcess();


          //删除已经被包含在通过筛选的group对应的spectra
          filteredSpectra.RemoveAll(m => result.AcceptedSpectra.Contains(m));

          //删除对应于已通过筛选的蛋白质的spectra（但未通过初始的肽段筛选）
          var proteinList = new HashSet<string>((from g in filteredGroups
                                                 from p in g
                                                 select p.Name).Distinct());
          filteredSpectra.RemoveAll(m =>
          {
            foreach (var pep in m.Peptides)
            {
              foreach (var p in pep.Proteins)
              {
                if (proteinList.Contains(p))
                {
                  return true;
                }
              }
            }
            return false;
          });

          filteredSpectra.TrimExcess();
          result.RejectedSpectra = filteredSpectra;

          List<IIdentifiedProtein> rejectProteins = proteinBuilder.Build(filteredSpectra);
          List<IIdentifiedProteinGroup> rejectGroups = groupBuilder.Build(rejectProteins);
          //using (StreamWriter sw = new StreamWriter(@"e:\temp\protein.txt", true))
          //{
          //  sw.WriteLine(task + " rejected proteins");
          //  foreach (var g in rejectGroups)
          //  {
          //    foreach (var p in g)
          //    {
          //      sw.WriteLine(p.Name);
          //    }
          //  }
          //  sw.WriteLine();
          //}

          GC.Collect();
          GC.WaitForPendingFinalizers();

          Progress.SetMessage(task + " finished.");
          return result;
        }
        else
        {
          double stepFdr = CalculateStepFdr(curFdr);
          curFdr -= stepFdr;
        }
      }
    }

    private double CalculateStepFdr(double initFdr)
    {
      double stepFdr = 0.1;

      while (initFdr <= stepFdr || Math.Abs(initFdr - stepFdr) <= 0.00001)
      {
        stepFdr /= 10;
      }

      return stepFdr;
    }

    private double CalculateProteinGroupFdr(List<IIdentifiedProteinGroup> groups)
    {
      var decoyCount = groups.Count(m => decoyFilter.Accept(m));

      var targetCount = groups.Count - decoyCount;

      return fdrCalc.Calculate(decoyCount, targetCount);
    }
  }
}
