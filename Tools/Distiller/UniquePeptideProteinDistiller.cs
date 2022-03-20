using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Tools.Distiller
{
  public class UniquePeptideProteinDistiller : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string fileName)
    {
      var format = new SequestResultTextFormat();
      format.Progress = this.Progress;

      Progress.SetMessage("Reading identified result from " + fileName + " ...");
      IIdentifiedResult ir = format.ReadFromFile(fileName);

      Progress.SetMessage("Removing duplicated peptide ...");
      Progress.SetRange(0, ir.Count);
      for (int i = 0; i < ir.Count; i++)
      {
        Progress.SetPosition(i);
        IIdentifiedProteinGroup group = ir[i];
        List<IIdentifiedSpectrum> peps = UniquePeptideDistiller.KeepMaxScorePeptideOnly(group.GetPeptides());
        foreach (var protein in group)
        {
          protein.Peptides.RemoveAll(m => !peps.Contains(m.Spectrum));
        }
      }

      string resultFileName = fileName + ".unique";

      Progress.SetMessage("Saving proteins to " + resultFileName + " ...");
      format.WriteToFile(resultFileName, ir);

      List<IIdentifiedSpectrum> spectra = ir.GetSpectra();
      var peptideFormat = new SequestPeptideTextFormat(format.PeptideFormat.GetHeader());
      string peptideFileName = fileName + ".unique.peptides";

      Progress.SetMessage("Saving peptides to " + peptideFileName + " ...");
      peptideFormat.WriteToFile(peptideFileName, spectra);

      Progress.SetMessage("Finished.");
      return new[] { resultFileName, peptideFileName };
    }
  }
}
