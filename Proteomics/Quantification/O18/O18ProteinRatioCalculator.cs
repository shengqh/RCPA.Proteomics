using System;
using System.Linq;
using System.Collections.Generic;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18ProteinRatioCalculator : IProteinRatioCalculator
  {
    private readonly IGetRatioIntensity intensityFunc;

    public O18ProteinRatioCalculator(IGetRatioIntensity intensityFunc)
    {
      this.intensityFunc = intensityFunc;
    }

    public O18ProteinRatioCalculator()
      : this(new O18GetRatioIntensity())
    { }

    public IGetRatioIntensity IntensityFunc
    {
      get { return this.intensityFunc; }
    }

    public void Calculate(IIdentifiedResult mr, Func<IIdentifiedSpectrum, bool> validFunc)
    {
      foreach (IIdentifiedProteinGroup mpg in mr)
      {
        Calculate(mpg, validFunc);
      }
    }

    public void Calculate(IIdentifiedProteinGroup proteinGroup, Func<IIdentifiedSpectrum, bool> validFunc)
    {
      List<IIdentifiedSpectrum> spectra = (from s in proteinGroup[0].GetSpectra()
                                           where validFunc(s) && s.IsEnabled(true) && HasPeptideRatio(s)
                                           select s).ToList();

      if (spectra.Count == 1)
      {
        var r = CalculatePeptideRatio(spectra[0]);
        foreach (var protein in proteinGroup)
        {
          protein.SetEnabled(true);

          protein.Annotations[O18QuantificationConstants.O18_RATIO] = new LinearRegressionRatioResult(r, 0.0) { PointCount = 1, FCalculatedValue = 0, FProbability = 1 };
          protein.Annotations[this.intensityFunc.ReferenceKey] = spectra[0].Annotations[this.intensityFunc.ReferenceKey];
          protein.Annotations[this.intensityFunc.SampleKey] = spectra[0].Annotations[this.intensityFunc.SampleKey];
        }
      }
      else if (spectra.Count > 1)
      {
        var intensities = this.intensityFunc.ConvertToArray(spectra);

        double maxA = intensities[0].Max();
        double maxB = intensities[1].Max();

        LinearRegressionRatioResult ratioResult;

        if (maxA == 0.0)
        {
          ratioResult = new LinearRegressionRatioResult(20, 0.0) { PointCount = intensities.Count(), FCalculatedValue = 0, FProbability = 0 };
          maxA = maxB / ratioResult.Ratio;
        }
        else
        {
          if (maxB == 0.0)
          {
            ratioResult = new LinearRegressionRatioResult(0.05, 0.0) { PointCount = intensities.Count(), FCalculatedValue = 0, FProbability = 0 };
          }
          else
          {
            ratioResult = LinearRegressionRatioCalculator.CalculateRatio(intensities);
          }
          maxB = maxA * ratioResult.Ratio;
        }

        foreach (IIdentifiedProtein protein in proteinGroup)
        {
          protein.SetEnabled(true);

          protein.Annotations[O18QuantificationConstants.O18_RATIO] = ratioResult;
          protein.Annotations[this.intensityFunc.ReferenceKey] = maxA;
          protein.Annotations[this.intensityFunc.SampleKey] = maxB;
        }
      }
      else
      {
        foreach (IIdentifiedProtein protein in proteinGroup)
        {
          protein.SetEnabled(false);
          protein.Annotations.Remove(O18QuantificationConstants.O18_RATIO);
          protein.Annotations.Remove(this.intensityFunc.ReferenceKey);
          protein.Annotations.Remove(this.intensityFunc.SampleKey);
        }
      }
    }

    public double CalculatePeptideRatio(IIdentifiedSpectrum mph)
    {
      double o16 = this.intensityFunc.GetReferenceIntensity(mph);
      double o18 = this.intensityFunc.GetSampleIntensity(mph);

      double result = 0;
      if (o16 == 0)
      {
        result = 20;
      }
      else if (o18 == 0)
      {
        result = 0.05;
      }
      else
      {
        result = o18 / o16;
      }

      result = Math.Min(20, Math.Max(result, 0.05));

      return result;
    }

    public bool HasPeptideRatio(IIdentifiedSpectrum spectrum)
    {
      return intensityFunc.HasRatio(spectrum);
    }

    public bool HasProteinRatio(IIdentifiedProtein protein)
    {
      if (!protein.Annotations.ContainsKey(O18QuantificationConstants.O18_RATIO))
      {
        return false;
      }

      return protein.Annotations[O18QuantificationConstants.O18_RATIO] is LinearRegressionRatioResult;
    }

    public double GetProteinRatio(IIdentifiedProtein protein)
    {
      if (HasProteinRatio(protein))
      {
        return (protein.Annotations[O18QuantificationConstants.O18_RATIO] as LinearRegressionRatioResult).Ratio;
      }
      else
      {
        return double.NaN;
      }
    }

    public string DetailDirectory{get;set;}

    public string SummaryFileDirectory{get;set;}
  }
}