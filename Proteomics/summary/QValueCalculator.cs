using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public class QValueCalculator
  {
    private IScoreFunction scoreFunc;

    private IFalseDiscoveryRateCalculator fdrCalc;

    public QValueCalculator(IScoreFunction scoreFunc, IFalseDiscoveryRateCalculator fdrCalc)
    {
      this.scoreFunc = scoreFunc;
      this.fdrCalc = fdrCalc;
    }

    public void CalculateQValue(List<IIdentifiedSpectrum> spectra)
    {
      IdentifiedSpectrumUtils.CalculateQValue(spectra, scoreFunc, fdrCalc);
    }

    public void CalculateQValue(Dictionary<OptimalResultCondition, List<IIdentifiedSpectrum>> peptideBin)
    {
      foreach (var spectra in peptideBin.Values)
      {
        IdentifiedSpectrumUtils.CalculateQValue(spectra, scoreFunc, fdrCalc);
      }
    }

    public void CalculateUniqueQValue(Dictionary<OptimalResultCondition, List<IIdentifiedSpectrum>> peptideBin)
    {
      foreach (var spectra in peptideBin.Values)
      {
        IdentifiedSpectrumUtils.CalculateUniqueQValue(spectra, scoreFunc, fdrCalc);
      }
    }
  }
}
