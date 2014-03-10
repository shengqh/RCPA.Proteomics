using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Distribution
{
  public class ModifiedPeptideDistributionCalculator : PeptideDistributionCalculator
  {
    private Regex modRegex;

    private string modifiedAminoacids;

    public ModifiedPeptideDistributionCalculator(string modifiedAminoacids, bool exportIndividual)
      : base(exportIndividual)
    {
      this.modifiedAminoacids = modifiedAminoacids;
      this.modRegex = new Regex(MyConvert.Format("[{0}][^A-Z.]", modifiedAminoacids));
    }

    public bool IsModified(IIdentifiedSpectrum spectrum)
    {
      return modRegex.Match(spectrum.Sequence).Success;
    }

    protected override IEnumerable<IIdentifiedSpectrum> FilterSpectrum(List<IIdentifiedSpectrum> spectra)
    {
      return spectra.FindAll(m => IsModified(m));
    }
  }
}
