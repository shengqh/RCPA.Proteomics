using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Numerics
{
  public interface IOutlierDetector
  {
    List<int> Detect(IEnumerable<double> values);
  }
}
