using System.Collections.Generic;

namespace RCPA.Numerics
{
  public interface IOutlierDetector
  {
    List<int> Detect(IEnumerable<double> values);
  }
}
