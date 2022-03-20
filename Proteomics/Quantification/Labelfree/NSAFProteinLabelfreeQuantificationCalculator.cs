using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  /// <summary>
  /// Zybailov, B. et al. Statistical analysis of membrane proteome expression changes in Saccharomyces cerevisiae . J. Proteome Res. 5, 2339–2347 (2006). 
  /// </summary>
  public class NSAFProteinLabelfreeQuantificationCalculator : IProteinLabelfreeQuantificationCalculator
  {
    #region IProteinLabelfreeQuantificationCalculator Members

    public void Calculate(IIdentifiedResult proteins, Dictionary<string, List<string>> datasets)
    {
      if (proteins == null || proteins.Count == 0)
      {
        throw new ArgumentNullException("Argument proteins cannot be null or empty in NSAFProteinLabelfreeQuantificationCalculator.Calculate");
      }

      if (proteins.Count > 0 && proteins.Any(m => m[0].Sequence == null))
      {
        throw new Exception("NSAF Quantification Calculator needs protein sequence information.");
      }

      foreach (var g in proteins)
      {
        var lr = new LabelfreeResult();
        foreach (var key in datasets.Keys)
        {
          lr[key] = new LabelfreeValue();
        }

        foreach (var p in g)
        {
          p.SetLabelfreeResult(lr);
        }
      }

      double zeroCount = 0.16;
      foreach (var key in datasets.Keys)
      {
        List<string> exps = datasets[key];

        double allnsaf = 0.0;
        foreach (var g in proteins)
        {
          var lr = g[0].GetLabelfreeResult();
          var lv = lr[key];
          lv.Count = g.GetPeptides().Count(m => exps.Contains(m.Query.FileScan.Experimental));
          var sc = (double)(lv.Count);
          lv.Value = sc / g[0].Sequence.Length;
          allnsaf += lv.Value;
        }

        foreach (var g in proteins)
        {
          var lr = g[0].GetLabelfreeResult();
          var lv = lr[key];
          var nsaf = lv.Value;
          if (nsaf == 0)
          {
            nsaf = zeroCount / g[0].Sequence.Length;
          }
          lv.Value = Math.Log(nsaf / allnsaf);
        }
      }
    }

    #endregion

    public override string ToString()
    {
      return "NSAF Calculator";
    }

    #region IProteinLabelfreeQuantificationCalculator Members


    public string GetExtension()
    {
      return "nsaf";
    }

    #endregion
  }
}
