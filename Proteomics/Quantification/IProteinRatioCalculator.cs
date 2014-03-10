﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification
{
  public interface IProteinRatioCalculator
  {
    void Calculate(IIdentifiedResult mr, Func<IIdentifiedSpectrum, bool> validFunc);

    void Calculate(IIdentifiedProteinGroup group, Func<IIdentifiedSpectrum, bool> validFunc);

    bool HasPeptideRatio(IIdentifiedSpectrum spectrum);

    double CalculatePeptideRatio(IIdentifiedSpectrum spectrum);

    bool HasProteinRatio(IIdentifiedProtein protein);

    double GetProteinRatio(IIdentifiedProtein protein);

    string DetailDirectory { get; set; }

    string SummaryFileDirectory { get; set; }
  }
}
