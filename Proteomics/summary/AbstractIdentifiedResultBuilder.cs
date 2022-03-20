using RCPA.Gui;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractIdentifiedResultBuilder : ProgressClass, IIdentifiedResultBuilder
  {
    public AbstractIdentifiedResultBuilder()
    { }

    #region IIdentifiedResultBuilder Members

    public IIdentifiedResult Build(List<IIdentifiedProteinGroup> groups)
    {
      IdentifiedResult result = new IdentifiedResult();

      result.AddRange(groups);
      result.Sort();
      result.BuildGroupIndex();

      if (FillSequence(result))
      {
        AveragePeptideMassCalculator massCalc = new AveragePeptideMassCalculator(new Aminoacids(), Atom.H.AverageMass, Atom.H.AverageMass + Atom.O.AverageMass);

        Progress.SetMessage("Calculating coverage and mass weight ...");
        foreach (IIdentifiedProteinGroup group in result)
        {
          foreach (IIdentifiedProtein protein in group)
          {
            if (!string.IsNullOrEmpty(protein.Sequence))
            {
              try
              {
                protein.CalculateCoverage();
                protein.MolecularWeight = massCalc.GetMass(protein.Sequence);
              }
              catch (Exception ex)
              {
                var error = "Calculating coverage and mass weight error:" + ex.Message;
                Progress.SetMessage(error);
                Console.Error.WriteLine(error);
              }
            }
          }
        }
      }

      return result;
    }

    protected abstract bool FillSequence(IIdentifiedResult result);

    #endregion
  }
}
