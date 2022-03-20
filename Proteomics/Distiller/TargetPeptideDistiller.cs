using RCPA.Proteomics.Mascot;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Distiller
{
  public class TargetPeptideDistiller : AbstractThreadFileProcessor
  {
    private Regex decoyReg;

    public TargetPeptideDistiller(string decoyPattern)
    {
      decoyReg = new Regex(decoyPattern);
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var format = new MascotPeptideTextFormat();
      var peptides = format.ReadFromFile(fileName);

      peptides.RemoveAll(m => m.Proteins.Any(n => decoyReg.Match(n).Success));

      var result = FileUtils.ChangeExtension(fileName, ".target.peptides");
      format.WriteToFile(result, peptides);

      return new string[] { result };
    }
  }
}
