using RCPA.Gui;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.MzIdent;
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

    private static readonly string TITLE_KEY = "Title";
    private static readonly string PROTEIN_KEY = "Protein Accessions";
    private static readonly string RT_KEY = "RT";
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
      result.RemoveAll(m => m.Rank != 1);

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
        peptide.Query.RetentionTime = double.Parse((peptide.Annotations[RT_KEY] as string).StringBefore("-"));
        peptide.Annotations.Remove(RT_KEY);
      }

      return result;
    }
  }
}
