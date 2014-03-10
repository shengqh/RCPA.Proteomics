using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;

namespace RCPA.Proteomics.Summary.Uniform
{
  /// <summary>
  /// 当过滤完unique two proteins以后，对于single-one-wonder，使用unique peptide fdr进行筛选。
  /// </summary>
  public class UniformSpectrumProteinFdrBuilder : AbstractUniformSpectrumProteinFdrBuilder
  {
    protected override ProteinFdrFilteredItem FilterOneHitWonders(IIdentifiedProteinGroupFilter conFilter, UniformProteinFdrOptimalResultCalculator proteinCalc)
    {
      var oldLevel = Options.FalseDiscoveryRate.FdrLevel;
      try
      {
        Progress.SetMessage("Filtering PSMs by protein fdr {0} using unique peptide fdr {0} ...", Options.FalseDiscoveryRate.FdrValue);

        Options.FalseDiscoveryRate.FdrLevel = FalseDiscoveryRateLevel.UniquePeptide;

        return proteinCalc.GetOptimalResultForGroupFilter(BuildResult, Options.FalseDiscoveryRate.FdrValue, Options.FalseDiscoveryRate.FdrValue, conFilter);
      }
      finally
      {
        Options.FalseDiscoveryRate.FdrLevel = oldLevel;
      }
    }
  }
}
