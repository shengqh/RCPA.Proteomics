using System;
using System.Linq;
using System.Collections.Generic;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.IO;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18ProteinRatioRScanCalculator : AbstractO18ProteinRatioRCalculator
  {
    public O18ProteinRatioRScanCalculator(IGetRatioIntensity intensityFunc, IO18QuantificationOptions option)
      :base(intensityFunc, option)
    {    }

    protected override void PrepareIntensityFile(List<IIdentifiedSpectrum> spectra, string filename)
    {
      var pepfilename = Path.ChangeExtension(filename, ".pep.csv");
      using (StreamWriter sw = new StreamWriter(pepfilename))
      {
        sw.WriteLine("RefIntensity,SamIntensity,Peptide,FileScan");
        foreach (var pep in spectra)
        {
          if (pep.IsEnabled(true) && Option.IsPeptideRatioValid(pep))
          {
            string ratioFile = Option.GetRatioFile(pep);
            if (ratioFile == null)
            {
              continue;
            }

            O18QuantificationSummaryItem item = new O18QuantificationSummaryItemXmlFormat().ReadFromFile(ratioFile);
            item.CalculateIndividualAbundance();

            foreach (var envelope in item.ObservedEnvelopes)
            {
              if (!envelope.Enabled)
              {
                continue;
              }

              double refIntensity = envelope.SampleAbundance.O16;
              double sampleIntensity = envelope.SampleAbundance.O18;

              if (refIntensity == 0.0 || sampleIntensity == 0.0)
              {
                continue;
              }

              sw.WriteLine("{0},{1},{2},{3}", refIntensity, sampleIntensity, pep.Sequence, pep.Query.FileScan);
            }
          }
        }
      }
    }
  }
}