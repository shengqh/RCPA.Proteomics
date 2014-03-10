using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroupMultipleLineWriter : IIdentifiedProteinGroupWriter
  {
    #region IIdentifiedProteinGroupWriter Members

    public LineFormat<IIdentifiedProtein> ProteinFormat { get; set; }

    public void WriteToStream(StreamWriter sw, IIdentifiedProteinGroup mpg)
    {
      for (int proteinIndex = 0; proteinIndex < mpg.Count; proteinIndex++)
      {
        IIdentifiedProtein mpro = mpg[proteinIndex];
        sw.WriteLine("${0}-{1}{2}",
          mpg.Index,
          proteinIndex + 1,
          ProteinFormat.GetString(mpro));
      }
    }

    #endregion
  }
}
