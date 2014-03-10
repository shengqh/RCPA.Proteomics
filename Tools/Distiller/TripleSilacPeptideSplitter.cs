using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics;

namespace RCPA.Tools.Distiller
{
  public class TripleSilacPeptideSplitter : AbstractThreadFileProcessor
  {
    private MascotModificationItem staticModification;
    private MascotModificationItem commonDynamicModification;
    private IEnumerable<MascotModificationItem> dynamicModifications;

    public TripleSilacPeptideSplitter(MascotModificationItem staticModification, MascotModificationItem commonDynamicModification, IEnumerable<MascotModificationItem> dynamicModifications)
    {
      this.staticModification = staticModification;
      this.commonDynamicModification = commonDynamicModification;
      this.dynamicModifications = dynamicModifications;


    }
    /*
    public IPeptideMassCalculator GetPeptideMassCalculator(MascotModificationItem dynamicModification)
    {
      bool isMono = true;

      var aas = new Aminoacids();
      staticModification.ForEach(m => aas[aas].ResetMass(aas[m].MonoMass + staticModifications[aa], aas[aa].AverageMass + staticModifications[aa]);
      }

      var diff = new[] { '*', '#', '@', '^', '~', '$' };
      int i = 0;
      foreach (double mod in Diff_search_options.Values)
      {
        aas[diff[i++]].ResetMass(mod, mod);
      }

      double nterm = isMono ? Atom.H.MonoMass : Atom.H.AverageMass;
      double cterm = isMono ? Atom.H.MonoMass + Atom.O.MonoMass : Atom.H.AverageMass + Atom.O.AverageMass;

      if (this.term_diff_search_options.First != 0.0 || this.term_diff_search_options.Second != 0.0)
      {
        throw new Exception(
          "Term dynamic modification has not been implemented into this function, call author to fix it.");
      }

      IPeptideMassCalculator result;
      if (isMono)
      {
        result = new MonoisotopicPeptideMassCalculator(aas, nterm, cterm);
      }
      else
      {
        result = new AveragePeptideMassCalculator(aas, nterm, cterm);
      }

      return result;
    }
    */
    public override IEnumerable<string> Process(string fileName)
    {
      var format = new MascotPeptideTextFormat();
      var spectra = format.ReadFromFile(fileName);

      return null;

    }
  }
}
