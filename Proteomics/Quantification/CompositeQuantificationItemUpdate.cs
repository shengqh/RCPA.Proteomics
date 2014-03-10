using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Drawing;

namespace RCPA.Proteomics.Quantification
{
  public class CompositeQuantificationItemUpdate : IQuantificationItemUpdate
  {
    public List<IQuantificationItemUpdate> Updates { get; private set; }

    public CompositeQuantificationItemUpdate()
    {
      Updates = new List<IQuantificationItemUpdate>();
    }

    #region IQuantificationItemUpdate Members

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      foreach (var item in Updates)
      {
        item.Update(sender, e);
      }
    }

    #endregion
  }
}
