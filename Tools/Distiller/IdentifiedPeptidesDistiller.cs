using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Tools.Distiller
{
  public class IdentifiedPeptidesDistiller : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string filename)
    {
      string resultFile = FileUtils.ChangeExtension(filename, "peptides");
      using (IdentifiedProteinGroupEnumerator iter = new IdentifiedProteinGroupEnumerator(filename, true))
      {
        using (StreamWriter sw = new StreamWriter(resultFile))
        {
          sw.WriteLine(iter.PeptideFormat.GetHeader());
          HashSet<string> saved = new HashSet<string>();
          HashSet<string> uniquepeptides = new HashSet<string>();
          while (iter.MoveNext())
          {
            IIdentifiedProteinGroup group = iter.Current;
            List<IIdentifiedSpectrum> spectra = group[0].GetSpectra();
            spectra.ForEach(delegate (IIdentifiedSpectrum spectrum)
            {
              string dta = spectrum.Query.FileScan.LongFileName;
              if (!saved.Contains(dta))
              {
                saved.Add(dta);
                uniquepeptides.Add(spectrum.Peptide.PureSequence);
                sw.WriteLine(iter.PeptideFormat.GetString(spectrum));
              }
            });
          }

          sw.WriteLine();

          sw.WriteLine("----- summary -----");
          sw.WriteLine("Total peptides: {0}", saved.Count);
          sw.WriteLine("Unique peptides: {0}", uniquepeptides.Count);
        }
      }

      return new[] { resultFile };
    }
  }
}
