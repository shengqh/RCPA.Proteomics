using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using System.IO;

namespace RCPA.Proteomics.PFind
{
  public class PMatchResultAnalyzer : AbstractThreadFileProcessor
  {
    private double fdr;
    public PMatchResultAnalyzer(double fdr)
    {
      this.fdr = fdr;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var format = new MascotPeptideTextFormat();

      var peptides = format.ReadFromFile(fileName);
      peptides.RemoveAll(m => m.QValue >= fdr);
      peptides.ForEach(m => m.TheoreticalMinusExperimentalMass = Math.Round(m.TheoreticalMinusExperimentalMass));
      peptides.RemoveAll(m => m.TheoreticalMinusExperimentalMass == 0.0);

      var result1 = MyConvert.Format("{0}.fdr{1:0.000}.txt", fileName, fdr);
      format.WriteToFile(result1, peptides);

      var groups = peptides.GroupBy(m => m.TheoreticalMinusExperimentalMass).ToList();
      groups.Sort((m1,m2) => -m1.Count().CompareTo(m2.Count()));

      var result2 = MyConvert.Format("{0}.fdr{1:0.000}.groups", fileName, fdr);
      using (StreamWriter sw = new StreamWriter(result2))
      {
        foreach (var group in groups)
        {
          sw.WriteLine("{0:0}\t{1}", -group.Key, group.Count());
        }
      }

      return new string[] { result1, result2 };
    }
  }
}
