using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public class DtaselectProteinItem
  {
    public string Name { get; set; }

    public string Context { get; set; }

    private List<DtaselectPeptideItem> peptides = new List<DtaselectPeptideItem>();

    public List<DtaselectPeptideItem> Peptides
    {
      get
      {
        return peptides;
      }
    }

    public Dictionary<string, DtaselectPeptideItem> GetPeptideMap()
    {
      Dictionary<string, DtaselectPeptideItem> result = new Dictionary<string, DtaselectPeptideItem>();
      foreach (DtaselectPeptideItem peptide in peptides)
      {
        result[peptide.Filename] = peptide;
      }
      return result;
    }

  }
}
