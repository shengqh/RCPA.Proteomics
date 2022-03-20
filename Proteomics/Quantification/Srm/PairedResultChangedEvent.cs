using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class PairedResultChangedEventArgs : EventArgs
  {
    public List<SrmPairedResult> PairedResult { get; set; }

    public SrmFileItemList ItemList { get; set; }
  }

  public delegate void PairedResultChangedEventHandler(object sender, PairedResultChangedEventArgs e);
}
