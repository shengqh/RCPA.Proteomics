using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacProteinRatioCalculator : IProteinRatioCalculator
  {
    public void Calculate(IIdentifiedProteinGroup proteinGroup, Func<IIdentifiedSpectrum, bool> validFunc)
    {
      List<IIdentifiedSpectrum> spectra = (from s in proteinGroup[0].GetSpectra()
                                           where s.GetRatioEnabled() && validFunc(s)
                                           select s).ToList();

      QuantificationItem item;
      if (spectra.Count == 1)
      {
        item = spectra[0].GetQuantificationItem();
      }
      else if (spectra.Count > 1)
      {
        var sampleIntensities = new List<double>();
        var refIntensities = new List<double>();

        spectra.ForEach(m =>
        {
          sampleIntensities.Add(m.GetQuantificationItem().SampleIntensity);
          refIntensities.Add(m.GetQuantificationItem().ReferenceIntensity);
        });

        var intensities = new double[2][];
        intensities[0] = refIntensities.ToArray();
        intensities[1] = sampleIntensities.ToArray();
        LinearRegressionRatioResult lrrr = LinearRegressionRatioCalculator.CalculateRatio(intensities);

        item = new QuantificationItem();
        item.Ratio = lrrr.Ratio;
        item.Correlation = lrrr.RSquare;

        double sampleIntensity, refIntensity;
        if (lrrr.Ratio > 1)
        {
          sampleIntensity = sampleIntensities.Max();
          refIntensity = sampleIntensity / lrrr.Ratio;
        }
        else
        {
          refIntensity = refIntensities.Max();
          sampleIntensity = refIntensity * lrrr.Ratio;
        }

        item.SampleIntensity = sampleIntensity;
        item.ReferenceIntensity = refIntensity;
        item.Enabled = true;
      }
      else
      {
        item = new QuantificationItem();
        item.Enabled = false;
      }

      foreach (IIdentifiedProtein protein in proteinGroup)
      {
        protein.SetQuantificationItem(item);
      }
    }

    public bool HasPeptideRatio(IIdentifiedSpectrum spectrum)
    {
      return spectrum.GetQuantificationItem() != null;
    }

    public double CalculatePeptideRatio(IIdentifiedSpectrum spectrum)
    {
      return spectrum.GetQuantificationItem().Ratio;
    }

    public bool HasProteinRatio(IIdentifiedProtein protein)
    {
      return protein.GetQuantificationItem() != null;
    }

    public double GetProteinRatio(IIdentifiedProtein protein)
    {
      if (HasProteinRatio(protein))
      {
        return protein.GetQuantificationItem().Ratio;
      }
      else
      {
        return double.NaN;
      }
    }

    public string DetailDirectory { get; set; }


    public string SummaryFileDirectory
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public void Calculate(IIdentifiedResult mr, Func<IIdentifiedSpectrum, bool> validFunc)
    {
      foreach (var mg in mr)
      {
        Calculate(mg, validFunc);
      }
    }
  }
}