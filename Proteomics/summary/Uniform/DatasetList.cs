using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class DatasetList : List<Dataset>
  {
    private List<Dataset> NoOverlaps;
    public List<List<Dataset>> OverlapBySearchEngine { get; private set; }

    private IConflictProcessor conflictFunc = null;
    private IFalseDiscoveryRateCalculator fdrCalc = null;

    private DatasetListOptions dsOptions;

    public class FdrResult
    {
      public FdrResult()
      {
        this.Fdr = 0.0;
        this.Spectra = new List<IIdentifiedSpectrum>();
        this.ConflictSpectra = new List<IIdentifiedSpectrum>();
      }

      public double Fdr { get; set; }

      public List<IIdentifiedSpectrum> Spectra { get; set; }

      public List<IIdentifiedSpectrum> ConflictSpectra { get; set; }
    }

    public void InitFromOptions(DatasetListOptions dsOptions, IProgressCallback progress, string paramFile)
    {
      this.Clear();

      this.conflictFunc = dsOptions.Options.GetConflictFunc();

      this.fdrCalc = dsOptions.Options.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();
      IFilter<IIdentifiedSpectrum> decoyFilter = null;
      if (dsOptions.Options.FalseDiscoveryRate.FilterByFdr)
      {
        decoyFilter = dsOptions.Options.GetDecoySpectrumFilter();
      }

      this.dsOptions = dsOptions;

      long afterFirstMemory = 0;
      DateTime afterFirstTime = DateTime.Now;
      var totalCount = dsOptions.Sum(l => l.PathNames.Count);
      var usedCount = 0;

      for (int i = 0; i < dsOptions.Count; i++)
      {
        var m = dsOptions[i];
        var builder = m.GetBuilder();

        builder.Progress = progress;

        Dataset ds = new Dataset(m);

        //首先，获取所有通过了固定筛选标准的谱图。
        ds.Spectra = builder.ParseFromSearchResult();
        ds.PSMPassedFixedCriteriaCount = ds.Spectra.Count;

        if (dsOptions.Options.FalseDiscoveryRate.FilterByFdr)
        {
          //对每个谱图设置是否来自诱饵库
          progress.SetMessage("Assigning decoy information...");
          DecoyPeptideBuilder.AssignDecoy(ds.Spectra, decoyFilter);
          var decoyCount = ds.Spectra.Count(l => l.FromDecoy);
          if (decoyCount == 0)
          {
            throw new Exception(string.Format("No decoy protein found at dataset {0}, make sure the protein access number parser and the decoy pattern are correctly defined!", m.Name));
          }

          progress.SetMessage("{0} decoys out of {1} hits found", decoyCount, ds.Spectra.Count);

          ds.BuildSpectrumBin();
          ds.CalculateCurrentFdr();
          ds.PushCurrentOptimalResults(string.Format("Before maximum peptide fdr {0}", dsOptions.Options.FalseDiscoveryRate.MaxPeptideFdr));

          progress.SetMessage("Filtering by maximum peptide fdr {0} ...", dsOptions.Options.FalseDiscoveryRate.MaxPeptideFdr);
          ds.FilterByFdr(dsOptions.Options.FalseDiscoveryRate.MaxPeptideFdr);
          ds.Spectra = ds.GetUnconflictedOptimalSpectra();
          ds.BuildSpectrumBin();
          ds.CalculateCurrentFdr();
          ds.PushCurrentOptimalResults(string.Format("After maximum peptide fdr {0}", dsOptions.Options.FalseDiscoveryRate.MaxPeptideFdr));
        }

        this.Add(ds);

        if (i == 0)
        {
          afterFirstMemory = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
          afterFirstTime = DateTime.Now;
        }
        else
        {
          usedCount += m.PathNames.Count;

          long currMemory = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
          double averageCost = (double)(currMemory - afterFirstMemory) / usedCount;
          double estimatedCost = afterFirstMemory + averageCost * totalCount;

          DateTime currTime = DateTime.Now;
          var averageTime = currTime.Subtract(afterFirstTime).TotalMinutes / usedCount;
          var finishTime = afterFirstTime.AddMinutes(averageTime * (totalCount - dsOptions[0].PathNames.Count));
          progress.SetMessage("{0}/{1} datasets, cost {2}M, avg {3:0.0}M, need {4:0.0}M, will finish at {5:MM-dd HH:mm:ss}", (i + 1), dsOptions.Count, currMemory, averageCost, estimatedCost, finishTime);
        }
      }

      //初始化实验列表
      this.ForEach(m => m.InitExperimentals());

      if (dsOptions.Count > 1)
      {
        if (dsOptions.Options.KeepTopPeptideFromSameEngineButDifferentSearchParameters)
        {
          //合并/删除那些相同搜索引擎，不同参数得到的结果。
          ProcessDatasetFromSameEngine(progress, (peptides, score) => IdentifiedSpectrumUtils.KeepTopPeptideFromSameEngineDifferentParameters(peptides, score), false);
        }
        else
        {
          ProcessDatasetFromSameEngine(progress, (peptides, score) => IdentifiedSpectrumUtils.KeepUnconflictPeptidesFromSameEngineDifferentParameters(peptides, score), true);
        }

        //初始化不同搜索引擎搜索的dataset之间的overlap关系。
        this.OverlapBySearchEngine = FindOverlap((m1, m2) => m1.Options.SearchEngine != m2.Options.SearchEngine);


        //初始化没有交集的dataset
        var overlaps = new HashSet<Dataset>(from m in OverlapBySearchEngine
                                            from s in m
                                            select s);
        this.NoOverlaps = this.Where(m => !overlaps.Contains(m)).ToList();

        if (OverlapBySearchEngine.Count > 0 && dsOptions.Options.FalseDiscoveryRate.FilterByFdr)
        {
          //根据最大的fdr进行筛选。
          progress.SetMessage("Filtering PSMs by maximum fdr {0}, considering multiple engine overlap...", dsOptions.Options.FalseDiscoveryRate.MaxPeptideFdr);
          var realFdr = this.FilterByFdr(dsOptions.Options.FalseDiscoveryRate.MaxPeptideFdr);
          if (realFdr.ConflictSpectra.Count > 0)
          {
            new MascotPeptideTextFormat(UniformHeader.PEPTIDE_HEADER).WriteToFile(Path.ChangeExtension(paramFile, ".conflicted.peps"), realFdr.ConflictSpectra);
          }

          //保留每个dataset的spectra为筛选后的结果，以用于后面的迭代。
          this.ForEach(m =>
          {
            m.Spectra = m.GetUnconflictedOptimalSpectra();
          });
        }
      }
      else
      {
        this.NoOverlaps = new List<Dataset>(this);
        this.OverlapBySearchEngine = new List<List<Dataset>>();
      }
    }

    public void BuildSpectrumBin()
    {
      //把谱图分类，以便计算fdr。
      this.ForEach(m =>
      {
        m.OptimalResults = m.Options.Parent.Classification.BuildSpectrumBin(m.Spectra);
      });
    }

    private void ProcessDatasetFromSameEngine(IProgressCallback progress, Action<List<IIdentifiedSpectrum>, IScoreFunction> process, bool hasDuplicatedSpectrum)
    {
      Func<Dataset, Dataset, bool> preFunc = (m1, m2) => m1.Options.SearchEngine == m2.Options.SearchEngine;

      var overlaps = FindOverlap(preFunc);

      if (overlaps.Count > 0)
      {
        progress.SetMessage("Filtering spectra from same engine but different search parameters ...");

        foreach (var dsList in overlaps)
        {
          var spectra = dsList.GetSpectra();

          Console.Write(spectra.Count);

          process(spectra, dsList.First().Options.ScoreFunction);

          Console.WriteLine(" -> {0}", spectra.Count);

          ResetClassificationTag(dsList, spectra);

          dsList[0].Spectra = spectra;
          dsList[0].HasDuplicatedSpectrum = hasDuplicatedSpectrum;

          //删除其余dataset。
          for (int i = 1; i < dsList.Count; i++)
          {
            this.Remove(dsList[i]);
          }


          //new MascotPeptideTextFormat().WriteToFile(@"U:\sqh\hppp\orbitrap\summary\overlap\new.merged.peptides", spectra);
        }
        progress.SetMessage("Filtering spectra from same engine but different search parameters finished.");
      }
    }

    //将所有spectra的ClassificationTag设置为合并后的tag，用于classification。
    private static void ResetClassificationTag(List<Dataset> dsList, List<IIdentifiedSpectrum> spectra)
    {
      var tags = StringUtils.Merge(from t in dsList select t.Options.Name, "/");
      spectra.ForEach(m => m.ClassificationTag = tags);
    }

    private List<List<Dataset>> FindOverlap(Func<Dataset, Dataset, bool> preFunc)
    {
      var result = new List<List<Dataset>>();

      for (int i = 0; i < this.Count; i++)
      {
        for (int j = i + 1; j < this.Count; j++)
        {
          //只考虑相同搜索引擎结果
          if (!preFunc(this[i], this[j]))
          {
            continue;
          }

          //原始数据有交集，意味着不同搜索参数
          if (this[i].Experimentals.Intersect(this[j].Experimentals).Count() > 0)
          {
            List<Dataset> find = null;
            //是否已经与其他dataset有了交集？
            foreach (var ds in result)
            {
              if (ds.Contains(this[i]) || ds.Contains(this[j]))
              {
                find = ds;
                break;
              }
            }

            if (null == find)
            {
              find = new List<Dataset>();
              result.Add(find);
            }

            if (!find.Contains(this[i]))
            {
              find.Add(this[i]);
            }
            if (!find.Contains(this[j]))
            {
              find.Add(this[j]);
            }
          }
        }
      }

      return result;
    }

    public FdrResult FilterByFdr(double maxFdr)
    {
      if (this.Count == 0)
      {
        return new FdrResult();
      }

      this.ForEach(m => m.FilterByFdr(maxFdr));

      if (OverlapBySearchEngine.Count == 0)
      {
        return new FdrResult() { Fdr = maxFdr, Spectra = this.GetUnconflictedOptimalSpectra() };
      }

      double nextFdr = maxFdr;
      while (true)
      {
        FdrResult result = DoCalculateOverlappedPeptideFdr(nextFdr);
        if (result.Fdr <= maxFdr)
        {
          return result;
        }
        nextFdr = nextFdr - FalseDiscoveryRateUtils.CalculateStepFdr(nextFdr);
        this.ForEach(m => m.FilterByFdr(nextFdr));
      }
    }

    //如果两个PSMs的peptide没有一个相同，则认为是conflict。
    private bool IsEngineConflict(List<IIdentifiedSpectrum> spectra)
    {
      for (int i = 1; i < spectra.Count; i++)
      {
        if (!spectra[i].SequenceEquals(spectra[0]))
        {
          return true;
        }
      }

      return false;
    }

    private FdrResult DoCalculateOverlappedPeptideFdr(double individualFdr)
    {
      FdrResult result = new FdrResult();

      List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>();

      int decoyCount = 0;
      int targetCount = 0;

      foreach (var dsList in OverlapBySearchEngine)
      {
        List<IIdentifiedSpectrum> peps = dsList.GetUnconflictedOptimalSpectra();

        //根据实验文件名分类。这样可以降低需要比较的集合大小。
        var expGroups = peps.GroupBy(m => m.Query.FileScan.Experimental);

        foreach (var group in expGroups)
        {
          //根据scan分类。
          var spGroup = group.GroupBy(m => m.Query.FileScan.FirstScan);
          foreach (var sp in spGroup)
          {
            var lst = sp.ToList();
            if (lst.Count > 1 && IsEngineConflict(lst))
            {
              result.ConflictSpectra.AddRange(lst);

              var spectrum = conflictFunc.Process(lst);
              lst.Clear();
              if (spectrum != null)
              {
                lst.AddRange(spectrum);
              }
            }

            if (lst.Count >= dsOptions.Options.MinimumEngineAgreeCount)
            {
              spectra.AddRange(lst);
              if (lst[0].FromDecoy)
              {
                decoyCount++;
              }
              else
              {
                targetCount++;
              }
            }
          }
        }
      }

      if (dsOptions.Options.MinimumEngineAgreeCount <= 1)
      {
        var noOverlapSpectra = NoOverlaps.GetOptimalSpectra();
        foreach (var s in noOverlapSpectra)
        {
          if (s.FromDecoy)
          {
            decoyCount++;
          }
          else
          {
            targetCount++;
          }
        }
        spectra.AddRange(noOverlapSpectra);
      }

      result.Spectra = spectra;
      result.Fdr = fdrCalc.Calculate(decoyCount, targetCount);

      return result;
    }

    /// <summary>
    /// 获取各个dataset的OptimalResult中的spctrum个数的和。
    /// </summary>
    /// <returns></returns>
    public int GetOptimalSpectrumCount()
    {
      return (from ds in this
              from or in ds.OptimalResults
              select or.Spectra.Count).Sum();
    }

    /// <summary>
    /// 过滤各个Dataset，使得其OptimalResult中只保留与指定集合的交集。当只有一个Dataset时，
    /// 直接将该指定谱图集合赋值给该Dataset并重建SpectrumBin。
    /// </summary>
    /// <param name="spectra"></param>
    public void KeepOptimalResultInSetOnly(IEnumerable<IIdentifiedSpectrum> spectra)
    {
      if (Count == 1)
      {
        this[0].OptimalResults = this[0].Options.Parent.Classification.BuildSpectrumBin(spectra);
        this[0].CalculateCurrentFdr();
      }
      else
      {
        if (spectra is HashSet<IIdentifiedSpectrum>)
        {
          this.ForEach(m => m.KeepOptimalResultInSetOnly(spectra as HashSet<IIdentifiedSpectrum>));
        }
        else
        {
          var set = new HashSet<IIdentifiedSpectrum>(spectra);
          this.ForEach(m => m.KeepOptimalResultInSetOnly(set));
        }
      }
    }

    public void ClearSpectra()
    {
      this.ForEach(m =>
      {
        m.Spectra = null;
        m.OptimalResults.ForEach(n => n.Spectra = null);
      });
    }
  }
}
