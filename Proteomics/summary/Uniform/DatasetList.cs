using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Utils;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class DatasetList : List<Dataset>
  {
    private List<Dataset> NoOverlaps;
    private List<List<Dataset>> OverlapBySearchEngine;

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

    public void InitFromOptions(DatasetListOptions dsOptions, IProgressCallback progress)
    {
      this.Clear();

      this.conflictFunc = dsOptions.Options.GetConflictFunc();

      this.fdrCalc = dsOptions.Options.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

      this.dsOptions = dsOptions;

      dsOptions.ForEach(m =>
      {
        var builder = m.GetBuilder();

        builder.Progress = progress;

        Dataset ds = new Dataset(m);

        this.Add(ds);

        //首先，获取所有通过了固定筛选标准的谱图。
        ds.Spectra = builder.ParseFromSearchResult();
      });

      if (dsOptions.Options.FalseDiscoveryRate.FilterByFdr)
      {
        var filter = dsOptions.Options.GetDecoySpectrumFilter();
        this.ForEach(m =>
        {
          //对每个谱图设置是否来自诱饵库
          DecoyPeptideBuilder.AssignDecoy(m.Spectra, filter);

          if (m.Spectra.All(l => !l.FromDecoy))
          {
            Console.Error.WriteLine(string.Format("No decoy protein found at dataset {0}, make sure the protein access number parser and the decoy pattern are correctly defined!", m.Options.Name));
            //throw new Exception(string.Format("No decoy protein found at dataset {0}, make sure the protein access number parser and the decoy pattern are correctly defined!", m.Options.Name));
          }
        });
      }

      //初始化实验列表
      this.ForEach(m => m.InitExperimentals());

      if (dsOptions.Options.KeepTopPeptideFromSameEngineButDifferentSearchParameters)
      {
        //合并/删除那些相同搜索引擎，不同参数得到的结果。
        ProcessDatasetFromSameEngine(progress, m => IdentifiedSpectrumUtils.KeepTopPeptideFromSameEngineDifferentParameters(m), false);
      }
      else
      {
        ProcessDatasetFromSameEngine(progress, m => IdentifiedSpectrumUtils.KeepUnconflictPeptidesFromSameEngineDifferentParameters(m), true);
      }

      //初始化不同搜索引擎搜索的dataset之间的overlap关系。
      this.OverlapBySearchEngine = FindOverlap((m1, m2) => m1.Options.SearchEngine != m2.Options.SearchEngine);


      //初始化没有交集的dataset
      var overlaps = new HashSet<Dataset>(from m in OverlapBySearchEngine
                                          from s in m
                                          select s);
      this.NoOverlaps = this.Where(m => !overlaps.Contains(m)).ToList();
    }

    public void BuildSpectrumBin()
    {
      //把谱图分类，以便计算fdr。并把Spectra清空，以节约空间。
      this.ForEach(m =>
      {
        m.OptimalResults = m.Options.Parent.Classification.BuildSpectrumBin(m.Spectra);
      });
    }

    private void ProcessDatasetFromSameEngine(IProgressCallback progress, Action<List<IIdentifiedSpectrum>> process, bool hasDuplicatedSpectrum)
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

          process(spectra);

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
    public void KeepOptimalResultInSetOnly(HashSet<IIdentifiedSpectrum> spectra)
    {
      if (Count == 1)
      {
        this[0].OptimalResults = this[0].Options.Parent.Classification.BuildSpectrumBin(spectra);
        this[0].CalculateCurrentFdr();
      }
      else
      {
        this.ForEach(m => m.KeepOptimalResultInSetOnly(spectra));
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
