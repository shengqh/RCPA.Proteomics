using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedResult : List<IIdentifiedProteinGroup>, IIdentifiedResult
  {
    public IdentifiedResult()
    {
      this.ProteinFDR = -1;
      this.PeptideFDR = -1;
    }

    #region IIdentifiedResult Members

    public double ProteinFDR { get; set; }
    public double PeptideFDR { get; set; }

    public List<IIdentifiedProtein> GetProteins()
    {
      List<IIdentifiedProtein> result = new List<IIdentifiedProtein>();

      ForEach(mpg => { result.AddRange(mpg); });

      return result;
    }

    public List<IIdentifiedSpectrum> GetSpectra()
    {
      Dictionary<string, IIdentifiedSpectrum> pepMap = new Dictionary<string, IIdentifiedSpectrum>();

      ForEach(mpg =>
      {
        foreach (IIdentifiedSpectrum mp in mpg.GetPeptides())
        {
          string filename = mp.Query.FileScan.LongFileName;
          if (!pepMap.ContainsKey(filename))
          {
            pepMap[filename] = mp;
          }
        }
      });

      return new List<IIdentifiedSpectrum>(pepMap.Values);
    }

    public void InitUniquePeptideCount()
    {
      ForEach(group => { group.InitUniquePeptideCount(); });
    }

    public void BuildGroupIndex()
    {
      int i = 0;
      ForEach(group => { group.Index = ++i; });
    }

    public Dictionary<IIdentifiedSpectrum, List<IIdentifiedProteinGroup>> GetPeptideProteinGroupMap()
    {
      Dictionary<IIdentifiedSpectrum, List<IIdentifiedProteinGroup>> result = new Dictionary<IIdentifiedSpectrum, List<IIdentifiedProteinGroup>>();

      ForEach(mpg =>
      {
        foreach (IIdentifiedSpectrum mph in mpg.GetPeptides())
        {
          if (!result.ContainsKey(mph))
          {
            result[mph] = new List<IIdentifiedProteinGroup>();
          }
          result[mph].Add(mpg);
        }
      });

      return result;
    }

    public Dictionary<string, HashSet<IIdentifiedSpectrum>> GetExperimentalPeptideMap()
    {
      Dictionary<string, HashSet<IIdentifiedSpectrum>> result = new Dictionary<string, HashSet<IIdentifiedSpectrum>>();

      GetSpectra().ForEach(pep =>
      {
        string exp = pep.Query.FileScan.Experimental;
        if (!result.ContainsKey(exp))
        {
          result[exp] = new HashSet<IIdentifiedSpectrum>();
        }
        result[exp].Add(pep);
      });

      return result;
    }

    public void Filter(Predicate<IIdentifiedPeptide> match)
    {
      if (match == null)
      {
        return;
      }

      ForEach(group =>
      {
        int maxCount = 0;
        foreach (IIdentifiedProtein protein in group)
        {
          protein.Peptides.RemoveAll(m =>
          {
            return !match(m);
          });
          maxCount = Math.Max(maxCount, protein.Peptides.Count);
        }

        for (int i = group.Count - 1; i >= 0; i--)
        {
          if (maxCount > group[i].Peptides.Count)
          {
            group.RemoveAt(i);
          }
        }
      });

      RemoveAll(group =>
      {
        return group.GetPeptides().Count == 0;
      });

      InitUniquePeptideCount();

      BuildGroupIndex();
    }

    public HashSet<string> GetExperimentals()
    {
      HashSet<string> result = new HashSet<string>();
      GetSpectra().ForEach(pep =>
      {
        result.Add(pep.Query.FileScan.Experimental);
      });
      return result;
    }

    /// <summary>
    /// Remove peptide-spectrum-match which belongs to multiple protein groups
    /// </summary>
    public void RemoveAmbiguousSpectra()
    {
      //remove empty group
      this.RemoveAll(l => l.Count == 0 || l[0].Peptides.Count == 0);

      //assign all PSM with group count = 0
      foreach (var group in this)
      {
        foreach (var spec in group[0].Peptides)
        {
          spec.Spectrum.GroupCount = 0;
        }
      }

      //sum up the group count for each PSM
      foreach (var group in this)
      {
        foreach (var spec in group[0].Peptides)
        {
          spec.Spectrum.GroupCount += 1;
        }
      }

      //remove the PSM mapped to multiple protein group
      foreach (var group in this)
      {
        foreach (var protein in group)
        {
          protein.Peptides.RemoveAll(l => l.Spectrum.GroupCount > 1);
        }
      }

      //remove the empty group
      this.RemoveAll(l => l[0].Peptides.Count == 0);
    }

    #endregion

    #region IAnnotation Members

    private Dictionary<string, object> annotations = new Dictionary<string, object>();

    public Dictionary<string, object> Annotations
    {
      get { return annotations; }
    }

    #endregion
  }
}
