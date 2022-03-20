﻿using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Image
{
  public interface IIdentifiedPeptideResult
  {
    string Peptide { get; set; }

    PeakList<MatchedPeak> ExperimentalPeakList { get; set; }

    Dictionary<IonType, List<MatchedPeak>> GetIonSeries();

    bool IsBy2Matched();

    Dictionary<string, string> ScoreMap { get; }

    Dictionary<char, double> DynamicModification { get; }
  }
}
