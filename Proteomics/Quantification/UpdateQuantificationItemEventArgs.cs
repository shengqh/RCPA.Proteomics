using System;

namespace RCPA.Proteomics.Quantification
{
  public class UpdateQuantificationItemEventArgs : EventArgs
  {
    public object Option { get; set; }

    public object Item { get; set; }

    public UpdateQuantificationItemEventArgs(object option, object item)
      : base()
    {
      this.Option = option;
      this.Item = item;
    }
  }
}
