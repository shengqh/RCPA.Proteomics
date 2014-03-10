using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.SmallMolecule
{
  public delegate void UpdateSmallMoleculeFilePeakEvent(object sender, UpdateSmallMoleculeFilePeakEventArgs e);

  public class UpdateSmallMoleculeFilePeakEventArgs : EventArgs
  {
    public string Peak { get; private set; }

    public UpdateSmallMoleculeFilePeakEventArgs(string peak)
      : base()
    {
      this.Peak = peak;
    }
  }
}
