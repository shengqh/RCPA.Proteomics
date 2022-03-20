using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public class ProteinFdrFilteredItem
  {
    private readonly HashSet<IIdentifiedSpectrum> acceptedSpectra = new HashSet<IIdentifiedSpectrum>();

    private readonly Dictionary<OptimalResultCondition, ProteinFdrScoreItem> scoreMap = new Dictionary<OptimalResultCondition, ProteinFdrScoreItem>();

    public int PeptideBeforeFdr { get; set; }

    public double PeptideFdr { get; set; }

    public int PeptideAfterFdr
    {
      get { return acceptedSpectra.Count + RejectedSpectra.Count; }
    }

    public string ProteinCondition { get; set; }

    public double ProteinFdr { get; set; }

    public int ProteinCount { get; set; }

    public int PeptideInProtein
    {
      get { return acceptedSpectra.Count; }
    }

    public HashSet<IIdentifiedSpectrum> AcceptedSpectra
    {
      get { return this.acceptedSpectra; }
    }

    public List<IIdentifiedSpectrum> RejectedSpectra { get; set; }

    public Dictionary<OptimalResultCondition, ProteinFdrScoreItem> ScoreMap
    {
      get { return this.scoreMap; }
    }

    public ProteinFdrFilteredItem()
    {
      this.RejectedSpectra = new List<IIdentifiedSpectrum>();
    }

    public override string ToString()
    {
      if (ProteinCondition == null)
      {
        return "-\t-\t-\t-\t-\t-";
      }
      else
      {
        return MyConvert.Format("{0:0.0000}\t{1}\t{2}\t{3:0.0000}\t{4}\t{5}",
          PeptideFdr,
          AcceptedSpectra.Count + RejectedSpectra.Count,
          ProteinCondition,
          ProteinFdr,
          ProteinCount,
          AcceptedSpectra.Count);
      }
    }
  }
}
