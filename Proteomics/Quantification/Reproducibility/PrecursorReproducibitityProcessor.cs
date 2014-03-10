using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;
using RCPA.Utils;
using System.IO;
using MathNet.Numerics.Distributions;

namespace RCPA.Proteomics.Quantification.Reproducibility
{
  public class PrecursorReproducibitityProcessor : AbstractThreadFileProcessor
  {
    public class PairItem
    {
      public PrecursorItem Item1 { get; set; }
      public PrecursorItem Item2 { get; set; }
      public bool Used { get; set; }

      public double Mz
      {
        get
        {
          if (Item1 != null)
          {
            return Item1.Mz;
          }
          else
          {
            return Item2.Mz;
          }
        }
      }

      public double RetentionTimeDiff
      {
        get
        {
          if (Item1 != null && Item2 != null)
            return Item1.RetentionTime - Item2.RetentionTime;
          else
            return double.MaxValue;
        }
      }
      public double AbundanceDiff
      {
        get
        {
          if (Item1 != null && Item2 != null)
            return (Item1.Abundance - Item2.Abundance) / (Item1.Abundance + Item2.Abundance);
          else
            return double.MaxValue;
        }
      }
    }

    public override IEnumerable<string> Process(string fileName)
    {
      PrecursorReproducibitityConfiguration conf = new PrecursorReproducibitityConfiguration();
      conf.LoadFromFile(fileName);

      string result = FileUtils.ChangeExtension(fileName, "rt.comparison");

      ProcessConfiguration(conf, result);

      return new string[] { result };
    }

    public static void ProcessConfiguration(PrecursorReproducibitityConfiguration conf, string resultFileName)
    {
      List<PrecursorItemList> items = new List<PrecursorItemList>();

      var reader = new PrecursorItemListExcelReader();

      foreach (var file in conf.FileNames)
      {
        items.Add(reader.ReadFromFile(file));
      }

      items.ForEach(m => m.Sort((n1, n2) => -n1.Abundance.CompareTo(n2.Abundance)));

      using (StreamWriter sw = new StreamWriter(resultFileName))
      {
        for (int i = 0; i < items.Count; i++)
        {
          var itemI = items[i];
          var fileI = new FileInfo(conf.FileNames[i]).Name;
          itemI.ForEach(m => m.Matched = false);
          for (int j = i + 1; j < items.Count; j++)
          {
            var itemJ = items[j];
            var fileJ = new FileInfo(conf.FileNames[j]).Name;
            itemJ.ForEach(m => m.Matched = false);

            List<PairItem> unions = new List<PairItem>();
            foreach (var m in itemI)
            {
              var matched = (from n in itemJ
                             where (!n.Matched) && (PrecursorUtils.mz2ppm(m.Mz, Math.Abs(m.Mz - n.Mz)) < conf.PPMTolerance)
                             select n).ToList();

              if (matched.Count > 0)
              {
                matched.Sort((m1, m2) => Math.Abs(m1.RetentionTime - m.RetentionTime).CompareTo(Math.Abs(m2.RetentionTime - m.RetentionTime)));
                matched[0].Matched = true;
                m.Matched = true;
                unions.Add(new PairItem()
                {
                  Item1 = m,
                  Item2 = matched[0],
                  Used = false
                });
              }
            }

            var rts = (from u in unions
                       orderby u.RetentionTimeDiff
                       select u).ToList();

            //keep 90% to do statistic.
            int count = rts.Count / 20;
            rts = rts.Take(rts.Count - count).Skip(count).ToList();

            rts.ForEach(m => m.Used = true);

            sw.WriteLine("{0} vs {1}\tmatched {2}",fileI,fileJ,rts.Count);

            var acc = new MeanStandardDeviation(rts.ConvertAll(m => m.RetentionTimeDiff).ToArray());
            sw.WriteLine("RETENTION TIME : Mean = {0:0.0000}, SD = {1:0.0000}", acc.Mean, acc.StdDev);

            var abundances = from u in unions
                             select u.AbundanceDiff;

            acc = new MeanStandardDeviation(abundances.ToArray());
            sw.WriteLine("ABUNDANCE : Mean = {0:0.0000}, SD = {1:0.0000}", acc.Mean, acc.StdDev);

            sw.WriteLine("MZ\tRT1\tRT2\tABUNDANCE1\tABUNDANCE2\tRTDiff\tAbundanceDiff\tUsed");
            unions.Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));
            unions.ForEach(m => sw.WriteLine("{0:0.0000}\t{1:0.0000}\t{2:0.0000}\t{3:0.0000}\t{4:0.0000}\t{5:0.0000}\t{6:0.0000}\t{7}",
              m.Mz,
              m.Item1.RetentionTime,
              m.Item2.RetentionTime,
              m.Item1.Abundance,
              m.Item2.Abundance,
              m.RetentionTimeDiff,
              m.AbundanceDiff,
              m.Used));

            WriteOnlyItem(sw, itemI, fileI + " only");
            WriteOnlyItem(sw, itemJ, fileJ + " only");
          }
        }
      }
    }

    private static void WriteOnlyItem(StreamWriter sw, PrecursorItemList itemI, string title)
    {
      sw.WriteLine();
      sw.WriteLine("{0}\t{1}",title,itemI.Count );
      var iOnly = (from item in itemI where !item.Matched select item).ToList();
      sw.WriteLine("MZ\tRT\tABUNDANCE");
      iOnly.ForEach(m => sw.WriteLine("{0:0.0000}\t{1:0.0000}\t{2:0.0000}",
        m.Mz,
        m.RetentionTime,
        m.Abundance));
    }
  }
}
