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
  public class MSAmandaRank2Parser : MSAmandaParser
  {
    public MSAmandaRank2Parser() { }

    protected override void FilterSpectra(List<IIdentifiedSpectrum> result)
    {
      var group = result.GroupBy(m => m.Annotations[TITLE_KEY] as string).ToArray();
      result.Clear();
      foreach (var g in group)
      {
        var lst = g.OrderBy(m => m.Rank).ToList();
        for (int i = 1; i < lst.Count; i++)
        {
          if (lst[i].Rank > 1 && !lst[i].Peptide.PureSequence.Equals(lst[0].Peptide.PureSequence))
          {
            result.Add(lst[i]);
            break;
          }
        }
      }
    }
  }
}
