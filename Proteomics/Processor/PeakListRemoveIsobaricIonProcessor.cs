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
    private IsobaricType iType;
    private double mzTolerance;
    private List<Pair<double, double>> fixIonRanges;
    private double maxFixIonMz;
    private IIsobaricLabellingProtease protease;
    private Aminoacids aas = new Aminoacids();
    private bool bRemovePrecursorMinusLabel = false;
    private double labelMass = 0.0;
    private bool removeLowerRange;
    private bool removeHighRange;

    public PeakListRemoveIsobaricIonProcessor(IsobaricType iType, double mzTolerance, IIsobaricLabellingProtease protease, bool removeLowerRange, bool removeHighRange)
    {
      this.iType = iType;
      this.mzTolerance = mzTolerance;
      this.protease = protease;
      this.removeLowerRange = removeLowerRange;
      this.removeHighRange = removeHighRange;

      fixIonRanges = new List<Pair<double, double>>();
      var def = iType.GetDefinition();
      var ions = (from item in def.Items
                  select item.Mass).Union(from tm in def.TagMass
                                          select tm).OrderBy(m => m).ToList();

      protease.InitializeByTag(def.TagMass[0]);

      if (removeLowerRange)
      {
        ions.RemoveAll(m => m < protease.GetLowestBYFreeWindow());
        fixIonRanges.Add(new Pair<double, double>(0.0, protease.GetLowestBYFreeWindow() - mzTolerance));

        foreach (var ion in ions)
        {
          fixIonRanges.Add(new Pair<double, double>(ion - mzTolerance, ion + mzTolerance));
        }
      }

      if (removeHighRange)
      {
        if (def.TagMass[0] > protease.GetHighBYFreeWindow())
        {
          bRemovePrecursorMinusLabel = true;
          labelMass = def.TagMass[0];
        }
      }

      maxFixIonMz = fixIonRanges.Count > 0 ? fixIonRanges.Last().Second : 0.0;
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

        if (peak.Mz > maxFixIonMz)
        {
          break;
        }

        double mass;
        if (peak.Charge > 1)
        {
          mass = peak.Mz * peak.Charge - (Atom.H.MonoMass - 1) * peak.Charge;
          if (mass > maxFixIonMz)
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
        for (int i = 0; i < fixIonRanges.Count; i++)
        {
          var ion = fixIonRanges[i];

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
      if (removeHighRange)
      {
        var maxPeak = precursorMass - protease.GetHighBYFreeWindow() - mzTolerance;
        t.RemoveAll(peak =>
        {
          double mass = PrecursorUtils.MzToMass(peak.Mz, peak.Charge, true);
          return mass >= maxPeak;
        });
      }

      if (bRemovePrecursorMinusLabel)
      {
        var ionmass = precursorMass - labelMass;
        var minIonMass = ionmass - mzTolerance;
        var maxIonMass = ionmass + mzTolerance;
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
      StringBuilder sb = new StringBuilder();

      sb.Append(string.Format("Protease={0}\n", protease.ToString()));

      sb.Append(string.Format("RemoveIonWindow={0:0.####} Dalton\n", mzTolerance));
      sb.Append("RemoveIsobaricIon=" + iType.ToString() + ",");

      foreach (var ion in fixIonRanges)
      {
        sb.Append(string.Format("{0:0.####}-{1:0.####},", ion.First, ion.Second));
      }

      if (removeHighRange)
      {
        sb.Append(">=" + protease.GetHighBYFreeWindowDescription() + ",");
      }

      if (bRemovePrecursorMinusLabel)
      {
        sb.Append("Precursor-Label;");
      }
      return sb.ToString();
    }

    #endregion
  }
}
