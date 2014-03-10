using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using RCPA.Utils;

namespace RCPA.Tools.Distiller
{
  public class UniquePeptideDistiller : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string fileName)
    {
      IFileFormat<List<IIdentifiedSpectrum>> format = new SequestPeptideTextFormat();

      List<IIdentifiedSpectrum> spectra = format.ReadFromFile(fileName);

      List<IIdentifiedSpectrum> result = KeepMaxScorePeptideOnly(spectra);

      string resultFileName = fileName + ".unique";

      format.WriteToFile(resultFileName, result);

      return new[] { resultFileName };
    }

    public static List<IIdentifiedSpectrum> KeepMaxScorePeptideOnly(IEnumerable<IIdentifiedSpectrum> oldSpectra)
    {
      var binMap = oldSpectra.GroupBy(m => m.Sequence + "_" + m.Charge);

      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();

      foreach (var spectra in binMap)
      {
        double maxValue = spectra.Max(m => m.Score);

        result.Add(spectra.First(m => m.Score == maxValue));
      }

      return result;
    }
  }
}
