using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqProteinRatioCalculator : IITraqProteinRatioCalculator
  {
    public ITraqProteinRatioCalculator()
    {
      this.GetSample = m => m[115];
      this.GetReference = m => m[114];
      this.ChannelName = "115/114";
      //this.OutlierEValue = 0.0001;
      this.MinimumCountForOutlierDetection = 4;
      this.Filter = m => true;
      this.OutlierDetector = new MADOutlierDetector();
    }

    public int MinimumCountForOutlierDetection { get; set; }

    //public double OutlierEValue { get; set; }

    public string DatasetName { get; set; }

    public string ChannelName { get; set; }

    public IOutlierDetector OutlierDetector { get; set; }

    public IRatioPeptideToProteinBuilder RatioBuilder { get; set; }

    public Func<IsobaricItem, double> GetSample { get; set; }

    public Func<IsobaricItem, double> GetReference { get; set; }

    public Predicate<IIdentifiedSpectrum> Filter { get; set; }

    #region IITraqProteinRatioCalculator Members

    public List<ITraqPeptideRatioItem> Calculate(IIdentifiedProteinGroup protein)
    {
      var result = (from s in protein.GetPeptides()
                    where Filter(s)
                    let i = s.FindIsobaricItem()
                    where i != null && i.Valid
                    let si = GetSample(i)
                    let ri = GetReference(i)
                    where !double.IsNaN(si) && !double.IsInfinity(si) && !double.IsNaN(ri) && !double.IsInfinity(ri) && (ri != 0)
                    select new ITraqPeptideRatioItem() { Sample = si, Reference = ri, Ratio = si / ri, IonInjectTime = i.Scan.IonInjectionTime }).ToList();

      if (result.Count == 0)
      {
        foreach (var p in protein)
        {
          p.FindOrCreateITraqChannelItem(DatasetName, ChannelName).RatioStr = "#N/A";
        }
      }
      else
      {
        var ratios = (from l in result
                      select Math.Log(l.Ratio)).ToList();

        var outliers = OutlierDetector.Detect(ratios);

        if (outliers.Count > 0)
        {
          //  using (StreamWriter sw = new StreamWriter("e:/temp.txt", true))
          //  {
          //    sw.WriteLine("{0} outliers detected in {1} ratios", outliers.Count, ratios.Count);
          //    sw.Write("Outlier");
          //    foreach (var outlier in outliers)
          //    {
          //      sw.Write(",{0:0.0000}", ratios[outlier]);
          //    }
          //    sw.WriteLine();

          //    for (int i = outliers.Count - 1; i >= 0; i--)
          //    {
          //      ratios.RemoveAt(outliers[i]);
          //    }
          //    sw.Write("Other");
          //    foreach (var r in ratios)
          //    {
          //      sw.Write(",{0:0.0000}", r);
          //    }
          //    sw.WriteLine();
          //    sw.WriteLine();
          //  }

          for (int i = outliers.Count - 1; i >= 0; i--)
          {
            result[outliers[i]].IsOutlier = true;
            //result.RemoveAt(outliers[i]);
          }
        }

        var validItems = (from r in result where !r.IsOutlier select r).ToList();

        var ratio = RatioBuilder.Calculate(validItems);

        foreach (var p in protein)
        {
          p.FindOrCreateITraqChannelItem(DatasetName, ChannelName).Ratio = ratio.Ratio;
        }
      }
      return result;
    }

    #endregion
  }
}
