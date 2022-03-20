using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmDistiller : AbstractThreadFileProcessor
  {
    private readonly static string DEFAULT = "DEFAULT";

    private SrmOptions options;

    public SrmDistiller() { }

    private string prefix = "";
    private void SetProgressMessage(string msg)
    {
      Progress.SetMessage("{0} {1}", prefix, msg);
    }

    public override IEnumerable<string> Process(string optionFile)
    {
      var targetDirectory = Path.GetDirectoryName(optionFile);

      options = new SrmOptions();
      options.FromXml(XElement.Load(optionFile));

      var keys = options.RawFiles.Keys.ToList();
      foreach (var key in keys)
      {
        if (string.IsNullOrEmpty(options.RawFiles[key]))
        {
          this.options.RawFiles[key] = key;
        }
      }


      List<string> result = new List<string>();

      bool ignoreWarning = false;

      Dictionary<string, List<string>> map = GetGroupMap();

      int totalCount = options.RawFiles.Count;
      int curIndex = 0;

      foreach (var groupKey in map.Keys)
      {
        var mrmFile = GetMrmFileName(targetDirectory, groupKey);

        SrmPairedResult taskResult = new SrmPairedResult()
        {
          Options = this.options
        };

        var files = map[groupKey];
        foreach (var rawFile in files)
        {
          curIndex++;
          this.prefix = MyConvert.Format("Processing {0}/{1}", curIndex, totalCount);
          SetProgressMessage(rawFile);

          Console.WriteLine(rawFile);
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          //读取所有scan信息
          List<SrmScan> mrmIntensities = GetMRMScans(rawFile);

          //mrmIntensities.RemoveAll(m => !( Math.Abs(m.PrecursorMz - 767.4) < 0.1 || Math.Abs(m.PrecursorMz - 770.9) < 0.1));

          SetProgressMessage("building mrm groups ...");

          var mrms = ScanToTransaction(mrmIntensities);

          //将MRMPeptideItem转换为MRMPairedPeptideItem对象
          SrmPairedResult mrmPairs;

          if (File.Exists(options.DefinitionFile))
          {
            mrmPairs = BuildResultBasedOnDefinedFile(ref ignoreWarning, mrms);
          }
          else
          {
            mrmPairs = BuildResultBasedOnRealData(mrms);
          }

          mrmPairs.Options = this.options;
          mrmPairs.PeakPicking();

          //固定阈值筛选。
          var filter = mrmPairs.Options.GetFilter();
          mrmPairs.ForEach(m =>
          {
            m.ProductIonPairs.ForEach(n => n.Enabled = filter.Accept(n));
          });

          taskResult.AddRange(mrmPairs);
        }

        if (options.HasDecoy)
        {
          //设置decoy标签
          taskResult.AssignDecoy();

          //是否有真实的decoy transition
          var hasDecoy = taskResult.Any(m => m.ProductIonPairs.Any(n => n.IsDecoy));
          if (hasDecoy)
          {
            //选取所有transition pairs，这些transition pairs有线性回归比值和回归系数
            var pairs = (from m in taskResult
                         from n in m.ProductIonPairs
                         where n.IsPaired && n.Enabled
                         orderby n.RegressionCorrelation descending
                         select n).ToList();

            //如果有这些pair，那么，那些不成pair的transition全部设置为disabled
            if (pairs.Count > 0)
            {
              taskResult.ForEach(m => m.ProductIonPairs.ForEach(n => n.Enabled = n.Enabled && n.IsPaired));
            }

            //计算每个pair的qvalue
            TargetFalseDiscoveryRateCalculator calc = new TargetFalseDiscoveryRateCalculator();
            var decoyCount = pairs.Count(m => m.IsDecoy);
            var targetCount = pairs.Count - decoyCount;

            for (int d = pairs.Count - 1; d >= 0; d--)
            {
              pairs[d].Qvalue = calc.Calculate(decoyCount, targetCount);
              if (pairs[d].IsDecoy)
              {
                decoyCount--;
              }
              else
              {
                targetCount--;
              }
            }

            //找到qvalue小于给定fdr的pair，qvalue大于该pair的全部设置为disable
            for (int d = pairs.Count - 1; d >= 0; d--)
            {
              if (!pairs[d].IsDecoy && pairs[d].Qvalue <= options.TransitionFdr)
              {
                for (int d2 = d + 1; d2 < pairs.Count - 1; d2++)
                {
                  pairs[d2].Enabled = false;
                }
                break;
              }
            }
          }
        }

        taskResult.ForEach(m =>
        {
          m.CheckEnabled(this.options.OutlierEvalue, this.options.MinValidTransitionPair);
        });

        taskResult.CalculatePeptideRatio();

        taskResult.Sort((m1, m2) => m1.LightPrecursorMZ.CompareTo(m2.LightPrecursorMZ));

        new SrmPairedResultXmlFormat().WriteToFile(mrmFile, taskResult);
        result.Add(mrmFile);
      }

      Progress.End();

      return result;
    }

    private SrmPairedResult BuildResultBasedOnRealData(List<SrmTransition> mrms)
    {
      SrmPairedResult mrmPairs;
      //将Transaction转换为MRMPeptideItem
      var peptides = new List<SrmPeptideItem>();
      foreach (var mrm in mrms)
      {
        bool bFound = false;
        foreach (var pep in peptides)
        {
          if (mrm.IsBrother(pep.ProductIons[0], this.options.RetentionTimeToleranceInSecond, 0.0001))
          {
            pep.ProductIons.Add(mrm);
            bFound = true;
            break;
          }
        }

        if (!bFound)
        {
          SrmPeptideItem item = new SrmPeptideItem();
          item.PrecursorMZ = mrm.PrecursorMZ;
          item.ProductIons.Add(mrm);
          peptides.Add(item);
        }
      }

      //按照precursormz排序
      foreach (var pep in peptides)
      {
        pep.ProductIons.Sort((m1, m2) => m1.ProductIon.CompareTo(m2.ProductIon));
      }

      mrmPairs = new SrmPairedResult();
      mrmPairs.AddRange(from p in peptides
                        orderby p.PrecursorMZ
                        select new SrmPairedPeptideItem(p));

      //合并ProductIon完全一样或者差距相同的Item
      for (int i = 0; i < mrmPairs.Count; i++)
      {
        var iLight = mrmPairs[i];

        for (int j = i + 1; j < mrmPairs.Count; j++)
        {
          var jLight = mrmPairs[j];

          if (Math.Abs(iLight.LightPrecursorMZ - jLight.LightPrecursorMZ) > this.options.MaxPrecursorDistance)
          {
            continue;
          }

          if (iLight.AddPerfectPairedPeptideItem(jLight, this.options.AllowedGaps, this.options.MzTolerance, this.options.RetentionTimeToleranceInSecond))
          {
            mrmPairs.RemoveAt(j);
            break;
          }
        }
      }

      //容错情况下，合并ProductIon完全一样或者差距相同的Item
      for (int i = 0; i < mrmPairs.Count; i++)
      {
        var iLight = mrmPairs[i];
        if (iLight.IsPaired)
        {
          continue;
        }

        for (int j = i + 1; j < mrmPairs.Count; j++)
        {
          var jLight = mrmPairs[j];

          if (jLight.IsPaired)
          {
            continue;
          }

          if (Math.Abs(iLight.LightPrecursorMZ - jLight.LightPrecursorMZ) > this.options.MaxPrecursorDistance)
          {
            continue;
          }

          if (iLight.AddErrorPairedPeptideItem(jLight, this.options.AllowedGaps, this.options.MzTolerance, this.options.RetentionTimeToleranceInSecond))
          {
            mrmPairs.RemoveAt(j);
            break;
          }
        }
      }
      return mrmPairs;
    }

    private SrmPairedResult BuildResultBasedOnDefinedFile(ref bool ignoreWarning, List<SrmTransition> mrms)
    {
      SrmPairedResult mrmPairs;
      //根据预先设定的Transaction成对关系来进行实际配对。
      var definedMrmList = options.GetReader().ReadFromFile(options.DefinitionFile);
      definedMrmList = (from m in definedMrmList
                        orderby m.PrecursorMZ, m.ProductIon
                        select m).ToList();

      HashSet<SrmTransition> unmatched = new HashSet<SrmTransition>();
      foreach (var defineMRM in definedMrmList)
      {
        var matched = mrms.FindAll(m => defineMRM.MzMatch(m, options.MzTolerance)).ToList();

        if (matched.Count == 0)
        {
          unmatched.Add(defineMRM);
        }
        else if (matched.Count == 1)
        {
          defineMRM.CopyData(matched[0]);
          mrms.Remove(matched[0]);
        }
        else
        {
          //超过一个候选者，取retention time最接近的
          var rtDiff = matched.ConvertAll<double>(m => Math.Abs(defineMRM.ExpectCenterRetentionTime - m.ActualCenterRetentionTime)).ToList();
          var minDiff = rtDiff.Min();
          var minIndex = rtDiff.IndexOf(minDiff);

          defineMRM.CopyData(matched[minIndex]);
          mrms.Remove(matched[minIndex]);
        }
      }

      if (mrms.Count > 0)
      {
        StringBuilder err = new StringBuilder();

        if (mrms.Count > 0)
        {
          err.AppendLine("Unmatched real transactions");
          foreach (var um in mrms)
          {
            err.AppendLine(MyConvert.Format("    {0:0.0000}-{1:0.0000} at {2} seconds", um.PrecursorMZ, um.ProductIon, um.ActualCenterRetentionTime));
          }
        }

        Console.WriteLine(err.ToString());

        err.AppendLine();
        err.AppendLine("Continue?");

        if (!ignoreWarning)
        {
          if (MessageBox.Show(err.ToString(), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
          {
            throw new UserTerminatedException();
          }

          if (MessageBox.Show("Ignore all confirmation?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
          {
            ignoreWarning = true;
          }
        }
      }

      mrmPairs = (from m in definedMrmList
                    //where !unmatched.Contains(m)
                  select m).ToList().ToPairedResult();

      mrmPairs.ForEach(m =>
      {
        if (m.IsPaired)
        {
          m.ProductIonPairs.RemoveAll(n => !n.IsPaired || n.Light.Intensities.Count == 0 && n.Heavy.Intensities.Count == 0);
        }
        else
        {
          m.ProductIonPairs.RemoveAll(n => (n.Light != null && n.Light.Intensities.Count == 0) || (n.Heavy != null && n.Heavy.Intensities.Count == 0));
        }
      });

      if (unmatched.Count > 0)
      {
        mrmPairs.ForEach(m =>
          m.ProductIonPairs.RemoveAll(n => unmatched.Contains(n.Light) || unmatched.Contains(n.Heavy)));
      }

      mrmPairs.RemoveAll(m => m.ProductIonPairs.Count == 0);

      return mrmPairs;
    }

    private List<SrmTransition> ScanToTransaction(List<SrmScan> mrmIntensities)
    {
      //赋值所有MRMTransaction
      Dictionary<string, SrmTransition> mrmMap = new Dictionary<string, SrmTransition>();
      SrmTransition action = new SrmTransition(0, 0);
      foreach (var scan in mrmIntensities)
      {
        //合并ProductIon完全一样的Item
        action.PrecursorMZ = scan.PrecursorMz;
        action.ProductIon = scan.ProductMz;
        var key = action.ToString();
        if (!mrmMap.ContainsKey(key))
        {
          mrmMap[key] = action;
          action.Intensities.Add(scan);
          action = new SrmTransition(0, 0);
        }
        else
        {
          mrmMap[key].Intensities.Add(scan);
        }
      }

      //获取检测到的MRM Transaction
      var mrms = (from m in mrmMap.Values
                  orderby m.PrecursorMZ, m.ProductIon
                  select m).ToList();

      //合并时间差距在1分钟，母离子、子离子均在给定阈值范围内的transition。
      for (int i = mrms.Count - 1; i >= 0; i--)
      {
        for (int j = i - 1; j >= 0; j--)
        {
          if (Math.Abs(mrms[i].PrecursorMZ - mrms[j].PrecursorMZ) < options.MzTolerance &&
            Math.Abs(mrms[i].ProductIon - mrms[j].ProductIon) < options.MzTolerance)
          {
            if (mrms[i].LastRetentionTime < mrms[j].FirstRetentionTime)
            {
              if (mrms[j].FirstRetentionTime - mrms[i].LastRetentionTime < 1)
              {
                mrms[j].Intensities.InsertRange(0, mrms[i].Intensities);
                mrms.RemoveAt(i);
                break;
              }
            }
            else if (mrms[j].LastRetentionTime < mrms[i].FirstRetentionTime)
            {
              if (mrms[i].FirstRetentionTime - mrms[j].LastRetentionTime < 1)
              {
                mrms[j].Intensities.AddRange(mrms[i].Intensities);
                mrms.RemoveAt(i);
                break;
              }
            }
          }
        }
      }

      //平滑
      if (this.options.AskForSmooth)
      {
        mrms.ForEach(m => m.Smoothing());
      }
      return mrms;
    }

    private Dictionary<string, List<string>> GetGroupMap()
    {
      Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

      var values = options.RawFiles.Values.Distinct().ToList();
      if (values.Count == 1 && values[0] == DEFAULT)
      {
        foreach (var m in this.options.RawFiles)
        {
          map[m.Key] = new[] { m.Key }.ToList();
        }
      }
      else
      {
        values.ForEach(m => map[m] = new List<string>());
        foreach (var m in options.RawFiles)
        {
          map[m.Value].Add(m.Key);
        }
      }
      return map;
    }

    private List<SrmScan> GetMRMScans(string directory)
    {
      var fileName = GetPeakFileName(directory);

      if (File.Exists(fileName))
      {
        SetProgressMessage("Reading scan from " + fileName + " ...");
        return new SrmScanFileFormat().ReadFromFile(fileName);
      }
      else
      {
        List<PeakList<Peak>> pkls = new List<PeakList<Peak>>();

        SetProgressMessage("Reading scan from " + directory + " ...");
        IRawFile reader = RawFileFactory.GetRawFileReader(directory);
        try
        {
          var firstCount = reader.GetFirstSpectrumNumber();
          var lastCount = reader.GetLastSpectrumNumber();

          Progress.SetRange(firstCount, lastCount);
          Progress.SetPosition(firstCount);
          for (int i = firstCount; i <= lastCount; i++)
          {
            if (reader.GetMsLevel(i) != 2)
            {
              continue;
            }

            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            Progress.Increment(1);

            var pkl = reader.GetPeakList(i);
            var precursor = reader.GetPrecursorPeak(i);
            if (precursor != null)
            {
              pkl.PrecursorMZ = precursor.Mz;
              pkl.PrecursorCharge = precursor.Charge;
            }
            pkl.ScanTimes.Add(new ScanTime(i, reader.ScanToRetentionTime(i)));

            pkls.Add(pkl);
          }

          List<SrmScan> result = new List<SrmScan>();

          pkls.ForEach(m => m.ForEach(n => result.Add(new SrmScan(m.PrecursorMZ, n.Mz, m.ScanTimes[0].RetentionTime, n.Intensity, true))));

          new SrmScanFileFormat().WriteToFile(fileName, result);

          SetProgressMessage("finished.");

          return result;
        }
        finally
        {
          reader.Close();
        }
      }
    }

    public static string GetMrmFileName(string targetDirectory, string rawFile)
    {
      string result;
      if (Directory.Exists(rawFile))
      {
        DirectoryInfo di = new DirectoryInfo(rawFile);
        result = targetDirectory + "/" + di.Name + ".mrm";
      }
      else if (File.Exists(rawFile))
      {
        FileInfo fi = new FileInfo(rawFile);
        result = targetDirectory + "/" + fi.Name + ".mrm";
      }
      else
      {
        result = targetDirectory + "/" + rawFile + ".mrm";
      }

      return new FileInfo(result).FullName;
    }

    private string GetPeakFileName(string rawFile)
    {
      if (Directory.Exists(rawFile))
      {
        DirectoryInfo di = new DirectoryInfo(rawFile);
        return di.Parent.FullName + "/" + di.Name + ".peak";
      }

      return new FileInfo(rawFile + ".peak").FullName;
    }
  }
}
