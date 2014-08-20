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

    public bool RemovePrecusor { get; set; }

    private Aminoacids aas = new Aminoacids();

    public List<Pair<double, double>> FixIonRanges { get; private set; }

    public double MaxFixIonMz { get; private set; }

    public bool RemovePrecursorMinusLabel { get; private set; }

    public double LabelMass { get; private set; }

    public void Initialize()
    {
      FixIonRanges = new List<Pair<double, double>>();

      var ions = (from item in IsoType.Channels
                  select item.Mz).Union(IsoType.TagMass).OrderBy(m => m).ToList();

      Protease.InitializeByTag(IsoType.TagMass[0]);

      if (RemoveLowerRange)
      {
        var minIon = Protease.GetLowestBYFreeWindow() - MzTolerance;
        ions.RemoveAll(m => m < minIon);
        FixIonRanges.Add(new Pair<double, double>(0.0, Protease.GetLowestBYFreeWindow() - MzTolerance));

        foreach (var ion in ions)
        {
          FixIonRanges.Add(new Pair<double, double>(ion - MzTolerance, ion + MzTolerance));
        }
      }

      if (RemoveHighRange)
      {
        if (IsoType.TagMass[0] > Protease.GetHighBYFreeWindow())
        {
          RemovePrecursorMinusLabel = true;
          LabelMass = IsoType.TagMass[0];
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
