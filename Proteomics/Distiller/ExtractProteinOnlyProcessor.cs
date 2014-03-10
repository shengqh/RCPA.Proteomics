using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Distiller
{
  public class ExtractProteinOnlyProcessor : AbstractThreadFileProcessor
  {
    public ExtractProteinOnlyProcessor()
    { }

    public override IEnumerable<string> Process(string sourceFile)
    {
      MascotResultTextFormat format = new MascotResultTextFormat();
      format.Progress = this.Progress;

      var ir = format.ReadFromFile(sourceFile);

      var result = FileUtils.ChangeExtension(sourceFile, ".proteinonly");
      using (StreamWriter sw = new StreamWriter(result))
      {
        sw.WriteLine(format.ProteinFormat.GetHeader());
        foreach (var mpg in ir)
        {
          for (int i = 0; i < mpg.Count; i++)
          {
            sw.WriteLine("${0}-{1}{2}", mpg.Index, i + 1, format.ProteinFormat.GetString(mpg[i]));
          }
        }
      }

      return new string[] { result };
    }
  }
}
