using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using Microsoft.Office.Interop.Excel;
using RCPA.Utils;
using RCPA.Gui;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Sequest
{
  public abstract class AbstractMsfToNoredundantProcessor : AbstractThreadFileProcessor
  {
    private static readonly string MODIFICATION_CHAR = "*#@&^%$~1234567890";

    public override IEnumerable<string> Process(string fileName)
    {
      var proteins = ParseProteins(fileName);

      Progress.SetMessage("Building protein groups ...");
      var groups = new IdentifiedProteinGroupBuilder().Build(proteins);

      Progress.SetMessage("Building result ...");
      var ir = new IdentifiedResultBuilder(null, null).Build(groups);

      var result = FileUtils.ChangeExtension(fileName, ".noredundant");

      RefineModifications(ir);

      Progress.SetMessage("Saving result ...");
      new SequestResultTextFormat(SequestHeader.SEQUEST_PROTEIN_HEADER, SequestHeader.SEQUEST_PEPTIDE_HEADER + "\tModification").WriteToFile(result, ir);

      Progress.SetMessage("Finished.");

      return new string[] { result };
    }

    protected virtual void RefineModifications(IIdentifiedResult ir)
    {
      HashSet<string> modifications = new HashSet<string>();
      var spectra = ir.GetSpectra();
      Regex reg = new Regex(@"\((.+)\)");
      var chars = new char[] { ' ', '\t' };
      foreach (var spectrum in spectra)
      {
        if (spectrum.Modifications == null)
        {
          continue;
        }

        string[] mods = spectrum.Modifications.Split(chars);
        foreach (var mod in mods)
        {
          var match = reg.Match(mod);
          modifications.Add(match.Groups[1].Value);
        }
      }

      var modstrings = (from m in modifications
                        where m != ""
                        orderby m
                        select m).ToList();

      Dictionary<string, string> modChars = new Dictionary<string, string>();
      modstrings.ForEach(m => modChars[m] = MODIFICATION_CHAR[modChars.Count].ToString());

      Regex reg2 = new Regex(@"(\d+)\((.+)\)");
      foreach (var spectrum in spectra)
      {
        if (spectrum.Modifications == null)
        {
          continue;
        }

        string[] mods = spectrum.Modifications.Split(chars);
        for (int i = mods.Length - 1; i >= 0; i--)
        {
          if (mods[i] == "")
          {
            continue;
          }
          var match = reg2.Match(mods[i]);
          var index = Convert.ToInt32(match.Groups[1].Value);
          var modstr = match.Groups[2].Value;
          spectrum.Peptide.Sequence = spectrum.Sequence.Insert(index, modChars[modstr]);
        }
      }
    }

    public abstract List<IIdentifiedProtein> ParseProteins(string fileName);
  }
}
