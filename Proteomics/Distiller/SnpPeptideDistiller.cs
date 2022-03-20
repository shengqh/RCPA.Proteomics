using RCPA.Proteomics.Mascot;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Distiller
{
  public class SnpPeptideDistiller : AbstractThreadFileProcessor
  {
    private Regex regex;
    public SnpPeptideDistiller(string snpPattern)
    {
      this.regex = new Regex(snpPattern);
    }
    public override IEnumerable<string> Process(string fileName)
    {
      var format = new MascotPeptideTextFormat();
      var peptides = format.ReadFromFile(fileName);

      var resultpeptides = peptides.FindAll(m =>
      {
        bool bNormal = false;
        bool bSnp = false;
        foreach (var p in m.Proteins)
        {
          if (!regex.Match(p).Success)
          {
            bNormal = true;
          }
          else
          {
            bSnp = true;
          }
        }
        return !bNormal && bSnp;
      });

      var result = FileUtils.ChangeExtension(fileName, ".snp.peptides");
      format.WriteToFile(result, resultpeptides);

      return new string[] { result };
    }
  }
}
