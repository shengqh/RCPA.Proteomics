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
  /// 当过滤完unique two proteins以后，对于single-one-wonder，需要至少两个以上hits进行筛选。
  /// </summary>
  public class UniformSpectrumProteinFdrBuilder2 : AbstractUniformSpectrumProteinFdrBuilder
  {
    protected override ProteinFdrFilteredItem FilterOneHitWonders(IIdentifiedProteinGroupFilter conFilter, UniformProteinFdrOptimalResultCalculator proteinCalc)
    {
      Progress.SetMessage("Filtering PSMs by protein fdr {0} using peptide fdr {0} ...", Options.FalseDiscoveryRate.FdrValue);

      var countFilter = new IdentifiedProteinGroupPeptideCountFilter(Options.FalseDiscoveryRate.MinOneHitWonderPeptideCount);

      AndIdentifiedProteinGroupFilter groupFilter;
      if (conFilter != null)
      {
        groupFilter = new AndIdentifiedProteinGroupFilter(new IIdentifiedProteinGroupFilter[] { conFilter, countFilter });
      }
      else
      {
        groupFilter = new AndIdentifiedProteinGroupFilter(new IIdentifiedProteinGroupFilter[] { countFilter });
      }

      return proteinCalc.GetOptimalResultForGroupFilter(BuildResult, Options.FalseDiscoveryRate.FdrValue, Options.FalseDiscoveryRate.FdrValue, groupFilter);
    }
  }
}
