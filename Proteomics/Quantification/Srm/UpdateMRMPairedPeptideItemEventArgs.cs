using System;

namespace RCPA.Proteomics.Quantification.Srm
{
  public enum DisplayType { FullSize, FullHeight, PerfectSize };

  public class UpdateMRMPairedPeptideItemEventArgs : EventArgs
  {
    public SrmPairedPeptideItem Item { get; private set; }

    public SrmViewOption ViewOption { get; private set; }

    public UpdateMRMPairedPeptideItemEventArgs(SrmPairedPeptideItem item, SrmViewOption viewOption)
      : base()
    {
      this.Item = item;
      this.ViewOption = viewOption;
    }
  }

  public delegate void UpdateMRMPairedPeptideItemEventHandler(object sender, UpdateMRMPairedPeptideItemEventArgs e);
}
