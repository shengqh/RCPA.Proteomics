using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Distiller
{
  public class ExtractProteinByIdProcessor : AbstractThreadFileProcessor
  {
    private string sourceFile;

    public ExtractProteinByIdProcessor(string sourceFile)
    {
      this.sourceFile = sourceFile;
    }

    private bool Accept(List<string> acNumbers, IIdentifiedProteinGroup group)
    {
      foreach (var protein in group)
      {
        foreach (var ac in acNumbers)
        {
          if (protein.Name.Contains(ac))
          {
            return true;
          }
        }
      }
      return false;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var entries = File.ReadAllLines(fileName);
      var acNumbers = (from e in entries
                        let l = e.Trim()
                        where l.Length > 0
                        select l).ToList();

      MascotResultTextFormat format = new MascotResultTextFormat();
      format.Progress = this.Progress;

      var ir = format.ReadFromFile(sourceFile);
      for (int i = ir.Count - 1; i >= 0; i--)
      {
        if (!Accept(acNumbers, ir[i]))
        {
          ir.RemoveAt(i);
        }
      }

      var result = fileName + ".noredundant";
      format.WriteToFile(result, ir);

      return new string[] { result };
    }
  }
}
