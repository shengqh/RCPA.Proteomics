using RCPA.Proteomics.Mascot;
using RCPA.Seq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ProteinLabelFreeQuantificationBuilder : AbstractThreadFileProcessor
  {
    private IProteinLabelfreeQuantificationCalculator calculator;
    private Dictionary<string, List<string>> expsMap;
    private IAccessNumberParser parser;
    private int MinSpectrumCount { get; set; }

    public ProteinLabelFreeQuantificationBuilder(IProteinLabelfreeQuantificationCalculator calculator, Dictionary<string, List<string>> expsMap, IAccessNumberParser parser)
    {
      this.calculator = calculator;
      this.expsMap = expsMap;
      this.parser = parser;
      this.MinSpectrumCount = 5;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var format = new MascotResultTextFormat();

      var ir = format.ReadFromFile(fileName);

      foreach (var g in ir)
      {
        foreach (var p in g)
        {
          string value;
          if (this.parser.TryParse(p.Name, out value))
          {
            p.Name = value;
          }
        }
      }

      for (int i = ir.Count - 1; i > 0; i--)
      {
        for (int j = i - 1; j >= 0; j--)
        {
          if (SameName(ir[i], ir[j]))
          {
            ir.RemoveAt(i);
            break;
          }
        }
      }

      calculator.Calculate(ir, expsMap);

      var countPassed = ir.TakeWhile(m =>
      {
        var lr = m[0].GetLabelfreeResult();
        return lr.HasCountLargerThan(MinSpectrumCount - 1);
      });

      var result1 = FileUtils.ChangeExtension(fileName, "count");

      using (StreamWriter sw = new StreamWriter(result1))
      {
        sw.Write("Protein\tDescription");
        foreach (var key in expsMap.Keys)
        {
          sw.Write("\t" + key);
        }
        sw.WriteLine();

        foreach (var group in ir)
        {
          var protein = group[0];
          sw.Write(protein.Name + "\t" + protein.Description);
          var lr = protein.GetLabelfreeResult();
          foreach (var dsKey in expsMap.Keys)
          {
            var nsaf = lr[dsKey];
            sw.Write("\t{0}", nsaf.Count);
          }
          sw.WriteLine();
        }
      }

      var result = FileUtils.ChangeExtension(fileName, this.calculator.GetExtension());

      using (StreamWriter sw = new StreamWriter(result))
      {
        sw.Write("Protein\tDescription");
        foreach (var key in expsMap.Keys)
        {
          sw.Write("\t" + key);
        }
        foreach (var key in expsMap.Keys)
        {
          sw.Write("\t" + key + "_count");
        }
        sw.WriteLine();

        foreach (var group in countPassed)
        {
          var protein = group[0];
          sw.Write(protein.Name + "\t" + protein.Description.Replace('\'', ' ').Replace('\t', ' '));
          var lr = protein.GetLabelfreeResult();
          foreach (var dsKey in expsMap.Keys)
          {
            var nsaf = lr[dsKey];
            sw.Write("\t{0:0.000000}", nsaf.Value);
          }
          foreach (var dsKey in expsMap.Keys)
          {
            var nsaf = lr[dsKey];
            sw.Write("\t{0}", nsaf.Count);
          }

          sw.WriteLine();
        }
      }

      return new string[] { result };
    }

    private bool SameName(Summary.IIdentifiedProteinGroup g1, Summary.IIdentifiedProteinGroup g2)
    {
      foreach (var p1 in g1)
      {
        foreach (var p2 in g2)
        {
          if (p1.Name.Equals(p2.Name))
          {
            return true;
          }
        }
      }

      return false;
    }
  }
}
