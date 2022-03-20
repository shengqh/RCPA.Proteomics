using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ExtendSilacProteinRatioCalculator
  {
    public QuantificationItem Calculate(IIdentifiedProteinGroup proteinGroup, Func<IIdentifiedSpectrum, bool> validFunc)
    {
      List<IIdentifiedSpectrum> spectra = (from s in proteinGroup[0].GetSpectra()
                                           where s.IsEnabled(true) && validFunc(s)
                                           select s).ToList();

      if (spectra.Count == 1)
      {
        var item = spectra[0].GetQuantificationItem();
        return new QuantificationItem()
        {
          Enabled = true,
          Ratio = item.Ratio,
          Correlation = item.Correlation,
          SampleIntensity = item.SampleIntensity,
          ReferenceIntensity = item.ReferenceIntensity
        };
      }

      if (spectra.Count > 1)
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

        double sampleIntensity, refIntensity;
        if (lrrr.Ratio > 1)
        {
          sampleIntensities.Sort();
          sampleIntensity = sampleIntensities[sampleIntensities.Count - 1];
          refIntensity = sampleIntensity / lrrr.Ratio;
        }
        else
        {
          refIntensities.Sort();
          refIntensity = refIntensities[refIntensities.Count - 1];
          sampleIntensity = refIntensity * lrrr.Ratio;
        }

        return new QuantificationItem()
        {
          Enabled = true,
          Ratio = lrrr.Ratio,
          Correlation = lrrr.RSquare,
          SampleIntensity = sampleIntensity,
          ReferenceIntensity = refIntensity
        };
      }

      return new QuantificationItem()
      {
        Enabled = false
      };
    }
  }
}