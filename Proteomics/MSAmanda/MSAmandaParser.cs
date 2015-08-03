using RCPA.Gui;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.MzIdent;
using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.MSAmanda
{
  public class MSAmandaParser : ProgressClass, ISpectrumParser
  {
    public MSAmandaParser() { }

    public static readonly string TITLE_KEY = "Title";
    public static readonly string PROTEIN_KEY = "Protein Accessions";
    public static readonly string RT_KEY = "RT";

    private static Regex modReg = new Regex(@"(.+?)\((.+?)\|\S+?\|(\S+?);{0,1}");

    public ITitleParser TitleParser { get; set; }

    public SearchEngineType Engine
    {
      get
      {
        return SearchEngineType.MSAmanda;
      }
    }

    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      var result = new MascotPeptideTextFormat().ReadFromFile(fileName);

      FilterSpectra(result);

      UpdateModifications(result);

      foreach (var peptide in result)
      {
        peptide.Peptide.AssignProteins((peptide.Annotations[PROTEIN_KEY] as string).Split(';'));
        peptide.Annotations.Remove(PROTEIN_KEY);
        peptide.TheoreticalMass = peptide.ExperimentalMass;
      }

      var i = 0;
      while (i < result.Count - 1)
      {
        var ititle = result[i].Annotations[TITLE_KEY] as string;
        while (i < result.Count - 1)
        {
          var jtitle = result[i + 1].Annotations[TITLE_KEY] as string;
          if (!ititle.Equals(jtitle))
          {
            i++;
            break;
          }

          for (int l = result[i + 1].Peptides.Count - 1; l >= 0; l--)
          {
            result[i].AddPeptide(result[i + 1].Peptides[l]);
          }

          result.RemoveAt(i + 1);
        }
      }

      foreach (var peptide in result)
      {
        var title = peptide.Annotations[TITLE_KEY] as string;
        peptide.Annotations.Remove(TITLE_KEY);

        var oldCharge = peptide.Query.FileScan.Charge;
        peptide.Query.FileScan = TitleParser.GetValue(title);

        peptide.Query.FileScan.Charge = oldCharge;
        if (string.IsNullOrEmpty(peptide.Query.FileScan.Experimental))
        {
          peptide.Query.FileScan.Experimental = Path.GetFileNameWithoutExtension(fileName);
        }
        var rtstr = peptide.Annotations[RT_KEY] as string;
        if (!string.IsNullOrWhiteSpace(rtstr))
        {
          peptide.Query.FileScan.RetentionTime = double.Parse(rtstr.StringBefore("-"));
        }
        peptide.Annotations.Remove(RT_KEY);
      }

      return result;
    }

    protected virtual void FilterSpectra(List<IIdentifiedSpectrum> result)
    {
      result.RemoveAll(m => m.Rank != 1);
    }

    private static Regex modReg2 = new Regex(@"\S+?(\d+)\((\S+?)\|");
    private void UpdateModifications(List<IIdentifiedSpectrum> result)
    {
      PepXmlModifications mods = new PepXmlModifications();
      var map = new Dictionary<string, PepXmlModificationItem>();
      foreach (var pep in result)
      {
        var curMods = pep.Modifications.Split(';');
        foreach (var curMod in curMods)
        {
          var m = modReg2.Match(curMod);
          if (m.Success)
          {
            var modname = m.Groups[2].Value;
            if (!map.ContainsKey(modname))
            {
              var item = new PepXmlModificationItem();
              item.Aminoacid = modname;
              item.IsVariable = curMod.Contains("variable");
              map[modname] = item;
            }
          }
        }
      }

      map.OrderBy(m => m.Key).ToList().ForEach(m => mods.Add(m.Value));
      mods.AssignModificationChar();

      foreach (var spectrum in result)
      {
        if (string.IsNullOrEmpty(spectrum.Modifications))
        {
          continue;
        }

        Stack<Tuple<int, string>> vmods = new Stack<Tuple<int, string>>();
        var curMods = spectrum.Modifications.Split(';');

        foreach (var curMod in curMods)
        {
          if (!curMod.Contains("variable"))
          {
            continue;
          }

          var m = modReg2.Match(curMod);
          if (m.Success)
          {
            var modname = m.Groups[2].Value;
            var modchar = map[modname].Symbol;
            var modpos = int.Parse(m.Groups[1].Value);
            vmods.Push(new Tuple<int, string>(modpos, modchar));
          }
        }

        foreach (var pep in spectrum.Peptides)
        {
          pep.Sequence = pep.Sequence.ToUpper();
        }

        while (vmods.Count > 0)
        {
          var vmod = vmods.Pop();
          foreach (var pep in spectrum.Peptides)
          {
            pep.Sequence = pep.Sequence.Insert(vmod.Item1, vmod.Item2);
          }
        }
      }
    }
  }
}
