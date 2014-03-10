using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Image
{
  public interface INeutralLossType
  {
    string Name { get; }

    double Mass { get; }

    bool CanMultipleLoss { get; }

    int Count { get; }
  }
}
