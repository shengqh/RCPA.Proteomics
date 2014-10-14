using RCPA.Proteomics.Quantification.IsobaricLabelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Processor
{
  public class PeakListRemoveIsobaricIonProcessorOptions
  {
    public IsobaricType IsoType { get; set; }

    public double MzTolerance { get; set; }

    public IIsobaricLabellingProtease Protease { get; set; }

    public bool RemoveLowerRange { get; set; }

    public bool RemoveHighRange { get; set; }

    public bool RemoveReporters { get; set; }

    public bool RemovePrecusor { get; set; }

    private Aminoacids aas = new Aminoacids();

    public List<Pair<double, double>> FixIonRanges { get; private set; }

    public double MaxFixIonMz { get; private set; }

    public bool RemovePrecursorMinusLabel { get; private set; }

    public double LabelMass { get; private set; }

    public void Initialize()
    {
      FixIonRanges = new List<Pair<double, double>>();

      var reporters = (from item in IsoType.Channels
                       select item.Mz).OrderBy(m => m).ToList();
      var reporterRange = new Pair<double, double>(reporters.First() - MzTolerance, reporters.Last() + MzTolerance);
      var tagRanges = (from tag in IsoType.TagMass
                       select new Pair<double, double>(tag - MzTolerance, tag + MzTolerance)).ToList();

      Protease.InitializeByTag(IsoType.TagMass[0]);

      if (RemoveLowerRange)
      {
        FixIonRanges.Add(new Pair<double, double>(0.0, Protease.GetLowestBYFreeWindow() - MzTolerance));
      }

      if (RemoveLowerRange || RemoveReporters)
      {
        FixIonRanges.Add(reporterRange);
        FixIonRanges.AddRange(tagRanges);
      }

      if (RemoveHighRange)
      {
        if (IsoType.TagMass[0] > Protease.GetHighBYFreeWindow())
        {
          RemovePrecursorMinusLabel = true;
          LabelMass = IsoType.TagMass[0];
        }
      }

      if (FixIonRanges.Count > 0)
      {
        FixIonRanges.Sort((m1, m2) => m1.First.CompareTo(m2.First));
        for (int i = FixIonRanges.Count - 1; i >= 0; i--)
        {
          for (int j = i - 1; j >= 0; j--)
          {
            if (FixIonRanges[j].Second >= FixIonRanges[i].Second)
            {
              FixIonRanges.RemoveAt(i);
              break;
            }
          }
        }
      }

      MaxFixIonMz = FixIonRanges.Count > 0 ? FixIonRanges.Last().Second : 0.0;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(string.Format("Protease={0}\n", Protease.ToString()));

      sb.Append(string.Format("RemoveIonWindow={0:0.####} Dalton\n", MzTolerance));
      sb.Append("RemoveIsobaricIon=" + IsoType.ToString() + ",");

      foreach (var ion in FixIonRanges)
      {
        sb.Append(string.Format("{0:0.####}-{1:0.####},", ion.First, ion.Second));
      }

      if (RemoveHighRange)
      {
        sb.Append(">=" + Protease.GetHighBYFreeWindowDescription() + ",");
      }

      if (RemovePrecursorMinusLabel)
      {
        sb.Append("Precursor-Label;");
      }
      return sb.ToString();
    }
  }
}
