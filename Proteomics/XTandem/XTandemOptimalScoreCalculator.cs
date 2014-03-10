using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Gui;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Modification;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemOptimalScoreCalculator : OptimalResultCalculator
  {
    public XTandemOptimalScoreCalculator()
      : base(new XTandemScoreFunctions())
    { }
  }
}