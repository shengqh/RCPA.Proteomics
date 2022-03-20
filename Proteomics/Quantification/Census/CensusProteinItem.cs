using MathNet.Numerics.Distributions;
using RCPA.Proteomics.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Quantification.Census
{
  public class CensusProteinItem : IAnnotation
  {
    private static readonly Regex proteinRegex =
      new Regex(@"P\s(\S+)\s([0-9.]+)\s([NA0-9.]*)\s([0-9.]+)\s([0-9]+)\s([0-9]+)\s(.+)");

    private Dictionary<string, object> annotations;
    private List<CensusPeptideItem> peptides;

    public string Locus { get; set; }

    public double AverageRatio { get; set; }

    public double StandardDeviation { get; set; }

    public double WeightedAverage { get; set; }

    public int PeptideNumber { get; set; }

    public int SpectraCount { get; set; }

    public string Description { get; set; }

    public List<CensusPeptideItem> Peptides
    {
      get
      {
        if (this.peptides == null)
        {
          this.peptides = new List<CensusPeptideItem>();
        }
        return this.peptides;
      }
    }

    #region IAnnotation Members

    public Dictionary<string, object> Annotations
    {
      get
      {
        if (this.annotations == null)
        {
          this.annotations = new Dictionary<string, object>();
        }
        return this.annotations;
      }
    }

    #endregion

    /// <summary>
    /// Validate peptides with same sequence, charge, ratio, sample intensity and reference intensity from same experimental. Those peptides share same 
    /// quantification chromotograph. Only the one with highest score will be marked as enabled. 
    /// </summary>
    public void ValidatePeptides()
    {
      if (this.peptides == null || this.peptides.Count == 0)
      {
        return;
      }

      this.peptides.Sort(delegate (CensusPeptideItem a, CensusPeptideItem b)
                           {
                             int result = a.Sequence.CompareTo(b.Sequence);
                             if (result != 0)
                             {
                               return result;
                             }

                             result = a.Filename.Experimental.CompareTo(b.Filename.Experimental);
                             if (result != 0)
                             {
                               return result;
                             }

                             result = a.Filename.Charge.CompareTo(b.Filename.Charge);
                             if (result != 0)
                             {
                               return result;
                             }

                             result = a.Ratio.CompareTo(b.Ratio);
                             if (result != 0)
                             {
                               return result;
                             }

                             result = a.SampleIntensity.CompareTo(b.SampleIntensity);
                             if (result != 0)
                             {
                               return result;
                             }

                             result = a.ReferenceIntensity.CompareTo(b.ReferenceIntensity);
                             if (result != 0)
                             {
                               return result;
                             }

                             result = -a.Score.CompareTo(b.Score);
                             if (result != 0)
                             {
                               return result;
                             }

                             return -a.DeltaScore.CompareTo(b.DeltaScore);
                           }
        );

      this.peptides[0].Enabled = true;
      for (int i = 1; i < this.peptides.Count; i++)
      {
        this.peptides[i].Enabled = !this.peptides[i].IsSameChro(this.peptides[i - 1]);
      }
    }

    public void Recalculate()
    {
      if (this.peptides.Count == 0)
      {
        SpectraCount = 0;
        PeptideNumber = 0;
        AverageRatio = 0;
        StandardDeviation = 0;
        WeightedAverage = 0;
        return;
      }

      SpectraCount = this.peptides.Count;

      var pepSeqs = new HashSet<string>();
      foreach (CensusPeptideItem item in this.peptides)
      {
        pepSeqs.Add(PeptideUtils.GetPureSequence(item.Sequence));
      }
      PeptideNumber = pepSeqs.Count;

      var pepRatio = new List<double>();
      double sumWeightedRatio = 0.0;
      double sumWeighted = 0.0;

      foreach (CensusPeptideItem item in this.peptides)
      {
        if (item.Ratio == -1 || item.Ratio == 0)
        {
          continue;
        }

        //if (item.Enabled)
        {
          pepRatio.Add(item.Ratio);
          sumWeightedRatio += item.Ratio * item.DeterminantFactor;
          sumWeighted += item.DeterminantFactor;
        }
      }
      var acc = new MeanStandardDeviation((IEnumerable<double>)pepRatio);
      AverageRatio = acc.Mean;
      if (double.IsNaN(acc.StdDev) || double.IsInfinity(acc.StdDev))
      {
        StandardDeviation = 0.0;
      }
      else
      {
        StandardDeviation = acc.StdDev;
      }

      if (sumWeighted != 0)
      {
        WeightedAverage = sumWeightedRatio / sumWeighted;
      }
      else
      {
        WeightedAverage = 0.0;
      }
    }

    public static CensusProteinItem Parse(string line)
    {
      Match m = proteinRegex.Match(line);
      if (!m.Success)
      {
        throw new Exception(
          "Cannot recognize protein line format, contact with author please. And, provide this line to author :" + line);
      }

      var result = new CensusProteinItem();

      result.Locus = m.Groups[1].Value;

      result.AverageRatio = MyConvert.ToDouble(m.Groups[2].Value);

      if (m.Groups[3].Length == 0 || m.Groups[3].Value.Equals("NA"))
      {
        result.StandardDeviation = 0;
      }
      else
      {
        result.StandardDeviation = MyConvert.ToDouble(m.Groups[3].Value);
      }

      result.WeightedAverage = MyConvert.ToDouble(m.Groups[4].Value);

      result.PeptideNumber = int.Parse(m.Groups[5].Value);

      result.SpectraCount = int.Parse(m.Groups[6].Value);

      result.Description = m.Groups[7].Value;

      return result;
    }
  }
}