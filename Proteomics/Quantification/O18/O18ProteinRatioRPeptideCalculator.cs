using System;
using System.Linq;
using System.Collections.Generic;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.IO;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18ProteinRatioRPeptideCalculator : AbstractO18ProteinRatioRCalculator
  {
    public O18ProteinRatioRPeptideCalculator(IGetRatioIntensity intensityFunc, IO18QuantificationOption option)
      :base(intensityFunc, option)
    {    }

    protected override void PrepareIntensityFile(List<IIdentifiedSpectrum> spectra, string filename)
    {
      using (StreamWriter sw = new StreamWriter(filename))
      {
        sw.WriteLine("RefIntensity,SamIntensity,Peptide");
        foreach (var sp in spectra)
        {
          sw.WriteLine("{0},{1},{2}", intensityFunc.GetReferenceIntensity(sp),
             intensityFunc.GetSampleIntensity(sp),
             sp.Peptide.Sequence);
        }
      }
    }
  }
}