using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification
{
  public interface IQuantificationItemUpdate
  {
    void Update(object sender, UpdateQuantificationItemEventArgs e);
  }
}
