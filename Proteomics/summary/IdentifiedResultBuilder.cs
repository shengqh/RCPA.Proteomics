using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RCPA.Gui;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedResultBuilder : ProgressClass, IIdentifiedResultBuilder
  {
    private IStringParser<string> acParser;

    private string fastaFilename;

    public IdentifiedResultBuilder(IStringParser<string> acParser, string fastaFilename)
    {
      this.acParser = acParser;
      this.fastaFilename = fastaFilename;
    }

    #region IIdentifiedResultBuilder Members

    public IIdentifiedResult Build(List<IIdentifiedProteinGroup> groups)
    {
      IdentifiedResult result = new IdentifiedResult();

      result.AddRange(groups);
      result.Sort();
      result.BuildGroupIndex();

      if (File.Exists(fastaFilename))
      {
        IdentifiedResultUtils.FillSequenceFromFasta(acParser, fastaFilename, result, Progress);

        AveragePeptideMassCalculator massCalc = new AveragePeptideMassCalculator(new Aminoacids(), Atom.H.AverageMass, Atom.H.AverageMass + Atom.O.AverageMass);

        Progress.SetMessage("Calculating coverage and mass weight ...");
        foreach (IIdentifiedProteinGroup group in result)
        {
          foreach (IIdentifiedProtein protein in group)
          {
            protein.CalculateCoverage();
            protein.MolecularWeight = massCalc.GetMass(protein.Sequence);
          }
        }
      }

      return result;
    }

    #endregion
  }
}
