using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class UpdateMRMPairedProductIonEventArgs : EventArgs
  {
    public SrmPairedProductIon Item { get; private set; }

    public SrmViewOption ViewOption { get; private set; }

    public UpdateMRMPairedProductIonEventArgs(SrmPairedProductIon item, SrmViewOption viewOption)
      : base()
    {
      this.Item = item;
      this.ViewOption = viewOption;
    }
  }

  public delegate void UpdateMRMPairedProductIonEventHandler(object sender, UpdateMRMPairedProductIonEventArgs e);
}
