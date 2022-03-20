using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  /// <summary>
  /// Griffin, N. M., Yu, J., Long, F., Oh, P., Shore, S., Li, Y., Koziol, J. A. and Schnitzer, J. E. (2010) Label-free, normalized quantification of complex mass spectrometry data for proteomic analysis. Nat Biotechnol 28, 83-9.
  /// </summary>
  public class SInProteinLabelfreeQuantificationCalculator : IProteinLabelfreeQuantificationCalculator
  {
    #region IProteinLabelfreeQuantificationCalculator Members

    public void Calculate(IIdentifiedResult proteins, Dictionary<string, List<string>> datasets)
    {
      if (proteins == null || proteins.Count == 0)
      {
        throw new ArgumentNullException("Argument proteins cannot be null or empty in SInProteinLabelfreeQuantificationCalculator.Calculate");
      }

      if (proteins.Count > 0 && proteins.Any(m => m[0].Sequence == null))
      {
        throw new Exception("SIn Quantification Calculator needs protein sequence information.");
      }

      var spectra = proteins.GetSpectra();
      if (spectra.Any(m => m.MatchedTIC == 0))
      {
        throw new Exception("SIn Quantification Calculator needs matched ion count information.");
      }

      //build experimental/dataset map
      var expDatasetMap = new Dictionary<string, string>();
      foreach (var ds in datasets)
      {
        ds.Value.ForEach(m => expDatasetMap[m] = ds.Key);
      }

      var minTic = spectra.Min(m => m.MatchedTIC) / 6;

      //group spectra by dataset
      var spectraDatasetGroup = spectra.GroupBy(m => expDatasetMap[m.Query.FileScan.Experimental]);

      //build dataset/sum(tic) map
      var datasetSumOfTicMap = new Dictionary<string, double>();
      foreach (var entry in spectraDatasetGroup)
      {
        datasetSumOfTicMap[entry.Key] = entry.Sum(m => m.MatchedTIC);
      }

      //calculate SIn of each protein
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

      foreach (var dataset in datasets.Keys)
      {
        List<string> exps = datasets[dataset];

        double sumOfTic = datasetSumOfTicMap[dataset];
        foreach (var g in proteins)
        {
          var lr = g[0].GetLabelfreeResult();
          var lv = lr[dataset];

          //get spectra in current dataset
          var specs = (from p in g.GetPeptides()
                       where exps.Contains(p.Query.FileScan.Experimental)
                       select p).ToList();

          if (specs.Count > 0)
          {
            lv.Count = specs.Count();
            //get SI of the protein in current dataset
            lv.Value = specs.Sum(m => m.MatchedTIC);
          }
          else
          {
            lv.Count = 0;
            //set as minimum TIC value.
            lv.Value = minTic;
          }

          //SI(GI) = SI / sumOfTic
          //SI(N) = SI(GI) / Length(Protein)
          lv.Value = Math.Log10(lv.Value / sumOfTic / g[0].Sequence.Length);
        }
      }
    }

    #endregion

    public override string ToString()
    {
      return "Normalized Spectral Index Calculator";
    }

    #region IProteinLabelfreeQuantificationCalculator Members


    public string GetExtension()
    {
      return "SIn";
    }

    #endregion
  }
}
