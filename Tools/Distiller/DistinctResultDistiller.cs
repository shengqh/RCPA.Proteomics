using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Mascot;

namespace RCPA.Tools.Distiller
{
  public class DistinctResultDistiller : AbstractThreadFileProcessor
  {
    public void KeepDistinctPeptideOnly(IIdentifiedResult ir)
    {
      ir.InitializeGroupCount();

      for (int i = ir.Count - 1; i >= 0; i--)
      {
        foreach (IIdentifiedProtein protein in ir[i])
        {
          protein.Peptides.RemoveAll(m => m.Spectrum.GroupCount > 1);
        }

        if (0 == ir[i].GetPeptides().Count)
        {
          ir.RemoveAt(i);
        }
      }
    }

    public override IEnumerable<string> Process(string fileName)
    {
      MascotResultTextFormat format = new MascotResultTextFormat();

      IIdentifiedResult ir = format.ReadFromFile(fileName);

      KeepDistinctPeptideOnly(ir);

      string resultFileName = fileName + ".distinct";
      format.WriteToFile(resultFileName, ir);

      return new [] { resultFileName };
    }
  }
}
