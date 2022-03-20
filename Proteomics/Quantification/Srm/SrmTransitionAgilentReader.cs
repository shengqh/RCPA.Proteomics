using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmTransitionAgilentReader : IFileReader<List<SrmTransition>>
  {
    #region IFileReader<List<SrmTransition>> Members

    public List<SrmTransition> ReadFromFile(string fileName)
    {
      var format = new SrmTransitionAgilentFormat();
      var result = format.ReadFromFile(fileName);
      return result.ConvertAll(m => m as SrmTransition).ToList();
    }

    #endregion

    public override string ToString()
    {
      return "Agilent Text Format";
    }
  }
}
