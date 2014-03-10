using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Image
{
  public interface IMatcher
  {
    void Match(IIdentifiedPeptideResult sr);
  }
}
