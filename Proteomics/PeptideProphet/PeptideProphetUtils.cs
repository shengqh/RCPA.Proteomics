using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RCPA.Proteomics.PeptideProphet
{
  public static class PeptideProphetUtils
  {
    public static List<ModificationAminoacidMass> ParseModificationAminoacidMass(XElement mod_info)
    {
      if (mod_info == null)
      {
        return null;
      }

      List<ModificationAminoacidMass> result = new List<ModificationAminoacidMass>();

      var modnterm = mod_info.Attribute("mod_nterm_mass");
      if (modnterm != null)
      {
        result.Add(new ModificationAminoacidMass()
        {
          Position = 0,
          Mass = MyConvert.ToDouble(modnterm.Value)
        });
      }

      var modaas = mod_info.FindDescendants("mod_aminoacid_mass");
      if (modaas.Count == 0)
      {
        return result;
      }

      foreach (var modaa in modaas)
      {
        var position = Convert.ToInt32(modaa.Attribute("position").Value);
        var mass = MyConvert.ToDouble(modaa.Attribute("mass").Value);
        result.Add(new ModificationAminoacidMass()
        {
          Position = position,
          Mass = mass
        });
      }

      result.Sort((m1, m2) => m1.Position.CompareTo(m2.Position));

      return result;
    }


    public static List<SequestPeptideProphetProteinItem> GetProteinsFromPeptides(
      List<SequestPeptideProphetItem> peptides)
    {
      var map = new Dictionary<string, SequestPeptideProphetProteinItem>();
      foreach (SequestPeptideProphetItem peptide in peptides)
      {
        foreach (string proteinName in peptide.SearchResult.Proteins)
        {
          if (!map.ContainsKey(proteinName))
          {
            var protein = new SequestPeptideProphetProteinItem();
            protein.Name = proteinName;
            protein.Peptides.Add(peptide);
            map[proteinName] = protein;
          }
          else
          {
            map[proteinName].Peptides.Add(peptide);
          }
        }
      }

      var result = new List<SequestPeptideProphetProteinItem>(map.Values);
      result.Sort();
      for (int i = 0; i < result.Count; i++)
      {
        SequestPeptideProphetProteinItem protein1 = result[i];
        for (int j = result.Count - 1; j >= i + 1; j--)
        {
          SequestPeptideProphetProteinItem protein2 = result[j];
          bool bContainAll = true;
          foreach (SequestPeptideProphetItem pItem2 in protein2.Peptides)
          {
            if (!protein1.Peptides.Contains(pItem2))
            {
              bContainAll = false;
              break;
            }
          }

          if (bContainAll)
          {
            result.RemoveAt(j);
          }
        }
      }

      return result;
    }

    public static List<MascotPeptideProphetProteinItem> GetProteinsFromPeptides(List<MascotPeptideProphetItem> peptides)
    {
      var map = new Dictionary<string, MascotPeptideProphetProteinItem>();
      foreach (MascotPeptideProphetItem peptide in peptides)
      {
        foreach (string proteinName in peptide.SearchResult.Proteins)
        {
          if (!map.ContainsKey(proteinName))
          {
            var protein = new MascotPeptideProphetProteinItem();
            protein.Name = proteinName;
            protein.Peptides.Add(peptide);
            map[proteinName] = protein;
          }
          else
          {
            map[proteinName].Peptides.Add(peptide);
          }
        }
      }
      var result = new List<MascotPeptideProphetProteinItem>(map.Values);
      result.Sort();
      for (int i = 0; i < result.Count; i++)
      {
        MascotPeptideProphetProteinItem protein1 = result[i];
        for (int j = result.Count - 1; j >= i + 1; j--)
        {
          MascotPeptideProphetProteinItem protein2 = result[j];
          bool bContainAll = true;
          foreach (MascotPeptideProphetItem pItem2 in protein2.Peptides)
          {
            if (!protein1.Peptides.Contains(pItem2))
            {
              bContainAll = false;
              break;
            }
          }

          if (bContainAll)
          {
            result.RemoveAt(j);
          }
        }
      }

      return result;
    }
  }
}