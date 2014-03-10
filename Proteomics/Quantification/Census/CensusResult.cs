using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.Census
{
  public class CensusResult
  {
    private List<string> headers = new List<string>();

    private List<CensusProteinItem> proteins = new List<CensusProteinItem>();

    public List<string> Headers
    {
      get { return this.headers; }
      set
      {
        if (value == null)
        {
          this.headers = new List<string>();
        }
        else
        {
          this.headers = value;
        }
      }
    }

    public List<CensusProteinItem> Proteins
    {
      get { return this.proteins; }
      set
      {
        if (value == null)
        {
          this.proteins = new List<CensusProteinItem>();
        }
        else
        {
          this.proteins = value;
        }
      }
    }

    public List<CensusPeptideItem> GetPeptides()
    {
      var result = new HashSet<CensusPeptideItem>();
      foreach (CensusProteinItem protein in this.proteins)
      {
        result.UnionWith(protein.Peptides);
      }

      return new List<CensusPeptideItem>(result);
    }

    public void Recalculate()
    {
      foreach (CensusProteinItem cpi in Proteins)
      {
        cpi.Recalculate();
      }
    }

    public Dictionary<string, HashSet<CensusPeptideItem>> GetExperimentalPeptideMap()
    {
      var result = new Dictionary<string, HashSet<CensusPeptideItem>>();

      foreach (CensusProteinItem pro in Proteins)
      {
        foreach (CensusPeptideItem pep in pro.Peptides)
        {
          string exp = pep.Filename.Experimental;
          if (!result.ContainsKey(exp))
          {
            result[exp] = new HashSet<CensusPeptideItem>();
          }
          result[exp].Add(pep);
        }
      }

      return result;
    }
  }
}