using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Quantification.ITraq;

namespace RCPA.Proteomics.Processor
{
  public class PeakListRemoveIsobaricIonProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private PeakListRemoveIsobaricIonProcessorOptions options;

    public PeakListRemoveIsobaricIonProcessor(PeakListRemoveIsobaricIonProcessorOptions options)
    {
      this.options = options;
      this.options.Initialize();
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      t.SortByMz();

      //删除固定区间。
      var index = 0;
      while (index < t.Count)
      {
        var peak = t[index];

        if (peak.Mz > options.MaxFixIonMz)
        {
          break;
        }

        double mass;
        if (peak.Charge > 1)
        {
          mass = PrecursorUtils.MzToMass(peak.Mz, peak.Charge, true);
          if (mass > options.MaxFixIonMz)
          {
            index++;
            continue;
          }
        }
        else
        {
          mass = peak.Mz;
        }

        bool bRemoved = false;
        for (int i = 0; i < options.FixIonRanges.Count; i++)
        {
          var ion = options.FixIonRanges[i];

          if (mass < ion.First)
          {
            break;
          }

          if (mass > ion.Second)
          {
            continue;
          }

          bRemoved = true;
          t.RemoveAt(index);
          break;
        }

        if (!bRemoved)
        {
          index++;
        }
      }

      var precursorMass = PrecursorUtils.MzToMass(t.PrecursorMZ, t.PrecursorCharge, true);
      if (options.RemoveHighRange)
      {
        var maxPeak = precursorMass - options.Protease.GetHighBYFreeWindow() - options.MzTolerance;
        if (options.RemovePrecusor)
        {
          t.RemoveAll(peak =>
          {
            double mass = PrecursorUtils.MzToMass(peak.Mz, peak.Charge, true);
            return mass >= maxPeak;
          });
        }
        else
        {
          var precursorLess = precursorMass - options.MzTolerance;
          var precursorLarge = precursorMass + options.MzTolerance;
          t.RemoveAll(peak =>
          {
            double mass = PrecursorUtils.MzToMass(peak.Mz, peak.Charge, true);
            return (mass >= maxPeak && mass <= precursorLess) || mass > precursorLarge;
          });
        }
      }

      if (options.RemovePrecursorMinusLabel)
      {
        var ionmass = precursorMass - options.LabelMass;
        var minIonMass = ionmass - options.MzTolerance;
        var maxIonMass = ionmass + options.MzTolerance;
        t.RemoveAll(peak =>
        {
          double mass = PrecursorUtils.MzToMass(peak.Mz, peak.Charge, true);
          return mass >= minIonMass && mass <= maxIonMass;
        });
      }

      return t;
    }

    public override string ToString()
    {
      return options.ToString();
    }

    #endregion
  }
}
