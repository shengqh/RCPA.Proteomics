using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using MathNet.Numerics.Statistics;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Utils;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqPeptideStatisticBuilder : AbstractITraqPeptideStatisticBuilder
  {
    private List<IsobaricIndex> refFuncs;

    private double minProbability;

    public ITraqPeptideStatisticBuilder(string rawFileName, List<IsobaricIndex> refFuncs, double minProbability)
      : base(rawFileName)
    {
      this.refFuncs = refFuncs;
      this.minProbability = minProbability;
    }

    private double GetReference(IsobaricItem item)
    {
      return (from f in refFuncs
              select f.GetValue(item)).Average();
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var spectra = GetSpectra(fileName);

      var find = (from s in spectra
                  where s.FindIsobaricItem() != null
                  select s).ToList();

      if (find.Count == 0)
      {
        throw new Exception("No pair found between peptide and itraq!");
      }

      var its = find.ConvertAll(m => m.FindIsobaricItem());

      if (refFuncs.Count == 2)
      {
        new ITraqItemValidator2(refFuncs[0], refFuncs[1], minProbability).Validate(its);
      }
      else
      {
        its.ForEach(m =>
        {
          m.Valid = true;
          m.ValidProbability = 1.0;
        });
      }

      var refNames = (from refF in refFuncs
                      select refF.Name).Merge("");

      var plexType = find.First().FindIsobaricItem().PlexType;

      var funcs = plexType.GetFuncs().ToList();
      funcs.RemoveAll(m => refFuncs.Any(n => m.Name.Equals(n.Name)));

      string refName = "REF";

      Progress.SetMessage("Calculating ...");
      foreach (var v in find)
      {
        var item = v.FindIsobaricItem();
        var refIntensity = GetReference(item);
        v.Annotations["REF"] = string.Format("{0:0.0}", refIntensity);
        foreach (var m in funcs)
        {
          var samIntensity = m.GetValue(item);
          v.Annotations[m.Name + "/REF"] = string.Format("{0:0.0000}", samIntensity / refIntensity);
        }
      }

      format.Initialize(find);

      Progress.SetMessage("Writing itraq ...");
      string resultFileName = fileName + ".itraq";
      format.WriteToFile(resultFileName, spectra);

      var seqFilename = fileName + ".groupseq";
      var bin = find.GroupBy(f => PeptideUtils.GetPureSequence(f.Sequence));
      using (StreamWriter sw = new StreamWriter(seqFilename))
      {
        var header = (from f in funcs
                      select f.Name + "/" + refName).Merge("\t");

        sw.WriteLine("PureSequence" + format.PeptideFormat.GetHeader());

        foreach (var b in bin)
        {
          sw.WriteLine(b.Key);

          var items = from i in b
                      orderby i.Sequence
                      select i;

          foreach (var v in items)
          {
            sw.WriteLine(format.PeptideFormat.GetString(v));
          }
        }
      }

      Progress.SetMessage("Writing statistic ...");
      string statisticFileName = resultFileName + ".stat";
      using (StreamWriter sw = new StreamWriter(statisticFileName))
      {
        sw.WriteLine("No itraq information count\t{0}", spectra.Count - find.Count);
        var validCount = find.Count(m => m.FindIsobaricItem().ValidProbability >= 0.01);
        sw.WriteLine("Valid count (p >= 0.01)\t{0}", validCount);
        sw.WriteLine("Invalid count (p < 0.01)\t{0}", find.Count - validCount);

        sw.WriteLine();
        sw.WriteLine("All itraq statistic");
        DoStatistic(sw, funcs, its);

        its = (from f in find
               where f.FindIsobaricItem().ValidProbability >= 0.01
               select f.FindIsobaricItem()).ToList();
        sw.WriteLine();
        sw.WriteLine("Valid itraq statistic");
        DoStatistic(sw, funcs, its);
      }

      Progress.SetMessage("Finished.");

      return new[] { resultFileName, seqFilename, statisticFileName };
    }

    private void DoStatistic(StreamWriter sw, List<IsobaricIndex> funcs, List<IsobaricItem> its)
    {
      sw.WriteLine("\tMean[log2(Ratio)]\tStandardDeviation[log2(Ratio)]\tRatio\t95%\t99%\t99.9%\tTotalCount\tValidCount\tP<0.05\tP<0.01\tp<0.001");
      for (int j = 0; j < refFuncs.Count; j++)
      {
        for (int i = 0; i < funcs.Count; i++)
        {
          Calculate(sw, funcs[i].Name + "/" + refFuncs[j].Name, funcs[i].GetValue, refFuncs[j].GetValue, its);
        }
      }
    }

    private void Calculate(StreamWriter sw, string title, Func<IsobaricItem, double> numerator, Func<IsobaricItem, double> denominator, IEnumerable<IsobaricItem> values)
    {
      List<double> ratios = new List<double>();

      foreach (IsobaricItem m in values)
      {
        double dNumerator = numerator(m);
        double dDenominator = denominator(m);

        if (dNumerator == ITraqConsts.NULL_INTENSITY || dDenominator == ITraqConsts.NULL_INTENSITY)
        {
          continue;
        }

        ratios.Add(Math.Log(dNumerator / dDenominator));
      }

      QuantificationUtils.RatioStatistic(sw, title, ratios, values.Count());
    }
  }
}
