using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using MathNet.Numerics.Statistics;
using System.IO;

namespace RCPA.Proteomics.Statistic
{
  public class CorrleationCalculator : AbstractThreadFileProcessor
  {
    public Dictionary<string, HashSet<string>> ClassificationSet { get; set; }

    public override IEnumerable<string> Process(string fileName)
    {
      var ir = new MascotResultTextFormat().ReadFromFile(fileName);

      ResultCorrelationItem rci = BuildResult(ir);

      var result = fileName + ".corr";
      using (StreamWriter sw = new StreamWriter(result))
      {
        sw.Write("Index\tName");
        foreach (var title in rci.ClassificationTitles)
        {
          sw.Write("\t" + title);
        }
        sw.WriteLine("\tCorrelation");

        foreach (var pro in rci)
        {
          sw.Write("{0}\t", pro.Index);
          PrintCorrelationItem(sw, pro.Protein);
          foreach (var pep in pro.Peptides)
          {
            sw.Write("\t");
            PrintCorrelationItem(sw, pep);
          }
        }
      }

      return new string[] { result };
    }

    private static void PrintCorrelationItem(StreamWriter sw, CorrelationItem vv)
    {
      sw.Write(vv.Name);
      foreach (var v in vv.Values)
      {
        sw.Write("\t{0}", v);
      }
      sw.WriteLine("\t{0:0.0000}", vv.Correlation);
    }

    private ResultCorrelationItem BuildResult(IIdentifiedResult ir)
    {
      var result = new ResultCorrelationItem();
      result.ClassificationTitles = ClassificationSet.Keys.ToArray();
      foreach (var g in ir)
      {
        var pro = g[0];
        var protein = new ProteinCorrelationItem();
        result.Add(protein);

        protein.Index = g.Index;
        protein.Protein = ParseItem(pro.Peptides, result.ClassificationTitles);
        protein.Protein.Name = pro.Name;

        var peps = pro.Peptides.GroupBy(m => PeptideUtils.GetMatchedSequence(m.Sequence));
        foreach (var pep in peps)
        {
          var pepitem = ParseItem(pep.ToList(), result.ClassificationTitles);
          protein.Peptides.Add(pepitem);

          pepitem.Name = pep.Key;
          pepitem.Correlation = Correlation.Pearson(pepitem.Values, protein.Protein.Values);
        }
      }

      result.ForEach(m =>
      {
        m.Peptides.Sort((m1, m2) => m2.Correlation.CompareTo(m1.Correlation));
      });
      return result;
    }

    private CorrelationItem ParseItem(List<IIdentifiedPeptide> list, string[] keys)
    {
      CorrelationItem result = new CorrelationItem();
      result.Values = new double[ClassificationSet.Count];
      for (int i = 0; i < result.Values.Length; i++)
      {
        result.Values[i] = 0;
      }

      for (int i = 0; i < keys.Length; i++)
      {
        var key = keys[i];
        var values = ClassificationSet[key];
        foreach (var pep in list)
        {
          if (values.Contains(pep.Spectrum.Query.FileScan.Experimental))
          {
            result.Values[i] += 1;
          }
        }
      }

      return result;
    }
  }
}
