using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public abstract class AbstractITraqProteinStatisticBuilder : AbstractThreadFileProcessor
  {
    private ITraqProteinStatisticOption option;

    private IITraqProteinRatioCalculator ratioCalc;

    public AbstractITraqProteinStatisticBuilder(ITraqProteinStatisticOption option)
    {
      this.option = option;
      this.ratioCalc = option.GetRatioCalculator();
    }

    protected abstract IIdentifiedResult GetIdentifiedResult(string fileName);

    protected abstract MascotResultTextFormat GetFormat(IIdentifiedResult ir);

    public override IEnumerable<string> Process(string fileName)
    {
      option.ProteinFileName = fileName;

      Progress.SetMessage("Reading proteins...");

      IIdentifiedResult ir = GetIdentifiedResult(fileName);

      List<IIdentifiedSpectrum> spectra = ir.GetSpectra();
      if (option.QuantifyModifiedPeptideOnly)
      {
        spectra.RemoveAll(m =>
        {
          foreach (char c in option.ModificationChars)
          {
            if (m.Sequence.Contains(c))
            {
              return false;
            }
          }
          return true;
        });
      }

      ITraqItemUtils.LoadITraq(spectra, option.ITraqFileName, false, this.Progress);

      var itraqs = (from s in spectra
                    where s.FindIsobaricItem() != null
                    select s.FindIsobaricItem()).ToList();

      if (itraqs.Count == 0)
      {
        throw new Exception("No itraq information matched from " + option.ITraqFileName);
      }

      Progress.SetMessage("Calculating ...");
      var funcs = itraqs[0].PlexType.GetFuncs();
      funcs.RemoveAll(m => option.References.Any(n => n.Name.Equals(m.Name)));

      option.GetValidator().Validate(itraqs);

      foreach (var dsName in option.DatasetMap.Keys)
      {
        var dsSet = new HashSet<string>(option.DatasetMap[dsName]);
        foreach (var func in funcs)
        {
          var channelName = func.ChannelRatioName;
          foreach (var group in ir)
          {
            CalculateProteinRatio(group, func.GetValue, dsName, channelName, dsSet);
          }
        }
      }

      if (option.NormalizeByMedianRatio)
      {
        foreach (var dsName in option.DatasetMap.Keys)
        {
          foreach (var func in funcs)
          {
            var channelName = func.ChannelRatioName;
            var groups = (from g in ir
                          let item = g[0].FindITraqChannelItem(dsName, channelName)
                          where null != item && item.HasRatio
                          select new { Group = g, LogRatio = Math.Log(item.Ratio) }).ToList();


            var ratios = (from g in groups
                          select g.LogRatio).ToArray();

            if (ratios.Length > 1)
            {
              var mean = Statistics.Mean(ratios);
              foreach (var g in groups)
              {
                var ratio = g.Group[0].FindITraqChannelItem(dsName, channelName).Ratio;
                var logRatio = Math.Log(ratio);
                var fixedLogRatio = logRatio - mean;
                var fixedRatio = Math.Exp(fixedLogRatio);
                foreach (var p in g.Group)
                {
                  p.FindITraqChannelItem(dsName, channelName).Ratio = fixedRatio;
                }
              }
            }
          }
        }
      }

      var irFormat = GetFormat(ir);

      var refNames = (from refF in option.References
                      select refF.Name).Merge("");

      string resultFileName = GetResultFileName(fileName, refNames);
      irFormat.WriteToFile(resultFileName, ir);

      string paramFileName = Path.ChangeExtension(resultFileName, ".param");
      option.SaveToFile(paramFileName);

      var statFileName = Path.ChangeExtension(resultFileName, ".stat");
      using (StreamWriter sw = new StreamWriter(statFileName))
      {
        DoStatistic(sw, funcs, ir);
      }

      Progress.SetMessage("Finished.");

      return new[] { resultFileName, statFileName };
    }

    protected virtual string GetResultFileName(string fileName, string refNames)
    {
      return fileName + "." + refNames + ".itraq";
    }

    private void CalculateProteinRatio(IIdentifiedProteinGroup group, Func<IsobaricItem, double> getSample, string dsName, string channelName, HashSet<string> expNames)
    {
      ratioCalc.GetSample = getSample;
      ratioCalc.DatasetName = dsName;
      ratioCalc.ChannelName = channelName;
      ratioCalc.Filter = m => expNames.Contains(m.Query.FileScan.Experimental);
      ratioCalc.Calculate(group);
    }

    private void DoStatistic(StreamWriter sw, List<IsobaricIndex> funcs, IIdentifiedResult ir)
    {
      sw.WriteLine("Dataset\tChannel\tMean[log2(Ratio)]\tStandardDeviation[log2(Ratio)]\tRatio\t95%\t99%\t99.9%\tTotalCount\tValidCount\tP<0.05\tP<0.01\tp<0.001");

      var dsNames = option.DatasetMap.Keys.OrderBy(m => m).ToList();
      foreach (var dsName in dsNames)
      {
        foreach (var func in funcs)
        {
          var channelName = func.ChannelRatioName;

          List<double> ratios = new List<double>();

          foreach (var m in ir)
          {
            var channelItem = m[0].FindITraqChannelItem(dsName, channelName);

            if (null != channelItem && channelItem.HasRatio)
            {
              ratios.Add(Math.Log(channelItem.Ratio));
            }
          }

          QuantificationUtils.RatioStatistic(sw, dsName + "\t" + channelName, ratios, ir.Count);
        }
      }

      sw.WriteLine();
      sw.WriteLine("Dataset\tChannel\tMean[log2(Ratio)]\tStandardDeviation[log2(Ratio)]\tRatio\t95%\t99%\t99.9%\tTotalCount\tValidCount\tP<0.05\tP<0.01\tp<0.001");
      //After normalization, have a look at the ratio/ratio in same dataset
      foreach (var dsName in option.DatasetMap.Keys)
      {
        for (int i = 0; i < funcs.Count; i++)
        {
          var channelNameI = funcs[i].ChannelRatioName;
          for (int j = i + 1; j < funcs.Count; j++)
          {
            var channelNameJ = funcs[j].ChannelRatioName;

            List<double> ratios = new List<double>();

            foreach (var m in ir)
            {
              var channelItemI = m[0].FindITraqChannelItem(dsName, channelNameI);
              var channelItemJ = m[0].FindITraqChannelItem(dsName, channelNameJ);

              if (null != channelItemI && channelItemI.HasRatio && null != channelItemJ && channelItemJ.HasRatio)
              {
                ratios.Add(Math.Log(channelItemJ.Ratio / channelItemI.Ratio));
              }
            }

            QuantificationUtils.RatioStatistic(sw, dsName + "\t" + funcs[j].Name + "/" + funcs[i].Name, ratios, ir.Count);
          }
        }
      }

      sw.WriteLine();
      sw.WriteLine("Dataset\tChannel\tMean[log2(Ratio)]\tStandardDeviation[log2(Ratio)]\tRatio\t95%\t99%\t99.9%\tTotalCount\tValidCount\tP<0.05\tP<0.01\tp<0.001");
      //After normalization, have a look at the ratio/ratio in diffent dataset
      foreach (var func in funcs)
      {
        var channelName = func.ChannelRatioName;
        for (int i = 0; i < dsNames.Count; i++)
        {
          var dsNameI = dsNames[i];
          for (int j = i + 1; j < dsNames.Count; j++)
          {
            var dsNameJ = dsNames[j];

            List<double> ratios = new List<double>();

            foreach (var m in ir)
            {
              var channelItemI = m[0].FindITraqChannelItem(dsNameI, channelName);
              var channelItemJ = m[0].FindITraqChannelItem(dsNameJ, channelName);

              if (null != channelItemI && channelItemI.HasRatio && null != channelItemJ && channelItemJ.HasRatio)
              {
                ratios.Add(Math.Log(channelItemJ.Ratio / channelItemI.Ratio));
              }
            }

            QuantificationUtils.RatioStatistic(sw, dsNameJ + "/" + dsNameI + "\t" + func.Name, ratios, ir.Count);
          }
        }
      }
      sw.WriteLine();
      sw.WriteLine("Dataset\tChannel\tMean[log2(Ratio)]\tStandardDeviation[log2(Ratio)]\tRatio\t95%\t99%\t99.9%\tTotalCount\tValidCount\tP<0.05\tP<0.01\tp<0.001");
      //After normalization, have a look at the ratio/ratio in diffent dataset
      for (int f1 = 0; f1 < funcs.Count; f1++)
      {
        var c1 = funcs[f1].ChannelRatioName;
        for (int f2 = f1 + 1; f2 < funcs.Count; f2++)
        {
          var c2 = funcs[f2].ChannelRatioName;

          for (int i = 0; i < dsNames.Count; i++)
          {
            var dsNameI = dsNames[i];
            for (int j = i + 1; j < dsNames.Count; j++)
            {
              var dsNameJ = dsNames[j];

              List<double> ratios = new List<double>();

              foreach (var m in ir)
              {
                var channelItemI = m[0].FindITraqChannelItem(dsNameI, c1);
                var channelItemJ = m[0].FindITraqChannelItem(dsNameJ, c2);

                if (null != channelItemI && channelItemI.HasRatio && null != channelItemJ && channelItemJ.HasRatio)
                {
                  ratios.Add(Math.Log(channelItemJ.Ratio / channelItemI.Ratio));
                }
              }

              QuantificationUtils.RatioStatistic(sw, dsNameJ + "/" + dsNameI + "\t" + funcs[f2].Name + "/" + funcs[f1].Name, ratios, ir.Count);
            }
          }
        }
      }
    }
  }
}
