using System;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class ComponentChangedEventArgs : EventArgs
  {
    public CompoundItem Precursor { get; set; }
  }

  public delegate void ComponentChangedEventHandler(object sender, ComponentChangedEventArgs e);
}
