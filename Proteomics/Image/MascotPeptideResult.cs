using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.IO;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Fragmentation;
using RCPA.Proteomics.Utils;

namespace RCPA.Proteomics.Image
{
  public class MascotPeptideResult : IIdentifiedPeptideResult
  {
    public bool PrecursorMonoMass { get; set; }

    public bool PeakMonoMass { get; set; }

    public Dictionary<char, double> StaticModification { get; set; }

    #region IIdentifiedPeptideResult Members

    public string Peptide { get; set; }

    public PeakList<MatchedPeak> ExperimentalPeakList { get; set; }

    private Dictionary<IonType, List<MatchedPeak>> ionSeries;
    public Dictionary<IonType, List<MatchedPeak>> GetIonSeries()
    {
      return ionSeries;
    }

    public bool IsBy2Matched()
    {
      return ExperimentalPeakList.Any(m => m.Matched && (m.PeakType == IonType.B2 || m.PeakType == IonType.Y2));
    }

    public Dictionary<string, string> ScoreMap { get; set; }

    public Dictionary<char, double> DynamicModification { get; set; }

    #endregion

    private void InitTheoreticalPeaks()
    {
      Aminoacids aas = new Aminoacids();

      aas.SetModification(StaticModification);
      aas.SetModification(DynamicModification);

      CIDFragmentationBuilder<MatchedPeak> builder = new CIDFragmentationBuilder<MatchedPeak>(ExperimentalPeakList.PrecursorCharge, aas);

      ionSeries = builder.GetIonSeries(PeptideUtils.GetMatchedSequence(Peptide));
    }
  }
}
